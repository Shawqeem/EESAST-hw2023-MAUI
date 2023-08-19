using static calculator.MainPage;

namespace calculator;

public partial class ComplexPage : ContentPage
{
	public ComplexPage()
	{
		InitializeComponent();
	}
    //定义单运算变量
    private string singleOperator = "";

    private CalculatorData data;

    public long factorial(double number)
    {
        long temp = 1;
        if(number %1 == 0 && number > 0)
        {
            for(int i = 2;i <= number;i++)
            {
                temp *= i;
            }
            return temp;
        }
        else { return 0 ; }
    }
    public ComplexPage(CalculatorData data)
    {
        InitializeComponent();
        this.data = data;
        displayLabel.Text = data.currentNumber.ToString();
    }
    private void OnPIClicked(object sender, EventArgs e)
    {
        displayLabel.Text = "";
        data.isResult = false;
        data.currentNumber = Math.PI;
        displayLabel.Text = data.currentNumber.ToString();
        MessagingCenter.Send(this, "DataChanged", data);
    }
    private void OnEClicked(object sender, EventArgs e)
    {
        displayLabel.Text = "";
        data.isResult = false;
        data.currentNumber = Math.E;
        displayLabel.Text = data.currentNumber.ToString();
        MessagingCenter.Send(this, "DataChanged", data);
    }

    // 定义OnOperatorClicked方法来处理运算符按钮点击事件
    private void OnComplexOperatorClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var op = button.Text;
        data.lastNumber = data.currentNumber;
        data.currentNumber = 0;
        MessagingCenter.Send(this, "DataChanged", data);
        displayLabel.Text = "0";
        data.isResult = false;

        // 将当前选择的运算符赋值给变量，并清空当前输入的数字
        data.currentOperator = op;
    }

    // 定义OnSingleOperatorClicked方法来处理直接复杂运算的按钮点击事件
    private void OnSingleOperatorClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var sop = button.Text;
        singleOperator = sop;
        data.isResult = false;
        singleCalculate();
    }
    private void singleCalculate()
    {
        switch (singleOperator)
        {
            case "lg":
                data.currentNumber = Math.Log10(data.currentNumber);
                break;
            case "ln":
                data.currentNumber = Math.Log(data.currentNumber);
                break;
            case "√x":
                data.currentNumber = Math.Sqrt(data.currentNumber);
                break;
            case "x!":
                data.currentNumber = factorial(data.currentNumber);
                break;
            case "sin":
                data.currentNumber = Math.Sin(data.currentNumber);
                break;
            case "cos":
                data.currentNumber = Math.Cos(data.currentNumber);
                break;
            case "tan":
                data.currentNumber = Math.Tan(data.currentNumber);
                break;
            case "1/x":
                data.currentNumber = 1/data.currentNumber;
                break;
            default:
                break;
        }
        data.currentNumber = Math.Round(data.currentNumber, 4);
        displayLabel.Text = data.currentNumber.ToString();
        MessagingCenter.Send(this, "DataChanged", data);
    }
}