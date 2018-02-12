using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace team
{
    class se
    {

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
    }
}
