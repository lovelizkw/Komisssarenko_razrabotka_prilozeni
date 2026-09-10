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
            txtDriver = new TextBox();
            txtTime = new TextBox();
            cmbCar = new ComboBox();
            cmbTrack = new ComboBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            lstResults = new ListBox();
            lblStatus = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtNewCar = new TextBox();
            txtNewTrack = new TextBox();
            BtnAddCar = new Button();
            BtnAddTrack = new Button();
            SuspendLayout();
            // 
            // txtDriver
            // 
            txtDriver.Location = new Point(154, 45);
            txtDriver.Name = "txtDriver";
            txtDriver.Size = new Size(125, 27);
            txtDriver.TabIndex = 0;
            // 
            // txtTime
            // 
            txtTime.Location = new Point(154, 78);
            txtTime.Name = "txtTime";
            txtTime.Size = new Size(125, 27);
            txtTime.TabIndex = 1;
            // 
            // cmbCar
            // 
            cmbCar.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCar.FormattingEnabled = true;
            cmbCar.Location = new Point(154, 113);
            cmbCar.Name = "cmbCar";
            cmbCar.Size = new Size(151, 28);
            cmbCar.TabIndex = 2;
            // 
            // cmbTrack
            // 
            cmbTrack.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTrack.FormattingEnabled = true;
            cmbTrack.Location = new Point(154, 147);
            cmbTrack.Name = "cmbTrack";
            cmbTrack.Size = new Size(151, 28);
            cmbTrack.TabIndex = 3;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(32, 202);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(154, 202);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(285, 202);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(154, 29);
            btnClear.TabIndex = 6;
            btnClear.Text = "Очистить поля";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // lstResults
            // 
            lstResults.BackColor = Color.Linen;
            lstResults.FormattingEnabled = true;
            lstResults.HorizontalScrollbar = true;
            lstResults.Location = new Point(683, 52);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(307, 344);
            lstResults.TabIndex = 7;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(32, 255);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(112, 20);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Готово к вводу";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 52);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 9;
            label1.Text = "Пилот:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 85);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 10;
            label2.Text = "Время, с :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 121);
            label3.Name = "label3";
            label3.Size = new Size(99, 20);
            label3.TabIndex = 11;
            label3.Text = "Автомобиль:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(32, 155);
            label4.Name = "label4";
            label4.Size = new Size(59, 20);
            label4.TabIndex = 12;
            label4.Text = "Трасса:";
            // 
            // txtNewCar
            // 
            txtNewCar.Location = new Point(311, 113);
            txtNewCar.Name = "txtNewCar";
            txtNewCar.Size = new Size(125, 27);
            txtNewCar.TabIndex = 13;
            // 
            // txtNewTrack
            // 
            txtNewTrack.Location = new Point(311, 148);
            txtNewTrack.Name = "txtNewTrack";
            txtNewTrack.Size = new Size(125, 27);
            txtNewTrack.TabIndex = 14;
            // 
            // BtnAddCar
            // 
            BtnAddCar.Location = new Point(442, 112);
            BtnAddCar.Name = "BtnAddCar";
            BtnAddCar.Size = new Size(94, 29);
            BtnAddCar.TabIndex = 15;
            BtnAddCar.Text = "AddCar";
            BtnAddCar.UseVisualStyleBackColor = true;
            BtnAddCar.Click += BtnAddCar_Click;
            // 
            // BtnAddTrack
            // 
            BtnAddTrack.Location = new Point(442, 148);
            BtnAddTrack.Name = "BtnAddTrack";
            BtnAddTrack.Size = new Size(94, 29);
            BtnAddTrack.TabIndex = 16;
            BtnAddTrack.Text = "AddTrack";
            BtnAddTrack.UseVisualStyleBackColor = true;
            BtnAddTrack.Click += BtnAddTrack_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            ClientSize = new Size(1026, 450);
            Controls.Add(BtnAddTrack);
            Controls.Add(BtnAddCar);
            Controls.Add(txtNewTrack);
            Controls.Add(txtNewCar);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblStatus);
            Controls.Add(lstResults);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(cmbTrack);
            Controls.Add(cmbCar);
            Controls.Add(txtTime);
            Controls.Add(txtDriver);
            Name = "MainForm";
            Text = "Журнал заездов";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDriver;
        private TextBox txtTime;
        private ComboBox cmbCar;
        private ComboBox cmbTrack;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnClear;
        private ListBox lstResults;
        private Label lblStatus;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtNewCar;
        private TextBox txtNewTrack;
        private Button BtnAddCar;
        private Button BtnAddTrack;
    }
}
