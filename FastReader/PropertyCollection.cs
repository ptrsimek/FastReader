namespace FastReader
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class PropertyCollection : IReadOnlyList<Property>
    {
        private readonly Property[] properties;

        private readonly Dictionary<string, Property> propertyIndexMap;

        public PropertyCollection(IEnumerable<Property> properties)
        {
            this.properties = properties.ToArray();
            this.propertyIndexMap = this.properties.ToDictionary(p => p.Name);

            for (int i = 0, len = this.properties.Length; i < len; i++)
            {
                this.properties[i].ValueIndex = i;
            }
        }

        public IEnumerator<Property> GetEnumerator()
        {
            return ((IEnumerable<Property>) this.properties).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count => this.properties.Length;

        public Property this[int index] => this.properties[index];

        public Property GetProperty(string propertyName)
        {
            var result = this.propertyIndexMap.TryGetValue(propertyName, out var temp) ? temp : null;
            return result;
        }
    }
}