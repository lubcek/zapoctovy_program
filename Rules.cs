using System;
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
