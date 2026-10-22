using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator_dsing
{
    public partial class Form1 : Form
    {
        double resultValue = 0;
        string operationPerformed = "";
        bool isOperationPerformed = false;

        public Form1()
        {
            InitializeComponent();

            // Товчлууруудын Click үйл явдлуудыг автоматаар кодтой холбох
            // Тоонууд (0-9 болон цэг)
            button2.Click += Number_Click;  // 0
            button5.Click += Number_Click;  // 1
            button4.Click += Number_Click;  // 2
            button18.Click += Number_Click; // 3
            button12.Click += Number_Click; // 4
            button11.Click += Number_Click; // 5
            button10.Click += Number_Click; // 6
            button16.Click += Number_Click; // 7
            button9.Click += Number_Click;  // 8
            button15.Click += Number_Click; // 9
            button8.Click += Number_Click;  // . (цэг)

            // Үйлдлийн тэмдэгтүүд (+, -, *, /)
            button7.Click += Operator_Click;  // +
            button17.Click += Operator_Click; // -
            button20.Click += Operator_Click; // *
            button19.Click += Operator_Click; // /

            // Тусгай товчлуурууд
            button6.Click += buttonEquals_Click;      // =
            button25.Click += buttonC_Click;          // C
            button26.Click += buttonCE_Click;         // CE
            button27.Click += buttonBackspace_Click;  // Backspace
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        // Тоо болон цэг дарахад ажиллах хэсэг
        private void Number_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (textBox1.Text == "0" || isOperationPerformed)
                textBox1.Text = "";

            isOperationPerformed = false;

            // Хэрэв цэг (.) дарсан бөгөөд өмнө нь цэг орсон байвал дахин давхарлаж оруулахгүй
            if (button.Text == "." && textBox1.Text.Contains("."))
                return;

            textBox1.Text += button.Text;
        }

        // Үйлдлийн тэмдэг (+, -, *, /) дарахад ажиллах хэсэг
        private void Operator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (resultValue != 0 && !isOperationPerformed)
            {
                buttonEquals_Click(sender, e);
            }
            else
            {
                resultValue = double.Parse(textBox1.Text);
            }

            operationPerformed = button.Text;
            isOperationPerformed = true;
        }

        // Тэнцүү (=) товчлуур дарахад тооцоолох хэсэг
        private void buttonEquals_Click(object sender, EventArgs e)
        {
            double secondNumber;
            if (!double.TryParse(textBox1.Text, out secondNumber))
                return;

            switch (operationPerformed)
            {
                case "+":
                    textBox1.Text = (resultValue + secondNumber).ToString();
                    break;
                case "-":
                    textBox1.Text = (resultValue - secondNumber).ToString();
                    break;
                case "*":
                    textBox1.Text = (resultValue * secondNumber).ToString();
                    break;
                case "/":
                    if (secondNumber != 0)
                    {
                        textBox1.Text = (resultValue / secondNumber).ToString();
                    }
                    else
                    {
                        textBox1.Text = "0"; // 0-д хуваавал шууд 0 гэж гаргана
                    }
                    break;
                default:
                    break;
            }

            resultValue = double.Parse(textBox1.Text);
            operationPerformed = "";
            isOperationPerformed = true;
        }

        // C товчлуур (Бүх утгыг тэглэх)
        private void buttonC_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            resultValue = 0;
            operationPerformed = "";
            isOperationPerformed = false;
        }

        // CE товчлуур (Сүүлийн утгыг тэглэх)
        private void buttonCE_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        // Backspace товчлуур (Ардаас нь нэг нэгээр нь устгах)
        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                if (textBox1.Text == "" || textBox1.Text == "-")
                {
                    textBox1.Text = "0";
                }
            }
        }
    }
}