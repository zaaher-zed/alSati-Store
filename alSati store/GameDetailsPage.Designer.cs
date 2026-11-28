
using System;
using System.Drawing;
using System.Windows.Forms;

namespace alSati_store
{
    partial class GameDetailsPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ratingToolTip.Dispose();

                foreach (Image image in loadedImages)
                    image.Dispose();

                loadedImages.Clear();

                if (components != null)
                    components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlDetailsHeader = new Panel();
            pnlDetailsUser = new Panel();
            lblDetailsUserIcon = new Label();
            lblDetailsUserName = new Label();
            btnBack = new Button();

            pnlGamePreview = new Panel();
            flpGameScreenshots = new FlowLayoutPanel();
            picMainGame = new PictureBox();

            pnlGameInformation = new Panel();
            lblDetailsGameTitle = new Label();
            lblRatingAverage = new Label();
            lblRatingCount = new Label();
            flpRatingStars = new FlowLayoutPanel();

            pnlAbout = new Panel();
            lblAboutTitle = new Label();
            lblGameDescription = new Label();

            pnlCategories = new Panel();
            lblCategoriesTitle = new Label();
            flpDetailsCategories = new FlowLayoutPanel();

            pnlGameMetadata = new Panel();
            lblDeveloper = new Label();
            lblPublisher = new Label();
            lblReleaseDate = new Label();

            pnlPurchase = new Panel();
            lblDetailsPrice = new Label();
            btnBuyGame = new Button();
            btnAddToWishlist = new Button();

