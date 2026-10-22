using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Дипломочка;
using Microsoft.Data.Sqlite;
using Дипломочка.Models;
using System.Linq;

namespace Дипломочка
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Добавление БД в проект
            Database database = new Database();
            database.InitializeDatabase();

            //Установка фильтра в исходное положение
            cmbSort.SelectedIndex = 0;

            LoadProducts();

            LoadDashboard();
        }
        //Загрузка данных про операции
        private void LoadDashboard()
        {
            Database database = new Database();

            // Update statistics
            lblProductsCount.Text =
                database.GetProductCount().ToString();

            lblCategoriesCount.Text =
                database.GetCategoryCount().ToString();

            lblLowStockCount.Text =
                database.GetLowStockCount().ToString();

            //Настройка таблицы
            dgvOperations.AutoGenerateColumns = true;
            dgvOperations.ReadOnly = true;
            dgvOperations.AllowUserToAddRows = false;
            dgvOperations.MultiSelect = false;

            dgvOperations.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvOperations.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            //Загрузка последних операций
            dgvOperations.DataSource = null;
            dgvOperations.DataSource =
                database.GetRecentOperations();

            //Имена столбцов
            if (dgvOperations.Columns.Contains("ProductName"))
                dgvOperations.Columns["ProductName"].HeaderText = "Product";

            if (dgvOperations.Columns.Contains("OperationType"))
                dgvOperations.Columns["OperationType"].HeaderText = "Operation";

            if (dgvOperations.Columns.Contains("OperationDate"))
                dgvOperations.Columns["OperationDate"].HeaderText = "Date";

            if (dgvOperations.Columns.Contains("ProductId"))
                dgvOperations.Columns["ProductId"].Visible = false;
        }
        private void LoadIncomeProducts()
        {
            Database database = new Database();

            List<Product> products = database.GetProducts();

            cmbIncomeProduct.DataSource = null;
            cmbIncomeProduct.DisplayMember = nameof(Product.Name);
            cmbIncomeProduct.ValueMember = nameof(Product.Id);
            cmbIncomeProduct.DataSource = products;

            cmbIncomeProduct.Enabled = products.Count > 0;
            btnRegisterIncome.Enabled = products.Count > 0;
        }
        private void LoadExpenseProducts()
        {
            Database database = new Database();

            List<Product> products = database.GetProducts();

            cmbExpenseProduct.DataSource = null;
            cmbExpenseProduct.DisplayMember = nameof(Product.Name);
            cmbExpenseProduct.ValueMember = nameof(Product.Id);
            cmbExpenseProduct.DataSource = products;

            cmbExpenseProduct.Enabled = products.Count > 0;
            btnRegisterExpense.Enabled = products.Count > 0;
        }
        //Список продуктов для таблицы
        private void LoadProducts(string searchText = "")
        {
            Database database = new Database();

            //НАстройка таблицы
            dgvProducts.AutoGenerateColumns = true;
            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvProducts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvProducts.DataSource = null;
            List<Product> products = database.SearchProducts(searchText);
            //Варианты фильтраций
            switch (cmbSort.SelectedIndex)
            {
                case 1:
                    products = products
                        .OrderBy(p => p.Name)
                        .ToList();
                    break;

                case 2:
                    products = products
                        .OrderBy(p => p.Price)
                        .ToList();
                    break;

                case 3:
                    products = products
                        .OrderByDescending(p => p.Price)
                        .ToList();
                    break;

                case 4:
                    products = products
                        .OrderBy(p => p.Quantity)
                        .ToList();
                    break;

                case 5:
                    products = products
                        .OrderByDescending(p => p.Quantity)
                        .ToList();
                    break;
            }
            //Обновление данных
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = products;
        }

        private void LoadHistory()
        {
            Database database = new Database();

            dgvHistory.AutoGenerateColumns = true;
            dgvHistory.ReadOnly = true;
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.MultiSelect = false;

            dgvHistory.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvHistory.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvHistory.DataSource = null;
            dgvHistory.DataSource = database.GetOperations();

            if (dgvHistory.Columns.Contains("Id"))
                dgvHistory.Columns["Id"].HeaderText = "ID";

            if (dgvHistory.Columns.Contains("ProductId"))
                dgvHistory.Columns["ProductId"].HeaderText = "Product ID";

            if (dgvHistory.Columns.Contains("ProductName"))
                dgvHistory.Columns["ProductName"].HeaderText = "Product";

            if (dgvHistory.Columns.Contains("OperationType"))
                dgvHistory.Columns["OperationType"].HeaderText = "Operation";

            if (dgvHistory.Columns.Contains("Quantity"))
                dgvHistory.Columns["Quantity"].HeaderText = "Quantity";

            if (dgvHistory.Columns.Contains("OperationDate"))
                dgvHistory.Columns["OperationDate"].HeaderText = "Date";
        }
        //Преключатели страниц
        private void ShowHomePage()
        {
            panelMain.Visible = true;
            panelProducts.Visible = false;
            panelIncome.Visible = false;
            panelExpense.Visible = false;
            panelHistory.Visible = false;

            LoadDashboard();
        }
        private void ShowProductsPage()
        {
            panelProducts.Visible = true;
            panelMain.Visible = false;
            panelIncome.Visible = false;
            panelExpense.Visible = false;
            panelHistory.Visible = false;
        }
        private void ShowIncomePage()
        {
            panelIncome.Visible = true;
            panelMain.Visible = false;
            panelProducts.Visible = false;
            panelExpense.Visible = false;
            panelHistory.Visible = false;

            LoadIncomeProducts();
        }
        private void ShowExpensePage()
        {
            panelExpense.Visible = true;
            panelMain.Visible = false;
            panelProducts.Visible = false;
            panelIncome.Visible = false;
            panelHistory.Visible = false;

            LoadExpenseProducts();
        }
        private void ShowHistoryPage()
        {
            panelHistory.Visible = true;
            panelMain.Visible = false;
            panelProducts.Visible = false;
            panelIncome.Visible = false;
            panelExpense.Visible = false;

            LoadHistory();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblLogo_Click(object sender, EventArgs e)
        {

        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblProductsCount_Click(object sender, EventArgs e)
        {

        }

        private void lblProductsText_Click(object sender, EventArgs e)
        {

        }

        private void panelProducts_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblLowStockText_Click(object sender, EventArgs e)
        {

        }

        private void dgvOperations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblProductsTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnHome_Click_1(object sender, EventArgs e)
        {
            ShowHomePage();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ShowProductsPage();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            //Уборка ненужных пробелов
            string name = txtProductName.Text.Trim();
            string category = txtCategory.Text.Trim();
            //Проверки ввода
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Enter correct product name!");
                return;
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Enter the category of product!");
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity)
                || quantity <= 0)
            {
                MessageBox.Show("Enter correct quantity!");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price)
                || price <= 0)
            {
                MessageBox.Show("Enter correct price!");
                return;
            }
            //Присваивание данных
            Product product = new Product
            {
                Name = name,
                Category = category,
                Quantity = quantity,
                Price = price
            };
            //Добавление продукта в БД
            try
            {
                Database database = new Database();
                database.AddProduct(product);
                LoadProducts();

                MessageBox.Show("Product added succesfully!");

                txtProductName.Clear();
                txtCategory.Clear();
                txtQuantity.Clear();
                txtPrice.Clear();

                txtProductName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось добавить товар.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelProducts_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            //Выбор продукта из таблицы
            if (dgvProducts.CurrentRow?.DataBoundItem is not Product product)
                return;

            txtProductName.Text = product.Name;
            txtCategory.Text = product.Category;
            txtQuantity.Text = product.Quantity.ToString();
            txtPrice.Text = product.Price.ToString();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            //Проверка выделенного товара
            if (dgvProducts.CurrentRow?.DataBoundItem is not Product product)
            {
                MessageBox.Show("Select a product first.");
                return;
            }

            //Подтверждение удаления
            DialogResult result = MessageBox.Show(
                $"Delete '{product.Name}'?",
                "Confirm deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            try
            {
                //Вызов удаления и очищение
                Database database = new Database();
                database.DeleteProduct(product.Id);

                LoadProducts(txtSearch.Text);

                txtProductName.Clear();
                txtCategory.Clear();
                txtQuantity.Clear();
                txtPrice.Clear();

                MessageBox.Show("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message);
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            //Проверка выделенного товара
            if (dgvProducts.CurrentRow?.DataBoundItem is not Product selectedProduct)
            {
                MessageBox.Show("Select a product first.");
                return;
            }

            //Обрезка пробелов
            string name = txtProductName.Text.Trim();
            string category = txtCategory.Text.Trim();

            //Проверки правильного ввода
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Enter the product name and category.");
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) ||
                quantity <= 0)
            {
                MessageBox.Show("Enter a valid quantity.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) ||
                price <= 0)
            {
                MessageBox.Show("Enter a valid price.");
                return;
            }

            //Присваивание новых данных
            Product updatedProduct = new Product
            {
                Id = selectedProduct.Id,
                Name = name,
                Category = category,
                Quantity = quantity,
                Price = price
            };

            try
            {
                Database database = new Database();
                database.UpdateProduct(updatedProduct);

                LoadProducts(txtSearch.Text);

                MessageBox.Show("Product updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblProductsDGV_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text.Trim());
        }

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text.Trim());
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            ShowIncomePage();
        }

        private void lblIncomeQuantity_Click(object sender, EventArgs e)
        {

        }

        private void btnRegisterIncome_Click(object sender, EventArgs e)
        {
            //Проверка выделения
            if (cmbIncomeProduct.SelectedItem is not Product product)
            {
                MessageBox.Show("Select a product first.");
                return;
            }

            //Перевод в интеджер
            int quantity = Decimal.ToInt32(numIncomeQuantity.Value);

            //Регистрация дохода
            try
            {
                Database database = new Database();

                database.RegisterOperation(
                    product.Id,
                    "Income",
                    quantity
                );

                LoadProducts(txtSearch.Text.Trim());
                LoadIncomeProducts();

                MessageBox.Show("Income registered successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not register income: " + ex.Message
                );
            }
        }

        private void btnExpense_Click(object sender, EventArgs e)
        {
            ShowExpensePage();
        }

        private void btnRegisterExpense_Click(object sender, EventArgs e)
        {
            //Проверка выделения
            if (cmbExpenseProduct.SelectedItem is not Product product)
            {
                MessageBox.Show("Select a product first.");
                return;
            }

            //Перевод в интеджер
            int quantity = Decimal.ToInt32(numExpenseQuantity.Value);

            //Регистрация затрат
            try
            {
                Database database = new Database();

                database.RegisterOperation(
                    product.Id,
                    "Expense",
                    quantity
                );

                LoadProducts(txtSearch.Text.Trim());
                LoadExpenseProducts();

                MessageBox.Show("Expense registered successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not register expense: " + ex.Message
                );
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            ShowHistoryPage();
        }
    }
}
