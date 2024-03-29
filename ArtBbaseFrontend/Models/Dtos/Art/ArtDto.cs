﻿namespace ArtBbaseFrontend.Models.Dtos.Art
{
    public class ArtDto
    {
        public Guid Id { get; set; }
        public Guid SellerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime StopTime { get; set; }
        public int StartPrice { get; set; }
        public double HighestBid { get; set; } = 0;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";
    }
}
