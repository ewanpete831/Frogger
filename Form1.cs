//created by Ewan Peterson, 06/17/21 for ICS3U

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Frogger
{
    public partial class Form1 : Form
    {
        Rectangle player = new Rectangle(240, 630, 70, 70);
        int playerMoveDistance = 70;

        List<Rectangle> vehicles = new List<Rectangle>();
        List<int> vehicleSpeeds = new List<int>();
        List<int> vehicleColours = new List<int>();
        List<Rectangle> obstacles = new List<Rectangle>();

        int vehicleWitdh = 150;
        int vehicleHeight = 70;

        int defaultMoveTimer = 9;

        string gameState = "starting";

        string endText = "";

        SoundPlayer horn = new SoundPlayer(Properties.Resources.horn);
        SoundPlayer win = new SoundPlayer(Properties.Resources.winner);

        bool upDown = false;
        bool downDown = false;
        bool leftDown = false;
        bool rightDown = false;

        Random randNum = new Random();
        int randValue = 0;
        int[] lastLanes = new int[5];

        int vehicleTimer = 5;
        int lastPlayerX = 0;
        int lastPlayerY = 0;


        int lives = 3;
        int moveTimer = 7;
        string frogDirection = "up";


        SolidBrush safeBrush = new SolidBrush(Color.Green);
        SolidBrush roadBrush = new SolidBrush(Color.DarkGray);
        SolidBrush vehicleBrush = new SolidBrush(Color.White);
        SolidBrush obstacleBrush = new SolidBrush(Color.Gold);


        public Form1()
        {
            obstacles.Add(new Rectangle(240, 210, 70, 70));
            obstacles.Add(new Rectangle(100, 210, 70, 70));
            obstacles.Add(new Rectangle(-40, 210, 70, 70));
            obstacles.Add(new Rectangle(380, 210, 70, 70));
            obstacles.Add(new Rectangle(520, 210, 70, 70));


            InitializeComponent();
        }

        private void GameInitialize()
        {
            player.X = 240;
            player.Y = 630;

            lives = 3;

            titleLabel.Text = "";
            subTitleLabel.Text = "";

            this.Size = new Size(550, 700);

            vehicles.Clear();
            vehicleSpeeds.Clear();
            vehicleColours.Clear();

            gameTimer.Enabled = true;
            gameState = "running";
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
                case Keys.Up:
                    upDown = false;
                    break;
                case Keys.Down:
                    downDown = false;
                    break;

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
                case Keys.Up:
                    upDown = true;
                    break;
                case Keys.Down:
                    downDown = true;
                    break;
                case Keys.Space:
                    if (gameState == "starting" || gameState == "over")
                    {
                        GameInitialize();
                    }
                    break;
                case Keys.Escape:
                    if (gameState == "starting" || gameState == "over")
                    {
                        Application.Exit();
                    }
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move player
            MovePlayer();

            //check for vehicle collision
            CheckVehicleCollision();

            //check for collision with obstacle
            CheckObstacleCollision();

            //create new vehicles
            CreateVehicles();

            //move vehicles
            MoveVehicles();

            //remove vehicles off screen
            RemoveVehicles();

            //check for player win, or game over
            CheckForGameOver();

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (gameState == "starting")
            {
                titleLabel.Text = "Frogger";
                subTitleLabel.Text = "Press 'Space' to Start      Press 'Esc' to Exit";
            }
            else if (gameState == "running")
            {

                e.Graphics.FillRectangle(roadBrush, 0, 0, this.Width, this.Height);
                e.Graphics.FillRectangle(safeBrush, 0, this.Height - 70, this.Width, 70);
                e.Graphics.FillRectangle(safeBrush, 0, 210, this.Width, 70);
                e.Graphics.FillRectangle(safeBrush, 0, 0, this.Width, 70);

                for (int i = 0; i < vehicles.Count(); i++)
                {
                    switch (vehicleColours[i])
                    {
                        case 1:
                            vehicleBrush.Color = Color.White;
                            e.Graphics.FillRectangle(vehicleBrush, vehicles[i]);
                            break;

                        case 2:
                            vehicleBrush.Color = Color.Red;
                            e.Graphics.FillRectangle(vehicleBrush, vehicles[i]);
                            break;

                        case 3:
                            vehicleBrush.Color = Color.Black;
                            e.Graphics.FillRectangle(vehicleBrush, vehicles[i]);
                            break;

                        case 4:
                            vehicleBrush.Color = Color.Blue;
                            e.Graphics.FillRectangle(vehicleBrush, vehicles[i]);
                            break;
                    }
                }
                for (int i = 0; i < obstacles.Count; i++)
                {
                    e.Graphics.FillEllipse(obstacleBrush, obstacles[i]);
                }

                if (frogDirection == "up")
                {
                    e.Graphics.RotateTransform(0);
                    e.Graphics.DrawImage(Properties.Resources.frog, player);
                    e.Graphics.ResetTransform();
                }
                else if (frogDirection == "down")
                {
                    e.Graphics.TranslateTransform(player.X + player.Width, player.Y + player.Height);
                    e.Graphics.RotateTransform(180);
                    e.Graphics.ScaleTransform(0.15F, 0.18F);
                    e.Graphics.DrawImage(Properties.Resources.frog, 0, 0);
                    e.Graphics.ResetTransform();
                }
                else if (frogDirection == "left")
                {
                    e.Graphics.TranslateTransform(player.X, player.Y + player.Height);
                    e.Graphics.RotateTransform(270);
                    e.Graphics.ScaleTransform(0.15F, 0.18F);
                    e.Graphics.DrawImage(Properties.Resources.frog, 0, 0);
                    e.Graphics.ResetTransform();
                }
                else if (frogDirection == "right")
                {
                    e.Graphics.TranslateTransform(player.X + player.Width, player.Y);
                    e.Graphics.RotateTransform(90);
                    e.Graphics.ScaleTransform(0.15F, 0.18F);
                    e.Graphics.DrawImage(Properties.Resources.frog, 0, 0);
                    e.Graphics.ResetTransform();
                }
            }
            else if (gameState == "over")
            {
                this.Size = new Size(550, 500);
                titleLabel.Text = "GAME OVER";
                subTitleLabel.Text = $"{endText}";
                subTitleLabel.Text += "\nPress 'Space' to Start Over      Press 'Esc' to Exit";
            }
        }

        private void RemoveVehicles()
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                if ((vehicles[i].Y == 70 || vehicles[i].Y == 280 || vehicles[i].Y == 420 || vehicles[i].Y == 560) && vehicles[i].X < 0 - vehicleWitdh)
                {
                    vehicles.RemoveAt(i);
                    vehicleSpeeds.RemoveAt(i);
                    vehicleColours.RemoveAt(i);
                }
                else if (vehicles[i].X > this.Width)
                {
                    vehicles.RemoveAt(i);
                    vehicleSpeeds.RemoveAt(i);
                    vehicleColours.RemoveAt(i);
                }
            }
        }

        private void CreateVehicles()


        {
            int randomSpeed = 0;

            if (vehicleTimer > 8)
            {
                randValue = randNum.Next(1, 9);

                if (randValue != lastLanes[0] && randValue != lastLanes[1] && randValue != lastLanes[2] && randValue != lastLanes[3] && randValue != lastLanes[4])
                {
                    switch (randValue)
                    {
                        case 1:
                            vehicles.Add(new Rectangle(this.Width, 70, vehicleWitdh, vehicleHeight));
                            randomSpeed = randNum.Next(6, 11);
                            vehicleSpeeds.Add(randomSpeed);
                            vehicleColours.Add(randNum.Next(1, 5));
                            vehicleTimer = 0;
                            break;

                        case 2:
                            vehicles.Add(new Rectangle(0 - vehicleWitdh, 140, vehicleWitdh, vehicleHeight));
                            randomSpeed = randNum.Next(6, 11);
                            vehicleSpeeds.Add(randomSpeed);
                            vehicleColours.Add(randNum.Next(1, 5));
                            vehicleTimer = 0;
                            break;

                        case 4:
                            vehicles.Add(new Rectangle(this.Width, 280, vehicleWitdh, vehicleHeight));
                            randomSpeed = randNum.Next(6, 11);
                            vehicleSpeeds.Add(randomSpeed);
                            vehicleColours.Add(randNum.Next(1, 5));
                            vehicleTimer = 0;
                            break;

                        case 5:
                            vehicles.Add(new Rectangle(0 - vehicleWitdh, 350, vehicleWitdh, vehicleHeight));
                            randomSpeed = randNum.Next(6, 11);
                            vehicleSpeeds.Add(randomSpeed);
                            vehicleColours.Add(randNum.Next(1, 5));
                            vehicleTimer = 0;
                            break;

                        case 6:
                            vehicles.Add(new Rectangle(this.Width, 420, vehicleWitdh, vehicleHeight));
                            randomSpeed = randNum.Next(6, 11);
                            vehicleSpeeds.Add(randomSpeed);
                            vehicleColours.Add(randNum.Next(1, 5));
                            vehicleTimer = 0;
                            break;

                        case 7:
                            vehicles.Add(new Rectangle(0 - vehicleWitdh, 490, vehicleWitdh, vehicleHeight));
                            randomSpeed = randNum.Next(6, 11);
                            vehicleSpeeds.Add(randomSpeed);
                            vehicleColours.Add(randNum.Next(1, 5));
                            vehicleTimer = 0;
                            break;

                        case 8:
                            vehicles.Add(new Rectangle(this.Width, 560, vehicleWitdh, vehicleHeight));
                            randomSpeed = randNum.Next(6, 11);
                            vehicleSpeeds.Add(randomSpeed);
                            vehicleColours.Add(randNum.Next(1, 5));
                            vehicleTimer = 0;
                            break;

                        default:

                            break;
                    }
                }
                lastLanes[4] = lastLanes[3];
                lastLanes[3] = lastLanes[2];
                lastLanes[2] = lastLanes[1];
                lastLanes[1] = lastLanes[0];
                lastLanes[0] = randValue;
            }

            vehicleTimer++;
        }

        private void MovePlayer()
        {
            moveTimer--;

            if (upDown == true && player.Y > 0 && moveTimer < 1)
            {
                player.Y -= playerMoveDistance;
                moveTimer = defaultMoveTimer;
                frogDirection = "up";
            }
            if (downDown == true && player.Y < 630 && moveTimer < 1)
            {
                player.Y += playerMoveDistance;
                moveTimer = defaultMoveTimer;
                frogDirection = "down";
            }
            if (leftDown == true && player.X > 0 && moveTimer < 1)
            {
                player.X -= playerMoveDistance;
                moveTimer = defaultMoveTimer;
                frogDirection = "left";
            }
            if (rightDown == true && player.X < this.Width - player.Width && moveTimer < 1)
            {
                player.X += playerMoveDistance;
                moveTimer = defaultMoveTimer;
                frogDirection = "right";
            }
        }

        private void CheckVehicleCollision()
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (player.IntersectsWith(vehicles[i]))
                {
                    horn.Play();
                    lives--;
                    player.X = 240;
                    player.Y = 630;
                    frogDirection = "up";
                }
            }
            switch (lives)
            {
                case 3:
                    life1Picture.BackgroundImage = Properties.Resources.heart;
                    life2Picture.BackgroundImage = Properties.Resources.heart;
                    life3Picture.BackgroundImage = Properties.Resources.heart;
                    break;

                case 2:
                    life1Picture.BackgroundImage = Properties.Resources.heart;
                    life2Picture.BackgroundImage = Properties.Resources.heart;
                    life3Picture.BackgroundImage = Properties.Resources.greyheart;
                    break;

                case 1:
                    life1Picture.BackgroundImage = Properties.Resources.heart;
                    life2Picture.BackgroundImage = Properties.Resources.greyheart;
                    life3Picture.BackgroundImage = Properties.Resources.greyheart;
                    break;

                case 0:
                    life1Picture.BackgroundImage = Properties.Resources.greyheart;
                    life2Picture.BackgroundImage = Properties.Resources.greyheart;
                    life3Picture.BackgroundImage = Properties.Resources.greyheart;
                    break;
            }

        }

        private void MoveVehicles()
        {
            for (int i = 0; i < vehicles.Count(); i++)
            {
                if (vehicles[i].Y == 140 || vehicles[i].Y == 350 || vehicles[i].Y == 490)
                {
                    int x = vehicles[i].X + vehicleSpeeds[i];
                    vehicles[i] = new Rectangle(x, vehicles[i].Y, vehicleWitdh, vehicleHeight);
                }
                else
                {
                    int x = vehicles[i].X - vehicleSpeeds[i];
                    vehicles[i] = new Rectangle(x, vehicles[i].Y, vehicleWitdh, vehicleHeight);
                }
            }
        }

        private void CheckForGameOver()
        {
            if (player.Y == 0)
            {
                win.Play();
                gameState = "over";
                endText = "You made it to the end!";
                gameTimer.Enabled = false;
            }
            if (lives == 0)
            {
                gameState = "over";
                endText = "You got hit by a vehicle";
                gameTimer.Enabled = false;

            }
        }

        private void CheckObstacleCollision()
        {
            for (int i = 0; i < obstacles.Count; i++)
            {
                if (player.IntersectsWith(obstacles[i]))
                {
                    player.X = lastPlayerX;
                    player.Y = lastPlayerY;
                }
            }

            lastPlayerX = player.X;
            lastPlayerY = player.Y;
        }
    }
}


