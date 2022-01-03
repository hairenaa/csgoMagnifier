using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace csgoMagnifier
{
    class FileUtil:Singlon<FileUtil>
    {

        public static string logpath = Application.StartupPath+@"\log.txt";

        public void WriteLog(string content)
        {
            content = System.DateTime.Now + Environment.NewLine + content + Environment.NewLine;
            File.AppendAllText(logpath, content);
        }
        
    }
}
