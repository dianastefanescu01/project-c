using System.ComponentModel;

namespace SwimmingApplication.forms;

partial class Login
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
        loginLabel = new System.Windows.Forms.Label();
        emailTextBox = new System.Windows.Forms.TextBox();
        passwordTextBox = new System.Windows.Forms.TextBox();
        loginBtn = new System.Windows.Forms.Button();
        registerBtn = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // loginLabel
        // 
        loginLabel.Font = new System.Drawing.Font("Georgia", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        loginLabel.Location = new System.Drawing.Point(34, 68);
        loginLabel.Margin = new System.Windows.Forms.Padding(29, 0, 29, 0);
        loginLabel.Name = "loginLabel";
        loginLabel.Size = new System.Drawing.Size(344, 70);
        loginLabel.TabIndex = 0;
        loginLabel.Text = "Log in";
        loginLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // emailTextBox
        // 
        emailTextBox.BackColor = System.Drawing.SystemColors.Control;
        emailTextBox.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        emailTextBox.ForeColor = System.Drawing.SystemColors.InfoText;
        emailTextBox.Location = new System.Drawing.Point(34, 230);
        emailTextBox.Margin = new System.Windows.Forms.Padding(21, 3, 21, 3);
        emailTextBox.Name = "emailTextBox";
        emailTextBox.Size = new System.Drawing.Size(344, 30);
        emailTextBox.TabIndex = 1;
        emailTextBox.Text = "Email";
        // 
        // passwordTextBox
        // 
        passwordTextBox.BackColor = System.Drawing.SystemColors.Control;
        passwordTextBox.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        passwordTextBox.ForeColor = System.Drawing.SystemColors.InfoText;
        passwordTextBox.Location = new System.Drawing.Point(34, 312);
        passwordTextBox.Margin = new System.Windows.Forms.Padding(21, 3, 21, 3);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.Size = new System.Drawing.Size(344, 30);
        passwordTextBox.TabIndex = 2;
        passwordTextBox.Text = "Password";
        // 
        // loginBtn
        // 
        loginBtn.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        loginBtn.Location = new System.Drawing.Point(134, 388);
        loginBtn.Margin = new System.Windows.Forms.Padding(21, 3, 21, 3);
        loginBtn.Name = "loginBtn";
        loginBtn.Size = new System.Drawing.Size(152, 74);
        loginBtn.TabIndex = 3;
        loginBtn.Text = "Log in";
        loginBtn.UseVisualStyleBackColor = true;
        loginBtn.Click += loginBtn_Click;
        // 
        // registerBtn
        // 
        registerBtn.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        registerBtn.Location = new System.Drawing.Point(134, 518);
        registerBtn.Margin = new System.Windows.Forms.Padding(21, 3, 21, 3);
        registerBtn.Name = "registerBtn";
        registerBtn.Size = new System.Drawing.Size(152, 74);
        registerBtn.TabIndex = 4;
        registerBtn.Text = "Register";
        registerBtn.UseVisualStyleBackColor = true;
        registerBtn.Click += registerBtn_Click;
        // 
        // Login
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.SteelBlue;
        ClientSize = new System.Drawing.Size(404, 698);
        Controls.Add(registerBtn);
        Controls.Add(loginBtn);
        Controls.Add(passwordTextBox);
        Controls.Add(emailTextBox);
        Controls.Add(loginLabel);
        Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Text = "Login";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button registerBtn;

    private System.Windows.Forms.Button loginBtn;

    private System.Windows.Forms.TextBox passwordTextBox;

    private System.Windows.Forms.TextBox emailTextBox;

    private System.Windows.Forms.Label loginLabel;

    #endregion
}