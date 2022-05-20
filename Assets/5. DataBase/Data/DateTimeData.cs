namespace _5._DataBase.Data
{
    public class DateTimeData
    {
        public int Days = 0;
        public int Hours = 0;
        public int Minutes = 0;
        public int Seconds = 0;

        public string ShowTime() => $"D {Days}, H {Hours}, M {Minutes}, S {Seconds}";

        public double TotalSeconds() => (Days * 86_400) + (Hours * 3600) + (Minutes * 60) + Seconds;
    }
}