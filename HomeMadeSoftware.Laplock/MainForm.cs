using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeMadeSoftware.Laplock
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                PowerEventHandler.HandleWindowProcCall(m);
                base.WndProc(ref m);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error handling power event. {e.Message}");
            }
        }

        
        private void menuItemClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Visible = false;
        }
    }
}
