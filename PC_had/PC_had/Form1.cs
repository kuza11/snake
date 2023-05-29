using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
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

        int direction = 1;


        //string path = "C:\\Users\\kuza\\source\\repos\\snake\\PC_had\\PC_had\\pic\\";
        string path = "C:\\Users\\kuza\\git\\snake\\PC_had\\PC_had\\pic\\";

        PictureBox[] snake_body;
        int[] snake_body_direction = { 1 };
        PictureBox snake_head = new PictureBox();
        int snake_head_direction = 1;
        PictureBox snake_tail = new PictureBox();
        int snake_tail_direction = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            step_size_x = 30;
            step_size_y = 30;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            snake_head.Image = Image.FromFile(path + "snake_head.png");
            snake_head.Size = new Size(30, 30);
            snake_head.Parent = this;
            snake_head.Location = new Point(step_size_x*5, step_size_y*5);


            snake_body = new PictureBox[1];
            snake_body[0] = new PictureBox();
            snake_body[0].Image = Image.FromFile(path + "snake_body.png");
            snake_body[0].Size = new Size(30, 30);
            snake_body[0].Parent = this;
            snake_body[0].Location = new Point(snake_head.Location.X - 30, snake_head.Location.Y);

            snake_tail.Image = Image.FromFile(path + "snake_tail.png");
            snake_tail.Size = new Size(30, 30);
            snake_tail.Parent = this;
            snake_tail.Location = new Point(snake_body.Last().Location.X - 30 , snake_body.Last().Location.Y);

           
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int t = 0;
            snake_tail.Location = snake_body.Last().Location;
            for (int i = 0; i < snake_body.Length; i++)
            {
                if(i == 0)
                {
                    snake_body[i].Location = snake_head.Location;
                    if(direction - snake_body_direction[i] == 1 || direction - snake_body_direction[i] == -3)
                    {
                        snake_body[i].Image = Image.FromFile(path + "snake_body.png");
                        snake_body[i].Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }else if (direction - snake_body_direction[i] == -1 || direction - snake_body_direction[i] == 3)
                    {
                        snake_body[i].Image = Image.FromFile(path + "snake_body.png");
                        snake_body[i].Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                }
                else
                {
                    snake_body[i].Location = snake_body[i-1].Location; 
                }
            }
            
            snake_head.Image = Image.FromFile(path + "snake_head.png");

            if (direction - snake_head_direction > 0)
            {
                
                for (int i = direction - snake_head_direction; i > 0; i--)
                {
                    
                    snake_head.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                
            }
            else if(direction - snake_head_direction < 0)
            {
                for(int i = direction - snake_head_direction; i < 0; i++)
                {
                    snake_head.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
            }
            label2.Text = Convert.ToString(direction);
            switch (direction)
            {
                case 1:
                    snake_head.Location = new Point(snake_head.Location.X + step_size_x, snake_head.Location.Y);
                    break;
                case 2:
                    snake_head.Location = new Point(snake_head.Location.X, snake_head.Location.Y - step_size_y);
                    break;
                case 3:
                    snake_head.Location = new Point(snake_head.Location.X - step_size_x, snake_head.Location.Y);
                    break;
                case 4:
                    snake_head.Location = new Point(snake_head.Location.X, snake_head.Location.Y + step_size_y);
                    break;
            }
            

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (Convert.ToString(e.KeyCode)){
                case "D":
                    direction = 1;
                    break;
                case "W":
                    direction = 2;
                    break;
                case "A":
                    direction = 3;
                    break;
                case "S":
                    direction = 4;
                    break;
            }
            e.SuppressKeyPress = true;
        }
    }
}
