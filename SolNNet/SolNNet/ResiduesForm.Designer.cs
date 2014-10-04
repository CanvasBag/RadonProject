namespace SolNNet
{
    partial class ResiduesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResiduesForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabStations = new System.Windows.Forms.TabPage();
            this.dataGVStations = new System.Windows.Forms.DataGridView();
            this.useTrigPt = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.trigPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.easting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.northing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eastAccu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.northAcc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eastVariation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.northVariation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabDistances = new System.Windows.Forms.TabPage();
            this.dataGVDistances = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.distance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabDirections = new System.Windows.Forms.TabPage();
            this.dataGVDirections = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.stations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acceptBut = new System.Windows.Forms.Button();
            this.updateBut = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabStations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVStations)).BeginInit();
            this.tabDistances.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVDistances)).BeginInit();
            this.tabDirections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVDirections)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabStations);
            this.tabControl1.Controls.Add(this.tabDistances);
            this.tabControl1.Controls.Add(this.tabDirections);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 364);
            this.tabControl1.TabIndex = 0;
            // 
            // tabStations
            // 
            this.tabStations.Controls.Add(this.dataGVStations);
            this.tabStations.Location = new System.Drawing.Point(4, 22);
            this.tabStations.Name = "tabStations";
            this.tabStations.Padding = new System.Windows.Forms.Padding(3);
            this.tabStations.Size = new System.Drawing.Size(752, 338);
            this.tabStations.TabIndex = 0;
            this.tabStations.Text = "Stations";
            this.tabStations.UseVisualStyleBackColor = true;
            // 
            // dataGVStations
            // 
            this.dataGVStations.AllowUserToAddRows = false;
            this.dataGVStations.AllowUserToDeleteRows = false;
            this.dataGVStations.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGVStations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVStations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.useTrigPt,
            this.trigPoints,
            this.easting,
            this.northing,
            this.eastAccu,
            this.northAcc,
            this.eastVariation,
            this.northVariation});
            this.dataGVStations.Location = new System.Drawing.Point(6, 6);
            this.dataGVStations.Name = "dataGVStations";
            this.dataGVStations.Size = new System.Drawing.Size(740, 326);
            this.dataGVStations.TabIndex = 0;
            this.dataGVStations.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGVStations_CellContentClick);
            // 
            // useTrigPt
            // 
            this.useTrigPt.HeaderText = "Use";
            this.useTrigPt.Name = "useTrigPt";
            this.useTrigPt.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.useTrigPt.Width = 40;
            // 
            // trigPoints
            // 
            this.trigPoints.HeaderText = "Trig Points";
            this.trigPoints.Name = "trigPoints";
            this.trigPoints.ReadOnly = true;
            this.trigPoints.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // easting
            // 
            this.easting.HeaderText = "Easting";
            this.easting.Name = "easting";
            this.easting.ReadOnly = true;
            this.easting.Width = 85;
            // 
            // northing
            // 
            this.northing.HeaderText = "Northing";
            this.northing.Name = "northing";
            this.northing.ReadOnly = true;
            this.northing.Width = 85;
            // 
            // eastAccu
            // 
            this.eastAccu.HeaderText = "East Accuracy (m)";
            this.eastAccu.Name = "eastAccu";
            this.eastAccu.ReadOnly = true;
            this.eastAccu.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eastAccu.Width = 95;
            // 
            // northAcc
            // 
            this.northAcc.HeaderText = "North Accuracy (m)";
            this.northAcc.Name = "northAcc";
            this.northAcc.ReadOnly = true;
            this.northAcc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.northAcc.Width = 95;
            // 
            // eastVariation
            // 
            this.eastVariation.HeaderText = "East Variation (m)";
            this.eastVariation.Name = "eastVariation";
            this.eastVariation.ReadOnly = true;
            this.eastVariation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eastVariation.Width = 90;
            // 
            // northVariation
            // 
            this.northVariation.HeaderText = "North Variation (m)";
            this.northVariation.Name = "northVariation";
            this.northVariation.ReadOnly = true;
            this.northVariation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.northVariation.Width = 90;
            // 
            // tabDistances
            // 
            this.tabDistances.Controls.Add(this.dataGVDistances);
            this.tabDistances.Location = new System.Drawing.Point(4, 22);
            this.tabDistances.Name = "tabDistances";
            this.tabDistances.Padding = new System.Windows.Forms.Padding(3);
            this.tabDistances.Size = new System.Drawing.Size(752, 338);
            this.tabDistances.TabIndex = 1;
            this.tabDistances.Text = "Distances";
            this.tabDistances.UseVisualStyleBackColor = true;
            // 
            // dataGVDistances
            // 
            this.dataGVDistances.AllowUserToAddRows = false;
            this.dataGVDistances.AllowUserToDeleteRows = false;
            this.dataGVDistances.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGVDistances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVDistances.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.distance,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn3});
            this.dataGVDistances.Location = new System.Drawing.Point(6, 6);
            this.dataGVDistances.Name = "dataGVDistances";
            this.dataGVDistances.Size = new System.Drawing.Size(740, 326);
            this.dataGVDistances.TabIndex = 1;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Use";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Stations";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 160;
            // 
            // distance
            // 
            this.distance.HeaderText = "Distance";
            this.distance.Name = "distance";
            this.distance.ReadOnly = true;
            this.distance.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.distance.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Residue";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Local Redundancy";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Std. Residue";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // tabDirections
            // 
            this.tabDirections.Controls.Add(this.dataGVDirections);
            this.tabDirections.Location = new System.Drawing.Point(4, 22);
            this.tabDirections.Name = "tabDirections";
            this.tabDirections.Size = new System.Drawing.Size(752, 338);
            this.tabDirections.TabIndex = 2;
            this.tabDirections.Text = "Directions";
            this.tabDirections.UseVisualStyleBackColor = true;
            // 
            // dataGVDirections
            // 
            this.dataGVDirections.AllowUserToAddRows = false;
            this.dataGVDirections.AllowUserToDeleteRows = false;
            this.dataGVDirections.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGVDirections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVDirections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn2,
            this.stations,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dataGVDirections.Location = new System.Drawing.Point(6, 6);
            this.dataGVDirections.Name = "dataGVDirections";
            this.dataGVDirections.Size = new System.Drawing.Size(740, 326);
            this.dataGVDirections.TabIndex = 2;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.HeaderText = "Use";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn2.Width = 40;
            // 
            // stations
            // 
            this.stations.HeaderText = "Stations";
            this.stations.Name = "stations";
            this.stations.ReadOnly = true;
            this.stations.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.stations.Width = 160;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Direction";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Residue";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Local Redundancy";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn7.Width = 120;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Std. Residue";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn8.Width = 120;
            // 
            // acceptBut
            // 
            this.acceptBut.Location = new System.Drawing.Point(541, 388);
            this.acceptBut.Name = "acceptBut";
            this.acceptBut.Size = new System.Drawing.Size(75, 28);
            this.acceptBut.TabIndex = 1;
            this.acceptBut.Text = "Accept";
            this.acceptBut.UseVisualStyleBackColor = true;
            this.acceptBut.Click += new System.EventHandler(this.acceptBut_Click);
            // 
            // updateBut
            // 
            this.updateBut.Location = new System.Drawing.Point(643, 388);
            this.updateBut.Name = "updateBut";
            this.updateBut.Size = new System.Drawing.Size(75, 28);
            this.updateBut.TabIndex = 2;
            this.updateBut.Text = "Update";
            this.updateBut.UseVisualStyleBackColor = true;
            this.updateBut.Click += new System.EventHandler(this.updateBut_Click);
            // 
            // ResiduesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 425);
            this.Controls.Add(this.updateBut);
            this.Controls.Add(this.acceptBut);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ResiduesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RP SolNNet - Residues Processing";
            this.tabControl1.ResumeLayout(false);
            this.tabStations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVStations)).EndInit();
            this.tabDistances.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVDistances)).EndInit();
            this.tabDirections.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVDirections)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabStations;
        private System.Windows.Forms.DataGridView dataGVStations;
        private System.Windows.Forms.TabPage tabDistances;
        private System.Windows.Forms.DataGridView dataGVDistances;
        private System.Windows.Forms.TabPage tabDirections;
        private System.Windows.Forms.DataGridView dataGVDirections;
        private System.Windows.Forms.Button acceptBut;
        private System.Windows.Forms.Button updateBut;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn stations;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn distance;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn useTrigPt;
        private System.Windows.Forms.DataGridViewTextBoxColumn trigPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn easting;
        private System.Windows.Forms.DataGridViewTextBoxColumn northing;
        private System.Windows.Forms.DataGridViewTextBoxColumn eastAccu;
        private System.Windows.Forms.DataGridViewTextBoxColumn northAcc;
        private System.Windows.Forms.DataGridViewTextBoxColumn eastVariation;
        private System.Windows.Forms.DataGridViewTextBoxColumn northVariation;
    }
}