namespace FastReader
{
    public static class FastRowTypesHolder
    {
        private static readonly FastRowTypes Types = new FastRowTypes();

        internal static T CreateFastRow<T>(Row row) where T : FastRowBase
        {
            var result = Types.CreateFastRow<T>(row);
            return result;
        }

        public static void Save()
        {
            Types.Save();
        }
    }
}