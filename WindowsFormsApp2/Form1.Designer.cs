namespace WindowsFormsApp2
{
    partial class ProgramDebt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 


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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Add_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Load_button = new System.Windows.Forms.Button();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Save_button = new System.Windows.Forms.Button();
            this.Search = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.Search_button = new System.Windows.Forms.Button();
            this.Select_button = new System.Windows.Forms.Button();
            this.Save_report_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Add_button
            // 
            this.Add_button.Location = new System.Drawing.Point(893, 110);
            this.Add_button.Name = "Add_button";
            this.Add_button.Size = new System.Drawing.Size(191, 38);
            this.Add_button.TabIndex = 1;
            this.Add_button.Text = "Add";
            this.Add_button.UseVisualStyleBackColor = true;
            this.Add_button.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Location = new System.Drawing.Point(893, 154);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(191, 39);
            this.Delete_button.TabIndex = 2;
            this.Delete_button.Text = "Delete";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Load_button
            // 
            this.Load_button.Location = new System.Drawing.Point(892, 257);
            this.Load_button.Name = "Load_button";
            this.Load_button.Size = new System.Drawing.Size(191, 43);
            this.Load_button.TabIndex = 3;
            this.Load_button.Text = "Open file";
            this.Load_button.UseVisualStyleBackColor = true;
            this.Load_button.Click += new System.EventHandler(this.Load_button_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(875, 517);
            this.dataGridView1.TabIndex = 4;
            // 
            // Save_button
            // 
            this.Save_button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Save_button.Location = new System.Drawing.Point(893, 539);
            this.Save_button.Name = "Save_button";
            this.Save_button.Size = new System.Drawing.Size(191, 44);
            this.Save_button.TabIndex = 5;
            this.Save_button.Text = "Save change";
            this.Save_button.UseVisualStyleBackColor = false;
            this.Save_button.Click += new System.EventHandler(this.Save_button_Click);
            // 
            // Search
            // 
            this.Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Search.Location = new System.Drawing.Point(12, 22);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(73, 29);
            this.Search.TabIndex = 6;
            this.Search.Text = "Search";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(91, 22);
            this.textBoxSearch.Multiline = true;
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(357, 29);
            this.textBoxSearch.TabIndex = 7;
            // 
            // Search_button
            // 
            this.Search_button.Location = new System.Drawing.Point(463, 22);
            this.Search_button.Name = "Search_button";
            this.Search_button.Size = new System.Drawing.Size(111, 29);
            this.Search_button.TabIndex = 8;
            this.Search_button.Text = "Find";
            this.Search_button.UseVisualStyleBackColor = true;
            this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
            // 
            // Select_button
            // 
            this.Select_button.Location = new System.Drawing.Point(893, 66);
            this.Select_button.Name = "Select_button";
            this.Select_button.Size = new System.Drawing.Size(191, 38);
            this.Select_button.TabIndex = 9;
            this.Select_button.Text = "Select all";
            this.Select_button.UseVisualStyleBackColor = true;
            this.Select_button.Click += new System.EventHandler(this.Select_button_Click);
            // 
            // Save_report_button
            // 
            this.Save_report_button.Location = new System.Drawing.Point(893, 316);
            this.Save_report_button.Name = "Save_report_button";
            this.Save_report_button.Size = new System.Drawing.Size(190, 42);
            this.Save_report_button.TabIndex = 10;
            this.Save_report_button.Text = "Save report";
            this.Save_report_button.UseVisualStyleBackColor = true;
            this.Save_report_button.Click += new System.EventHandler(this.Save_report_button_Click);
            // 
            // ProgramDebt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 602);
            this.Controls.Add(this.Save_report_button);
            this.Controls.Add(this.Select_button);
            this.Controls.Add(this.Search_button);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.Save_button);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Load_button);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.Add_button);
            this.Name = "ProgramDebt";
            this.Text = "ProgramDebt";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Add_button;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button Load_button;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Save_button;
        private System.Windows.Forms.Label Search;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button Search_button;
        private System.Windows.Forms.Button Select_button;
        private System.Windows.Forms.Button Save_report_button;
    }
}

