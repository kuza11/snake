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
        Random random = new Random();

        Point[] old_pos = new Point[2];

        int next = 0;
        bool[] occupancy = new bool[10];
        

        int step_size_x;
        int step_size_y;

        int direction;


        string path = "C:\\Users\\kuza\\source\\repos\\snake\\PC_had\\PC_had\\pic\\";
        //string path = "C:\\Users\\kuza\\git\\snake\\PC_had\\PC_had\\pic\\";

        PictureBox[] food = new PictureBox[10];
        PictureBox[] snake_body;
        PictureBox snake_head = new PictureBox();
        int snake_head_direction = 1;
        PictureBox snake_tail = new PictureBox();
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
            direction = 1;
            for(int i = 0; i < food.Length; i++)
            {
                food[i] = new PictureBox();
                food[i].Size = new Size(30, 30);
                food[i].Parent = this;
                food[i].SizeMode = PictureBoxSizeMode.Zoom;
            }
            for(int i = 0; i < occupancy.Length; i++)
            {
                occupancy[i] = false;
            }


            button1.Visible = false;
            snake_head.Image = Image.FromFile(path + "snake_head.png");
            snake_head.Size = new Size(30, 30);
            snake_head.Parent = this;
            snake_head.Location = new Point(step_size_x*5, step_size_y*5);
            snake_head.Visible = true;


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
            snake_tail.Visible = true;

            if (radioButton1.Checked)
            {
                timer1.Interval = 500;
            }
            else if (radioButton2.Checked)
            {
                timer1.Interval = 250;
            }
            else
            {
                timer1.Interval = 150;
            }

            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;

            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Food_Body();   
            Head();
            Tail();
            GameOver();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(label2.Visible == true)
            {
                label2.Visible = false;
                label1.Visible = false;
                snake_head.Visible = false;
                snake_tail.Visible = false;
                foreach(PictureBox seg in snake_body)
                {
                    seg.Visible = false;
                }
                foreach(PictureBox item in food)
                {

                    item.Visible = false;
                }
                button1.Visible = true;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
            }
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
                case "L":
                    resize = true;
                    break;
            }
            e.SuppressKeyPress = true;
        }

        private void Food_Body()
        {
            bool scored = false;
            int free = -1;
            bool fit = false;
            next++;
            if (next > 6)
            {
                for (int i = 0; i < occupancy.Length; i++)
                {
                    if (!occupancy[i])
                    {
                        free = i;
                        break;
                    }
                }
                next = 0;
                if (free >= 0)
                {
                    food[free].Image = Image.FromFile(path + "PCparts\\image_(" + random.Next(1, 11) + ").png");
                    food[free].Location = new Point(30 * random.Next(this.Width / 30 - 1), 30 * random.Next(this.Height / 30 - 1));
                    food[free].Visible = true;
                    for (int i = 0; i < snake_body.Length; i++)
                    {
                        if (food[free].Location == snake_body[i].Location)
                        {
                            fit = true;
                        }
                    }
                    while (food[free].Location == snake_head.Location || food[free].Location == snake_tail.Location || fit)
                    {
                        fit = false;
                        food[free].Location = new Point(30 * random.Next(this.Width / 30 - 1), 30 * random.Next(this.Height / 30 - 1));
                        for (int i = 0; i < snake_body.Length; i++)
                        {
                            if (food[free].Location == snake_body[i].Location)
                            {
                                fit = true;
                            }
                        }
                    }

                    occupancy[free] = true;
                }
            }

            for (int i = 0; i < food.Length; i++)
            {
                if (snake_head.Location == food[i].Location)
                {
                    scored = true;
                    food[i].Visible = false;
                    occupancy[i] = false;
                    Array.Reverse(snake_body);
                    Array.Resize(ref snake_body, snake_body.Length + 1);
                    Array.Reverse(snake_body);
                    snake_body[0] = new PictureBox();
                    snake_body[0].Image = Image.FromFile(path + "snake_body.png");
                    snake_body[0].Size = new Size(30, 30);
                    snake_body[0].Parent = this;
                    snake_body[0].Location = snake_head.Location;
                }
            }

            if (!scored)
            {
                snake_tail.Location = snake_body.Last().Location;
                snake_body.Last().Location = snake_head.Location;
                Array.Reverse(snake_body);
                Array.Resize(ref snake_body, snake_body.Length + 1);
                snake_body[snake_body.Length - 1] = snake_body[0];
                Array.Reverse(snake_body);
                Array.Resize(ref snake_body, snake_body.Length - 1);
            }
        }

        private void Head()
        {
            snake_head.Image = Image.FromFile(path + "snake_head.png");
            snake_head.BringToFront();

            if (direction - snake_head_direction > 0)
            {

                for (int i = direction - snake_head_direction; i > 0; i--)
                {

                    snake_head.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }

            }
            else if (direction - snake_head_direction < 0)
            {
                for (int i = direction - snake_head_direction; i < 0; i++)
                {
                    snake_head.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
            }
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

        private void Tail()
        {
            snake_tail.Image = Image.FromFile(path + "snake_tail.png");
            if (snake_body.Last().Location.X < snake_tail.Location.X)
            {
                snake_tail.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            else if (snake_body.Last().Location.Y > snake_tail.Location.Y)
            {
                snake_tail.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if (snake_body.Last().Location.Y < snake_tail.Location.Y)
            {
                snake_tail.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
        }

        private void GameOver()
        {
            if (snake_head.Location.X < 0 || snake_head.Location.X + 30 > this.Width || snake_head.Location.Y < 0 || snake_head.Location.Y + 31 > this.Height || snake_head.Location == snake_tail.Location)
            {
                timer1.Enabled = false;
                label2.Visible = true;
                label1.Visible = true;
            }
            for (int i = 0; i < snake_body.Length; i++)
            {
                if (snake_body[i].Location == snake_head.Location)
                {
                    timer1.Enabled = false;
                    label2.Visible = true;
                    label1.Visible = true;
                }
            }
        }
    }
}
