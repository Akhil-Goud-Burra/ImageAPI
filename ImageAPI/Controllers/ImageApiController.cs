using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using ImageManipulation.Data.Repositories;
using ImageManipulation.Data.Services;
using ImageManipulation.Data.Models.DTO;
using ImageManipulation.Data.Models;

namespace ImageAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ImageApiController(IFileService fileService, IProductRepository productRepo, ILogger<ImageApiController> logger) : ControllerBase
    {


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDTO productToAdd)
        {
            try
            {
                if (productToAdd.ImageFile?.Length > 1 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                }

                string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                string createdImageName = await fileService.SaveFileAsync(productToAdd.ImageFile, allowedFileExtentions);

                // mapping `ProductDTO` to `Product` manually. You can use automapper.
                var product = new Product
                {
                    ProductName = productToAdd.ProductName,
                    ProductImage = createdImageName
                };

                var createdProduct = await productRepo.AddProductAsync(product);
                return CreatedAtAction(nameof(CreateProduct), createdProduct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
