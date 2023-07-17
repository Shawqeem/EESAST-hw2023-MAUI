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

    public enum ButtonType
    {
        DEFAULT,
        NUMBER,
        OPERATOR,
        EQUAL,
        UNARY
    }

    // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果
    private double currentNumber = 0;
    private double lastNumber = 0;
    private string currentOperator = "";
    private bool isResult = false;
    private ButtonType lastButton = ButtonType.DEFAULT;
    public bool isAdvancedMode = false;

    // 定义OnNumberClicked方法来处理数字按钮点击事件
    private void OnNumberClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var number = button.Text;

        if (number != "π" && number != "e")
        {
            // 如果当前显示的是结果，或者是0，就清空显示屏
            if (isResult || displayLabel.Text == "0")
            {
                displayLabel.Text = "";
                if (number == ".")
                    displayLabel.Text = "0";
                isResult = false;
            }

            // 将数字追加到显示屏，并更新当前输入的数字
            displayLabel.Text += number;
            currentNumber = double.Parse(displayLabel.Text);
        }
        else
        {
            if (isResult || displayLabel.Text == "0")
            {
                currentNumber = (number == "π" ? Math.PI : Math.E);
                displayLabel.Text = currentNumber.ToString();
            }
            else
            {
                currentNumber *= (number == "π" ? Math.PI : Math.E);
                displayLabel.Text = currentNumber.ToString();
            }
        }

        lastButton = ButtonType.NUMBER;
    }

    // 定义OnOperatorClicked方法来处理运算符按钮点击事件
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var op = button.Text;

        switch (op)
        {
            case "lg":
            case "ln":
            case "√":
            case "!":
            case "sin":
            case "cos":
            case "tan":
                lastButton = ButtonType.UNARY;
                break;
            default:
                break;
        }

        // 如果当前的运算符不为空，就执行上一次选择的运算，并显示结果
        if (currentOperator != "")
        {
            if (lastButton == ButtonType.NUMBER || lastButton == ButtonType.UNARY)
            {
                Calculate();
                displayLabel.Text = lastNumber.ToString();
                isResult = true;
            }
            else
            {
                currentOperator = op;
            }
        }
        else
        {
            // 否则，就将当前输入的数字赋值给上一次计算的结果
            lastNumber = currentNumber;
            if (lastButton == ButtonType.UNARY)
            {
                currentOperator = op;
                Calculate();
                displayLabel.Text = lastNumber.ToString();
                isResult = true;
            }
            else
            {
                displayLabel.Text = "0";
                isResult = false;
            }
        }

        // 将当前选择的运算符赋值给变量，并清空当前输入的数字
        currentOperator = op;

    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnEqualClicked(object sender, EventArgs e)
    {
        // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
        if (currentOperator != "")
        {
            Calculate();
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
            currentOperator = "";
        }

        lastButton = ButtonType.EQUAL;
    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnClearClicked(object sender, EventArgs e)
    {
        currentNumber = 0;
        lastNumber = 0;
        currentOperator = "";
        isResult = false;
        displayLabel.Text = lastNumber.ToString();

        lastButton = ButtonType.DEFAULT;
    }

    // 定义OnDeleteClicked方法来处理删除按钮点击事件
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        // 如果当前显示的是结果，就清空显示屏
        if (isResult && lastButton == ButtonType.EQUAL)
        {
            displayLabel.Text = "0";
            isResult = false;
        }
        else if (lastButton == ButtonType.NUMBER)
        {
            // 否则，就删除最后一个字符，并更新当前输入的数字
            displayLabel.Text = displayLabel.Text.Remove(displayLabel.Text.Length - 1);
            if (displayLabel.Text == "")
                displayLabel.Text = "0";
            currentNumber = double.Parse(displayLabel.Text);
        }
        else if(lastButton == ButtonType.OPERATOR)
        {
            currentOperator = "";
        }
    }

    // 定义OnSwitchClicked方法来处理复杂计算模式切换按钮点击事件
    private void OnSwitchClicked(object sender, EventArgs e)
    {
        standard.IsVisible = !standard.IsVisible;
        scientific.IsVisible = !scientific.IsVisible;
    }

    // 定义Factorial方法来计算阶乘
    private int Factorial(int n)
    {
        if (n == 0)
            return 1;
        else
            return n * Factorial(n - 1);
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
            case "^":
                lastNumber = Math.Pow(lastNumber, currentNumber);
                break;
            case "lg":
                lastNumber = Math.Log10(lastNumber);
                break;
            case "ln":
                lastNumber = Math.Log(lastNumber);
                break;
            case "√":
                lastNumber = Math.Sqrt(lastNumber);
                break;
            case "!":
                lastNumber = Factorial((int)lastNumber);
                break;
            case "sin":
                lastNumber = Math.Sin(lastNumber);
                break;
            case "cos":
                lastNumber = Math.Cos(lastNumber);
                break;
            case "tan":
                lastNumber = Math.Tan(lastNumber);
                break;
            default:
                break;
        }
        lastNumber= Math.Round(lastNumber, 4);
        currentNumber = lastNumber;
    }
}


