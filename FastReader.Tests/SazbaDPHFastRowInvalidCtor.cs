namespace FastReader.Tests
{
    using FastReader;

    public abstract class SazbaDPHFastRowInvalidCtor : FastRowBase
    {
        public SazbaDPHFastRowInvalidCtor(int dummy)
        {            
        }

        public abstract int ID { get; set; }

        public abstract decimal? Sazba { get; set; }

        public abstract DruhSazbyDPH DruhSazby { get; set; }

        public abstract string Poznamka { get; set; }
    }
}