using System;
using System.Windows.Forms;

namespace odkladiste
{
    public partial class Game : Form
    {
        // založení proměnných potřebných pro hru
        bool goUp, goDown, goLeft, goRight, noUp, noDown, noLeft, noRight; // ukazatele směru pohybu Horace, když je některá true, Horace se pohybuje daným směrem
        bool goUpG, goDownG, goLeftG, goRightG; // tytéž ukazatele, ale pro pohyb strážce
        bool collectedBell; // ukazatel toho, zdali je aktivní zvon        
        int score, newScore, lives, round; // proměnné pro počítání postupu ve hře
        int playerSpeed, guardianSpeed, faster; // rychlost pohybu po mapě pro hráče a pro strážce
        int directionG; // proměnná na změnu směru strážce, je určována náhodně
        int enemyCounter, counter, imageCounter; // počítadla pro časové prodlevy ve hře
        int coinImage; // proměnná pro animaci rotace mincí (animace je zapoznámkovaná kvůli náročnosti)
        
        public Random rnd = new Random(12345); // generátor náhodných čísel

        public Game()
        // inicializace mapy po spuštění formuláře
        {
            InitializeComponent();
            resetGame();
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        // pohyb Horace pomocí šipek - pohybuje se tím směrem, kterou šipku stiskneme
        {
            if (e.KeyCode == Keys.Up && noUp == false)
                // pohyb nahoru
            {
                goUp = true;
                goDown = goLeft = goRight = false;
                noDown = noLeft = noRight = false;
            }
            if (e.KeyCode == Keys.Down && noDown == false)
                //pohyb dolů
            {
                goDown = true;
                goLeft = goRight = goUp = false;
                noLeft = noRight = noUp = false;
            }
            if (e.KeyCode == Keys.Left && noLeft == false)
                // pohyb doleva
            {
                goLeft = true;
                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false;
            }
            if (e.KeyCode == Keys.Right && noRight == false)
                //pohyb doprava
            {
                goRight = true;
                goUp = goDown = goLeft = false;
                noUp = noDown = noLeft = false;
            }
            if (e.KeyCode == Keys.Escape)
            // při stisknutí Escape se vrátíme do Menu
            {
                this.Hide();
                Menu back = new Menu();
                back.Show();
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        // při puštění klávesy šipky se směr pohybu zachová a Horace pokračuje v pohybu
        {}

        private void mainGameTimer(object sender, EventArgs e)
        // herní smyčka
        {
            // Zobrazování aktuální situace hry - skóre a počet životů
            textScore.Text = "Score: " + score;
            textLives.Text = "Lives: " + lives;
            textRounds.Text = "Round: " + round;           
            imageCounter++;

            if (lives == 0)
            // Horace ztratil všechny životy a nastal konec hry
            {
                string message = score.ToString();
                gameOver(message);
            }

            // animace pohybu některých prvků, v čase se střídají obrázky v závislosti na hodnotě
            // proměnné imageCounter
            if (imageCounter%40 < 20)            
            {
                Horace.BackgroundImage = global::odkladiste.Properties.Resources.horace1;
                bell.BackgroundImage = global::odkladiste.Properties.Resources.bell1;
                arrow.BackgroundImage = global::odkladiste.Properties.Resources.arrow1;
                if (collectedBell == false) Guardian.BackgroundImage = global::odkladiste.Properties.Resources.guardian1;
                else Guardian.BackgroundImage = global::odkladiste.Properties.Resources.guardian3;
            }
            else
            {
                Horace.BackgroundImage = global::odkladiste.Properties.Resources.horace2;
                bell.BackgroundImage = global::odkladiste.Properties.Resources.bell2;
                arrow.BackgroundImage = global::odkladiste.Properties.Resources.arrow2;
                if (collectedBell == false) Guardian.BackgroundImage = global::odkladiste.Properties.Resources.guardian2;
                else Guardian.BackgroundImage = global::odkladiste.Properties.Resources.guardian4;
            }

            // HORACE - pohyb  (změna souřadnic objektu)
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
            // konec bloku Horacova pohybu

            // GUARDIAN - pohyb (změna souřadnic objektu)
            if (goUpG == true) // pohyb směrem nahoru
            {
                Guardian.Top -= guardianSpeed;
            }
            if (goDownG == true) // pohyb směrem dolů
            {
                Guardian.Top += guardianSpeed;
            }
            if (goLeftG == true) // pohyb směrem doleva
            {
                Guardian.Left -= guardianSpeed;
            }
            if (goRightG == true) // pohyb směrem doprava
            {
                Guardian.Left += guardianSpeed;
            }                        
            // konec bloku pohybu strážce

            if (newScore >= 50)
            // když se nasbírá dostatečné skóre, otevře se brána do dalšího kola
            {
                gameGate.Visible = false;
            }

            if (Horace.Left > 520)
            // když Horace vyjede branou mimo herní plochu, hra se resetuje
            {
                GameTimer.Stop();
                nextGame();
            }
            if (Guardian.Left > 520 | Guardian.Left < 0 | Guardian.Top < 20 | Guardian.Top > 300)
            // ošetření případu, kdy by strážce opustil herní plochu
            {
                Guardian.Left = 445;
                Guardian.Top = 250;
            }

            if (counter > 2000 && collectedBell == true)
            // počítadlo pro aktivitu zvonku, když counter dosáhne 2000, zvonek se deaktivuje
            {
                collectedBell = false;
                counter = 0;
            }
            else counter++;                        

            foreach (Control x in this.Controls)
            // procházení všech komponent formuláře
            {
                if (x is PictureBox) // všechny aktivní herní prvky jsou typu Picturebox, proto stačí pracovat jen s tímto typem objektů
                {

                    if ((string)x.Tag == "coin" && x.Visible == true)
                    // Horace sbírá mince - mince zmizí a zvýší se skóre
                    {
                        if (Horace.Bounds.IntersectsWith(x.Bounds)) // pokud se Horace a mince překrývají 
                        {
                            score += 1;
                            newScore += 1;
                            x.Visible = false;
                        }

                        //animace mincí
                        // bohužel příliš zpomaluje herní smyčku, ale myslím, že se celkem povedla
                        /*
                        coinImage = (imageCounter / 5) % 4;
                        switch (coinImage)
                        {
                            case 0:
                                x.BackgroundImage = global::odkladiste.Properties.Resources.coin1;
                                break;
                            case 1:
                                x.BackgroundImage = global::odkladiste.Properties.Resources.coin3;
                                break;                            
                            case 2:
                                x.BackgroundImage = global::odkladiste.Properties.Resources.coin2;
                                break;
                            case 3:
                                x.BackgroundImage = global::odkladiste.Properties.Resources.coin4;
                                break;                            
                            default:
                                break;
                        }
                        */
                    }

                    if ((string)x.Tag == "activeWall")
                    // pohyblivé stěny se objevují a mizí v závislosti na hodnotě imageCounteru
                    {
                        if (imageCounter%900 == 0 || imageCounter % 900 == 600)
                        {
                            if (x.Visible == true) x.Visible = false;                            
                        }
                        if (imageCounter % 900 == 300)
                        {
                            if (x.Visible == false) x.Visible = true;
                        }
                    }

                    if ((string)x.Tag == "bell")
                    // Horace vzal zvon a může sníst stráže, 
                    {
                        if (x.Visible == true && Horace.Bounds.IntersectsWith(x.Bounds))
                        {
                            collectedBell = true;
                            counter = 0;
                            bell.Visible = false;                                                        
                        }
                    }

                    if (((string)x.Tag == "wall" || (string)x.Tag == "gate" || (string)x.Tag == "activeWall") && x.Visible == true)
                    // interakce se zdí
                    {
                        // když Horace narazí do zdi, zastaví se
                        if (goUp == true && Horace.Bounds.IntersectsWith(x.Bounds))
                        {
                            Horace.Top = Horace.Top + playerSpeed; // ošetření, aby se Horace nezasekl ve zdi
                            // Horace je navrácen na poslední pozici před kolizí se zdí.
                            noUp = true;
                            goUp = false;
                        }
                        if (goDown == true && Horace.Bounds.IntersectsWith(x.Bounds))
                        {
                            Horace.Top = Horace.Top - playerSpeed;
                            noDown = true;
                            goDown = false;
                        }
                        if (goLeft == true && Horace.Bounds.IntersectsWith(x.Bounds))
                        {
                            Horace.Left = Horace.Left + playerSpeed;
                            noLeft = true;
                            goLeft = false;
                        }
                        if (goRight == true && Horace.Bounds.IntersectsWith(x.Bounds))
                        {
                            Horace.Left = Horace.Left - playerSpeed;
                            noRight = true;
                            goRight = false;
                        }

                        // když strážce narazí do zdi, změní směr
                        if (goUpG == true && Guardian.Bounds.IntersectsWith(x.Bounds))
                        {
                            Guardian.Top = Guardian.Top + guardianSpeed;                            
                            goUpG = false;
                            directionG = rnd.Next(0, 2);
                            if (directionG == 0) goLeftG = true;
                            else goRightG = true;
                        }
                        if (goDownG == true && Guardian.Bounds.IntersectsWith(x.Bounds))
                        {
                            Guardian.Top = Guardian.Top - guardianSpeed;                            
                            goDownG = false;
                            directionG = rnd.Next(0, 2);
                            if (directionG == 0) goLeftG = true;
                            else goRightG = true;
                        }
                        if (goLeftG == true && Guardian.Bounds.IntersectsWith(x.Bounds))
                        {
                            Guardian.Left = Guardian.Left + guardianSpeed;                           
                            goLeftG = false;
                            directionG = rnd.Next(0, 2);
                            if (directionG == 0) goUpG = true;
                            else goDownG = true;
                        }
                        if (goRightG == true && Guardian.Bounds.IntersectsWith(x.Bounds))
                        {
                            Guardian.Left = Guardian.Left - guardianSpeed;                           
                            goRightG = false;
                            directionG = rnd.Next(0, 2);
                            if (directionG == 0) goUpG = true;
                            else goDownG = true;
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
                                Guardian.Visible = false;
                                enemyCounter = 0;
                            }
                            else
                            {
                                // horace ztratí život a přesune se na začátek
                                lives--;
                                Horace.Left = 10;
                                Horace.Top = 200;
                                goLeft = goRight = goUp = goDown = false;
                                // strážce se vrátí na výchozí pozici
                                Guardian.Left = 445;
                                Guardian.Top = 250;                                

                            }
                        }
                        enemyCounter++;

                        if (Guardian.Visible == false && enemyCounter >= 1000)
                        // po své porážce se střážce objeví za 10 sekund
                        {
                            Guardian.Visible = true;
                        }
                    }

                }
            }
        }        

        private void resetGame()
        // resetování hry při jejím opětovném spuštění
        {
            // vložení základních údajů
            textScore.Text = "Score: 0";
            score = 0;
            newScore = 0;
            lives = 5;
            round = 1;
            guardianSpeed = 1;
            playerSpeed = 2;

            // nastavení výchozí pozice pro Horace
            Horace.Left = 10;
            Horace.Top = 200;
            goLeft = goRight = goUp = goDown = false;

            //nastavení výchozí pozice pro strážce
            Guardian.Left = 445;
            Guardian.Top = 250;
            goLeftG = goRightG = goUpG = goDownG = false;

            // počáteční směr strážce
            directionG = rnd.Next(0, 2);
            if (directionG == 0) goUpG = true;
            else goDownG = true;
            
            GameTimer.Start();
        }

        private void nextGame()
        // další kolo hry, Horace i strážce jsou přesunuti na výchozí pozice,
        // skóre i počet životů se přenáší, strážce se postupně zrychluje
        {
            newScore = 0;
            collectedBell = false;
            counter = 0;
            enemyCounter = 10000;
            imageCounter = 0;
            round++; //zvýší se číslo herního kola

            // úprava rychlosti strážce - zrychlí se o 1 každá 3 kola
            // strážce zrychluje až do 10
            faster++;
            if (faster%3 == 0 & guardianSpeed < 10) guardianSpeed++;
            
            // vrátí Horace, zvonek a strážce na výchozí pozice
            Horace.Left = 10;
            Horace.Top = 200;
            goLeft = goRight = goUp = goDown = false;
            Guardian.Left = 445;
            Guardian.Top = 250;
            
            foreach (Control x in this.Controls)
            // zobrazí všechny sebrané předměty (mince, zvonek)
            {
                if (x.Visible == false && x is PictureBox) // prochází všechny prvky typu PictureBox
                {
                    if ((string)x.Tag == "coin" || (string)x.Tag == "bell" || (string)x.Tag == "guardian" || (string)x.Tag == "gate")
                    // když nalezne prvek s požadovaným tagem, zobrazí jej
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

        private void newGame_Click(object sender, EventArgs e)
        // spuštění nové hry - úplné resetování herní mapy
        {
            this.Hide();
            Game f3 = new Game();
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