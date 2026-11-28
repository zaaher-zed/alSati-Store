
namespace alSati_store
{
    partial class WishlistPage
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
            pnlWishlistHeader = new Panel();
            pnlWishlistUser = new Panel();
            lblWishlistUserIcon = new Label();
            lblWishlistUserName = new Label();
            btnWishlistLogout = new Button();
            pnlWishlistSearch = new Panel();
            lblWishlistSearchIcon = new Label();
            txtWishlistSearch = new TextBox();
            lblWishlistTitle = new Label();
            flpWishlistGames = new HiddenScrollFlowLayoutPanel();

            pnlWishlistHeader.SuspendLayout();
            pnlWishlistUser.SuspendLayout();
            pnlWishlistSearch.SuspendLayout();
            SuspendLayout();

            // pnlWishlistHeader
            pnlWishlistHeader.Controls.Add(pnlWishlistUser);
            pnlWishlistHeader.Controls.Add(btnWishlistLogout);
            pnlWishlistHeader.Controls.Add(pnlWishlistSearch);
            pnlWishlistHeader.Dock = DockStyle.Top;
            pnlWishlistHeader.Location = new Point(0, 0);
            pnlWishlistHeader.Name = "pnlWishlistHeader";
            pnlWishlistHeader.Size = new Size(1200, 65);
            pnlWishlistHeader.TabIndex = 3;

            // pnlWishlistUser
            pnlWishlistUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlWishlistUser.BackColor = Color.FromArgb(43, 29, 35);
            pnlWishlistUser.Controls.Add(lblWishlistUserIcon);
            pnlWishlistUser.Controls.Add(lblWishlistUserName);
            pnlWishlistUser.Location = new Point(950, 11);
            pnlWishlistUser.Name = "pnlWishlistUser";
            pnlWishlistUser.Size = new Size(180, 42);
            pnlWishlistUser.TabIndex = 2;

            // lblWishlistUserIcon
            lblWishlistUserIcon.BackColor = Color.Transparent;
            lblWishlistUserIcon.Font = new Font("Segoe UI Symbol", 16.8F);
            lblWishlistUserIcon.ForeColor = Color.FromArgb(210, 175, 190);
            lblWishlistUserIcon.Location = new Point(5, 0);
            lblWishlistUserIcon.Name = "lblWishlistUserIcon";
            lblWishlistUserIcon.Size = new Size(40, 42);
            lblWishlistUserIcon.TabIndex = 1;
            lblWishlistUserIcon.Text = "◉";
            lblWishlistUserIcon.TextAlign = ContentAlignment.MiddleCenter;

            // lblWishlistUserName
            lblWishlistUserName.BackColor = Color.Transparent;
            lblWishlistUserName.Font = new Font("Segoe UI", 10.2F);
            lblWishlistUserName.ForeColor = Color.White;
            lblWishlistUserName.Location = new Point(55, 0);
            lblWishlistUserName.Name = "lblWishlistUserName";
            lblWishlistUserName.Size = new Size(110, 42);
            lblWishlistUserName.TabIndex = 0;
            lblWishlistUserName.Text = "Guest";
            lblWishlistUserName.TextAlign = ContentAlignment.MiddleLeft;

            // btnWishlistLogout
            btnWishlistLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnWishlistLogout.BackColor = Color.FromArgb(65, 35, 45);
            btnWishlistLogout.FlatAppearance.BorderSize = 0;
            btnWishlistLogout.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(145, 55, 75);
            btnWishlistLogout.FlatStyle = FlatStyle.Flat;
            btnWishlistLogout.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnWishlistLogout.ForeColor = Color.White;
            btnWishlistLogout.Location = new Point(830, 11);
            btnWishlistLogout.Name = "btnWishlistLogout";
            btnWishlistLogout.Size = new Size(105, 42);
            btnWishlistLogout.TabIndex = 3;
            btnWishlistLogout.Text = "Logout";
            btnWishlistLogout.UseVisualStyleBackColor = false;
            btnWishlistLogout.Cursor = Cursors.Hand;

