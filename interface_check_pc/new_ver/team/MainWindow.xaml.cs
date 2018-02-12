using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using WinHttp;
using System.Web;
using System.Text.RegularExpressions;

namespace team
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        WebClient WC = new WebClient();
        public WinHttpRequest winhttp = new WinHttpRequest();
        se k = new se();

        public MainWindow()
        {
            InitializeComponent();

        }
       

        public void fileDownload(String url, String path)

        {

            try

            {

                WebClient webClient = new WebClient();

                webClient.DownloadFile(url, path);

            }
            catch (Exception e)

            {

                Console.WriteLine(e);

                Console.ReadLine();

            }

        }


        public void ReDim(ref String[] Ary)
        {
            int i;
            i = Ary.Length;
            Array.Resize(ref Ary, i + 1);
            Ary[i] = null;
        }

        private String[] PersingM(String Str, String Str_Start, String Str_End)
        {
            String Source = Str;
            String[] Result = { null };
            int Count = 0;
            while (Source.IndexOf(Str_Start) > -1)
            {
                ReDim(ref Result);
                Source = Source.Substring(Source.IndexOf(Str_Start) + Str_Start.Length);
                if (Source.IndexOf(Str_End) != -1)
                {
                    Result[Count] = Source.Substring(0, Source.IndexOf(Str_End));
                }
                else return Result;
                Count++;
            }
            return Result;
        }

        private String PersingS(String Str, String Str_Start, String Str_End)
        {
            String Source = Str;
            String Result = null;
            Source = Source.Substring(Source.IndexOf(Str_Start) + Str_Start.Length);
            Result = Source.Substring(0, Source.IndexOf(Str_End));
            return Result;
        }
        private void search_Click(object sender, RoutedEventArgs e)
        {
            winhttp.Open("POST", "http://interface518.dothome.co.kr/inter/query.php?", false);
            winhttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");
            winhttp.Send("id=" + stusearch.Text);
            winhttp.WaitForResponse();
            //textBox1.ToString();

            textBox1.Text = Encoding.Default.GetString(winhttp.ResponseBody);
            if (textBox1.Text.Contains("찾을"))
                stustatue.Text = "없는 사람일세";
            else
                //listBox1.Items.Add(PersingS(textBox1.Text, "[id] => ", "[") + " " + PersingS(textBox1.Text, "[name] =>", "[") + " " + PersingS(textBox1.Text, "[address] =>", "[") + " " + PersingS(textBox1.Text, " [department] =>", ")"));
                // stustatue.Text = "              " + PersingS(textBox1.Text, "[id] => ", "[") + " " + PersingS(textBox1.Text, "[name] =>", "[") + " " + PersingS(textBox1.Text, "[address] =>", "[") + " " + PersingS(textBox1.Text, " [department] =>", ")");
                r_num.Text = PersingS(textBox1.Text, "[id] => ", "[");
            r_name.Text = PersingS(textBox1.Text, "[name] =>", "[");
            r_tele.Text = PersingS(textBox1.Text, "[address] =>", "[");
            r_dep.Text = PersingS(textBox1.Text, " [department] =>", ")");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //listBox1.Items.Clear();
            string text = notice.Text;
            
            byte[] bSendMsg = Encoding.UTF8.GetBytes(notice.Text);

            string strEncode = HttpUtility.UrlEncode(notice.Text);
           // MessageBox.Show(strEncode);



            //string readString = Encoding.UTF8.GetString(bSendMsg);
            //textBox2.Text = readString;
            //text = Encoding.UTF8.GetString(textBox2.Text);
            winhttp.Open("POST", "http://interface518.dothome.co.kr/inter/ADadd.php", false);
            winhttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf8");
            winhttp.Send("contents=" + strEncode);
            winhttp.WaitForResponse();
            //textBox1.ToString();

            textBox1.Text = Encoding.Default.GetString(winhttp.ResponseBody);
            //전송후 새로고침
            adlist.Items.Clear();
            WebClient WC = new WebClient();
            WC.Encoding = Encoding.UTF8;//한글 안깨지게.

            string DownLoad = WC.DownloadString("http://interface518.dothome.co.kr/inter/AD.php");
            textBox1.Text = DownLoad;
            //using System.Text.RegularExpressions;

            List<string> hide = new List<string>();

            string data = WC.DownloadString("http://interface518.dothome.co.kr/inter/AD.php");

            Regex regex = new Regex("contents\": \"(.*)\",");

            MatchCollection mc = regex.Matches(data);

            foreach (Match m in mc)

            {

                for (int i = 1; i < m.Groups.Count; i++)

                {
                    adlist.Items.Add(m.Groups[i].Value);


                    //hide.Add(m.Groups[i].Value);



                }

            }

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void stuadd_Click(object sender, RoutedEventArgs e)
        {
            string text = notice.Text;

            byte[] bSendMsg = Encoding.UTF8.GetBytes(notice.Text);

            string strEncodename = HttpUtility.UrlEncode(name.Text);
            string strEncodeco = HttpUtility.UrlEncode(co.Text);
            // MessageBox.Show(strEncode);



            //string readString = Encoding.UTF8.GetString(bSendMsg);
            //textBox2.Text = readString;
            //text = Encoding.UTF8.GetString(textBox2.Text);
            winhttp.Open("POST", "http://interface518.dothome.co.kr/inter/add.php", false);
            winhttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf8");
            winhttp.Send("id=" + stui.Text + "&name=" + strEncodename + "&phone=" + tele.Text + "&department=" + strEncodeco);
            winhttp.WaitForResponse();
            //textBox1.ToString();

            textBox1.Text = Encoding.Default.GetString(winhttp.ResponseBody);

            


        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            adlist.Items.Clear();
            WebClient WC = new WebClient();
            WC.Encoding = Encoding.UTF8;//한글 안깨지게.

            string DownLoad = WC.DownloadString("http://interface518.dothome.co.kr/inter/AD.php");
            textBox1.Text = DownLoad;
            //using System.Text.RegularExpressions;

            List<string> hide = new List<string>();

            string data = WC.DownloadString("http://interface518.dothome.co.kr/inter/AD.php");

            Regex regex = new Regex("contents\": \"(.*)\",");

            MatchCollection mc = regex.Matches(data);

            foreach (Match m in mc)

            {

                for (int i = 1; i < m.Groups.Count; i++)

                {
                    adlist.Items.Add(m.Groups[i].Value);

                    
                    //hide.Add(m.Groups[i].Value);



                }

            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            //저장전에 한번 갱신후 저장
            //webClient.Headers.Add( "User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.3; WOW64; Trident/7.0)" );
            string sUrl = "http://interface518.dothome.co.kr/inter/myeng.txt";
            webClient.DownloadFileAsync( new Uri( sUrl ), "myeng.txt");

           


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            adlist.Items.Clear();
            WebClient WC = new WebClient();
            WC.Encoding = Encoding.UTF8;//한글 안깨지게.

            string DownLoad = WC.DownloadString("http://interface518.dothome.co.kr/inter/AD.php");
            textBox1.Text = DownLoad;
            //using System.Text.RegularExpressions;

            List<string> hide = new List<string>();

            string data = WC.DownloadString("http://interface518.dothome.co.kr/inter/AD.php");

            Regex regex = new Regex("contents\": \"(.*)\",");

            MatchCollection mc = regex.Matches(data);

            foreach (Match m in mc)

            {

                for (int i = 1; i < m.Groups.Count; i++)

                {
                    adlist.Items.Add(m.Groups[i].Value);


                    //hide.Add(m.Groups[i].Value);



                }

            }
        }

        private void tele_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
