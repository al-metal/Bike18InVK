using Bike18;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bike18InVK
{
    public partial class Form1 : Form
    {
        nethouse nethouse = new nethouse();
        httpRequest http = new httpRequest();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.loginNethouse = tbNethouseLogin.Text;
            Properties.Settings.Default.passwordNethouse = tbNethousePass.Text;
            Properties.Settings.Default.loginVk = tbVkLogin.Text;
            Properties.Settings.Default.passwordVk = tbVkPass.Text;
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbNethouseLogin.Text = Properties.Settings.Default.loginNethouse;
            tbNethousePass.Text = Properties.Settings.Default.passwordNethouse;
            tbVkLogin.Text = Properties.Settings.Default.loginVk;
            tbVkPass.Text = Properties.Settings.Default.passwordVk;
        }
    }
}
