using System;
using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    // FluentValidation
    // AbstractValidator<T> : T tipinde bir nesne alir
    // RuleFor(x => x.PropertyName) : x tipindeki PropertyName property'si icin kural belirler
    // GreaterThan(0) : 0'dan buyuk olmalidir
    // NotEmpty() : bos olmamali
    // WithMessage("Mesaj") : hata mesaji
    //LessThan(1000) : 1000'den kucuk olmalidir
    //LessThanOrEqualTo(1000) : 1000'den kucuk veya esit olmalidir
    //MinimumLength(3) : en az 3 karakter olmalidir
    //MaximumLength(3) : en fazla 3 karakter olmalidir
    public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command=>command.Model.GenreId).GreaterThan(0);
            RuleFor(command=>command.Model.PageCount).GreaterThan(0);
            RuleFor(command=>command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command=>command.Model.Title).NotEmpty().MinimumLength(4);
           
        }
    }
  
}