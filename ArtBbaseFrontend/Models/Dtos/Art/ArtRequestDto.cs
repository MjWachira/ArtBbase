﻿namespace ArtBbaseFrontend.Models.Dtos.Art
{
    public class ArtRequestDto
    {
        public Guid SellerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime StopTime { get; set; }
        public double StartPrice { get; set; }
        public double HighestBid { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}