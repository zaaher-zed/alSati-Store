
namespace alSati_store
{
    partial class StorePage
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
            pnlStoreHeader = new Panel();
            pnlStoreUser = new Panel();
            lblStoreUserIcon = new Label();
            lblStoreUserName = new Label();
            btnStoreLogout = new Button();
            pnlStoreSearch = new Panel();
            lblStoreSearchIcon = new Label();
            txtStoreSearch = new TextBox();
            lblStoreTitle = new Label();
            flpStoreCategories = new FlowLayoutPanel();
            btnAllGames = new Button();
            btnRPG = new Button();
            btnAction = new Button();
            btnAdventure = new Button();
            flpStoreGames = new HiddenScrollFlowLayoutPanel();
            cmbStoreSort = new ComboBox();
            lblAllGamesTitle = new Label();

            pnlStoreHeader.SuspendLayout();
            pnlStoreUser.SuspendLayout();
            pnlStoreSearch.SuspendLayout();
            flpStoreCategories.SuspendLayout();
            SuspendLayout();

            pnlStoreHeader.Controls.Add(pnlStoreUser);
            pnlStoreHeader.Controls.Add(btnStoreLogout);
            pnlStoreHeader.Controls.Add(pnlStoreSearch);
            pnlStoreHeader.Dock = DockStyle.Top;
            pnlStoreHeader.Location = new Point(0, 0);
            pnlStoreHeader.Name = "pnlStoreHeader";
            pnlStoreHeader.Size = new Size(1200, 65);
            pnlStoreHeader.TabIndex = 1;

            pnlStoreUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlStoreUser.BackColor = Color.FromArgb(43, 29, 35);
            pnlStoreUser.Controls.Add(lblStoreUserIcon);
            pnlStoreUser.Controls.Add(lblStoreUserName);
            pnlStoreUser.Location = new Point(1000, 11);
            pnlStoreUser.Name = "pnlStoreUser";
            pnlStoreUser.Size = new Size(180, 42);
            pnlStoreUser.TabIndex = 2;

            lblStoreUserIcon.BackColor = Color.Transparent;
            lblStoreUserIcon.Font = new Font("Segoe UI Symbol", 16.8F);
            lblStoreUserIcon.ForeColor = Color.FromArgb(210, 175, 190);
            lblStoreUserIcon.Location = new Point(5, 0);
            lblStoreUserIcon.Name = "lblStoreUserIcon";
            lblStoreUserIcon.Size = new Size(40, 42);
            lblStoreUserIcon.TabIndex = 1;
            lblStoreUserIcon.Text = "◉";
            lblStoreUserIcon.TextAlign = ContentAlignment.MiddleCenter;

            lblStoreUserName.BackColor = Color.Transparent;
            lblStoreUserName.Font = new Font("Segoe UI", 10.2F);
            lblStoreUserName.ForeColor = Color.White;
            lblStoreUserName.Location = new Point(55, 0);
            lblStoreUserName.Name = "lblStoreUserName";
            lblStoreUserName.Size = new Size(110, 42);
            lblStoreUserName.TabIndex = 0;
            lblStoreUserName.Text = "Guest";
            lblStoreUserName.TextAlign = ContentAlignment.MiddleLeft;

            btnStoreLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStoreLogout.BackColor = Color.FromArgb(65, 35, 45);
            btnStoreLogout.FlatAppearance.BorderSize = 0;
            btnStoreLogout.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(145, 55, 75);
            btnStoreLogout.FlatStyle = FlatStyle.Flat;
            btnStoreLogout.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnStoreLogout.ForeColor = Color.White;
            btnStoreLogout.Location = new Point(875, 11);
            btnStoreLogout.Name = "btnStoreLogout";
            btnStoreLogout.Size = new Size(105, 42);
            btnStoreLogout.TabIndex = 3;
            btnStoreLogout.Text = "Logout";
            btnStoreLogout.UseVisualStyleBackColor = false;
            btnStoreLogout.Cursor = Cursors.Hand;

