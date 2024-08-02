using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace ImageManipulation.Data.Models.DTO;
public class ProductDTO
{
    [Required]
    [MaxLength(30)]
    public string? ProductName { get; set; }

    [Required]
    public IFormFile? ImageFile { get; set; }


}

public class ProductUpdateDTO
{

    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string? ProductName { get; set; }

    [Required]
    [MaxLength(50)]
    public string? ProductImage { get; set; }

    public IFormFile? ImageFile { get; set; }
}