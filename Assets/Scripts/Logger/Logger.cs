using System;
using System.IO;
using UnityEngine;

namespace TriPeaks
{
    /// <summary>
    /// Helps with saving logs to persistent path
    /// </summary>
    public class Logger : ILogHandler
    {
        private readonly FileStream _fileStream;
        private readonly StreamWriter _streamWriter;
        private readonly ILogHandler _defaultLogHandler = Debug.unityLogger.logHandler;

        public Logger()
        {
            string filePath = Application.persistentDataPath + "/Logs.txt";

            if (File.Exists(filePath))
                File.Delete(filePath);

            _fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            _streamWriter = new StreamWriter(_fileStream);

            // Replace the default debug log handler
            Debug.unityLogger.logHandler = this;
        }

        ~Logger()
        {
            _streamWriter.Close();
            _fileStream.Close();
        }

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            _streamWriter.WriteLine(string.Format(format, args));
            _streamWriter.Flush();
            _defaultLogHandler.LogFormat(logType, context, format, args);
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            _defaultLogHandler.LogException(exception, context);
        }
    }
}