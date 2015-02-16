using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EbertzMastermind
{
    public partial class Form1 : Form
    {
        Code userCode;
        Code secretCode;
        Key userKey;
        Panel[,] boxes;
		Panel[,] keyBoxes;
        List<Panel> colors;
		Button checkButton;
		int turnsLeft;

        public Form1()
        {
         

            InitializeComponent();
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            Panel source = (Panel)sender;
            DoDragDrop(source, DragDropEffects.Copy);
        }

        private void panel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Panel)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void panel_DragDrop(object sender, DragEventArgs e)
        {
            Panel target = (Panel)e.Data.GetData(typeof(Panel));
            Panel destination = (Panel)sender;

            destination.BackColor = target.BackColor;
            int row = 0;
            int col = 0;
            bool stop = false;

            for (int j = 0; j < 15 && !stop; ++j)
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (destination == boxes[i, j]){
                        row = i;
                        stop = false;
                        break;
                    }
                }
                col = j;
            }
			userCode[row] = target.BackColor;
        }

		private void checkButton_Click(object sender, EventArgs e) 
		{
			userKey = userCode.compareCodes(secretCode);
			Size panelSize = new Size(5, 5);
			bool gameWon = true;

			for (int i = 0; i < 5; ++i) {
				Point boxLoc = new Point(boxes[i, turnsLeft].Location.X, boxes[i, turnsLeft].Location.Y);
				keyBoxes[i, turnsLeft] = new Panel();
				keyBoxes[i, turnsLeft].Location = new Point(boxLoc.X, boxLoc.Y + 22);
				keyBoxes[i, turnsLeft].Size = panelSize;
				keyBoxes[i, turnsLeft].BackColor = userKey[i];
				System.Diagnostics.Debug.Write(keyBoxes[i, turnsLeft].BackColor.ToString());

				if (userKey[i] == Color.White || userKey[i] == Color.Empty)
					gameWon = false;

				this.Controls.Add(keyBoxes[i, turnsLeft]);
			}

			turnsLeft--;

			if (gameWon)
				processWin();
			else if (!gameWon && turnsLeft == 0)
				processLoss();
			else if(turnsLeft > 0)
				prepareNextTurn();
		}

		private void processWin() {
			string message = "You won!!";
			MessageBox.Show(message);
		}

		private void processLoss() {
			string message = "You lost :P";
			MessageBox.Show(message);
		}

		private void prepareNextTurn() {
			for (int i = 0; i < 5; ++i) {
				boxes[i, turnsLeft + 1].AllowDrop = false;
				boxes[i, turnsLeft].AllowDrop = true;
			}
		}

		private void Form1_Load(object sender, EventArgs e) {
			userCode = new Code();
			secretCode = new Code();
			userKey = new Key();
			colors = new List<Panel>(7);
			boxes = new Panel[5, 15];
			keyBoxes = new Panel[5, 15];

			secretCode.createPattern();

			checkButton = new Button();
			checkButton.Size = new Size(50, 25);
			checkButton.Location = new Point(10, 700);
			checkButton.Click += new EventHandler(checkButton_Click);
			this.Controls.Add(checkButton);

			turnsLeft = 14;

			Size panelSize = new Size(20, 20);
			int ctr = 1;

			for (int i = 0; i < 7; ++i) {
				Panel panel = new Panel();
				panel.Size = panelSize;
				panel.Location = new Point((ctr * panelSize.Width) + 300, 400);
				panel.BackColor = Code.colors[i];
				//panel.AllowDrop = true;
				panel.MouseDown += new MouseEventHandler(panel_MouseDown);
				//panel.DragEnter += new DragEventHandler(panel_DragEnter);
				//panel.DragDrop+= new DragEventHandler(panel_DragDrop);
				colors.Add(panel);
				this.Controls.Add(panel);
				++ctr;
			}

			ctr = 1;
			for (int j = 0; j < 15; ++j) {
				for (int i = 0; i < 5; ++i) {
					Panel panel = new Panel();
					panel.Size = panelSize;
					panel.Location = new Point((i + 1) * 2 * panelSize.Width, (j + 1) * 2 * panelSize.Height);
					panel.BackColor = Color.DimGray;
					panel.AllowDrop = false;
					panel.MouseDown += new MouseEventHandler(panel_MouseDown);
					panel.DragEnter += new DragEventHandler(panel_DragEnter);
					panel.DragDrop += new DragEventHandler(panel_DragDrop);
					boxes[i, j] = panel;
					this.Controls.Add(panel);
					++ctr;
				}
			}

			for (int i = 0; i < 5; ++i) {
				boxes[i, 14].AllowDrop = true;
			}
		}
    }
}
