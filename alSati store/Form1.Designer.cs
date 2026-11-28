
namespace alSati_store
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlSidebar = new Panel();
            btnAddGame = new Button();
            btnStore = new Button();
            btnLibrary = new Button();
            btnWishlist = new Button();
            btnHome = new Button();
            lblLogo = new Label();

            pnlMain = new Panel();

            pnlTopBar = new Panel();
            pnlUser = new Panel();
            lblUserIcon = new Label();
            lblUserName = new Label();
            pnlSearch = new Panel();
            lblSearchIcon = new Label();
            txtSearch = new TextBox();

            pnlFeatured = new Panel();
            picFeaturedGame = new PictureBox();
            pnlGameInfo = new Panel();
            lblGameTitle = new Label();
            lblGameRating = new Label();
            lblGamePrice = new Label();

            pnlTagRPG = new Panel();
            lblTagRPG = new Label();
            pnlTagAction = new Panel();
            lblTagAction = new Label();
            pnlTagAdventure = new Panel();
            lblTagAdventure = new Label();

            pnlSlideIndicators = new Panel();
            lblIndicator1 = new Label();
            lblIndicator2 = new Label();
            lblIndicator3 = new Label();
            lblIndicator4 = new Label();

            pnlDiscover = new Panel();
            lblDiscoverTitle = new Label();
            flpGames = new HiddenScrollFlowLayoutPanel();
            btnDiscoverLeft = new Button();
            btnDiscoverRight = new Button();

            pnlRecommended = new Panel();
            lblRecommendedTitle = new Label();
            flpRecommended = new HiddenScrollFlowLayoutPanel();
            btnRecommendedLeft = new Button();
            btnRecommendedRight = new Button();

            pnlRecommendedCard1 = new Panel();
            pnlRecommendedCard2 = new Panel();
            pnlRecommendedCard3 = new Panel();
            pnlRecommendedCard4 = new Panel();

            picRecommendedCover1 = new PictureBox();
            picRecommendedCover2 = new PictureBox();
            picRecommendedCover3 = new PictureBox();
            picRecommendedCover4 = new PictureBox();

            lblRecommendedName1 = new Label();
            lblRecommendedName2 = new Label();
            lblRecommendedName3 = new Label();
            lblRecommendedName4 = new Label();

            lblRecommendedRating1 = new Label();
            lblRecommendedRating2 = new Label();
            lblRecommendedRating3 = new Label();
            lblRecommendedRating4 = new Label();

            lblRecommendedPrice1 = new Label();
            lblRecommendedPrice2 = new Label();
            lblRecommendedPrice3 = new Label();
            lblRecommendedPrice4 = new Label();

            lblCardGamePrice = new Label();
            lblCardGameRating = new Label();
            lblCardGameName = new Label();
            picGameCover = new PictureBox();

            lblCardGamePrice2 = new Label();
            lblCardGameRating2 = new Label();
            lblCardGameName2 = new Label();
            picGameCover2 = new PictureBox();

            lblCardGamePrice3 = new Label();
            lblCardGameRating3 = new Label();
            lblCardGameName3 = new Label();
            picGameCover3 = new PictureBox();

            lblCardGamePrice4 = new Label();
            lblCardGameRating4 = new Label();
            lblCardGameName4 = new Label();
            picGameCover4 = new PictureBox();

            oracleCommandBuilder1 = new Oracle.ManagedDataAccess.Client.OracleCommandBuilder();

            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlTopBar.SuspendLayout();
            pnlUser.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlFeatured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picFeaturedGame).BeginInit();
            pnlGameInfo.SuspendLayout();
            pnlTagRPG.SuspendLayout();
            pnlTagAction.SuspendLayout();
            pnlTagAdventure.SuspendLayout();
            pnlSlideIndicators.SuspendLayout();
            pnlDiscover.SuspendLayout();
            flpGames.SuspendLayout();
            pnlRecommended.SuspendLayout();
            flpRecommended.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)picRecommendedCover1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picRecommendedCover2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picRecommendedCover3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picRecommendedCover4).BeginInit();

            ((System.ComponentModel.ISupportInitialize)picGameCover).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picGameCover2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picGameCover3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picGameCover4).BeginInit();

            SuspendLayout();

            // pnlSidebar
            pnlSidebar.BackColor = Color.FromArgb(29, 18, 25);
            pnlSidebar.Controls.Add(btnAddGame);
            pnlSidebar.Controls.Add(btnStore);
            pnlSidebar.Controls.Add(btnLibrary);
            pnlSidebar.Controls.Add(btnWishlist);
            pnlSidebar.Controls.Add(btnHome);
            pnlSidebar.Controls.Add(lblLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(250, 900);
            pnlSidebar.TabIndex = 0;

            // lblLogo
            lblLogo.BackColor = Color.Transparent;
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Height = 105;
            lblLogo.Name = "lblLogo";
            lblLogo.TabIndex = 1;
            lblLogo.Text = "alSati Store";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            // btnHome
            ConfigureNavigationButton(btnHome, "⌂   Home", 2);
            btnHome.BackColor = Color.FromArgb(160, 65, 105);
            btnHome.ForeColor = Color.White;
            btnHome.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnHome.Click += btnHome_Click;

            // btnWishlist
            ConfigureNavigationButton(btnWishlist, "♡   Wishlist", 3);
            btnWishlist.Click += btnWishlist_Click;

            // btnLibrary
            ConfigureNavigationButton(btnLibrary, "▣   Library", 4);
            btnLibrary.Click += btnLibrary_Click;

            // btnStore
            ConfigureNavigationButton(btnStore, "▦   Store", 5);
            btnStore.Click += btnStore_Click;

            // btnAddGame
            ConfigureNavigationButton(btnAddGame, "🎮   Add Game", 6);
            btnAddGame.Visible = false;
            btnAddGame.Click += btnAddGame_Click;

            // pnlMain
            pnlMain.BackColor = Color.FromArgb(22, 11, 15);
            pnlMain.Controls.Add(pnlRecommended);
            pnlMain.Controls.Add(pnlDiscover);
            pnlMain.Controls.Add(pnlFeatured);
            pnlMain.Controls.Add(pnlTopBar);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(24, 18, 24, 18);
            pnlMain.TabIndex = 1;
            pnlMain.AutoScroll = true;

            // pnlTopBar
            pnlTopBar.BackColor = Color.Transparent;
            pnlTopBar.Controls.Add(pnlUser);
            pnlTopBar.Controls.Add(pnlSearch);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Height = 65;
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.TabIndex = 0;

            // pnlSearch
            pnlSearch.BackColor = Color.FromArgb(43, 29, 35);
            pnlSearch.Controls.Add(lblSearchIcon);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Location = new Point(5, 10);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(370, 44);
            pnlSearch.TabIndex = 1;

            // lblSearchIcon
            lblSearchIcon.BackColor = Color.Transparent;
            lblSearchIcon.Font = new Font("Segoe UI Symbol", 19F);
            lblSearchIcon.ForeColor = Color.FromArgb(210, 175, 190);
            lblSearchIcon.Location = new Point(5, 0);
            lblSearchIcon.Name = "lblSearchIcon";
            lblSearchIcon.Size = new Size(40, 42);
            lblSearchIcon.TabIndex = 1;
            lblSearchIcon.Text = "⌕";
            lblSearchIcon.TextAlign = ContentAlignment.MiddleCenter;

            // txtSearch
            txtSearch.BackColor = Color.FromArgb(43, 29, 35);
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.ForeColor = Color.White;
            txtSearch.Location = new Point(48, 11);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search games...";
            txtSearch.Size = new Size(305, 25);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += textBox1_TextChanged;

            // pnlUser
            pnlUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlUser.BackColor = Color.FromArgb(43, 29, 35);
            pnlUser.Controls.Add(lblUserIcon);
            pnlUser.Controls.Add(lblUserName);
            pnlUser.Location = new Point(650, 10);
            pnlUser.Name = "pnlUser";
            pnlUser.Size = new Size(190, 44);
            pnlUser.TabIndex = 2;

            // lblUserIcon
            lblUserIcon.BackColor = Color.Transparent;
            lblUserIcon.Font = new Font("Segoe UI Symbol", 17F);
            lblUserIcon.ForeColor = Color.FromArgb(210, 175, 190);
            lblUserIcon.Location = new Point(5, 0);
            lblUserIcon.Name = "lblUserIcon";
            lblUserIcon.Size = new Size(40, 44);
            lblUserIcon.TabIndex = 1;
            lblUserIcon.Text = "◉";
            lblUserIcon.TextAlign = ContentAlignment.MiddleCenter;

            // lblUserName
            lblUserName.BackColor = Color.Transparent;
            lblUserName.Font = new Font("Segoe UI", 10.5F);
            lblUserName.ForeColor = Color.White;
            lblUserName.Location = new Point(52, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(125, 44);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "Guest";
            lblUserName.TextAlign = ContentAlignment.MiddleLeft;

            // pnlFeatured
            pnlFeatured.BackColor = Color.FromArgb(34, 19, 25);
            pnlFeatured.Controls.Add(pnlSlideIndicators);
            pnlFeatured.Controls.Add(pnlGameInfo);
            pnlFeatured.Controls.Add(picFeaturedGame);
            pnlFeatured.Dock = DockStyle.Top;
            pnlFeatured.Height = 405;
            pnlFeatured.Name = "pnlFeatured";
            pnlFeatured.TabIndex = 1;
            pnlFeatured.Margin = new Padding(0, 0, 0, 18);

            // picFeaturedGame
            picFeaturedGame.BackColor = Color.FromArgb(25, 19, 34);
            picFeaturedGame.Dock = DockStyle.Fill;
            picFeaturedGame.Name = "picFeaturedGame";
            picFeaturedGame.SizeMode = PictureBoxSizeMode.Zoom;
            picFeaturedGame.TabIndex = 0;
            picFeaturedGame.TabStop = false;

            // pnlGameInfo
            pnlGameInfo.BackColor = Color.FromArgb(225, 24, 15, 22);
            pnlGameInfo.Controls.Add(lblGameTitle);
            pnlGameInfo.Controls.Add(lblGameRating);
            pnlGameInfo.Controls.Add(lblGamePrice);
            pnlGameInfo.Controls.Add(pnlTagRPG);
            pnlGameInfo.Controls.Add(pnlTagAction);
            pnlGameInfo.Controls.Add(pnlTagAdventure);
            pnlGameInfo.Dock = DockStyle.Top;
            pnlGameInfo.Height = 135;
            pnlGameInfo.Name = "pnlGameInfo";
            pnlGameInfo.TabIndex = 2;

            // lblGameTitle
            lblGameTitle.BackColor = Color.Transparent;
            lblGameTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblGameTitle.ForeColor = Color.White;
            lblGameTitle.Location = new Point(28, 12);
            lblGameTitle.Name = "lblGameTitle";
            lblGameTitle.Size = new Size(650, 48);
            lblGameTitle.TabIndex = 0;
            lblGameTitle.Text = "The Witcher 3: Wild Hunt";
            lblGameTitle.TextAlign = ContentAlignment.MiddleLeft;

            // lblGameRating
            lblGameRating.BackColor = Color.Transparent;
            lblGameRating.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblGameRating.ForeColor = Color.FromArgb(255, 190, 90);
            lblGameRating.Location = new Point(30, 65);
            lblGameRating.Name = "lblGameRating";
            lblGameRating.Size = new Size(180, 28);
            lblGameRating.TabIndex = 1;
            lblGameRating.Text = "★  0.0 / 5";

            // lblGamePrice
            lblGamePrice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblGamePrice.BackColor = Color.Transparent;
            lblGamePrice.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblGamePrice.ForeColor = Color.White;
            lblGamePrice.Location = new Point(650, 45);
            lblGamePrice.Name = "lblGamePrice";
            lblGamePrice.Size = new Size(180, 48);
            lblGamePrice.TabIndex = 4;
            lblGamePrice.Text = "$29.99";
            lblGamePrice.TextAlign = ContentAlignment.MiddleRight;

            // pnlTagRPG
            ConfigureTagPanel(pnlTagRPG, lblTagRPG, "RPG",
                Color.FromArgb(109, 49, 73), 30, 99, 65);

            // pnlTagAction
            ConfigureTagPanel(pnlTagAction, lblTagAction, "Action",
                Color.FromArgb(104, 76, 36), 105, 99, 70);

            // pnlTagAdventure
            ConfigureTagPanel(pnlTagAdventure, lblTagAdventure, "Adventure",
                Color.FromArgb(72, 91, 49), 185, 99, 100);

            // pnlSlideIndicators
            pnlSlideIndicators.Anchor = AnchorStyles.Bottom;
            pnlSlideIndicators.BackColor = Color.Transparent;
            pnlSlideIndicators.Controls.Add(lblIndicator1);
            pnlSlideIndicators.Controls.Add(lblIndicator2);
            pnlSlideIndicators.Controls.Add(lblIndicator3);
            pnlSlideIndicators.Controls.Add(lblIndicator4);
            pnlSlideIndicators.Location = new Point(350, 375);
            pnlSlideIndicators.Name = "pnlSlideIndicators";
            pnlSlideIndicators.Size = new Size(166, 20);
            pnlSlideIndicators.TabIndex = 5;

            // Indicators
            ConfigureIndicator(lblIndicator1, 0, true);
            ConfigureIndicator(lblIndicator2, 42, false);
            ConfigureIndicator(lblIndicator3, 84, false);
            ConfigureIndicator(lblIndicator4, 126, false);

            // pnlDiscover
            pnlDiscover.BackColor = Color.Transparent;
            pnlDiscover.Controls.Add(lblDiscoverTitle);
            pnlDiscover.Controls.Add(flpGames);
            pnlDiscover.Controls.Add(btnDiscoverLeft);
            pnlDiscover.Controls.Add(btnDiscoverRight);
            pnlDiscover.Dock = DockStyle.Top;
            pnlDiscover.Height = 480;
            pnlDiscover.Name = "pnlDiscover";
            pnlDiscover.TabIndex = 2;

            // lblDiscoverTitle
            lblDiscoverTitle.BackColor = Color.Transparent;
            lblDiscoverTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblDiscoverTitle.ForeColor = Color.White;
            lblDiscoverTitle.Location = new Point(5, 0);
            lblDiscoverTitle.Name = "lblDiscoverTitle";
            lblDiscoverTitle.Size = new Size(350, 42);
            lblDiscoverTitle.TabIndex = 0;
            lblDiscoverTitle.Text = "Discover Games";
            lblDiscoverTitle.TextAlign = ContentAlignment.MiddleLeft;

            // flpGames
            flpGames.AutoScroll = true;
            flpGames.BackColor = Color.Transparent;
            flpGames.Dock = DockStyle.Bottom;
            flpGames.FlowDirection = FlowDirection.LeftToRight;
            flpGames.Location = new Point(0, 45);
            flpGames.Name = "flpGames";
            flpGames.Padding = new Padding(5);
            flpGames.Size = new Size(850, 435);
            flpGames.TabIndex = 1;
            flpGames.WrapContents = false;

            // btnDiscoverLeft
            ConfigureArrowButton(btnDiscoverLeft, "btnDiscoverLeft", "‹", 0);

            // btnDiscoverRight
            ConfigureArrowButton(btnDiscoverRight, "btnDiscoverRight", "›", 0);

            // pnlRecommended
            pnlRecommended.BackColor = Color.Transparent;
            pnlRecommended.Controls.Add(lblRecommendedTitle);
            pnlRecommended.Controls.Add(flpRecommended);
            pnlRecommended.Controls.Add(btnRecommendedLeft);
            pnlRecommended.Controls.Add(btnRecommendedRight);
            pnlRecommended.Dock = DockStyle.Top;
            pnlRecommended.Height = 480;
            pnlRecommended.Name = "pnlRecommended";
            pnlRecommended.TabIndex = 3;

            // lblRecommendedTitle
            lblRecommendedTitle.BackColor = Color.Transparent;
            lblRecommendedTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblRecommendedTitle.ForeColor = Color.White;
            lblRecommendedTitle.Location = new Point(5, 0);
            lblRecommendedTitle.Name = "lblRecommendedTitle";
            lblRecommendedTitle.Size = new Size(400, 42);
            lblRecommendedTitle.TabIndex = 0;
            lblRecommendedTitle.Text = "Recommended for You";
            lblRecommendedTitle.TextAlign = ContentAlignment.MiddleLeft;

            // flpRecommended
            flpRecommended.AutoScroll = true;
            flpRecommended.BackColor = Color.Transparent;
            flpRecommended.Dock = DockStyle.Bottom;
            flpRecommended.FlowDirection = FlowDirection.LeftToRight;
            flpRecommended.Location = new Point(0, 45);
            flpRecommended.Name = "flpRecommended";
            flpRecommended.Padding = new Padding(5);
            flpRecommended.Size = new Size(850, 435);
            flpRecommended.TabIndex = 1;
            flpRecommended.WrapContents = false;

            // Legacy recommended card controls
            ConfigureLegacyCard(pnlRecommendedCard1, "pnlRecommendedCard1", 0);
            ConfigureLegacyCard(pnlRecommendedCard2, "pnlRecommendedCard2", 1);
            ConfigureLegacyCard(pnlRecommendedCard3, "pnlRecommendedCard3", 2);
            ConfigureLegacyCard(pnlRecommendedCard4, "pnlRecommendedCard4", 3);

            ConfigureLegacyPicture(picRecommendedCover1, "picRecommendedCover1");
            ConfigureLegacyPicture(picRecommendedCover2, "picRecommendedCover2");
            ConfigureLegacyPicture(picRecommendedCover3, "picRecommendedCover3");
            ConfigureLegacyPicture(picRecommendedCover4, "picRecommendedCover4");

            ConfigureLegacyLabel(lblRecommendedName1, "lblRecommendedName1");
            ConfigureLegacyLabel(lblRecommendedName2, "lblRecommendedName2");
            ConfigureLegacyLabel(lblRecommendedName3, "lblRecommendedName3");
            ConfigureLegacyLabel(lblRecommendedName4, "lblRecommendedName4");

            ConfigureLegacyLabel(lblRecommendedRating1, "lblRecommendedRating1");
            ConfigureLegacyLabel(lblRecommendedRating2, "lblRecommendedRating2");
            ConfigureLegacyLabel(lblRecommendedRating3, "lblRecommendedRating3");
            ConfigureLegacyLabel(lblRecommendedRating4, "lblRecommendedRating4");

            ConfigureLegacyLabel(lblRecommendedPrice1, "lblRecommendedPrice1");
            ConfigureLegacyLabel(lblRecommendedPrice2, "lblRecommendedPrice2");
            ConfigureLegacyLabel(lblRecommendedPrice3, "lblRecommendedPrice3");
            ConfigureLegacyLabel(lblRecommendedPrice4, "lblRecommendedPrice4");

            // btnRecommendedLeft
            ConfigureArrowButton(btnRecommendedLeft, "btnRecommendedLeft", "‹", 0);

            // btnRecommendedRight
            ConfigureArrowButton(btnRecommendedRight, "btnRecommendedRight", "›", 0);

            // Legacy Discover controls
            ConfigureLegacyLabel(lblCardGameName, "lblCardGameName");
            ConfigureLegacyLabel(lblCardGameRating, "lblCardGameRating");
            ConfigureLegacyLabel(lblCardGamePrice, "lblCardGamePrice");
            ConfigureLegacyPicture(picGameCover, "picGameCover");

            ConfigureLegacyLabel(lblCardGameName2, "lblCardGameName2");
            ConfigureLegacyLabel(lblCardGameRating2, "lblCardGameRating2");
            ConfigureLegacyLabel(lblCardGamePrice2, "lblCardGamePrice2");
            ConfigureLegacyPicture(picGameCover2, "picGameCover2");

            ConfigureLegacyLabel(lblCardGameName3, "lblCardGameName3");
            ConfigureLegacyLabel(lblCardGameRating3, "lblCardGameRating3");
            ConfigureLegacyLabel(lblCardGamePrice3, "lblCardGamePrice3");
            ConfigureLegacyPicture(picGameCover3, "picGameCover3");

            ConfigureLegacyLabel(lblCardGameName4, "lblCardGameName4");
            ConfigureLegacyLabel(lblCardGameRating4, "lblCardGameRating4");
            ConfigureLegacyLabel(lblCardGamePrice4, "lblCardGamePrice4");
            ConfigureLegacyPicture(picGameCover4, "picGameCover4");

            // MainForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 11, 22);
            ClientSize = new Size(1440, 900);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            MinimumSize = new Size(1100, 700);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "alSati Store";
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;

            pnlSidebar.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            pnlTopBar.ResumeLayout(false);
            pnlUser.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();

            pnlFeatured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picFeaturedGame).EndInit();
            pnlGameInfo.ResumeLayout(false);
            pnlTagRPG.ResumeLayout(false);
            pnlTagAction.ResumeLayout(false);
            pnlTagAdventure.ResumeLayout(false);
            pnlSlideIndicators.ResumeLayout(false);

            pnlDiscover.ResumeLayout(false);
            flpGames.ResumeLayout(false);

            pnlRecommended.ResumeLayout(false);
            flpRecommended.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)picRecommendedCover1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picRecommendedCover2).EndInit();
            ((System.ComponentModel.ISupportInitialize)picRecommendedCover3).EndInit();
            ((System.ComponentModel.ISupportInitialize)picRecommendedCover4).EndInit();

            ((System.ComponentModel.ISupportInitialize)picGameCover).EndInit();
            ((System.ComponentModel.ISupportInitialize)picGameCover2).EndInit();
            ((System.ComponentModel.ISupportInitialize)picGameCover3).EndInit();
            ((System.ComponentModel.ISupportInitialize)picGameCover4).EndInit();

            ResumeLayout(false);
        }

        private void ConfigureNavigationButton(Button button, string text, int tabIndex)
        {
            button.BackColor = Color.FromArgb(29, 18, 25);
            button.Cursor = Cursors.Hand;
            button.Dock = DockStyle.Top;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(75, 42, 58);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 11F);
            button.ForeColor = Color.FromArgb(230, 215, 220);
            button.Height = 58;
            button.Name = "btn" + text.Replace(" ", "").Replace("⌂", "").Replace("♡", "").Replace("▣", "").Replace("▦", "").Replace("🎮", "");
            button.TabIndex = tabIndex;
            button.Text = text;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.UseVisualStyleBackColor = false;
        }

        private void ConfigureTagPanel(
            Panel panel,
            Label label,
            string text,
            Color color,
            int x,
            int y,
            int width)
        {
            panel.BackColor = color;
            panel.Controls.Add(label);
            panel.Location = new Point(x, y);
            panel.Name = "pnlTag" + text;
            panel.Size = new Size(width, 25);
            panel.TabIndex = 1;

            label.BackColor = Color.Transparent;
            label.Dock = DockStyle.Fill;
            label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label.ForeColor = Color.White;
            label.Name = "lblTag" + text;
            label.Text = text;
            label.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void ConfigureIndicator(Label indicator, int x, bool active)
        {
            indicator.BackColor = active
                ? Color.FromArgb(190, 75, 125)
                : Color.FromArgb(150, 140, 150);

            indicator.Location = new Point(x, 6);
            indicator.Name = "lblIndicator" + (x / 42 + 1);
            indicator.Size = active ? new Size(30, 8) : new Size(9, 8);
            indicator.TabIndex = 0;
            indicator.Text = "";
        }

        private void ConfigureArrowButton(
            Button button,
            string controlName,
            string symbol,
            int x)
        {
            button.BackColor = Color.FromArgb(55, 38, 48);
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(125, 65, 100);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            button.ForeColor = Color.White;
            button.Location = new Point(x, 165);
            button.Name = controlName;
            button.Size = new Size(42, 58);
            button.TabIndex = 10;
            button.Text = symbol;
            button.UseVisualStyleBackColor = false;
            button.Visible = false;
            button.BringToFront();
        }

        private void ConfigureLegacyCard(Panel card, string name, int index)
        {
            card.BackColor = Color.FromArgb(35, 29, 43);
            card.Name = name;
            card.Size = new Size(350, 400);
            card.TabIndex = index;
        }

        private void ConfigureLegacyPicture(PictureBox picture, string name)
        {
            picture.BackColor = Color.FromArgb(25, 19, 34);
            picture.Name = name;
            picture.Size = new Size(350, 260);
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.TabStop = false;
        }

        private void ConfigureLegacyLabel(Label label, string name)
        {
            label.BackColor = Color.Transparent;
            label.ForeColor = Color.White;
            label.Name = name;
            label.Size = new Size(320, 28);
        }

        private void ConfigureGameCard(
            Panel card, PictureBox cover, Label name,
            Label rating, Label price, string cardName,
            string coverName, string nameLabel, string ratingLabel,
            string priceLabel, string gameTitle, int index)
        {
            card.BackColor = Color.FromArgb(35, 29, 43);
            card.Controls.Add(price);
            card.Controls.Add(rating);
            card.Controls.Add(name);
            card.Controls.Add(cover);
            card.Cursor = Cursors.Hand;
            card.Margin = new Padding(8);
            card.Name = cardName;
            card.Size = new Size(350, 400);
            card.TabIndex = index;

            price.BackColor = Color.Transparent;
            price.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            price.ForeColor = Color.White;
            price.Location = new Point(15, 355);
            price.Name = priceLabel;
            price.Size = new Size(320, 28);
            price.Text = "$29.99";
            price.TextAlign = ContentAlignment.MiddleLeft;

            rating.BackColor = Color.Transparent;
            rating.Font = new Font("Segoe UI", 9.5F);
            rating.ForeColor = Color.FromArgb(255, 190, 90);
            rating.Location = new Point(15, 320);
            rating.Name = ratingLabel;
            rating.Size = new Size(320, 28);
            rating.Text = "★  4.8 / 5";
            rating.TextAlign = ContentAlignment.MiddleLeft;

            name.BackColor = Color.Transparent;
            name.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            name.ForeColor = Color.White;
            name.Location = new Point(15, 270);
            name.Name = nameLabel;
            name.Size = new Size(320, 42);
            name.Text = gameTitle;
            name.TextAlign = ContentAlignment.MiddleLeft;

            cover.BackColor = Color.FromArgb(25, 19, 34);
            cover.Location = new Point(0, 0);
            cover.Name = coverName;
            cover.Size = new Size(350, 260);
            cover.SizeMode = PictureBoxSizeMode.Zoom;
            cover.TabStop = false;
        }

        private void ConfigureRecommendedCard(
            Panel card, PictureBox cover, Label name,
            Label rating, Label price, string cardName,
            string coverName, string nameLabel, string ratingLabel,
            string priceLabel, int index)
        {
            ConfigureGameCard(
                card, cover, name, rating, price,
                cardName, coverName, nameLabel, ratingLabel,
                priceLabel, "Recommended Game", index);
        }

        #endregion

        private Panel pnlSidebar;
        private Label lblLogo;
        private Button btnStore;
        private Button btnLibrary;
        private Button btnWishlist;
        private Button btnHome;
        private Panel pnlMain;

        private Panel pnlDiscover;
        private Label lblDiscoverTitle;
        private HiddenScrollFlowLayoutPanel flpGames;
        private Label lblCardGamePrice;
        private Label lblCardGameRating;
        private Label lblCardGameName;
        private PictureBox picGameCover;
        private Label lblCardGamePrice2;
        private Label lblCardGameRating2;
        private Label lblCardGameName2;
        private PictureBox picGameCover2;
        private Label lblCardGamePrice3;
        private Label lblCardGameRating3;
        private Label lblCardGameName3;
        private PictureBox picGameCover3;
        private Label lblCardGamePrice4;
        private Label lblCardGameRating4;
        private Label lblCardGameName4;
        private PictureBox picGameCover4;

        private Button btnDiscoverLeft;
        private Button btnDiscoverRight;

        private Panel pnlRecommended;
        private Label lblRecommendedTitle;
        private HiddenScrollFlowLayoutPanel flpRecommended;

        private Panel pnlRecommendedCard1;
        private PictureBox picRecommendedCover1;
        private Label lblRecommendedName1;
        private Label lblRecommendedRating1;
        private Label lblRecommendedPrice1;

        private Panel pnlRecommendedCard2;
        private PictureBox picRecommendedCover2;
        private Label lblRecommendedName2;
        private Label lblRecommendedRating2;
        private Label lblRecommendedPrice2;

        private Panel pnlRecommendedCard3;
        private PictureBox picRecommendedCover3;
        private Label lblRecommendedName3;
        private Label lblRecommendedRating3;
        private Label lblRecommendedPrice3;

        private Panel pnlRecommendedCard4;
        private PictureBox picRecommendedCover4;
        private Label lblRecommendedName4;
        private Label lblRecommendedRating4;
        private Label lblRecommendedPrice4;

        private Button btnRecommendedLeft;
        private Button btnRecommendedRight;

        private Panel pnlFeatured;
        private Panel pnlGameInfo;
        private Panel pnlSlideIndicators;
        private Label lblIndicator4;
        private Label lblIndicator3;
        private Label lblIndicator2;
        private Label lblIndicator1;
        private Label lblGameTitle;
        private Label lblGameRating;
        private Label lblGamePrice;

        private Panel pnlTagAdventure;
        private Label lblTagAdventure;
        private Panel pnlTagAction;
        private Label lblTagAction;
        private Panel pnlTagRPG;
        private Label lblTagRPG;

        private PictureBox picFeaturedGame;

        private Panel pnlTopBar;
        private Panel pnlUser;
        private Label lblUserIcon;
        private Label lblUserName;
        private Panel pnlSearch;
        private Label lblSearchIcon;
        private TextBox txtSearch;

        private Button btnAddGame;
        private Oracle.ManagedDataAccess.Client.OracleCommandBuilder oracleCommandBuilder1;
    }
}
