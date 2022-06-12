namespace ITM.Service.DataImporter.Workers
{
    public interface IForm4ImportersRespository
    {
        Form4Importer this[string id] { get; set; }

        void Remove(string id);

        void Add(string id, Form4Importer importer);
    }
}
