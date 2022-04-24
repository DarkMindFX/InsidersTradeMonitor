namespace ITM.Service.DataImporter
{

    public class ImporterParams
    {
        public string ProcessID
        {
            get; set;
        }

        public string CIK
        {
            get; set;
        }

        public DateTime DateFrom
        {
            get; set;
        }

        public DateTime DateTo
        {
            get; set;
        }
    }

    public class Importer
    {
        protected void ImporterThread(object importerParams)
        {

        }
    }
}
