using System.Drawing;
using System.Windows.Forms;

namespace Дипломочка
{
    public partial class Form1
    {
        private void ApplyStyles()
        {
            // Main background colors
            BackColor = Color.FromArgb(243, 246, 250);
            panelMenu.BackColor = Color.FromArgb(27, 39, 56);
            panelMain.BackColor = Color.FromArgb(243, 246, 250);

            // Style sidebar buttons
            foreach (Control control in panelMenu.Controls)
            {
                if (control is Button button)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;

                    button.BackColor = Color.FromArgb(27, 39, 56);
                    button.ForeColor = Color.White;

                    button.FlatAppearance.MouseOverBackColor =
                        Color.FromArgb(47, 65, 88);

                    button.FlatAppearance.MouseDownBackColor =
                        Color.FromArgb(59, 130, 246);

                    button.Font = new Font(
                        "Segoe UI", 10F, FontStyle.Regular);

                    button.TextAlign = ContentAlignment.MiddleLeft;
                    button.Padding = new Padding(12, 0, 0, 0);
                    button.UseVisualStyleBackColor = false;
                }
            }

            // Style the logo
            lblLogo.Font = new Font(
                "Segoe UI", 18F, FontStyle.Bold);

            // Style page titles
            StyleTitle(lblMain);
            StyleTitle(lblProducts);
            StyleTitle(lblIncome);
            StyleTitle(lblExpense);
            StyleTitle(lblHistory);

            // Style dashboard cards
            StyleCard(frameProducts);
            StyleCard(frameCategories);
            StyleCard(frameStock);

            // Style dashboard statistics
            StyleCount(lblProductsCount);
            StyleCount(lblCategoriesCount);
            StyleCount(lblLowStockCount);

            // Style card descriptions
            lblProductsText.ForeColor = Color.FromArgb(107, 114, 128);
            lblCategoriesText.ForeColor = Color.FromArgb(107, 114, 128);
            lblLowStockText.ForeColor = Color.FromArgb(107, 114, 128);

            // Style tables
            StyleGrid(dgvProducts);
            StyleGrid(dgvOperations);
            StyleGrid(dgvHistory);
        }

        private void StyleTitle(Label label)
        {
            label.ForeColor = Color.FromArgb(30, 41, 59);
            label.Font = new Font(
                "Segoe UI", 22F, FontStyle.Bold);
        }

        private void StyleCount(Label label)
        {
            label.ForeColor = Color.FromArgb(37, 99, 235);
            label.Font = new Font(
                "Segoe UI", 22F, FontStyle.Bold);
        }

        private void StyleCard(Panel panel)
        {
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Padding = new Padding(8);
        }

        private void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.GridColor = Color.FromArgb(226, 232, 240);

            grid.EnableHeadersVisualStyles = false;

            grid.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(37, 52, 72);

            grid.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            grid.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);

            grid.ColumnHeadersHeight = 40;
            grid.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            grid.DefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Regular);

            grid.DefaultCellStyle.ForeColor =
                Color.FromArgb(30, 41, 59);

            grid.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(219, 234, 254);

            grid.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(30, 41, 59);

            grid.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 250, 252);

            grid.RowTemplate.Height = 34;
            grid.RowHeadersVisible = false;
            grid.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            grid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            grid.MultiSelect = false;
            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
