using System;


namespace calculator;

public partial class NewPage : ContentPage
{
	public NewPage()
	{
		InitializeComponent();
	}

    // ����һЩ�������洢��ǰ��������֣���ǰѡ�����������Լ���һ�μ���Ľ��
    private double currentNumber = 0;
    private double lastNumber = 0;
    private string currentOperator = "";
    private bool isResult = false;
    private bool op_Trigger = false;
    private bool del_Trigger = false;

    // ����OnNumberClicked�������������ְ�ť����¼�
    private void OnNumberClicked(object sender, EventArgs e)
    {
        del_Trigger = false;//��del���������عص�
        // ��ȡ��ť���ı�ֵ
        var button = sender as Button;
        var number = button.Text;

        // �����ǰ��ʾ���ǽ����������0���������ʾ��
        if (isResult || displayLabel.Text == "0")
        {
            displayLabel.Text = "";
            if (number == ".")
                displayLabel.Text = "0";
            isResult = false;
        }

        // ������׷�ӵ���ʾ���������µ�ǰ���������
        if(op_Trigger==true)
        {
            displayLabel.Text="";
            op_Trigger = false;
        }
        displayLabel.Text += number;
        currentNumber = double.Parse(displayLabel.Text);
    }

    // ����OnSuperNumberClicked���������������ְ�ť����¼�
    private void OnSuperNumberClicked(object sender, EventArgs e)
    {
        del_Trigger = false;//��del���������عص�
        // ��ȡ��ť���ı�ֵ
        var button = sender as Button;
        var number = (double)0;
        if(button.Text == "pi")
        {
            number = Math.PI;
        }
        else
        {
            number = Math.E;
        }
        


        // �����ǰ��ʾ���ǽ����������0���������ʾ��
        if (isResult || displayLabel.Text == "0")
        {
            displayLabel.Text = "";
            isResult = false;
        }

        // ������׷�ӵ���ʾ���������µ�ǰ���������
        displayLabel.Text =number.ToString();
        currentNumber = double.Parse(displayLabel.Text);
    }

    // ����OnOperatorClicked�����������������ť����¼�
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        del_Trigger = false;//��del���������عص�
        // ��ȡ��ť���ı�ֵ
        var button = sender as Button;
        var op = button.Text;

        // �����ǰ���������Ϊ�գ���ִ����һ��ѡ������㣬����ʾ���
        if (currentOperator != "")
        {
            //�ж��Ƿ��ظ����������
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
            // ���򣬾ͽ���ǰ��������ָ�ֵ����һ�μ���Ľ��
            lastNumber = currentNumber;
            displayLabel.Text = op;
            op_Trigger = true;//����ǰlabel������������
            isResult = false;
        }

        // ����ǰѡ����������ֵ������������յ�ǰ���������
        currentOperator = op;
    }

    // ����OnEqualClicked����������ȺŰ�ť����¼�
    private void OnEqualClicked(object sender, EventArgs e)
    {
        //��del���������ش�
        del_Trigger = true;
        // �����ǰѡ����������Ϊ�գ���ִ����һ��ѡ������㣬����ʾ���
        if (currentOperator != "")
        {
            Calculate();
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
            currentOperator = "";
        }
    }

    // ����OnClearClicked����������������ť����¼�
    private void OnClearClicked(object sender, EventArgs e)
    {
        del_Trigger = false;//��del���������عص�
        currentNumber = 0;
        lastNumber = 0;
        currentOperator = "";
        isResult = false;
        displayLabel.Text = lastNumber.ToString();
    }

    // ����OnDeleteClicked����������ɾ����ť����¼�
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        //�ж����������Ƿ�Ϊ��
        if (del_Trigger == true)
        {
            displayLabel.Text = "0";
        }

        //����������������������
        else if (double.Parse(displayLabel.Text) == currentNumber)
        {
            if (displayLabel.Text.Length == 1)
            {
                displayLabel.Text = "0";
            }
            else
            {
                displayLabel.Text = displayLabel.Text.Remove(displayLabel.Text.Length - 1);
                currentNumber = double.Parse(displayLabel.Text);
            }
        }

        //��������������=
        else if(isResult==true)
        {
            displayLabel.Text = "0";
            currentOperator = "";
            isResult=false;
        }

        //�������������ǲ�����
        else
        {
            displayLabel.Text = currentNumber.ToString();
            currentOperator = "";
            isResult = false;
        }
    }

    // ����OnMoreClicked����������ɾ����ť����¼�
    private async void OnMoreClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    // ����Calculate������ִ�������߼�
    private void Calculate()
    {
        // ���ݵ�ǰѡ��������������һ�μ���Ľ���͵�ǰ��������ֽ�����Ӧ�����㣬��������һ�μ���Ľ��
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
            case "sin":
                lastNumber = Math.Sin(currentNumber);
                break;
            case "cos":
                lastNumber = Math.Cos(currentNumber); 
                break;
            case "tan":
                lastNumber = Math.Tan(currentNumber);   
                break;
            case "lg":
                lastNumber = Math.Log10(currentNumber);
                break;
            case "ln":
                lastNumber = Math.Log(currentNumber); 
                break;
            case "^":
                lastNumber = Math.Pow(lastNumber,currentNumber);
                break;
            case "!":
                int num = 1;
                for(int i=0;i<currentNumber;i++)
                {
                    num *= (i + 1);
                }
                lastNumber = num;
                break;
            case "sqrt":
                lastNumber=Math.Sqrt(currentNumber);
                break;
            default:
                break;
        }
        lastNumber = Math.Round(lastNumber, 4);
        currentNumber = lastNumber;
    }
}

