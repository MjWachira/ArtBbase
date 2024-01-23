﻿using System.ComponentModel.DataAnnotations;

namespace ArtBbaseFrontend.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int Price { get; set; }
        [Required]
        public string Image { get; set; } = string.Empty;
    }
}