            // pnlWishlistSearch
            pnlWishlistSearch.BackColor = Color.FromArgb(43, 29, 35);
            pnlWishlistSearch.Controls.Add(lblWishlistSearchIcon);
            pnlWishlistSearch.Controls.Add(txtWishlistSearch);
            pnlWishlistSearch.Location = new Point(20, 11);
            pnlWishlistSearch.Name = "pnlWishlistSearch";
            pnlWishlistSearch.Size = new Size(350, 42);
            pnlWishlistSearch.TabIndex = 1;

            // lblWishlistSearchIcon
            lblWishlistSearchIcon.BackColor = Color.Transparent;
            lblWishlistSearchIcon.Font = new Font("Segoe UI Symbol", 18F);
            lblWishlistSearchIcon.ForeColor = Color.FromArgb(210, 175, 190);
            lblWishlistSearchIcon.Location = new Point(5, 0);
            lblWishlistSearchIcon.Name = "lblWishlistSearchIcon";
            lblWishlistSearchIcon.Size = new Size(40, 40);
            lblWishlistSearchIcon.TabIndex = 1;
            lblWishlistSearchIcon.Text = "⌕";
            lblWishlistSearchIcon.TextAlign = ContentAlignment.MiddleCenter;

            // txtWishlistSearch
            txtWishlistSearch.BackColor = Color.FromArgb(43, 29, 35);
            txtWishlistSearch.BorderStyle = BorderStyle.None;
            txtWishlistSearch.Font = new Font("Segoe UI", 10.8F);
            txtWishlistSearch.ForeColor = Color.White;
            txtWishlistSearch.Location = new Point(45, 10);
            txtWishlistSearch.Name = "txtWishlistSearch";
            txtWishlistSearch.PlaceholderText = "Search games...";
            txtWishlistSearch.Size = new Size(290, 24);
            txtWishlistSearch.TabIndex = 0;

            // lblWishlistTitle
            lblWishlistTitle.BackColor = Color.Transparent;
            lblWishlistTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblWishlistTitle.ForeColor = Color.White;
            lblWishlistTitle.Location = new Point(20, 75);
            lblWishlistTitle.Name = "lblWishlistTitle";
            lblWishlistTitle.Size = new Size(250, 45);
            lblWishlistTitle.TabIndex = 4;
            lblWishlistTitle.Text = "My Wishlist";
            lblWishlistTitle.TextAlign = ContentAlignment.MiddleLeft;

            // flpWishlistGames
            flpWishlistGames.AutoScroll = true;
            flpWishlistGames.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;
            flpWishlistGames.Location = new Point(20, 140);
            flpWishlistGames.Name = "flpWishlistGames";
            flpWishlistGames.Size = new Size(1200, 600);
            flpWishlistGames.TabIndex = 6;
            flpWishlistGames.WrapContents = true;

            // WishlistPage
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = false;
            BackColor = Color.FromArgb(15, 11, 22);
            Controls.Add(flpWishlistGames);
            Controls.Add(lblWishlistTitle);
            Controls.Add(pnlWishlistHeader);
            Name = "WishlistPage";
            Size = new Size(1200, 700);

            pnlWishlistHeader.ResumeLayout(false);
            pnlWishlistUser.ResumeLayout(false);
            pnlWishlistSearch.ResumeLayout(false);
            pnlWishlistSearch.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlWishlistHeader;
        private Panel pnlWishlistUser;
        private Label lblWishlistUserIcon;
        private Label lblWishlistUserName;
        private Button btnWishlistLogout;
        private Panel pnlWishlistSearch;
        private Label lblWishlistSearchIcon;
        private TextBox txtWishlistSearch;
        private Label lblWishlistTitle;
        private HiddenScrollFlowLayoutPanel flpWishlistGames;
    }
}
