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
    public partial class Rules : Form
    {
        public Rules()
        {
            InitializeComponent();
        }

        private void menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu back = new Menu();
            back.Show();
        }
    }
}
