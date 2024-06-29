namespace Carlton.Core.BrowserStroage;

public interface IIndexDbService<TRecord> : IReadOnlyIndexDbService<TRecord>
{
    public Task AddRecord(TRecord record);
    public Task BulkAddRecords(IEnumerable<TRecord> records);
    public Task UpdateRecord<TKey>(TKey key, TRecord record);
    public Task PutRecord<TKey>(TRecord record);
    public Task DeleteRecord<TKey>(TKey key);
    public Task ClearStore();
}
