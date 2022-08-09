using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookStore.Infrastructure.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var image = Path.GetExtension(file.FileName);

                string[] images = { "jpg", "JPG", "png", "PNG" };

                bool result = images.Any(x => image.EndsWith(x));

                if (!result)
                {
                    return new ValidationResult("Файл должен быть с расширением jpg или png");
                }
            }

            return ValidationResult.Success;
        }
    }
}
