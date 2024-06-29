namespace Carlton.Core.BrowserStroage;

public interface IReadOnlyIndexDbService<TRecord>
{
    public Task<IEnumerable<TRecord>> GetRecordsByIndex(string index, object filter);
    public Task<TRecord> GetRecordById<TKey>(TKey id);
}
