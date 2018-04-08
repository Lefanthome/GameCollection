namespace GameCollection.ElasticSearch.Interface
{
    public interface IIndex
    {
        void CreateIndex();
        void DeleteIndex();
        void ReIndex(string src, string dest);
    }
}
