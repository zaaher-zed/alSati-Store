
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace alSati_store
{
    public partial class StorePage : UserControl
    {
        public event EventHandler LogoutRequested;

        private List<Game> allGames = new List<Game>();
        private List<Game> currentGames = new List<Game>();

        private GameRepository repository;
        private string selectedCategory = null;
        private bool isUpdatingLayout;

        public StorePage(string displayName)
        {
            InitializeComponent();

            AutoScroll = false;
            flpStoreGames.AutoScroll = true;
            flpStoreGames.WrapContents = true;

            lblStoreUserName.Text = string.IsNullOrWhiteSpace(displayName)
                ? "Player"
                : displayName;

            btnStoreLogout.Click += (s, e) =>
                LogoutRequested?.Invoke(this, EventArgs.Empty);

            pnlStoreHeader.SizeChanged += pnlStoreHeader_SizeChanged;
            flpStoreGames.SizeChanged += flpStoreGames_SizeChanged;

            PositionHeaderControls();

            repository = new GameRepository();

            cmbStoreSort.Items.Clear();

            cmbStoreSort.Items.AddRange(new object[]
            {
                "Default",
                "Name: A to Z",
                "Name: Z to A",
                "Price: Low to High",
                "Price: High to Low",
                "Release Date: Newest",
                "Release Date: Oldest",
                "Rating: Highest"
            });

            cmbStoreSort.SelectedIndex = 0;

            txtStoreSearch.TextChanged += txtStoreSearch_TextChanged;

            LoadGames();
            LoadCategories();
        }

        private void PositionHeaderControls()
        {
            int headerWidth = pnlStoreHeader.ClientSize.Width;

            pnlStoreUser.Location = new Point(
                headerWidth - pnlStoreUser.Width - 20,
                11
            );

            btnStoreLogout.Location = new Point(
                pnlStoreUser.Left - btnStoreLogout.Width - 15,
                11
            );
        }

        private void pnlStoreHeader_SizeChanged(object sender, EventArgs e)
        {
            PositionHeaderControls();
        }

        private void LoadGames()
        {
            try
            {
                allGames = repository.GetAllGames();
                currentGames = allGames;

                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "حدث خطأ أثناء تحميل الألعاب:\n" + ex.Message,
                    "Store Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoadCategories()
        {
            try
            {
                flpStoreCategories.Controls.Clear();

                btnAllGames.Text = "All Games";
                btnAllGames.AutoSize = false;
                btnAllGames.Size = new Size(110, 35);
                btnAllGames.Margin = new Padding(5);
                btnAllGames.Click += btnAllGames_Click;

                flpStoreCategories.Controls.Add(btnAllGames);

                List<string> categories = repository.GetAllCategories();

                foreach (string category in categories)
                {
                    Button categoryButton = new Button
                    {
                        Text = category,
                        Size = new Size(110, 35),
                        AutoSize = false,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.FromArgb(35, 29, 43),
                        ForeColor = Color.FromArgb(220, 215, 230),
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        Cursor = Cursors.Hand,
                        Margin = new Padding(5),
                        Tag = category
                    };

                    categoryButton.FlatAppearance.BorderSize = 0;
                    categoryButton.Click += CategoryButton_Click;

                    flpStoreCategories.Controls.Add(categoryButton);
                }

                UpdateCategoryButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "حدث خطأ أثناء تحميل التصنيفات:\n" + ex.Message,
                    "Category Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAllGames_Click(object sender, EventArgs e)
        {
            selectedCategory = null;
            currentGames = allGames;

            lblAllGamesTitle.Text = "All Games";

            UpdateCategoryButtons();
            ApplyFilters();
        }

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton == null)
                return;

            string categoryName = clickedButton.Tag.ToString();

            try
            {
                List<Game> categoryGames =
                    repository.GetGamesByCategory(categoryName);

                selectedCategory = categoryName;
                currentGames = categoryGames;

                lblAllGamesTitle.Text = categoryName;

                UpdateCategoryButtons();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "حدث خطأ أثناء تحميل ألعاب هذا التصنيف:\n" + ex.Message,
                    "Category Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void UpdateCategoryButtons()
        {
            foreach (Control control in flpStoreCategories.Controls)
            {
                if (control is Button button)
                {
                    bool isSelected;

                    if (button == btnAllGames)
                    {
                        isSelected = selectedCategory == null;
                    }
                    else
                    {
                        isSelected =
                            button.Tag != null &&
                            button.Tag.ToString() == selectedCategory;
                    }

                    button.BackColor = isSelected
                        ? Color.FromArgb(105, 65, 160)
                        : Color.FromArgb(35, 29, 43);

                    button.ForeColor = Color.White;
                }
            }
        }

        private void ApplyFilters()
        {
            string searchText = txtStoreSearch.Text.Trim();

            List<Game> filteredGames = currentGames;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredGames = filteredGames
                    .Where(game =>
                        !string.IsNullOrWhiteSpace(game.GameName) &&
                        game.GameName.Contains(
                            searchText,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            switch (cmbStoreSort.SelectedItem?.ToString())
            {
                case "Name: A to Z":
                    filteredGames = filteredGames
                        .OrderBy(game => game.GameName, StringComparer.OrdinalIgnoreCase)
                        .ToList();
                    break;

                case "Name: Z to A":
                    filteredGames = filteredGames
                        .OrderByDescending(game => game.GameName, StringComparer.OrdinalIgnoreCase)
                        .ToList();
                    break;

                case "Price: Low to High":
                    filteredGames = filteredGames
                        .OrderBy(game => game.Price)
                        .ToList();
                    break;

                case "Price: High to Low":
                    filteredGames = filteredGames
                        .OrderByDescending(game => game.Price)
                        .ToList();
                    break;

                case "Release Date: Newest":
                    filteredGames = filteredGames
                        .OrderByDescending(game => game.ReleaseDate.HasValue)
                        .ThenByDescending(game => game.ReleaseDate)
                        .ToList();
                    break;

                case "Release Date: Oldest":
                    filteredGames = filteredGames
                        .OrderByDescending(game => game.ReleaseDate.HasValue)
                        .ThenBy(game => game.ReleaseDate)
                        .ToList();
                    break;

                case "Rating: Highest":
                    filteredGames = filteredGames
                        .OrderByDescending(game => game.AverageRating)
                        .ThenByDescending(game => game.RatingCount)
                        .ToList();
                    break;
            }

            DisplayGames(filteredGames);
        }

        private int GetGameCardWidth()
        {
            int availableWidth = flpStoreGames.ClientSize.Width - 25;
            int gap = 20;
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

        private void DisplayGames(List<Game> games)
        {
            if (isUpdatingLayout)
                return;

            isUpdatingLayout = true;

            try
            {
                flpStoreGames.SuspendLayout();
                flpStoreGames.Controls.Clear();

                flpStoreGames.AutoScroll = true;
                flpStoreGames.WrapContents = true;

                if (games.Count == 0)
                {
                    Label noResults = new Label
                    {
                        Text = "No games found.",
                        AutoSize = true,
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        Margin = new Padding(15)
                    };

                    flpStoreGames.Controls.Add(noResults);
                    return;
                }

                foreach (Game game in games)
                {
                    flpStoreGames.Controls.Add(CreateGameCard(game));
                }
            }
            finally
            {
                flpStoreGames.ResumeLayout(true);
                isUpdatingLayout = false;
            }
        }

        private void flpStoreGames_SizeChanged(object sender, EventArgs e)
        {
            if (currentGames.Count > 0)
                ApplyFilters();
        }

        private void txtStoreSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private Panel CreateGameCard(Game game)
        {
            int cardWidth = GetGameCardWidth();
            int imageHeight = (int)(cardWidth * 0.86);

            Panel card = new Panel
            {
                Size = new Size(cardWidth, imageHeight + 130),
                BackColor = Color.FromArgb(35, 28, 45),
                Margin = new Padding(10),
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
                Location = new Point(10, imageHeight + 8),
                Size = new Size(cardWidth - 20, 35),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoEllipsis = true,
                Cursor = Cursors.Hand
            };

            string ratingText = game.RatingCount > 0
                ? $"★ {game.AverageRating:0.0} ({game.RatingCount} reviews)"
                : "★ No reviews yet";

            Label rating = new Label
            {
                Text = ratingText,
                Location = new Point(10, imageHeight + 48),
                Size = new Size(cardWidth - 20, 25),
                ForeColor = Color.FromArgb(245, 197, 66),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            Label price = new Label
            {
                Text = game.Price == 0
                    ? "Free"
                    : "$" + game.Price.ToString("0.00"),

                Location = new Point(10, imageHeight + 80),
                Size = new Size(cardWidth - 20, 25),
                ForeColor = Color.FromArgb(190, 150, 255),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            EventHandler openDetails = (sender, e) =>
            {
                MainForm mainForm = FindForm() as MainForm;

                if (mainForm != null)
                {
                    mainForm.OpenGameDetails(game);
                }
            };

            card.Click += openDetails;
            cover.Click += openDetails;
            title.Click += openDetails;
            rating.Click += openDetails;
            price.Click += openDetails;

            card.Controls.Add(cover);
            card.Controls.Add(title);
            card.Controls.Add(rating);
            card.Controls.Add(price);

            return card;
        }

        private void pnlStoreHeader_Paint(object sender, PaintEventArgs e)
        {
        }

        private void StorePage_Load(object sender, EventArgs e)
        {
        }

        private void cmbStoreSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }
    }
}
