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
        private FileStream m_FileStream;
        private StreamWriter m_StreamWriter;
        private ILogHandler m_DefaultLogHandler = UnityEngine.Debug.unityLogger.logHandler;

        public Logger()
        {
            string filePath = Application.persistentDataPath + "/Logs.txt";

            m_FileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            m_StreamWriter = new StreamWriter(m_FileStream);

            // Replace the default debug log handler
            UnityEngine.Debug.unityLogger.logHandler = this;
        }

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            m_StreamWriter.WriteLine(String.Format(format, args));
            m_StreamWriter.Flush();
            m_DefaultLogHandler.LogFormat(logType, context, format, args);
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            m_DefaultLogHandler.LogException(exception, context);
        }
    }
}