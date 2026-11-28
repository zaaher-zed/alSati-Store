
using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace alSati_store
{
    public partial class GameRepository
    {
        public List<Game> GetAllGames()
        {
            List<Game> games = new List<Game>();

            string query = @"
                SELECT
                    G.GAME_ID,
                    G.GAME_NAME,
                    G.DESCRIPTION,
                    G.PRICE,
                    G.COVER_IMAGE,
                    G.ICON_IMAGE,
                    G.DEVELOPER,
                    G.PUBLISHER,
                    G.RELEASE_DATE,
                    G.GAME_STATUS,
                    NVL(R.AVERAGE_RATING, 0) AS AVERAGE_RATING,
                    NVL(R.RATING_COUNT, 0) AS RATING_COUNT
                FROM GAMES G
                LEFT JOIN
                (
                    SELECT
                        GAME_ID,
                        ROUND(AVG(RATING), 2) AS AVERAGE_RATING,
                        COUNT(*) AS RATING_COUNT
                    FROM REVIEWS
                    GROUP BY GAME_ID
                ) R
                    ON G.GAME_ID = R.GAME_ID
                WHERE G.GAME_STATUS = 'ACTIVE'
                ORDER BY G.GAME_ID DESC";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Game game = new Game
                        {
                            GameId = Convert.ToInt32(reader["GAME_ID"]),
                            GameName = reader["GAME_NAME"].ToString(),
                            Description = reader["DESCRIPTION"] == DBNull.Value
                                ? ""
                                : reader["DESCRIPTION"].ToString(),
                            Price = Convert.ToDecimal(reader["PRICE"]),
                            CoverImage = reader["COVER_IMAGE"].ToString(),
                            IconImage = reader["ICON_IMAGE"].ToString(),
                            Developer = reader["DEVELOPER"] == DBNull.Value
                                ? ""
                                : reader["DEVELOPER"].ToString(),
                            Publisher = reader["PUBLISHER"] == DBNull.Value
                                ? ""
                                : reader["PUBLISHER"].ToString(),
                            ReleaseDate = reader["RELEASE_DATE"] == DBNull.Value
                                ? null
                                : Convert.ToDateTime(reader["RELEASE_DATE"]),
                            GameStatus = reader["GAME_STATUS"].ToString(),

                            AverageRating = Convert.ToDecimal(reader["AVERAGE_RATING"]),
                            RatingCount = Convert.ToInt32(reader["RATING_COUNT"])
                        };

                        games.Add(game);
                    }
                }
            }

            return games;
        }


        public List<Game> GetGamesByCategory(string categoryName)
        {
            List<Game> games = new List<Game>();

            string query = @"
                SELECT
                    G.GAME_ID,
                    G.GAME_NAME,
                    G.DESCRIPTION,
                    G.PRICE,
                    G.COVER_IMAGE,
                    G.ICON_IMAGE,
                    G.DEVELOPER,
                    G.PUBLISHER,
                    G.RELEASE_DATE,
                    G.GAME_STATUS,
                    NVL(R.AVERAGE_RATING, 0) AS AVERAGE_RATING,
                    NVL(R.RATING_COUNT, 0) AS RATING_COUNT
                FROM GAMES G
                LEFT JOIN
                (
                    SELECT
                        GAME_ID,
                        ROUND(AVG(RATING), 2) AS AVERAGE_RATING,
                        COUNT(*) AS RATING_COUNT
                    FROM REVIEWS
                    GROUP BY GAME_ID
                ) R
                    ON G.GAME_ID = R.GAME_ID
                WHERE G.GAME_STATUS = 'ACTIVE'
                  AND EXISTS
                  (
                      SELECT 1
                      FROM GAME_CATEGORIES GC
                      INNER JOIN CATEGORIES C
                          ON GC.CATEGORY_ID = C.CATEGORY_ID
                      WHERE GC.GAME_ID = G.GAME_ID
                        AND C.CATEGORY_NAME = :p_category_name
                  )
                ORDER BY G.GAME_ID DESC";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;

                    command.Parameters.Add(
                        "p_category_name",
                        OracleDbType.Varchar2
                    ).Value = categoryName;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            games.Add(new Game
                            {
                                GameId = Convert.ToInt32(reader["GAME_ID"]),
                                GameName = reader["GAME_NAME"].ToString(),

                                Description = reader["DESCRIPTION"] == DBNull.Value
                                    ? ""
                                    : reader["DESCRIPTION"] is OracleClob clob
                                        ? clob.Value
                                        : reader["DESCRIPTION"].ToString(),

                                Price = Convert.ToDecimal(reader["PRICE"]),
                                CoverImage = reader["COVER_IMAGE"].ToString(),
                                IconImage = reader["ICON_IMAGE"].ToString(),

                                Developer = reader["DEVELOPER"] == DBNull.Value
                                    ? ""
                                    : reader["DEVELOPER"].ToString(),

                                Publisher = reader["PUBLISHER"] == DBNull.Value
                                    ? ""
                                    : reader["PUBLISHER"].ToString(),

                                ReleaseDate = reader["RELEASE_DATE"] == DBNull.Value
                                    ? null
                                    : Convert.ToDateTime(reader["RELEASE_DATE"]),

                                GameStatus = reader["GAME_STATUS"].ToString(),

                                AverageRating = Convert.ToDecimal(reader["AVERAGE_RATING"]),
                                RatingCount = Convert.ToInt32(reader["RATING_COUNT"])
                            });
                        }
                    }
                }
            }

            return games;
        }




        public List<Game> GetFavoriteGames(int userId)
        {
            List<Game> games = new List<Game>();

            string query = @"
        SELECT
            G.GAME_ID,
            G.GAME_NAME,
            G.DESCRIPTION,
            G.PRICE,
            G.COVER_IMAGE,
            G.ICON_IMAGE,
            G.DEVELOPER,
            G.PUBLISHER,
            G.RELEASE_DATE,
            G.GAME_STATUS,
            NVL(R.AVERAGE_RATING, 0) AS AVERAGE_RATING,
            NVL(R.RATING_COUNT, 0) AS RATING_COUNT
        FROM GAMES G
        INNER JOIN FAVORITES F
            ON G.GAME_ID = F.GAME_ID
        LEFT JOIN (
            SELECT
                GAME_ID,
                AVG(RATING) AS AVERAGE_RATING,
                COUNT(*) AS RATING_COUNT
            FROM REVIEWS
            GROUP BY GAME_ID
        ) R
            ON G.GAME_ID = R.GAME_ID
        WHERE F.USER_ID = :p_user_id
          AND G.GAME_STATUS = 'ACTIVE'
        ORDER BY F.GAME_ID DESC";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;
                    command.Parameters.Add(
                        "p_user_id", OracleDbType.Int32).Value = userId;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Game game = new Game
                            {
                                GameId = Convert.ToInt32(reader["GAME_ID"]),
                                GameName = reader["GAME_NAME"].ToString(),
                                Description = reader["DESCRIPTION"] == DBNull.Value
                                    ? ""
                                    : reader["DESCRIPTION"].ToString(),
                                Price = Convert.ToDecimal(reader["PRICE"]),
                                CoverImage = reader["COVER_IMAGE"].ToString(),
                                IconImage = reader["ICON_IMAGE"].ToString(),
                                Developer = reader["DEVELOPER"] == DBNull.Value
                                    ? ""
                                    : reader["DEVELOPER"].ToString(),
                                Publisher = reader["PUBLISHER"] == DBNull.Value
                                    ? ""
                                    : reader["PUBLISHER"].ToString(),
                                ReleaseDate = reader["RELEASE_DATE"] == DBNull.Value
                                    ? null
                                    : Convert.ToDateTime(reader["RELEASE_DATE"]),
                                GameStatus = reader["GAME_STATUS"].ToString(),

                                AverageRating = Convert.ToDecimal(reader["AVERAGE_RATING"]),
                                RatingCount = Convert.ToInt32(reader["RATING_COUNT"])
                            };

                            games.Add(game);
                        }
                    }
                }
            }

            return games;
        }


        public List<Game> GetLibraryGames(int userId)
        {
            List<Game> games = new List<Game>();

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        G.GAME_ID,
                        G.GAME_NAME,
                        G.DESCRIPTION,
                        G.PRICE,
                        G.COVER_IMAGE,
                        G.ICON_IMAGE,
                        G.DEVELOPER,
                        G.PUBLISHER,
                        G.RELEASE_DATE,
                        G.GAME_STATUS
                    FROM LIBRARY L
                    INNER JOIN GAMES G
                        ON L.GAME_ID = G.GAME_ID
                    WHERE L.USER_ID = :p_user_id
                      AND L.OWNERSHIP_STATUS = 'ACTIVE'
                      AND G.GAME_STATUS = 'ACTIVE'
                    ORDER BY L.ACQUIRED_AT DESC";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(
                        "p_user_id", OracleDbType.Int32).Value = userId;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            games.Add(new Game
                            {
                                GameId = Convert.ToInt32(reader["GAME_ID"]),
                                GameName = reader["GAME_NAME"].ToString(),
                                Description = reader["DESCRIPTION"] == DBNull.Value
                                    ? ""
                                    : reader["DESCRIPTION"].ToString(),
                                Price = Convert.ToDecimal(reader["PRICE"]),
                                CoverImage = reader["COVER_IMAGE"].ToString(),
                                IconImage = reader["ICON_IMAGE"].ToString(),
                                Developer = reader["DEVELOPER"] == DBNull.Value
                                    ? ""
                                    : reader["DEVELOPER"].ToString(),
                                Publisher = reader["PUBLISHER"] == DBNull.Value
                                    ? ""
                                    : reader["PUBLISHER"].ToString(),
                                ReleaseDate = reader["RELEASE_DATE"] == DBNull.Value
                                    ? null
                                    : Convert.ToDateTime(reader["RELEASE_DATE"]),
                                GameStatus = reader["GAME_STATUS"].ToString()
                            });
                        }
                    }
                }
            }

            return games;
        }

        public bool IsGameOwned(int userId, int gameId)
        {
            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT COUNT(*)
                    FROM LIBRARY
                    WHERE USER_ID = :p_user_id
                      AND GAME_ID = :p_game_id
                      AND OWNERSHIP_STATUS = 'ACTIVE'";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(
                        "p_user_id", OracleDbType.Int32).Value = userId;

                    command.Parameters.Add(
                        "p_game_id", OracleDbType.Int32).Value = gameId;

                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        public int AddGame(
            Game game,
            List<string> screenshotPaths,
            List<string> categoryNames)
        {
            if (screenshotPaths == null)
                screenshotPaths = new List<string>();

            if (categoryNames == null)
                categoryNames = new List<string>();

            if (screenshotPaths.Count > 4)
                throw new ArgumentException(
                    "A maximum of 4 screenshots is allowed.");

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string gameQuery = @"
                            INSERT INTO GAMES
                            (
                                GAME_NAME,
                                DESCRIPTION,
                                PRICE,
                                COVER_IMAGE,
                                ICON_IMAGE,
                                DEVELOPER,
                                PUBLISHER,
                                RELEASE_DATE
                            )
                            VALUES
                            (
                                :p_name,
                                :p_description,
                                :p_price,
                                :p_cover,
                                :p_icon,
                                :p_developer,
                                :p_publisher,
                                :p_release_date
                            )
                            RETURNING GAME_ID INTO :p_game_id";

                        int gameId;

                        using (OracleCommand command = new OracleCommand(gameQuery, connection))
                        {
                            command.Transaction = transaction;
                            command.BindByName = true;

                            command.Parameters.Add(
                                "p_name", OracleDbType.Varchar2).Value =
                                game.GameName;

                            command.Parameters.Add(
                                "p_description", OracleDbType.Clob).Value =
                                string.IsNullOrEmpty(game.Description)
                                    ? (object)DBNull.Value
                                    : game.Description;

                            command.Parameters.Add(
                                "p_price", OracleDbType.Decimal).Value =
                                game.Price;

                            command.Parameters.Add(
                                "p_cover", OracleDbType.Varchar2).Value =
                                game.CoverImage;

                            command.Parameters.Add(
                                "p_icon", OracleDbType.Varchar2).Value =
                                game.IconImage;

                            command.Parameters.Add(
                                "p_developer", OracleDbType.Varchar2).Value =
                                string.IsNullOrEmpty(game.Developer)
                                    ? (object)DBNull.Value
                                    : game.Developer;

                            command.Parameters.Add(
                                "p_publisher", OracleDbType.Varchar2).Value =
                                string.IsNullOrEmpty(game.Publisher)
                                    ? (object)DBNull.Value
                                    : game.Publisher;

                            command.Parameters.Add(
                                "p_release_date", OracleDbType.Date).Value =
                                game.ReleaseDate.HasValue
                                    ? (object)game.ReleaseDate.Value
                                    : DBNull.Value;

                            OracleParameter outputParameter =
                                command.Parameters.Add(
                                    "p_game_id", OracleDbType.Int32);

                            outputParameter.Direction = ParameterDirection.Output;

                            command.ExecuteNonQuery();

                            gameId = Convert.ToInt32(
                                outputParameter.Value.ToString());
                        }

                        string imageQuery = @"
                            INSERT INTO GAME_IMAGES
                            (
                                GAME_ID,
                                IMAGE_PATH,
                                DISPLAY_ORDER
                            )
                            VALUES
                            (
                                :p_game_id,
                                :p_image_path,
                                :p_display_order
                            )";

                        for (int i = 0; i < screenshotPaths.Count; i++)
                        {
                            using (OracleCommand command = new OracleCommand(imageQuery, connection))
                            {
                                command.Transaction = transaction;
                                command.BindByName = true;

                                command.Parameters.Add(
                                    "p_game_id", OracleDbType.Int32).Value = gameId;

                                command.Parameters.Add(
                                    "p_image_path", OracleDbType.Varchar2).Value =
                                    screenshotPaths[i];

                                command.Parameters.Add(
                                    "p_display_order", OracleDbType.Int32).Value =
                                    i + 1;

                                command.ExecuteNonQuery();
                            }
                        }

                        string categoryQuery = @"
                            INSERT INTO GAME_CATEGORIES
                            (
                                GAME_ID,
                                CATEGORY_ID
                            )
                            SELECT
                                :p_game_id,
                                CATEGORY_ID
                            FROM CATEGORIES
                            WHERE CATEGORY_NAME = :p_category_name";

                        foreach (string categoryName in categoryNames)
                        {
                            using (OracleCommand command = new OracleCommand(categoryQuery, connection))
                            {
                                command.Transaction = transaction;
                                command.BindByName = true;

                                command.Parameters.Add(
                                    "p_game_id", OracleDbType.Int32).Value = gameId;

                                command.Parameters.Add(
                                    "p_category_name", OracleDbType.Varchar2).Value =
                                    categoryName;

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected == 0)
                                {
                                    throw new Exception(
                                        "Category not found: " + categoryName);
                                }
                            }
                        }

                        transaction.Commit();

                        return gameId;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<string> GetGameImages(int gameId)
        {
            List<string> images = new List<string>();

            string query = @"
                SELECT IMAGE_PATH
                FROM GAME_IMAGES
                WHERE GAME_ID = :p_game_id
                ORDER BY DISPLAY_ORDER";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(
                        "p_game_id", OracleDbType.Int32).Value = gameId;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            images.Add(reader["IMAGE_PATH"].ToString());
                        }
                    }
                }
            }

            return images;
        }

        public List<string> GetGameCategories(int gameId)
        {
            List<string> categories = new List<string>();

            string query = @"
                SELECT C.CATEGORY_NAME
                FROM CATEGORIES C
                INNER JOIN GAME_CATEGORIES GC
                    ON C.CATEGORY_ID = GC.CATEGORY_ID
                WHERE GC.GAME_ID = :p_game_id
                ORDER BY C.CATEGORY_NAME";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(
                        "p_game_id", OracleDbType.Int32).Value = gameId;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(reader["CATEGORY_NAME"].ToString());
                        }
                    }
                }
            }

            return categories;
        }

        public List<string> GetAllCategories()
        {
            List<string> categories = new List<string>();

            string query = @"
                SELECT CATEGORY_NAME
                FROM CATEGORIES
                ORDER BY CATEGORY_NAME";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(reader["CATEGORY_NAME"].ToString());
                    }
                }
            }

            return categories;
        }

        public bool IsGameInFavorites(int userId, int gameId)
        {
            string query = @"
                SELECT COUNT(*)
                FROM FAVORITES
                WHERE USER_ID = :p_user_id
                  AND GAME_ID = :p_game_id";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;

                    command.Parameters.Add(
                        "p_user_id", OracleDbType.Int32).Value = userId;

                    command.Parameters.Add(
                        "p_game_id", OracleDbType.Int32).Value = gameId;

                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        public void AddToFavorites(int userId, int gameId)
        {
            string query = @"
                INSERT INTO FAVORITES (USER_ID, GAME_ID)
                VALUES (:p_user_id, :p_game_id)";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;

                    command.Parameters.Add(
                        "p_user_id", OracleDbType.Int32).Value = userId;

                    command.Parameters.Add(
                        "p_game_id", OracleDbType.Int32).Value = gameId;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveFromFavorites(int userId, int gameId)
        {
            string query = @"
                DELETE FROM FAVORITES
                WHERE USER_ID = :p_user_id
                  AND GAME_ID = :p_game_id";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;

                    command.Parameters.Add(
                        "p_user_id", OracleDbType.Int32).Value = userId;

                    command.Parameters.Add(
                        "p_game_id", OracleDbType.Int32).Value = gameId;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void PurchaseGame(int userId, int gameId)
        {
            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string priceQuery = @"
                            SELECT PRICE
                            FROM GAMES
                            WHERE GAME_ID = :p_game_id
                              AND GAME_STATUS = 'ACTIVE'";

                        decimal purchasePrice;

                        using (OracleCommand command = new OracleCommand(priceQuery, connection))
                        {
                            command.Transaction = transaction;
                            command.BindByName = true;

                            command.Parameters.Add(
                                "p_game_id", OracleDbType.Int32).Value = gameId;

                            object result = command.ExecuteScalar();

                            if (result == null)
                                throw new Exception("This game is not available.");

                            purchasePrice = Convert.ToDecimal(result);
                        }

                        string ownershipQuery = @"
                            SELECT COUNT(*)
                            FROM LIBRARY
                            WHERE USER_ID = :p_user_id
                              AND GAME_ID = :p_game_id
                              AND OWNERSHIP_STATUS = 'ACTIVE'";

                        using (OracleCommand command = new OracleCommand(ownershipQuery, connection))
                        {
                            command.Transaction = transaction;
                            command.BindByName = true;

                            command.Parameters.Add(
                                "p_user_id", OracleDbType.Int32).Value = userId;

                            command.Parameters.Add(
                                "p_game_id", OracleDbType.Int32).Value = gameId;

                            if (Convert.ToInt32(command.ExecuteScalar()) > 0)
                                throw new Exception("You already own this game.");
                        }

                        string purchaseQuery = @"
                            INSERT INTO PURCHASES
                            (
                                USER_ID,
                                GAME_ID,
                                PURCHASE_PRICE,
                                PAYMENT_METHOD,
                                PAYMENT_STATUS
                            )
                            VALUES
                            (
                                :p_user_id,
                                :p_game_id,
                                :p_price,
                                'SIMULATED',
                                'PAID'
                            )
                            RETURNING PURCHASE_ID INTO :p_purchase_id";

                        int purchaseId;

                        using (OracleCommand command = new OracleCommand(purchaseQuery, connection))
                        {
                            command.Transaction = transaction;
                            command.BindByName = true;

                            command.Parameters.Add(
                                "p_user_id", OracleDbType.Int32).Value = userId;

                            command.Parameters.Add(
                                "p_game_id", OracleDbType.Int32).Value = gameId;

                            command.Parameters.Add(
                                "p_price", OracleDbType.Decimal).Value = purchasePrice;

                            OracleParameter outputParameter = command.Parameters.Add(
                                "p_purchase_id", OracleDbType.Int32);

                            outputParameter.Direction = ParameterDirection.Output;

                            command.ExecuteNonQuery();

                            purchaseId = Convert.ToInt32(
                                outputParameter.Value.ToString());
                        }

                        string libraryQuery = @"
                            MERGE INTO LIBRARY L
                            USING
                            (
                                SELECT
                                    :p_user_id AS USER_ID,
                                    :p_game_id AS GAME_ID
                                FROM DUAL
                            ) S
                            ON
                            (
                                L.USER_ID = S.USER_ID
                                AND L.GAME_ID = S.GAME_ID
                            )
                            WHEN MATCHED THEN
                                UPDATE SET
                                    L.PURCHASE_ID = :p_purchase_id,
                                    L.OWNERSHIP_STATUS = 'ACTIVE',
                                    L.ACQUIRED_AT = SYSTIMESTAMP
                            WHEN NOT MATCHED THEN
                                INSERT
                                (
                                    USER_ID,
                                    GAME_ID,
                                    PURCHASE_ID,
                                    OWNERSHIP_STATUS
                                )
                                VALUES
                                (
                                    S.USER_ID,
                                    S.GAME_ID,
                                    :p_purchase_id,
                                    'ACTIVE'
                                )";

                        using (OracleCommand command = new OracleCommand(libraryQuery, connection))
                        {
                            command.Transaction = transaction;
                            command.BindByName = true;

                            command.Parameters.Add(
                                "p_user_id", OracleDbType.Int32).Value = userId;

                            command.Parameters.Add(
                                "p_game_id", OracleDbType.Int32).Value = gameId;

                            command.Parameters.Add(
                                "p_purchase_id", OracleDbType.Int32).Value = purchaseId;

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool CanUserRateGame(int userId, int gameId)
        {
            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT COUNT(*)
                    FROM LIBRARY
                    WHERE USER_ID = :p_user_id
                      AND GAME_ID = :p_game_id
                      AND OWNERSHIP_STATUS = 'ACTIVE'";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(
                        "p_user_id", OracleDbType.Int32).Value = userId;

                    command.Parameters.Add(
                        "p_game_id", OracleDbType.Int32).Value = gameId;

                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        public int? GetUserGameRating(int userId, int gameId)
        {
            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT RATING
                    FROM REVIEWS
                    WHERE USER_ID = :p_user_id
                      AND GAME_ID = :p_game_id";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(
                        "p_user_id", OracleDbType.Int32).Value = userId;

                    command.Parameters.Add(
                        "p_game_id", OracleDbType.Int32).Value = gameId;

                    object result = command.ExecuteScalar();

                    return result == null || result == DBNull.Value
                        ? null
                        : Convert.ToInt32(result);
                }
            }
        }

        public (decimal AverageRating, int RatingCount) GetGameRatingSummary(int gameId)
        {
            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        NVL(AVG(RATING), 0) AS AVERAGE_RATING,
                        COUNT(*) AS RATING_COUNT
                    FROM REVIEWS
                    WHERE GAME_ID = :p_game_id";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(
                        "p_game_id", OracleDbType.Int32).Value = gameId;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (
                                Convert.ToDecimal(reader["AVERAGE_RATING"]),
                                Convert.ToInt32(reader["RATING_COUNT"])
                            );
                        }
                    }
                }
            }

            return (0, 0);
        }

        public void SaveGameRating(int userId, int gameId, int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException(nameof(rating));

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string ownershipQuery = @"
                            SELECT COUNT(*)
                            FROM LIBRARY
                            WHERE USER_ID = :p_user_id
                              AND GAME_ID = :p_game_id
                              AND OWNERSHIP_STATUS = 'ACTIVE'";

                        using (OracleCommand ownershipCommand =
                            new OracleCommand(ownershipQuery, connection))
                        {
                            ownershipCommand.Transaction = transaction;
                            ownershipCommand.Parameters.Add(
                                "p_user_id", OracleDbType.Int32).Value = userId;

                            ownershipCommand.Parameters.Add(
                                "p_game_id", OracleDbType.Int32).Value = gameId;

                            if (Convert.ToInt32(ownershipCommand.ExecuteScalar()) == 0)
                                throw new InvalidOperationException(
                                    "You must own this game to rate it.");
                        }

                        string query = @"
                            MERGE INTO REVIEWS R
                            USING
                            (
                                SELECT
                                    :p_user_id USER_ID,
                                    :p_game_id GAME_ID
                                FROM DUAL
                            ) S
                            ON
                            (
                                R.USER_ID = S.USER_ID
                                AND R.GAME_ID = S.GAME_ID
                            )
                            WHEN MATCHED THEN
                                UPDATE SET
                                    R.RATING = :p_rating,
                                    R.UPDATED_AT = SYSTIMESTAMP
                            WHEN NOT MATCHED THEN
                                INSERT
                                (
                                    USER_ID,
                                    GAME_ID,
                                    RATING
                                )
                                VALUES
                                (
                                    :p_user_id,
                                    :p_game_id,
                                    :p_rating
                                )";

                        using (OracleCommand command = new OracleCommand(query, connection))
                        {
                            command.Transaction = transaction;
                            command.BindByName = true;

                            command.Parameters.Add(
                                "p_user_id", OracleDbType.Int32).Value = userId;

                            command.Parameters.Add(
                                "p_game_id", OracleDbType.Int32).Value = gameId;

                            command.Parameters.Add(
                                "p_rating", OracleDbType.Int32).Value = rating;

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public List<Game> GetLatestGames(int count)
        {
            List<Game> games = new List<Game>();

            if (count <= 0)
                return games;

            string query = @"
                SELECT *
                FROM
                (
                    SELECT
                        G.GAME_ID,
                        G.GAME_NAME,
                        G.DESCRIPTION,
                        G.PRICE,
                        G.COVER_IMAGE,
                        G.ICON_IMAGE,
                        G.DEVELOPER,
                        G.PUBLISHER,
                        G.RELEASE_DATE,
                        G.GAME_STATUS,
                        NVL(R.AVERAGE_RATING, 0) AS AVERAGE_RATING,
                        NVL(R.RATING_COUNT, 0) AS RATING_COUNT
                    FROM GAMES G
                    LEFT JOIN
                    (
                        SELECT
                            GAME_ID,
                            ROUND(AVG(RATING), 2) AS AVERAGE_RATING,
                            COUNT(*) AS RATING_COUNT
                        FROM REVIEWS
                        GROUP BY GAME_ID
                    ) R
                        ON G.GAME_ID = R.GAME_ID
                    WHERE G.GAME_STATUS = 'ACTIVE'
                    ORDER BY G.CREATED_AT DESC, G.GAME_ID DESC
                )
                WHERE ROWNUM <= :p_count";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;
                    command.Parameters.Add(
                        "p_count", OracleDbType.Int32).Value = count;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Game game = ReadGame(reader);

                            game.AverageRating =
                                Convert.ToDecimal(reader["AVERAGE_RATING"]);

                            game.RatingCount =
                                Convert.ToInt32(reader["RATING_COUNT"]);

                            games.Add(game);
                        }
                    }
                }
            }

            return games;
        }


        public List<Game> GetTopRatedGames(int count)
        {
            List<Game> games = new List<Game>();

            if (count <= 0)
                return games;

            string query = @"
                SELECT *
                FROM
                (
                    SELECT
                        G.GAME_ID,
                        G.GAME_NAME,
                        G.DESCRIPTION,
                        G.PRICE,
                        G.COVER_IMAGE,
                        G.ICON_IMAGE,
                        G.DEVELOPER,
                        G.PUBLISHER,
                        G.RELEASE_DATE,
                        G.GAME_STATUS,
                        R.AVERAGE_RATING,
                        R.RATING_COUNT
                    FROM GAMES G
                    INNER JOIN
                    (
                        SELECT
                            GAME_ID,
                            ROUND(AVG(RATING), 2) AS AVERAGE_RATING,
                            COUNT(*) AS RATING_COUNT
                        FROM REVIEWS
                        GROUP BY GAME_ID
                        HAVING COUNT(*) >= 3
                    ) R
                        ON G.GAME_ID = R.GAME_ID
                    WHERE G.GAME_STATUS = 'ACTIVE'
                    ORDER BY
                        R.AVERAGE_RATING DESC,
                        R.RATING_COUNT DESC,
                        G.GAME_ID DESC
                )
                WHERE ROWNUM <= :p_count";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;
                    command.Parameters.Add(
                        "p_count", OracleDbType.Int32).Value = count;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Game game = ReadGame(reader);

                            game.AverageRating =
                                Convert.ToDecimal(reader["AVERAGE_RATING"]);

                            game.RatingCount =
                                Convert.ToInt32(reader["RATING_COUNT"]);

                            games.Add(game);
                        }
                    }
                }
            }

            return games;
        }

        public List<Game> GetBestSellingGames(int count)
        {
            List<Game> games = new List<Game>();

            if (count <= 0)
                return games;

            string query = @"
                SELECT *
                FROM
                (
                    SELECT
                        G.GAME_ID,
                        G.GAME_NAME,
                        G.DESCRIPTION,
                        G.PRICE,
                        G.COVER_IMAGE,
                        G.ICON_IMAGE,
                        G.DEVELOPER,
                        G.PUBLISHER,
                        G.RELEASE_DATE,
                        G.GAME_STATUS,
                        NVL(R.AVERAGE_RATING, 0) AS AVERAGE_RATING,
                        NVL(R.RATING_COUNT, 0) AS RATING_COUNT,
                        P.PURCHASE_COUNT
                    FROM GAMES G
                    INNER JOIN
                    (
                        SELECT
                            GAME_ID,
                            COUNT(PURCHASE_ID) AS PURCHASE_COUNT
                        FROM PURCHASES
                        WHERE PAYMENT_STATUS = 'PAID'
                        GROUP BY GAME_ID
                    ) P
                        ON G.GAME_ID = P.GAME_ID
                    LEFT JOIN
                    (
                        SELECT
                            GAME_ID,
                            ROUND(AVG(RATING), 2) AS AVERAGE_RATING,
                            COUNT(*) AS RATING_COUNT
                        FROM REVIEWS
                        GROUP BY GAME_ID
                    ) R
                        ON G.GAME_ID = R.GAME_ID
                    WHERE G.GAME_STATUS = 'ACTIVE'
                    ORDER BY
                        P.PURCHASE_COUNT DESC,
                        G.GAME_ID DESC
                )
                WHERE ROWNUM <= :p_count";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;
                    command.Parameters.Add(
                        "p_count", OracleDbType.Int32).Value = count;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Game game = ReadGame(reader);

                            game.AverageRating =
                                Convert.ToDecimal(reader["AVERAGE_RATING"]);

                            game.RatingCount =
                                Convert.ToInt32(reader["RATING_COUNT"]);

                            game.PurchaseCount =
                                Convert.ToInt32(reader["PURCHASE_COUNT"]);

                            games.Add(game);
                        }
                    }
                }
            }

            return games;
        }

        private Game ReadGame(OracleDataReader reader)
        {
            return new Game
            {
                GameId = Convert.ToInt32(reader["GAME_ID"]),
                GameName = reader["GAME_NAME"].ToString(),

                Description = reader["DESCRIPTION"] == DBNull.Value
                    ? ""
                    : reader["DESCRIPTION"].ToString(),

                Price = Convert.ToDecimal(reader["PRICE"]),

                CoverImage = reader["COVER_IMAGE"] == DBNull.Value
                    ? ""
                    : reader["COVER_IMAGE"].ToString(),

                IconImage = reader["ICON_IMAGE"] == DBNull.Value
                    ? ""
                    : reader["ICON_IMAGE"].ToString(),

                Developer = reader["DEVELOPER"] == DBNull.Value
                    ? ""
                    : reader["DEVELOPER"].ToString(),

                Publisher = reader["PUBLISHER"] == DBNull.Value
                    ? ""
                    : reader["PUBLISHER"].ToString(),

                ReleaseDate = reader["RELEASE_DATE"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(reader["RELEASE_DATE"]),

                GameStatus = reader["GAME_STATUS"].ToString()
            };
        }
    }
}
