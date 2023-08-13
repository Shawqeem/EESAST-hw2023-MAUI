﻿using System;

namespace calculator;

public partial class MainPage : ContentPage
{
    // 使用构造函数来初始化页面
    public MainPage()
    {
        // 调用InitializeComponent方法来加载XAML文件中定义的控件
        InitializeComponent();
        displayLabel.Text = Data.DisplayText;
    }

    // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果
    private string currentOperator = "";


    protected override void OnAppearing() {
        base.OnAppearing();
        displayLabel.Text = Data.DisplayText;
    }

    // 定义OnNumberClicked方法来处理数字按钮点击事件
    private void OnNumberClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var number = button.Text;

        // 如果当前显示的是结果，或者是0，就清空显示屏
        if (Data.isResult || Data.DisplayText == "0")
        {
            Data.DisplayText = "";
            displayLabel.Text = "";
            if (number == ".") {
                Data.DisplayText = "0";
                displayLabel.Text = "0";
            }
            Data.isResult = false;
        }

        // 将数字追加到显示屏，并更新当前输入的数字
        Data.DisplayText += number;
        displayLabel.Text += number;
        Data.currentNumber = double.Parse(Data.DisplayText);
    }

    // 定义OnOperatorClicked方法来处理运算符按钮点击事件
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var op = button.Text;
        if (currentOperator == "" && !Data.isResult) {
            Data.lastNumber = Data.currentNumber;
            Data.currentNumber = 0;
        }
        displayLabel.Text = "0";
        Data.DisplayText = "0";
        Data.isResult = false;

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
            Data.DisplayText = Data.lastNumber.ToString();
            displayLabel.Text = Data.DisplayText;
            Data.isResult = true;
            currentOperator = "";
        }
    }

    // 定义OnClearClicked方法来处理等号按钮点击事件
    private void OnClearClicked(object sender, EventArgs e)
    {
        Data.currentNumber = 0;
        Data.lastNumber = 0;
        currentOperator = "";
        Data.isResult = false;
        Data.DisplayText = Data.lastNumber.ToString();
        displayLabel.Text = Data.DisplayText;
    }
    // 定义OnDeleteClicked方法来处理等号按钮点击事件
    private void OnDeleteClicked(object sender, EventArgs e) {
        if (Data.isResult) {
            Data.DisplayText = "0";
            displayLabel.Text = "0";
        } else if (currentOperator != "" && Data.currentNumber == 0) {
            currentOperator = "";
            Data.currentNumber = Data.lastNumber;
            Data.lastNumber = 0;
        } else {
            Data.currentNumber = (Data.currentNumber - Data.currentNumber % 10) / 10;
            Data.DisplayText = Data.currentNumber.ToString();
            displayLabel.Text = Data.DisplayText;
        }
    }

    // 定义Calculate方法来执行运算逻辑
    private void Calculate()
    {
        // 根据当前选择的运算符，对上一次计算的结果和当前输入的数字进行相应的运算，并更新上一次计算的结果
        switch (currentOperator)
        {
            case "+":
                Data.lastNumber += Data.currentNumber;
                break;
            case "-":
                Data.lastNumber -= Data.currentNumber;
                break;
            case "*":
                Data.lastNumber *= Data.currentNumber;
                break;
            case "/":
                Data.lastNumber /= Data.currentNumber;
                break;
            default:
                break;
        }
        Data.lastNumber= System.Math.Round(Data.lastNumber, 4);
        Data.currentNumber = Data.lastNumber;
    }
}


