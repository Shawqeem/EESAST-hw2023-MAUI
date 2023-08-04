namespace appdata
{
    public class AppData
    {
        private static readonly AppData _instance = new AppData();

        public static AppData Instance => _instance;

        public double currentNumber { get; set; } = 0;
        public double lastNumber { get; set; } = 0;
        public string currentOperator { get; set; } = "";
        public bool isResult { get; set; } = false;
        public bool iscurrentNumber { get; set; } = false;

        public event EventHandler<string> TextUpdated;

        // 触发事件的方法
        public void RaiseTextUpdated(string newText)
        {
            TextUpdated?.Invoke(this, newText);
        }

        // 私有构造函数，防止类外部创建实例
        private AppData()
        {
        }
    }
}