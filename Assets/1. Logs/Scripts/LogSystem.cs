using System;
using System.Diagnostics;
using System.IO;
using _1._Logs.Lists;
using _1._Logs.Scripts.Interfaces;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace _1._Logs.Scripts
{
    public class LogSystem : ILogSystem
    {
        private const string PATH_LOG_DEV =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/1. Logs/Logs";

        private readonly string _pathLog = Path.Combine(Application.dataPath, "DataPlayer");

        public void AddLog(LogsType logsType, string text)
        {
            var nameFile = logsType switch
            {
                LogsType.Unknown => "Unknown.txt",
                LogsType.System => "System.txt",
                LogsType.Information => "Information.txt",
                LogsType.Error => "Error.txt",
                LogsType.Setting => "Setting.txt",
                LogsType.Loading => "Loading.txt",
                LogsType.Save => "Player.txt",
                LogsType.LoadingSave => "LoadingSave.txt",
                LogsType.Level => "Level.txt",
                _ => "Error.txt"
            };
#if UNITY_EDITOR
            File.AppendAllText(Path.Combine(PATH_LOG_DEV, nameFile), $"{DateTime.Now} | {text} {Environment.NewLine}");
#else
            File.AppendAllText(Path.Combine(_pathLog, nameFile), $"{DateTime.Now} | {text} {Environment.NewLine}");
#endif
            Debug.Log($"{logsType} | {text}");
        }
    }
}