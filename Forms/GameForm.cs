using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class GameForm : Form
    {
        private int rows;
        private int cols;
        private int mines;
        private int flags;
        private Timer timer;
        private int timeLeft;

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
            // Инициализация игрового поля и таймера
            timer = new Timer();
            timer.Interval = 1000; // 1 секунда
            timer.Tick += Timer_Tick;
            timeLeft = 300; // 5 минут
            timer.Start();

            // Создание игрового поля
            // ...
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
    }
}