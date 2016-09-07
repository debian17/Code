using System;
using System.Windows.Forms;

namespace WebAPI_Teacher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SignInBTN_Click(object sender, EventArgs e)
        {
            NSForm f = new NSForm();
            f.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void RegBTN_Click(object sender, EventArgs e)
        {
            RegForm f = new RegForm();
            f.Show();
        }
    }
}
