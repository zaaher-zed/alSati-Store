
namespace alSati_store
{
    partial class AddGamePage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            if (disposing)
            {
                picCover.Image?.Dispose();
                picIcon.Image?.Dispose();

                foreach (Control control in flpScreenshots.Controls)
                {
                    if (control is Panel card)
                    {
                        foreach (Control item in card.Controls)
                        {
                            if (item is PictureBox picture)
                            {
                                picture.Image?.Dispose();
                                picture.Image = null;
                            }
                        }
                    }
                }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlPageHeader = new Panel();
            lblPageTitle = new Label();
            lblPageSubtitle = new Label();

            pnlGameInfo = new Panel();
            lblInfoTitle = new Label();

            lblGameName = new Label();
            txtGameName = new TextBox();

            lblDeveloper = new Label();
            txtDeveloper = new TextBox();

            lblPublisher = new Label();
            txtPublisher = new TextBox();

            lblPrice = new Label();
            numPrice = new NumericUpDown();

            lblReleaseDate = new Label();
            dtpReleaseDate = new DateTimePicker();

            lblCategoriesTitle = new Label();
            clbGameCategories = new CheckedListBox();

            lblDescription = new Label();
            txtDescription = new TextBox();

            pnlScreenshots = new Panel();
            lblScreenshotsTitle = new Label();
            btnChooseScreenshots = new Button();
            flpScreenshots = new FlowLayoutPanel();

            pnlArtwork = new Panel();
            lblArtworkTitle = new Label();

            lblIconTitle = new Label();
            picIcon = new PictureBox();
            btnChooseIcon = new Button();

            lblCoverTitle = new Label();
            picCover = new PictureBox();
            btnChooseCover = new Button();

            pnlActions = new Panel();
            btnClear = new Button();
            btnSaveGame = new Button();

            pnlPageHeader.SuspendLayout();
            pnlGameInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            pnlScreenshots.SuspendLayout();
            pnlArtwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCover).BeginInit();
            pnlActions.SuspendLayout();
            SuspendLayout();

            pnlPageHeader.BackColor = Color.FromArgb(35, 28, 45);
            pnlPageHeader.Controls.Add(lblPageTitle);
            pnlPageHeader.Controls.Add(lblPageSubtitle);
            pnlPageHeader.Location = new Point(0, 0);
            pnlPageHeader.Name = "pnlPageHeader";
            pnlPageHeader.Size = new Size(1283, 100);
            pnlPageHeader.TabIndex = 0;

            lblPageTitle.AutoSize = true;
            lblPageTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.White;
            lblPageTitle.Location = new Point(24, 15);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new Size(250, 50);
            lblPageTitle.Text = "Add New Game";

            lblPageSubtitle.AutoSize = true;
            lblPageSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblPageSubtitle.ForeColor = Color.FromArgb(175, 160, 195);
            lblPageSubtitle.Location = new Point(27, 62);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new Size(300, 20);
            lblPageSubtitle.Text = "Add a new title to your digital game store";

            pnlGameInfo.BackColor = Color.FromArgb(35, 28, 45);
            pnlGameInfo.Controls.Add(lblInfoTitle);
            pnlGameInfo.Controls.Add(lblGameName);
            pnlGameInfo.Controls.Add(txtGameName);
            pnlGameInfo.Controls.Add(lblDeveloper);
            pnlGameInfo.Controls.Add(txtDeveloper);
            pnlGameInfo.Controls.Add(lblPublisher);
            pnlGameInfo.Controls.Add(txtPublisher);
            pnlGameInfo.Controls.Add(lblPrice);
            pnlGameInfo.Controls.Add(numPrice);
            pnlGameInfo.Controls.Add(lblReleaseDate);
            pnlGameInfo.Controls.Add(dtpReleaseDate);
            pnlGameInfo.Controls.Add(lblCategoriesTitle);
            pnlGameInfo.Controls.Add(clbGameCategories);
            pnlGameInfo.Controls.Add(lblDescription);
            pnlGameInfo.Controls.Add(txtDescription);
            pnlGameInfo.Controls.Add(pnlScreenshots);
            pnlGameInfo.Location = new Point(24, 120);
            pnlGameInfo.Name = "pnlGameInfo";
            pnlGameInfo.Size = new Size(820, 780);
            pnlGameInfo.TabIndex = 1;

