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
                string description = ReturnDescriptionProduct(product[7].ToString(), product[8].ToString(), urlBike18Tovar);
                articl = product[6].ToString();
                articl = articl.Replace(" ", "").Replace("/", "");
                string nameProduct = product[4].ToString();
               
                
                SaveAllImages(product[44].ToString(), articl);
                int price = Convert.ToInt32(product[9].ToString());


                AddInVK(vk, articl, nameProduct, description, price);
            }
            
            
        }

        private string ReturnDescriptionProduct(string miniText, string fullText, string urlProduct)
        {
            string text = "";
            MatchCollection tagsDescription = new Regex("<.*?>").Matches(miniText);
            foreach (Match s in tagsDescription)
            {
                string tag = s.ToString();
                miniText = miniText.Replace(tag, " ");
            }
            string rassrochkaBike18 = new Regex("\\+.*").Match(miniText).ToString();
            if(rassrochkaBike18 != "")
            miniText = miniText.Replace(rassrochkaBike18, "");
            string vkGroup = new Regex("Вступай в нашу группу  .*").Match(miniText).ToString();
            if (vkGroup != "")
                miniText = miniText.Replace(vkGroup, "");

            tagsDescription = new Regex("<.*?>").Matches(fullText);
            foreach (Match s in tagsDescription)
            {
                string tag = s.ToString();
                fullText = fullText.Replace(tag, " ");
            }

            string tehnichHarak = new Regex("ТЕХНИЧЕСКИЕ ХАРАКТЕРИСТИКИ.*").Match(fullText).ToString();
            if (tehnichHarak != "")
                fullText = fullText.Replace(tehnichHarak, " ");
            text = miniText + "\r\n" + fullText;
            MatchCollection ampers = new Regex("&.*;").Matches(text);
            if(ampers.Count != 0)
                foreach(Match s in ampers)
                {
                    string str = s.ToString();
                    text = text.Replace(str, "");
                }
            return text;
        }

        private void AddInVK(VkApi vk, string articl, string nameProduct, string descriptionProduct, int price)
        {
            // Получить адрес сервера для загрузки.
            var uploadServer = vk.Photo.GetMarketUploadServer(63895737, true, 1, 1, 400);
            // Загрузить фотографию.
            var wc = new WebClient();

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
                    var responseImg = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, nameImage));
                    // Сохранить загруженную фотографию
                    var photo = vk.Photo.SaveMarketPhoto(63895737, responseImg);
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
                Name = nameProduct,
                Description = descriptionProduct,
                Price = price,
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
                    url = "http://" + url;
                    webClient.DownloadFile(url, "pic\\" + articl + "_" + i + ".jpg");
                    System.Drawing.Image objImage = System.Drawing.Image.FromFile("pic\\" + articl + "_" + i + ".jpg");
                    int lbl_ImageWidth = objImage.Width;
                    int lbl_ImageHeight = objImage.Height;
                    objImage.Dispose();
                    if (lbl_ImageHeight < 400 || lbl_ImageWidth < 400)
                    {
                        File.Delete("pic\\" + articl + "_" + i + ".jpg");
                        i--;
                    }                    
                }
                catch
                {
                    i--;
                }
                i++;
            }
        }
    }
}
