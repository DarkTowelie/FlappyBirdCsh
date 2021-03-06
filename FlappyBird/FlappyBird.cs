using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class f_FlappyBird : Form
    {
        int score;

        Bird bird;
        Pipe pipe1;
        Pipe pipe2;

        float pipeShift;
        float gravityValue;

        bool gameOver;

        public f_FlappyBird()
        {
            InitializeComponent();
            init();
            Invalidate();
        }

        private void init()
        {
            gameOver = false;

            gravityValue = 0.3f;
            pipeShift = 206;
            score = 0;

            bird = new Bird(50, 206);
            pipe1 = new Pipe(300, -pipeShift);
            pipe2 = new Pipe(600, -pipeShift);
        }

        private void update()
        {
            if (!gameOver)
            {
                this.Text = $"Score: {score}";
                bird.fall(gravityValue);
                pipe1.Move();
                pipe2.Move();
                CreateWall();

                if (CheckGameOver(pipe1) || CheckGameOver(pipe2) || CheckGameOver())
                    gameOver = true;

                updateScore();
                Invalidate();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void CreateWall()
        {
            Random ran = new Random();
            float newY = -pipeShift + ran.Next(-100, 100);

            if (bird.X - 300 > pipe1.X)
            {
                pipe1 = new Pipe(bird.X + 300, newY);
            }

            if (bird.X - 300 > pipe2.X)
            {
                pipe2 = new Pipe(bird.X + 300, newY);
            }
        }

        private void updateScore()
        {
            if (bird.X > pipe1.X + pipe1.SpriteSizeX && !pipe1.Passed)
            {
                score++;
                pipe1.Passed = true;
            }

            if (bird.X > pipe2.X + pipe2.SpriteSizeX && !pipe2.Passed)
            {
                score++;
                pipe2.Passed = true;
            }
        }
        private bool CheckGameOver(Pipe pipe)
        {
            float birdLBound = bird.X;
            float birdRBound = bird.X + bird.SpriteSize;
            float birdUBound = bird.Y;
            float birdBBound = bird.Y + bird.SpriteSize;

            float pipesLBound = pipe.X;
            float pipesRBound = pipe.X + pipe.SpriteSizeX;
            float topPipeBound = pipe.Y + pipe.SpriteSizeY;
            float bottomPipeBound = pipe.Y + pipe.SpriteSizeY + pipe.WindowSize;

            if ((birdLBound > pipesLBound && birdLBound < pipesRBound)
                 || (birdRBound > pipesLBound && birdRBound < pipe.X + pipesRBound))
            {
                if ((birdUBound <= topPipeBound) || (birdBBound >= bottomPipeBound))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool CheckGameOver()
        {
            if (bird.Y > 500)
                return true;
            else
                return false;
        }

        private void controll()
        {
            if (!gameOver)
            {
                bird.JumpSound.Play();
                bird.jump();
            }
            else
                init();

            if (!timer1.Enabled)
            {
                timer1.Start();
                timer1.Interval = 10;
                bird.jump();
            }
        }

        private void f_FlappyBird_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics graphics = e.Graphics;
            if (!gameOver)
                graphics.DrawImage(bird.Sprite, bird.X, bird.Y, bird.SpriteSize, bird.SpriteSize);
            else
            {
                Image deadBird = bird.Sprite;
                deadBird.RotateFlip(RotateFlipType.Rotate180FlipX);
                graphics.DrawImage(deadBird, bird.X, bird.Y, bird.SpriteSize, bird.SpriteSize);
            }
            
            graphics.DrawImage(bird.Sprite, bird.X, bird.Y, bird.SpriteSize, bird.SpriteSize);

            graphics.DrawImage(pipe1.SpriteTop, pipe1.X, pipe1.Y, pipe1.SpriteSizeX, pipe1.SpriteSizeY);
            graphics.DrawImage(pipe1.SpriteBottom, pipe1.X, pipe1.Y + pipe1.SpriteSizeY + pipe1.WindowSize, pipe1.SpriteSizeX, pipe1.SpriteSizeY);

            graphics.DrawImage(pipe2.SpriteTop, pipe2.X, pipe2.Y, pipe2.SpriteSizeX, pipe2.SpriteSizeY);
            graphics.DrawImage(pipe2.SpriteBottom, pipe2.X, pipe2.Y + pipe2.SpriteSizeY + pipe2.WindowSize, pipe2.SpriteSizeX, pipe2.SpriteSizeY);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            update();
        }

        private void f_FlappyBird_MouseDown(object sender, MouseEventArgs e)
        {
            controll();
        }

        private void f_FlappyBird_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 32)
            {
                controll();
            }
        }
    }
}
