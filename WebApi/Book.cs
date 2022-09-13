using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi
{
    public class Book
    {
        // id'yi otomatik artan yapmak icin
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }


    }
}
