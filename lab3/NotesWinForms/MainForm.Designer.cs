namespace NotesWinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtDraft = new TextBox();
            txtQuery = new TextBox();
            lstNotes = new ListBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnSearch = new Button();
            btnReset = new Button();
            btnSave = new Button();
            btnLoad = new Button();
            lblCount = new Label();
            lblSaveState = new Label();
            lblStatus = new Label();
            lblPath = new Label();
            lblDraft = new Label();
            lblQuery = new Label();
            SuspendLayout();
            lblDraft.AutoSize = true;
            lblDraft.Location = new Point(12, 12);
            lblDraft.Name = "lblDraft";
            lblDraft.Size = new Size(90, 15);
            lblDraft.TabIndex = 0;
            lblDraft.Text = "Новая заметка";
            txtDraft.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDraft.Location = new Point(120, 9);
            txtDraft.Name = "txtDraft";
            txtDraft.Size = new Size(295, 23);
            txtDraft.TabIndex = 1;
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.Enabled = false;
            btnAdd.Location = new Point(421, 7);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 26);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            lblQuery.AutoSize = true;
            lblQuery.Location = new Point(12, 42);
            lblQuery.Name = "lblQuery";
            lblQuery.Size = new Size(50, 15);
            lblQuery.TabIndex = 3;
            lblQuery.Text = "Поиск";
            txtQuery.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtQuery.Location = new Point(120, 39);
            txtQuery.Name = "txtQuery";
            txtQuery.Size = new Size(180, 23);
            txtQuery.TabIndex = 4;
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(306, 37);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 26);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Найти";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            btnReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnReset.Location = new Point(406, 37);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(109, 26);
            btnReset.TabIndex = 6;
            btnReset.Text = "Сбросить поиск";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            lstNotes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstNotes.FormattingEnabled = true;
            lstNotes.ItemHeight = 15;
            lstNotes.Location = new Point(12, 70);
            lstNotes.Name = "lstNotes";
            lstNotes.SelectionMode = SelectionMode.One;
            lstNotes.Size = new Size(503, 214);
            lstNotes.TabIndex = 7;
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Enabled = false;
            btnDelete.Location = new Point(12, 300);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 26);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.Location = new Point(112, 300);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 26);
            btnSave.TabIndex = 9;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            btnLoad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLoad.Location = new Point(212, 300);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(94, 26);
            btnLoad.TabIndex = 10;
            btnLoad.Text = "Загрузить";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            lblCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblCount.AutoSize = true;
            lblCount.Location = new Point(312, 306);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(110, 15);
            lblCount.TabIndex = 11;
            lblCount.Text = "Показано: 0. Всего: 0";
            lblSaveState.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblSaveState.Location = new Point(12, 331);
            lblSaveState.Name = "lblSaveState";
            lblSaveState.Size = new Size(300, 15);
            lblSaveState.TabIndex = 12;
            lblSaveState.Text = "Все изменения сохранены";
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 352);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(50, 15);
            lblStatus.TabIndex = 13;
            lblStatus.Text = "Готово";
            lblPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblPath.AutoSize = true;
            lblPath.ForeColor = Color.Gray;
            lblPath.Location = new Point(12, 373);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(60, 15);
            lblPath.TabIndex = 14;
            lblPath.Text = "Папка данных";
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(527, 398);
            Controls.Add(lblPath);
            Controls.Add(lblStatus);
            Controls.Add(lblSaveState);
            Controls.Add(lblCount);
            Controls.Add(btnLoad);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(lstNotes);
            Controls.Add(btnReset);
            Controls.Add(btnSearch);
            Controls.Add(txtQuery);
            Controls.Add(lblQuery);
            Controls.Add(btnAdd);
            Controls.Add(txtDraft);
            Controls.Add(lblDraft);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Заметки пилота";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtDraft;
        private TextBox txtQuery;
        private ListBox lstNotes;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnSearch;
        private Button btnReset;
        private Button btnSave;
        private Button btnLoad;
        private Label lblCount;
        private Label lblSaveState;
        private Label lblStatus;
        private Label lblPath;
        private Label lblDraft;
        private Label lblQuery;
    }
}
