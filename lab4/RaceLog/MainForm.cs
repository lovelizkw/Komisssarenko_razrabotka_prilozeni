using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using RaceData;

namespace RaceLog
{
    public partial class MainForm : Form
    {
        private readonly RaceTransfer transfer = new RaceTransfer();
        private List<RaceResult> allRows = new List<RaceResult>();
        private CancellationTokenSource? active;
        private int pingCount;

        public MainForm()
        {
            InitializeComponent();

            gridResults.AutoGenerateColumns = false;
            gridResults.DataSource = source;

            cmbTrack.Items.Clear();
            cmbTrack.Items.Add("Все трассы");
            cmbTrack.Items.AddRange(RaceRules.Tracks);
            if (cmbTrack.Items.Count > 0) cmbTrack.SelectedIndex = 0;

            bar.Minimum = 0;
            bar.Maximum = 100;

            btnCancel.Enabled = false;
            lblPing.Text = "Нажатий: 0";
            lblStatus.Text = "Выберите источник и нажмите Загрузить";
            lblTime.Text = "Время: -";
        }

        private CancellationTokenSource BeginOperation()
        {
            active = new CancellationTokenSource();

            txtSource.Enabled = false;
            btnChoose.Enabled = false;
            btnLoad.Enabled = false;
            btnExport.Enabled = false;
            chkSlow.Enabled = false;
            btnCancel.Enabled = true;

            bar.Value = 0;
            bar.Style = ProgressBarStyle.Continuous;

            return active;
        }

        private void EndOperation(bool success)
        {
            active = null;

            btnCancel.Enabled = false;
            txtSource.Enabled = true;
            btnChoose.Enabled = true;
            btnLoad.Enabled = true;
            btnExport.Enabled = true;
            chkSlow.Enabled = true;

            bar.Style = ProgressBarStyle.Continuous;
            bar.Value = success ? 100 : 0;
        }

        private void UpdateProgress(WorkProgress p, CancellationTokenSource run)
        {
            if (active != run || run.IsCancellationRequested)
                return;

            lblStatus.Text = p.Total > 0
                ? $"{p.Stage}: {p.Done} из {p.Total}"
                : $"{p.Stage}...";

            bar.Style = p.Total == 0
                ? ProgressBarStyle.Marquee
                : ProgressBarStyle.Continuous;

            if (p.Total > 0)
                bar.Value = (int)p.Percent;
        }

        private void ApplyFilter()
        {
            var filtered = RaceQuery.Apply(allRows, cmbTrack.Text);
            source.DataSource = filtered;
            gridResults.ClearSelection();
            lblCount.Text = $"Показано: {filtered.Count}. Всего: {allRows.Count}";
        }

        private async void btnLoad_Click(object? sender, EventArgs e)
        {
            if (active != null)
                return;

            string path = txtSource.Text.Trim();
            if (path.Length == 0)
            {
                lblStatus.Text = "Выберите источник";
                return;
            }

            bool slow = chkSlow.Checked;
            var run = BeginOperation();
            var progress = new Progress<WorkProgress>(p => UpdateProgress(p, run));
            var watch = Stopwatch.StartNew();

            try
            {
                var loaded = await transfer.LoadAsync(path, slow, progress, run.Token);
                run.Token.ThrowIfCancellationRequested();
                allRows = loaded;
                ApplyFilter();
                lblStatus.Text = $"Загружено записей: {loaded.Count}";
                EndOperation(true);
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "Загрузка отменена";
                EndOperation(false);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка загрузки: " + ex.Message;
                EndOperation(false);
            }
            finally
            {
                watch.Stop();
                lblTime.Text = "Время: " + watch.Elapsed.ToString(@"mm\:ss\.ff");
            }
        }

        private async void btnExport_Click(object? sender, EventArgs e)
        {
            if (active != null)
                return;

            string path = txtSource.Text.Trim();
            if (path.Length == 0)
            {
                lblStatus.Text = "Выберите источник";
                return;
            }

            string target;
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "JSON файл (*.json)|*.json";
                dialog.Title = "Экспорт копии журнала";
                dialog.FileName = "Заезды.json";
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                target = dialog.FileName;
            }

            bool slow = chkSlow.Checked;
            var run = BeginOperation();
            var progress = new Progress<WorkProgress>(p => UpdateProgress(p, run));
            var watch = Stopwatch.StartNew();

            try
            {
                long count = await transfer.ExportAsync(path, target, slow, progress, run.Token);
                lblStatus.Text = $"Скопировано записей: {count}";
                EndOperation(true);
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "Экспорт отменён";
                EndOperation(false);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка экспорта: " + ex.Message;
                EndOperation(false);
            }
            finally
            {
                watch.Stop();
                lblTime.Text = "Время: " + watch.Elapsed.ToString(@"mm\:ss\.ff");
            }
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            active?.Cancel();
            btnCancel.Enabled = false;
            lblStatus.Text = "Ожидаем завершения отмены...";
        }

        private void btnChoose_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "База SQLite (*.db)|*.db|Все файлы (*.*)|*.*";
            dialog.Title = "Выбор источника";
            if (dialog.ShowDialog() == DialogResult.OK)
                txtSource.Text = dialog.FileName;
        }

        private void btnShow_Click(object? sender, EventArgs e)
        {
            if (active != null)
                return;
            ApplyFilter();
        }

        private void btnPing_Click(object? sender, EventArgs e)
        {
            pingCount++;
            lblPing.Text = "Нажатий: " + pingCount;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (active is null)
                return;

            e.Cancel = true;
            active.Cancel();
            btnCancel.Enabled = false;
            lblStatus.Text = "Дождитесь отмены и закройте окно ещё раз";
        }
    }
}
