using System;
using System.Linq;
using FluentValidation;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator:AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.FirstName).MinimumLength(4).When(command => command.Model.FirstName != string.Empty);
            RuleFor(command => command.Model.LastName).MinimumLength(4).When(command => command.Model.LastName != string.Empty);

        }
    }
}