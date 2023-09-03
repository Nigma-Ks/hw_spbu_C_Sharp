namespace homework7
{
    public partial class Form1 : Form
    {
        private Calculator calculator;

        public Form1()
        {
            InitializeComponent();
            calculator = new();
        }

        public void Write(object sender, EventArgs e)
        {

            label1.Text = calculator.accOperand;
        }

        public void WriteExpression(object sender, EventArgs e)
        {
            label2.Text = calculator.expression;
        }
        public void CalculationProcess(object sender, EventArgs e)
        {

            var character = (sender as Button)?.Text;
            calculator.CalculationProcess(character);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}