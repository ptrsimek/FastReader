namespace FastReader
{
    using System;
    using System.Runtime.CompilerServices;

    public sealed class Row
    {
        private readonly object[] values;

        private FastRowBase fastRow;

        public Row(TypeMetaData metaData)
        {
            this.MetaData = metaData;

            this.values = new object[metaData.Properties.Count];
        }

        public TypeMetaData MetaData { get; }

        public T AsFastRow<T>() where T : FastRowBase
        {
            var result = Unsafe.As<T>(this.fastRow);
            if (result == null)
            {
                result = FastRowTypesHolder.CreateFastRow<T>(this);
                this.fastRow = result;
            }

            return result;
        }

        public object GetValue(string propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var property = this.MetaData.Properties.GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException(string.Format(Language.PropertyWasNotFound, propertyName));
            }

            var result = this.values[property.ValueIndex];
            return result;
        }

        public void SetValue(string propertyName, object value)
        {
            var property = this.MetaData.Properties.GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException(string.Format(Language.PropertyWasNotFound, propertyName));
            }

            if (value == null)
            {
                this.values[property.ValueIndex] = null;
                return;
            }

            var v = NormalizeValue(value, property.Type);

            this.values[property.ValueIndex] = v;
        }

        private static object NormalizeValue(object value, Type expectedType)
        {
            var valueType = value.GetType();

            if (valueType == expectedType)
            {
                return value;
            }

            if (!expectedType.IsValueType)
            {
                throw CreateException();
            }

            var underlyingType = Nullable.GetUnderlyingType(expectedType);
            if (underlyingType == null)
            {
                throw CreateException();
            }

            if (underlyingType == expectedType)
            {
                return true;
            }

            var v = Convert.ChangeType(value, underlyingType);
            return v;

            ArgumentException CreateException()
            {
                return new ArgumentException(string.Format(Language.UnexpectedValueType, valueType.FullName, expectedType.FullName));
            }
        }

        internal T GetValueValueType<T>(int index) where T : struct
        {
            var value = this.values[index];
            if (value == null)
            {
                return default;
            }

            return (T) value;
        }

        internal T? GetValueValueTypeNullable<T>(int index) where T : struct
        {
            var value = this.values[index];
            if (value == null)
            {
                return null;
            }

            return (T) value;
        }

        internal T GetValueRef<T>(int index) where T : class
        {
            var value = this.values[index];
            return (T) value;
        }

        internal void SetValue<T>(int index, T value)
        {
            if (value == null)
            {
                this.values[index] = null;
                return;
            }

            this.values[index] = value;
        }
    }
}