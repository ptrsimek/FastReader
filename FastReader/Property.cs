namespace FastReader
{
    using System;

    public sealed class Property
    {
        public Property(string name, Type type)
        {
            this.Name = name;
            this.Type = type;
        }

        public string Name { get; }

        public Type Type { get; }

        internal int ValueIndex { get; set; }
    }
}