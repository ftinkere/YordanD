using LinqToDB;
using LinqToDB.Data;
using YordanApi.Dbos;

namespace YordanApi;

public class AppDataConnection(DataOptions<AppDataConnection> options) : DataConnection(options.Options) {
    public ITable<LanguageDbo> Languages => this.GetTable<LanguageDbo>();
    public ITable<ImageDbo> Images => this.GetTable<ImageDbo>();
    
    
}