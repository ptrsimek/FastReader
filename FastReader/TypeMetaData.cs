namespace FastReader
{
    using System.Collections.Generic;

    public sealed class TypeMetaData
    {
        public TypeMetaData(string name, IEnumerable<Property> properties)
        {
            this.Name = name;
            this.Properties = new PropertyCollection(properties);
        }

        public string Name { get; }

        public PropertyCollection Properties { get; }
    }
}