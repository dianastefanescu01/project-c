using model;
using services;

namespace SwimmingApplication.forms;

public partial class Registration : Form
{
    private readonly SwimmingClientController _swimmingClientController;

    public Registration(SwimmingClientController swimmingClientController)
    {
        _swimmingClientController = swimmingClientController;
        InitializeComponent();

        // Placeholder handlers
        usernameTextBox.Enter += (s, e) =>
        {
            if (usernameTextBox.Text == "Username")
                usernameTextBox.Text = "";
        };

        emailTextBox.Enter += (s, e) =>
        {
            if (emailTextBox.Text == "Email")
                emailTextBox.Text = "";
        };

        passTextBox.Enter += (s, e) =>
        {
            if (passTextBox.Text == "Password")
            {
                passTextBox.Text = "";
                passTextBox.UseSystemPasswordChar = true;
            }
        };

        usernameTextBox.Leave += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
                usernameTextBox.Text = "Username";
        };

        emailTextBox.Leave += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(emailTextBox.Text))
                emailTextBox.Text = "Email";
        };

        passTextBox.Leave += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(passTextBox.Text))
            {
                passTextBox.UseSystemPasswordChar = false;
                passTextBox.Text = "Password";
            }
        };
    }

    private void registerBtn_Click(object sender, EventArgs e)
    {
        string name = usernameTextBox.Text.Trim();
        string email = emailTextBox.Text.Trim();
        string password = passTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
            name == "Username" || email == "Email" || password == "Password")
        {
            MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var newUser = new User(name, email, password);
            _swimmingClientController.addUser(newUser);

            MessageBox.Show("✅ Registration successful. Please log in.");
            var loginForm = new Login(_swimmingClientController);
            loginForm.Show();
            this.Hide();
        }
        catch (Exception ex)
        {
            MessageBox.Show("❌ Registration failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
