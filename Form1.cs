using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odkladiste
{
    public partial class Form1 : Form
    {
        // založení proměnných potřebných pro hru
        bool goUp, goDown, goLeft, goRight, noUp, noDown, noLeft, noRight, isGameOver, collectedBell; //pro hráče
        bool goUpG, goDownG, goLeftG, goRightG, noUpG, noDownG, noLeftG, noRightG; //pro strážce                

        int score, newScore, lives, playerSpeed, ghostSpeed, faster, enemyCounter, counter, imageCounter;       
 
        public Form1()
        {
            InitializeComponent();
            resetGame();
        }        

        private void keyisdown(object sender, KeyEventArgs e)
        // pohyb Horace pomocí šipek - pohybuje se tím směrem, kterou šipku stiskneme
        {
            if (e.KeyCode == Keys.Up && noUp == false)
            {
                goUp = true;
                goDown = goLeft = goRight = false;
                noDown = noLeft = noRight = false;
            }
            if (e.KeyCode == Keys.Down && noDown == false)
            {
                goDown = true;
                goLeft = goRight = goUp = false;
                noLeft = noRight = noUp = false;
            }
            if (e.KeyCode == Keys.Left && noLeft == false)
            {
                goLeft = true;
                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false;
            }
            if (e.KeyCode == Keys.Right && noRight == false)
            {
                goRight = true;
                goUp = goDown = goLeft = false;
                noUp = noDown = noLeft = false;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        // při puštění klávesy šipky se směr pohybu zachová a Horace pokračuje v pohybu
        {}

        private void mainGameTimer(object sender, EventArgs e)
        // herní smyčka
        {
            textScore.Text = "Score: " + score;
            textLives.Text = "Lives: " + lives;
            imageCounter++;

            if (lives == 0)
            // Horace ztratil všechny životy a nastal konec hry
            {
                string message = score.ToString();
                gameOver(message);
            }

            if (imageCounter%40 < 20)
            // animace pohybu některých prvků
            {
                Horace.BackgroundImage = global::odkladiste.Properties.Resources.horace1;
                bell.BackgroundImage = global::odkladiste.Properties.Resources.bell1;
                arrow.BackgroundImage = global::odkladiste.Properties.Resources.arrow1;
            }
            else
            {
                Horace.BackgroundImage = global::odkladiste.Properties.Resources.horace2;
                bell.BackgroundImage = global::odkladiste.Properties.Resources.bell2;
                arrow.BackgroundImage = global::odkladiste.Properties.Resources.arrow2;
            }

            if (goUp == true) // pohyb směrem nahoru
            {
                Horace.Top -= playerSpeed;                
            }
            if (goDown == true) // pohyb směrem dolů
            {
                Horace.Top += playerSpeed;                
            }
            if (goLeft == true) // pohyb směrem doleva
            {
                Horace.Left -= playerSpeed;                
            }
            if (goRight == true) // pohyb směrem doprava
            {
                Horace.Left += playerSpeed;
            }

            if (newScore >= 50)
            // když se nasbírá dostatečné skóre, otevře se brána do dalšího kola
            {
                gameGate.Visible = false;
            }

            if (Horace.Left > 700)
            // když Horace vyjede branou mimo hrení plochu, hra se resetuje
            {
                GameTimer.Stop();
                nextGame();
            }            

            if (counter > 2000 && collectedBell == true)
            // Horace sebral zvonek a nyní může odstranit strážce
            {
                collectedBell = false;
                counter = 0;
                Guardian.BackColor = System.Drawing.Color.Red;                
            }
            else counter++;

            foreach (Control x in this.Controls)
            // procházení všech komponent formuláře
            {
                if (x is PictureBox)
                {

                    if ((string)x.Tag == "coin" && x.Visible == true)
                    // Horace sbírá jídlo - jídlo zmizí a zvýší se skóre
                    {
                        if (Horace.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            newScore += 1;
                            x.Visible = false;
                        }
                    }

                    if ((string)x.Tag == "bell" && x.Visible == true)
                    // Horace vzal zvon a může sníst stráže
                    {
                        if (Horace.Bounds.IntersectsWith(x.Bounds))
                        {
                            collectedBell = true;
                            x.Visible = false;
                            Guardian.BackColor = System.Drawing.Color.Blue;                            
                        }
                    }

                    if ((string)x.Tag == "wall" || (string)x.Tag == "gate" && x.Visible == true)
                    // Horace narazí do zdi a zarazí se o ni
                    {
                        if (goUp == true && Horace.Bounds.IntersectsWith(x.Bounds))
                        {
                            Horace.Top = Horace.Top + 2;
                            noUp = true;
                            goUp = false;
                        }
                        if (goDown == true && Horace.Bounds.IntersectsWith(x.Bounds))
                        {
                            Horace.Top = Horace.Top - 2;
                            noDown = true;
                            goDown = false;
                        }
                        if (goLeft == true && Horace.Bounds.IntersectsWith(x.Bounds))
                        {
                            Horace.Left = Horace.Left + 2;
                            noLeft = true;
                            goLeft = false;
                        }
                        if (goRight == true && Horace.Bounds.IntersectsWith(x.Bounds))
                        {
                            Horace.Left = Horace.Left - 2;
                            noRight = true;
                            goRight = false;
                        }
                    }

                    if ((string)x.Tag == "guardian")
                    {
                        if (Horace.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        // Horace se potká se strážcem
                        {
                            if (collectedBell == true)
                            // pokud je aktivní zvon, Horace strážce sní
                            {
                                score += 50;
                                newScore += 50;
                                x.Visible = false;
                                enemyCounter = 0;
                            }
                            else
                            {
                                //horace ztratí život a přesune se na začátek
                                lives--;
                                Horace.Left = 10;
                                Horace.Top = 200;
                                goLeft = goRight = goUp = goDown = false;
                            }
                        }
                        enemyCounter++;

                        if (x.Visible == false && enemyCounter >= 1000)
                        // po své porážce se střážce objeví za 10 sekund
                        {
                            x.Visible = true;
                        }
                    }

                }
            }
        }        

        private void resetGame()
        // resetování hry při jejím opětovném spuštění
        {
            textScore.Text = "Score: 0";
            score = 0;
            newScore = 0;
            lives = 5;
            ghostSpeed = 1;
            playerSpeed = 2;

            isGameOver = false;
            Horace.Left = 10;
            Horace.Top = 200;
            goLeft = goRight = goUp = goDown = false;
            Guardian.Left = 10;
            Guardian.Top = 250;           

            GameTimer.Start();
        }

        private void nextGame()
        // další kolo hry, Horace i strážce jsou přesunuti na výchozí pozice,
        // skóre i počet životů se přenáší, strážce se postupně zrychluje
        {
            newScore = 0;
            collectedBell = false;
            counter = 0;

            // úprava rychlosti strážce - zrychlí se o 1 každá 3 kola
            faster++;
            ghostSpeed = ghostSpeed + faster/3;
            
            // vrátí Horace a strážce na výchozí pozice
            Horace.Left = 10;
            Horace.Top = 200;
            goLeft = goRight = goUp = goDown = false;
            Guardian.Left = 10;
            Guardian.Top = 250;
            Guardian.BackColor = System.Drawing.Color.Red;

            foreach (Control x in this.Controls)
            // zobrazí všechny sebrané předměty (mince, zvonek)
            {
                if (x.Visible == false && x is PictureBox)
                {
                    if ((string)x.Tag == "coin" || (string)x.Tag == "bell" || (string)x.Tag == "guardian" || (string)x.Tag == "gate")
                    {
                        x.Visible = true;
                    }
                }
            }
            GameTimer.Start();
        }

        private void gameOver(string message)
        // ztratí-li Horace všechny životy, hra se zastaví a zobrazí se hláška o konci hry,
        // dosažené skóre a možnost vrátit se do menu
        {
            GameTimer.Stop();
            FinalScore.Text = "Score: " + message;
            FinalScore.Visible = true;
            GameOverMessage.Visible = true;
            BackToMenu.Visible = true;
            newGame.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        // spuštění nové hry
        {
            this.Hide();
            Form1 f3 = new Form1();
            f3.Show();
        }

        private void BackToMenu_Click(object sender, EventArgs e)
        // návrat do Menu
        {
            this.Hide();
            Menu back = new Menu();
            back.Show();
        }
    }
}