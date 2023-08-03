using System;
using appdata;
using static System.Net.Mime.MediaTypeNames;

namespace calculator_plus;

public partial class NewPage : ContentPage
{
   
    public NewPage(string text)
    {
        // 调用InitializeComponent方法来加载XAML文件中定义的控件
        InitializeComponent();
        displayLabel.Text = text;
    }
    // 定义OnClearClicked方法来处理AC按钮点击事件
    private void OnClearClicked(object sender, EventArgs e)
    {
        AppData.Instance.currentNumber = 0;
        AppData.Instance.lastNumber = 0;
        AppData.Instance.currentOperator = "";
        AppData.Instance.isResult = false;
        AppData.Instance.iscurrentNumber = false;
        displayLabel.Text = AppData.Instance.lastNumber.ToString();
        AppData.Instance.RaiseTextUpdated(displayLabel.Text);
    }

    private void OnDELClicked(object sender, EventArgs e)
    {
        if (!AppData.Instance.isResult)
        {
            if (AppData.Instance.iscurrentNumber == false)
            {
                AppData.Instance.currentOperator = "";
                AppData.Instance.iscurrentNumber = true;
                displayLabel.Text = AppData.Instance.lastNumber.ToString();
            }
            else
            {
                string text = displayLabel.Text;
                if (text.Length > 1)
                {
                    double tempCurrentNumber;
                    double.TryParse(text.Substring(0, text.Length - 1), out tempCurrentNumber);
                    AppData.Instance.currentNumber = tempCurrentNumber;
                    displayLabel.Text = AppData.Instance.currentNumber.ToString();
                    if (text[text.Length - 2] == '.')
                    {
                        displayLabel.Text += ".";
                    }
                }
                else
                {
                    AppData.Instance.currentNumber = 0;
                    if (AppData.Instance.currentOperator != "")
                    {
                        AppData.Instance.iscurrentNumber = false;
                    }
                    displayLabel.Text = "";
                }
            }
        }
        else
        {
            displayLabel.Text = "";
        }
        AppData.Instance.RaiseTextUpdated(displayLabel.Text);
    }

    private void OnOperatorClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var op = button.Text;

        AppData.Instance.lastNumber = AppData.Instance.currentNumber;
        displayLabel.Text = "0";
        AppData.Instance.isResult = false;
        AppData.Instance.iscurrentNumber = false;

        // 将当前选择的运算符赋值给变量，并清空当前输入的数字
        AppData.Instance.currentOperator = op;
        AppData.Instance.RaiseTextUpdated(displayLabel.Text);
    }

    // 定义OnNumberClicked方法来处理数字按钮点击事件
    private void OnNumberClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var number = button.Text;
        AppData.Instance.iscurrentNumber = true;

        // 如果当前显示的是结果，或者是0，就清空显示屏
        if (AppData.Instance.isResult || displayLabel.Text == "0")
        {
            displayLabel.Text = "";
            if (number == ".")
                displayLabel.Text = "0";
            AppData.Instance.isResult = false;
        }

        //输入..会崩溃
        if (!(number == "." && displayLabel.Text.Contains(".")))
        {
            // 将数字追加到显示屏，并更新当前输入的数字
            displayLabel.Text += number;
        }
        if (displayLabel.Text.Contains("pi"))
        {
            // 将 "pi" 替换为 Math.PI 的字符串表示，并解析为 double
            displayLabel.Text = displayLabel.Text.Replace("pi", Math.PI.ToString());
        }
        if (displayLabel.Text.Contains("e"))
        {
            // 将 "e" 替换为 Math.E 的字符串表示，并解析为 double
            displayLabel.Text = displayLabel.Text.Replace("e", Math.E.ToString());
        }
        AppData.Instance.currentNumber = double.Parse(displayLabel.Text);
        AppData.Instance.RaiseTextUpdated(displayLabel.Text);
    }
    private void OnEqualClicked(object sender, EventArgs e)
    {
        // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
        if (AppData.Instance.currentOperator != "")
        {
            Calculate();
            displayLabel.Text = AppData.Instance.lastNumber.ToString();
            AppData.Instance.isResult = true;
            AppData.Instance.currentOperator = "";
        }
        AppData.Instance.RaiseTextUpdated(displayLabel.Text);
    }
    private void Calculate()
    {
        // 根据当前选择的运算符，对上一次计算的结果和当前输入的数字进行相应的运算，并更新上一次计算的结果
        switch (AppData.Instance.currentOperator)
        {
            case "+":
                AppData.Instance.lastNumber += AppData.Instance.currentNumber;
                break;
            case "-":
                AppData.Instance.lastNumber -= AppData.Instance.currentNumber;
                break;
            case "*":
                AppData.Instance.lastNumber *= AppData.Instance.currentNumber;
                break;
            case "/":
                AppData.Instance.lastNumber /= AppData.Instance.currentNumber;
                break;
            case "^":
                AppData.Instance.lastNumber = Math.Pow(AppData.Instance.lastNumber, AppData.Instance.currentNumber);
                break;
            case "!":
                int factorial = 1;
                for (int i = 1; i <= AppData.Instance.lastNumber; i++)
                {
                    factorial *= i;
                }
                AppData.Instance.lastNumber = factorial;
                break;
            case "lg":
                AppData.Instance.lastNumber = Math.Log10(AppData.Instance.currentNumber);
                break;
            case "ln":
                AppData.Instance.lastNumber = Math.Log(AppData.Instance.currentNumber);
                break;
            case "√":
                AppData.Instance.lastNumber = Math.Sqrt(AppData.Instance.currentNumber);
                break;
            case "sin":
                AppData.Instance.lastNumber = Math.Sin(AppData.Instance.currentNumber);
                break;
            case "cos":
                AppData.Instance.lastNumber = Math.Cos(AppData.Instance.currentNumber);
                break;
            case "tan":
                AppData.Instance.lastNumber = Math.Tan(AppData.Instance.currentNumber);
                break;
            default:
                break;
        }
        if (AppData.Instance.iscurrentNumber == false && AppData.Instance.currentOperator != "!")
        {
            AppData.Instance.lastNumber = AppData.Instance.currentNumber;
        }
        AppData.Instance.lastNumber = Math.Round(AppData.Instance.lastNumber, 4);
        AppData.Instance.currentNumber = AppData.Instance.lastNumber;
    }
}