namespace GameCollection.ElasticSearch.Interface
{
    public interface IDocument<T> where T : class
    {
        void Insert(T document);
        void Update(T document);
        void Delete(T document);
    }
}
