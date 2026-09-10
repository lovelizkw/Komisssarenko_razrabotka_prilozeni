using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            cmbCar.Items.AddRange(new string[]
            {
                "Порш 911",
                "Toyota Supra",
                "Audi RS8"
            });

            cmbTrack.Items.AddRange(new string[]
            {
                "Сочи",
                "Казань",
                "Москва"
            });

            cmbCar.SelectedIndex = 0;
            cmbTrack.SelectedIndex = 0;

            txtDriver.Focus();

            ShowStatus("Готово к вводу", Color.Black);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string driver = txtDriver.Text.Trim();

            if (driver.Length == 0)
            {
                ShowError("Введите имя пилота");
                txtDriver.Focus();
                return;
            }

            if (cmbCar.SelectedIndex < 0)
            {
                ShowError("Выберите автомобиль");
                return;
            }

            if (cmbTrack.SelectedIndex < 0)
            {
                ShowError("Выберите трассу");
                return;
            }

            if (!int.TryParse(txtTime.Text.Trim(), out int time))
            {
                ShowError("Введите время (целое число)");
                txtTime.Focus();
                return;
            }

            if (time < 30 || time > 600)
            {
                ShowError("Время должно быть от 30 до 600 секунд");
                txtTime.Focus();
                return;
            }

            string result = $"{driver} | {cmbCar.SelectedItem} | {cmbTrack.SelectedItem} | {time} с";
            lstResults.Items.Add(result);

            ShowStatus($"Запись добавлена. Всего записей: {lstResults.Items.Count}", Color.DarkGreen);

            ClearInput();
            txtDriver.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstResults.SelectedIndex < 0)
            {
                ShowError("Выберите строку для удаления");
                return;
            }

            lstResults.Items.RemoveAt(lstResults.SelectedIndex);
            ShowStatus($"Запись удалена. Осталось записей: {lstResults.Items.Count}", Color.DarkBlue);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
            ShowStatus("Поля очищены", Color.Black);
            txtDriver.Focus();
        }

        private void ClearInput()
        {
            txtDriver.Clear();
            txtTime.Clear();
            cmbCar.SelectedIndex = 0;
            cmbTrack.SelectedIndex = 0;
        }

        private void ShowError(string message)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = Color.Red;
        }

        private void ShowStatus(string message, Color color)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;
        }

        private void BtnAddCar_Click(object sender, EventArgs e)
        {
            string newCar = txtNewCar.Text.Trim();

            if (newCar.Length == 0)
            {
                ShowError("Введите название автомобиля");
                txtNewCar.Focus();
                return;
            }

            if ((cmbCar.Items.Contains(newCar)))
            {
                ShowError("Такой автомобиль существует");
                return;
            }

            cmbCar.Items.Add(newCar);
            cmbCar.SelectedItem = newCar;
            txtNewCar.Clear();
            ShowStatus($"Автомобиль {newCar} добавлен", Color.DarkGreen);

        }

        private void BtnAddTrack_Click(object sender, EventArgs e)
        {
            string newTrack = txtNewTrack.Text.Trim();

            if (newTrack.Length == 0)
            {
                ShowError("Введите название трассы");
                txtNewTrack.Focus();
                return;
            }

            if ((cmbTrack.Items.Contains(newTrack)))
            {
                ShowError("Такая трасса существует");
                return;
            }

            cmbTrack.Items.Add(newTrack);
            cmbTrack.SelectedItem = newTrack;
            txtNewTrack.Clear();
            ShowStatus($"Трасса {newTrack} добавлена", Color.DarkGreen);
        }
    }
}