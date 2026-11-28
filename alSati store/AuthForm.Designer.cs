
namespace alSati_store
{
    partial class AuthForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlAuth = new Panel();
            pnlFormSide = new Panel();
            lblFormCaption = new Label();
            btnRegister = new Button();
            txtRegisterPassword = new TextBox();
            txtRegisterEmail = new TextBox();
            txtDisplayName = new TextBox();
            txtUsername = new TextBox();
            lblSwitchLogin = new Label();
            lblRegisterTitle = new Label();
            lblSwitchRegister = new Label();
            chkRememberMe = new CheckBox();
            btnLogin = new Button();
            txtPassword = new TextBox();
            txtEmail = new TextBox();
            lblAuthTitle = new Label();
            pnlArtwork = new Panel();
            pnlAuth.SuspendLayout();
            pnlFormSide.SuspendLayout();
            SuspendLayout();
            // 
            // pnlAuth
            // 
            pnlAuth.BackColor = Color.FromArgb(20, 16, 29);
            pnlAuth.Controls.Add(pnlFormSide);
            pnlAuth.Controls.Add(pnlArtwork);
            pnlAuth.Dock = DockStyle.Fill;
            pnlAuth.Location = new Point(0, 0);
            pnlAuth.Name = "pnlAuth";
            pnlAuth.Size = new Size(1180, 720);
            pnlAuth.TabIndex = 0;
            // 
            // pnlFormSide
            // 
            pnlFormSide.BackColor = Color.FromArgb(20, 16, 29);
            pnlFormSide.Controls.Add(lblFormCaption);
            pnlFormSide.Controls.Add(btnRegister);
            pnlFormSide.Controls.Add(txtRegisterPassword);
            pnlFormSide.Controls.Add(txtRegisterEmail);
            pnlFormSide.Controls.Add(txtDisplayName);
            pnlFormSide.Controls.Add(txtUsername);
            pnlFormSide.Controls.Add(lblSwitchLogin);
            pnlFormSide.Controls.Add(lblRegisterTitle);
            pnlFormSide.Controls.Add(lblSwitchRegister);
            pnlFormSide.Controls.Add(chkRememberMe);
            pnlFormSide.Controls.Add(btnLogin);
            pnlFormSide.Controls.Add(txtPassword);
            pnlFormSide.Controls.Add(txtEmail);
            pnlFormSide.Controls.Add(lblAuthTitle);
            pnlFormSide.Dock = DockStyle.Fill;
            pnlFormSide.Location = new Point(570, 0);
            pnlFormSide.Name = "pnlFormSide";
            pnlFormSide.Size = new Size(610, 720);
            pnlFormSide.TabIndex = 1;
            // 
            // lblFormCaption
            // 
            lblFormCaption.AutoSize = true;
            lblFormCaption.Font = new Font("Segoe UI", 9F);
            lblFormCaption.ForeColor = Color.FromArgb(155, 145, 175);
            lblFormCaption.Location = new Point(90, 150);
            lblFormCaption.Name = "lblFormCaption";
            lblFormCaption.Size = new Size(201, 20);
            lblFormCaption.TabIndex = 0;
            lblFormCaption.Text = "WELCOME TO AL SATI STORE";
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.FromArgb(119, 75, 190);
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(90, 485);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(400, 48);
            btnRegister.TabIndex = 7;
            btnRegister.Text = "Create Account";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Visible = false;
            btnRegister.Click += btnRegister_Click;
            // 
            // txtRegisterPassword
            // 
            txtRegisterPassword.BackColor = Color.FromArgb(37, 31, 49);
            txtRegisterPassword.BorderStyle = BorderStyle.FixedSingle;
            txtRegisterPassword.Font = new Font("Segoe UI", 11F);
            txtRegisterPassword.ForeColor = Color.White;
            txtRegisterPassword.Location = new Point(90, 420);
            txtRegisterPassword.Name = "txtRegisterPassword";
            txtRegisterPassword.PlaceholderText = "Password";
            txtRegisterPassword.Size = new Size(400, 32);
            txtRegisterPassword.TabIndex = 6;
            txtRegisterPassword.UseSystemPasswordChar = true;
            txtRegisterPassword.Visible = false;
            // 
            // txtRegisterEmail
            // 
            txtRegisterEmail.BackColor = Color.FromArgb(37, 31, 49);
            txtRegisterEmail.BorderStyle = BorderStyle.FixedSingle;
            txtRegisterEmail.Font = new Font("Segoe UI", 11F);
            txtRegisterEmail.ForeColor = Color.White;
            txtRegisterEmail.Location = new Point(90, 365);
            txtRegisterEmail.Name = "txtRegisterEmail";
            txtRegisterEmail.PlaceholderText = "Email address";
            txtRegisterEmail.Size = new Size(400, 32);
            txtRegisterEmail.TabIndex = 5;
            txtRegisterEmail.Visible = false;
            // 
            // txtDisplayName
            // 
            txtDisplayName.BackColor = Color.FromArgb(37, 31, 49);
            txtDisplayName.BorderStyle = BorderStyle.FixedSingle;
            txtDisplayName.Font = new Font("Segoe UI", 11F);
            txtDisplayName.ForeColor = Color.White;
            txtDisplayName.Location = new Point(90, 310);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.PlaceholderText = "Display name";
            txtDisplayName.Size = new Size(400, 32);
            txtDisplayName.TabIndex = 4;
            txtDisplayName.Visible = false;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(37, 31, 49);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.ForeColor = Color.White;
            txtUsername.Location = new Point(90, 255);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Username";
            txtUsername.Size = new Size(400, 32);
            txtUsername.TabIndex = 3;
            txtUsername.Visible = false;
            // 
            // lblSwitchLogin
            // 
            lblSwitchLogin.AutoSize = true;
            lblSwitchLogin.Cursor = Cursors.Hand;
            lblSwitchLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSwitchLogin.ForeColor = Color.FromArgb(180, 140, 240);
            lblSwitchLogin.Location = new Point(190, 555);
            lblSwitchLogin.Name = "lblSwitchLogin";
            lblSwitchLogin.Size = new Size(272, 23);
            lblSwitchLogin.TabIndex = 8;
            lblSwitchLogin.Text = "Already have an account? Sign In";
            lblSwitchLogin.Visible = false;
            lblSwitchLogin.Click += lblSwitchLogin_Click;
            // 
            // lblRegisterTitle
            // 
            lblRegisterTitle.AutoSize = true;
            lblRegisterTitle.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            lblRegisterTitle.ForeColor = Color.White;
            lblRegisterTitle.Location = new Point(85, 185);
            lblRegisterTitle.Name = "lblRegisterTitle";
            lblRegisterTitle.Size = new Size(327, 57);
            lblRegisterTitle.TabIndex = 9;
            lblRegisterTitle.Text = "Create Account";
            lblRegisterTitle.Visible = false;
            // 
            // lblSwitchRegister
            // 
            lblSwitchRegister.AutoSize = true;
            lblSwitchRegister.Cursor = Cursors.Hand;
            lblSwitchRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSwitchRegister.ForeColor = Color.FromArgb(180, 140, 240);
            lblSwitchRegister.Location = new Point(145, 500);
            lblSwitchRegister.Name = "lblSwitchRegister";
            lblSwitchRegister.Size = new Size(284, 23);
            lblSwitchRegister.TabIndex = 10;
            lblSwitchRegister.Text = "Don't have an account? Create one";
            lblSwitchRegister.Click += lblSwitchRegister_Click;
            // 
            // chkRememberMe
            // 
            chkRememberMe.AutoSize = true;
            chkRememberMe.BackColor = Color.Transparent;
            chkRememberMe.Cursor = Cursors.Hand;
            chkRememberMe.FlatStyle = FlatStyle.Flat;
            chkRememberMe.Font = new Font("Segoe UI", 9.5F);
            chkRememberMe.ForeColor = Color.FromArgb(180, 170, 200);
            chkRememberMe.Location = new Point(90, 385);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(119, 25);
            chkRememberMe.TabIndex = 12;
            chkRememberMe.Text = "Remember Me";
            chkRememberMe.UseVisualStyleBackColor = false;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(119, 75, 190);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(90, 420);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(400, 48);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Sign In";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(37, 31, 49);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.ForeColor = Color.White;
            txtPassword.Location = new Point(90, 345);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Password";
            txtPassword.Size = new Size(400, 32);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(37, 31, 49);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.ForeColor = Color.White;
            txtEmail.Location = new Point(90, 280);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email address";
            txtEmail.Size = new Size(400, 32);
            txtEmail.TabIndex = 0;
            // 
            // lblAuthTitle
            // 
            lblAuthTitle.AutoSize = true;
            lblAuthTitle.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            lblAuthTitle.ForeColor = Color.White;
            lblAuthTitle.Location = new Point(85, 185);
            lblAuthTitle.Name = "lblAuthTitle";
            lblAuthTitle.Size = new Size(313, 57);
            lblAuthTitle.TabIndex = 11;
            lblAuthTitle.Text = "Welcome Back";
            // 
            // pnlArtwork
            // 
            pnlArtwork.BackColor = Color.FromArgb(27, 19, 40);
            pnlArtwork.BackgroundImage = Properties.Resources.alSati_store_auth_background;
            pnlArtwork.BackgroundImageLayout = ImageLayout.Zoom;
            pnlArtwork.Dock = DockStyle.Left;
            pnlArtwork.Location = new Point(0, 0);
            pnlArtwork.Name = "pnlArtwork";
            pnlArtwork.Size = new Size(570, 720);
            pnlArtwork.TabIndex = 0;
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 16, 29);
            ClientSize = new Size(1180, 720);
            Controls.Add(pnlAuth);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AuthForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "alSati Store - Welcome";
            pnlAuth.ResumeLayout(false);
            pnlFormSide.ResumeLayout(false);
            pnlFormSide.PerformLayout();
            ResumeLayout(false);
        }

        private Panel pnlAuth;
        private Panel pnlArtwork;
        private Panel pnlFormSide;
        private Label lblFormCaption;
        private TextBox txtPassword;
        private TextBox txtEmail;
        private Label lblAuthTitle;
        private TextBox txtRegisterPassword;
        private TextBox txtRegisterEmail;
        private TextBox txtDisplayName;
        private TextBox txtUsername;
        private Label lblSwitchLogin;
        private Label lblRegisterTitle;
        private Label lblSwitchRegister;
        private Button btnLogin;
        private Button btnRegister;
        private CheckBox chkRememberMe;
    }
}
