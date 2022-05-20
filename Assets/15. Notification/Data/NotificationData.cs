namespace _15._Notification.Data
{
    public class NotificationData
    {
        public string Text { get; set; }
        public float Timer { get; set; }

        public NotificationData(string text, float timer)
        {
            Timer = timer;
            Text = text;
        }
    }
}