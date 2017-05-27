using Bike18;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using VkNet;

namespace Bike18InVK
{
    public partial class Form1 : Form
    {
        nethouse nethouse = new nethouse();
        httpRequest http = new httpRequest();
        WebClient webClient = new WebClient();

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

            string otv = http.getRequest("https://bike18.ru/products/category/pitbayki?page=all");
            MatchCollection bike18Tovar = new Regex("(?<=<div class=\"product-link -text-center\"><a href=\").*(?=\" >)").Matches(otv);

            string articl = "";

            foreach (Match str in bike18Tovar)
            {
                string urlBike18Tovar = str.ToString();
                List<string> product = nethouse.GetProductList(cookieNethouse, urlBike18Tovar);
                articl = product[6].ToString();
                SaveAllImages(product[44].ToString(), articl);

                AddInVK(vk, articl);
            }
            
            
        }

        private void AddInVK(VkApi vk, string articl)
        {
            // Получить адрес сервера для загрузки.
            var uploadServer = vk.Photo.GetMarketUploadServer(63895737, true, 5, 5, 600);
            // Загрузить фотографию.
            var wc = new WebClient();
            var responseImg = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, "s2-012_3.jpg"));
            // Сохранить загруженную фотографию
            var photo = vk.Photo.SaveMarketPhoto(63895737, responseImg);

            long firstPhoto = 0;
            List<long> photos = new List<long>();
            for (int i = 0; 5 > i; i++)
            {
                string nameImage = "pic\\" + articl + "_" + i + ".jpg";
                if (File.Exists(nameImage))
                {
                    Thread.Sleep(5000);
                    // Загрузить фотографию.
                    wc = new WebClient();
                    responseImg = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, nameImage));
                    // Сохранить загруженную фотографию
                    photo = vk.Photo.SaveMarketPhoto(63895737, responseImg);
                    if (i == 0)
                    {
                        firstPhoto = photo[0].Id.Value;
                    }
                    else
                        photos.Add(photo[0].Id.Value);
                }
            }

            IEnumerable<long> photosArray = (IEnumerable<long>)photos;

            var tovar = vk.Markets.Add(new MarketProductParams
            {
                OwnerId = -63895737,
                CategoryId = 401,
                MainPhotoId = firstPhoto,
                Deleted = false,
                Name = "Телефон",
                Description = "Описание товара",
                Price = 10000,
                PhotoIds = photosArray
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

        private void SaveAllImages(string str, string articl)
        {
            string[] images = str.Split(';');
            int i = 0;
            foreach (string ss in images)
            {
                Thread.Sleep(2000);
                string url = ss;
                if (url == "")
                    continue;
                url = url.Replace("\\/", "/");

                url = url.Replace("//", "");
                webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.81 Safari/537.36");
                try
                {
                    webClient.DownloadFile("http://" + url, "pic\\" + articl + "_" + i + ".jpg");
                }
                catch
                {

                }
                i++;
            }
        }
    }
}
