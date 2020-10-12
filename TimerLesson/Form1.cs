using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerLesson
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}



		private void Form1_Load(object sender, EventArgs e)
		{

			timer1.Interval = timer1.Interval + 2000;
			timer1.Tick += timer1_Tick;
			timer1.Enabled = true;

			timer3.Interval = timer3.Interval + 3000;
			timer3.Tick += timer3_Tick;
			timer3.Enabled = true;


			timer2.Interval = timer2.Interval + 4000;
			timer2.Tick += timer2_Tick;
			timer2.Enabled = true;


		}


		private void timer1_Tick(object sender, EventArgs e)
		{

			Random rnd = new Random();
			label1.Font = new Font("Font family", rnd.Next(8, 72));


		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			Random rnd = new Random();
			BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
		}

		FontStyle[] arr = new FontStyle[] { FontStyle.Regular,FontStyle.Bold,FontStyle.Italic,FontStyle.Underline };
		private void timer3_Tick(object sender, EventArgs e)
		{
			Random rnd = new Random();
			label1.Font = new Font(FontFamily.GenericSansSerif,15, arr[rnd.Next(4)]);
		}
	}
}
