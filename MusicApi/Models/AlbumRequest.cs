using System;
using System.ComponentModel.DataAnnotations;

namespace MusicApi.Models
{
    public class AlbumRequest
    {
        public string? Artist { get; set; }
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
    }
}
