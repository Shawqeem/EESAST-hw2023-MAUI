using System;

namespace calculator;

public partial class MainPage : ContentPage
{
    // 使用构造函数来初始化页面
    public MainPage()
    {
        // 调用InitializeComponent方法来加载XAML文件中定义的控件
        InitializeComponent();
    }

    // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果
    private double currentNumber = 0;
    private double lastNumber = 0;
    private string currentOperator = "";
    private bool isResult = false;
    private bool op_Trigger = false;
    private bool del_Trigger=false;
    private NewPage newPage = new NewPage();

    // 定义OnNumberClicked方法来处理数字按钮点击事件
    private void OnNumberClicked(object sender, EventArgs e)
    {
        del_Trigger = false;//将del的清屏开关关掉
        // 获取按钮的文本值
        var button = sender as Button;
        var number = button.Text;

        // 如果当前显示的是结果，或者是0，就清空显示屏
        if (isResult || displayLabel.Text == "0")
        {
            displayLabel.Text = "";
            if (number == ".")
                displayLabel.Text = "0";
            isResult = false;
        }

        // 将数字追加到显示屏，并更新当前输入的数字
        if (op_Trigger == true)
        {
            displayLabel.Text = "";
            op_Trigger = false;
        }
        displayLabel.Text += number;
        currentNumber = double.Parse(displayLabel.Text);
    }

    // 定义OnOperatorClicked方法来处理运算符按钮点击事件
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        del_Trigger = false;//将del的清屏开关关掉
        // 获取按钮的文本值
        var button = sender as Button;
        var op = button.Text;

        // 如果当前的运算符不为空，就执行上一次选择的运算，并显示结果
        if (currentOperator != "")
        {
            //判断是否重复输入运算符
            if (displayLabel.Text == "+" || displayLabel.Text == "-" || displayLabel.Text == "*" || displayLabel.Text == "/") { }
            else
            {
                Calculate();
                displayLabel.Text = lastNumber.ToString();
                isResult = true;
            }
        }
        else
        {
            // 否则，就将当前输入的数字赋值给上一次计算的结果
            lastNumber = currentNumber;
            displayLabel.Text = op;
            op_Trigger = true;//代表当前label里面的是运算符
            isResult = false;
        }

        // 将当前选择的运算符赋值给变量，并清空当前输入的数字
        currentOperator = op;
    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnEqualClicked(object sender, EventArgs e)
    {
        //将del的清屏开关打开
        del_Trigger = true;
        // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
        if (currentOperator != "")
        {
            Calculate();
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
            currentOperator = "";
        }
    }

    // 定义OnClearClicked方法来处理清屏按钮点击事件
    private void OnClearClicked(object sender, EventArgs e)
    {
        del_Trigger = false;//将del的清屏开关关掉
        currentNumber = 0;
        lastNumber = 0;
        currentOperator = "";
        isResult = false;
        displayLabel.Text = lastNumber.ToString();
    }

    // 定义OnDeleteClicked方法来处理删除按钮点击事件
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        //判断清屏开关是否为开
        if (del_Trigger == true)
        {
            displayLabel.Text = "0";
        }

        //如果正在输入的是数字数据
        else if (double.Parse(displayLabel.Text) ==currentNumber)
        {
            if(displayLabel.Text.Length == 1)
            {
                displayLabel.Text = "0";
            }
            else
            {
                displayLabel.Text = displayLabel.Text.Remove(displayLabel.Text.Length - 1);
                currentNumber = double.Parse(displayLabel.Text);
            }
        }
        

        //如果正在输入的是操作符
        else
        {
            displayLabel.Text=currentNumber.ToString();
            currentOperator = "";
            isResult = false;
        }
    }

    // 定义OnMoreClicked方法来处理删除按钮点击事件
    private async void OnMoreClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(newPage);
    }

    // 定义Calculate方法来执行运算逻辑
    private void Calculate()
    {
        // 根据当前选择的运算符，对上一次计算的结果和当前输入的数字进行相应的运算，并更新上一次计算的结果
        switch (currentOperator)
        {
            case "+":
                lastNumber += currentNumber;
                break;
            case "-":
                lastNumber -= currentNumber;
                break;
            case "*":
                lastNumber *= currentNumber;
                break;
            case "/":
                lastNumber /= currentNumber;
                break;
            default:
                break;
        }
        lastNumber = Math.Round(lastNumber, 4);
        currentNumber = lastNumber;
    }
}


