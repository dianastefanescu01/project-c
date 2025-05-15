using System;
using System.Windows.Forms;
using log4net;
using model;
using networking.protocol;
using services;

namespace SwimmingApplication.forms;

public partial class Login : Form
{
    private readonly SwimmingClientController swimmingClientController;
    private static readonly ILog log = LogManager.GetLogger(typeof(Login));

    public Login(SwimmingClientController swimmingClientController)
    {
        this.swimmingClientController = swimmingClientController;
        InitializeComponent();

        // Setup placeholders
        emailTextBox.Enter += (s, e) =>
        {
            if (emailTextBox.Text == "Email")
                emailTextBox.Text = "";
        };

        passwordTextBox.Enter += (s, e) =>
        {
            if (passwordTextBox.Text == "Password")
            {
                passwordTextBox.Text = "";
                passwordTextBox.UseSystemPasswordChar = true;
            }
        };

        emailTextBox.Leave += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(emailTextBox.Text))
                emailTextBox.Text = "Email";
        };

        passwordTextBox.Leave += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                passwordTextBox.UseSystemPasswordChar = false;
                passwordTextBox.Text = "Password";
            }
        };
    }

    private void loginBtn_Click(object sender, EventArgs e)
    {
       
        string email = emailTextBox.Text.Trim();
        string password = passwordTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
            email == "Email" || password == "Password")
        {
            MessageBox.Show("Please enter both email and password.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            swimmingClientController.login(email, password);
            log.Debug("Login succeded");
            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainForm mainForm = new MainForm(swimmingClientController);
            //swimmingClientController.updateEvent += mainForm.r;
            mainForm.Show();
            this.Hide();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Login error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void registerBtn_Click(object sender, EventArgs e)
    {
        var registerForm = new Registration(swimmingClientController); // Pass the proxy to Registration
        registerForm.Show();
        this.Hide();
    }
}

