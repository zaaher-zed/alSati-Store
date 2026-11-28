
namespace alSati_store
{
    partial class LibraryPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            pnlLibraryHeader = new Panel();
            pnlLibraryUser = new Panel();
            lblLibraryUserIcon = new Label();
            lblLibraryUserName = new Label();
            btnLibraryLogout = new Button();
            pnlLibrarySearch = new Panel();
            lblLibrarySearchIcon = new Label();
            txtLibrarySearch = new TextBox();
            lblLibraryTitle = new Label();
            flpLibraryGames = new HiddenScrollFlowLayoutPanel();

            pnlLibraryHeader.SuspendLayout();
            pnlLibraryUser.SuspendLayout();
            pnlLibrarySearch.SuspendLayout();
            SuspendLayout();

            // pnlLibraryHeader
            pnlLibraryHeader.Controls.Add(pnlLibraryUser);
            pnlLibraryHeader.Controls.Add(btnLibraryLogout);
            pnlLibraryHeader.Controls.Add(pnlLibrarySearch);
            pnlLibraryHeader.Dock = DockStyle.Top;
            pnlLibraryHeader.Location = new Point(0, 0);
            pnlLibraryHeader.Name = "pnlLibraryHeader";
            pnlLibraryHeader.Size = new Size(1200, 65);
            pnlLibraryHeader.TabIndex = 2;

            // pnlLibraryUser
            pnlLibraryUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlLibraryUser.BackColor = Color.FromArgb(43, 29, 35);
            pnlLibraryUser.Controls.Add(lblLibraryUserIcon);
            pnlLibraryUser.Controls.Add(lblLibraryUserName);
            pnlLibraryUser.Location = new Point(950, 11);
            pnlLibraryUser.Name = "pnlLibraryUser";
            pnlLibraryUser.Size = new Size(180, 42);
            pnlLibraryUser.TabIndex = 2;

            // lblLibraryUserIcon
            lblLibraryUserIcon.BackColor = Color.Transparent;
            lblLibraryUserIcon.Font = new Font("Segoe UI Symbol", 16.8F);
            lblLibraryUserIcon.ForeColor = Color.FromArgb(210, 175, 190);
            lblLibraryUserIcon.Location = new Point(5, 0);
            lblLibraryUserIcon.Name = "lblLibraryUserIcon";
            lblLibraryUserIcon.Size = new Size(40, 42);
            lblLibraryUserIcon.TabIndex = 1;
            lblLibraryUserIcon.Text = "◉";
            lblLibraryUserIcon.TextAlign = ContentAlignment.MiddleCenter;

            // lblLibraryUserName
            lblLibraryUserName.BackColor = Color.Transparent;
            lblLibraryUserName.Font = new Font("Segoe UI", 10.2F);
            lblLibraryUserName.ForeColor = Color.White;
            lblLibraryUserName.Location = new Point(55, 0);
            lblLibraryUserName.Name = "lblLibraryUserName";
            lblLibraryUserName.Size = new Size(110, 42);
            lblLibraryUserName.TabIndex = 0;
            lblLibraryUserName.Text = "Guest";
            lblLibraryUserName.TextAlign = ContentAlignment.MiddleLeft;

            // btnLibraryLogout
            btnLibraryLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLibraryLogout.BackColor = Color.FromArgb(65, 35, 45);
            btnLibraryLogout.FlatAppearance.BorderSize = 0;
            btnLibraryLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(145, 55, 75);
            btnLibraryLogout.FlatStyle = FlatStyle.Flat;
            btnLibraryLogout.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLibraryLogout.ForeColor = Color.White;
            btnLibraryLogout.Location = new Point(830, 11);
            btnLibraryLogout.Name = "btnLibraryLogout";
            btnLibraryLogout.Size = new Size(105, 42);
            btnLibraryLogout.TabIndex = 3;
            btnLibraryLogout.Text = "Logout";
            btnLibraryLogout.UseVisualStyleBackColor = false;

            // pnlLibrarySearch
            pnlLibrarySearch.BackColor = Color.FromArgb(43, 29, 35);
            pnlLibrarySearch.Controls.Add(lblLibrarySearchIcon);
            pnlLibrarySearch.Controls.Add(txtLibrarySearch);
            pnlLibrarySearch.Location = new Point(20, 11);
            pnlLibrarySearch.Name = "pnlLibrarySearch";
            pnlLibrarySearch.Size = new Size(350, 42);
            pnlLibrarySearch.TabIndex = 1;

            // lblLibrarySearchIcon
            lblLibrarySearchIcon.BackColor = Color.Transparent;
            lblLibrarySearchIcon.Font = new Font("Segoe UI Symbol", 18F);
            lblLibrarySearchIcon.ForeColor = Color.FromArgb(210, 175, 190);
            lblLibrarySearchIcon.Location = new Point(5, 0);
            lblLibrarySearchIcon.Name = "lblLibrarySearchIcon";
            lblLibrarySearchIcon.Size = new Size(40, 40);
            lblLibrarySearchIcon.TabIndex = 1;
            lblLibrarySearchIcon.Text = "⌕";
            lblLibrarySearchIcon.TextAlign = ContentAlignment.MiddleCenter;

            // txtLibrarySearch
            txtLibrarySearch.BackColor = Color.FromArgb(43, 29, 35);
            txtLibrarySearch.BorderStyle = BorderStyle.None;
            txtLibrarySearch.Font = new Font("Segoe UI", 10.8F);
            txtLibrarySearch.ForeColor = Color.White;
            txtLibrarySearch.Location = new Point(45, 10);
            txtLibrarySearch.Name = "txtLibrarySearch";
            txtLibrarySearch.PlaceholderText = "Search games...";
            txtLibrarySearch.Size = new Size(290, 24);
            txtLibrarySearch.TabIndex = 0;

            // lblLibraryTitle
            lblLibraryTitle.BackColor = Color.Transparent;
            lblLibraryTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblLibraryTitle.ForeColor = Color.White;
            lblLibraryTitle.Location = new Point(20, 75);
            lblLibraryTitle.Name = "lblLibraryTitle";
            lblLibraryTitle.Size = new Size(250, 45);
            lblLibraryTitle.TabIndex = 3;
            lblLibraryTitle.Text = "My Library";
            lblLibraryTitle.TextAlign = ContentAlignment.MiddleLeft;

            // flpLibraryGames
            flpLibraryGames.AutoScroll = true;
            flpLibraryGames.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpLibraryGames.Location = new Point(20, 140);
            flpLibraryGames.Name = "flpLibraryGames";
            flpLibraryGames.Size = new Size(1200, 600);
            flpLibraryGames.TabIndex = 5;
            flpLibraryGames.WrapContents = true;

            // LibraryPage
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = false;
            BackColor = Color.FromArgb(15, 11, 22);
            Controls.Add(flpLibraryGames);
            Controls.Add(lblLibraryTitle);
            Controls.Add(pnlLibraryHeader);
            Name = "LibraryPage";
            Size = new Size(1200, 700);

            pnlLibraryHeader.ResumeLayout(false);
            pnlLibraryUser.ResumeLayout(false);
            pnlLibrarySearch.ResumeLayout(false);
            pnlLibrarySearch.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlLibraryHeader;
        private Panel pnlLibraryUser;
        private Label lblLibraryUserIcon;
        private Label lblLibraryUserName;
        private Button btnLibraryLogout;
        private Panel pnlLibrarySearch;
        private Label lblLibrarySearchIcon;
        private TextBox txtLibrarySearch;
        private Label lblLibraryTitle;
        private HiddenScrollFlowLayoutPanel flpLibraryGames;
    }
}
