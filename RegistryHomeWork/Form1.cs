using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace RegistryHomeWork
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

		}

		private void entries_registry_Click(object sender, EventArgs e)
		{
	
			RegistryKey[] keys = new RegistryKey[] { Registry.ClassesRoot,Registry.CurrentUser,Registry.LocalMachine,Registry.Users,Registry.CurrentConfig};
			int res = 0;
			foreach (RegistryKey key in keys)
			{
				res += key.SubKeyCount;
			}
			
			entries_txt.Text = "Kol-vo elementov v reestre => " + res.ToString();


		}

		
		private void current_entry_Click(object sender, EventArgs e)
		{
			RegistryKey key = Registry.CurrentUser;

			entry_current_label.Text = "Kol-vo elementov v CURRENT_USER = > " + key.SubKeyCount;
		}

		
	

		private void get_input_Click(object sender, EventArgs e)
		{

			string text = input_entry_textbox.Text.ToUpper();

		
				if (text == "CURRENT_USER")
				{
					RegistryKey key = Registry.CurrentUser;

					input_label.Text = "Kol-vo elementov v CURRENT_USER = > " + key.SubKeyCount;
				}

				else if (text == "CLASSES_ROOT")
				{
					RegistryKey key = Registry.ClassesRoot;

					input_label.Text = "Kol-vo elementov v CLASSES_ROOT = > " + key.SubKeyCount;
				}

				else if (text == "LOCAL_MACHINE")
				{
					RegistryKey key = Registry.LocalMachine;

					input_label.Text = "Kol-vo elementov v LOCAL_MACHINE = > " + key.SubKeyCount;
				}

				else if (text == "USERS")
				{
					RegistryKey key = Registry.Users;

					input_label.Text = "Kol-vo elementov v USERS = > " + key.SubKeyCount;
				}

				else if (text == "CURRENT_CONFIG")
				{
					RegistryKey key = Registry.CurrentConfig;

					input_label.Text = "Kol-vo elementov v CURRENT_CONFIG = > " + key.SubKeyCount;
				}
				else
				{
					MessageBox.Show("Nekorrektniy vvod!\n(CURRENT_USER,CURRENT_CONFIG,USERS,LOCAL_MACHINE,CLASSES_ROOT)");
				}
			
		}
	}
}
