namespace Client
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.currency_one = new System.Windows.Forms.ListBox();
			this.currency_two = new System.Windows.Forms.ListBox();
			this.btn_convert = new System.Windows.Forms.Button();
			this.count_money = new System.Windows.Forms.NumericUpDown();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.count_money)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(75, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Сумма";
			// 
			// currency_one
			// 
			this.currency_one.FormattingEnabled = true;
			this.currency_one.Items.AddRange(new object[] {
            "EUR",
            "USD",
            "KZT"});
			this.currency_one.Location = new System.Drawing.Point(213, 33);
			this.currency_one.Name = "currency_one";
			this.currency_one.Size = new System.Drawing.Size(81, 95);
			this.currency_one.TabIndex = 2;
			// 
			// currency_two
			// 
			this.currency_two.FormattingEnabled = true;
			this.currency_two.Items.AddRange(new object[] {
            "EUR",
            "USD",
            "KZT"});
			this.currency_two.Location = new System.Drawing.Point(360, 33);
			this.currency_two.Name = "currency_two";
			this.currency_two.Size = new System.Drawing.Size(79, 95);
			this.currency_two.TabIndex = 3;
			// 
			// btn_convert
			// 
			this.btn_convert.Location = new System.Drawing.Point(524, 70);
			this.btn_convert.Name = "btn_convert";
			this.btn_convert.Size = new System.Drawing.Size(98, 34);
			this.btn_convert.TabIndex = 4;
			this.btn_convert.Text = "Конвертировать";
			this.btn_convert.UseVisualStyleBackColor = true;
			this.btn_convert.Click += new System.EventHandler(this.btn_convert_click);
			// 
			// count_money
			// 
			this.count_money.Location = new System.Drawing.Point(41, 70);
			this.count_money.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.count_money.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.count_money.Name = "count_money";
			this.count_money.Size = new System.Drawing.Size(120, 20);
			this.count_money.TabIndex = 5;
			this.count_money.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.count_money.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// webBrowser1
			// 
			this.webBrowser1.Location = new System.Drawing.Point(-191, -109);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.ScrollBarsEnabled = false;
			this.webBrowser1.Size = new System.Drawing.Size(539, 287);
			this.webBrowser1.TabIndex = 6;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.webBrowser1);
			this.panel1.Location = new System.Drawing.Point(180, 201);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(433, 195);
			this.panel1.TabIndex = 7;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.count_money);
			this.Controls.Add(this.btn_convert);
			this.Controls.Add(this.currency_two);
			this.Controls.Add(this.currency_one);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.count_money)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox currency_one;
		private System.Windows.Forms.ListBox currency_two;
		private System.Windows.Forms.Button btn_convert;
		private System.Windows.Forms.NumericUpDown count_money;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.Panel panel1;
	}
}

