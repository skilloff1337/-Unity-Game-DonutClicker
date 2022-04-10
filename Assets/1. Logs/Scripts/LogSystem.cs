using System;
using System.IO;
using _1._Logs.Lists;
using _1._Logs.Scripts.Interfaces;
using UnityEngine;

namespace _1._Logs.Scripts
{
    public class LogSystem : ILogSystem
    {
        private const string PATH_LOG = "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/1. Logs/Logs";
        public void AddLog(LogsType logsType, string text)
        {
            switch (logsType)
            {
                case LogsType.Unknown:
                    File.AppendAllText(PATH_LOG + "/Unknown.txt",$"{DateTime.Now} | {text} {Environment.NewLine}");
                    break;
                case LogsType.System:
                    File.AppendAllText(PATH_LOG + "/System.txt",$"{DateTime.Now} | {text} {Environment.NewLine}");
                    break;
                case LogsType.Information:
                    File.AppendAllText(PATH_LOG + "/Information.txt",$"{DateTime.Now} | {text} {Environment.NewLine}");
                    break;
                case LogsType.Error:
                    File.AppendAllText(PATH_LOG + "/Error.txt",$"{DateTime.Now} | {text} {Environment.NewLine}");
                    break;
                case LogsType.Setting:
                    File.AppendAllText(PATH_LOG + "/Setting.txt",$"{DateTime.Now} | {text} {Environment.NewLine}");
                    break;
                case LogsType.Loading:
                    File.AppendAllText(PATH_LOG + "/Loading.txt",$"{DateTime.Now} | {text} {Environment.NewLine}");
                    break;
                case LogsType.Save:
                    File.AppendAllText(PATH_LOG + "/Save.txt",$"{DateTime.Now} | {text} {Environment.NewLine}");
                    break;
                case LogsType.LoadingSave:
                    File.AppendAllText(PATH_LOG + "/LoadingSave.txt",$"{DateTime.Now} | {text} {Environment.NewLine}");
                    break;
                default:
                    File.AppendAllText(PATH_LOG + "/Error.txt",$"{DateTime.Now} | UNDEFINED TYPE: {logsType}, text: {text} {Environment.NewLine}");
                    Debug.LogError($"ERROR! UNDEFINED TYPE: <color=red>{logsType}</color>");
                    break;
            }
            Debug.Log($"{logsType} | {text}");
        }
    }
}