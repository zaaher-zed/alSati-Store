
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace alSati_store
{
    public partial class AddGamePage
    {
        private Panel pnlManagement;
        private TextBox txtGameSearch;
        private DataGridView dgvGames;
        private Button btnOpenManagement;
        private Button btnBackToAdd;
        private Button btnEditSelected;
        private Button btnDeleteSelected;
        private Button btnRefreshGames;
        private Label lblManagementTitle;
        private Label lblManagementCount;

        private List<Game> managementGames = new List<Game>();

        private int editingGameId = -1;
        private string existingCoverImagePath = "";
        private string existingIconImagePath = "";
        private readonly List<string> existingScreenshotPaths = new List<string>();

        private void InitializeManagementInterface()
        {
            btnOpenManagement = new Button
            {
                Text = "Manage Games",
                Size = new Size(125, 38),
                BackColor = Color.FromArgb(65, 55, 75),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnOpenManagement.FlatAppearance.BorderSize = 0;
            btnOpenManagement.Click += (s, e) => ShowManagement();

            pnlPageHeader.Controls.Add(btnOpenManagement);

            btnBackToAdd = new Button
            {
                Text = "Back to Add Game",
                Size = new Size(145, 38),
                Location = new Point(15, 15),
                BackColor = Color.FromArgb(65, 55, 75),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnBackToAdd.FlatAppearance.BorderSize = 0;
            btnBackToAdd.Click += (s, e) => ShowAddGame();

            lblManagementTitle = new Label
            {
                Text = "Manage Games",
                AutoSize = true,
                Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20)
            };

            lblManagementCount = new Label
            {
                Text = "0 Games",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(175, 160, 195)
            };

            txtGameSearch = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(48, 38, 58),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Search games by name..."
            };

            txtGameSearch.TextChanged += (s, e) => FilterManagementGames();

            dgvGames = new DataGridView
            {
                BackgroundColor = Color.FromArgb(35, 28, 45),
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(65, 55, 75),
                ForeColor = Color.White,
                EnableHeadersVisualStyles = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9F),
                ColumnHeadersHeight = 42,
                RowTemplate = { Height = 38 }
            };

            dgvGames.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(65, 48, 85);

            dgvGames.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvGames.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvGames.DefaultCellStyle.BackColor =
                Color.FromArgb(42, 34, 53);

            dgvGames.DefaultCellStyle.ForeColor = Color.White;
            dgvGames.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(105, 65, 160);

            dgvGames.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvGames.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(48, 38, 58);

            dgvGames.Columns.Add("GameId", "ID");
            dgvGames.Columns.Add("GameName", "Game Name");
            dgvGames.Columns.Add("Developer", "Developer");
            dgvGames.Columns.Add("Price", "Price");
            dgvGames.Columns.Add("ReleaseDate", "Release Date");
            dgvGames.Columns.Add("Rating", "Rating");

            dgvGames.Columns["GameId"].FillWeight = 35;
            dgvGames.Columns["GameName"].FillWeight = 180;
            dgvGames.Columns["Developer"].FillWeight = 100;
            dgvGames.Columns["Price"].FillWeight = 55;
            dgvGames.Columns["ReleaseDate"].FillWeight = 80;
            dgvGames.Columns["Rating"].FillWeight = 55;

            btnEditSelected = CreateManagementButton(
                "Edit Selected",
                Color.FromArgb(105, 65, 160));

            btnDeleteSelected = CreateManagementButton(
                "Delete Selected",
                Color.FromArgb(145, 48, 60));

            btnRefreshGames = CreateManagementButton(
                "Refresh",
                Color.FromArgb(65, 55, 75));

            btnEditSelected.Click += (s, e) => EditSelectedGame();
            btnDeleteSelected.Click += (s, e) => DeleteSelectedGame();
            btnRefreshGames.Click += (s, e) => LoadManagementGames();

            pnlManagement = new Panel
            {
                BackColor = Color.FromArgb(30, 22, 30),
                Visible = false
            };

            pnlManagement.Controls.Add(lblManagementTitle);
            pnlManagement.Controls.Add(lblManagementCount);
            pnlManagement.Controls.Add(txtGameSearch);
            pnlManagement.Controls.Add(dgvGames);
            pnlManagement.Controls.Add(btnEditSelected);
            pnlManagement.Controls.Add(btnDeleteSelected);
            pnlManagement.Controls.Add(btnRefreshGames);

            Controls.Add(pnlManagement);

            Resize += (s, e) => LayoutManagementInterface();

            LayoutManagementInterface();
        }

        private Button CreateManagementButton(string text, Color color)
        {
            Button button = new Button
            {
                Text = text,
                Size = new Size(135, 40),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private void LayoutManagementInterface()
        {
            if (pnlManagement == null || IsDisposed)
                return;

            int width = Math.Max(320, ClientSize.Width);
            int margin = width < 600 ? 12 : 24;

            btnOpenManagement.Location = new Point(
                Math.Max(margin, width - btnOpenManagement.Width - margin),
                30
            );

            pnlManagement.SetBounds(
                0,
                100,
                width,
                Math.Max(300, ClientSize.Height - 182)
            );

            lblManagementTitle.Location = new Point(margin, 18);

            lblManagementCount.Location = new Point(
                margin + 2,
                58
            );

            txtGameSearch.SetBounds(
                margin,
                95,
                Math.Max(250, width - margin * 2),
                38
            );

            int buttonWidth = width < 600 ? 100 : 135;
            int buttonGap = 10;

            btnRefreshGames.SetBounds(
                margin,
                145,
                buttonWidth,
                40
            );

            btnDeleteSelected.SetBounds(
                margin + buttonWidth + buttonGap,
                145,
                buttonWidth,
                40
            );

            btnEditSelected.SetBounds(
                margin + (buttonWidth + buttonGap) * 2,
                145,
                buttonWidth,
                40
            );

            dgvGames.SetBounds(
                margin,
                200,
                Math.Max(250, width - margin * 2),
                Math.Max(120, pnlManagement.Height - 220)
            );
        }

        private void ShowManagement()
        {
            pnlGameInfo.Visible = false;
            pnlArtwork.Visible = false;
            pnlActions.Visible = false;

            pnlManagement.Visible = true;
            pnlManagement.BringToFront();

            btnOpenManagement.Visible = false;
            btnBackToAdd.Visible = true;

            pnlPageHeader.Controls.Add(btnBackToAdd);

            lblPageTitle.Text = "Manage Games";
            lblPageSubtitle.Text = "Search, edit, and remove games from your store";

            LayoutManagementInterface();
            LoadManagementGames();
        }

        private void ShowAddGame()
        {
            pnlManagement.Visible = false;

            pnlGameInfo.Visible = true;
            pnlArtwork.Visible = true;
            pnlActions.Visible = true;

            btnBackToAdd.Visible = false;
            btnOpenManagement.Visible = true;

            lblPageTitle.Text = editingGameId == -1
                ? "Add New Game"
                : "Edit Game";

            lblPageSubtitle.Text = editingGameId == -1
                ? "Add a new title to your digital game store"
                : "Update the selected game's information";

            ConfigureResponsiveLayout();
        }

        private void LoadManagementGames()
        {
            try
            {
                GameRepository repository = new GameRepository();

                managementGames = repository.GetAllGames();

                FilterManagementGames();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load games:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void FilterManagementGames()
        {
            if (dgvGames == null)
                return;

            string search = txtGameSearch.Text.Trim();

            IEnumerable<Game> filteredGames = managementGames;

            if (!string.IsNullOrWhiteSpace(search))
            {
                filteredGames = filteredGames.Where(
                    game => game.GameName != null &&
                    game.GameName.IndexOf(
                        search,
                        StringComparison.OrdinalIgnoreCase) >= 0
                );
            }

            dgvGames.Rows.Clear();

            foreach (Game game in filteredGames)
            {
                dgvGames.Rows.Add(
                    game.GameId,
                    game.GameName,
                    game.Developer,
                    game.Price == 0
                        ? "Free"
                        : "$" + game.Price.ToString("0.00"),
                    game.ReleaseDate?.ToString("yyyy-MM-dd") ?? "",
                    game.AverageRating.ToString("0.0")
                );
            }

            lblManagementCount.Text =
                $"{filteredGames.Count()} Games";
        }

        private Game GetSelectedManagementGame()
        {
            if (dgvGames.SelectedRows.Count == 0)
                return null;

            int gameId = Convert.ToInt32(
                dgvGames.SelectedRows[0].Cells["GameId"].Value
            );

            return managementGames.FirstOrDefault(
                game => game.GameId == gameId
            );
        }

        private void DeleteSelectedGame()
        {
            Game game = GetSelectedManagementGame();

            if (game == null)
            {
                MessageBox.Show(
                    "Please select a game first.",
                    "No Game Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to remove \"{game.GameName}\" from the store?\n\n" +
                "It will no longer be available for purchase, but existing ownership and purchase records will be preserved.",
                "Delete Game",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                GameRepository repository = new GameRepository();

                if (repository.DeactivateGame(game.GameId))
                {
                    MessageBox.Show(
                        "The game has been removed from the store.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    LoadManagementGames();
                }
                else
                {
                    MessageBox.Show(
                        "The game could not be found or has already been removed.",
                        "Delete Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to remove the game:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void EditSelectedGame()
        {
            Game game = GetSelectedManagementGame();

            if (game == null)
            {
                MessageBox.Show(
                    "Please select a game first.",
                    "No Game Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            try
            {
                GameRepository repository = new GameRepository();

                editingGameId = game.GameId;

                txtGameName.Text = game.GameName ?? "";
                txtDeveloper.Text = game.Developer ?? "";
                txtPublisher.Text = game.Publisher ?? "";
                txtDescription.Text = game.Description ?? "";

                numPrice.Value = Math.Min(
                    numPrice.Maximum,
                    Math.Max(numPrice.Minimum, game.Price)
                );

                if (game.ReleaseDate.HasValue)
                {
                    DateTime releaseDate = game.ReleaseDate.Value;

                    if (releaseDate < dtpReleaseDate.MinDate)
                        releaseDate = dtpReleaseDate.MinDate;

                    if (releaseDate > dtpReleaseDate.MaxDate)
                        releaseDate = dtpReleaseDate.MaxDate;

                    dtpReleaseDate.Value = releaseDate;
                }

                existingCoverImagePath = game.CoverImage ?? "";
                existingIconImagePath = game.IconImage ?? "";

                coverImagePath = "";
                iconImagePath = "";

                LoadManagementImage(picCover, existingCoverImagePath);
                LoadManagementImage(picIcon, existingIconImagePath);

                List<string> categories =
                    repository.GetGameCategories(game.GameId);

                for (int i = 0; i < clbGameCategories.Items.Count; i++)
                {
                    string category = clbGameCategories.Items[i].ToString();

                    clbGameCategories.SetItemChecked(
                        i,
                        categories.Contains(category)
                    );
                }

                ClearScreenshotPreviews();

                existingScreenshotPaths.Clear();
                screenshotImagePaths.Clear();

                List<string> savedImages =
                    repository.GetGameImages(game.GameId);

                foreach (string savedPath in savedImages)
                {
                    string fullPath = GetFullGameImagePath(savedPath);

                    if (!File.Exists(fullPath))
                        continue;

                    AddScreenshotPreview(fullPath);

                    screenshotImagePaths.Add(fullPath);
                    existingScreenshotPaths.Add(savedPath);
                }

                UpdateScreenshotTitle();

                btnSaveGame.Text = "Save Changes";
                ShowAddGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load the selected game:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoadManagementImage(
            PictureBox pictureBox,
            string databasePath)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }

            string fullPath = GetFullGameImagePath(databasePath);

            if (!File.Exists(fullPath))
                return;

            using (Image original = Image.FromFile(fullPath))
            {
                pictureBox.Image = new Bitmap(original);
            }
        }

        private string GetFullGameImagePath(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return "";

            if (Path.IsPathRooted(imagePath))
                return imagePath;

            return Path.Combine(
                Application.StartupPath,
                imagePath.Replace(
                    '/',
                    Path.DirectorySeparatorChar
                )
            );
        }

        private void ClearScreenshotPreviews()
        {
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

                    card.Dispose();
                }
            }

            flpScreenshots.Controls.Clear();
        }
    }
}
