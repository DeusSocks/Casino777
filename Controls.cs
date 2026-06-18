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
            if (!checkBalance(choiseBet)) return;
            Balance.Cash += (long)choiseBet.Value;
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
        public long MSbet;
        public int MScountCash;
        public int MScountDiamonds;
        private void BtnMSstart_Click(object? sender, EventArgs e)
        {
            if (!checkBalance(MSchoiseBet)) return;
            btnMSstart.Enabled = false;
            btnMSstop.Enabled = true;
            MSbet = (long)MSchoiseBet.Value;
            Balance.Cash -= MSbet;
            MSwinCash = 0;
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
            for (int i = 0; i < countBombs; i++)
            {
                int row = rand.Next(0, countBombs);
                int col = rand.Next(0, countBombs);
                if (_MSField[row][col] != Bombs.Bomb)
                { _MSField[row][col] = Bombs.Bomb; }
                else
                {
                    while (_MSField[row][col] == Bombs.Bomb)
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
            clearField(true);
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
                    MScountCash++;
                    break;
                case Bombs.Diamond:
                    clickedButton.Text = "💎";
                    MScountDiamonds++;
                    break;
                case Bombs.Bomb:
                    clickedButton.Text = "💣";
                    MSbet = 0;
                    BtnMSstop_Click(sender, e);
                    break;
            }
            lblMSstat.Text = $"Ваша ставка: {MSbet}\nВаш выигрыш: {(long)(MSbet * (1 + MScountCash * 0.3) * (Math.Pow(1.5, MScountDiamonds)))}";
        }
        public void clearField(bool enabled)
        {
            for (int row = 0; row < MSField.RowCount; row++)
            {
                for (int col = 0; col < MSField.ColumnCount; col++)
                {
                    MSbuttons[row][col].Enabled = enabled;
                    MSbuttons[row][col].Text = "";
                }
            }
        }
        private void BtnMSstop_Click(object? sender, EventArgs e)
        {

            MSwinCash = (long)(MSbet * (1 + MScountCash * 0.3) * (Math.Pow(1.5, MScountDiamonds)));
            MScountDiamonds = 0;
            MScountCash = 0;
            btnMSstart.Enabled = true;
            btnMSstop.Enabled = false;
            MessageBox.Show($"Вы выиграли: {MSwinCash}");
            Balance.Cash += MSwinCash;
            MSwinCash = 0;
            clearField(false);
            
        }
        //Общие
        private void BtnToMenu_Click(object? sender, EventArgs e)
        {
            if (slotsTable != null && slotsTable.Visible) { slotsTable.Hide(); }
            else if (MSTable != null && MSTable.Visible) { MSTable.Hide(); }
            table.Show();
            table.Controls.Add(btnToMenu, 0, 0);
            table.Controls.Add(lblBalance, 1, 0);
        }
        public void balanceChanged()
        {
            lblBalance.Text = $"Баланс: {Balance.Cash}";
        }
        public bool checkBalance(NumericUpDown bet)
        {
            long newCash = Balance.Cash - (long)bet.Value;
            if (Balance.Cash == 0)
            {
                MessageBox.Show("Ты нищий XDDDD");
                return false;
            }
            else if (newCash < 0)
            {
                MessageBox.Show("Не хвататет денег?\nПродай бабушку");
                return false;
            }
            return true;
        }
        public bool checkLoad(TableLayoutPanel? ftable)
        {
            table.Hide();
            if (ftable != null)
            {
                ftable.Controls.Add(lblBalance, 1, 0);
                ftable.Controls.Add(btnToMenu, 0, 0);
                ftable.Show();
                return false;
            }
            return true;
        }
        public void loadHeader(TableLayoutPanel? ftable, int size)
        {
            ftable.Dock = DockStyle.Fill;
            ftable.ColumnCount = 3;
            ftable.RowCount = 3;

            ftable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            ftable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            ftable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));

            ftable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            ftable.RowStyles.Add(new RowStyle(SizeType.Absolute, size));
            ftable.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            Controls.Add(ftable);

            ftable.Controls.Add(lblBalance, 1, 0);
            ftable.SetColumnSpan(lblBalance, 2);

            ftable.Controls.Add(btnToMenu, 0, 0);
        }
    }
}
