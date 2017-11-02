using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using WinHttp;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        WebClient WC = new WebClient();
        public WinHttpRequest winhttp = new WinHttpRequest();

        public Form1()
        {
            InitializeComponent();
        }
        private void msgbox(string 내용, string 제목)
        {
            MessageBox.Show(내용, 제목);
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

  


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient WC = new WebClient();
            WC.Encoding = Encoding.UTF8;//한글 안깨지게.
            
            string DownLoad = WC.DownloadString("http://interface518.dothome.co.kr/inter/getjson.php");
            textBox1.Text = DownLoad;
            //": "1234"
            
            listBox1.Items.Add(PersingS(DownLoad, "id\": \"", "\",") +" "+PersingS(DownLoad, "name\": \"", "\",") + " " + PersingS(DownLoad, "address\": \"", "\",") + " " + PersingS(DownLoad, "department\": \"", "\""));
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string text = "";
            //text = Encoding.UTF8.GetString(textBox2.Text);
            winhttp.Open("POST", "http://interface518.dothome.co.kr/inter/ADadd.php?",false);
            winhttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            winhttp.Send("contents=" + textBox2.Text);
            winhttp.WaitForResponse();
            //textBox1.ToString();

             textBox1.Text = Encoding.Default.GetString(winhttp.ResponseBody);


            /*
            string[] parsing = System.Text.RegularExpressions.Regex.Split(winhttp.ResponseText, "<form action=\"http://search.naver.com/search.naver\">");

            for (int a = 1; a <= 10; a++)
            {
                string[] parsing1 = parsing[1].Split(new string[] { "<option value=\"" }, 0);
                string[] parsing2 = parsing1[a].Split(new string[] { "\"" }, 0);

                listBox1.Items.Add(a + "위 " + parsing2[0]);
            }*/
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string text = "";
            //text = Encoding.UTF8.GetString(textBox2.Text);
            winhttp.Open("POST", "http://interface518.dothome.co.kr/inter/query.php?", false);
            winhttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            winhttp.Send("id=" + textBox3.Text);
            winhttp.WaitForResponse();
            //textBox1.ToString();

            textBox1.Text = Encoding.Default.GetString(winhttp.ResponseBody);
            if (textBox1.Text.Contains("찾을"))
                listBox1.Items.Add("없는 사람일세");
            else
            listBox1.Items.Add(PersingS(textBox1.Text, "[id] => ", "[") + " " + PersingS(textBox1.Text, "[name] =>", "[") + " " + PersingS(textBox1.Text, "[address] =>", "[") + " " + PersingS(textBox1.Text, " [department] =>", ")"));

        }
    }
}
