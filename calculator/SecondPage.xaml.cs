namespace calculator;

public partial class SecondPage : ContentPage
{
	public SecondPage()
	{
        
        InitializeComponent();
		displayLabel.Text = MainPage.SharedVariables.Display;
    }

	

    public void OnOneOperatorClicked(object sender, EventArgs e)
	{
        var button = sender as Button;
        var op = button.Text;
		switch(op)
		{
			case "sin":
				if (MainPage.SharedVariables.SecondOperating) displayLabel.Text = Math.Sin(MainPage.SharedVariables.SecondNumber).ToString();
				else displayLabel.Text = Math.Sin(MainPage.SharedVariables.FirstNumber).ToString();
                break;
			case "cos":
                if (MainPage.SharedVariables.SecondOperating) displayLabel.Text = Math.Cos(MainPage.SharedVariables.SecondNumber).ToString();
                else displayLabel.Text = Math.Cos(MainPage.SharedVariables.FirstNumber).ToString();
                break;
			case "tan":
                if (MainPage.SharedVariables.SecondOperating) displayLabel.Text = Math.Tan(MainPage.SharedVariables.SecondNumber).ToString();
                else displayLabel.Text = Math.Tan(MainPage.SharedVariables.FirstNumber).ToString();
                break;
			case "��":
                if (MainPage.SharedVariables.SecondOperating) displayLabel.Text = Math.Sqrt(MainPage.SharedVariables.SecondNumber).ToString();
                else displayLabel.Text = Math.Sqrt(MainPage.SharedVariables.FirstNumber).ToString();
                break;
			case "!":
				if (MainPage.SharedVariables.SecondOperating)
				{
					if (Math.Abs(MainPage.SharedVariables.SecondNumber - (int)MainPage.SharedVariables.SecondNumber) < 1e-5)
					{ displayLabel.Text = Factorial((int)MainPage.SharedVariables.SecondNumber).ToString(); }
					else
					{
						displayLabel.Text = "����������";
						return;
                    }
				}
				else
				{
					if (Math.Abs(MainPage.SharedVariables.FirstNumber - (int)MainPage.SharedVariables.FirstNumber) < 1e-5)
					{
                        displayLabel.Text = Factorial((int)MainPage.SharedVariables.FirstNumber).ToString();
                    }
					else
					{
                        displayLabel.Text = "����������";
						return;
                    }
				}
				break;
			case "ln":
                if (MainPage.SharedVariables.SecondOperating) displayLabel.Text = Math.Log(MainPage.SharedVariables.SecondNumber).ToString();
                else displayLabel.Text = Math.Log(MainPage.SharedVariables.FirstNumber).ToString();
                break;
			case "lg":
                if (MainPage.SharedVariables.SecondOperating) displayLabel.Text = Math.Log10(MainPage.SharedVariables.SecondNumber).ToString();
                else displayLabel.Text = Math.Log10(MainPage.SharedVariables.FirstNumber).ToString();
                break;


        }
		MainPage.SharedVariables.FirstNumber = double.Parse(displayLabel.Text);
		MainPage.SharedVariables.SecondOperating = false;
		MainPage.SharedVariables.SecondNumber = 0;
		MainPage.SharedVariables.LastIsCommonOperator = false;
        MainPage.SharedVariables.Display=displayLabel.Text;

    }

	public int Factorial(int n)
	{
		int Result = 1;
		for(int i=1;i<=n;i++)
		{
			Result *= i;
		}
		return Result;
	}
	public void OnOperatorClicked(object sender, EventArgs e) 
	{
        var button = sender as Button;
        var op = button.Text;
		MainPage.SharedVariables.CurrentOperator = op;
		MainPage.SharedVariables.SecondOperating = true;
		displayLabel.Text = "";
		MainPage.SharedVariables.LastIsCommonOperator = true;
        MainPage.SharedVariables.Display = displayLabel.Text;
    }
	public void OnConstantClicked(object sender, EventArgs e) 
	{

        var button = sender as Button;
        var number = button.Text;
		if (MainPage.SharedVariables.SecondOperating)
		{
			if (MainPage.SharedVariables.SecondNumber != 0)
			{
				displayLabel.Text = "�밴AC�������������������ֵ�����µ�����ѧ����";
			}
			else
			{
				if (number == "pi")
					MainPage.SharedVariables.SecondNumber = Math.PI;
				else
					MainPage.SharedVariables.SecondNumber = Math.E;
			}

		}
		else
		{
			if (MainPage.SharedVariables.FirstNumber != 0)
			{
				displayLabel.Text = "�밴AC�������������������ֵ�����µ�����ѧ����";
			}
			else
			{
				if (number == "pi")
					MainPage.SharedVariables.FirstNumber = Math.PI;
				else
					MainPage.SharedVariables.FirstNumber = Math.E;
			}
		}
        MainPage.SharedVariables.Display = displayLabel.Text;


    }
	private void OnClearClicked(object sender, EventArgs e)
	{

        MainPage.SharedVariables.FirstNumber = 0;
		MainPage.SharedVariables.SecondNumber = 0;
		MainPage.SharedVariables.CurrentOperator = "";
		MainPage.SharedVariables.IsResult = false;
		displayLabel.Text = "0";
		MainPage.SharedVariables.SecondOperating = false;
		MainPage.SharedVariables.LastIsCommonOperator = false;
        MainPage.SharedVariables.Display = displayLabel.Text;
    }

	

	private void OnEqualClicked(object sender, EventArgs e)
	{ 
		if(MainPage.SharedVariables.CurrentOperator!="")
		{
			MainPage.SharedVariables.Calculate();
			displayLabel.Text = MainPage.SharedVariables.FirstNumber.ToString("0.######");
            MainPage.SharedVariables.IsResult = true;
            MainPage.SharedVariables.CurrentOperator = "";
            MainPage.SharedVariables.SecondOperating = false;
            MainPage.SharedVariables.LastIsCommonOperator = false;
        }
		else
		{
			displayLabel.Text = "û��ѡ���������";
		}
        MainPage.SharedVariables.Display = displayLabel.Text;
    }

	private async void OnDelClicked(object sender, EventArgs e)
	{
		
		displayLabel.Text = "��ʾ���������������ѧ����(��sin��!��ln���̡�e)�����ᱻ��ԭ��";
        await Task.Delay(2000);

        if (MainPage.SharedVariables.IsResult)
        {
            displayLabel.Text = "��ʾ����գ����Ǽ�����������";
            
        }
        else if (MainPage.SharedVariables.LastIsCommonOperator)
        {
            displayLabel.Text = "�ѽ��ϸ����������";
            MainPage.SharedVariables.CurrentOperator = "";
            MainPage.SharedVariables.SecondOperating = false;
        }
    }
   
	private void OnReturnClicked(object sender, EventArgs e) 
	{
        Navigation.PushAsync(new MainPage());
    }
}