            lblInfoTitle.AutoSize = true;
            lblInfoTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblInfoTitle.ForeColor = Color.White;
            lblInfoTitle.Location = new Point(22, 18);
            lblInfoTitle.Name = "lblInfoTitle";
            lblInfoTitle.Size = new Size(250, 37);
            lblInfoTitle.Text = "Game Information";

            lblGameName.AutoSize = true;
            lblGameName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGameName.ForeColor = Color.White;
            lblGameName.Text = "Game Name";

            txtGameName.BackColor = Color.FromArgb(48, 38, 58);
            txtGameName.BorderStyle = BorderStyle.FixedSingle;
            txtGameName.Font = new Font("Segoe UI", 10F);
            txtGameName.ForeColor = Color.White;
            txtGameName.Name = "txtGameName";

            lblDeveloper.AutoSize = true;
            lblDeveloper.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDeveloper.ForeColor = Color.White;
            lblDeveloper.Text = "Developer";

            txtDeveloper.BackColor = Color.FromArgb(48, 38, 58);
            txtDeveloper.BorderStyle = BorderStyle.FixedSingle;
            txtDeveloper.Font = new Font("Segoe UI", 10F);
            txtDeveloper.ForeColor = Color.White;
            txtDeveloper.Name = "txtDeveloper";

            lblPublisher.AutoSize = true;
            lblPublisher.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPublisher.ForeColor = Color.White;
            lblPublisher.Text = "Publisher";

            txtPublisher.BackColor = Color.FromArgb(48, 38, 58);
            txtPublisher.BorderStyle = BorderStyle.FixedSingle;
            txtPublisher.Font = new Font("Segoe UI", 10F);
            txtPublisher.ForeColor = Color.White;
            txtPublisher.Name = "txtPublisher";

            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPrice.ForeColor = Color.White;
            lblPrice.Text = "Price (USD)";

            numPrice.BackColor = Color.FromArgb(48, 38, 58);
            numPrice.BorderStyle = BorderStyle.FixedSingle;
            numPrice.DecimalPlaces = 2;
            numPrice.Font = new Font("Segoe UI", 10F);
            numPrice.ForeColor = Color.White;
            numPrice.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numPrice.Name = "numPrice";

            lblReleaseDate.AutoSize = true;
            lblReleaseDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblReleaseDate.ForeColor = Color.White;
            lblReleaseDate.Text = "Release Date";

            dtpReleaseDate.CalendarMonthBackground = Color.FromArgb(48, 38, 58);
            dtpReleaseDate.Font = new Font("Segoe UI", 10F);
            dtpReleaseDate.Format = DateTimePickerFormat.Short;
            dtpReleaseDate.Name = "dtpReleaseDate";

            lblCategoriesTitle.AutoSize = true;
            lblCategoriesTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCategoriesTitle.ForeColor = Color.White;
            lblCategoriesTitle.Text = "Game Categories";

            clbGameCategories.BackColor = Color.FromArgb(48, 38, 58);
            clbGameCategories.BorderStyle = BorderStyle.FixedSingle;
            clbGameCategories.CheckOnClick = true;
            clbGameCategories.Font = new Font("Segoe UI", 9F);
            clbGameCategories.ForeColor = Color.White;
            clbGameCategories.Name = "clbGameCategories";

            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDescription.ForeColor = Color.White;
            lblDescription.Text = "Game Description";

            txtDescription.BackColor = Color.FromArgb(48, 38, 58);
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.ForeColor = Color.White;
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Name = "txtDescription";

