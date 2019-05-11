namespace FastReader
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    internal sealed class FastRowTypes
    {
        private readonly AssemblyBuilder asm;

        private readonly ModuleBuilder moduleBuilder;

        private readonly Dictionary<Key, object> rowFactory = new Dictionary<Key, object>();

        public FastRowTypes()
        {
            var appDomain = AppDomain.CurrentDomain;
            var asmName = new AssemblyName(Constants.AssemblyName);
            this.asm = appDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);
            this.moduleBuilder = this.asm.DefineDynamicModule(asmName.Name, $"{asmName.Name}.dll");
        }

        public void Save()
        {
            this.asm.Save($"{Constants.AssemblyName}.dll");
        }

        public T CreateFastRow<T>(Row row) where T : FastRowBase
        {
            var key = new Key(row.MetaData.Name, typeof(T));

            Func<Row, T> factory;

            if (this.rowFactory.TryGetValue(key, out var o))
            {
                factory = (Func<Row, T>) o;
            }
            else
            {
                var te = new FastRowTypeGenerator(this.moduleBuilder, typeof(T), row.MetaData);
                var type = te.CreateType();

                factory = CreateRowFactoryMethod<T>(type);
                this.rowFactory[key] = factory;
            }

            var result = factory(row);
            return result;
        }

        private static Func<Row, T> CreateRowFactoryMethod<T>(Type type) where T : FastRowBase
        {
            var ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)[0];
            var setRow = type.GetMethod(nameof(FastRowBase.SetRow), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            var m = new DynamicMethod(
                string.Empty,
                typeof(T),
                new[]
                {
                    typeof(Row)
                },
                true);

            var il = m.GetILGenerator();

            il.Emit(OpCodes.Newobj, ctor);
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Ldarg_0); // Row            
            // ReSharper disable once AssignNullToNotNullAttribute
            il.Emit(OpCodes.Callvirt, setRow);
            il.Emit(OpCodes.Ret);

            var result = (Func<Row, T>) m.CreateDelegate(typeof(Func<Row, T>));
            return result;
        }

        private struct Key : IEquatable<Key>
        {
            private readonly string typeName;

            private readonly Type fastType;

            public Key(string typeName, Type fastType)
            {
                this.typeName = typeName;
                this.fastType = fastType;
            }

            public bool Equals(Key other)
            {
                return string.Equals(this.typeName, other.typeName) && this.fastType == other.fastType;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                return obj is Key other && this.Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((this.typeName != null ? this.typeName.GetHashCode() : 0) * 397) ^ (this.fastType != null ? this.fastType.GetHashCode() : 0);
                }
            }
        }
    }
}