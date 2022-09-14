using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Common
{
    public enum GenreEnum
    {
        PersonalGrowth = 1,
        ScienceFiction = 2,
        Noval = 3,
        Historical = 4,
        Fantastic = 5,
        Biography = 6,
        Adventure = 7,
        Horror = 8,
        Romance = 9,
        Thriller = 10,
        Other = 11
    }
}