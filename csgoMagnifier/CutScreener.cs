using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PrintWindowDll;



namespace csgoMagnifier
{
    public class CutScreener
    {
       

        public CutScreener(int cutWidth,int cutHeight,float scale,string selWinName) {
            
            //var s = System.Windows.Forms.Screen.AllScreens;
            this.SceenWidth =Convert.ToInt32(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width*scale);
            this.ScreenHeight = Convert.ToInt32(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height * scale);
            this.CutWidth = cutWidth;
            this.CutHeight = cutHeight;
            this.Scale = scale;
            //this.MagRate = magRate;
            this.SelWinName = selWinName;
        }

        public int SceenWidth {get;}

        public int ScreenHeight { get; }

        public int CutWidth { get; set; }

        public int CutHeight { get; set; }
        public float Scale { get; set; }
        //public int MagRate { get;  set; }
    
        public string SelWinName { get; set; }

        private Bitmap GetForeWindowBitmap() {
            //string winName = "Counter-Strike: Global Offensive";
            IntPtr hwnd= PrintWindowDll.WindowPrintor.FindWindow(null, this.SelWinName);
            //Bitmap bmp = new Bitmap(this.SceenWidth, this.ScreenHeight);
            //using (Graphics g = Graphics.FromImage(bmp))
            //{
            //    g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(this.SceenWidth, this.ScreenHeight), CopyPixelOperation.SourceCopy);
            //}

            //return bmp;
            if (hwnd == IntPtr.Zero) return null;
            Bitmap bmp= PrintWindowDll.WindowPrintor.RenderWindow(hwnd, false);
            return bmp;
        }

        public Bitmap dis;

        public Image Handle() {
            try
            {

                if (dis != null)
                {
                    dis.Dispose();
                    dis = null;
                }
                dis = new Bitmap(this.CutWidth, this.CutHeight);
                Point centerPo = new Point(this.SceenWidth / 2, this.ScreenHeight / 2);
                Point startPo = new Point(centerPo.X - this.CutWidth / 2, centerPo.Y - this.CutHeight / 2);

                //Point endPo = new Point(centerPo.X + this.CutWidth / 2, centerPo.Y + this.CutHeight);
                using (Graphics g1 = Graphics.FromImage(dis))
                {
                    using (Bitmap sbmp = GetForeWindowBitmap())
                    {
                        if (sbmp == null) return null;
                        g1.DrawImage(sbmp, new Rectangle(0, 0, this.CutWidth, this.CutHeight), new Rectangle(Convert.ToInt32(startPo.X), Convert.ToInt32(startPo.Y), this.CutWidth, this.CutHeight), GraphicsUnit.Pixel);
                    }
                }
               
                

                    //System.Windows.Forms.Clipboard.SetImage(dis);
                    return dis;
            }
            catch (Exception ex)
            {
                if (dis != null) {
                    dis.Dispose();
                    dis = null;
                }
                throw ex;
            }
            
        }  



    }
}