            pnlStoreSearch.BackColor = Color.FromArgb(43, 29, 35);
            pnlStoreSearch.Controls.Add(lblStoreSearchIcon);
            pnlStoreSearch.Controls.Add(txtStoreSearch);
            pnlStoreSearch.Location = new Point(20, 11);
            pnlStoreSearch.Name = "pnlStoreSearch";
            pnlStoreSearch.Size = new Size(350, 42);
            pnlStoreSearch.TabIndex = 1;

            lblStoreSearchIcon.BackColor = Color.Transparent;
            lblStoreSearchIcon.Font = new Font("Segoe UI Symbol", 18F);
            lblStoreSearchIcon.ForeColor = Color.FromArgb(210, 175, 190);
            lblStoreSearchIcon.Location = new Point(5, 0);
            lblStoreSearchIcon.Name = "lblStoreSearchIcon";
            lblStoreSearchIcon.Size = new Size(40, 40);
            lblStoreSearchIcon.TabIndex = 1;
            lblStoreSearchIcon.Text = "⌕";
            lblStoreSearchIcon.TextAlign = ContentAlignment.MiddleCenter;

            txtStoreSearch.BackColor = Color.FromArgb(43, 29, 35);
            txtStoreSearch.BorderStyle = BorderStyle.None;
            txtStoreSearch.Font = new Font("Segoe UI", 10.8F);
            txtStoreSearch.ForeColor = Color.White;
            txtStoreSearch.Location = new Point(45, 10);
            txtStoreSearch.Name = "txtStoreSearch";
            txtStoreSearch.PlaceholderText = "Search games...";
            txtStoreSearch.Size = new Size(290, 24);
            txtStoreSearch.TabIndex = 0;

            lblStoreTitle.BackColor = Color.Transparent;
            lblStoreTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblStoreTitle.ForeColor = Color.White;
            lblStoreTitle.Location = new Point(20, 75);
            lblStoreTitle.Name = "lblStoreTitle";
            lblStoreTitle.Size = new Size(200, 45);
            lblStoreTitle.TabIndex = 2;
            lblStoreTitle.Text = "Game Store";
            lblStoreTitle.TextAlign = ContentAlignment.MiddleLeft;

            flpStoreCategories.AutoScroll = true;
            flpStoreCategories.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;
            flpStoreCategories.Controls.Add(btnAllGames);
            flpStoreCategories.Controls.Add(btnRPG);
            flpStoreCategories.Controls.Add(btnAction);
            flpStoreCategories.Controls.Add(btnAdventure);
            flpStoreCategories.Location = new Point(20, 130);
            flpStoreCategories.Name = "flpStoreCategories";
            flpStoreCategories.Size = new Size(1160, 45);
            flpStoreCategories.TabIndex = 3;
            flpStoreCategories.WrapContents = false;

            btnAllGames.BackColor = Color.FromArgb(105, 65, 160);
            btnAllGames.Cursor = Cursors.Hand;
            btnAllGames.FlatAppearance.BorderSize = 0;
            btnAllGames.FlatStyle = FlatStyle.Flat;
            btnAllGames.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnAllGames.ForeColor = Color.White;
            btnAllGames.Location = new Point(3, 3);
            btnAllGames.Name = "btnAllGames";
            btnAllGames.Size = new Size(110, 38);
            btnAllGames.TabIndex = 0;
            btnAllGames.Text = "All Games";
            btnAllGames.UseVisualStyleBackColor = false;

            btnRPG.BackColor = Color.FromArgb(35, 29, 43);
            btnRPG.Cursor = Cursors.Hand;
            btnRPG.FlatAppearance.BorderSize = 0;
            btnRPG.FlatStyle = FlatStyle.Flat;
            btnRPG.Font = new Font("Segoe UI", 10.2F);
            btnRPG.ForeColor = Color.FromArgb(220, 215, 230);
            btnRPG.Location = new Point(119, 3);
            btnRPG.Name = "btnRPG";
            btnRPG.Size = new Size(80, 38);
            btnRPG.TabIndex = 1;
            btnRPG.Text = "RPG";
            btnRPG.UseVisualStyleBackColor = false;

