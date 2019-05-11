namespace FastReader.Tests
{
    using FastReader;

    public abstract class SazbaDPHFastRow : FastRowBase
    {
        public abstract int ID { get; set; }

        public abstract decimal? Sazba { get; set; }

        public abstract DruhSazbyDPH DruhSazby { get; set; }

        public abstract string Poznamka { get; set; }
    }
}