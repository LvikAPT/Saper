using System;
using System.Windows.Forms;

namespace Saper.Forms
{
    public partial class GameForm : Form
    {
        private int rows;
        private int cols;
        private int mines;
        private int flags;
        private System.Windows.Forms.Timer timer;
        private int timeLeft;
        private bool[,] mineField; // Поле с минами

        public GameForm(int rows, int cols, int mines)
        {
            InitializeComponent();
            this.rows = rows;
            this.cols = cols;
            this.mines = mines;
            this.flags = mines;
            InitializeGame();
        }

        private void InitializeGame()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 секунда
            timer.Tick += Timer_Tick;
            timeLeft = 300; // 5 минут
            timer.Start();

            // Инициализация игрового поля
            gameGrid.ColumnCount = cols;
            gameGrid.RowCount = rows;
            for (int i = 0; i < cols; i++)
            {
                gameGrid.Columns[i].Width = 30; // Ширина колонки
            }
            for (int i = 0; i < rows; i++)
            {
                gameGrid.Rows[i].Height = 30; // Высота строки
            }

            // Инициализация поля с минами
            mineField = new bool[rows, cols];
            PlaceMines();
        }

        private void PlaceMines()
        {
            Random rand = new Random();
            for (int i = 0; i < mines; i++)
            {
                int row, col;
                do
                {
                    row = rand.Next(rows);
                    col = rand.Next(cols);
                } while (mineField[row, col]); // Повторяем, пока не найдем пустую ячейку
                mineField[row, col] = true; // Устанавливаем мину
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            lblTime.Text = $"Осталось времени: {timeLeft} сек";
            if (timeLeft <= 0)
            {
                timer.Stop();
                MessageBox.Show("Время вышло! Вы проиграли!", "Проигрыш");
                this.Close();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer.Stop();
            PauseForm pauseForm = new PauseForm(this);
            pauseForm.Show();
        }

        public void ResumeGame()
        {
            timer.Start();
        }

        // Добавьте обработчик события для ячеек игрового поля
        private void gameGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return; // Игнорируем заголовки

            // Логика открытия ячейки
            if (mineField[e.RowIndex, e.ColumnIndex])
            {
                MessageBox.Show("Вы попали на мину! Игра окончена.", "Проигрыш");
                this.Close();
            }
            else
            {
                // Открыть ячейку (например, изменить цвет или текст)
                gameGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.LightGreen;
            }
        }
    }
}