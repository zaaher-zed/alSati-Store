
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace alSati_store
{
    public partial class GameDetailsPage : UserControl
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private Game currentGame;
        private readonly int currentUserId;
        private readonly string currentDisplayName;

        private readonly List<Image> loadedImages = new List<Image>();
        private readonly List<Label> ratingStars = new List<Label>();

        private int selectedRating = 0;
        private bool canRateGame = false;
        private bool isUpdatingLayout = false;

        private readonly ToolTip ratingToolTip = new ToolTip();

        public event Action BackRequested;

        public GameDetailsPage(Game game, int userId, string displayName)
        {
            InitializeComponent();

            currentGame = game;
            currentUserId = userId;
            currentDisplayName = displayName;

            ConfigureResponsiveLayout();

            Resize += (s, e) => ConfigureResponsiveLayout();
            HandleCreated += (s, e) => ConfigureResponsiveLayout();

            lblDetailsUserName.Text =
                string.IsNullOrWhiteSpace(currentDisplayName)
                    ? "Player"
                    : currentDisplayName;

            btnBack.Click += (s, e) => BackRequested?.Invoke();
            btnAddToWishlist.Click += btnAddToWishlist_Click;
            btnBuyGame.Click += btnBuyGame_Click;

            LoadGameDetails();
            UpdateWishlistButton();
            UpdatePurchaseButton();
            LoadRatingSystem();
        }

        private void ConfigureResponsiveLayout()
        {
            if (isUpdatingLayout || IsDisposed)
                return;

            isUpdatingLayout = true;

            try
            {
                int contentWidth = Math.Max(400, ClientSize.Width - 40);

                int previewHeight = Math.Clamp(
                    (int)(contentWidth * 0.62),
                    440,
                    690
                );

                int left = 20;
                int currentTop = 125;
                int sectionGap = 20;

                pnlGamePreview.Anchor =
                    AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                pnlGameInformation.Anchor =
                    AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                pnlAbout.Anchor =
                    AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                pnlCategories.Anchor =
                    AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                pnlGameMetadata.Anchor =
                    AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                pnlPurchase.Anchor =
                    AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                pnlGamePreview.SetBounds(
                    left,
                    currentTop,
                    contentWidth,
                    previewHeight
                );

                flpGameScreenshots.Height = 90;
                flpGameScreenshots.Dock = DockStyle.Bottom;
                flpGameScreenshots.WrapContents = false;
                AutoScroll = true;

                PerformLayout();

                AutoScrollMinSize = new Size(
                    0,
                    pnlPurchase.Bottom + 50
                );

                lblDetailsGameTitle.Anchor =
                    AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                currentTop = pnlGameInformation.Bottom + sectionGap;

                pnlAbout.SetBounds(
                    left,
                    currentTop,
                    contentWidth,
                    170
                );

                lblGameDescription.Anchor =
                    AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                lblGameDescription.Width = contentWidth - 38;

                currentTop = pnlAbout.Bottom + sectionGap;

                pnlCategories.SetBounds(
                    left,
                    currentTop,
                    contentWidth,
                    95
                );

                flpDetailsCategories.Anchor =
                    AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                flpDetailsCategories.Width = contentWidth - 23;

                currentTop = pnlCategories.Bottom + sectionGap;

                pnlGameMetadata.SetBounds(
                    left,
                    currentTop,
                    contentWidth,
                    115
                );

                lblPublisher.Anchor =
                    AnchorStyles.Top | AnchorStyles.Right;

                lblPublisher.Location = new Point(
                    Math.Max(200, contentWidth / 2),
                    10
                );

                currentTop = pnlGameMetadata.Bottom + sectionGap;

                pnlPurchase.SetBounds(
                    left,
                    currentTop,
                    contentWidth,
                    90
                );

                btnAddToWishlist.Anchor =
                    AnchorStyles.Top | AnchorStyles.Right;

                btnBuyGame.Anchor =
                    AnchorStyles.Top | AnchorStyles.Right;

                btnAddToWishlist.Location = new Point(
                    contentWidth - btnAddToWishlist.Width - 23,
                    20
                );

                btnBuyGame.Location = new Point(
                    btnAddToWishlist.Left - btnBuyGame.Width - 10,
                    20
                );

                lblDetailsPrice.Anchor =
                    AnchorStyles.Top | AnchorStyles.Left;

                AutoScroll = true;

                AutoScrollMinSize = new Size(
                    0,
                    currentTop + pnlPurchase.Height + 50
                );

                PerformLayout();
            }
            finally
            {
                isUpdatingLayout = false;
            }
        }

        private void LoadGameDetails()
        {
            GameRepository repository = new GameRepository();

            lblDetailsGameTitle.Text = currentGame.GameName;
            lblGameDescription.Text = currentGame.Description;
            lblDetailsPrice.Text = FormatGamePrice();
            lblDeveloper.Text = "Developer: " + currentGame.Developer;
            lblPublisher.Text = "Publisher: " + currentGame.Publisher;

            lblReleaseDate.Text = currentGame.ReleaseDate.HasValue
                ? "Release Date: " + currentGame.ReleaseDate.Value.ToString("MMMM dd, yyyy")
                : "Release Date: Unknown";

            LoadMainImage(currentGame.CoverImage);
            LoadScreenshots(repository.GetGameImages(currentGame.GameId));
            LoadCategories(repository.GetGameCategories(currentGame.GameId));
        }

        private Image LoadImage(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return null;

            try
            {
                byte[] imageData;

                if (Uri.TryCreate(imagePath, UriKind.Absolute, out Uri uri)
                    && (uri.Scheme == Uri.UriSchemeHttp ||
                        uri.Scheme == Uri.UriSchemeHttps))
                {
                    imageData = httpClient
                        .GetByteArrayAsync(uri)
                        .GetAwaiter()
                        .GetResult();
                }
                else
                {
                    string fullPath = Path.IsPathRooted(imagePath)
                        ? imagePath
                        : Path.Combine(Application.StartupPath, imagePath);

                    if (!File.Exists(fullPath))
                        return null;

                    imageData = File.ReadAllBytes(fullPath);
                }

                using (MemoryStream stream = new MemoryStream(imageData))
                using (Image originalImage = Image.FromStream(stream))
                {
                    Image image = new Bitmap(originalImage);
                    loadedImages.Add(image);
                    return image;
                }
            }
            catch
            {
                return null;
            }
        }

        private Image CloneImage(Image source)
        {
            if (source == null)
                return null;

            Image copy = new Bitmap(source);
            loadedImages.Add(copy);
            return copy;
        }

        private void SetMainImage(Image source)
        {
            Image previousImage = picMainGame.Image;

            picMainGame.Image = CloneImage(source);

            if (previousImage != null)
            {
                loadedImages.Remove(previousImage);
                previousImage.Dispose();
            }
        }

        private void LoadMainImage(string imagePath)
        {
            Image image = LoadImage(imagePath);

            if (image != null)
                SetMainImage(image);
        }

        private void LoadScreenshots(List<string> screenshotPaths)
        {
            flpGameScreenshots.Controls.Clear();

            foreach (string path in screenshotPaths)
            {
                Image image = LoadImage(path);

                if (image == null)
                    continue;

                PictureBox thumbnail = new PictureBox
                {
                    Size = new Size(110, 65),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.FromArgb(45, 35, 55),
                    BorderStyle = BorderStyle.FixedSingle,
                    Cursor = Cursors.Hand,
                    Margin = new Padding(5),
                    Image = CloneImage(image)
                };

                thumbnail.Click += (s, e) =>
                {
                    SetMainImage(image);
                };

                flpGameScreenshots.Controls.Add(thumbnail);
            }
        }

        private void LoadCategories(List<string> categories)
        {
            flpDetailsCategories.Controls.Clear();

            foreach (string category in categories)
            {
                Label categoryLabel = new Label
                {
                    Text = category,
                    AutoSize = true,
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(75, 50, 110),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Padding = new Padding(10, 5, 10, 5),
                    Margin = new Padding(5)
                };

                flpDetailsCategories.Controls.Add(categoryLabel);
            }
        }

        private void UpdateWishlistButton()
        {
            GameRepository repository = new GameRepository();

            bool isFavorite = repository.IsGameInFavorites(
                currentUserId,
                currentGame.GameId);

            btnAddToWishlist.Text = isFavorite
                ? "Remove from Wishlist"
                : "Add to Wishlist";
        }

        private void btnAddToWishlist_Click(object sender, EventArgs e)
        {
            try
            {
                GameRepository repository = new GameRepository();

                if (repository.IsGameInFavorites(currentUserId, currentGame.GameId))
                {
                    repository.RemoveFromFavorites(currentUserId, currentGame.GameId);
                    MessageBox.Show("Game removed from your wishlist.");
                }
                else
                {
                    repository.AddToFavorites(currentUserId, currentGame.GameId);
                    MessageBox.Show("Game added to your wishlist.");
                }

                UpdateWishlistButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An error occurred:\n" + ex.Message,
                    "Wishlist Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnBuyGame_Click(object sender, EventArgs e)
        {
            btnBuyGame.Enabled = false;

            try
            {
                DialogResult result = MessageBox.Show(
                    $"Do you want to purchase {currentGame.GameName} for {FormatGamePrice()}?",
                    "Confirm Purchase",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    UpdatePurchaseButton();
                    return;
                }

                GameRepository repository = new GameRepository();

                repository.PurchaseGame(currentUserId, currentGame.GameId);

                MessageBox.Show(
                    "Game purchased successfully and added to your library!",
                    "Purchase Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                UpdatePurchaseButton();
                LoadRatingSystem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Purchase Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                UpdatePurchaseButton();
            }
        }

        private void UpdatePurchaseButton()
        {
            GameRepository repository = new GameRepository();

            bool isOwned = repository.IsGameOwned(
                currentUserId,
                currentGame.GameId);

            if (isOwned)
            {
                btnBuyGame.Text = "Already in Library";
                btnBuyGame.Enabled = false;
            }
            else
            {
                btnBuyGame.Text = $"Buy - {FormatGamePrice()}";
                btnBuyGame.Enabled = true;
            }
        }

        private void LoadRatingSystem()
        {
            GameRepository repository = new GameRepository();

            canRateGame = repository.CanUserRateGame(
                currentUserId,
                currentGame.GameId);

            int? userRating = repository.GetUserGameRating(
                currentUserId,
                currentGame.GameId);

            selectedRating = userRating ?? 0;

            CreateRatingStars();
            UpdateRatingDisplay();
        }

        private void CreateRatingStars()
        {
            flpRatingStars.Controls.Clear();
            ratingStars.Clear();

            for (int i = 1; i <= 5; i++)
            {
                int ratingValue = i;

                Label star = new Label
                {
                    Text = "★",
                    Font = new Font("Segoe UI Symbol", 22F, FontStyle.Regular),
                    Size = new Size(40, 45),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(100, 90, 105),
                    BackColor = Color.Transparent,
                    Padding = new Padding(0),
                    Margin = new Padding(0),
                    Cursor = canRateGame ? Cursors.Hand : Cursors.Default
                };

                star.Click += (s, e) =>
                {
                    if (canRateGame)
                        SaveRating(ratingValue);
                };

                ratingToolTip.SetToolTip(
                    star,
                    canRateGame
                        ? $"Rate {ratingValue} out of 5"
                        : "Purchase this game to rate it");

                ratingStars.Add(star);
                flpRatingStars.Controls.Add(star);
            }

            UpdateStarColors();
        }

        private void UpdateStarColors()
        {
            for (int i = 0; i < ratingStars.Count; i++)
            {
                ratingStars[i].ForeColor = i + 1 <= selectedRating
                    ? Color.FromArgb(232, 185, 79)
                    : Color.FromArgb(100, 90, 105);
            }
        }

        private void SaveRating(int rating)
        {
            try
            {
                GameRepository repository = new GameRepository();

                repository.SaveGameRating(
                    currentUserId,
                    currentGame.GameId,
                    rating);

                selectedRating = rating;

                UpdateStarColors();
                UpdateRatingDisplay();

                MessageBox.Show(
                    "Your rating has been saved successfully!",
                    "Rating Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Rating Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                LoadRatingSystem();
            }
        }

        private string FormatGamePrice()
        {
            return currentGame.Price == 0
                ? "Free"
                : "$" + currentGame.Price.ToString("0.00");
        }

        private void UpdateRatingDisplay()
        {
            GameRepository repository = new GameRepository();

            var summary = repository.GetGameRatingSummary(currentGame.GameId);

            if (summary.RatingCount == 0)
            {
                lblRatingAverage.Text = "No ratings";
                lblRatingCount.Text = "0 ratings";
            }
            else
            {
                lblRatingAverage.Text =
                    "★ " + summary.AverageRating.ToString("0.0") + " / 5";

                lblRatingCount.Text = summary.RatingCount == 1
                    ? "1 rating"
                    : summary.RatingCount + " ratings";
            }

            UpdateStarColors();
        }

        private void pnlDetailsHeader_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
