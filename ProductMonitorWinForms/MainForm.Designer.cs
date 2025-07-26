using System.Windows.Forms;

namespace ProductMonitorWinForms
{
    partial class MainForm
    {
        private DataGridView gridView;
        private ComboBox filterBox;

        private void InitializeComponent()
        {
            this.gridView = new DataGridView();
            this.filterBox = new ComboBox();

            this.SuspendLayout();

            // gridView
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Columns.Add("Produto", "Produto");
            this.gridView.Columns.Add("Consultas", "Consultas");
            this.gridView.Location = new System.Drawing.Point(12, 41);
            this.gridView.Name = "gridView";
            this.gridView.ReadOnly = true;
            this.gridView.RowTemplate.Height = 25;
            this.gridView.Size = new System.Drawing.Size(360, 200);
            this.gridView.TabIndex = 0;

            // filterBox
            this.filterBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.filterBox.Items.AddRange(new object[] { "Hoje", "Este Mês", "Este Ano" });
            this.filterBox.SelectedIndex = 0;
            this.filterBox.Location = new System.Drawing.Point(12, 12);
            this.filterBox.Name = "filterBox";
            this.filterBox.Size = new System.Drawing.Size(121, 23);
            this.filterBox.TabIndex = 1;
            this.filterBox.SelectedIndexChanged += filterBox_SelectedIndexChanged;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.filterBox);
            this.Controls.Add(this.gridView);
            this.Name = "MainForm";
            this.Text = "Monitor de Produtos";
            this.FormClosing += (s, e) => { e.Cancel = true; this.Hide(); };
            this.ResumeLayout(false);
        }
    }
}
