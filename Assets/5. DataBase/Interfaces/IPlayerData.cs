using _5._DataBase.Data;

namespace _5._DataBase.Interfaces
{
    public interface IPlayerData
    {
        public double Donut { get; set; }
        public double Donate{ get; set; }
        
        public double StrengthClick{ get; set; }
        public double FactorClick { get; set; }
        public int DonutLevel{ get; set; }
        
        public int Level{ get; set; }
        public double Exp{ get; set; }
        public int ChanceCrit { get; set; }
        public StatisticsData StatisticsData { get; set; }
        
    }
}