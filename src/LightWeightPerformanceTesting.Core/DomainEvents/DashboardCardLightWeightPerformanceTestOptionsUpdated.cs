namespace LightWeightPerformanceTesting.Core.DomainEvents
{
    public class DashboardCardLightWeightPerformanceTestOptionsUpdated: DomainEvent
    {
        public DashboardCardLightWeightPerformanceTestOptionsUpdated(
            int top, 
            int left, 
            int height, 
            int width,
            string selector,
            string username,
            string password,
            string partionKey
            
            )
        {
            Top = top;
            Left = left;
            Height = height;
            Width = width;
            Selector = selector;
            Username = username;
            Password = password;
            PartionKey = partionKey;
        }

        public int Top { get; set; }
        public int Left { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Selector { get; set; }
        public string Username { get; set; }    
        public string Password { get; set; }  
        public string PartionKey { get; set; }
    }
}
