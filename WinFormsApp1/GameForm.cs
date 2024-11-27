using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class GameForm : Form
    {
        private int rows;
        private int cols;
        private int mines;
        private Button[,] buttons;
        private Random random;
        private System.Windows.Forms.Timer timer;
        private int timeElapsed;
        private int minesLeft;
        private int cellsLeft; // Количество оставшихся клеток для открытия
        private bool isPaused = false; // Флаг для отслеживания состояния паузы

        public GameForm(int difficulty)
        {
            InitializeComponent();
            random = new Random();
            SetDifficulty(difficulty);
            CreateButtons();
            PlaceMines();
            StartTimer();
        }

        private void SetDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case 0: // Easy
                    rows = 8;
                    cols = 8;
                    mines = 10;
                    break;
                case 1: // Medium
                    rows = 16;
                    cols = 16;
                    mines = 40;
                    break;
                case 2: // Hard
                    rows = 16;
                    cols = 30;
                    mines = 99;
                    break;
            }
            minesLeft = mines;
            cellsLeft = rows * cols - mines; // Общее количество клеток минус мины
        }

        private void CreateButtons()
        {
            buttons = new Button[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(30, 30);
                    buttons[i, j].Location = new Point(j * 30, i * 30);
                    buttons[i, j].Tag = false; // Initially not a mine
                    buttons[i, j].Click += Button_Click;
                    buttons[i, j].MouseUp += Button_MouseUp;
                    this.Controls.Add(buttons[i, j]);
                }
            }
        }

        private void PlaceMines()
        {
            for (int i = 0; i < mines; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(cols);
                    y = random.Next(rows);
                } while ((bool)buttons[y, x].Tag); // Ensure the cell is not already a mine
                buttons[y, x].Tag = true; // Mark the cell as a mine
            }
        }

        private void StartTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
            timeElapsed = 0;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int x = button.Location.X / 30;
            int y = button.Location.Y / 30;
            if ((bool)button.Tag) // If the button is a mine
            {
                button.Text = "X"; // Reveal the mine
                GameOver();
            }
            else
            {
                int count = CountAdjacentMines(x, y);
                if (count > 0)
                {
                    button.Text = count.ToString(); // Show the count of adjacent mines
                }
                else
                {
                    button.Enabled = false; // Disable the button
                    OpenAdjacentCells(x, y); // Open adjacent cells
                }
                cellsLeft--;
                if (cellsLeft == 0) // If all non-mine cells are opened
                {
                    WinGame();
                }
            }
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Button button = (Button)sender;
                int x = button.Location.X / 30;
                int y = button.Location.Y / 30;
                if (button.Text == "")
                {
                    button.Text = "?"; // Mark the cell as suspected
                }
                else if (button.Text == "?")
                {
                    button.Text = ""; // Unmark the cell
                }
            }
        }

        private int CountAdjacentMines(int x, int y)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int adjacentX = x + j;
                    int adjacentY = y + i;
                    if (adjacentX >= 0 && adjacentX < cols && adjacentY >= 0 && adjacentY < rows)
                    {
                        if ((bool)buttons[adjacentY, adjacentX].Tag)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private void OpenAdjacentCells(int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int adjacentX = x + j;
                    int adjacentY = y + i;
                    if (adjacentX >= 0 && adjacentX < cols && adjacentY >= 0 && adjacentY < rows)
                    {
                        Button adjacentButton = buttons[adjacentY, adjacentX];
                        if (!adjacentButton.Enabled)
                        {
                            continue;
                        }
                        int count = CountAdjacentMines(adjacentX, adjacentY);
                        if (count > 0)
                        {
                            adjacentButton.Text = count.ToString(); // Show the count of adjacent mines
                        }
                        else
                        {
                            adjacentButton.Enabled = false; // Disable the button
                            OpenAdjacentCells(adjacentX, adjacentY); // Open adjacent cells
                        }
                        cellsLeft--;
                        if (cellsLeft == 0) // If all non-mine cells are opened
                        {
                            WinGame();
                        }
                    }
                }
            }
        }

        private void GameOver()
        {
            timer.Stop();
            MessageBox.Show("Game Over!");
            this.Close();
        }

        private void WinGame()
        {
            timer.Stop();
            MessageBox.Show("Congratulations, you won!");
            this.Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            this.Text = "Minesweeper - Time: " + timeElapsed + " seconds";
        }
    }
}

