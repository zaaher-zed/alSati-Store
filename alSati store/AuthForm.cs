
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using Oracle.ManagedDataAccess.Client;

namespace alSati_store
{
    public partial class AuthForm : Form
    {
        private AppContext appContext;
        private Label btnShowLoginPassword;
        private Label btnShowRegisterPassword;
        private Timer loginEyeTimer;
        private Timer registerEyeTimer;

        private void LoadRememberedEmail()
        {
            chkRememberMe.Checked = Properties.Settings.Default.RememberEmail;

            if (chkRememberMe.Checked)
            {
                txtEmail.Text = Properties.Settings.Default.RememberedEmail;
            }
            else
            {
                txtEmail.Clear();
            }
        }

        private void SaveRememberedEmail(string email)
        {
            if (chkRememberMe.Checked)
            {
                Properties.Settings.Default.RememberEmail = true;
                Properties.Settings.Default.RememberedEmail = email;
            }
            else
            {
                Properties.Settings.Default.RememberEmail = false;
                Properties.Settings.Default.RememberedEmail = "";
            }

            Properties.Settings.Default.Save();
        }

        private void ShowLogin()
        {
            lblAuthTitle.Visible = true;
            txtEmail.Visible = true;
            txtPassword.Visible = true;
            btnLogin.Visible = true;
            lblSwitchRegister.Visible = true;
            chkRememberMe.Visible = true;

            lblRegisterTitle.Visible = false;
            txtUsername.Visible = false;
            txtDisplayName.Visible = false;
            txtRegisterEmail.Visible = false;
            txtRegisterPassword.Visible = false;
            btnRegister.Visible = false;
            lblSwitchLogin.Visible = false;

            lblFormCaption.Text = "WELCOME BACK";
            lblAuthTitle.Text = "Welcome Back";

            btnShowLoginPassword.Visible = true;
            btnShowRegisterPassword.Visible = false;

            txtPassword.UseSystemPasswordChar = true;
            btnShowLoginPassword.ForeColor = Color.FromArgb(180, 140, 240);

            txtEmail.Focus();
        }

        private void ShowRegister()
        {
            lblAuthTitle.Visible = false;
            txtEmail.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
            lblSwitchRegister.Visible = false;
            chkRememberMe.Visible = false;

            lblRegisterTitle.Visible = true;
            txtUsername.Visible = true;
            txtDisplayName.Visible = true;
            txtRegisterEmail.Visible = true;
            txtRegisterPassword.Visible = true;
            btnRegister.Visible = true;
            lblSwitchLogin.Visible = true;

            lblFormCaption.Text = "JOIN OUR GAMING COMMUNITY";
            lblRegisterTitle.Text = "Create Account";

            btnShowLoginPassword.Visible = false;
            btnShowRegisterPassword.Visible = true;

            txtRegisterPassword.UseSystemPasswordChar = true;
            btnShowRegisterPassword.ForeColor = Color.FromArgb(180, 140, 240);

            txtUsername.Focus();
        }

