using System.ComponentModel;

namespace SwimmingApplication.forms;

partial class Registration
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
        registerLabel = new System.Windows.Forms.Label();
        usernameTextBox = new System.Windows.Forms.TextBox();
        emailTextBox = new System.Windows.Forms.TextBox();
        passTextBox = new System.Windows.Forms.TextBox();
        registerBtn = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // registerLabel
        // 
        registerLabel.Font = new System.Drawing.Font("Georgia", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        registerLabel.Location = new System.Drawing.Point(86, 22);
        registerLabel.Name = "registerLabel";
        registerLabel.Size = new System.Drawing.Size(230, 56);
        registerLabel.TabIndex = 0;
        registerLabel.Text = "Register";
        registerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // usernameTextBox
        // 
        usernameTextBox.BackColor = System.Drawing.SystemColors.Control;
        usernameTextBox.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        usernameTextBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
        usernameTextBox.Location = new System.Drawing.Point(36, 118);
        usernameTextBox.Name = "usernameTextBox";
        usernameTextBox.Size = new System.Drawing.Size(335, 28);
        usernameTextBox.TabIndex = 1;
        usernameTextBox.Text = "Username";
        // 
        // emailTextBox
        // 
        emailTextBox.BackColor = System.Drawing.SystemColors.Control;
        emailTextBox.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        emailTextBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
        emailTextBox.Location = new System.Drawing.Point(36, 180);
        emailTextBox.Name = "emailTextBox";
        emailTextBox.Size = new System.Drawing.Size(335, 28);
        emailTextBox.TabIndex = 2;
        emailTextBox.Text = "Email";
        // 
        // passTextBox
        // 
        passTextBox.BackColor = System.Drawing.SystemColors.Control;
        passTextBox.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        passTextBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
        passTextBox.Location = new System.Drawing.Point(36, 245);
        passTextBox.Name = "passTextBox";
        passTextBox.Size = new System.Drawing.Size(335, 28);
        passTextBox.TabIndex = 3;
        passTextBox.Text = "Password";
        // 
        // registerBtn
        // 
        registerBtn.Font = new System.Drawing.Font("Georgia", 19.800001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        registerBtn.Location = new System.Drawing.Point(86, 298);
        registerBtn.Name = "registerBtn";
        registerBtn.Size = new System.Drawing.Size(230, 53);
        registerBtn.TabIndex = 4;
        registerBtn.Text = "Register";
        registerBtn.UseVisualStyleBackColor = true;
        registerBtn.Click += registerBtn_Click;
        // 
        // Registration
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.SteelBlue;
        ClientSize = new System.Drawing.Size(400, 371);
        Controls.Add(registerBtn);
        Controls.Add(passTextBox);
        Controls.Add(emailTextBox);
        Controls.Add(usernameTextBox);
        Controls.Add(registerLabel);
        Text = "Registration";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button registerBtn;

    private System.Windows.Forms.TextBox passTextBox;

    private System.Windows.Forms.TextBox emailTextBox;

    private System.Windows.Forms.TextBox usernameTextBox;

    private System.Windows.Forms.Label registerLabel;

    #endregion
}