using Bike18;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet.Enums.Filters;

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

            CookieContainer cookieNethouse = new CookieContainer();
            cookieNethouse = nethouse.CookieNethouse(tbNethouseLogin.Text, tbNethousePass.Text);

            if(cookieNethouse.Count != 4)
            {
                MessageBox.Show("Логин/пароль не верный");
                return;
            }
            CookieContainer cookieVk = new CookieContainer();
            var vk = new VkNet.VkApi();
            var idVkProgram = 5464980;
            Settings scope = Settings.Friends;
            vk.Authorize(new VkNet.ApiAuthParams
            {
                ApplicationId = (ulong)idVkProgram,
                Login = tbVkLogin.Text,
                Password = tbVkPass.Text,
                Settings = scope
            });
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
