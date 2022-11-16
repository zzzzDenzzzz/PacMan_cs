using System;
using System.Windows.Forms;

namespace PacMan_cs
{
    public partial class Form1 : Form
    {
        bool goUp, goDown, goLeft, goRight, isGameOver;
        int score, playerSpeed, redGhostSpeed, yellowGhostSpeed, pinkGhostX, pinkGhostY;

        public Form1()
        {
            InitializeComponent();
            ResetGame();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    goUp = true;
                    break;
                case Keys.Down:
                    goDown = true;
                    break;
                case Keys.Left:
                    goLeft = true;
                    break;
                case Keys.Right:
                    goRight = true;
                    break;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    goUp = false;
                    break;
                case Keys.Down:
                    goDown = false;
                    break;
                case Keys.Left:
                    goLeft = false;
                    break;
                case Keys.Right:
                    goRight = false;
                    break;
                case Keys.Enter:
                    if (isGameOver == true)
                    {
                        ResetGame();
                    }
                    break;
            }
        }

        private void MainGameTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score.ToString();

            MoveLeft();
            MoveRight();
            MoveDown();
            MoveUp();

            MovingLeft();
            MovingRight();
            MovingTop();
            MovingBottom();

            foreach (Control x in Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            EatCoin(x as PictureBox);
                        }
                    }

                    if ((string)x.Tag == "wall")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            GameOver("You Lose!");
                        }

                        if (pinkGhost.Bounds.IntersectsWith(x.Bounds))
                        {
                            pinkGhostX = -pinkGhostX;
                        }
                    }

                    if ((string)x.Tag == "ghost")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            GameOver("You Lose!");
                        }
                    }
                }
            }

            redGhost.Left += redGhostSpeed;
            if (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds) ||
                redGhost.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                redGhostSpeed = -redGhostSpeed;
            }

            yellowGhost.Left -= yellowGhostSpeed;
            if (yellowGhost.Bounds.IntersectsWith(pictureBox3.Bounds) ||
                yellowGhost.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                yellowGhostSpeed = -yellowGhostSpeed;
            }

            pinkGhost.Left -= pinkGhostX;
            pinkGhost.Top -= pinkGhostY;
            if (pinkGhost.Top < 0 || pinkGhost.Top > 520)
            {
                pinkGhostY = -pinkGhostY;
            }
            if (pinkGhost.Left < 0 || pinkGhost.Left > 620)
            {
                pinkGhostX = -pinkGhostX;
            }

            if (score == 53)
            {
                GameOver("You Win!");
            }
        }

        void ResetGame()
        {
            txtScore.Text = "Score: 0";
            score = 0;

            redGhostSpeed = 5;
            yellowGhostSpeed = 5;
            pinkGhostX = 5;
            pinkGhostY = 5;
            playerSpeed = 8;

            isGameOver = false;

            pacman.Left = 34;
            pacman.Top = 67;

            redGhost.Left = 216;
            redGhost.Top = 62;

            yellowGhost.Left = 397;
            yellowGhost.Top = 433;

            pinkGhost.Left = 458;
            pinkGhost.Top = 254;

            foreach (Control x in Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }

            gameTimer.Start();
        }

        void GameOver(string message)
        {
            isGameOver = true;

            gameTimer.Stop();

            txtScore.Text = "Score: " + score + Environment.NewLine + message;
        }

        void MoveLeft()
        {
            if (goLeft == true)
            {
                pacman.Left -= playerSpeed;
                pacman.Image = Properties.Resources.left;
            }
        }

        void MoveRight()
        {
            if (goRight == true)
            {
                pacman.Left += playerSpeed;
                pacman.Image = Properties.Resources.right;
            }
        }

        void MoveDown()
        {
            if (goDown == true)
            {
                pacman.Top += playerSpeed;
                pacman.Image = Properties.Resources.down;
            }
        }

        void MoveUp()
        {
            if (goUp == true)
            {
                pacman.Top -= playerSpeed;
                pacman.Image = Properties.Resources.Up;
            }
        }

        void MovingLeft()
        {
            if (pacman.Left < -10)
            {
                pacman.Left = 680;
            }
        }

        void MovingRight()
        {
            if (pacman.Left > 680)
            {
                pacman.Left = -10;
            }
        }

        void MovingTop()
        {
            if (pacman.Top < -10)
            {
                pacman.Top = 550;
            }
        }

        void MovingBottom()
        {
            if (pacman.Top > 550)
            {
                pacman.Top = 0;
            }
        }

        void EatCoin(PictureBox coin)
        {
            score++;
            coin.Visible = false;
        }
    }
}
