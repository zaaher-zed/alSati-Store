
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace alSati_store
{
    public partial class LibraryPage : UserControl
    {
        private readonly int currentUserId;
        private readonly GameRepository repository = new GameRepository();
        private List<Game> libraryGames = new List<Game>();

        private bool isUpdatingLayout;
        public event EventHandler LogoutRequested;

        public LibraryPage(int userId, string displayName)
        {
            InitializeComponent();

            currentUserId = userId;

            lblLibraryUserName.Text = string.IsNullOrWhiteSpace(displayName)
                ? "Player"
                : displayName;

            txtLibrarySearch.TextChanged += txtLibrarySearch_TextChanged;
            flpLibraryGames.SizeChanged += flpLibraryGames_SizeChanged;

            LoadLibraryGames();

            btnLibraryLogout.Click += (s, e) =>
                LogoutRequested?.Invoke(this, EventArgs.Empty);

            HiddenScrollFlowLayoutPanel.HideScrollBars(flpLibraryGames);
        }

        private void LoadLibraryGames()
        {
            try
            {
                libraryGames = repository.GetLibraryGames(currentUserId);
                DisplayGames(libraryGames);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Library Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private int GetGameCardWidth()
        {
            int availableWidth = flpLibraryGames.ClientSize.Width - 25;
            int gap = 16;
            int minimumCardWidth = 350;

            int columns = Math.Clamp(
                (availableWidth + gap) / (minimumCardWidth + gap),
                1,
                8);

            return Math.Max(
                minimumCardWidth,
                (availableWidth - columns * gap) / columns);
        }

        private void DisplayGames(List<Game> games)
        {
            if (isUpdatingLayout)
                return;

            isUpdatingLayout = true;

            try
            {
                flpLibraryGames.SuspendLayout();
                flpLibraryGames.Controls.Clear();

                if (games.Count == 0)
                {
                    bool libraryIsEmpty =
                        string.IsNullOrWhiteSpace(txtLibrarySearch.Text);

                    Label lblEmpty = new Label
                    {
                        Text = libraryIsEmpty
                            ? "Your library is empty. Purchase games to see them here!"
                            : "No games found matching your search.",

                        ForeColor = Color.LightGray,
                        Font = new Font("Segoe UI", 12F),
                        AutoSize = true,
                        Margin = new Padding(20)
                    };

                    flpLibraryGames.Controls.Add(lblEmpty);
                    return;
                }

                foreach (Game game in games)
                {
                    flpLibraryGames.Controls.Add(CreateGameCard(game));
                }
            }
            finally
            {
                flpLibraryGames.ResumeLayout(true);
                isUpdatingLayout = false;

                HiddenScrollFlowLayoutPanel.HideScrollBars(flpLibraryGames);
            }
        }

        private Panel CreateGameCard(Game game)
        {
            int cardWidth = GetGameCardWidth();
            int imageHeight = (int)(cardWidth * 0.95);

            Panel card = new Panel
            {
                Size = new Size(cardWidth, imageHeight + 100),
                BackColor = Color.FromArgb(35, 28, 45),
                Margin = new Padding(8),
                Cursor = Cursors.Hand
            };

            PictureBox cover = new PictureBox
            {
                Size = new Size(cardWidth, imageHeight),
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(48, 38, 58),
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
                        using (Image image = Image.FromFile(fullImagePath))
                        {
                            cover.Image = new Bitmap(image);
                        }
                    }
                    else if (Uri.TryCreate(
                        imagePath,
                        UriKind.Absolute,
                        out Uri imageUri)
                        && (imageUri.Scheme == "http"
                            || imageUri.Scheme == "https"))
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
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, imageHeight + 5),
                Size = new Size(cardWidth - 20, 35),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true,
                Cursor = Cursors.Hand
            };

            Label status = new Label
            {
                Text = "Owned",
                ForeColor = Color.FromArgb(170, 130, 255),
                Font = new Font("Segoe UI", 9F),
                Location = new Point(10, imageHeight + 45),
                Size = new Size(cardWidth - 20, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };

            card.Controls.Add(cover);
            card.Controls.Add(title);
            card.Controls.Add(status);

            card.Click += (s, e) => OpenGame(game);
            cover.Click += (s, e) => OpenGame(game);
            title.Click += (s, e) => OpenGame(game);
            status.Click += (s, e) => OpenGame(game);

            return card;
        }

        private void flpLibraryGames_SizeChanged(object sender, EventArgs e)
        {
            if (libraryGames.Count == 0)
                return;

            string searchText = txtLibrarySearch.Text.Trim();

            List<Game> gamesToDisplay = string.IsNullOrWhiteSpace(searchText)
                ? libraryGames
                : libraryGames.FindAll(game =>
                    !string.IsNullOrWhiteSpace(game.GameName) &&
                    game.GameName.IndexOf(
                        searchText,
                        StringComparison.OrdinalIgnoreCase) >= 0);

            DisplayGames(gamesToDisplay);
        }

        private void OpenGame(Game game)
        {
            if (FindForm() is MainForm mainForm)
            {
                mainForm.OpenGameDetails(game);
            }
        }

        private void txtLibrarySearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtLibrarySearch.Text.Trim();

            List<Game> filteredGames = libraryGames.FindAll(game =>
                !string.IsNullOrWhiteSpace(game.GameName) &&
                game.GameName.IndexOf(
                    searchText,
                    StringComparison.OrdinalIgnoreCase) >= 0);

            DisplayGames(filteredGames);
        }
    }
}
