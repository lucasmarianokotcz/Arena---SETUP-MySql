using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arena
{
	public partial class frmMenu : Form
	{
		public frmMenu()
		{
			InitializeComponent();
		}

		private void btnJogar_Click(object sender, EventArgs e)
		{
			frmSelecionar Sel = new frmSelecionar();
			Hide();
			Sel.ShowDialog();
			Show();

		}

		private void btnSobre_Click(object sender, EventArgs e)
		{
			frmSobre Sobre = new frmSobre();
			Sobre.ShowDialog();
		}
	}
}
