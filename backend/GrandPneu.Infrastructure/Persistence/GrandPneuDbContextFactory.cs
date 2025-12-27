using GrandPneu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DotNetEnv;

namespace GrandPneu.Infrastructure.Persistence;

public class GrandPneuDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Carrega variáveis do .env
        Env.Load();

        var host = Env.GetString("DB_HOST") ?? throw new InvalidOperationException("DB_HOST não encontrado");
        var port = Env.GetString("DB_PORT") ?? throw new InvalidOperationException("DB_PORT não encontrado");
        var db   = Env.GetString("DB_NAME") ?? throw new InvalidOperationException("DB_NAME não encontrado");
        var user = Env.GetString("DB_USER") ?? throw new InvalidOperationException("DB_USER não encontrado");
        var pass = Env.GetString("DB_PASS") ?? throw new InvalidOperationException("DB_PASS não encontrado");

        // conectaão com o banco de dados para host externo (Docker local ou nuvem)
        // var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={pass}";
        // var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        // optionsBuilder.UseNpgsql(connectionString);

        //conexão com o banco de dados SQLite
        var dbPath = Env.GetString("SQLITE_PATH") ?? "grandpneu.db";
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new AppDbContext(optionsBuilder.Options);
    }
}
