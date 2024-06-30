using BlazorDB;
namespace Carlton.Core.BrowserStroage;

public class IndexDbService<TRecord>(IBlazorDbFactory blazorDbFactory, string dbName, string storeName) 
    : IIndexDbService<TRecord>
{
    private readonly IBlazorDbFactory _dbFactory = blazorDbFactory;
    private readonly string _dbName = dbName;
    private readonly string _storeName = storeName;
    private IndexedDbManager _manager;

    public async Task<TRecord> GetRecordById<TKey>(TKey id)
    {
        try
        {
            var manager = await GetDbManager();
            return await manager.GetRecordByIdAsync<TKey, TRecord>(_storeName, id);
        }
        catch (Exception ex)
        {
            //swallow for now
            throw;
        }
    }
    public async Task<IEnumerable<TRecord>> GetRecordsByIndex(string index, object filter)
    {
        try
        {
            var manager = await GetDbManager();
            var result = await manager.Where<TRecord>(_storeName, index, filter);
            return result.ToList();
        }
        catch (Exception ex)
        {
            //swallow for now
            throw;
        }
    }

    public async Task AddRecord(TRecord record)
    {
        try
        {
            //Get IndexDB reference
            var manager = await GetDbManager();

            //Commit State to IndexDB
            var result = await manager.AddRecord(new StoreRecord<TRecord>
            {
                StoreName = _storeName,
                Record = record
            });
        }
        catch (Exception ex)
        {
            //swallow for now
        }
    }

    public async Task BulkAddRecords(IEnumerable<TRecord> records)
    {
        try
        {
            //Get IndexDB reference
            var manager = await GetDbManager();

            //Commit State to IndexDB
            var result = await manager.BulkAddRecord(_storeName, records);
        }
        catch (Exception ex)
        {
            //swallow for now
        }
    }

    public async Task UpdateRecord<TKey>(TKey key, TRecord record)
    {
        try
        {
            //Get IndexDB reference
            var manager = await GetDbManager();

            var updateRecord = new UpdateRecord<TRecord>()
            {
                DbName = _dbName,
                StoreName = _storeName,
                Key = key,
                Record = record
            };

            //Commit State to IndexDB
            var result = await manager.UpdateRecord(updateRecord);
        }
        catch (Exception ex)
        {
            //swallow for now
        }
    }

    public async Task PutRecord<TKey>(TRecord record)
    {
        try
        {
            //Get IndexDB reference
            var manager = await GetDbManager();

            var storeRecord = new StoreRecord<TRecord>()
            {
                DbName = _dbName,
                StoreName = _storeName,
                Record = record
            };

            //Commit State to IndexDB
            var result = await manager.PutRecord(storeRecord);
        }
        catch (Exception ex)
        {
            //swallow for now
        }
    }

    public async Task DeleteRecord<TKey>(TKey key)
    {
        try
        {
            //Get IndexDB reference
            var manager = await GetDbManager();

            //Commit State to IndexDB
            var result = await manager.DeleteRecord(_storeName, key);
        }
        catch (Exception ex)
        {
            //swallow for now
        }
    }

    public async Task ClearStore()
    {
        try
        {
            //Get IndexDB reference
            var manager = await GetDbManager();

            //Commit State to IndexDB
            var result = await manager.ClearTable(_storeName);
        }
        catch (Exception ex)
        {
            //swallow for now
        }
    }

    private async Task<IndexedDbManager> GetDbManager()
    {
        _manager ??= await _dbFactory.GetDbManager(_dbName);
        return _manager;
    }
}
