using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameForm.window
{
    public partial class NameWindow : Form
    {
        public NameWindow()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Window.game.assignName(textBox1.Text);
            this.Close();
        }

        private void PlayerName_Load(object sender, EventArgs e)
        {

        }

    }
}
