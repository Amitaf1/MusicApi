using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Models;
using MusicApi.Validators;
using System.Collections.Generic;
using System.Linq;

namespace MusicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MusicController : ControllerBase
    {
        #region GET

        [HttpGet]
        public ActionResult<IEnumerable<AlbumResponse>> GetAlbums()
        {
            var albums = Album.Albums.Select(a => new AlbumResponse
            {
                Id = a.Id,
                Artist = a.Artist,
                Name = a.Name,
                ReleaseDate = a.ReleaseDate,
                Genre = a.Genre,
                Price = a.Price
            });
            return Ok(albums);
        }

        #endregion

        #region GET (ID)

        [HttpGet("{id}")]
        public ActionResult<AlbumResponse> GetAlbumById(int id)
        {
            Album album = Album.Albums.FirstOrDefault(a => a.Id == id)!;
            if (album == null)
            {
                return NotFound();
            }

            AlbumResponse response = new()
            {
                Id = album.Id,
                Artist = album.Artist,
                Name = album.Name,
                ReleaseDate = album.ReleaseDate,
                Genre = album.Genre,
                Price = album.Price
            };
            return Ok(response);
        }

        #endregion

        #region POST

        [HttpPost]
        public ActionResult<AlbumResponse> CreateAlbum([FromBody] AlbumRequest request)
        {
            AlbumRequestValidator validator = new();
            ValidationResult result = validator.Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            Album newAlbum = new()
            {
                Id = Album.Albums.Any() ? Album.Albums.Max(a => a.Id) + 1 : 1,
                Artist = request.Artist,
                Name = request.Name,
                ReleaseDate = request.ReleaseDate ?? DateTime.MaxValue,
                Genre = request.Genre,
                Price = request.Price
            };

            Album.Albums.Add(newAlbum);

            AlbumResponse response = new()
            {
                Id = newAlbum.Id,
                Artist = newAlbum.Artist,
                Name = newAlbum.Name,
                ReleaseDate = newAlbum.ReleaseDate,
                Genre = newAlbum.Genre,
                Price = newAlbum.Price
            };

            return CreatedAtAction(nameof(GetAlbumById), new { id = newAlbum.Id }, response);
        }

        #endregion

        #region PUT

        [HttpPut("{id}")]
        public ActionResult UpdateAlbum(int id, [FromBody] AlbumRequest request)
        {
            AlbumRequestValidator validator = new();
            ValidationResult result = validator.Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            Album album = Album.Albums.FirstOrDefault(a => a.Id == id)!;
            if (album == null)
            {
                return NotFound();
            }

            album.Artist = request.Artist;
            album.Name = request.Name;
            album.ReleaseDate = request.ReleaseDate ?? DateTime.MaxValue;
            album.Genre = request.Genre;
            album.Price = request.Price;

            return NoContent();
        }

        #endregion

        #region DELETE

        [HttpDelete("{id}")]
        public ActionResult DeleteAlbum(int id)
        {
            Album album = Album.Albums.FirstOrDefault(a => a.Id == id)!;
            if (album == null)
            {
                return NotFound();
            }

            Album.Albums.Remove(album);
            return NoContent();
        }

        #endregion
    }
}