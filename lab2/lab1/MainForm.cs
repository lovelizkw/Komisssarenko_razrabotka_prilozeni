using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace lab1
{
    public partial class MainForm : Form
    {
        private List<RaceResult> allResults = new List<RaceResult>();
        private RaceRepository repository;

        public MainForm()
        {
            InitializeComponent();

            gridResults.AutoGenerateColumns = false;
            gridResults.DataSource = source;

            cmbCar.Items.Clear();
            cmbCar.Items.AddRange(RaceRules.Cars);

            cmbTrack.Items.Clear();
            cmbTrack.Items.AddRange(RaceRules.Tracks);

            if (cmbCar.Items.Count > 0) cmbCar.SelectedIndex = 0;
            if (cmbTrack.Items.Count > 0) cmbTrack.SelectedIndex = 0;
            
            cmbTrackFilter.Items.Clear();
            cmbTrackFilter.Items.Add("Все трассы");
            cmbTrackFilter.Items.AddRange(RaceRules.Tracks);
            if (cmbTrackFilter.Items.Count > 0) cmbTrackFilter.SelectedIndex = 0;
            
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "RaceLogLab2");
            Directory.CreateDirectory(folder);

            string dbPath = Path.Combine(folder, "races.db");
            lblStorage.Text = "База: " + dbPath;

            repository = new RaceRepository(dbPath);
            repository.Initialize();

            gridResults.SelectionChanged += gridResults_SelectionChanged;

            Reload();

            txtDriver.Focus();
            ShowStatus("Готово к вводу", Color.Black);
        }

        private void Reload()
        {
            try
            {
                allResults = repository.GetAll();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                ShowError("Ошибка чтения базы: " + ex.Message);
            }
        }

        private void ApplyFilter()
        {
            var filtered = RaceQuery.Apply(allResults, txtSearch.Text, cmbTrackFilter.Text);
            source.DataSource = filtered;
            gridResults.ClearSelection();

            lblStatus.Text = $"Показано: {source.Count}. Всего: {allResults.Count}";
            lblStatus.ForeColor = Color.Black;
        }

        private RaceResult ReadInput(long id)
        {
            var row = new RaceResult
            {
                Id = id,
                Driver = txtDriver.Text.Trim(),
                Car = cmbCar.SelectedItem?.ToString() ?? "",
                Track = cmbTrack.SelectedItem?.ToString() ?? "",
                LapSeconds = 0
            };

            if (!int.TryParse(txtSeconds.Text.Trim(), out int seconds))
                throw new ArgumentException("Введите время (целое число)");

            row.LapSeconds = seconds;
            RaceRules.Validate(row);
            return row;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var row = ReadInput(0);
                long newId = repository.Add(row);
                Reload();
                ClearInput();
                ShowStatus("Запись добавлена, Id = " + newId, Color.DarkGreen);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gridResults.SelectedRows.Count == 0)
            {
                ShowError("Выберите строку для правки");
                return;
            }

            var selected = gridResults.SelectedRows[0].DataBoundItem as RaceResult;
            if (selected == null) return;

            try
            {
                var updated = ReadInput(selected.Id);
                repository.Update(updated);
                Reload();
                ClearInput();
                ShowStatus("Запись обновлена", Color.DarkGreen);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridResults.SelectedRows.Count == 0)
            {
                ShowError("Выберите строку для удаления");
                return;
            }

            var selected = gridResults.SelectedRows[0].DataBoundItem as RaceResult;
            if (selected == null) return;

            var answer = MessageBox.Show(
                $"Удалить Id {selected.Id}, пилот {selected.Driver}?",
                "Подтверждение", MessageBoxButtons.YesNo);

            if (answer != DialogResult.Yes) return;

            try
            {
                repository.Delete(selected.Id);
                Reload();
                ClearInput();
                ShowStatus("Запись удалена", Color.DarkBlue);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
            ShowStatus("Поля очищены", Color.Black);
            txtDriver.Focus();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (cmbTrackFilter.Items.Count > 0)
                cmbTrackFilter.SelectedIndex = 0;
            ApplyFilter();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void gridResults_SelectionChanged(object sender, EventArgs e)
        {
            if (gridResults.SelectedRows.Count == 0) return;

            var selected = gridResults.SelectedRows[0].DataBoundItem as RaceResult;
            if (selected == null) return;

            txtDriver.Text = selected.Driver;
            txtSeconds.Text = selected.LapSeconds.ToString();
            cmbCar.Text = selected.Car;
            cmbTrack.Text = selected.Track;
        }

        private void ClearInput()
        {
            txtDriver.Clear();
            txtSeconds.Clear();
            if (cmbCar.Items.Count > 0) cmbCar.SelectedIndex = 0;
            if (cmbTrack.Items.Count > 0) cmbTrack.SelectedIndex = 0;
            gridResults.ClearSelection();
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
            if (newCar.Length == 0) { ShowError("Введите название автомобиля"); txtNewCar.Focus(); return; }
            if (cmbCar.Items.Contains(newCar)) { ShowError("Такой автомобиль существует"); return; }

            cmbCar.Items.Add(newCar);
            cmbCar.SelectedItem = newCar;
            txtNewCar.Clear();
            ShowStatus($"Автомобиль {newCar} добавлен", Color.DarkGreen);
        }

        private void BtnAddTrack_Click(object sender, EventArgs e)
        {
            string newTrack = txtNewTrack.Text.Trim();
            if (newTrack.Length == 0) { ShowError("Введите название трассы"); txtNewTrack.Focus(); return; }
            if (cmbTrack.Items.Contains(newTrack)) { ShowError("Такая трасса существует"); return; }

            cmbTrack.Items.Add(newTrack);
            cmbTrack.SelectedItem = newTrack;
            txtNewTrack.Clear();
            ShowStatus($"Трасса {newTrack} добавлена", Color.DarkGreen);
        }

        private void btnSaveJson_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog();
            dialog.Filter = "JSON файл (*.json)|*.json";
            dialog.Title = "Сохранить журнал в JSON";
            dialog.FileName = "races.json";

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var all = repository.GetAll();  
                RaceTransfer.SaveJson(dialog.FileName, all);
                ShowStatus("Сохранено в JSON: " + dialog.FileName, Color.DarkGreen);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка сохранения JSON: " + ex.Message);
            }
        }

        private void btnSaveXml_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog();
            dialog.Filter = "XML файл (*.xml)|*.xml";
            dialog.Title = "Сохранить журнал в XML";
            dialog.FileName = "races.xml";

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var all = repository.GetAll();
                RaceTransfer.SaveXml(dialog.FileName, all);
                ShowStatus("Сохранено в XML: " + dialog.FileName, Color.DarkGreen);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка сохранения XML: " + ex.Message);
            }
        }

        private void btnLoadJson_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "JSON файл (*.json)|*.json";
            dialog.Title = "Загрузить журнал из JSON";

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var rows = RaceTransfer.LoadJson(dialog.FileName);
                RaceRules.ValidateImport(rows);

                var answer = MessageBox.Show(
                    $"Заменить весь журнал?\nСейчас записей: {allResults.Count}\nБудет записей: {rows.Count}",
                    "Подтверждение замены",
                    MessageBoxButtons.YesNo);

                if (answer != DialogResult.Yes) return;

                repository.ReplaceAll(rows);
                txtSearch.Text = "";
                if (cmbTrackFilter.Items.Count > 0)
                    cmbTrackFilter.SelectedIndex = 0;
                Reload();
                ShowStatus($"Журнал восстановлен из JSON ({rows.Count} записей)", Color.DarkGreen);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка загрузки JSON: " + ex.Message);
            }
        }

        private void btnLoadXml_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "XML файл (*.xml)|*.xml";
            dialog.Title = "Загрузить журнал из XML";

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var rows = RaceTransfer.LoadXml(dialog.FileName);
                RaceRules.ValidateImport(rows);

                var answer = MessageBox.Show(
                    $"Заменить весь журнал?\nСейчас записей: {allResults.Count}\nБудет записей: {rows.Count}",
                    "Подтверждение замены",
                    MessageBoxButtons.YesNo);

                if (answer != DialogResult.Yes) return;

                repository.ReplaceAll(rows);
                txtSearch.Text = "";
                if (cmbTrackFilter.Items.Count > 0)
                    cmbTrackFilter.SelectedIndex = 0;
                Reload();
                ShowStatus($"Журнал восстановлен из XML ({rows.Count} записей)", Color.DarkGreen);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка загрузки XML: " + ex.Message);
            }
        }
    }
}