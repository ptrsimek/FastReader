namespace FastReader
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    internal sealed class FastRowTypeGenerator
    {
        private readonly Type baseType;

        private readonly TypeMetaData metaData;

        private readonly ModuleBuilder moduleBuilder;

        private MethodInfo getValueRefMethod;

        private MethodInfo getValueValueNullableMethod;

        private MethodInfo getValueValueTypeMethod;

        private MethodInfo setValueMethod;

        private TypeBuilder typeBuilder;

        public FastRowTypeGenerator(ModuleBuilder moduleBuilder, Type baseType, TypeMetaData metaData)
        {
            this.moduleBuilder = moduleBuilder;
            this.baseType = baseType;
            this.metaData = metaData;
        }

        public Type CreateType()
        {
            this.getValueValueTypeMethod = this.baseType.GetMethod(nameof(FastRowBase.GetValueValueType), BindingFlags.Instance | BindingFlags.NonPublic);
            this.getValueValueNullableMethod = this.baseType.GetMethod(nameof(FastRowBase.GetValueValueNullable), BindingFlags.Instance | BindingFlags.NonPublic);
            this.getValueRefMethod = this.baseType.GetMethod(nameof(FastRowBase.GetValueRef), BindingFlags.Instance | BindingFlags.NonPublic);
            this.setValueMethod = this.baseType.GetMethod(nameof(FastRowBase.SetValue), BindingFlags.Instance | BindingFlags.NonPublic);

            var typeName = $"{Constants.FastRowsNamespace}.{this.baseType.FullName}";

            this.typeBuilder = this.moduleBuilder.DefineType(typeName, TypeAttributes.Public, this.baseType);

            this.CreateCtor();

            foreach (var p in this.metaData.Properties)
            {
                this.CreateProperty(p);
            }

            var result = this.typeBuilder.CreateType();
            return result;
        }

        private void CreateProperty(Property property)
        {
            var baseProperty = this.baseType.GetProperty(property.Name);
            if (baseProperty == null)
            {
                return;
            }

            if (baseProperty.PropertyType != property.Type)
            {
                throw new TypeGeneratorException(string.Format(Language.TypeMismatch, baseProperty.PropertyType.FullName, property.Type.FullName));
            }

            var propertyBuilder = this.typeBuilder.DefineProperty(baseProperty.Name, PropertyAttributes.None, baseProperty.PropertyType, null);

            this.CreateGetter(propertyBuilder, property, baseProperty);
            this.CreateSetter(propertyBuilder, property, baseProperty);
        }

        private void CreateGetter(PropertyBuilder propertyBuilder, Property property, PropertyInfo baseProperty)
        {
            var baseGetMethod = baseProperty.GetGetMethod();
            if (baseGetMethod == null)
            {
                return;
            }

            if (!baseGetMethod.IsAbstract)
            {
                throw new TypeGeneratorException(string.Format(Language.GetMethodIsNotAbstract, baseProperty.Name, this.baseType.FullName));
            }

            var getter = this.typeBuilder.DefineMethod(
                "get_" + baseProperty.Name,
                MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.Virtual,
                baseProperty.PropertyType,
                Type.EmptyTypes);
            var il = getter.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.EmitLdc(property.ValueIndex);

            var getMethod = this.GetGetValueMethod(baseProperty);
            il.Emit(OpCodes.Callvirt, getMethod);
            il.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getter);
        }

        private MethodInfo GetGetValueMethod(PropertyInfo baseProperty)
        {
            var propertyType = baseProperty.PropertyType;
            var underlyingType = Nullable.GetUnderlyingType(propertyType);
            var isNullable = underlyingType != null;

            if (isNullable)
            {
                return this.getValueValueNullableMethod.MakeGenericMethod(underlyingType);
            }

            if (propertyType.IsValueType)
            {
                return this.getValueValueTypeMethod.MakeGenericMethod(propertyType);
            }

            return this.getValueRefMethod.MakeGenericMethod(propertyType);
        }

        private void CreateSetter(PropertyBuilder propertyBuilder, Property property, PropertyInfo baseProperty)
        {
            var baseSetMethod = baseProperty.GetSetMethod();
            if (baseSetMethod == null)
            {
                return;
            }

            if (!baseSetMethod.IsAbstract)
            {
                throw new TypeGeneratorException(string.Format(Language.SetMethodIsNotAbstract, baseProperty.Name, this.baseType.FullName));
            }

            var setter = this.typeBuilder.DefineMethod(
                "set_" + baseProperty.Name,
                MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.Virtual,
                null,
                new[]
                {
                    baseProperty.PropertyType
                });

            var il = setter.GetILGenerator();

            var setMethod = this.setValueMethod.MakeGenericMethod(baseProperty.PropertyType);
            il.Emit(OpCodes.Ldarg_0);
            il.EmitLdc(property.ValueIndex);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Callvirt, setMethod);
            il.Emit(OpCodes.Ret);

            propertyBuilder.SetSetMethod(setter);
        }

        private void CreateCtor()
        {
            var ctor = this.typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);

            var baseCtor = this.baseType.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                CallingConventions.Any,
                Type.EmptyTypes,
                null);

            if (baseCtor == null)
            {
                throw new TypeGeneratorException(string.Format(Language.SuitableCtorNotFound, this.baseType.FullName));
            }

            var il = ctor.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0); // this
            il.Emit(OpCodes.Call, baseCtor);
            il.Emit(OpCodes.Ret);
        }
    }
}