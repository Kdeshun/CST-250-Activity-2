using ChessBoardModel1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChessBoardGUIApp
{
    public partial class Form1 : Form
    {
        static public Board myBoard = new Board(8);
        public System.Windows.Forms.Button[,] btnGrid = new System.Windows.Forms.Button[myBoard.Size, myBoard.Size];
        private string selectedChessPiece;
        public Form1()
        {
            InitializeComponent();
            //comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            populateGrid();
        }

        public void populateGrid()
        {
            int buttonSize = panel1.Width / myBoard.Size;
            panel1.Height = panel1.Width;

            for (int r = 0; r < myBoard.Size; r++)
            {
                for (int c = 0; c < myBoard.Size; c++)
                {
                    btnGrid[r, c] = new System.Windows.Forms.Button();

                    btnGrid[r, c].Width = buttonSize;
                    btnGrid[r, c].Height = buttonSize;

                    btnGrid[r, c].Click += Grid_Button_Click;
                    panel1.Controls.Add(btnGrid[r, c]);
                    btnGrid[r, c].Location = new Point(buttonSize * r, buttonSize * c);

                    btnGrid[r, c].Text = r.ToString() + "|" + c.ToString();

                    btnGrid[r, c].Tag = r.ToString() + "|" + c.ToString();
                }
            }
        }

        private void Grid_Button_Click(object sender, EventArgs e)
        {
            string[] strArr = (sender as System.Windows.Forms.Button).Tag.ToString().Split('|');
            int r = int.Parse(strArr[0]);
            int c = int.Parse(strArr[1]);

            Cell currentCell = myBoard.theGrid[r, c];
            myBoard.MarkNextLegalMoves(currentCell, selectedChessPiece);
            updateButtonLabels();

            for (int i = 0; i < myBoard.Size; i++)
            {
                for (int j = 0; j < myBoard.Size; j++)
                {
                    btnGrid[i, j].BackColor = default(Color);
                }
            }

            (sender as System.Windows.Forms.Button).BackColor = Color.Cornsilk;
            (sender as System.Windows.Forms.Button).Text = selectedChessPiece;
        }

        public void updateButtonLabels()
        {
            for (int r = 0; r < myBoard.Size; r++)
            {
                for (int c = 0; c < myBoard.Size; c++)
                {
                    btnGrid[r, c].Text = "";
                    if (myBoard.theGrid[r, c].CurrentlyOccupied) btnGrid[r, c].Text = "Knight";
                    if (myBoard.theGrid[r, c].LegalNextMove) btnGrid[r, c].Text = "Legal";
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedChessPiece = comboBox1.SelectedItem.ToString();
        }
    }
}