using System.ComponentModel;

namespace SwimmingApplication.forms;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        mainTableLabel = new System.Windows.Forms.Label();
        mainTable = new System.Windows.Forms.DataGridView();
        participantNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
        raceStyleCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
        raceDistanceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
        racesLabel = new System.Windows.Forms.Label();
        racesTable = new System.Windows.Forms.DataGridView();
        raceIDCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
        styleCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
        distanceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
        participantsLabel = new System.Windows.Forms.Label();
        participantsTable = new System.Windows.Forms.DataGridView();
        participantIDCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
        nameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
        ageCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
        raceStyleTextBox = new System.Windows.Forms.TextBox();
        raceDistTextBox = new System.Windows.Forms.TextBox();
        partNameTextBox = new System.Windows.Forms.TextBox();
        addBtn = new System.Windows.Forms.Button();
        logOutBtn = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)mainTable).BeginInit();
        ((System.ComponentModel.ISupportInitialize)racesTable).BeginInit();
        ((System.ComponentModel.ISupportInitialize)participantsTable).BeginInit();
        SuspendLayout();
        // 
        // mainTableLabel
        // 
        mainTableLabel.Location = new System.Drawing.Point(19, 21);
        mainTableLabel.Name = "mainTableLabel";
        mainTableLabel.Size = new System.Drawing.Size(237, 49);
        mainTableLabel.TabIndex = 0;
        mainTableLabel.Text = "Participants and Races";
        mainTableLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // mainTable
        // 
        mainTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        mainTable.BackgroundColor = System.Drawing.Color.MidnightBlue;
       // mainTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        mainTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { participantNameCol, raceStyleCol, raceDistanceCol });
        mainTable.Location = new System.Drawing.Point(19, 73);
        mainTable.Name = "mainTable";
        mainTable.RowHeadersWidth = 51;
        mainTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        mainTable.Size = new System.Drawing.Size(475, 262);
        mainTable.TabIndex = 1;
        mainTable.Text = "dataGridView1";
        // 
        // participantNameCol
        // 
        participantNameCol.HeaderText = "Participant Name";
        participantNameCol.MinimumWidth = 6;
        participantNameCol.Name = "participantNameCol";
        // 
        // raceStyleCol
        // 
        raceStyleCol.HeaderText = "Race Style";
        raceStyleCol.MinimumWidth = 6;
        raceStyleCol.Name = "raceStyleCol";
        // 
        // raceDistanceCol
        // 
        raceDistanceCol.HeaderText = "Race Distance";
        raceDistanceCol.MinimumWidth = 6;
        raceDistanceCol.Name = "raceDistanceCol";
        // 
        // racesLabel
        // 
        racesLabel.Location = new System.Drawing.Point(19, 367);
        racesLabel.Name = "racesLabel";
        racesLabel.Size = new System.Drawing.Size(221, 34);
        racesLabel.TabIndex = 2;
        racesLabel.Text = "Races";
        racesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // racesTable
        // 
        racesTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        racesTable.BackgroundColor = System.Drawing.Color.MidnightBlue;
        racesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        racesTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { raceIDCol, styleCol, distanceCol });
        racesTable.Location = new System.Drawing.Point(19, 416);
        racesTable.Name = "racesTable";
        racesTable.RowHeadersWidth = 51;
        racesTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        racesTable.Size = new System.Drawing.Size(475, 205);
        racesTable.TabIndex = 3;
        racesTable.Text = "dataGridView1";
        // 
        // raceIDCol
        // 
        raceIDCol.HeaderText = "ID";
        raceIDCol.MinimumWidth = 6;
        raceIDCol.Name = "raceIDCol";
        // 
        // styleCol
        // 
        styleCol.HeaderText = "Style";
        styleCol.MinimumWidth = 6;
        styleCol.Name = "styleCol";
        // 
        // distanceCol
        // 
        distanceCol.HeaderText = "Distance";
        distanceCol.MinimumWidth = 6;
        distanceCol.Name = "distanceCol";
        // 
        // participantsLabel
        // 
        participantsLabel.Location = new System.Drawing.Point(567, 21);
        participantsLabel.Name = "participantsLabel";
        participantsLabel.Size = new System.Drawing.Size(188, 37);
        participantsLabel.TabIndex = 4;
        participantsLabel.Text = "Participants";
        participantsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // participantsTable
        // 
        participantsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        participantsTable.BackgroundColor = System.Drawing.Color.MidnightBlue;
        participantsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        participantsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { participantIDCol, nameCol, ageCol });
        participantsTable.Location = new System.Drawing.Point(567, 73);
        participantsTable.Name = "participantsTable";
        participantsTable.RowHeadersWidth = 51;
        participantsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        participantsTable.Size = new System.Drawing.Size(691, 262);
        participantsTable.TabIndex = 5;
        participantsTable.Text = "dataGridView1";
        // 
        // participantIDCol
        // 
        participantIDCol.HeaderText = "ID";
        participantIDCol.MinimumWidth = 6;
        participantIDCol.Name = "participantIDCol";
        // 
        // nameCol
        // 
        nameCol.HeaderText = "Name";
        nameCol.MinimumWidth = 6;
        nameCol.Name = "nameCol";
        // 
        // ageCol
        // 
        ageCol.HeaderText = "Age";
        ageCol.MinimumWidth = 6;
        ageCol.Name = "ageCol";
        // 
        // raceStyleTextBox
        // 
        raceStyleTextBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        raceStyleTextBox.Location = new System.Drawing.Point(567, 416);
        raceStyleTextBox.Name = "raceStyleTextBox";
        raceStyleTextBox.Size = new System.Drawing.Size(247, 32);
        raceStyleTextBox.TabIndex = 6;
        raceStyleTextBox.Text = "Selected Race Style";
        // 
        // raceDistTextBox
        // 
        raceDistTextBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        raceDistTextBox.Location = new System.Drawing.Point(567, 508);
        raceDistTextBox.Name = "raceDistTextBox";
        raceDistTextBox.Size = new System.Drawing.Size(247, 32);
        raceDistTextBox.TabIndex = 7;
        raceDistTextBox.Text = "Selected Race Distance";
        // 
        // partNameTextBox
        // 
        partNameTextBox.Font = new System.Drawing.Font("Century", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        partNameTextBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        partNameTextBox.Location = new System.Drawing.Point(567, 589);
        partNameTextBox.Name = "partNameTextBox";
        partNameTextBox.Size = new System.Drawing.Size(247, 29);
        partNameTextBox.TabIndex = 8;
        partNameTextBox.Text = "Selected Participant Name";
        // 
        // addBtn
        // 
        addBtn.Location = new System.Drawing.Point(1028, 416);
        addBtn.Name = "addBtn";
        addBtn.Size = new System.Drawing.Size(230, 84);
        addBtn.TabIndex = 9;
        addBtn.Text = "Add";
        addBtn.UseVisualStyleBackColor = true;
        addBtn.Click += addBtn_Click;
        // 
        // logOutBtn
        // 
        logOutBtn.Location = new System.Drawing.Point(1028, 536);
        logOutBtn.Name = "logOutBtn";
        logOutBtn.Size = new System.Drawing.Size(230, 82);
        logOutBtn.TabIndex = 10;
        logOutBtn.Text = "Log out";
        logOutBtn.UseVisualStyleBackColor = true;
        logOutBtn.Click += logOutBtn_Click;
        // 
        // MainForm
        // 
        BackColor = System.Drawing.Color.SlateBlue;
        ClientSize = new System.Drawing.Size(1304, 742);
        Controls.Add(logOutBtn);
        Controls.Add(addBtn);
        Controls.Add(partNameTextBox);
        Controls.Add(raceDistTextBox);
        Controls.Add(raceStyleTextBox);
        Controls.Add(participantsTable);
        Controls.Add(participantsLabel);
        Controls.Add(racesTable);
        Controls.Add(racesLabel);
        Controls.Add(mainTable);
        Controls.Add(mainTableLabel);
        Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        ((System.ComponentModel.ISupportInitialize)mainTable).EndInit();
        ((System.ComponentModel.ISupportInitialize)racesTable).EndInit();
        ((System.ComponentModel.ISupportInitialize)participantsTable).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button logOutBtn;

    private System.Windows.Forms.Button addBtn;

    private System.Windows.Forms.TextBox partNameTextBox;

    private System.Windows.Forms.TextBox raceDistTextBox;

    private System.Windows.Forms.TextBox raceStyleTextBox;

    private System.Windows.Forms.DataGridViewTextBoxColumn participantIDCol;
    private System.Windows.Forms.DataGridViewTextBoxColumn nameCol;
    private System.Windows.Forms.DataGridViewTextBoxColumn ageCol;

    private System.Windows.Forms.DataGridView participantsTable;

    private System.Windows.Forms.Label participantsLabel;

    private System.Windows.Forms.DataGridViewTextBoxColumn raceIDCol;
    private System.Windows.Forms.DataGridViewTextBoxColumn styleCol;
    private System.Windows.Forms.DataGridViewTextBoxColumn distanceCol;

    private System.Windows.Forms.DataGridView racesTable;

    private System.Windows.Forms.Label racesLabel;

    private System.Windows.Forms.DataGridViewTextBoxColumn participantNameCol;
    private System.Windows.Forms.DataGridViewTextBoxColumn raceStyleCol;
    private System.Windows.Forms.DataGridViewTextBoxColumn raceDistanceCol;

    private System.Windows.Forms.DataGridView mainTable;

    private System.Windows.Forms.Label mainTableLabel;

    #endregion
}