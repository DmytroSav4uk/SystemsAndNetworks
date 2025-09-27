using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DataVisualiser
{
    public partial class Form1 : Form
    {
        private readonly string path = "../../../../Shared/data.dat";
        private int[] numbers = Array.Empty<int>();

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; 
            timer1.Start(); 
        }

 
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    using (var br = new BinaryReader(fs))
                    {
                        int length = (int)(fs.Length / sizeof(int));
                        numbers = new int[length];
                        for (int i = 0; i < length; i++)
                            numbers[i] = br.ReadInt32();
                    }

                    panel1.Invalidate();
                }
            }
            catch
            {
              
            }
        }


       
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (numbers.Length == 0) return;

            Graphics g = e.Graphics;
            int width = panel1.ClientSize.Width / numbers.Length;
            int max = numbers.Max();

            for (int i = 0; i < numbers.Length; i++)
            {
                int barHeight = (int)((numbers[i] / (float)max) * (panel1.ClientSize.Height - 40));
                Rectangle rect = new Rectangle(i * width, panel1.ClientSize.Height - barHeight - 20, width - 2, barHeight);

                g.FillRectangle(Brushes.SkyBlue, rect);
                g.DrawRectangle(Pens.Black, rect);

             
                string text = numbers[i].ToString();
                SizeF size = g.MeasureString(text, this.Font);
                g.DrawString(text, this.Font, Brushes.Black,
                    i * width + (width - size.Width) / 2,
                    panel1.ClientSize.Height - barHeight - size.Height - 22);
            }
        }
    }
}
