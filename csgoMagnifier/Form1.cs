using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIndowLong_Dll;

namespace csgoMagnifier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public CutScreener cser = null;
       
        private void Form1_Load(object sender, EventArgs e)
        {
            WIndowLong_Dll.WindowLonger.SetWindowLong_Transparent(this.Handle);
            int x = (int)(0.5 * (this.Width - this.pictureBox1.Width));
            int y = (int)(0.5 * (this.Height - this.pictureBox1.Height));
            this.pictureBox1.Location = new System.Drawing.Point(x, y);
            int x1 = (int)(0.5 * (this.Width - this.pictureBox2.Width));
            int y1 = (int)(0.5 * (this.Height - this.pictureBox2.Height));
            this.pictureBox2.Location = new Point(x1, y1);
            
            //this.Opacity = 0.5;
            
            
        }

        public void CustomShow(int cutWidth,int cutHeight,int update,float magRate,string shape,string mask) {
            pictureBox1.Visible = true;
            pictureBox1.Width =(int)(cutWidth * magRate);
            pictureBox1.Height = (int)(cutHeight * magRate);
            pictureBox2.Width = (int)(pictureBox1.Width *1.2);
            pictureBox2.Height =(int)( pictureBox1.Height*1.2);
            timer1.Interval = 1000 / update;
            timer1.Enabled = true;
            if (shape == StartForm.ShapeType.circular.ToString())
            {
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, pictureBox1.Width - 3, pictureBox1.Height - 3);
                Region rg = new Region(gp);
                pictureBox1.Region = rg;

            }
            else if (shape == StartForm.ShapeType.triangle.ToString())
            {
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

                Point po1 = new Point(this.pictureBox1.Width/2,0);
                Point po2 = new Point(0, this.pictureBox1.Height);
                Point po3 = new Point(this.pictureBox1.Width,this.pictureBox1.Height);
                //po1 = this.PointToClient(po1);
                //po2 = this.PointToClient(po2);
                //po3 = this.PointToClient(po3);
                gp.AddLine(po1,po2);
                gp.AddLine(po2, po3);
                Region rg = new Region(gp);
                pictureBox1.Region = rg;


            }


            if (mask == StartForm.MaskType.mask1.ToString())
            {
                pictureBox2.Image = Properties.Resources.mask1;
                //this.Opacity = 0.6;
                this.pictureBox2.Visible = true;
            }
            else if (mask == StartForm.MaskType.mask3.ToString())
            {

                System.Drawing.Drawing2D.GraphicsPath gp1 = new System.Drawing.Drawing2D.GraphicsPath();
                gp1.AddEllipse(0, 0, pictureBox2.Width - 3, pictureBox2.Height - 3);
                Region rg1 = new Region(gp1);
                pictureBox2.Region = rg1;
                pictureBox2.Image = Properties.Resources.mask3;
                //this.Opacity = 0.6;
                this.pictureBox2.Visible = true;

            }
            else if (mask == StartForm.MaskType.mask2.ToString())
            {
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

                Point po1 = new Point(this.pictureBox2.Width / 2, 0);
                Point po2 = new Point(0, this.pictureBox2.Height);
                Point po3 = new Point(this.pictureBox2.Width, this.pictureBox2.Height);
                //po1 = this.PointToClient(po1);
                //po2 = this.PointToClient(po2);
                //po3 = this.PointToClient(po3);
                gp.AddLine(po1, po2);
                gp.AddLine(po2, po3);
                Region rg = new Region(gp);
                pictureBox2.Region = rg;
                pictureBox2.Image = Properties.Resources.mask2;
                //this.Opacity = 0.6;
                this.pictureBox2.Visible = true;
            }
            else if (mask == StartForm.MaskType.mask0.ToString())
            {
                //this.Opacity = 1;
                pictureBox2.Visible = false;
            }


            //int z_index = this.Controls.GetChildIndex(pictureBox1);
            pictureBox1.BringToFront();
            //this.Controls.SetChildIndex(pictureBox1, z_index);
            this.Show();

        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            int x = (int)(0.5 * (this.Width - this.pictureBox1.Width));
            int y = (int)(0.5 * (this.Height -this.pictureBox1.Height));
            this.pictureBox1.Location = new System.Drawing.Point(x, y);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try {
                
                if (cser == null) {
                    return;
                }
                Image re = cser.Handle();
                pictureBox1.Image = re;
            }
            catch (Exception ex) {
                string content = ex.ToString();
                FileUtil.Instance.WriteLog(content);
            }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.pictureBox1.Region?.Dispose();
            this.pictureBox2.Region ?. Dispose();
            cser.dis?.Dispose();
            
        }

      
    }
}
