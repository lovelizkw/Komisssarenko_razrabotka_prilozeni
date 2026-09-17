namespace lab1
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtDriver = new TextBox();
            txtSeconds = new TextBox();
            cmbCar = new ComboBox();
            cmbTrack = new ComboBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            lblStatus = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtNewCar = new TextBox();
            txtNewTrack = new TextBox();
            BtnAddCar = new Button();
            BtnAddTrack = new Button();
            gridResults = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colDriver = new DataGridViewTextBoxColumn();
            colCar = new DataGridViewTextBoxColumn();
            colTrack = new DataGridViewTextBoxColumn();
            colLapSeconds = new DataGridViewTextBoxColumn();
            source = new BindingSource(components);
            txtSearch = new TextBox();
            cmbTrackFilter = new ComboBox();
            btnResetFilter = new Button();
            btnUpdate = new Button();
            lblStorage = new Label();
            btnFilter = new Button();
            btnSaveJson = new Button();
            btnSaveXml = new Button();
            btnLoadJson = new Button();
            btnLoadXml = new Button();
            ((System.ComponentModel.ISupportInitialize)gridResults).BeginInit();
            ((System.ComponentModel.ISupportInitialize)source).BeginInit();
            SuspendLayout();
            // 
            // txtDriver
            // 
            txtDriver.Location = new Point(148, 6);
            txtDriver.Name = "txtDriver";
            txtDriver.Size = new Size(125, 27);
            txtDriver.TabIndex = 0;
            // 
            // txtSeconds
            // 
            txtSeconds.Location = new Point(148, 39);
            txtSeconds.Name = "txtSeconds";
            txtSeconds.Size = new Size(125, 27);
            txtSeconds.TabIndex = 1;
            // 
            // cmbCar
            // 
            cmbCar.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCar.FormattingEnabled = true;
            cmbCar.Location = new Point(148, 75);
            cmbCar.Name = "cmbCar";
            cmbCar.Size = new Size(151, 28);
            cmbCar.TabIndex = 2;
            // 
            // cmbTrack
            // 
            cmbTrack.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTrack.FormattingEnabled = true;
            cmbTrack.Location = new Point(148, 109);
            cmbTrack.Name = "cmbTrack";
            cmbTrack.Size = new Size(151, 28);
            cmbTrack.TabIndex = 3;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(32, 145);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(305, 145);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(405, 145);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(154, 29);
            btnClear.TabIndex = 6;
            btnClear.Text = "Очистить поля";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 553);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(112, 20);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Готово к вводу";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 9);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 9;
            label1.Text = "Пилот:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 45);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 10;
            label2.Text = "Время, с :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 78);
            label3.Name = "label3";
            label3.Size = new Size(99, 20);
            label3.TabIndex = 11;
            label3.Text = "Автомобиль:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(32, 110);
            label4.Name = "label4";
            label4.Size = new Size(59, 20);
            label4.TabIndex = 12;
            label4.Text = "Трасса:";
            // 
            // txtNewCar
            // 
            txtNewCar.Location = new Point(305, 75);
            txtNewCar.Name = "txtNewCar";
            txtNewCar.Size = new Size(125, 27);
            txtNewCar.TabIndex = 13;
            // 
            // txtNewTrack
            // 
            txtNewTrack.Location = new Point(305, 110);
            txtNewTrack.Name = "txtNewTrack";
            txtNewTrack.Size = new Size(125, 27);
            txtNewTrack.TabIndex = 14;
            // 
            // BtnAddCar
            // 
            BtnAddCar.Location = new Point(436, 74);
            BtnAddCar.Name = "BtnAddCar";
            BtnAddCar.Size = new Size(94, 29);
            BtnAddCar.TabIndex = 15;
            BtnAddCar.Text = "AddCar";
            BtnAddCar.UseVisualStyleBackColor = true;
            BtnAddCar.Click += BtnAddCar_Click;
            // 
            // BtnAddTrack
            // 
            BtnAddTrack.Location = new Point(436, 110);
            BtnAddTrack.Name = "BtnAddTrack";
            BtnAddTrack.Size = new Size(94, 29);
            BtnAddTrack.TabIndex = 16;
            BtnAddTrack.Text = "AddTrack";
            BtnAddTrack.UseVisualStyleBackColor = true;
            BtnAddTrack.Click += BtnAddTrack_Click;
            // 
            // gridResults
            // 
            gridResults.AllowUserToAddRows = false;
            gridResults.Anchor = AnchorStyles.None;
            gridResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridResults.Columns.AddRange(new DataGridViewColumn[] { colID, colDriver, colCar, colTrack, colLapSeconds });
            gridResults.Location = new Point(12, 274);
            gridResults.MultiSelect = false;
            gridResults.Name = "gridResults";
            gridResults.ReadOnly = true;
            gridResults.RowHeadersWidth = 51;
            gridResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridResults.Size = new Size(676, 276);
            gridResults.TabIndex = 17;
            // 
            // colID
            // 
            colID.DataPropertyName = "Id";
            colID.HeaderText = "Id";
            colID.MinimumWidth = 6;
            colID.Name = "colID";
            colID.ReadOnly = true;
            colID.SortMode = DataGridViewColumnSortMode.NotSortable;
            colID.Width = 125;
            // 
            // colDriver
            // 
            colDriver.DataPropertyName = "Driver";
            colDriver.HeaderText = "Пилот";
            colDriver.MinimumWidth = 6;
            colDriver.Name = "colDriver";
            colDriver.ReadOnly = true;
            colDriver.SortMode = DataGridViewColumnSortMode.NotSortable;
            colDriver.Width = 125;
            // 
            // colCar
            // 
            colCar.DataPropertyName = "Car";
            colCar.HeaderText = "Автомобиль";
            colCar.MinimumWidth = 6;
            colCar.Name = "colCar";
            colCar.ReadOnly = true;
            colCar.SortMode = DataGridViewColumnSortMode.NotSortable;
            colCar.Width = 125;
            // 
            // colTrack
            // 
            colTrack.DataPropertyName = "Track";
            colTrack.HeaderText = "Трасса";
            colTrack.MinimumWidth = 6;
            colTrack.Name = "colTrack";
            colTrack.ReadOnly = true;
            colTrack.SortMode = DataGridViewColumnSortMode.NotSortable;
            colTrack.Width = 125;
            // 
            // colLapSeconds
            // 
            colLapSeconds.DataPropertyName = "LapSeconds";
            colLapSeconds.HeaderText = "Время, с";
            colLapSeconds.MinimumWidth = 6;
            colLapSeconds.Name = "colLapSeconds";
            colLapSeconds.ReadOnly = true;
            colLapSeconds.SortMode = DataGridViewColumnSortMode.NotSortable;
            colLapSeconds.Width = 125;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(32, 241);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(99, 27);
            txtSearch.TabIndex = 18;
            // 
            // cmbTrackFilter
            // 
            cmbTrackFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTrackFilter.FormattingEnabled = true;
            cmbTrackFilter.Items.AddRange(new object[] { "Все трассы", "Сочи", "Казань", "Москва" });
            cmbTrackFilter.Location = new Point(148, 240);
            cmbTrackFilter.Name = "cmbTrackFilter";
            cmbTrackFilter.Size = new Size(151, 28);
            cmbTrackFilter.TabIndex = 19;
            // 
            // btnResetFilter
            // 
            btnResetFilter.Location = new Point(405, 239);
            btnResetFilter.Name = "btnResetFilter";
            btnResetFilter.Size = new Size(115, 29);
            btnResetFilter.TabIndex = 20;
            btnResetFilter.Text = "Сброс поиска";
            btnResetFilter.UseVisualStyleBackColor = true;
            btnResetFilter.Click += btnResetFilter_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(148, 145);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(145, 29);
            btnUpdate.TabIndex = 21;
            btnUpdate.Text = "Сохранить правку";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // lblStorage
            // 
            lblStorage.AutoSize = true;
            lblStorage.Location = new Point(493, 245);
            lblStorage.Name = "lblStorage";
            lblStorage.Size = new Size(0, 20);
            lblStorage.TabIndex = 22;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(305, 239);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(94, 29);
            btnFilter.TabIndex = 23;
            btnFilter.Text = "Найти";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // btnSaveJson
            // 
            btnSaveJson.Location = new Point(148, 180);
            btnSaveJson.Name = "btnSaveJson";
            btnSaveJson.Size = new Size(62, 29);
            btnSaveJson.TabIndex = 24;
            btnSaveJson.Text = "в Json";
            btnSaveJson.UseVisualStyleBackColor = true;
            btnSaveJson.Click += btnSaveJson_Click;
            // 
            // btnSaveXml
            // 
            btnSaveXml.Location = new Point(229, 180);
            btnSaveXml.Name = "btnSaveXml";
            btnSaveXml.Size = new Size(64, 29);
            btnSaveXml.TabIndex = 25;
            btnSaveXml.Text = "в XML";
            btnSaveXml.UseVisualStyleBackColor = true;
            btnSaveXml.Click += btnSaveXml_Click;
            // 
            // btnLoadJson
            // 
            btnLoadJson.Location = new Point(32, 180);
            btnLoadJson.Name = "btnLoadJson";
            btnLoadJson.Size = new Size(73, 29);
            btnLoadJson.TabIndex = 26;
            btnLoadJson.Text = "из Json";
            btnLoadJson.UseVisualStyleBackColor = true;
            btnLoadJson.Click += btnLoadJson_Click;
            // 
            // btnLoadXml
            // 
            btnLoadXml.Location = new Point(32, 206);
            btnLoadXml.Name = "btnLoadXml";
            btnLoadXml.Size = new Size(73, 29);
            btnLoadXml.TabIndex = 27;
            btnLoadXml.Text = "из XML";
            btnLoadXml.UseVisualStyleBackColor = true;
            btnLoadXml.Click += btnLoadXml_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            ClientSize = new Size(700, 582);
            Controls.Add(btnLoadXml);
            Controls.Add(btnLoadJson);
            Controls.Add(btnSaveXml);
            Controls.Add(btnSaveJson);
            Controls.Add(btnFilter);
            Controls.Add(lblStorage);
            Controls.Add(btnUpdate);
            Controls.Add(btnResetFilter);
            Controls.Add(cmbTrackFilter);
            Controls.Add(txtSearch);
            Controls.Add(gridResults);
            Controls.Add(BtnAddTrack);
            Controls.Add(BtnAddCar);
            Controls.Add(txtNewTrack);
            Controls.Add(txtNewCar);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblStatus);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(cmbTrack);
            Controls.Add(cmbCar);
            Controls.Add(txtSeconds);
            Controls.Add(txtDriver);
            Name = "MainForm";
            Text = "Журнал заездов";
            Click += btnUpdate_Click;
            ((System.ComponentModel.ISupportInitialize)gridResults).EndInit();
            ((System.ComponentModel.ISupportInitialize)source).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDriver;
        private TextBox txtSeconds;
        private ComboBox cmbCar;
        private ComboBox cmbTrack;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnClear;
        private Label lblStatus;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtNewCar;
        private TextBox txtNewTrack;
        private Button BtnAddCar;
        private Button BtnAddTrack;
        private DataGridView gridResults;
        private BindingSource source;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colDriver;
        private DataGridViewTextBoxColumn colCar;
        private DataGridViewTextBoxColumn colTrack;
        private DataGridViewTextBoxColumn colLapSeconds;
        private TextBox txtSearch;
        private ComboBox cmbTrackFilter;
        private Button btnResetFilter;
        private Button btnUpdate;
        private Label lblStorage;
        private Button btnFilter;
        private Button btnSaveJson;
        private Button btnSaveXml;
        private Button btnLoadJson;
        private Button btnLoadXml;
    }
}
