namespace FastReader.Tests
{
    using FastReader;

    public abstract class SazbaDPHFastRowNoAbstract : FastRowBase
    {
        public abstract int ID { get; set; }

        public abstract decimal? Sazba { get; set; }

        public DruhSazbyDPH DruhSazby { get; set; }

        public abstract string Poznamka { get; set; }
    }
}