using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace SerilogMsSqlSinkTrain
{
    internal class Program
    {
        //private static ILogger _diagnosticLogger;
        //private static ILogger _errorLogger;
        //private static ILogger _performanceLogger;
        //private static ILogger _usageLogger;
        static void Main(string[] args)
        {
            //var t = Path.Combine(Directory.GetCurrentDirectory(), "self.log");
            //var file = File.CreateText(Path.Combine(Directory.GetCurrentDirectory(), "self.log"));

            //Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(file));
            Serilog.Debugging.SelfLog.Enable(msg =>
            {
                Debug.Print(msg);
                Debugger.Break();
            });

            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=SerilogTest;Trusted_Connection=True;MultipleActiveResultSets=true";
            //_diagnosticLogger = CreateLogger("DiagnosticLogs", connectionString);
            //_errorLogger = CreateLogger("ErrorLogs", connectionString);
            //_performanceLogger = CreateLogger("PerformanceLogs", connectionString);
            //_usageLogger = CreateLogger("UsageLogs", connectionString);
            //LogUsage(new MyLog() { UserId = "abc", UserEmail = "jan@kowalski.pl", Model = "jakiś tam model", Message = "jakaś tam wiadomość", Location = "B-B", Layer = "Controller", HostName = "pawwil01-lt", ElapsedMilliseconds = 100, CorrelationId = "?" });
            Log.Logger = CreateLogger("UsageLogs", connectionString);
            var kom = "Komunikat";
            Exception ex = new Exception("TEST");
            Log.Logger
                .ForContext("wcr_Komunikat", kom)
                .ForContext("wcr_RozszerzenieWersja", "1")
                .ForContext("wcr_RozszerzenieNazwa", "22")
                .ForContext("wcr_OperatorId", 10)
                .Error(ex, kom);
            Console.WriteLine("Koniec");
            Console.ReadLine();
        }

        //public static void LogDiagnostic(MyLog log)
        //{
        //    var shouldWrite = Convert.ToBoolean(Environment.GetEnvironmentVariable("LOG_DIAGNOSTICS"));
        //    if (!shouldWrite) return;

        //    _diagnosticLogger.Write(LogEventLevel.Information, "{@MyLog}", log);
        //}

        //public static void LogError(MyLog log)
        //{
        //    log.Message = GetMessageFromException(log.Exception);
        //    _errorLogger.Write(LogEventLevel.Information, "{@MyLog}", log);
        //}

        //public static void LogPerformance(MyLog log) =>
        //    _performanceLogger.Write(LogEventLevel.Information, "{@MyLog}", log);

        //public static void LogUsage(MyLog log) =>
        //    _usageLogger.Write(LogEventLevel.Information, "{@MyLog}", log);

        private static string GetMessageFromException(Exception exception)
        {
            while (true)
            {
                if (exception.InnerException == null) return exception.Message;
                exception = exception.InnerException;
            }
        }

        private static ILogger CreateLogger(string name, string connectionString) =>
            new LoggerConfiguration()
                //.WriteTo.File(path: Environment.GetEnvironmentVariable(name))
                .WriteTo.MSSqlServer(connectionString,
                    sinkOptions: GetSinkOptions(name),
                    columnOptions: GetColumnOptions())
                .CreateLogger();

        private static ColumnOptions GetColumnOptions()
        {
            var columnOptions = new ColumnOptions();
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.AdditionalColumns = new List<SqlColumn>
            {
                new SqlColumn { ColumnName = "wcr_OperatorId", DataType = SqlDbType.Int, AllowNull = true },
                new SqlColumn { ColumnName = "wcr_RozszerzenieNazwa", DataType = SqlDbType.VarChar, DataLength = 150 },
                new SqlColumn { ColumnName = "wcr_RozszerzenieWersja", DataType = SqlDbType.VarChar, DataLength = 50 },
            };
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Id.ColumnName = "wcr_Id";
            columnOptions.Message.ColumnName = "wcr_Komunikat";
            columnOptions.Level.ColumnName = "wcr_Rodzaj";
            columnOptions.TimeStamp.ColumnName = "wcr_Data";
            columnOptions.Exception.ColumnName = "wcr_Wyjatek";

            return columnOptions;
        }

        private static SinkOptions GetSinkOptions(string name)
        {
            return new SinkOptions
            {
                TableName = "bb_wc_RozszerzenieKomunikaty",
                SchemaName = "dbo",
                AutoCreateSqlTable = false,
                BatchPostingLimit = 1,
            };
        }
    }
}
