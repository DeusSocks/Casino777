using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Casino777
{
    public partial class MainForm : Form
    {

        public void MainForm_Load(object sender, EventArgs e)
        {
            string path = "save_file.json";
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                Dictionary<string, long>? config = JsonSerializer.Deserialize<Dictionary<string, long>>(jsonString);
                if (config != null)
                {
                    Balance.Cash = config["Balance"];
                }
            }



            table = new TableLayoutPanel();
            table.Dock = DockStyle.Fill;
            table.ColumnCount = 3;
            table.RowCount = 3;

            //отобразить сетку
            //table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));

            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            Controls.Add(table);

            table.Controls.Add(lblBalance, 0, 0);
            table.SetColumnSpan(lblBalance, 3);


            Label lblCasino = new Label();
            lblCasino.Text = "НеКазино 777";
            lblCasino.ForeColor = Color.Red;
            lblCasino.TextAlign = ContentAlignment.TopCenter;
            lblCasino.Dock = DockStyle.Fill;
            lblCasino.Font = new Font("Impact", 36, FontStyle.Regular);
            table.Controls.Add(lblCasino, 1, 0);
            table.SetColumnSpan(lblCasino, 3);

            Button btnSlots = new Button();
            btnSlots.Text = "Слоты";
            btnSlots.Click += slotsLoad;

            Button btnMS = new Button();
            btnMS.Text = "Сапер";
            btnMS.Click += mineSweeperLoad;

            List<Button> menuButtons = new List<Button>() {
                btnSlots,btnMS
            };
            int i = 0;
            foreach (Button button in menuButtons)
            {
                table.RowCount++;
                table.RowStyles.Add(
                    new RowStyle(SizeType.Absolute, 50));
                table.Controls.Add(button, 1, 3 + i);
                button.AutoSize = true;
                button.Anchor = AnchorStyles.Top;
                i++;
            }
        }

        private void slotsLoad(object sender, EventArgs e)
        {
            table.Hide();
            if (slotsTable != null)
            {
                slotsTable.Show();
                slotsTable.Controls.Add(btnToMenu, 0, 0);
                slotsTable.Controls.Add(lblBalance, 1, 0);
                return;
            }
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



            slotsTable.Controls.Add(lblBalance, 1, 0);
            slotsTable.SetColumnSpan(lblBalance, 2);


            slotsTable.Controls.Add(btnToMenu, 0, 0);

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

            slotsTable.Controls.Add(slotsWindow, 0, 1);
            slotsTable.SetColumnSpan(slotsWindow, 3);
            slotsWindow.CellBorderStyle =
                TableLayoutPanelCellBorderStyle.Single;
            payLinesLabels = new List<Label>();
            for (int i = 0; i < 3; i++)
            {
                payLinesLabels.Add(new Label());
                slotsWindow.Controls.Add(payLinesLabels[i]);
                payLinesLabels[i].Dock = DockStyle.Fill;
                payLinesLabels[i].Anchor = AnchorStyles.None;
                payLinesLabels[i].AutoSize = true;
                payLinesLabels[i].Font = new Font(
                    slotsTable.Font.FontFamily, 50, slotsTable.Font.Style);
            }
            Button btnDep = new Button();
            btnDep.Text = "Депнуть";
            slotsTable.RowCount++;
            slotsTable.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 50));
            slotsTable.Controls.Add(btnDep, 1, 3);
            btnDep.AutoSize = true;
            btnDep.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnDep.Click += BtnDep_Click;

            choiseBet = new NumericUpDown();
            choiseBet.Value = 10;
            choiseBet.Minimum = 10;
            choiseBet.Maximum = 1000000;
            choiseBet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            choiseBet.MinimumSize = new Size(150, 50);
            choiseBet.AutoSize = true;
            choiseBet.Increment = 10;
            slotsTable.Controls.Add(choiseBet, 0, 3);

        }

        private void mineSweeperLoad(object sender, EventArgs e)
        {
            table.Hide();
            if (MSTable != null)
            {
                MSTable.Controls.Add(lblBalance, 1, 0);
                MSTable.Controls.Add(btnToMenu, 0, 0);
                MSTable.Show();
                return;
            }
            MSTable = new TableLayoutPanel();
            MSTable.Dock = DockStyle.Fill;
            MSTable.ColumnCount = 3;
            MSTable.RowCount = 3;

            //MSTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            MSTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            MSTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            MSTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));

            MSTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            MSTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 400));
            MSTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            Controls.Add(MSTable);

            MSTable.Controls.Add(lblBalance, 1, 0);
            MSTable.SetColumnSpan(lblBalance, 2);

            MSTable.Controls.Add(btnToMenu, 0, 0);

            MSField = new TableLayoutPanel();

            MSField.Size = new Size(350, 350);

            MSField.Dock = DockStyle.None;
            MSField.Anchor = AnchorStyles.None;

            MSTable.Controls.Add(MSField, 0, 1);
            MSTable.SetColumnSpan(MSField, 3);

            MSField.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            MSField.ColumnCount = 5;
            MSField.RowCount = 5;

            for (int i = 0; i < MSField.ColumnCount; i++)
            {
                MSField.ColumnStyles.Add(new ColumnStyle(
                    SizeType.Percent, 100f / MSField.ColumnCount));
            }

            for (int i = 0; i < MSField.RowCount; i++)
            {
                MSField.RowStyles.Add(new RowStyle(
                    SizeType.Percent, 100f / MSField.RowCount));
            }
            MSbuttons = new List<List<Button>>();

            for (int row = 0; row < MSField.RowCount; row++)
            {
                MSbuttons.Add(new List<Button>());
                for (int col = 0; col < MSField.ColumnCount; col++)
                {
                    MSbuttons[row].Add(new Button());
                    MSbuttons[row][col].Text = "";
                    MSbuttons[row][col].Dock = DockStyle.Fill;
                    MSbuttons[row][col].FlatStyle = FlatStyle.Flat;
                    MSbuttons[row][col].BackColor = Color.LightGray;
                    MSField.Controls.Add(MSbuttons[row][col], col, row);
                    MSbuttons[row][col].Enabled = false;
                    MSbuttons[row][col].Click += buttonsMS_Click;
                }
            }

            btnMSstart = new Button();
            btnMSstart.Text = "Начать";
            MSTable.RowCount++;
            MSTable.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 50));
            MSTable.Controls.Add(btnMSstart, 1, 3);
            btnMSstart.AutoSize = true;
            btnMSstart.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnMSstart.Click += BtnMSstart_Click;

            MSchoiseBet = new NumericUpDown();
            MSchoiseBet.Value = 10;
            MSchoiseBet.Minimum = 10;
            MSchoiseBet.Maximum = 1000000;
            MSchoiseBet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MSchoiseBet.MinimumSize = new Size(150, 50);
            MSchoiseBet.AutoSize = true;
            MSchoiseBet.Increment = 10;
            MSTable.Controls.Add(MSchoiseBet, 0, 3);

            btnMSstop = new Button();
            btnMSstop.Text = "Забрать";
            MSTable.RowCount++;
            MSTable.Controls.Add(btnMSstop, 2, 3);
            btnMSstop.AutoSize = true;
            btnMSstop.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnMSstop.Click += BtnMSstop_Click;
            btnMSstop.Enabled = false;

            lblMSstat = new Label();
            lblMSstat.Text = "Ваша ставка: 0\nВаш выигрыш: 0";
            lblMSstat.AutoSize = true;
            lblMSstat.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            MSTable.RowCount++;
            MSTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            MSTable.Controls.Add(lblMSstat, 0, 4);
        }
    }
}