        private void RegisterUser()
        {
            string username = txtUsername.Text.Trim();
            string displayName = txtDisplayName.Text.Trim();
            string email = txtRegisterEmail.Text.Trim();
            string password = txtRegisterPassword.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(displayName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (username.Length > 50 ||
                displayName.Length > 50 ||
                email.Length > 100)
            {
                MessageBox.Show("One or more fields exceed the allowed length.");
                return;
            }

            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters.");
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            try
            {
                using (OracleConnection connection = Database.GetConnection())
                {
                    connection.Open();

                    string checkQuery = @"
                        SELECT COUNT(*)
                        FROM USERS
                        WHERE USERNAME = :username
                           OR EMAIL = :email";

                    using (OracleCommand checkCommand =
                           new OracleCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.Add(
                            "username", OracleDbType.Varchar2, 50
                        ).Value = username;

                        checkCommand.Parameters.Add(
                            "email", OracleDbType.Varchar2, 100
                        ).Value = email;

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show(
                                "Username or email is already registered.",
                                "Registration Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return;
                        }
                    }

                    string passwordHash = PasswordHasher.HashPassword(password);

                    string insertQuery = @"
                        INSERT INTO USERS
                            (USERNAME, DISPLAY_NAME, EMAIL, PASSWORD_HASH)
                        VALUES
                            (:username, :displayName, :email, :passwordHash)";

                    using (OracleCommand insertCommand =
                           new OracleCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.Add(
                            "username", OracleDbType.Varchar2, 50
                        ).Value = username;

                        insertCommand.Parameters.Add(
                            "displayName", OracleDbType.Varchar2, 50
                        ).Value = displayName;

                        insertCommand.Parameters.Add(
                            "email", OracleDbType.Varchar2, 100
                        ).Value = email;

                        insertCommand.Parameters.Add(
                            "passwordHash", OracleDbType.Varchar2, 255
                        ).Value = passwordHash;

                        insertCommand.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Account created successfully!",
                    "Registration Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                txtEmail.Text = email;
                txtPassword.Clear();

                txtUsername.Clear();
                txtDisplayName.Clear();
                txtRegisterEmail.Clear();
                txtRegisterPassword.Clear();

                ShowLogin();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(
                    "Database error: " + ex.Message,
                    "Registration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unexpected error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoginUser()
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your email and password.");
                return;
            }

            try
            {
                using (OracleConnection connection = Database.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT USER_ID, USERNAME, DISPLAY_NAME,
                               PASSWORD_HASH, ACCOUNT_ROLE
                        FROM USERS
                        WHERE EMAIL = :email
                          AND ACCOUNT_STATUS = 'ACTIVE'
                          AND IS_DELETED = 0";

                    using (OracleCommand command =
                           new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(
                            "email", OracleDbType.Varchar2, 100
                        ).Value = email;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Invalid email or password.");
                                return;
                            }

                            string storedHash = reader["PASSWORD_HASH"].ToString();

                            if (!PasswordHasher.VerifyPassword(password, storedHash))
                            {
                                MessageBox.Show("Invalid email or password.");
                                return;
                            }

                            int userId = Convert.ToInt32(reader["USER_ID"]);
                            string username = reader["USERNAME"].ToString();
                            string displayName = reader["DISPLAY_NAME"].ToString();
                            string role = reader["ACCOUNT_ROLE"].ToString();

                            reader.Close();

                            SaveRememberedEmail(email);

                            appContext.ShowMainForm(
                                userId,
                                username,
                                displayName,
                                role);

                            this.Close();
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show(
                    "Database error: " + ex.Message,
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unexpected error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public AuthForm(AppContext context)
        {
            InitializeComponent();
            appContext = context;

            InitializePasswordVisibility();
            InitializeInputFocusEffects();

            LoadRememberedEmail();

            ShowLogin();
        }

        private void InitializePasswordVisibility()
        {
            btnShowLoginPassword = new Label
            {
                Text = "◉",
                BackColor = txtPassword.BackColor,
                UseCompatibleTextRendering = true,
                ForeColor = Color.FromArgb(180, 140, 240),
                Font = new Font("Segoe UI", 13F),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(35, 29),
                Location = new Point(451, 346),
                Cursor = Cursors.Hand
            };

            btnShowRegisterPassword = new Label
            {
                Text = "◉",
                BackColor = txtPassword.BackColor,
                UseCompatibleTextRendering = true,
                ForeColor = Color.FromArgb(180, 140, 240),
                Font = new Font("Segoe UI", 13F),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(35, 29),
                Location = new Point(451, 421),
                Cursor = Cursors.Hand,
                Visible = false
            };

            btnShowLoginPassword.Click += (s, e) =>
            {
                txtPassword.UseSystemPasswordChar =
                    !txtPassword.UseSystemPasswordChar;

                btnShowLoginPassword.ForeColor =
                    txtPassword.UseSystemPasswordChar
                        ? Color.FromArgb(180, 140, 240)
                        : Color.White;
            };

            btnShowRegisterPassword.Click += (s, e) =>
            {
                txtRegisterPassword.UseSystemPasswordChar =
                    !txtRegisterPassword.UseSystemPasswordChar;

                btnShowRegisterPassword.ForeColor =
                    txtRegisterPassword.UseSystemPasswordChar
                        ? Color.FromArgb(180, 140, 240)
                        : Color.White;
            };

            pnlFormSide.Controls.Add(btnShowLoginPassword);
            pnlFormSide.Controls.Add(btnShowRegisterPassword);

            btnShowLoginPassword.BringToFront();
            btnShowRegisterPassword.BringToFront();

            loginEyeTimer = AddEyeHoverAnimation(btnShowLoginPassword);
            registerEyeTimer = AddEyeHoverAnimation(btnShowRegisterPassword);
        }

        private Timer AddEyeHoverAnimation(Label eye)
        {
            float normalSize = 13F;
            float hoverSize = 15F;

            bool hovering = false;

            Timer animationTimer = new Timer
            {
                Interval = 15
            };

            eye.MouseEnter += (s, e) =>
            {
                hovering = true;
                eye.ForeColor = Color.White;
                animationTimer.Start();
            };

            eye.MouseLeave += (s, e) =>
            {
                hovering = false;
                eye.ForeColor = Color.FromArgb(180, 140, 240);
                animationTimer.Start();
            };

            animationTimer.Tick += (s, e) =>
            {
                float currentSize = eye.Font.Size;
                float targetSize = hovering ? hoverSize : normalSize;

                if (Math.Abs(currentSize - targetSize) <= 0.5F)
                {
                    Font oldFont = eye.Font;
                    eye.Font = new Font("Segoe UI", targetSize);
                    oldFont.Dispose();

                    animationTimer.Stop();
                    return;
                }

                float nextSize = hovering
                    ? currentSize + 0.5F
                    : currentSize - 0.5F;

                Font previousFont = eye.Font;
                eye.Font = new Font("Segoe UI", nextSize);
                previousFont.Dispose();
            };

            return animationTimer;
        }

        private void InitializeInputFocusEffects()
        {
            Color focusColor = Color.FromArgb(45, 35, 60);

            Control[] inputs =
            {
                txtEmail,
                txtPassword,
                txtUsername,
                txtDisplayName,
                txtRegisterEmail,
                txtRegisterPassword
            };

            foreach (Control input in inputs)
            {
                Color originalColor = input.BackColor;

                input.Enter += (s, e) =>
                {
                    input.BackColor = focusColor;
                };

                input.Leave += (s, e) =>
                {
                    input.BackColor = originalColor;
                };
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            loginEyeTimer?.Stop();
            loginEyeTimer?.Dispose();

            registerEyeTimer?.Stop();
            registerEyeTimer?.Dispose();

            base.OnFormClosed(e);
        }

        private void pnlAuth_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblSwitchRegister_Click(object sender, EventArgs e)
        {
            ShowRegister();
        }

        private void lblSwitchLogin_Click(object sender, EventArgs e)
        {
            ShowLogin();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterUser();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginUser();
        }
    }
}
