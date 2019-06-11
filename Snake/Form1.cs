using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snake.Model;

namespace Snake
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        SnakeBody snake;
        Food food;
        public int inX = 0;
        public int inY = 0;
        public bool inrX = true;
        public bool inrY = true;
        public int step = 15;
        public int aspeed = 1;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            graphics = pictureBox1.CreateGraphics();
            snake = new SnakeBody(30, 30);
            food = new Food();
        }

        public void MovingSnake()
        {
            snake.SetPosition(snake.GetX() + inX, snake.GetY() + inY);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);

            snake.PaintingSnake(graphics);

            food.ShowFood(graphics);

            MovingSnake();

            if (snake.Intersections(food))
            {
                food.Regeneration();
                snake.AddPointBody();
                aspeed++;
            }
            TrainingBorder();

        }

        public void TrainingBorder()
        {
            if (snake.GetX() < 0 || snake.GetY() < 0 || snake.GetX() > pictureBox1.Width || snake.GetY() > pictureBox1.Height)
            {
                snake = new SnakeBody(30, 30);
                food = new Food();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (inrX)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        inY = -step;
                        inX = 0;
                        inrX = false;
                        inrY = true;
                        break;
                    case Keys.Down:
                        inY = step;
                        inX = 0;
                        inrX = false;
                        inrY = true;
                        break;
                }
            }

            if (inrY)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        inY = 0;
                        inX = -step;
                        inrX = true;
                        inrY = false;
                        break;
                    case Keys.Right:
                        inY = 0;
                        inX = step;
                        inrX = true;
                        inrY = false;
                        break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task t1 = new Task(()=>
            {
                int port = 8005; // порт для приема входящих запросов
                                 // получаем адреса для запуска сокета
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("192.168.0.100"), port);

                // создаем сокет
                Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    // связываем сокет с локальной точкой, по которой будем принимать данные
                    listenSocket.Bind(ipPoint);

                    // начинаем прослушивание
                    listenSocket.Listen(10);

                    MessageBox.Show("Сервер запущен. Ожидание подключений...");

                    while (true)
                    {
                        Socket handler = listenSocket.Accept();
                        // получаем сообщение
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0; // количество полученных байтов
                        byte[] data = new byte[256]; // буфер для получаемых данных

                        do
                        {
                            bytes = handler.Receive(data);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (handler.Available > 0);

                        MessageBox.Show(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                        // отправляем ответ
                        string message = "ваше сообщение доставлено";
                        data = Encoding.Unicode.GetBytes(message);
                        handler.Send(data);
                        // закрываем сокет
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            t1.Start();
           
        }
    }
}
