namespace MusicApi.Models
{
    public class AlbumResponse
    {
        public int Id { get; set; }
        public string? Artist { get; set; }
        public string? Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
    }
}
