
using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace alSati_store
{
    public partial class GameRepository
    {
        public bool DeactivateGame(int gameId)
        {
            const string query = @"
                UPDATE GAMES
                SET GAME_STATUS = 'REMOVED'
                WHERE GAME_ID = :p_game_id
                  AND GAME_STATUS = 'ACTIVE'";

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;
                    command.Parameters.Add(
                        "p_game_id",
                        OracleDbType.Int32).Value = gameId;

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateGame(
            Game game,
            List<string> screenshotPaths,
            List<string> categoryNames,
            bool replaceScreenshots)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            screenshotPaths ??= new List<string>();
            categoryNames ??= new List<string>();

            using (OracleConnection connection = Database.GetConnection())
            {
                connection.Open();

                using (OracleTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        const string updateQuery = @"
                            UPDATE GAMES
                            SET
                                GAME_NAME = :p_name,
                                DESCRIPTION = :p_description,
                                PRICE = :p_price,
                                COVER_IMAGE = :p_cover,
                                ICON_IMAGE = :p_icon,
                                DEVELOPER = :p_developer,
                                PUBLISHER = :p_publisher,
                                RELEASE_DATE = :p_release_date
                            WHERE GAME_ID = :p_game_id
                              AND GAME_STATUS = 'ACTIVE'";

                        using (OracleCommand command = new OracleCommand(updateQuery, connection))
                        {
                            command.Transaction = transaction;
                            command.BindByName = true;

                            command.Parameters.Add("p_name", OracleDbType.Varchar2).Value = game.GameName;
                            command.Parameters.Add("p_description", OracleDbType.Clob).Value =
                                string.IsNullOrEmpty(game.Description) ? (object)DBNull.Value : game.Description;
                            command.Parameters.Add("p_price", OracleDbType.Decimal).Value = game.Price;
                            command.Parameters.Add("p_cover", OracleDbType.Varchar2).Value = game.CoverImage;
                            command.Parameters.Add("p_icon", OracleDbType.Varchar2).Value = game.IconImage;
                            command.Parameters.Add("p_developer", OracleDbType.Varchar2).Value =
                                string.IsNullOrEmpty(game.Developer) ? (object)DBNull.Value : game.Developer;
                            command.Parameters.Add("p_publisher", OracleDbType.Varchar2).Value =
                                string.IsNullOrEmpty(game.Publisher) ? (object)DBNull.Value : game.Publisher;
                            command.Parameters.Add("p_release_date", OracleDbType.Date).Value =
                                game.ReleaseDate.HasValue ? (object)game.ReleaseDate.Value : DBNull.Value;
                            command.Parameters.Add("p_game_id", OracleDbType.Int32).Value = game.GameId;

                            if (command.ExecuteNonQuery() == 0)
                                throw new InvalidOperationException("Game not found or inactive.");
                        }

                        const string deleteCategoriesQuery = @"
                            DELETE FROM GAME_CATEGORIES
                            WHERE GAME_ID = :p_game_id";

                        using (OracleCommand command = new OracleCommand(deleteCategoriesQuery, connection))
                        {
                            command.Transaction = transaction;
                            command.BindByName = true;
                            command.Parameters.Add("p_game_id", OracleDbType.Int32).Value = game.GameId;
                            command.ExecuteNonQuery();
                        }

                        const string insertCategoryQuery = @"
                            INSERT INTO GAME_CATEGORIES (GAME_ID, CATEGORY_ID)
                            SELECT :p_game_id, CATEGORY_ID
                            FROM CATEGORIES
                            WHERE CATEGORY_NAME = :p_category_name";

                        foreach (string categoryName in categoryNames)
                        {
                            using (OracleCommand command = new OracleCommand(insertCategoryQuery, connection))
                            {
                                command.Transaction = transaction;
                                command.BindByName = true;
                                command.Parameters.Add("p_game_id", OracleDbType.Int32).Value = game.GameId;
                                command.Parameters.Add("p_category_name", OracleDbType.Varchar2).Value = categoryName;

                                if (command.ExecuteNonQuery() == 0)
                                    throw new Exception("Category not found: " + categoryName);
                            }
                        }

                        if (replaceScreenshots)
                        {
                            const string deleteImagesQuery = @"
                                DELETE FROM GAME_IMAGES
                                WHERE GAME_ID = :p_game_id";

                            using (OracleCommand command = new OracleCommand(deleteImagesQuery, connection))
                            {
                                command.Transaction = transaction;
                                command.BindByName = true;
                                command.Parameters.Add("p_game_id", OracleDbType.Int32).Value = game.GameId;
                                command.ExecuteNonQuery();
                            }

                            const string insertImageQuery = @"
                                INSERT INTO GAME_IMAGES
                                (GAME_ID, IMAGE_PATH, DISPLAY_ORDER)
                                VALUES
                                (:p_game_id, :p_image_path, :p_display_order)";

                            for (int i = 0; i < screenshotPaths.Count; i++)
                            {
                                using (OracleCommand command = new OracleCommand(insertImageQuery, connection))
                                {
                                    command.Transaction = transaction;
                                    command.BindByName = true;
                                    command.Parameters.Add("p_game_id", OracleDbType.Int32).Value = game.GameId;
                                    command.Parameters.Add("p_image_path", OracleDbType.Varchar2).Value = screenshotPaths[i];
                                    command.Parameters.Add("p_display_order", OracleDbType.Int32).Value = i + 1;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
