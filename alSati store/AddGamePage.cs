
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace alSati_store
{
    public partial class AddGamePage : UserControl
    {
        private string coverImagePath = "";
        private string iconImagePath = "";

        private readonly List<string> screenshotImagePaths = new List<string>();

        private bool isUpdatingLayout;

        public AddGamePage()
        {
            InitializeComponent();

            LoadCategories();

            btnChooseCover.Click += btnChooseCover_Click;
            btnChooseIcon.Click += btnChooseIcon_Click;
            btnChooseScreenshots.Click += btnChooseScreenshots_Click;
            btnSaveGame.Click += btnSaveGame_Click;
            btnClear.Click += btnClear_Click;

            Resize += (s, e) => ConfigureResponsiveLayout();
            HandleCreated += (s, e) => ConfigureResponsiveLayout();

            ConfigureResponsiveLayout();
            InitializeManagementInterface();
            UpdateScreenshotTitle();
        }

        private void ConfigureResponsiveLayout()
        {
            if (isUpdatingLayout || IsDisposed)
                return;

            isUpdatingLayout = true;

            try
            {
                int pageWidth = Math.Max(320, ClientSize.Width);
                int margin = pageWidth < 600 ? 12 : 24;
                int gap = 18;
                int availableWidth = Math.Max(280, pageWidth - margin * 2);

                bool compact = availableWidth < 850;

                pnlPageHeader.SetBounds(0, 0, pageWidth, 100);
                lblPageTitle.Location = new Point(margin, 18);
                lblPageSubtitle.Location = new Point(margin + 2, 57);

                pnlActions.SetBounds(
                    0,
                    Math.Max(0, ClientSize.Height - 82),
                    pageWidth,
                    82
                );

                btnSaveGame.Location = new Point(
                    pageWidth - margin - btnSaveGame.Width,
                    20
                );

                btnClear.Location = new Point(
                    btnSaveGame.Left - 12 - btnClear.Width,
                    20
                );

                int contentTop = 120;
                int contentBottom = 0;
                int contentHeight;

                pnlGameInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                pnlArtwork.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                if (!compact)
                {
                    int artworkWidth = Math.Clamp(
                        (int)(availableWidth * 0.32),
                        280,
                        360
                    );

                    int infoWidth = availableWidth - artworkWidth - gap;

                    contentHeight = 780;

                    pnlGameInfo.SetBounds(
                        margin,
                        contentTop,
                        infoWidth,
                        contentHeight
                    );

                    pnlArtwork.SetBounds(
                        margin + infoWidth + gap,
                        contentTop,
                        artworkWidth,
                        contentHeight
                    );

                    LayoutGameInfo(infoWidth, contentHeight, false);
                    LayoutArtwork(artworkWidth, contentHeight, false);

                    contentBottom = contentTop + contentHeight;
                }
                else
                {
                    int infoHeight = 720;
                    int artworkHeight = 430;

                    pnlGameInfo.SetBounds(
                        margin,
                        contentTop,
                        availableWidth,
                        infoHeight
                    );

                    LayoutGameInfo(availableWidth, infoHeight, true);

                    int artworkTop = contentTop + infoHeight + gap;

                    pnlArtwork.SetBounds(
                        margin,
                        artworkTop,
                        availableWidth,
                        artworkHeight
                    );

                    LayoutArtwork(availableWidth, artworkHeight, true);

                    contentBottom = artworkTop + artworkHeight;
                }

                AutoScroll = true;
                AutoScrollMinSize = new Size(
                    0,
                    contentBottom + 25
                );

                pnlActions.BringToFront();
                PerformLayout();
            }
            finally
            {
                isUpdatingLayout = false;
            }
        }

        private void LayoutGameInfo(int width, int height, bool compact)
        {
            int padding = 22;
            int innerWidth = width - padding * 2;
            int fieldGap = 18;

            lblInfoTitle.Location = new Point(padding, 18);

            int fieldTop = 65;
            int halfWidth = (innerWidth - fieldGap) / 2;

            if (!compact)
            {
                SetField(lblGameName, txtGameName, padding, fieldTop, halfWidth);
                SetField(lblDeveloper, txtDeveloper, padding + halfWidth + fieldGap, fieldTop, halfWidth);

                fieldTop += 92;

                SetField(lblPublisher, txtPublisher, padding, fieldTop, halfWidth);
                SetField(lblPrice, numPrice, padding + halfWidth + fieldGap, fieldTop, halfWidth);

                fieldTop += 92;

                SetField(lblReleaseDate, dtpReleaseDate, padding, fieldTop, halfWidth);
                SetField(lblCategoriesTitle, clbGameCategories, padding + halfWidth + fieldGap, fieldTop, halfWidth);

                clbGameCategories.Height = 105;

                fieldTop += 145;

                SetField(lblDescription, txtDescription, padding, fieldTop, innerWidth);
                txtDescription.Height = 115;

                pnlScreenshots.SetBounds(
                    padding,
                    fieldTop + 160,
                    innerWidth,
                    height - fieldTop - 180
                );
            }
            else
            {
                SetField(lblGameName, txtGameName, padding, fieldTop, innerWidth);
                fieldTop += 80;

                SetField(lblDeveloper, txtDeveloper, padding, fieldTop, innerWidth);
                fieldTop += 80;

                SetField(lblPublisher, txtPublisher, padding, fieldTop, innerWidth);
                fieldTop += 80;

                SetField(lblPrice, numPrice, padding, fieldTop, innerWidth);
                fieldTop += 80;

                SetField(lblReleaseDate, dtpReleaseDate, padding, fieldTop, innerWidth);
                fieldTop += 80;

                SetField(lblCategoriesTitle, clbGameCategories, padding, fieldTop, innerWidth);
                clbGameCategories.Height = 100;
                fieldTop += 130;

                SetField(lblDescription, txtDescription, padding, fieldTop, innerWidth);
                txtDescription.Height = 100;

                pnlScreenshots.SetBounds(
                    padding,
                    fieldTop + 145,
                    innerWidth,
                    Math.Max(150, height - fieldTop - 160)
                );
            }

            pnlScreenshots.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            lblScreenshotsTitle.Location = new Point(0, 5);
            btnChooseScreenshots.Location = new Point(
                Math.Max(0, pnlScreenshots.Width - btnChooseScreenshots.Width),
                0
            );

            flpScreenshots.SetBounds(
                0,
                48,
                pnlScreenshots.Width,
                Math.Max(90, pnlScreenshots.Height - 48)
            );

            flpScreenshots.Anchor =
                AnchorStyles.Top | AnchorStyles.Left |
                AnchorStyles.Right | AnchorStyles.Bottom;

            pnlScreenshots.BringToFront();
        }

        private void LayoutArtwork(int width, int height, bool compact)
        {
            int padding = 20;

            lblArtworkTitle.Location = new Point(padding, 18);

            if (!compact)
            {
                int coverWidth = Math.Min(170, width - 40);
                int coverHeight = 210;
                int iconSize = Math.Min(110, width - 40);

                lblIconTitle.Location = new Point(padding, 70);
                picIcon.SetBounds(padding, 105, iconSize, iconSize);
                btnChooseIcon.SetBounds(padding, 105 + iconSize + 10, 125, 34);

                lblCoverTitle.Location = new Point(padding, 280);
                picCover.SetBounds(padding, 315, coverWidth, coverHeight);
                btnChooseCover.SetBounds(padding, 315 + coverHeight + 10, 135, 34);
            }
            else
            {
                int available = width - padding * 2;
                int iconSize = Math.Min(110, available / 3);
                int coverWidth = Math.Min(170, available / 2);
                int coverHeight = 210;

                int iconLeft = padding;
                int coverLeft = padding + iconSize + 45;

                lblIconTitle.Location = new Point(iconLeft, 70);
                picIcon.SetBounds(iconLeft, 105, iconSize, iconSize);
                btnChooseIcon.SetBounds(iconLeft, 225, 125, 34);

                lblCoverTitle.Location = new Point(coverLeft, 70);
                picCover.SetBounds(coverLeft, 105, coverWidth, coverHeight);
                btnChooseCover.SetBounds(coverLeft, 325, 135, 34);
            }

            picCover.SizeMode = PictureBoxSizeMode.Zoom;
            picIcon.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void SetField(
            Label label,
            Control input,
            int left,
            int top,
            int width)
        {
            label.Location = new Point(left, top);
            input.SetBounds(left, top + 29, width, input is TextBox textBox && textBox.Multiline ? input.Height : 36);

            if (input is TextBox text)
            {
                text.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }
            else
            {
                input.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        private void ChooseImage(PictureBox pictureBox, bool isCover)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Choose Game Image";
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    using (Image original = Image.FromFile(dialog.FileName))
                    {
                        Image preview = new Bitmap(original);

                        if (pictureBox.Image != null)
                            pictureBox.Image.Dispose();

                        pictureBox.Image = preview;
                    }

                    if (isCover)
                        coverImagePath = dialog.FileName;
                    else
                        iconImagePath = dialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Unable to load image:\n" + ex.Message,
                        "Image Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void ChooseScreenshots()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Choose Game Screenshots";
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                dialog.Multiselect = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                foreach (string filePath in dialog.FileNames)
                {
                    if (screenshotImagePaths.Count >= 4)
                    {
                        MessageBox.Show(
                            "You can add a maximum of 4 screenshots.",
                            "Image Limit",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        break;
                    }

                    if (screenshotImagePaths.Contains(filePath))
                        continue;

                    try
                    {
                        AddScreenshotPreview(filePath);
                        screenshotImagePaths.Add(filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Unable to load this image:\n" + ex.Message,
                            "Image Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }

                UpdateScreenshotTitle();
            }
        }

        private void AddScreenshotPreview(string imagePath)
        {
            Panel card = new Panel
            {
                Size = new Size(145, 135),
                BackColor = Color.FromArgb(48, 38, 58),
                Margin = new Padding(6)
            };

            PictureBox picture = new PictureBox
            {
                Size = new Size(135, 95),
                Location = new Point(5, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(35, 28, 45)
            };

            try
            {
                using (Image original = Image.FromFile(imagePath))
                {
                    picture.Image = new Bitmap(original);
                }

                Button btnRemove = new Button
                {
                    Text = "Remove",
                    Size = new Size(135, 25),
                    Location = new Point(5, 105),
                    BackColor = Color.FromArgb(120, 45, 55),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };

                btnRemove.FlatAppearance.BorderSize = 0;

                btnRemove.Click += (sender, e) =>
                {
                    RemoveScreenshot(imagePath, card, picture);
                };

                card.Controls.Add(picture);
                card.Controls.Add(btnRemove);
                flpScreenshots.Controls.Add(card);
            }
            catch
            {
                picture.Image?.Dispose();
                card.Dispose();
                throw;
            }
        }

        private void RemoveScreenshot(
            string imagePath,
            Panel card,
            PictureBox picture)
        {
            picture.Image?.Dispose();
            picture.Image = null;

            screenshotImagePaths.Remove(imagePath);

            flpScreenshots.Controls.Remove(card);
            card.Dispose();

            UpdateScreenshotTitle();
        }

        private void UpdateScreenshotTitle()
        {
            lblScreenshotsTitle.Text =
                $"Additional Screenshots ({screenshotImagePaths.Count}/4)";
        }

        private string CopyGameImage(
            string sourcePath,
            List<string> copiedFiles)
        {
            string folderPath = Path.Combine(
                Application.StartupPath,
                "GameImages"
            );

            Directory.CreateDirectory(folderPath);

            string extension = Path.GetExtension(sourcePath);
            string fileName = Guid.NewGuid().ToString() + extension;

            string destinationPath = Path.Combine(folderPath, fileName);

            File.Copy(sourcePath, destinationPath, true);
            copiedFiles.Add(destinationPath);

            return Path.Combine("GameImages", fileName);
        }

        private void LoadCategories()
        {
            GameRepository repository = new GameRepository();

            clbGameCategories.Items.Clear();

            List<string> categories = repository.GetAllCategories();

            foreach (string category in categories)
                clbGameCategories.Items.Add(category);
        }


        private void SaveGame()
        {
            if (string.IsNullOrWhiteSpace(txtGameName.Text))
            {
                MessageBox.Show("Please enter the game name.");
                txtGameName.Focus();
                return;
            }

            if (editingGameId == -1 &&
                (string.IsNullOrEmpty(coverImagePath) ||
                 string.IsNullOrEmpty(iconImagePath)))
            {
                MessageBox.Show("Please choose a cover image and a game icon.");
                return;
            }

            List<string> selectedCategories = new List<string>();

            foreach (object item in clbGameCategories.CheckedItems)
                selectedCategories.Add(item.ToString());

            List<string> copiedFiles = new List<string>();

            try
            {
                string coverPath = string.IsNullOrEmpty(coverImagePath)
                    ? existingCoverImagePath
                    : CopyGameImage(coverImagePath, copiedFiles);

                string iconPath = string.IsNullOrEmpty(iconImagePath)
                    ? existingIconImagePath
                    : CopyGameImage(iconImagePath, copiedFiles);

                List<string> screenshotPaths = new List<string>();

                foreach (string path in screenshotImagePaths)
                {
                    string existingDatabasePath = null;

                    foreach (string savedPath in existingScreenshotPaths)
                    {
                        if (string.Equals(
                            GetFullGameImagePath(savedPath),
                            path,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            existingDatabasePath = savedPath;
                            break;
                        }
                    }

                    if (existingDatabasePath != null)
                    {
                        screenshotPaths.Add(existingDatabasePath);
                    }
                    else
                    {
                        screenshotPaths.Add(
                            CopyGameImage(path, copiedFiles));
                    }
                }

                Game game = new Game
                {
                    GameId = editingGameId == -1 ? 0 : editingGameId,
                    GameName = txtGameName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Price = numPrice.Value,
                    CoverImage = coverPath,
                    IconImage = iconPath,
                    Developer = txtDeveloper.Text.Trim(),
                    Publisher = txtPublisher.Text.Trim(),
                    ReleaseDate = dtpReleaseDate.Value.Date,
                    GameStatus = "ACTIVE"
                };

                GameRepository repository = new GameRepository();

                if (editingGameId == -1)
                {
                    int gameId = repository.AddGame(
                        game,
                        screenshotPaths,
                        selectedCategories
                    );

                    MessageBox.Show(
                        $"Game added successfully!\nGame ID: {gameId}",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    bool updated = repository.UpdateGame(
                        game,
                        screenshotPaths,
                        selectedCategories,
                        true
                    );

                    if (!updated)
                        throw new Exception("The game could not be updated.");

                    MessageBox.Show(
                        "Game updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                ClearForm();
                LoadManagementGames();
            }
            catch (Exception ex)
            {
                foreach (string file in copiedFiles)
                {
                    try
                    {
                        if (File.Exists(file))
                            File.Delete(file);
                    }
                    catch
                    {
                    }
                }

                MessageBox.Show(
                    "Unable to save the game:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void ClearForm()
        {
            txtGameName.Clear();
            txtDeveloper.Clear();
            txtPublisher.Clear();
            txtDescription.Clear();

            numPrice.Value = 0;
            dtpReleaseDate.Value = DateTime.Today;

            if (picCover.Image != null)
            {
                picCover.Image.Dispose();
                picCover.Image = null;
            }

            if (picIcon.Image != null)
            {
                picIcon.Image.Dispose();
                picIcon.Image = null;
            }

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
                editingGameId = -1;
                existingCoverImagePath = "";
                existingIconImagePath = "";
                existingScreenshotPaths.Clear();
                btnSaveGame.Text = "Add Game";
            }

            flpScreenshots.Controls.Clear();
            screenshotImagePaths.Clear();

            for (int i = 0; i < clbGameCategories.Items.Count; i++)
                clbGameCategories.SetItemChecked(i, false);

            coverImagePath = "";
            iconImagePath = "";

            UpdateScreenshotTitle();
        }

        private void btnChooseCover_Click(object sender, EventArgs e)
        {
            ChooseImage(picCover, true);
        }

        private void btnChooseIcon_Click(object sender, EventArgs e)
        {
            ChooseImage(picIcon, false);
        }

        private void btnChooseScreenshots_Click(object sender, EventArgs e)
        {
            ChooseScreenshots();
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            btnSaveGame.Enabled = false;

            try
            {
                SaveGame();
            }
            finally
            {
                btnSaveGame.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear all entered game information?",
                "Clear Form",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
                ClearForm();
        }
    }
}
