using FluentValidation;
using MusicApi.Models;

namespace MusicApi.Validators
{
    public class AlbumRequestValidator : AbstractValidator<AlbumRequest>
    {
        public AlbumRequestValidator()
        {
            RuleFor(x => x.Artist).NotEmpty().WithMessage("Artist name is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Album name is required.");
            RuleFor(x => x.ReleaseDate).NotEmpty().WithMessage("Release date is required.");
            RuleFor(x => x.Genre).NotEmpty().WithMessage("Genre is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}
