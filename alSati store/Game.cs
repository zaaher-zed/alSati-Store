using System;

namespace alSati_store
{
    public class Game
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CoverImage { get; set; }
        public string IconImage { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string GameStatus { get; set; }

        public decimal AverageRating { get; set; }
        public int RatingCount { get; set; }

        public int PurchaseCount { get; set; }
    }
}