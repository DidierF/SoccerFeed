using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class GameOpenWindow : Form
    {
        private Game game;

        public Game Game { get { return game; } }
        public GameOpenWindow()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();

            List<Game> games = new DataBaseInterface().GetAllGames();
            foreach (Game g in games)
            {
                string line = "Game" + g.ID + " (" + g.homeTeam().Name + ", " + g.awayTeam().Name + ")";
                gamesList.Items.Add(line);
            }

        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            if (gamesList.SelectedItem != null)
            {
                game = new DataBaseInterface().GetGame(gamesList.SelectedIndex);
                this.DialogResult = DialogResult.OK; 
            }
        }
    }
}
