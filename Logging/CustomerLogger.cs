namespace MinhaApi.Logging
{
    public class CustomerLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration configuration;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            configuration = config;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == configuration.LogLevel;
        }

        public IDisposable? BeginScope<TState>(TState state)
        {
            return null;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = $"{logLevel.ToString()} : {eventId.Id} - {formatter(state, exception)}";
            WriteTextInFile(message);
        }

        private void WriteTextInFile(string message)
        {
            string filePath = @"C:\Users\Thiago\Documents\Cursos\Dev\C#\testes\aulas\MinhaApi\log.txt";

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                try
                {
                    sw.WriteLine(message);
                    sw.Close();
                } catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
