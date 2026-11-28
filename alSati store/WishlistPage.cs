
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace alSati_store
{
    public partial class WishlistPage : UserControl
    {
        private readonly int currentUserId;
        private readonly GameRepository repository = new GameRepository();

        private List<Game> favoriteGames = new List<Game>();
        private bool isUpdatingLayout;
        public event EventHandler LogoutRequested;

        public WishlistPage(int userId, string displayName)
        {
            InitializeComponent();

            currentUserId = userId;

            lblWishlistUserName.Text = string.IsNullOrWhiteSpace(displayName)
                ? "Player"
                : displayName;

            txtWishlistSearch.TextChanged += txtWishlistSearch_TextChanged;
            flpWishlistGames.SizeChanged += flpWishlistGames_SizeChanged;

            LoadFavoriteGames();

            btnWishlistLogout.Click += (s, e) =>
                LogoutRequested?.Invoke(this, EventArgs.Empty);

            HiddenScrollFlowLayoutPanel.HideScrollBars(flpWishlistGames);
        }

        private void LoadFavoriteGames()
        {
            try
            {
                favoriteGames = repository.GetFavoriteGames(currentUserId);
                ApplySearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "حدث خطأ أثناء تحميل قائمة الأمنيات:\n" + ex.Message,
                    "Wishlist Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private int GetGameCardWidth()
        {
            int availableWidth = flpWishlistGames.ClientSize.Width - 25;
            int gap = 16;
            int minimumCardWidth = 350;

            int columns = Math.Clamp(
                (availableWidth + gap) / (minimumCardWidth + gap),
                1,
                8
            );

            return Math.Max(
                minimumCardWidth,
                (availableWidth - columns * gap) / columns
            );
        }

        private void ApplySearch()
        {
            string searchText = txtWishlistSearch.Text.Trim();

            List<Game> filteredGames = favoriteGames.FindAll(game =>
                !string.IsNullOrWhiteSpace(game.GameName) &&
                game.GameName.IndexOf(
                    searchText,
                    StringComparison.OrdinalIgnoreCase) >= 0
            );

            DisplayGames(filteredGames);
        }

        private void DisplayGames(List<Game> games)
        {
            if (isUpdatingLayout)
                return;

            isUpdatingLayout = true;

            try
            {
                flpWishlistGames.SuspendLayout();
                flpWishlistGames.Controls.Clear();

                if (games.Count == 0)
                {
                    bool wishlistIsEmpty = favoriteGames.Count == 0;

                    Label emptyMessage = new Label
                    {
                        Text = wishlistIsEmpty
                            ? "Your wishlist is empty."
                            : "No games found matching your search.",

                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        AutoSize = true,
                        Margin = new Padding(20)
                    };

                    flpWishlistGames.Controls.Add(emptyMessage);
                    return;
                }

                foreach (Game game in games)
                {
                    flpWishlistGames.Controls.Add(CreateGameCard(game));
                }
            }
            finally
            {
                flpWishlistGames.ResumeLayout(true);
                isUpdatingLayout = false;

                HiddenScrollFlowLayoutPanel.HideScrollBars(flpWishlistGames);
            }
        }

        private Panel CreateGameCard(Game game)
        {
            int cardWidth = GetGameCardWidth();
            int imageHeight = (int)(cardWidth * 0.95);

            Panel card = new Panel
            {
                Size = new Size(cardWidth, imageHeight + 155),
                BackColor = Color.FromArgb(35, 28, 45),
                Margin = new Padding(8),
                Cursor = Cursors.Hand
            };

            PictureBox cover = new PictureBox
            {
                Location = new Point(0, 0),
                Size = new Size(cardWidth, imageHeight),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(25, 20, 32),
                Cursor = Cursors.Hand
            };

            string imagePath = game.CoverImage;

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                try
                {
                    string fullImagePath = Path.IsPathRooted(imagePath)
                        ? imagePath
                        : Path.Combine(Application.StartupPath, imagePath);

                    if (File.Exists(fullImagePath))
                    {
                        using (Image originalImage = Image.FromFile(fullImagePath))
                        {
                            cover.Image = new Bitmap(originalImage);
                        }
                    }
                    else if (Uri.TryCreate(
                        imagePath,
                        UriKind.Absolute,
                        out Uri imageUri)
                        && (imageUri.Scheme == "http" || imageUri.Scheme == "https"))
                    {
                        cover.LoadAsync(imagePath);
                    }
                }
                catch
                {
                }
            }

            Label title = new Label
            {
                Text = game.GameName,
                Location = new Point(12, imageHeight + 8),
                Size = new Size(cardWidth - 24, 38),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };

            string ratingText = game.RatingCount > 0
                ? $"★ {game.AverageRating:0.0} ({game.RatingCount} reviews)"
                : "★ No reviews yet";

            Label rating = new Label
            {
                Text = ratingText,
                Location = new Point(12, imageHeight + 49),
                Size = new Size(cardWidth - 24, 28),
                ForeColor = Color.FromArgb(245, 197, 66),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            Label price = new Label
            {
                Text = game.Price == 0
                    ? "Free"
                    : "$" + game.Price.ToString("0.00"),

                Location = new Point(12, imageHeight + 80),
                Size = new Size(cardWidth - 24, 28),
                ForeColor = Color.FromArgb(190, 150, 255),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            Button btnRemove = new Button
            {
                Text = "Remove from Wishlist",
                Location = new Point(12, imageHeight + 115),
                Size = new Size(cardWidth - 24, 35),
                BackColor = Color.FromArgb(105, 65, 160),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };

            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(130, 80, 190);

            EventHandler openDetails = (sender, e) =>
            {
                if (FindForm() is MainForm mainForm)
                {
                    mainForm.OpenGameDetails(game);
                }
            };

            card.Click += openDetails;
            cover.Click += openDetails;
            title.Click += openDetails;
            rating.Click += openDetails;
            price.Click += openDetails;

            btnRemove.Click += (sender, e) =>
            {
                try
                {
                    repository.RemoveFromFavorites(
                        currentUserId,
                        game.GameId
                    );

                    favoriteGames.RemoveAll(
                        item => item.GameId == game.GameId
                    );

                    if (cover.Image != null)
                    {
                        cover.Image.Dispose();
                        cover.Image = null;
                    }

                    ApplySearch();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "حدث خطأ أثناء إزالة اللعبة:\n" + ex.Message,
                        "Wishlist Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            };

            card.Controls.Add(cover);
            card.Controls.Add(title);
            card.Controls.Add(rating);
            card.Controls.Add(price);
            card.Controls.Add(btnRemove);

            return card;
        }

        private void flpWishlistGames_SizeChanged(object sender, EventArgs e)
        {
            if (favoriteGames.Count > 0)
                ApplySearch();
        }

        private void txtWishlistSearch_TextChanged(object sender, EventArgs e)
        {
            ApplySearch();
        }
    }
}
