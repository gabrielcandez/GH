using MySqlConnector;

namespace GH.Application;

public sealed class DatabaseContext : DbContext
{
    private readonly string _database;
    private readonly string _host;
    private readonly string _password;
    private readonly string _user;

    public DatabaseContext(string host, string user, string password, string database)
    {
        _host = host;
        _user = user;
        _password = password;
        _database = database;
    }

    public DbSet<Post> Posts { get; private set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new MySqlConnectionStringBuilder
        {
            Server = _host,
            UserID = _user,
            Password = _password,
            Database = _database
        }.ToString();

        var version = new MySqlServerVersion("8.0.0");

        optionsBuilder.UseMySql(connectionString, version);
    }
}