            pnlDetailsHeader.SuspendLayout();
            pnlDetailsUser.SuspendLayout();
            pnlGamePreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMainGame).BeginInit();
            pnlGameInformation.SuspendLayout();
            pnlAbout.SuspendLayout();
            pnlCategories.SuspendLayout();
            pnlGameMetadata.SuspendLayout();
            pnlPurchase.SuspendLayout();
            SuspendLayout();

            pnlDetailsHeader.BackColor = Color.FromArgb(24, 18, 33);
            pnlDetailsHeader.Controls.Add(pnlDetailsUser);
            pnlDetailsHeader.Dock = DockStyle.Top;
            pnlDetailsHeader.Location = new Point(0, 0);
            pnlDetailsHeader.Name = "pnlDetailsHeader";
            pnlDetailsHeader.Size = new Size(838, 65);
            pnlDetailsHeader.TabIndex = 0;
            pnlDetailsHeader.Paint += pnlDetailsHeader_Paint;

            pnlDetailsUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlDetailsUser.BackColor = Color.FromArgb(43, 29, 55);
            pnlDetailsUser.Controls.Add(lblDetailsUserIcon);
            pnlDetailsUser.Controls.Add(lblDetailsUserName);
            pnlDetailsUser.Location = new Point(638, 11);
            pnlDetailsUser.Name = "pnlDetailsUser";
            pnlDetailsUser.Size = new Size(180, 42);
            pnlDetailsUser.TabIndex = 1;

            lblDetailsUserIcon.BackColor = Color.Transparent;
            lblDetailsUserIcon.Font = new Font("Segoe UI Symbol", 17F);
            lblDetailsUserIcon.ForeColor = Color.FromArgb(190, 150, 235);
            lblDetailsUserIcon.Location = new Point(5, 0);
            lblDetailsUserIcon.Name = "lblDetailsUserIcon";
            lblDetailsUserIcon.Size = new Size(40, 42);
            lblDetailsUserIcon.TabIndex = 0;
            lblDetailsUserIcon.Text = "◉";
            lblDetailsUserIcon.TextAlign = ContentAlignment.MiddleCenter;

            lblDetailsUserName.BackColor = Color.Transparent;
            lblDetailsUserName.Font = new Font("Segoe UI", 10F);
            lblDetailsUserName.ForeColor = Color.White;
            lblDetailsUserName.Location = new Point(50, 0);
            lblDetailsUserName.Name = "lblDetailsUserName";
            lblDetailsUserName.Size = new Size(125, 42);
            lblDetailsUserName.TabIndex = 1;
            lblDetailsUserName.Text = "Player";
            lblDetailsUserName.TextAlign = ContentAlignment.MiddleLeft;

            btnBack.BackColor = Color.Transparent;
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 34, 59);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBack.ForeColor = Color.FromArgb(195, 170, 225);
            btnBack.Location = new Point(20, 78);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(125, 36);
            btnBack.TabIndex = 1;
            btnBack.Text = "←  Back to Store";
            btnBack.UseVisualStyleBackColor = false;

            pnlGamePreview.BackColor = Color.FromArgb(25, 19, 34);
            pnlGamePreview.Controls.Add(picMainGame);
            pnlGamePreview.Controls.Add(flpGameScreenshots);
            pnlGamePreview.Location = new Point(20, 125);
            pnlGamePreview.Name = "pnlGamePreview";
            pnlGamePreview.Size = new Size(798, 365);
            pnlGamePreview.TabIndex = 2;

            picMainGame.BackColor = Color.FromArgb(18, 14, 25);
            picMainGame.Dock = DockStyle.Fill;
            picMainGame.Location = new Point(0, 0);
            picMainGame.Name = "picMainGame";
            picMainGame.Size = new Size(798, 280);
            picMainGame.SizeMode = PictureBoxSizeMode.StretchImage;
            picMainGame.TabIndex = 0;
            picMainGame.TabStop = false;

            flpGameScreenshots.AutoScroll = true;
            flpGameScreenshots.BackColor = Color.FromArgb(25, 19, 34);
            flpGameScreenshots.Dock = DockStyle.Bottom;
            flpGameScreenshots.Location = new Point(0, 280);
            flpGameScreenshots.Name = "flpGameScreenshots";
            flpGameScreenshots.Padding = new Padding(5);
            flpGameScreenshots.Size = new Size(798, 85);
            flpGameScreenshots.TabIndex = 1;
            flpGameScreenshots.WrapContents = false;

            pnlGameInformation.BackColor = Color.FromArgb(23, 17, 32);
            pnlGameInformation.Controls.Add(lblDetailsGameTitle);
            pnlGameInformation.Controls.Add(lblRatingAverage);
            pnlGameInformation.Controls.Add(lblRatingCount);
            pnlGameInformation.Controls.Add(flpRatingStars);
            pnlGameInformation.Location = new Point(20, 510);
            pnlGameInformation.Name = "pnlGameInformation";
            pnlGameInformation.Size = new Size(798, 145);
            pnlGameInformation.TabIndex = 3;

            lblDetailsGameTitle.BackColor = Color.Transparent;
            lblDetailsGameTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblDetailsGameTitle.ForeColor = Color.White;
            lblDetailsGameTitle.Location = new Point(15, 8);
            lblDetailsGameTitle.Name = "lblDetailsGameTitle";
            lblDetailsGameTitle.Size = new Size(765, 52);
            lblDetailsGameTitle.TabIndex = 0;
            lblDetailsGameTitle.Text = "Game Title";
            lblDetailsGameTitle.TextAlign = ContentAlignment.MiddleLeft;

            lblRatingAverage.BackColor = Color.Transparent;
            lblRatingAverage.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRatingAverage.ForeColor = Color.FromArgb(232, 185, 79);
            lblRatingAverage.Location = new Point(17, 65);
            lblRatingAverage.Name = "lblRatingAverage";
            lblRatingAverage.Size = new Size(130, 30);
            lblRatingAverage.TabIndex = 1;
            lblRatingAverage.Text = "No ratings";
            lblRatingAverage.TextAlign = ContentAlignment.MiddleLeft;

            lblRatingCount.BackColor = Color.Transparent;
            lblRatingCount.Font = new Font("Segoe UI", 9.5F);
            lblRatingCount.ForeColor = Color.FromArgb(165, 155, 180);
            lblRatingCount.Location = new Point(150, 65);
            lblRatingCount.Name = "lblRatingCount";
            lblRatingCount.Size = new Size(150, 30);
            lblRatingCount.TabIndex = 2;
            lblRatingCount.Text = "0 ratings";
            lblRatingCount.TextAlign = ContentAlignment.MiddleLeft;

            flpRatingStars.AutoSize = false;
            flpRatingStars.BackColor = Color.Transparent;
            flpRatingStars.FlowDirection = FlowDirection.LeftToRight;
            flpRatingStars.Location = new Point(15, 100);
            flpRatingStars.Name = "flpRatingStars";
            flpRatingStars.Padding = new Padding(3);
            flpRatingStars.Size = new Size(230, 42);
            flpRatingStars.TabIndex = 3;
            flpRatingStars.WrapContents = false;

            pnlAbout.BackColor = Color.FromArgb(23, 17, 32);
            pnlAbout.Controls.Add(lblAboutTitle);
            pnlAbout.Controls.Add(lblGameDescription);
            pnlAbout.Location = new Point(20, 675);
            pnlAbout.Name = "pnlAbout";
            pnlAbout.Size = new Size(798, 170);
            pnlAbout.TabIndex = 4;

            lblAboutTitle.BackColor = Color.Transparent;
            lblAboutTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblAboutTitle.ForeColor = Color.White;
            lblAboutTitle.Location = new Point(15, 8);
            lblAboutTitle.Name = "lblAboutTitle";
            lblAboutTitle.Size = new Size(400, 38);
            lblAboutTitle.TabIndex = 0;
            lblAboutTitle.Text = "About This Game";
            lblAboutTitle.TextAlign = ContentAlignment.MiddleLeft;

            lblGameDescription.BackColor = Color.Transparent;
            lblGameDescription.Font = new Font("Segoe UI", 10F);
            lblGameDescription.ForeColor = Color.FromArgb(195, 188, 207);
            lblGameDescription.Location = new Point(17, 50);
            lblGameDescription.Name = "lblGameDescription";
            lblGameDescription.Size = new Size(760, 105);
            lblGameDescription.TabIndex = 1;
            lblGameDescription.Text = "Game description will appear here...";

            pnlCategories.BackColor = Color.FromArgb(23, 17, 32);
            pnlCategories.Controls.Add(lblCategoriesTitle);
            pnlCategories.Controls.Add(flpDetailsCategories);
            pnlCategories.Location = new Point(20, 860);
            pnlCategories.Name = "pnlCategories";
            pnlCategories.Size = new Size(798, 95);
            pnlCategories.TabIndex = 5;

            lblCategoriesTitle.BackColor = Color.Transparent;
            lblCategoriesTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCategoriesTitle.ForeColor = Color.White;
            lblCategoriesTitle.Location = new Point(15, 5);
            lblCategoriesTitle.Name = "lblCategoriesTitle";
            lblCategoriesTitle.Size = new Size(200, 30);
            lblCategoriesTitle.TabIndex = 0;
            lblCategoriesTitle.Text = "Categories";
            lblCategoriesTitle.TextAlign = ContentAlignment.MiddleLeft;

            flpDetailsCategories.AutoScroll = true;
            flpDetailsCategories.BackColor = Color.Transparent;
            flpDetailsCategories.Location = new Point(10, 40);
            flpDetailsCategories.Name = "flpDetailsCategories";
            flpDetailsCategories.Size = new Size(775, 45);
            flpDetailsCategories.TabIndex = 1;
            flpDetailsCategories.WrapContents = false;

            pnlGameMetadata.BackColor = Color.FromArgb(23, 17, 32);
            pnlGameMetadata.Controls.Add(lblDeveloper);
            pnlGameMetadata.Controls.Add(lblPublisher);
            pnlGameMetadata.Controls.Add(lblReleaseDate);
            pnlGameMetadata.Location = new Point(20, 970);
            pnlGameMetadata.Name = "pnlGameMetadata";
            pnlGameMetadata.Size = new Size(798, 115);
            pnlGameMetadata.TabIndex = 6;

            lblDeveloper.BackColor = Color.Transparent;
            lblDeveloper.Font = new Font("Segoe UI", 10F);
            lblDeveloper.ForeColor = Color.FromArgb(195, 188, 207);
            lblDeveloper.Location = new Point(15, 10);
            lblDeveloper.Name = "lblDeveloper";
            lblDeveloper.Size = new Size(370, 35);
            lblDeveloper.TabIndex = 0;
            lblDeveloper.Text = "Developer:";
            lblDeveloper.TextAlign = ContentAlignment.MiddleLeft;

            lblPublisher.BackColor = Color.Transparent;
            lblPublisher.Font = new Font("Segoe UI", 10F);
            lblPublisher.ForeColor = Color.FromArgb(195, 188, 207);
            lblPublisher.Location = new Point(400, 10);
            lblPublisher.Name = "lblPublisher";
            lblPublisher.Size = new Size(370, 35);
            lblPublisher.TabIndex = 1;
            lblPublisher.Text = "Publisher:";
            lblPublisher.TextAlign = ContentAlignment.MiddleLeft;

            lblReleaseDate.BackColor = Color.Transparent;
            lblReleaseDate.Font = new Font("Segoe UI", 10F);
            lblReleaseDate.ForeColor = Color.FromArgb(195, 188, 207);
            lblReleaseDate.Location = new Point(15, 60);
            lblReleaseDate.Name = "lblReleaseDate";
            lblReleaseDate.Size = new Size(370, 35);
            lblReleaseDate.TabIndex = 2;
            lblReleaseDate.Text = "Release Date:";
            lblReleaseDate.TextAlign = ContentAlignment.MiddleLeft;

            pnlPurchase.BackColor = Color.FromArgb(34, 25, 48);
            pnlPurchase.Controls.Add(lblDetailsPrice);
            pnlPurchase.Controls.Add(btnBuyGame);
            pnlPurchase.Controls.Add(btnAddToWishlist);
            pnlPurchase.Location = new Point(20, 1100);
            pnlPurchase.Name = "pnlPurchase";
            pnlPurchase.Size = new Size(798, 90);
            pnlPurchase.TabIndex = 7;

            lblDetailsPrice.BackColor = Color.Transparent;
            lblDetailsPrice.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblDetailsPrice.ForeColor = Color.White;
            lblDetailsPrice.Location = new Point(15, 18);
            lblDetailsPrice.Name = "lblDetailsPrice";
            lblDetailsPrice.Size = new Size(170, 50);
            lblDetailsPrice.TabIndex = 0;
            lblDetailsPrice.Text = "$29.99";
            lblDetailsPrice.TextAlign = ContentAlignment.MiddleLeft;

            btnBuyGame.BackColor = Color.FromArgb(105, 65, 160);
            btnBuyGame.Cursor = Cursors.Hand;
            btnBuyGame.FlatAppearance.BorderSize = 0;
            btnBuyGame.FlatAppearance.MouseOverBackColor = Color.FromArgb(135, 85, 195);
            btnBuyGame.FlatStyle = FlatStyle.Flat;
            btnBuyGame.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnBuyGame.ForeColor = Color.White;
            btnBuyGame.Location = new Point(390, 20);
            btnBuyGame.Name = "btnBuyGame";
            btnBuyGame.Size = new Size(180, 48);
            btnBuyGame.TabIndex = 1;
            btnBuyGame.Text = "Buy Now";
            btnBuyGame.UseVisualStyleBackColor = false;

            btnAddToWishlist.BackColor = Color.FromArgb(45, 34, 59);
            btnAddToWishlist.Cursor = Cursors.Hand;
            btnAddToWishlist.FlatAppearance.BorderColor = Color.FromArgb(125, 85, 175);
            btnAddToWishlist.FlatAppearance.BorderSize = 1;
            btnAddToWishlist.FlatAppearance.MouseOverBackColor = Color.FromArgb(65, 46, 85);
            btnAddToWishlist.FlatStyle = FlatStyle.Flat;
            btnAddToWishlist.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddToWishlist.ForeColor = Color.White;
            btnAddToWishlist.Location = new Point(580, 20);
            btnAddToWishlist.Name = "btnAddToWishlist";
            btnAddToWishlist.Size = new Size(195, 48);
            btnAddToWishlist.TabIndex = 2;
            btnAddToWishlist.Text = "♡  Add to Wishlist";
            btnAddToWishlist.UseVisualStyleBackColor = false;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(15, 11, 22);
            Controls.Add(pnlDetailsHeader);
            Controls.Add(btnBack);
            Controls.Add(pnlGamePreview);
            Controls.Add(pnlGameInformation);
            Controls.Add(pnlAbout);
            Controls.Add(pnlCategories);
            Controls.Add(pnlGameMetadata);
            Controls.Add(pnlPurchase);
            Name = "GameDetailsPage";
            Size = new Size(838, 1200);

            pnlDetailsHeader.ResumeLayout(false);
            pnlDetailsUser.ResumeLayout(false);
            pnlGamePreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picMainGame).EndInit();
            pnlGameInformation.ResumeLayout(false);
            pnlAbout.ResumeLayout(false);
            pnlCategories.ResumeLayout(false);
            pnlGameMetadata.ResumeLayout(false);
            pnlPurchase.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel pnlDetailsHeader;
        private Panel pnlDetailsUser;
        private Label lblDetailsUserIcon;
        private Label lblDetailsUserName;

        private Button btnBack;

        private Panel pnlGamePreview;
        private PictureBox picMainGame;
        private FlowLayoutPanel flpGameScreenshots;

        private Panel pnlGameInformation;
        private Label lblDetailsGameTitle;
        private Label lblRatingAverage;
        private Label lblRatingCount;
        private FlowLayoutPanel flpRatingStars;

        private Panel pnlAbout;
        private Label lblAboutTitle;
        private Label lblGameDescription;

        private Panel pnlCategories;
        private Label lblCategoriesTitle;
        private FlowLayoutPanel flpDetailsCategories;

        private Panel pnlGameMetadata;
        private Label lblDeveloper;
        private Label lblPublisher;
        private Label lblReleaseDate;

        private Panel pnlPurchase;
        private Label lblDetailsPrice;
        private Button btnBuyGame;
        private Button btnAddToWishlist;
    }
}
