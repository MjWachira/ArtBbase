﻿namespace ArtBbaseFrontend.Models.Dtos.Order
{
    public class StripeRequestDto
    {
        public string? StripeSessionUrl { get; set; }
        public string? StripeSessionId { get; set; }
        public string ApprovedUrl { get; set; } 
        public string CancelUrl { get; set; } 
        public Guid OrderId { get; set; }
    }
}
