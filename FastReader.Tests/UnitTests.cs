namespace FastReader.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTests
    {
        private const string IDPropertyName = "ID";

        private const string SazbaPropertyName = "Sazba";

        private const string DruhSazbyPropertyName = "DruhSazby";

        private const string PoznamkaPropertyName = "Poznamka";

        [TestMethod]
        public void TestRowWrite_FastRowRead()
        {
            var row = new Row(CreateTestMetaData());
            var fastRow = row.AsFastRow<SazbaDPHFastRow>();

            row.SetValue(IDPropertyName, 11);
            row.SetValue(SazbaPropertyName, 21);
            row.SetValue(DruhSazbyPropertyName, DruhSazbyDPH.Zakladni);
            row.SetValue(PoznamkaPropertyName, "poznamka");

            Assert.AreEqual(fastRow.ID, 11);
            Assert.AreEqual(fastRow.Sazba, 21);
            Assert.AreEqual(fastRow.DruhSazby, DruhSazbyDPH.Zakladni);
            Assert.AreEqual(fastRow.Poznamka, "poznamka");
        }

        [TestMethod]
        public void TestFastRowWrite_RowRead()
        {
            var row = new Row(CreateTestMetaData());
            var fastRow = row.AsFastRow<SazbaDPHFastRow>();

            fastRow.ID = 11;
            fastRow.Sazba = 21;
            fastRow.DruhSazby = DruhSazbyDPH.Zakladni;
            fastRow.Poznamka = "poznamka";

            Assert.AreEqual(row.GetValue(IDPropertyName), 11);
            Assert.AreEqual(row.GetValue(SazbaPropertyName), 21m);
            Assert.AreEqual(row.GetValue(DruhSazbyPropertyName), DruhSazbyDPH.Zakladni);
            Assert.AreEqual(row.GetValue(PoznamkaPropertyName), "poznamka");
        }

        [TestMethod]
        [ExpectedException(typeof(TypeGeneratorException))]
        public void TestNoAbstract()
        {
            var row = new Row(CreateTestMetaData());
            var fastRow = row.AsFastRow<SazbaDPHFastRowNoAbstract>();
        }

        [TestMethod]
        [ExpectedException(typeof(TypeGeneratorException))]
        public void TestTypeMismatch()
        {
            var row = new Row(CreateTestMetaData());
            var fastRow = row.AsFastRow<SazbaDPHFastRowTypeMismatch>();
        }

        [TestMethod]
        [ExpectedException(typeof(TypeGeneratorException))]
        public void TestInvalidCtor()
        {
            var row = new Row(CreateTestMetaData());
            var fastRow = row.AsFastRow<SazbaDPHFastRowInvalidCtor>();
        }

        private static TypeMetaData CreateTestMetaData()
        {
            var result = new TypeMetaData(
                "SazbaDPH",
                new[]
                {
                    new Property(IDPropertyName, typeof(int)),
                    new Property(SazbaPropertyName, typeof(decimal?)),
                    new Property(DruhSazbyPropertyName, typeof(DruhSazbyDPH)),
                    new Property(PoznamkaPropertyName, typeof(string))
                });

            return result;
        }
    }
}