using System;
using static calculator.ComplexPage;

namespace calculator;

public partial class MainPage : ContentPage
{
    // 使用构造函数来初始化页面
    public MainPage()
    {
        // 调用InitializeComponent方法来加载XAML文件中定义的控件
        InitializeComponent();
        // 订阅消息
        MessagingCenter.Subscribe<ComplexPage, CalculatorData>(this, "DataChanged", (sender, updatedData) =>
        {
            // 更新数据
            data = updatedData;
            displayLabel.Text = data.currentNumber.ToString();
        });
    }

    // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果
    public class CalculatorData
    {
        public double currentNumber { get; set; }
        public double lastNumber { get; set; }
        public string currentOperator { get; set; }
        public bool isResult { get; set; }
        public CalculatorData()
        {
            currentNumber = 0;
            lastNumber = 0;
            currentOperator = "";
            isResult = false;
        }
    }
    private CalculatorData data = new CalculatorData();


    // 定义OnNumberClicked方法来处理数字按钮点击事件
    private void OnNumberClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var number = button.Text;

        // 如果当前显示的是结果，或者是0，就清空显示屏
        if (data.isResult || displayLabel.Text == "0")
        {
            displayLabel.Text = "";
            if (number == ".")
                displayLabel.Text = "0";
            data.isResult = false;
        }

        // 将数字追加到显示屏，并更新当前输入的数字
        displayLabel.Text += number;
        data.currentNumber = double.Parse(displayLabel.Text);
    }

    // 定义OnOperatorClicked方法来处理运算符按钮点击事件
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var op = button.Text;
        data.lastNumber = data.currentNumber;
        displayLabel.Text = "0";
        data.isResult = false;

        // 将当前选择的运算符赋值给变量，并清空当前输入的数字
        data.currentOperator = op;
    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnEqualClicked(object sender, EventArgs e)
    {
        // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
        if (data.currentOperator != "")
        {
            Calculate();
            displayLabel.Text = data.lastNumber.ToString();
            data.isResult = true;
            data.currentOperator = "";
        }
    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnClearClicked(object sender, EventArgs e)
    {
        data.currentNumber = 0;
        data.lastNumber = 0;
        data.currentOperator = "";
        data.isResult = false;
        displayLabel.Text = data.lastNumber.ToString();
    }

    // 定义OnDeteleClicked方法来处理DEL按钮点击事件
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (data.isResult)
        {
            displayLabel.Text = "";
        }
        else
        {
            if(displayLabel.Text == "0")
            {
                data.currentOperator = "";
                displayLabel.Text = data.currentNumber.ToString();
            }
            else
            {
                if (displayLabel.Text.Length > 0)
                {
                    if (displayLabel.Text.Length == 1)
                    {
                        displayLabel.Text = "";
                        data.currentNumber = 0;
                    }
                    else
                    {
                        displayLabel.Text = displayLabel.Text.Remove(displayLabel.Text.Length - 1);
                        data.currentNumber = double.Parse(displayLabel.Text);
                    }
                }
            }
        }
    }

    // 定义Calculate方法来执行运算逻辑
    private void Calculate()
    {
        // 根据当前选择的运算符，对上一次计算的结果和当前输入的数字进行相应的运算，并更新上一次计算的结果
        switch (data.currentOperator)
        {
            case "+":
                data.lastNumber += data.currentNumber;
                break;
            case "-":
                data.lastNumber -= data.currentNumber;
                break;
            case "*":
                data.lastNumber *= data.currentNumber;
                break;
            case "/":
                data.lastNumber /= data.currentNumber;
                break;
            case "x^y":
                data.lastNumber = Math.Pow(data.lastNumber, data.currentNumber);
                break;
            default:
                break;
        }
        data.lastNumber = Math.Round(data.lastNumber, 4);
        data.currentNumber = data.lastNumber;
    }

    // 定义ToComplex方法来执行页面切换
    private  void OnToComplexClicked(object sender, EventArgs e)
    {

        Navigation.PushAsync(new ComplexPage(data));
    }
}


