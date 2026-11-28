
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace alSati_store
{
    public partial class MainForm : Form
    {
        private UserControl currentPage;
        private GameRepository gameRepository;
        private AppContext appContext;

        private List<Game> allGames = new List<Game>();
        private List<Game> featuredGames = new List<Game>();

        private System.Windows.Forms.Timer featuredTimer;
        private int currentFeaturedIndex;
        private int featuredImageRequestId;

        private WebClient featuredImageClient;
        private bool isClosing;

        private Bitmap featuredOriginalImage;
        private Bitmap featuredCoverImage;
        private Size featuredCoverSize = Size.Empty;

        private readonly Color backgroundColor =
            Color.FromArgb(17, 12, 21);

        private readonly Color sidebarColor =
            Color.FromArgb(22, 15, 25);

        private const int GameCardWidth = 350;
        private const int GameCardHeight = 400;
        private const int GameCoverWidth = 350;
        private const int GameCoverHeight = 260;
        private const int RecommendedLimit = 12;
        private const int ScrollAmount = 600;
        private Button btnLogout;

        public int CurrentUserId { get; private set; }
        public string CurrentUsername { get; private set; }
        public string CurrentDisplayName { get; private set; }
        public string CurrentUserRole { get; private set; }

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(
            IntPtr hwnd,
            string pszSubAppName,
            string pszSubIdList);

        private void ApplyDarkScrollBar()
{
    ApplyDarkThemeToScrollBar(pnlMain);
    ApplyDarkThemeToScrollBar(flpGames);
    ApplyDarkThemeToScrollBar(flpRecommended);
}

private void ApplyDarkThemeToScrollBar(Control control)
{
    if (control == null || control.IsDisposed)
        return;

    if (!control.IsHandleCreated)
        return;

    SetWindowTheme(
        control.Handle,
        "DarkMode_Explorer",
        null);
}

        public MainForm(
            int userId,
            string username,
            string displayName,
            string role,
            AppContext context)
        {
            InitializeComponent();
            HiddenScrollFlowLayoutPanel.HideScrollBars(pnlMain);

            pnlMain.HandleCreated += (s, e) =>
                SetWindowTheme(
                    pnlMain.Handle,
                    "DarkMode_Explorer",
                    null);

            flpGames.HandleCreated += (s, e) =>
                SetWindowTheme(
                    flpGames.Handle,
                    "DarkMode_Explorer",
                    null);

            flpRecommended.HandleCreated += (s, e) =>
                SetWindowTheme(
                    flpRecommended.Handle,
                    "DarkMode_Explorer",
                    null);

            ApplyDarkScrollBar();

            appContext = context;

            CurrentUserId = userId;
            CurrentUsername = username;
            CurrentDisplayName = displayName;
            CurrentUserRole = role;

            gameRepository = new GameRepository();

            ApplyMainBackground();
            InitializeFeaturedCarousel();

            ConfigureHorizontalScrolling(flpGames);
            ConfigureHorizontalScrolling(flpRecommended);

            ConfigureArrowButton(
                btnDiscoverLeft, "btnDiscoverLeft", "‹");

            ConfigureArrowButton(
                btnDiscoverRight, "btnDiscoverRight", "›");

            ConfigureArrowButton(
                btnRecommendedLeft, "btnRecommendedLeft", "‹");

            ConfigureArrowButton(
                btnRecommendedRight, "btnRecommendedRight", "›");

            PositionArrowButtons();

            pnlDiscover.Resize += (s, e) =>
            {
                PositionArrowButtons();
                UpdateDiscoverArrows();
            };

            pnlRecommended.Resize += (s, e) =>
            {
                PositionArrowButtons();
                UpdateRecommendedArrows();
            };

            flpGames.Resize += (s, e) =>
                UpdateDiscoverArrows();

            flpRecommended.Resize += (s, e) =>
                UpdateRecommendedArrows();

            flpGames.Layout += (s, e) =>
                UpdateDiscoverArrows();

            flpRecommended.Layout += (s, e) =>
                UpdateRecommendedArrows();

            pnlFeatured.Resize += (s, e) =>
                PositionFeaturedControls();

            picFeaturedGame.Resize += (s, e) =>
            {
                RenderFeaturedCover();
                PositionFeaturedControls();
            };

            pnlMain.Resize += (s, e) =>
            {
                PositionFeaturedControls();
                PositionArrowButtons();
                HiddenScrollFlowLayoutPanel.HideScrollBars(pnlMain);
            };

            pnlMain.Layout += (s, e) =>
                HiddenScrollFlowLayoutPanel.HideScrollBars(pnlMain);

            btnDiscoverLeft.Click += (s, e) =>
                ScrollGames(flpGames, -ScrollAmount);

            btnDiscoverRight.Click += (s, e) =>
                ScrollGames(flpGames, ScrollAmount);

            btnRecommendedLeft.Click += (s, e) =>
                ScrollGames(flpRecommended, -ScrollAmount);

            btnRecommendedRight.Click += (s, e) =>
                ScrollGames(flpRecommended, ScrollAmount);

            SetupCarouselHover(
                pnlDiscover,
                flpGames,
                btnDiscoverLeft,
                btnDiscoverRight);

            SetupCarouselHover(
                pnlRecommended,
                flpRecommended,
                btnRecommendedLeft,
                btnRecommendedRight);

            lblUserName.Text =
                string.IsNullOrWhiteSpace(CurrentDisplayName)
                    ? "Player"
                    : CurrentDisplayName;

            CreateLogoutButton();
            pnlTopBar.Resize += (s, e) =>
            {
                PositionHeaderControls();
            };

            this.Shown += (s, e) =>
            {
                PositionHeaderControls();
            };
            lblUserName.Visible = true;
            lblUserName.BringToFront();

            foreach (Control control in pnlTopBar.Controls)
            {
                control.Visible = true;
            }

            pnlTopBar.PerformLayout();

            SetActiveButton(btnHome);

            btnAddGame.Visible =
                string.Equals(
                    CurrentUserRole,
                    "ADMIN",
                    StringComparison.OrdinalIgnoreCase);

            PositionFeaturedControls();

        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            ApplyDarkScrollBar();
        }

        private void ApplyMainBackground()
        {
            BackColor = backgroundColor;

            pnlMain.BackColor = backgroundColor;
            pnlSidebar.BackColor = sidebarColor;

            picFeaturedGame.BackColor =
                Color.FromArgb(25, 19, 34);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadHomeData();
        }

        private void LoadHomeData()
        {
            try
            {
                allGames = gameRepository.GetAllGames()
                    ?? new List<Game>();

                List<Game> latestGames =
                    gameRepository.GetLatestGames(20)
                    ?? new List<Game>();

                featuredGames = new List<Game>();

                for (int i = 0;
                     i < latestGames.Count && i < 4;
                     i++)
                {
                    featuredGames.Add(latestGames[i]);
                }

                StartFeaturedCarousel();

                PopulateGameCarousel(flpGames, allGames);

                List<Game> recommendedGames =
                    gameRepository.GetTopRatedGames(10)
                    ?? new List<Game>();

                List<Game> bestSellingGames =
                    gameRepository.GetBestSellingGames(10)
                    ?? new List<Game>();

                HashSet<int> addedGameIds =
                    new HashSet<int>();

                List<Game> finalRecommended =
                    new List<Game>();

                foreach (Game game in recommendedGames)
                {
                    if (game == null)
                        continue;

                    if (addedGameIds.Add(game.GameId))
                        finalRecommended.Add(game);

                    if (finalRecommended.Count >= RecommendedLimit)
                        break;
                }

                foreach (Game game in bestSellingGames)
                {
                    if (finalRecommended.Count >= RecommendedLimit)
                        break;

                    if (game == null)
                        continue;

                    if (addedGameIds.Add(game.GameId))
                        finalRecommended.Add(game);
                }

                PopulateGameCarousel(
                    flpRecommended,
                    finalRecommended);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "حدث خطأ أثناء تحميل الصفحة الرئيسية:\n" +
                    ex.Message,
                    "Home Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void InitializeFeaturedCarousel()
        {
            picFeaturedGame.SizeMode =
                PictureBoxSizeMode.StretchImage;

            picFeaturedGame.BackColor =
                Color.FromArgb(25, 19, 34);

            pnlFeatured.BackColor =
                Color.FromArgb(22, 11, 15);

            if (pnlGameInfo.Parent != picFeaturedGame)
            {
                Control oldParent = pnlGameInfo.Parent;

                if (oldParent != null)
                    oldParent.Controls.Remove(pnlGameInfo);

                picFeaturedGame.Controls.Add(pnlGameInfo);
            }

            if (pnlSlideIndicators.Parent != picFeaturedGame)
            {
                Control oldParent = pnlSlideIndicators.Parent;

                if (oldParent != null)
                    oldParent.Controls.Remove(pnlSlideIndicators);

                picFeaturedGame.Controls.Add(pnlSlideIndicators);
            }

            pnlGameInfo.Dock = DockStyle.Bottom;
            pnlGameInfo.Height = 135;
            pnlGameInfo.Anchor =
                AnchorStyles.Left |
                AnchorStyles.Right |
                AnchorStyles.Bottom;

            pnlGameInfo.BackColor = Color.Transparent;

            pnlSlideIndicators.Anchor =
                AnchorStyles.Bottom;

            featuredTimer = new System.Windows.Forms.Timer
            {
                Interval = 4000
            };

            featuredTimer.Tick += (s, e) =>
            {
                if (isClosing ||
                    currentPage != null ||
                    !pnlFeatured.Visible)
                    return;

                ShowNextFeaturedGame();
            };

            PositionFeaturedControls();
        }

        private void PositionFeaturedControls()
        {
            if (picFeaturedGame == null ||
                pnlGameInfo == null ||
                pnlSlideIndicators == null)
                return;

            int width = picFeaturedGame.ClientSize.Width;
            int height = picFeaturedGame.ClientSize.Height;

            if (width <= 0 || height <= 0)
                return;

            pnlGameInfo.Dock = DockStyle.Bottom;
            pnlGameInfo.Height = Math.Min(135, height);

            int indicatorWidth =
                pnlSlideIndicators.Width;

            pnlSlideIndicators.Location = new Point(
                Math.Max(0, (width - indicatorWidth) / 2),
                Math.Max(
                    0,
                    height - pnlSlideIndicators.Height - 8));

            pnlSlideIndicators.BringToFront();

            lblGameTitle.BringToFront();
            lblGameRating.BringToFront();
            lblGamePrice.BringToFront();

            pnlTagRPG.BringToFront();
            pnlTagAction.BringToFront();
            pnlTagAdventure.BringToFront();
        }

        private void StartFeaturedCarousel()
        {
            if (featuredTimer != null)
                featuredTimer.Stop();

            if (featuredGames == null ||
                featuredGames.Count == 0)
            {
                ClearFeaturedGame();
                UpdateFeaturedIndicators();
                return;
            }

            currentFeaturedIndex = 0;

            ShowFeaturedGameAt(currentFeaturedIndex);

            if (featuredGames.Count > 1 &&
                currentPage == null &&
                pnlFeatured.Visible)
            {
                featuredTimer.Start();
            }
        }

        private void ShowNextFeaturedGame()
        {
            if (isClosing ||
                currentPage != null ||
                !pnlFeatured.Visible ||
                featuredGames == null ||
                featuredGames.Count <= 1)
                return;

            currentFeaturedIndex++;

            if (currentFeaturedIndex >= featuredGames.Count)
                currentFeaturedIndex = 0;

            ShowFeaturedGameAt(currentFeaturedIndex);
        }

        private void ShowFeaturedGameAt(int index)
        {
            if (isClosing ||
                currentPage != null ||
                !pnlFeatured.Visible ||
                featuredGames == null ||
                index < 0 ||
                index >= featuredGames.Count)
                return;

            currentFeaturedIndex = index;

            LoadFeaturedGame(featuredGames[index]);

            UpdateFeaturedIndicators();
        }

        private void UpdateFeaturedIndicators()
        {
            Label[] indicators =
            {
                lblIndicator1,
                lblIndicator2,
                lblIndicator3,
                lblIndicator4
            };

            for (int i = 0; i < indicators.Length; i++)
            {
                bool active =
                    i == currentFeaturedIndex &&
                    i < featuredGames.Count;

                indicators[i].BackColor = active
                    ? Color.FromArgb(190, 75, 125)
                    : Color.FromArgb(130, 130, 140);

                indicators[i].Size = active
                    ? new Size(30, 8)
                    : new Size(9, 8);

                indicators[i].Visible =
                    i < featuredGames.Count;

                indicators[i].Cursor = Cursors.Hand;

                indicators[i].Click -= FeaturedIndicator_Click;
                indicators[i].Click += FeaturedIndicator_Click;
            }

            int x = 5;

            for (int i = 0; i < indicators.Length; i++)
            {
                if (!indicators[i].Visible)
                    continue;

                indicators[i].Location =
                    new Point(x, 6);

                x += indicators[i].Width + 12;
            }

            int totalWidth = x > 5 ? x - 12 : 0;

            pnlSlideIndicators.Width =
                Math.Max(0, totalWidth + 10);

            PositionFeaturedControls();
        }

        private void FeaturedIndicator_Click(
            object sender,
            EventArgs e)
        {
            Label[] indicators =
            {
                lblIndicator1,
                lblIndicator2,
                lblIndicator3,
                lblIndicator4
            };

            for (int i = 0; i < indicators.Length; i++)
            {
                if (sender == indicators[i] &&
                    i < featuredGames.Count)
                {
                    ShowFeaturedGameAt(i);
                    break;
                }
            }
        }

        private void LoadFeaturedGame(Game game)
        {
            if (game == null)
            {
                ClearFeaturedGame();
                return;
            }

            lblGameTitle.Text =
                game.GameName ?? "Unknown Game";

            lblGamePrice.Text =
                game.Price == 0
                ? "Free"
                : "$" + game.Price.ToString("0.00");

            lblGameRating.Text =
                game.RatingCount > 0
                    ? "★  " +
                      game.AverageRating.ToString("0.0") +
                      " / 5  (" + game.RatingCount + ")"
                    : "★  No ratings yet";

            LoadFeaturedBackground(game.CoverImage);

            pnlTagRPG.Visible = false;
            pnlTagAction.Visible = false;
            pnlTagAdventure.Visible = false;

            try
            {
                List<string> categories =
                    gameRepository.GetGameCategories(game.GameId);

                if (categories == null)
                    categories = new List<string>();

                Panel[] tagPanels =
                {
                    pnlTagRPG,
                    pnlTagAction,
                    pnlTagAdventure
                };

                Label[] tagLabels =
                {
                    lblTagRPG,
                    lblTagAction,
                    lblTagAdventure
                };

                for (int i = 0; i < tagPanels.Length; i++)
                {
                    if (i < categories.Count)
                    {
                        tagLabels[i].Text = categories[i];
                        tagPanels[i].Visible = true;
                    }
                    else
                    {
                        tagPanels[i].Visible = false;
                    }
                }
            }
            catch
            {
                pnlTagRPG.Visible = false;
                pnlTagAction.Visible = false;
                pnlTagAdventure.Visible = false;
            }

            PositionFeaturedControls();
        }

        private void LoadFeaturedBackground(string imagePath)
        {
            featuredImageRequestId++;

            int requestId = featuredImageRequestId;

            CancelFeaturedImageRequest();

            ClearFeaturedImage();

            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            try
            {
                if (Uri.TryCreate(
                    imagePath,
                    UriKind.Absolute,
                    out Uri imageUri) &&
                    (imageUri.Scheme == Uri.UriSchemeHttp ||
                     imageUri.Scheme == Uri.UriSchemeHttps))
                {
                    WebClient client = new WebClient();

                    featuredImageClient = client;

                    client.DownloadDataCompleted +=
                        (sender, e) =>
                        {
                            WebClient completedClient =
                                sender as WebClient;

                            if (completedClient != null)
                            {
                                if (featuredImageClient == completedClient)
                                    featuredImageClient = null;

                                completedClient.Dispose();
                            }

                            if (isClosing ||
                                requestId != featuredImageRequestId ||
                                e.Cancelled ||
                                e.Error != null ||
                                e.Result == null)
                                return;

                            try
                            {
                                using (MemoryStream stream =
                                    new MemoryStream(e.Result))
                                using (Image downloaded =
                                    Image.FromStream(stream))
                                {
                                    Bitmap image =
                                        new Bitmap(downloaded);

                                    if (requestId != featuredImageRequestId ||
                                        isClosing)
                                    {
                                        image.Dispose();
                                        return;
                                    }

                                    SetFeaturedImage(image);
                                }
                            }
                            catch
                            {
                                if (requestId == featuredImageRequestId)
                                    ClearFeaturedImage();
                            }
                        };

                    client.DownloadDataAsync(
                        imageUri,
                        requestId);

                    return;
                }

                string fullPath = Path.IsPathRooted(imagePath)
                    ? imagePath
                    : Path.Combine(
                        Application.StartupPath,
                        imagePath);

                if (!File.Exists(fullPath))
                    return;

                using (Image original = Image.FromFile(fullPath))
                {
                    SetFeaturedImage(new Bitmap(original));
                }
            }
            catch
            {
                if (requestId == featuredImageRequestId)
                    ClearFeaturedImage();
            }
        }

        private void SetFeaturedImage(Bitmap image)
        {
            if (image == null)
                return;

            if (isClosing)
            {
                image.Dispose();
                return;
            }

            DisposeFeaturedImages();

            featuredOriginalImage = image;

            RenderFeaturedCover();
        }

        private void RenderFeaturedCover()
        {
            if (picFeaturedGame == null ||
                featuredOriginalImage == null ||
                isClosing)
                return;

            int targetWidth = picFeaturedGame.ClientSize.Width;
            int targetHeight = picFeaturedGame.ClientSize.Height;

            if (targetWidth <= 0 || targetHeight <= 0)
                return;

            Size targetSize =
                new Size(targetWidth, targetHeight);

            if (featuredCoverImage != null &&
                featuredCoverSize == targetSize)
                return;

            Bitmap newCover = null;

            try
            {
                newCover = CreateCoverImage(
                    featuredOriginalImage,
                    targetWidth,
                    targetHeight);

                Image oldImage = picFeaturedGame.Image;

                picFeaturedGame.Image = newCover;
                picFeaturedGame.SizeMode =
                    PictureBoxSizeMode.StretchImage;

                featuredCoverImage = newCover;
                featuredCoverSize = targetSize;

                newCover = null;

                if (oldImage != null &&
                    oldImage != featuredOriginalImage &&
                    oldImage != featuredCoverImage)
                {
                    oldImage.Dispose();
                }

                picFeaturedGame.Invalidate();
            }
            catch
            {
                if (newCover != null)
                    newCover.Dispose();
            }
        }

        private Bitmap CreateCoverImage(
            Image source,
            int targetWidth,
            int targetHeight)
        {
            if (source == null ||
                targetWidth <= 0 ||
                targetHeight <= 0)
                return null;

            Bitmap result =
                new Bitmap(targetWidth, targetHeight);

            float sourceRatio =
                (float)source.Width / source.Height;

            float targetRatio =
                (float)targetWidth / targetHeight;

            Rectangle sourceRectangle;

            if (sourceRatio > targetRatio)
            {
                int cropWidth =
                    (int)(source.Height * targetRatio);

                int cropX =
                    (source.Width - cropWidth) / 2;

                sourceRectangle = new Rectangle(
                    cropX,
                    0,
                    cropWidth,
                    source.Height);
            }
            else
            {
                int cropHeight =
                    (int)(source.Width / targetRatio);

                int cropY =
                    (source.Height - cropHeight) / 2;

                sourceRectangle = new Rectangle(
                    0,
                    cropY,
                    source.Width,
                    cropHeight);
            }

            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

                graphics.SmoothingMode =
                    SmoothingMode.HighQuality;

                graphics.PixelOffsetMode =
                    PixelOffsetMode.HighQuality;

                graphics.CompositingQuality =
                    CompositingQuality.HighQuality;

                graphics.DrawImage(
                    source,
                    new Rectangle(
                        0,
                        0,
                        targetWidth,
                        targetHeight),
                    sourceRectangle,
                    GraphicsUnit.Pixel);
            }

            return result;
        }

        private void DisposeFeaturedImages()
        {
            Image oldPictureImage = picFeaturedGame.Image;

            picFeaturedGame.Image = null;

            if (oldPictureImage != null &&
                oldPictureImage != featuredOriginalImage &&
                oldPictureImage != featuredCoverImage)
            {
                oldPictureImage.Dispose();
            }

            if (featuredCoverImage != null)
            {
                featuredCoverImage.Dispose();
                featuredCoverImage = null;
            }

            if (featuredOriginalImage != null)
            {
                featuredOriginalImage.Dispose();
                featuredOriginalImage = null;
            }

            featuredCoverSize = Size.Empty;
        }

        private void ClearFeaturedImage()
        {
            DisposeFeaturedImages();

            picFeaturedGame.Invalidate();
        }

        private void CancelFeaturedImageRequest()
        {
            if (featuredImageClient == null)
                return;

            WebClient client = featuredImageClient;
            featuredImageClient = null;

            try
            {
                client.CancelAsync();
            }
            catch
            {
            }
        }

        private void ClearFeaturedGame()
        {
            featuredImageRequestId++;

            CancelFeaturedImageRequest();

            ClearFeaturedImage();

            lblGameTitle.Text = "No games available";
            lblGameRating.Text = "";
            lblGamePrice.Text = "";

            pnlTagRPG.Visible = false;
            pnlTagAction.Visible = false;
            pnlTagAdventure.Visible = false;

            UpdateFeaturedIndicators();
        }

        private void PopulateGameCarousel(
            FlowLayoutPanel flowPanel,
            List<Game> games)
        {
            if (flowPanel == null)
                return;

            foreach (Control control in flowPanel.Controls)
            {
                DisposeControlImages(control);
                control.Dispose();
            }

            flowPanel.Controls.Clear();

            ConfigureHorizontalScrolling(flowPanel);

            if (games == null)
            {
                UpdateArrowsForFlow(flowPanel);
                return;
            }

            foreach (Game game in games)
            {
                if (game == null)
                    continue;

                Panel card = CreateGameCard(game);

                flowPanel.Controls.Add(card);

                Panel carouselPanel =
                    flowPanel == flpGames
                        ? pnlDiscover
                        : pnlRecommended;

                Button leftButton =
                    flowPanel == flpGames
                        ? btnDiscoverLeft
                        : btnRecommendedLeft;

                Button rightButton =
                    flowPanel == flpGames
                        ? btnDiscoverRight
                        : btnRecommendedRight;

                AttachCardHoverEvents(
                    card,
                    carouselPanel,
                    flowPanel,
                    leftButton,
                    rightButton);
            }

            flowPanel.AutoScrollPosition =
                new Point(0, 0);

            flowPanel.PerformLayout();

            UpdateArrowsForFlow(flowPanel);
        }

        private Panel CreateGameCard(Game game)
        {
            Panel card = new Panel
            {
                Size = new Size(
                    GameCardWidth,
                    GameCardHeight),

                BackColor = Color.FromArgb(35, 29, 43),

                Margin = new Padding(5),

                Cursor = Cursors.Hand
            };

            PictureBox cover = new PictureBox
            {
                Location = new Point(0, 0),

                Size = new Size(
                    GameCoverWidth,
                    GameCoverHeight),

                SizeMode = PictureBoxSizeMode.StretchImage,

                BackColor = Color.FromArgb(25, 19, 34),

                Cursor = Cursors.Hand
            };

            Label name = new Label
            {
                Location = new Point(10, 266),
                Size = new Size(330, 42),

                ForeColor = Color.White,
                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI", 12F, FontStyle.Bold),

                Text = game.GameName,

                AutoEllipsis = true,

                TextAlign = ContentAlignment.MiddleLeft,

                Cursor = Cursors.Hand
            };

            string ratingText =
                game.RatingCount > 0
                    ? "★ " +
                      game.AverageRating.ToString("0.0") +
                      " / 5"
                    : "No ratings yet";

            Label rating = new Label
            {
                Location = new Point(10, 315),
                Size = new Size(330, 30),

                ForeColor = Color.FromArgb(255, 190, 90),
                BackColor = Color.Transparent,

                Font = new Font("Segoe UI", 10F),

                Text = ratingText,

                TextAlign = ContentAlignment.MiddleLeft,

                Cursor = Cursors.Hand
            };

            Label price = new Label
            {
                Location = new Point(10, 355),
                Size = new Size(330, 32),

                ForeColor = Color.White,
                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI", 12F, FontStyle.Bold),

                Text = game.Price == 0 ? "Free" : "$" + game.Price.ToString("0.00"),

                TextAlign = ContentAlignment.MiddleLeft,

                Cursor = Cursors.Hand
            };

            LoadPicture(cover, game.CoverImage);

            EventHandler openDetails = (sender, e) =>
            {
                OpenGameDetails(game, true);
            };

            card.Click += openDetails;
            cover.Click += openDetails;
            name.Click += openDetails;
            rating.Click += openDetails;
            price.Click += openDetails;

            card.Controls.Add(cover);
            card.Controls.Add(name);
            card.Controls.Add(rating);
            card.Controls.Add(price);

            return card;
        }

        private void LoadPicture(
            PictureBox pictureBox,
            string imagePath)
        {
            ClearPicture(pictureBox);

            if (pictureBox == null ||
                pictureBox.IsDisposed ||
                string.IsNullOrWhiteSpace(imagePath))
                return;

            try
            {
                if (Uri.TryCreate(
                    imagePath,
                    UriKind.Absolute,
                    out Uri imageUri) &&
                    (imageUri.Scheme == Uri.UriSchemeHttp ||
                     imageUri.Scheme == Uri.UriSchemeHttps))
                {
                    WebClient client = new WebClient();

                    pictureBox.Tag = client;

                    client.DownloadDataCompleted += (sender, e) =>
                    {
                        WebClient completedClient =
                            sender as WebClient;

                        if (completedClient != null)
                            completedClient.Dispose();

                        if (pictureBox.IsDisposed ||
                            pictureBox.Disposing ||
                            pictureBox.Tag != completedClient)
                            return;

                        pictureBox.Tag = null;

                        if (e.Cancelled ||
                            e.Error != null ||
                            e.Result == null)
                            return;

                        try
                        {
                            using (MemoryStream stream =
                                new MemoryStream(e.Result))
                            using (Image downloaded =
                                Image.FromStream(stream))
                            {
                                Bitmap croppedImage = CreateCoverImage(
                                    downloaded,
                                    GameCoverWidth,
                                    GameCoverHeight);

                                if (pictureBox.IsDisposed ||
                                    pictureBox.Disposing)
                                {
                                    croppedImage?.Dispose();
                                    return;
                                }

                                ClearPicture(pictureBox);

                                pictureBox.Image = croppedImage;
                            }
                        }
                        catch
                        {
                            ClearPicture(pictureBox);
                        }
                    };

                    client.DownloadDataAsync(imageUri);

                    return;
                }

                string fullPath = Path.IsPathRooted(imagePath)
                    ? imagePath
                    : Path.Combine(
                        Application.StartupPath,
                        imagePath);

                if (File.Exists(fullPath))
                {
                    using (Image original =
                        Image.FromFile(fullPath))
                    {
                        pictureBox.Image = CreateCoverImage(
                            original,
                            GameCoverWidth,
                            GameCoverHeight);
                    }
                }
            }
            catch
            {
                ClearPicture(pictureBox);
            }
        }

        private void ClearPicture(PictureBox pictureBox)
        {
            if (pictureBox == null)
                return;

            if (pictureBox.Tag is WebClient client)
            {
                pictureBox.Tag = null;

                try
                {
                    client.CancelAsync();
                }
                catch
                {
                }

                client.Dispose();
            }

            if (pictureBox.Image != null)
            {
                Image oldImage = pictureBox.Image;

                pictureBox.Image = null;

                oldImage.Dispose();
            }
        }

        private void DisposeControlImages(Control control)
        {
            if (control is PictureBox pictureBox)
                ClearPicture(pictureBox);

            foreach (Control child in control.Controls)
                DisposeControlImages(child);
        }

        private void ConfigureHorizontalScrolling(
    FlowLayoutPanel flowPanel)
        {
            if (flowPanel == null)
                return;

            flowPanel.AutoScroll = true;
            flowPanel.WrapContents = false;
            flowPanel.FlowDirection = FlowDirection.LeftToRight;
            flowPanel.Padding = new Padding(0);
            flowPanel.AutoScrollMinSize = Size.Empty;

            flowPanel.HorizontalScroll.Enabled = true;
            flowPanel.VerticalScroll.Enabled = false;
        }

        private void ScrollGames(
            FlowLayoutPanel flowPanel,
            int amount)
        {
            if (flowPanel == null)
                return;

            int currentPosition =
                -flowPanel.AutoScrollPosition.X;

            int maximumPosition =
                GetMaximumScrollPosition(flowPanel);

            int newPosition =
                currentPosition + amount;

            newPosition =
                Math.Max(0, Math.Min(
                    newPosition,
                    maximumPosition));

            flowPanel.AutoScrollPosition =
                new Point(newPosition, 0);

            UpdateArrowsForFlow(flowPanel);
        }

        private int GetMaximumScrollPosition(
            FlowLayoutPanel flowPanel)
        {
            if (flowPanel == null)
                return 0;

            flowPanel.PerformLayout();

            int maximumPosition =
                flowPanel.DisplayRectangle.Width -
                flowPanel.ClientSize.Width;

            return Math.Max(0, maximumPosition);
        }

        private void SetupCarouselHover(
            Panel carouselPanel,
            FlowLayoutPanel flowPanel,
            Button leftButton,
            Button rightButton)
        {
            leftButton.Visible = false;
            rightButton.Visible = false;

            carouselPanel.MouseEnter += (s, e) =>
                UpdateCarouselArrows(
                    carouselPanel,
                    flowPanel,
                    leftButton,
                    rightButton);

            carouselPanel.MouseLeave += (s, e) =>
                HideArrowsIfPointerOutside(
                    carouselPanel,
                    leftButton,
                    rightButton);

            flowPanel.MouseEnter += (s, e) =>
                UpdateCarouselArrows(
                    carouselPanel,
                    flowPanel,
                    leftButton,
                    rightButton);

            flowPanel.MouseLeave += (s, e) =>
                HideArrowsIfPointerOutside(
                    carouselPanel,
                    leftButton,
                    rightButton);

            leftButton.MouseEnter += (s, e) =>
                UpdateCarouselArrows(
                    carouselPanel,
                    flowPanel,
                    leftButton,
                    rightButton);

            rightButton.MouseEnter += (s, e) =>
                UpdateCarouselArrows(
                    carouselPanel,
                    flowPanel,
                    leftButton,
                    rightButton);

            leftButton.MouseLeave += (s, e) =>
                HideArrowsIfPointerOutside(
                    carouselPanel,
                    leftButton,
                    rightButton);

            rightButton.MouseLeave += (s, e) =>
                HideArrowsIfPointerOutside(
                    carouselPanel,
                    leftButton,
                    rightButton);
        }

        private void AttachCardHoverEvents(
            Control control,
            Panel carouselPanel,
            FlowLayoutPanel flowPanel,
            Button leftButton,
            Button rightButton)
        {
            control.MouseEnter += (s, e) =>
                UpdateCarouselArrows(
                    carouselPanel,
                    flowPanel,
                    leftButton,
                    rightButton);

            control.MouseLeave += (s, e) =>
                HideArrowsIfPointerOutside(
                    carouselPanel,
                    leftButton,
                    rightButton);

            foreach (Control child in control.Controls)
            {
                AttachCardHoverEvents(
                    child,
                    carouselPanel,
                    flowPanel,
                    leftButton,
                    rightButton);
            }
        }

        private void HideArrowsIfPointerOutside(
            Panel carouselPanel,
            Button leftButton,
            Button rightButton)
        {
            if (carouselPanel == null ||
                carouselPanel.IsDisposed)
                return;

            Rectangle bounds =
                carouselPanel.RectangleToScreen(
                    carouselPanel.ClientRectangle);

            if (!bounds.Contains(Cursor.Position))
            {
                leftButton.Visible = false;
                rightButton.Visible = false;
            }
        }

        private void UpdateCarouselArrows(
            Panel carouselPanel,
            FlowLayoutPanel flowPanel,
            Button leftButton,
            Button rightButton)
        {
            if (carouselPanel == null ||
                flowPanel == null ||
                !carouselPanel.Visible ||
                !flowPanel.Visible)
                return;

            Rectangle bounds =
                carouselPanel.RectangleToScreen(
                    carouselPanel.ClientRectangle);

            bool pointerInside =
                bounds.Contains(Cursor.Position);

            if (!pointerInside)
            {
                leftButton.Visible = false;
                rightButton.Visible = false;
                return;
            }

            int maximumPosition =
                GetMaximumScrollPosition(flowPanel);

            int currentPosition =
                -flowPanel.AutoScrollPosition.X;

            leftButton.Visible =
                currentPosition > 0;

            rightButton.Visible =
                currentPosition < maximumPosition;

            leftButton.BringToFront();
            rightButton.BringToFront();
        }

        private void UpdateArrowsForFlow(
            FlowLayoutPanel flowPanel)
        {
            if (flowPanel == flpGames)
            {
                UpdateCarouselArrows(
                    pnlDiscover,
                    flpGames,
                    btnDiscoverLeft,
                    btnDiscoverRight);
            }
            else if (flowPanel == flpRecommended)
            {
                UpdateCarouselArrows(
                    pnlRecommended,
                    flpRecommended,
                    btnRecommendedLeft,
                    btnRecommendedRight);
            }
        }

        private void UpdateDiscoverArrows()
        {
            UpdateCarouselArrows(
                pnlDiscover,
                flpGames,
                btnDiscoverLeft,
                btnDiscoverRight);
        }

        private void UpdateRecommendedArrows()
        {
            UpdateCarouselArrows(
                pnlRecommended,
                flpRecommended,
                btnRecommendedLeft,
                btnRecommendedRight);
        }

        private void ConfigureArrowButton(
            Button button,
            string controlName,
            string symbol)
        {
            button.BackColor =
                Color.FromArgb(55, 38, 48);

            button.Cursor = Cursors.Hand;

            button.FlatAppearance.BorderSize = 0;

            button.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(105, 65, 105);

            button.FlatStyle = FlatStyle.Flat;

            button.Font = new Font(
                "Segoe UI", 24F, FontStyle.Bold);

            button.ForeColor = Color.White;

            button.Size = new Size(38, 55);

            button.Name = controlName;

            button.Text = symbol;

            button.UseVisualStyleBackColor = false;

            button.Visible = false;
        }

        private void PositionArrowButtons()
        {
            if (pnlDiscover == null ||
                pnlRecommended == null)
                return;

            int discoverY =
                Math.Max(
                    0,
                    (flpGames.Top +
                     (flpGames.Height / 2)) - 27);

            int recommendedY =
                Math.Max(
                    0,
                    (flpRecommended.Top +
                     (flpRecommended.Height / 2)) - 27);

            btnDiscoverLeft.Location =
                new Point(10, discoverY);

            btnDiscoverRight.Location =
                new Point(
                    pnlDiscover.ClientSize.Width - 48,
                    discoverY);

            btnRecommendedLeft.Location =
                new Point(10, recommendedY);

            btnRecommendedRight.Location =
                new Point(
                    pnlRecommended.ClientSize.Width - 48,
                    recommendedY);

            btnDiscoverLeft.BringToFront();
            btnDiscoverRight.BringToFront();

            btnRecommendedLeft.BringToFront();
            btnRecommendedRight.BringToFront();
        }

        private void ShowPage(UserControl page)
        {
            if (page == null)
                return;

            if (currentPage != null &&
                currentPage.GetType() == page.GetType())
            {
                page.Dispose();
                return;
            }

            if (currentPage != null)
            {
                pnlMain.Controls.Remove(currentPage);
                currentPage.Dispose();
            }

            currentPage = page;
            currentPage.Dock = DockStyle.Fill;

            pnlMain.Controls.Add(currentPage);
            currentPage.BringToFront();
        }

        private void HideHome()
        {
            pnlTopBar.Visible = false;
            pnlFeatured.Visible = false;
            pnlDiscover.Visible = false;
            pnlRecommended.Visible = false;

            if (featuredTimer != null)
                featuredTimer.Stop();
        }

        private void ShowHome()
        {
            if (currentPage != null)
            {
                pnlMain.Controls.Remove(currentPage);
                currentPage.Dispose();
                currentPage = null;
            }

            pnlTopBar.Visible = true;
            pnlFeatured.Visible = true;
            pnlDiscover.Visible = true;
            pnlRecommended.Visible = true;

            pnlTopBar.BringToFront();
            pnlFeatured.BringToFront();
            pnlDiscover.BringToFront();
            pnlRecommended.BringToFront();

            if (featuredGames.Count > 1 &&
                featuredTimer != null)
            {
                featuredTimer.Start();
            }

            PositionArrowButtons();

            UpdateDiscoverArrows();
            UpdateRecommendedArrows();
        }

        private void SetActiveButton(Button activeButton)
        {
            Button[] buttons =
            {
                btnHome,
                btnStore,
                btnLibrary,
                btnWishlist
            };

            foreach (Button button in buttons)
            {
                button.BackColor =
                    Color.FromArgb(29, 18, 25);

                button.ForeColor =
                    Color.FromArgb(220, 215, 230);

                button.Font = new Font(
                    "Segoe UI", 11F, FontStyle.Regular);
            }

            activeButton.BackColor =
                Color.FromArgb(160, 65, 105);

            activeButton.ForeColor = Color.White;

            activeButton.Font = new Font(
                "Segoe UI", 11F, FontStyle.Bold);
        }

        public void OpenGameDetails(Game game)
        {
            OpenGameDetails(game, false);
        }

        private void OpenGameDetails(
    Game game,
    bool returnToHome)
        {
            if (game == null)
                return;

            HideHome();

            GameDetailsPage detailsPage =
                new GameDetailsPage(
                    game,
                    CurrentUserId,
                    CurrentDisplayName);

            detailsPage.BackRequested += () =>
            {
                if (returnToHome)
                {
                    ShowHome();
                    SetActiveButton(btnHome);
                }
                else
                {
                    var storePage = new StorePage(CurrentDisplayName);
                    storePage.LogoutRequested += btnLogout_Click;

                    ShowPage(storePage);
                    SetActiveButton(btnStore);
                }
            };

            ShowPage(detailsPage);
        }

        private void btnHome_Click(
            object sender,
            EventArgs e)
        {
            ShowHome();
            SetActiveButton(btnHome);
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            HideHome();

            var page = new StorePage(CurrentDisplayName);
            page.LogoutRequested += btnLogout_Click;

            ShowPage(page);
            SetActiveButton(btnStore);
        }

        private void btnLibrary_Click(object sender, EventArgs e)
        {
            HideHome();

            var page = new LibraryPage(CurrentUserId, CurrentDisplayName);
            page.LogoutRequested += btnLogout_Click;

            ShowPage(page);
            SetActiveButton(btnLibrary);
        }

        private void btnWishlist_Click(object sender, EventArgs e)
        {
            HideHome();

            var page = new WishlistPage(CurrentUserId, CurrentDisplayName);
            page.LogoutRequested += btnLogout_Click;

            ShowPage(page);
            SetActiveButton(btnWishlist);
        }

        private void btnAddGame_Click(
            object sender,
            EventArgs e)
        {
            HideHome();
            ShowPage(new AddGamePage());
            SetActiveButton(btnAddGame);
        }

        private void textBox1_TextChanged(
            object sender,
            EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            List<Game> filteredGames;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredGames = allGames;
            }
            else
            {
                filteredGames = allGames.FindAll(game =>
                    game != null &&
                    !string.IsNullOrEmpty(game.GameName) &&
                    game.GameName.IndexOf(
                        searchText,
                        StringComparison.OrdinalIgnoreCase) >= 0);
            }

            PopulateGameCarousel(
                flpGames,
                filteredGames);
        }


        private void CreateLogoutButton()
        {
            btnLogout = new Button
            {
                Name = "btnLogout",
                Text = "Logout",
                Size = new Size(105, 42),
                Location = new Point(535, 11),
                Anchor = AnchorStyles.Top,
                BackColor = Color.FromArgb(65, 35, 45),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };

            btnLogout.FlatAppearance.BorderSize = 0;

            btnLogout.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(145, 55, 75);

            btnLogout.Click += btnLogout_Click;

            pnlTopBar.Controls.Add(btnLogout);
            btnLogout.BringToFront();
        }

        private void PositionHeaderControls()
        {
            if (pnlTopBar == null ||
                pnlUser == null ||
                btnLogout == null)
                return;

            int rightMargin = 5;
            int gap = 10;

            int userX = pnlTopBar.ClientSize.Width
                        - pnlUser.Width
                        - rightMargin;

            int logoutX = userX
                          - btnLogout.Width
                          - gap;

            userX = Math.Max(0, userX);
            logoutX = Math.Max(0, logoutX);

            pnlUser.Anchor = AnchorStyles.Top;
            btnLogout.Anchor = AnchorStyles.Top;

            pnlUser.Location = new Point(userX, 10);
            btnLogout.Location = new Point(logoutX, 11);

            pnlUser.BringToFront();
            btnLogout.BringToFront();
        }

        private void btnLogout_Click(
            object sender,
            EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                appContext.ShowAuthForm();
        }

        protected override void OnFormClosed(
            FormClosedEventArgs e)
        {
            isClosing = true;

            if (featuredTimer != null)
            {
                featuredTimer.Stop();
                featuredTimer.Dispose();
                featuredTimer = null;
            }

            featuredImageRequestId++;

            CancelFeaturedImageRequest();

            ClearFeaturedImage();

            foreach (Control control in flpGames.Controls)
                DisposeControlImages(control);

            foreach (Control control in flpRecommended.Controls)
                DisposeControlImages(control);

            base.OnFormClosed(e);
        }
    }
}
