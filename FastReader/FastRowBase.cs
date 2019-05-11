namespace FastReader
{
    using System.Runtime.CompilerServices;

    public abstract class FastRowBase
    {
        private Row row;

        protected FastRowBase()
        {
        }

        internal void SetRow(Row r)
        {
            this.row = r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal T GetValueValueType<T>(int index) where T : struct
        {
            var result = this.row.GetValueValueType<T>(index);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal T? GetValueValueNullable<T>(int index) where T : struct
        {
            var result = this.row.GetValueValueTypeNullable<T>(index);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal T GetValueRef<T>(int index) where T : class
        {
            var result = this.row.GetValueRef<T>(index);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal void SetValue<T>(int index, T value)
        {
            this.row.SetValue(index, value);
        }
    }
}