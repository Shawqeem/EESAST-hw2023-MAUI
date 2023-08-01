using Microsoft.Maui.ApplicationModel.DataTransfer;
using System;
using System.ComponentModel;
using System.Numerics;
using System.Threading;

namespace calculator;






public partial class MainPage : ContentPage
{
    // 使用构造函数来初始化页面

    // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果

    public static class SharedVariables
    {
        public static double FirstNumber { get; set; } = 0;
        public static double SecondNumber { get; set; } = 0;
        public static string CurrentOperator { get; set; } = "";
        public static bool SecondOperating { get; set; } = false;
        public static bool LastIsCommonOperator { get;set; } = false;
        public static bool IsResult { get; set; } = false;
        public static string Display { get; set; } = "";
        public static void Calculate()
        {


            // 根据当前选择的运算符，对上一次计算的结果和当前输入的数字进行相应的运算，并更新上一次计算的结果
            switch (CurrentOperator)
            {
                case "+":
                    FirstNumber += SecondNumber;
                    break;
                case "-":
                    FirstNumber -= SecondNumber;
                    break;
                case "*":
                    FirstNumber *= SecondNumber;
                    break;
                case "/":
                    FirstNumber /= SecondNumber;
                    break;
                default:
                    break;
                case "x^y":
                    FirstNumber = Math.Pow(FirstNumber, SecondNumber);
                    break;
            }

        }
    }


    public MainPage()
    {

        
        // 调用InitializeComponent方法来加载XAML文件中定义的控件
        InitializeComponent();
        displayLabel.Text = "";
        
    }

    
    // 定义OnNumberClicked方法来处理数字按钮点击事件
    private void OnNumberClicked(object sender, EventArgs e)
    {
       
        // 获取按钮的文本值
        var button = sender as Button;
        var number = button.Text;
        
                
        // 如果当前显示的是结果，或者是0，就清空显示屏
        if (SharedVariables.IsResult )
        {
            SharedVariables.FirstNumber = 0;
            SharedVariables.SecondNumber = 0;
            displayLabel.Text = "";
            if (number == ".")
                displayLabel.Text = "0";
            SharedVariables.IsResult = false;
        }

        if(SharedVariables.LastIsCommonOperator)
        {
              SharedVariables.SecondOperating = true;
        }

        // 将数字追加到显示屏，并更新当前输入的数字
        displayLabel.Text += number;
        if(!SharedVariables.LastIsCommonOperator && !SharedVariables.SecondOperating)
        {
             SharedVariables.FirstNumber = double.Parse(displayLabel.Text);
        }
        else
        {
            SharedVariables.SecondNumber = double.Parse(displayLabel.Text);
        }


        SharedVariables.LastIsCommonOperator = false;
        SharedVariables.Display = displayLabel.Text;

    }

    // 定义OnOperatorClicked方法来处理运算符按钮点击事件
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        
        // 获取按钮的文本值
        var button = sender as Button;
        var op = button.Text;
        SharedVariables.CurrentOperator = op;
        displayLabel.Text = "";
        // 将当前选择的运算符赋值给变量，并清空当前输入的数字
        SharedVariables.LastIsCommonOperator = true;
        SharedVariables.Display = displayLabel.Text;


    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnEqualClicked(object sender, EventArgs e)
    {
        // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
        if (SharedVariables.CurrentOperator != "")
        {
            SharedVariables.Calculate();
            displayLabel.Text =  SharedVariables.FirstNumber.ToString();
            SharedVariables.IsResult = true;
            SharedVariables.CurrentOperator = "";
            SharedVariables.SecondOperating = false;
            SharedVariables.LastIsCommonOperator = false;
        }
        else
        {
            displayLabel.Text = "未选择运算符！";
        }
        SharedVariables.Display = displayLabel.Text;
    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnClearClicked(object sender, EventArgs e)
    {
        SharedVariables.FirstNumber = 0;
        SharedVariables.SecondNumber = 0;
        SharedVariables.CurrentOperator = "";
        SharedVariables.IsResult = false;
        displayLabel.Text = "";
        SharedVariables.SecondOperating = false;
        SharedVariables.LastIsCommonOperator = false;
        SharedVariables.Display = displayLabel.Text;
    }

    // 定义Calculate方法来执行运算逻辑
   

    private void OnDelClicked(object sender, EventArgs e)
    {
        if(SharedVariables.IsResult)
        {
            displayLabel.Text = "注意：显示屏清空，但是计算结果将保留";
            
        }
        else if(SharedVariables.LastIsCommonOperator)
        {
            SharedVariables.CurrentOperator = "";
            SharedVariables.SecondOperating = false;
        }
        else if(SharedVariables.SecondOperating)
        {
            string number = SharedVariables.SecondNumber.ToString();
            if (number.Length == 1)
            { SharedVariables.SecondNumber = 0; }
            else
            {
                number = number.Substring(0, number.Length - 1);
                if (number[number.Length - 1] == '.')
                    number = number.Substring(0, number.Length - 1);
                SharedVariables.SecondNumber = double.Parse(number);
            }
            displayLabel.Text = SharedVariables.FirstNumber.ToString();
        }
        else
        {
            string number = SharedVariables.FirstNumber.ToString();
            if (number.Length == 1)
            { SharedVariables.FirstNumber = 0; }
            else
            {
                number = number.Substring(0, number.Length - 1);
                if (number[number.Length - 1] == '.')
                    number = number.Substring(0, number.Length - 1);
                SharedVariables.FirstNumber = double.Parse(number);
            }

            displayLabel.Text = SharedVariables.FirstNumber.ToString();
            ;

        }
        SharedVariables.Display = displayLabel.Text;
    }

    private void OnNewPageClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SecondPage());
    }
}




