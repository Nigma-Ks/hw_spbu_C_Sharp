namespace homework7
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            panel1 = new Panel();
            button11 = new Button();
            button10 = new Button();
            buttonPlus = new Button();
            buttonMinus = new Button();
            buttonMultiply = new Button();
            buttonDivide = new Button();
            buttonZero = new Button();
            button9 = new Button();
            button8 = new Button();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            panel2 = new Panel();
            label2 = new Label();
            buttonClear = new Button();
            label1 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 192, 192);
            button1.Cursor = Cursors.Hand;
            button1.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(22, 390);
            button1.Name = "button1";
            button1.Size = new Size(165, 116);
            button1.TabIndex = 0;
            button1.Text = "1";
            button1.UseVisualStyleBackColor = false;
            button1.Click += WriteExpression;
            button1.Click += CalculationProcess;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 192, 192);
            button2.Cursor = Cursors.Hand;
            button2.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            button2.ForeColor = Color.Black;
            button2.Location = new Point(213, 390);
            button2.Name = "button2";
            button2.Size = new Size(165, 116);
            button2.TabIndex = 1;
            button2.Text = "2";
            button2.UseVisualStyleBackColor = false;
            button2.Click += WriteExpression;
            button2.Click += CalculationProcess;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(255, 192, 192);
            button3.Cursor = Cursors.Hand;
            button3.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            button3.ForeColor = Color.Black;
            button3.Location = new Point(409, 390);
            button3.Name = "button3";
            button3.Size = new Size(165, 116);
            button3.TabIndex = 2;
            button3.Text = "3";
            button3.UseVisualStyleBackColor = false;
            button3.Click += WriteExpression;
            button3.Click += CalculationProcess;
            // 
            // panel1
            // 
            panel1.BackColor = Color.GhostWhite;
            panel1.Controls.Add(button11);
            panel1.Controls.Add(button10);
            panel1.Controls.Add(buttonPlus);
            panel1.Controls.Add(buttonMinus);
            panel1.Controls.Add(buttonMultiply);
            panel1.Controls.Add(buttonDivide);
            panel1.Controls.Add(buttonZero);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(button8);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(798, 653);
            panel1.TabIndex = 3;
            // 
            // button11
            // 
            button11.BackColor = Color.FromArgb(255, 128, 128);
            button11.Cursor = Cursors.Hand;
            button11.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            button11.ForeColor = Color.Black;
            button11.Location = new Point(595, 542);
            button11.Name = "button11";
            button11.Size = new Size(194, 99);
            button11.TabIndex = 15;
            button11.Text = "=";
            button11.UseVisualStyleBackColor = false;
            button11.Click += CalculationProcess;
            button11.Click += WriteExpression;
            button11.Click += Write;
            // 
            // button10
            // 
            button10.BackColor = Color.FromArgb(255, 192, 192);
            button10.Cursor = Cursors.Hand;
            button10.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            button10.ForeColor = Color.Black;
            button10.Location = new Point(266, 523);
            button10.Name = "button10";
            button10.Size = new Size(308, 116);
            button10.TabIndex = 14;
            button10.Text = ",";
            button10.UseVisualStyleBackColor = false;
            button10.Click += CalculationProcess;
            button10.Click += WriteExpression;
            // 
            // buttonPlus
            // 
            buttonPlus.BackColor = Color.FromArgb(255, 128, 128);
            buttonPlus.Cursor = Cursors.Hand;
            buttonPlus.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            buttonPlus.ForeColor = Color.Black;
            buttonPlus.Location = new Point(595, 436);
            buttonPlus.Name = "buttonPlus";
            buttonPlus.Size = new Size(194, 99);
            buttonPlus.TabIndex = 13;
            buttonPlus.Text = "+";
            buttonPlus.UseVisualStyleBackColor = false;
            buttonPlus.Click += CalculationProcess;
            buttonPlus.Click += WriteExpression;
            buttonPlus.Click += Write;
            // 
            // buttonMinus
            // 
            buttonMinus.BackColor = Color.FromArgb(255, 128, 128);
            buttonMinus.Cursor = Cursors.Hand;
            buttonMinus.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            buttonMinus.ForeColor = Color.Black;
            buttonMinus.Location = new Point(595, 326);
            buttonMinus.Name = "buttonMinus";
            buttonMinus.Size = new Size(194, 104);
            buttonMinus.TabIndex = 12;
            buttonMinus.Text = "-";
            buttonMinus.UseVisualStyleBackColor = false;
            buttonMinus.Click += CalculationProcess;
            buttonMinus.Click += WriteExpression;
            buttonMinus.Click += Write;
            // 
            // buttonMultiply
            // 
            buttonMultiply.BackColor = Color.FromArgb(255, 128, 128);
            buttonMultiply.Cursor = Cursors.Hand;
            buttonMultiply.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            buttonMultiply.ForeColor = Color.Black;
            buttonMultiply.Location = new Point(595, 220);
            buttonMultiply.Name = "buttonMultiply";
            buttonMultiply.Size = new Size(194, 100);
            buttonMultiply.TabIndex = 11;
            buttonMultiply.Text = "*";
            buttonMultiply.UseVisualStyleBackColor = false;
            buttonMultiply.Click += CalculationProcess;
            buttonMultiply.Click += WriteExpression;
            buttonMultiply.Click += Write;
            // 
            // buttonDivide
            // 
            buttonDivide.BackColor = Color.FromArgb(255, 128, 128);
            buttonDivide.Cursor = Cursors.Hand;
            buttonDivide.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            buttonDivide.ForeColor = Color.Black;
            buttonDivide.Location = new Point(593, 111);
            buttonDivide.Name = "buttonDivide";
            buttonDivide.Size = new Size(196, 103);
            buttonDivide.TabIndex = 10;
            buttonDivide.Text = "/";
            buttonDivide.UseVisualStyleBackColor = false;
            buttonDivide.Click += CalculationProcess;
            buttonDivide.Click += WriteExpression;
            buttonDivide.Click += Write;
            // 
            // buttonZero
            // 
            buttonZero.BackColor = Color.FromArgb(255, 192, 192);
            buttonZero.Cursor = Cursors.Hand;
            buttonZero.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            buttonZero.ForeColor = Color.Black;
            buttonZero.Location = new Point(22, 523);
            buttonZero.Name = "buttonZero";
            buttonZero.Size = new Size(218, 116);
            buttonZero.TabIndex = 9;
            buttonZero.Text = "0";
            buttonZero.UseVisualStyleBackColor = false;
            buttonZero.Click += CalculationProcess; 
            buttonZero.Click += WriteExpression;
            // 
            // button9
            // 
            button9.BackColor = Color.FromArgb(255, 192, 192);
            button9.Cursor = Cursors.Hand;
            button9.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            button9.ForeColor = Color.Black;
            button9.Location = new Point(409, 111);
            button9.Name = "button9";
            button9.Size = new Size(165, 121);
            button9.TabIndex = 8;
            button9.Text = "9";
            button9.UseVisualStyleBackColor = false;
            button9.Click += CalculationProcess;
            button9.Click += WriteExpression;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(255, 192, 192);
            button8.Cursor = Cursors.Hand;
            button8.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            button8.ForeColor = Color.Black;
            button8.Location = new Point(213, 111);
            button8.Name = "button8";
            button8.Size = new Size(165, 121);
            button8.TabIndex = 7;
            button8.Text = "8";
            button8.UseVisualStyleBackColor = false;
            button8.Click += CalculationProcess;
            button8.Click += WriteExpression;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(255, 192, 192);
            button7.Cursor = Cursors.Hand;
            button7.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            button7.ForeColor = Color.Black;
            button7.Location = new Point(22, 111);
            button7.Name = "button7";
            button7.Size = new Size(165, 121);
            button7.TabIndex = 6;
            button7.Text = "7";
            button7.UseVisualStyleBackColor = false;
            button7.Click += CalculationProcess;    
            button7.Click += WriteExpression;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(255, 192, 192);
            button6.Cursor = Cursors.Hand;
            button6.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            button6.ForeColor = Color.Black;
            button6.Location = new Point(409, 250);
            button6.Name = "button6";
            button6.Size = new Size(165, 116);
            button6.TabIndex = 5;
            button6.Text = "6";
            button6.UseVisualStyleBackColor = false;
            button6.Click += CalculationProcess;
            button6.Click += WriteExpression;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(255, 192, 192);
            button5.Cursor = Cursors.Hand;
            button5.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            button5.ForeColor = Color.Black;
            button5.Location = new Point(213, 250);
            button5.Name = "button5";
            button5.Size = new Size(165, 116);
            button5.TabIndex = 4;
            button5.Text = "5";
            button5.UseVisualStyleBackColor = false;
            button5.Click += CalculationProcess;
            button5.Click += WriteExpression;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(255, 192, 192);
            button4.Cursor = Cursors.Hand;
            button4.Font = new Font("Bookman Old Style", 30F, FontStyle.Regular, GraphicsUnit.Point);
            button4.ForeColor = Color.Black;
            button4.Location = new Point(22, 250);
            button4.Name = "button4";
            button4.Size = new Size(165, 116);
            button4.TabIndex = 3;
            button4.Text = "4";
            button4.UseVisualStyleBackColor = false;
            button4.Click += CalculationProcess;
            button4.Click += WriteExpression;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(192, 192, 255);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(buttonClear);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(801, 90);
            panel2.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Bookman Old Style", 13F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(273, 10);
            label2.Name = "label2";
            label2.Size = new Size(20, 25);
            label2.TabIndex = 2;
            // 
            // buttonClear
            // 
            buttonClear.BackColor = Color.FromArgb(255, 192, 128);
            buttonClear.Cursor = Cursors.Hand;
            buttonClear.Font = new Font("Bookman Old Style", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            buttonClear.Location = new Point(22, 12);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(76, 67);
            buttonClear.TabIndex = 1;
            buttonClear.Text = "AC";
            buttonClear.UseVisualStyleBackColor = false;
            buttonClear.Click += WriteExpression;
            buttonClear.Click += CalculationProcess;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Bookman Old Style", 20F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(663, 38);
            label1.Name = "label1";
            label1.Size = new Size(20, 41);
            label1.TabIndex = 0;
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(798, 653);
            Controls.Add(panel2);
            Controls.Add(panel1);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimumSize = new Size(816, 700);
            Name = "Form1";
            Text = "Calculator";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Panel panel1;
        private Button buttonZero;
        private Button button9;
        private Button button8;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button buttonPlus;
        private Button buttonMinus;
        private Button buttonMultiply;
        private Button buttonDivide;
        private Panel panel2;
        private Button button10;
        private Label label1;
        private Button buttonClear;
        private Button button11;
        private Label label2;
    }
}