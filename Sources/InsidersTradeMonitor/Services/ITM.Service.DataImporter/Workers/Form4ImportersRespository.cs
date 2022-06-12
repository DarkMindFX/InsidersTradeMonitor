namespace ITM.Service.DataImporter.Workers
{
    public class Form4ImportersRespository : IForm4ImportersRespository
    {
        private readonly IDictionary<string, Form4Importer> _importers = null;

        public Form4ImportersRespository()
        {
            _importers = new Dictionary<string, Form4Importer>();
        }

        public Form4Importer this[string id] { get => _importers[id]; set => _importers[id] = value; }

        public void Add(string id, Form4Importer importer)
        {
            _importers.Add(id, importer);
        }

        public void Remove(string id)
        {
            _importers.Remove(id);
        }
    }
}
