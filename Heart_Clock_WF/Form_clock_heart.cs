using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Heart_Clock_WF
{
    public partial class Form_clock_heart : Form
    {
        Point moveStart;
        public Form_clock_heart()
        {
            InitializeComponent();
        }

        private void Form_clock_heart_Load(object sender, EventArgs e)
        {
            
        }

        private void Form_clock_heart_Paint(object sender, PaintEventArgs e)
        {
            Graphics grafics = e.Graphics;
            GraphicsPath grafics_path = new GraphicsPath();
            int radius = ClientSize.Height > ClientSize.Width ? ClientSize.Height / 2 : ClientSize.Width / 2;

            grafics_path.AddBezier(
                new Point(ClientSize.Width / 2, ClientSize.Height / 4),
                new Point(ClientSize.Width / 3, 0),
                new Point(0, ClientSize.Height * 2 / 4),
                new Point(ClientSize.Width / 2, ClientSize.Height)
            );
            grafics_path.AddBezier(
                new Point(ClientSize.Width / 2, ClientSize.Height),
                new Point(ClientSize.Width, ClientSize.Height * 2 / 4),
                new Point(ClientSize.Width - ClientSize.Width / 3, 0),
                new Point(ClientSize.Width / 2, ClientSize.Height / 4)
            );

            PathGradientBrush gradient_brush = new PathGradientBrush(grafics_path);
            gradient_brush.CenterPoint = new PointF(ClientSize.Width / 2, ClientSize.Height / 2+30);
            gradient_brush.CenterColor = Color.White;
            gradient_brush.SurroundColors = new Color[] { Color.Maroon };

            grafics.FillPath(gradient_brush, grafics_path);

            DrawScale(Color.Black, 1, grafics, radius - 120, -30);
            Draw_Arrow_Watch_hour(grafics);
            Draw_Arrow_Watch_Minutes(grafics);
            Draw_Arrow_Watch_Secund(grafics);
            
            
        }

        private void Draw_Arrow_Watch_hour(Graphics graphics)
        {
            Point centerPoint = new Point(ClientSize.Width / 2, ClientSize.Height /2+40);
            int arrowLength = (centerPoint.X > centerPoint.Y ? centerPoint.Y : centerPoint.X) - 180;
            GraphicsContainer container = graphics.BeginContainer(
                new Rectangle(centerPoint.X, centerPoint.Y, ClientSize.Width, ClientSize.Height),
                new Rectangle(0, 0, ClientSize.Width, ClientSize.Height),

                GraphicsUnit.Pixel);
            graphics.RotateTransform((DateTime.Now.Hour + 1) * 30);
            graphics.DrawLine(Pens.Red, 0, 20, 10, 10);
            graphics.DrawLine(Pens.Red, 10, 10, -10, -10);
            graphics.DrawLine(Pens.Red, -10, -10, 0, -arrowLength);
            graphics.DrawLine(Pens.Red, 0, -arrowLength, 10, -10);
            graphics.DrawLine(Pens.Red, 10, -10, -10, 10);
            graphics.DrawLine(Pens.Red, -10, 10, 0, 20);
            graphics.EndContainer(container);
        }

        private void Draw_Arrow_Watch_Minutes(Graphics graphics)
        {
            Point centerPoint = new Point(ClientSize.Width / 2, ClientSize.Height / 2 + 40);
            int arrowLength = (centerPoint.X > centerPoint.Y ? centerPoint.Y : centerPoint.X) - 145;
            GraphicsContainer container = graphics.BeginContainer(
                new Rectangle(centerPoint.X, centerPoint.Y, ClientSize.Width, ClientSize.Height),
                new Rectangle(0, 0, ClientSize.Width, ClientSize.Height),

                GraphicsUnit.Pixel);
            graphics.RotateTransform(DateTime.Now.Minute * 6);
            graphics.DrawLine(Pens.DarkGreen, 0, 16, 8, 8);
            graphics.DrawLine(Pens.DarkGreen, 8, 8, -8, -8);
            graphics.DrawLine(Pens.DarkGreen, -8, -8, 0, -arrowLength);
            graphics.DrawLine(Pens.DarkGreen, 0, -arrowLength, 8, -8);
            graphics.DrawLine(Pens.DarkGreen, 8, -8, -8, 8);
            graphics.DrawLine(Pens.DarkGreen, -8, 8, 0, 16);
            graphics.EndContainer(container);
        }

        private void Draw_Arrow_Watch_Secund(Graphics graphics)
        {
            Point centerPoint = new Point(ClientSize.Width / 2, ClientSize.Height / 2 + 40);
            int arrowLength = (centerPoint.X > centerPoint.Y ? centerPoint.Y : centerPoint.X) - 130;
            GraphicsContainer container = graphics.BeginContainer(
                new Rectangle(centerPoint.X, centerPoint.Y, ClientSize.Width, ClientSize.Height),
                new Rectangle(0, 0, ClientSize.Width, ClientSize.Height),

                GraphicsUnit.Pixel);
            graphics.RotateTransform(DateTime.Now.Second * 6);
            graphics.DrawLine(Pens.Navy, 0, 10, 5, 5);
            graphics.DrawLine(Pens.Navy, 5, 5, -5, -5);
            graphics.DrawLine(Pens.Navy, -5, -5, 0, -arrowLength);
            graphics.DrawLine(Pens.Navy, 0, -arrowLength, 5, -5);
            graphics.DrawLine(Pens.Navy, 5, -5, -5, 5);
            graphics.DrawLine(Pens.Navy, -5, 5, 0, 10);
            graphics.EndContainer(container);
        }

        private void DrawScale(Color color, int penWidth, Graphics gr, int radius, int length)
        {
            for (int i = 1; i < 13; ++i)
            {
                GraphicsContainer container =
                    gr.BeginContainer(
                        new Rectangle(ClientSize.Width / 2, ClientSize.Height * 5 / 9+15, ClientSize.Width / 2, ClientSize.Height / 2),
                        new Rectangle(0, 0, ClientSize.Width, ClientSize.Height),
                        GraphicsUnit.Pixel);
                gr.RotateTransform(i * 30);
                gr.DrawString(i.ToString(), new Font(Font.FontFamily, 40, FontStyle.Bold), new SolidBrush(Color.Blue), length, -radius);
                gr.EndContainer(container);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form_clock_heart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void Form_clock_heart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moveStart=new Point(e.X, e.Y);
            }
        }

        private void Form_clock_heart_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Point delta_position = new Point(e.X-moveStart.X,e.Y-moveStart.Y);
                this.Location=new Point(this.Location.X+delta_position.X,
                    this.Location.Y+delta_position.Y);
            }
        }
    }
}
