using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casino777
{
    public partial class MainForm : Form
    {
        //Слоты
        private void BtnDep_Click(object? sender, EventArgs e)
        {
            long newCash = Balance.Cash - (long)choiseBet.Value;
            if (Balance.Cash == 0)
            {
                MessageBox.Show("Ты нищий XDDDD");
                return;
            }
            else if (newCash < 0)
            {
                MessageBox.Show("Не хвататет денег?\nПродай бабушку");
                return;
            }
            Balance.Cash = newCash;
            timerSlots.Start();
            countSpin = 0;

        }

        private void TimerSlots_Tick(object? sender, EventArgs e)
        {
            countSpin++;
            long winCash = 0;
            Random random = new Random();
            for (int i = 0; i < 3; i++)
            {
                int randInt = random.Next(0, payLines.Length); // Используем длину массива (было 7, а элементов 8)
                payLinesLabels[i].Text = payLines[randInt];
            }
            if (countSpin >= 30)
            {
                timerSlots.Stop();
                string s1 = payLinesLabels[0].Text;
                string s2 = payLinesLabels[1].Text;
                string s3 = payLinesLabels[2].Text;
                // Джекпот: все три совпали
                if (s1 == s2 && s2 == s3)
                {
                    winCash = (long)(choiseBet.Value * 10);
                }
                // Минимальный выигрыш: совпали любые два
                else if (s1 == s2 || s1 == s3 || s2 == s3)
                {
                    winCash = (long)(choiseBet.Value * 2);
                }
                Balance.Cash += winCash;
                if (winCash > 0)
                {
                    MessageBox.Show($"Вы выиграли {winCash}\n{s1} {s2} {s3}");
                }
                return;
            }
        }
        //Сапер
        private void BtnMSstart_Click(object? sender, EventArgs e)
        {
            btnMSstart.Enabled = false;
            long bet = (long)MSchoiseBet.Value;
            Balance.Cash -= bet;
            _MSField = new List<List<Bombs>>(MSField.RowCount);
            for (int row = 0; row < MSField.RowCount; row++)
            {
                _MSField.Add(new List<Bombs>(MSField.ColumnCount));
                for (int col = 0; col < MSField.ColumnCount; col++)
                {
                    _MSField[row].Add(Bombs.Cash);
                }
            }
            int countBombs = 5;
            int countDiamonds = 5;
            Random rand = new Random();
            for (int i = 0; i < countBombs; i++) {
                int row = rand.Next(0, countBombs);
                int col = rand.Next(0, countBombs);
                if (_MSField[row][col] != Bombs.Bomb)
                    { _MSField[row][col] = Bombs.Bomb; }
                else
                {
                    while(_MSField[row][col] == Bombs.Bomb)
                    {
                        row = rand.Next(0, countBombs);
                        col = rand.Next(0, countBombs);
                    }
                    _MSField[row][col] = Bombs.Bomb;
                }
            }
            for (int i = 0; i < countDiamonds; i++)
            {
                int row = rand.Next(0, countDiamonds);
                int col = rand.Next(0, countDiamonds);
                if (_MSField[row][col] != Bombs.Diamond && _MSField[row][col] != Bombs.Bomb)
                { _MSField[row][col] = Bombs.Diamond; }
                else
                {
                    while (_MSField[row][col] == Bombs.Diamond || _MSField[row][col] == Bombs.Bomb)
                    {
                        row = rand.Next(0, countDiamonds);
                        col = rand.Next(0, countDiamonds);
                    }
                    _MSField[row][col] = Bombs.Diamond;
                }
            }
            for (int row = 0; row < MSField.RowCount; row++)
            {
                MSbuttons.Add(new List<Button>());
                for (int col = 0; col < MSField.ColumnCount; col++)
                {
                    MSbuttons[row][col].Enabled = true;
                    MSbuttons[row][col].Text = "";
                    MSbuttons[row][col].Click += buttonsMS_Click;
                }
            }
        }
        private void buttonsMS_Click(object? sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            TableLayoutPanelCellPosition pos = MSField.GetCellPosition(clickedButton);
            int row = pos.Row; 
            int col = pos.Column; 
            clickedButton.Enabled = false;
            switch (_MSField[row][col])
            {
                case Bombs.Cash:
                    clickedButton.Text = "$";

                    break;
                case Bombs.Diamond:
                    clickedButton.Text = "💎";
                    break;
                case Bombs.Bomb:
                    clickedButton.Text = "💣";
                    break;
            }
        }
        private void BtnMSstop_Click(object? sender, EventArgs e)
        {
            
        }
        //Общие
        private void BtnToMenu_Click(object? sender, EventArgs e)
        {
            if (slotsTable!=null && slotsTable.Visible) { slotsTable.Hide(); }
            else if (MSTable!=null && MSTable.Visible) { MSTable.Hide(); }
            table.Show();
        }
        public void balanceChanged()
        {
            lblBalance.Text = $"Баланс: {Balance.Cash}";
        }
        public static class Balance
        {
            public static event Action OnCashChanged;
            private static long _cash;
            static public long Cash
            {
                get { return _cash; }
                set
                {
                    _cash = value;
                    OnCashChanged?.Invoke();
                }
            }
            static Balance() { _cash = 1000; }
        }
    }
}