            pnlScreenshots.BackColor = Color.FromArgb(42, 34, 53);
            pnlScreenshots.Controls.Add(lblScreenshotsTitle);
            pnlScreenshots.Controls.Add(btnChooseScreenshots);
            pnlScreenshots.Controls.Add(flpScreenshots);
            pnlScreenshots.Name = "pnlScreenshots";

            lblScreenshotsTitle.AutoSize = true;
            lblScreenshotsTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblScreenshotsTitle.ForeColor = Color.White;
            lblScreenshotsTitle.Location = new Point(0, 5);
            lblScreenshotsTitle.Name = "lblScreenshotsTitle";
            lblScreenshotsTitle.Text = "Additional Screenshots (0/4)";

            btnChooseScreenshots.BackColor = Color.FromArgb(105, 65, 160);
            btnChooseScreenshots.FlatStyle = FlatStyle.Flat;
            btnChooseScreenshots.FlatAppearance.BorderSize = 0;
            btnChooseScreenshots.ForeColor = Color.White;
            btnChooseScreenshots.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChooseScreenshots.Name = "btnChooseScreenshots";
            btnChooseScreenshots.Size = new Size(165, 35);
            btnChooseScreenshots.Text = "Choose Screenshots";
            btnChooseScreenshots.Cursor = Cursors.Hand;
            btnChooseScreenshots.UseVisualStyleBackColor = false;

            flpScreenshots.AutoScroll = true;
            flpScreenshots.BackColor = Color.FromArgb(42, 34, 53);
            flpScreenshots.WrapContents = false;
            flpScreenshots.FlowDirection = FlowDirection.LeftToRight;
            flpScreenshots.Name = "flpScreenshots";

            pnlArtwork.BackColor = Color.FromArgb(35, 28, 45);
            pnlArtwork.Controls.Add(lblArtworkTitle);
            pnlArtwork.Controls.Add(lblIconTitle);
            pnlArtwork.Controls.Add(picIcon);
            pnlArtwork.Controls.Add(btnChooseIcon);
            pnlArtwork.Controls.Add(lblCoverTitle);
            pnlArtwork.Controls.Add(picCover);
            pnlArtwork.Controls.Add(btnChooseCover);
            pnlArtwork.Location = new Point(860, 120);
            pnlArtwork.Name = "pnlArtwork";
            pnlArtwork.Size = new Size(360, 780);
            pnlArtwork.TabIndex = 2;

            lblArtworkTitle.AutoSize = true;
            lblArtworkTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblArtworkTitle.ForeColor = Color.White;
            lblArtworkTitle.Location = new Point(20, 18);
            lblArtworkTitle.Name = "lblArtworkTitle";
            lblArtworkTitle.Text = "Game Artwork";

            lblIconTitle.AutoSize = true;
            lblIconTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblIconTitle.ForeColor = Color.White;
            lblIconTitle.Text = "Game Icon";

            picIcon.BackColor = Color.FromArgb(48, 38, 58);
            picIcon.BorderStyle = BorderStyle.FixedSingle;
            picIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picIcon.Name = "picIcon";

            btnChooseIcon.BackColor = Color.FromArgb(105, 65, 160);
            btnChooseIcon.FlatStyle = FlatStyle.Flat;
            btnChooseIcon.FlatAppearance.BorderSize = 0;
            btnChooseIcon.ForeColor = Color.White;
            btnChooseIcon.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChooseIcon.Text = "Choose Icon";
            btnChooseIcon.Name = "btnChooseIcon";
            btnChooseIcon.Size = new Size(125, 34);
            btnChooseIcon.Cursor = Cursors.Hand;
            btnChooseIcon.UseVisualStyleBackColor = false;

            lblCoverTitle.AutoSize = true;
            lblCoverTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCoverTitle.ForeColor = Color.White;
            lblCoverTitle.Text = "Cover Image";

