using BookStore.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class ProductModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Cover { get; set; } = "/StaticFiles/emptycover.png";

        [Required(ErrorMessage = "Добавьте автора")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Добавьте Название")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Добавьте цену")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена не может быть ниже 1 копейки")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Slug { get; set; }

        public long ProductID { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
