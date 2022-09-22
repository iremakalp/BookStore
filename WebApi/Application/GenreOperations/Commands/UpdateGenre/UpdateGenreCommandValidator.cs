using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
<<<<<<< HEAD
            // 4'ten buyuk olmali ama name bos gelmezse 
            RuleFor(x => x.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
            
=======
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(4);
>>>>>>> da40616ad028dcd1773971631e98ea225cdb0cb5
        }
    }
}
