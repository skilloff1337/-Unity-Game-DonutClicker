using _1._Logs.Lists;

namespace _1._Logs.Scripts.Interfaces
{
    public interface ILogSystem
    {
        void AddLog(LogsType logsType, string text);
    }
}