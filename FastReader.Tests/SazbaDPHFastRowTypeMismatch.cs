namespace FastReader.Tests
{
    using FastReader;

    public abstract class SazbaDPHFastRowTypeMismatch : FastRowBase
    {
        public abstract int ID { get; set; }

        public abstract int Sazba { get; set; }

        public abstract DruhSazbyDPH DruhSazby { get; set; }

        public abstract string Poznamka { get; set; }
    }
}