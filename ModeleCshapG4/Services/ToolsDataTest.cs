using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModeleCshapG4.Services
{
    class ToolsDataTest
    {
        public static string[] isExtensionEqualsToTxt(OpenFileDialog openFileDialog)
        {
            string ext = openFileDialog.FileName.Split('.').Last();
            if (ext == "txt")
            {
                return System.IO.File.ReadAllLines(openFileDialog.FileName);

            }
            else
            {
                throw new Exception();
            }

        }

        public static DateTime isValidDateFormat(string d, string h)
        {
            return Convert.ToDateTime(string.Format("{0} {1}", d, h));
        }


    }
}