            picCover.BackColor = Color.FromArgb(48, 38, 58);
            picCover.BorderStyle = BorderStyle.FixedSingle;
            picCover.SizeMode = PictureBoxSizeMode.Zoom;
            picCover.Name = "picCover";

            btnChooseCover.BackColor = Color.FromArgb(105, 65, 160);
            btnChooseCover.FlatStyle = FlatStyle.Flat;
            btnChooseCover.FlatAppearance.BorderSize = 0;
            btnChooseCover.ForeColor = Color.White;
            btnChooseCover.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChooseCover.Text = "Choose Cover";
            btnChooseCover.Name = "btnChooseCover";
            btnChooseCover.Size = new Size(135, 34);
            btnChooseCover.Cursor = Cursors.Hand;
            btnChooseCover.UseVisualStyleBackColor = false;

            pnlActions.BackColor = Color.FromArgb(35, 28, 45);
            pnlActions.Controls.Add(btnClear);
            pnlActions.Controls.Add(btnSaveGame);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(1283, 82);
            pnlActions.TabIndex = 3;

            btnClear.BackColor = Color.FromArgb(65, 55, 75);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderColor = Color.FromArgb(90, 75, 105);
            btnClear.ForeColor = Color.White;
            btnClear.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(110, 42);
            btnClear.Text = "Clear";
            btnClear.Cursor = Cursors.Hand;
            btnClear.UseVisualStyleBackColor = false;

            btnSaveGame.BackColor = Color.FromArgb(105, 65, 160);
            btnSaveGame.FlatStyle = FlatStyle.Flat;
            btnSaveGame.FlatAppearance.BorderSize = 0;
            btnSaveGame.ForeColor = Color.White;
            btnSaveGame.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSaveGame.Name = "btnSaveGame";
            btnSaveGame.Size = new Size(145, 42);
            btnSaveGame.Text = "Add Game";
            btnSaveGame.Cursor = Cursors.Hand;
            btnSaveGame.UseVisualStyleBackColor = false;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(30, 22, 30);
            Controls.Add(pnlPageHeader);
            Controls.Add(pnlGameInfo);
            Controls.Add(pnlArtwork);
            Controls.Add(pnlActions);
            Name = "AddGamePage";
            Size = new Size(1283, 850);

            pnlPageHeader.ResumeLayout(false);
            pnlPageHeader.PerformLayout();
            pnlGameInfo.ResumeLayout(false);
            pnlGameInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            pnlScreenshots.ResumeLayout(false);
            pnlScreenshots.PerformLayout();
            pnlArtwork.ResumeLayout(false);
            pnlArtwork.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)picCover).EndInit();
            pnlActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel pnlPageHeader;
        private Label lblPageTitle;
        private Label lblPageSubtitle;

        private Panel pnlGameInfo;
        private Label lblInfoTitle;

        private Label lblGameName;
        private TextBox txtGameName;

        private Label lblDeveloper;
        private TextBox txtDeveloper;

        private Label lblPublisher;
        private TextBox txtPublisher;

        private Label lblPrice;
        private NumericUpDown numPrice;

        private Label lblReleaseDate;
        private DateTimePicker dtpReleaseDate;

        private Label lblCategoriesTitle;
        private CheckedListBox clbGameCategories;

        private Label lblDescription;
        private TextBox txtDescription;

        private Panel pnlScreenshots;
        private Label lblScreenshotsTitle;
        private Button btnChooseScreenshots;
        private FlowLayoutPanel flpScreenshots;

        private Panel pnlArtwork;
        private Label lblArtworkTitle;

        private Label lblIconTitle;
        private PictureBox picIcon;
        private Button btnChooseIcon;

        private Label lblCoverTitle;
        private PictureBox picCover;
        private Button btnChooseCover;

        private Panel pnlActions;
        private Button btnClear;
        private Button btnSaveGame;
    }
}
