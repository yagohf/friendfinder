using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.IO;

namespace Yagohf.Cubo.FriendFinder.Api
{
    public class Program
    {
        private const string ARQUIVO_CONFIGURACAO = "appsettings.json";
        private const string CONNECTION_STRING_LOGDB = "LogDB";

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile(ARQUIVO_CONFIGURACAO, optional: false, reloadOnChange: true)
          .AddEnvironmentVariables()
          .Build();


        public static void Main(string[] args)
        {
            ConfigurarSerilog();

            try
            {
                Log.Information("Main - Startando aplicação...");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Main - Aplicação encontrou uma exceção e encerrou a execução...");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>()
                .UseConfiguration(Configuration)
                .Build();

            return webHostBuilder;
        }

        #region [ Auxiliares ]
        private static void ConfigurarSerilog()
        {
            var serilogColumnOptions = new ColumnOptions();
            //Remover coluna de XML.
            serilogColumnOptions.Store.Remove(StandardColumn.Properties);

            //Adicionar coluna com objeto JSON.
            serilogColumnOptions.Store.Add(StandardColumn.LogEvent);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(
                    connectionString: Configuration.GetConnectionString(CONNECTION_STRING_LOGDB),
                    tableName: Configuration.GetSection("Serilog:LogTableName").Value ?? "Log",
                    autoCreateSqlTable: false,
                    columnOptions: serilogColumnOptions)
                .CreateLogger();
        }
        #endregion
    }
}
