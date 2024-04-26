using Assignment.Models;
using Assignment.Models.LoginRequest;
using Assignment.MovieRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
       /* public static IWebHostEnvironment _webHostEnvironment;
        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
         */  /* _webHostEnvironment = webHostEnvironment;
        }*/
        private readonly IimageRepository _imageRepository;
        public FileUploadController(IimageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<string> Post([FromForm] FilesUplaods filesUploads)
        {
            try
            {
                if (filesUploads.File != null && filesUploads.File.Length > 0)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await filesUploads.File.CopyToAsync(memoryStream);
                        byte[] fileBytes = memoryStream.ToArray();
                        // Call SaveImageAsync with the byte array of the file
                        await _imageRepository.SaveImageAsync(fileBytes);
                        return "Upload done";
                    }
                }
                else
                {
                    return "No file provided";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet("GetImageById/{id}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            try
            {
                var imageData = await _imageRepository.GetImageDataAsync(id);
                if (imageData == null)
                {
                    return NotFound("Image not found");
                }

                return File(imageData, "image/jpeg"); // Assuming images are JPEGs
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving image: {ex.Message}");
            }
        }


        /*
          [HttpPost("UploadImage")]
          public async Task<ActionResult> UploadImage(IFormFile file)
          {
              if (file == null || file.Length == 0)
              {
                  return BadRequest("Please provide a valid image file");
              }

              if (!IsImageFile(file))
              {
                  return BadRequest("Please provide a valid image file (JPEG, PNG, GIF, BMP)");
              }

              try
              {
                  string base64String;
                  using (var memoryStream = new MemoryStream())
                  {
                      await file.CopyToAsync(memoryStream);
                      base64String = Convert.ToBase64String(memoryStream.ToArray());
                  }

                  var imageUrl = await _imageRepository.UploadImageAsync(base64String);
                  return Content(imageUrl, "text/plain");
              }
              catch (Exception ex)
              {
                  return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading image: {ex.Message}");
              }

          }
          private bool IsImageFile(IFormFile file)
          {
              var contentType = file.ContentType.ToLower();
              return contentType.StartsWith("image/");
          }*/
    }
}