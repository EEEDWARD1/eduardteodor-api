using Npgsql;

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Database")
    ?? throw new InvalidOperationException("Database connection string is missing.");

await using var connection = new NpgsqlConnection(connectionString);
await connection.OpenAsync();

var runner = new MigrationRunner(connection, Path.Combine(AppContext.BaseDirectory, "Migrations"));

await runner.RunAsync();
