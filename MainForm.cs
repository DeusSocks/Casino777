using System.Windows.Forms;

namespace Casino777
{
    public enum Bombs { Cash, Diamond, Bomb }
    public partial class MainForm : Form
    {
        Label lblBalance;
        Label lblMSstat;

        NumericUpDown choiseBet;
        NumericUpDown MSchoiseBet;
        string[] payLines = { "🍒", "🍋", "🍊", "🍇", "🍉", "💎", "🌟", "7" };

        TableLayoutPanel table;     //MainForm 
        TableLayoutPanel? slotsTable = null;
        TableLayoutPanel slotsWindow;
        TableLayoutPanel? MSTable = null;
        TableLayoutPanel MSField;

        List<Label> payLinesLabels;
        List<List<Button>> MSbuttons;

        List<List<Bombs>> _MSField;

        Button btnToMenu;
        Button btnMSstart;
        Button btnMSstop;
        int countSpin;
        long MSwinCash;

        public MainForm()
        {
            InitializeComponent();
            lblBalance = new Label();
            Balance.OnCashChanged += balanceChanged;
            timerSlots.Tick += TimerSlots_Tick;

            btnToMenu = new Button();
            btnToMenu.Text = "В меню";
            btnToMenu.AutoSize = true;
            btnToMenu.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            btnToMenu.Click += BtnToMenu_Click;

            lblBalance.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBalance.AutoSize = true;
            lblBalance.TextAlign = ContentAlignment.TopRight;
            lblBalance.Text = $"Баланс: {Balance.Cash}";
        }



    }

}
