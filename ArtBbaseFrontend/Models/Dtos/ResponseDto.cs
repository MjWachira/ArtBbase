﻿namespace ArtBbaseFrontend.Models.Dtos
{
    public class ResponseDto
    {
        public object? Result { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string Errormessage { get; set; } = string.Empty;
    }
}