            btnAction.BackColor = Color.FromArgb(35, 29, 43);
            btnAction.Cursor = Cursors.Hand;
            btnAction.FlatAppearance.BorderSize = 0;
            btnAction.FlatStyle = FlatStyle.Flat;
            btnAction.Font = new Font("Segoe UI", 10.2F);
            btnAction.ForeColor = Color.FromArgb(220, 215, 230);
            btnAction.Location = new Point(205, 3);
            btnAction.Name = "btnAction";
            btnAction.Size = new Size(100, 38);
            btnAction.TabIndex = 2;
            btnAction.Text = "Action";
            btnAction.UseVisualStyleBackColor = false;

            btnAdventure.BackColor = Color.FromArgb(35, 29, 43);
            btnAdventure.Cursor = Cursors.Hand;
            btnAdventure.FlatAppearance.BorderSize = 0;
            btnAdventure.FlatStyle = FlatStyle.Flat;
            btnAdventure.Font = new Font("Segoe UI", 10.2F);
            btnAdventure.ForeColor = Color.FromArgb(220, 215, 230);
            btnAdventure.Location = new Point(311, 3);
            btnAdventure.Name = "btnAdventure";
            btnAdventure.Size = new Size(120, 38);
            btnAdventure.TabIndex = 3;
            btnAdventure.Text = "Adventure";
            btnAdventure.UseVisualStyleBackColor = false;

            flpStoreGames.AutoScroll = true;
            flpStoreGames.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;
            flpStoreGames.Location = new Point(20, 230);
            flpStoreGames.Name = "flpStoreGames";
            flpStoreGames.Size = new Size(1160, 650);
            flpStoreGames.TabIndex = 4;
            flpStoreGames.WrapContents = true;

            cmbStoreSort.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;
            cmbStoreSort.BackColor = Color.FromArgb(35, 29, 43);
            cmbStoreSort.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStoreSort.Font = new Font("Segoe UI", 10.2F);
            cmbStoreSort.ForeColor = Color.White;
            cmbStoreSort.FormattingEnabled = true;
            cmbStoreSort.Location = new Point(1010, 185);
            cmbStoreSort.Name = "cmbStoreSort";
            cmbStoreSort.Size = new Size(170, 31);
            cmbStoreSort.TabIndex = 5;
            cmbStoreSort.SelectedIndexChanged += cmbStoreSort_SelectedIndexChanged;

            lblAllGamesTitle.BackColor = Color.Transparent;
            lblAllGamesTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblAllGamesTitle.ForeColor = Color.White;
            lblAllGamesTitle.Location = new Point(20, 185);
            lblAllGamesTitle.Name = "lblAllGamesTitle";
            lblAllGamesTitle.Size = new Size(300, 35);
            lblAllGamesTitle.TabIndex = 6;
            lblAllGamesTitle.Text = "All Games";
            lblAllGamesTitle.TextAlign = ContentAlignment.MiddleLeft;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(15, 11, 22);
            Controls.Add(lblAllGamesTitle);
            Controls.Add(cmbStoreSort);
            Controls.Add(flpStoreGames);
            Controls.Add(flpStoreCategories);
            Controls.Add(lblStoreTitle);
            Controls.Add(pnlStoreHeader);
            Name = "StorePage";
            Size = new Size(1200, 900);
            Load += StorePage_Load;

            pnlStoreHeader.ResumeLayout(false);
            pnlStoreUser.ResumeLayout(false);
            pnlStoreSearch.ResumeLayout(false);
            pnlStoreSearch.PerformLayout();
            flpStoreCategories.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlStoreHeader;
        private Panel pnlStoreUser;
        private Label lblStoreUserIcon;
        private Label lblStoreUserName;
        private Button btnStoreLogout;
        private Panel pnlStoreSearch;
        private Label lblStoreSearchIcon;
        private TextBox txtStoreSearch;
        private Label lblStoreTitle;
        private FlowLayoutPanel flpStoreCategories;
        private Button btnAllGames;
        private HiddenScrollFlowLayoutPanel flpStoreGames;
        private Button btnRPG;
        private Button btnAction;
        private Button btnAdventure;
        private ComboBox cmbStoreSort;
        private Label lblAllGamesTitle;
    }
}
