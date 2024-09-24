using System.ComponentModel.DataAnnotations;

namespace MusicApi.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }


        public static List<Album> Albums = new List<Album>
        {
            new Album
            {
                Id = 1,
                Artist = "Metallica",
                Name = "...And Justice for All",
                Genre = "Metal",
                ReleaseDate = new DateTime(1988, 8, 25),
                Price = 299
            },
            new Album
            {
                Id = 2,
                Artist = "Metallica",
                Name = "Hardwired... to Self-Destruct",
                Genre = "Metal",
                ReleaseDate = new DateTime(2016, 11, 18),
                Price = 399
            },
            new Album
            {
                Id = 3,
                Artist = "The Black Eyed Peas",
                Name = "Elephunk",
                Genre = "Pop",
                ReleaseDate = new DateTime(2003, 6, 24),
                Price = 199
            },
            new Album         
            {
                Id = 4,
                Artist = "Chappell Roan",
                Name = "The Rise and Fall of a Midwest Princess",
                Genre = "Pop",
                ReleaseDate = new DateTime(2023, 9, 22),
                Price = 666
            },
            new Album
            {
                Id = 5,
                Artist = "Gorillaz",
                Name = "Demon Days",
                Genre = "Trip Hop",
                ReleaseDate = new DateTime(2016, 4, 21),
                Price = 250
            }
        };
    }

}
