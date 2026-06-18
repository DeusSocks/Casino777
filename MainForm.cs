using System.Windows.Forms;

namespace Casino777
{
    public partial class MainForm : Form
    {
        Label lblBalance;
        NumericUpDown choiseBet;
        string[] payLines = { "🍒","🍋","🍊","🍇","🍉","💎","🌟","7"};
        //MainForm V
        TableLayoutPanel table;
        TableLayoutPanel slotsWindow;
        List<Label> payLinesLabels;
        int countSpin;

        TableLayoutPanel slotsTable;
        public MainForm()
        {
            InitializeComponent();
            lblBalance = new Label();
            Balance.OnCashChanged += balanceChanged;
            timerSlots.Tick += TimerSlots_Tick;
        }

        

        public void MainForm_Load(object sender, EventArgs e)
        {
            table = new TableLayoutPanel();
            table.Dock = DockStyle.Fill;
            table.ColumnCount = 3;
            table.RowCount = 3;
            
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));

            table.RowStyles.Add(new RowStyle(SizeType.Absolute,50));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            Controls.Add(table);

            table.Controls.Add(lblBalance,0,0);
            table.SetColumnSpan(lblBalance, 3);
            lblBalance.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBalance.AutoSize = true;
            lblBalance.TextAlign = ContentAlignment.TopRight;
            lblBalance.Text = $"Баланс: {Balance.Cash}";

            Label lblCasino = new Label();
            lblCasino.Text = "НеКазино 777";
            lblCasino.TextAlign = ContentAlignment.TopCenter;
            lblCasino.Dock = DockStyle.Fill;
            lblCasino.Font = new Font("Impact", 36, FontStyle.Regular);
            table.Controls.Add(lblCasino,1,0);
            table.SetColumnSpan(lblCasino, 3);

            Button btnSlots = new Button();
            btnSlots.Text = "Слоты";

            btnSlots.Click += slotsLoad;

            List<Button> menuButtons = new List<Button>() {
                btnSlots
            };
            int i = 0;
            foreach(Button button in menuButtons)
            {
                table.RowCount++;
                table.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 100/menuButtons.Count));
                table.Controls.Add(button,1,3);
                button.AutoSize = true;
                button.Anchor = AnchorStyles.None;
            }
        }

        private void slotsLoad(object sender, EventArgs e)
        {
            table.Hide();
            slotsTable = new TableLayoutPanel();
            slotsTable.Dock = DockStyle.Fill;
            slotsTable.ColumnCount = 3;
            slotsTable.RowCount = 3;

            slotsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            slotsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            slotsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));

            slotsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            slotsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 175));
            slotsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            Controls.Add(slotsTable);

            slotsTable.Controls.Add(lblBalance, 0, 0);
            slotsTable.SetColumnSpan(lblBalance, 3);
            lblBalance.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBalance.AutoSize = true;
            lblBalance.TextAlign = ContentAlignment.TopRight;
            lblBalance.Text = $"Баланс: {Balance.Cash}";

            slotsWindow = new TableLayoutPanel();
            slotsWindow.RowCount = 1;
            slotsWindow.ColumnCount = 3;
            slotsWindow.ColumnStyles.Add
                (new ColumnStyle(SizeType.Absolute, 150));
            slotsWindow.ColumnStyles.Add
                (new ColumnStyle(SizeType.Absolute, 150));
            slotsWindow.ColumnStyles.Add
                (new ColumnStyle(SizeType.Absolute, 150));
            slotsWindow.RowStyles.Add
                (new RowStyle(SizeType.Absolute, 175));
            slotsWindow.Anchor = AnchorStyles.Top;
            slotsWindow.AutoSize = true;
            slotsTable.Controls.Add(slotsWindow,0,1);
            slotsTable.SetColumnSpan(slotsWindow,3);
            slotsWindow.CellBorderStyle = 
                TableLayoutPanelCellBorderStyle.Single;
             payLinesLabels = new List<Label>();
            for (int i = 0; i < 3; i++)
            {
                payLinesLabels.Add(new Label());
                slotsWindow.Controls.Add(payLinesLabels[i]);
                payLinesLabels[i].Dock = DockStyle.Fill;
                payLinesLabels[i].Font = new Font(
                    slotsTable.Font.FontFamily,30, slotsTable.Font.Style);
            }
            Button btnDep = new Button();
            btnDep.Text = "Депнуть";
            slotsTable.RowCount++;
            slotsTable.RowStyles.Add(
                new RowStyle(SizeType.Absolute,50));
            slotsTable.Controls.Add(btnDep, 1, 3);
            btnDep.AutoSize = true;
            btnDep.Anchor = AnchorStyles.Top;
            btnDep.Click += BtnDep_Click;

            choiseBet = new NumericUpDown();
            choiseBet.Value = 10;
            choiseBet.Minimum = 10;
            choiseBet.Anchor = AnchorStyles.Top;
            choiseBet.AutoSize = true;
            choiseBet.Increment = 10;
            slotsTable.Controls.Add(choiseBet, 0, 3);

            

        }

        private void BtnDep_Click(object? sender, EventArgs e)
        {
            Balance.Cash -= (long)choiseBet.Value;
            timerSlots.Start();
            countSpin = 0;

        }

        private void TimerSlots_Tick(object? sender, EventArgs e)
        {
            
            countSpin++;
            string result = "";
            long winCash = 0;
            Random random = new Random();
            for (int i = 0; i < 3; i++)
            {
                int randInt = random.Next(0, 7);
                payLinesLabels[i].Text = payLines[randInt];
                result += payLines[randInt];
            }
            if (countSpin >= 30)
            {
                if (result[0] == result[1] && result[1] == result[2])
                {
                    //jackpot
                    winCash= (long)(choiseBet.Value * 10);
                }
                else if (result[0] == result[1] || result[0] == result[2] || result[1] == result[2])
                {
                    //min win
                    winCash = (long)(choiseBet.Value * 2);
                    
                }

                timerSlots.Stop();
                Balance.Cash += winCash;
                if (winCash > 0) MessageBox.Show($"Вы выиграли {winCash}\n{result}");
                return;
            }
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
