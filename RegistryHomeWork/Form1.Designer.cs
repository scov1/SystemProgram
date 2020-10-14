namespace RegistryHomeWork
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
			this.entries_registry = new System.Windows.Forms.Button();
			this.entries_txt = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.entry_current_label = new System.Windows.Forms.Label();
			this.input_label = new System.Windows.Forms.Label();
			this.input_entry_textbox = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// entries_registry
			// 
			this.entries_registry.Location = new System.Drawing.Point(99, 43);
			this.entries_registry.Name = "entries_registry";
			this.entries_registry.Size = new System.Drawing.Size(75, 23);
			this.entries_registry.TabIndex = 0;
			this.entries_registry.Text = "All entries";
			this.entries_registry.UseVisualStyleBackColor = true;
			this.entries_registry.Click += new System.EventHandler(this.entries_registry_Click);
			// 
			// entries_txt
			// 
			this.entries_txt.AutoSize = true;
			this.entries_txt.Location = new System.Drawing.Point(236, 48);
			this.entries_txt.Name = "entries_txt";
			this.entries_txt.Size = new System.Drawing.Size(0, 13);
			this.entries_txt.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(99, 105);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "CurrentUser";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.current_entry_Click);
			// 
			// entry_current_label
			// 
			this.entry_current_label.AutoSize = true;
			this.entry_current_label.Location = new System.Drawing.Point(236, 110);
			this.entry_current_label.Name = "entry_current_label";
			this.entry_current_label.Size = new System.Drawing.Size(0, 13);
			this.entry_current_label.TabIndex = 4;
			// 
			// input_label
			// 
			this.input_label.AutoSize = true;
			this.input_label.Location = new System.Drawing.Point(236, 173);
			this.input_label.Name = "input_label";
			this.input_label.Size = new System.Drawing.Size(0, 13);
			this.input_label.TabIndex = 6;
			// 
			// input_entry_textbox
			// 
			this.input_entry_textbox.Location = new System.Drawing.Point(99, 166);
			this.input_entry_textbox.Name = "input_entry_textbox";
			this.input_entry_textbox.Size = new System.Drawing.Size(100, 20);
			this.input_entry_textbox.TabIndex = 7;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(109, 193);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 8;
			this.button2.Text = "GET";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.get_input_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 411);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.input_entry_textbox);
			this.Controls.Add(this.input_label);
			this.Controls.Add(this.entry_current_label);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.entries_txt);
			this.Controls.Add(this.entries_registry);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button entries_registry;
		private System.Windows.Forms.Label entries_txt;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label entry_current_label;
		private System.Windows.Forms.Label input_label;
		private System.Windows.Forms.TextBox input_entry_textbox;
		private System.Windows.Forms.Button button2;
	}
}

