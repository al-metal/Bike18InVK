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
using VkNet.Model.RequestParams;

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
            Settings scope = Settings.All;
            vk.Authorize(new VkNet.ApiAuthParams
            {
                ApplicationId = (ulong)idVkProgram,
                Login = tbVkLogin.Text,
                Password = tbVkPass.Text,
                Settings = scope
            });

            // Получить адрес сервера для загрузки.
            var uploadServer = vk.Photo.GetMarketUploadServer(63895737, true, 5, 5, 600);
            // Загрузить фотографию.
            var wc = new WebClient();
            var responseImg = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, "s2-012_3.jpg"));
            // Сохранить загруженную фотографию
            var photo = vk.Photo.SaveMarketPhoto(63895737, responseImg);
            var tovar = vk.Markets.Add(new MarketProductParams
            {
                OwnerId = -63895737,
                CategoryId = 401,
                MainPhotoId = photo.FirstOrDefault().Id.Value,
                Deleted = false,
                Name = "Телефон",
                Description = "Описание товара",
                Price = 10000
            });

            ///id подборки товара
            List<long> lon = new List<long>();
            lon.Add(37);
            IEnumerable<long> albums = (IEnumerable<long>)lon;

            var addToTovarInAlbum = vk.Markets.AddToAlbum(-63895737, tovar, albums);
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
