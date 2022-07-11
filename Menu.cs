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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }        
        private void game_Click(object sender, EventArgs e)
        {           
            this.Hide();
            Game f3 = new Game();
            f3.Show();
        }
        private void info_Click(object sender, EventArgs e)
        {            
            this.Hide();
            Info f2 = new Info();
            f2.Show();            
        }        

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rules_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rules r = new Rules();
            r.Show();
        }
    }
}
