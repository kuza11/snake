using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_had
{
    public partial class Form1 : Form
    {
        int cnt;
        int step_size_x;
        int step_size_y;
        string path = "C:\\Users\\kuza\\source\\repos\\PC_had\\PC_had\\pic\\";
        PictureBox[] snake_body;
        PictureBox snake_head = new PictureBox();
        PictureBox snake_tail = new PictureBox();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            step_size_x = (int)this.Size.Width/30;
            step_size_y = (int)this.Size.Height/30;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            snake_head.ImageLocation = path + "snake_head.png";
            snake_head.Size = new Size(30, 38);
            snake_head.Parent = this;
            snake_head.Location = new Point(step_size_x*5, step_size_y*5);

            snake_body = new PictureBox[1];
            snake_body[0] = new PictureBox();
            snake_body[0].ImageLocation = path + "snake_body.png";
            snake_body[0].Size = new Size(30, 26);
            snake_body[0].Parent = this;
            snake_body[0].Location = new Point(snake_head.Location.X - 30, snake_head.Location.Y + 6);

            snake_tail.ImageLocation = path + "snake_tail.png";
            snake_tail.Size = new Size(30, 26);
            snake_tail.Parent = this;
            snake_tail.Location = new Point(snake_body.Last().Location.X - 30 , snake_body.Last().Location.Y);

            cnt = 6;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            snake_tail.Location = snake_body.Last().Location;
            for (int i = 0; i < snake_body.Length; i++)
            {
                if(i == 0)
                {
                    snake_body[i].Location = new Point(snake_head.Location.X, snake_head.Location.Y + 6);
                }
                else
                {
                    snake_body[i].Location = snake_body[i-1].Location; 
                }
            }
            snake_head.Location = new Point(step_size_x * cnt, step_size_y*5);
            cnt++;
        }
    }
}
