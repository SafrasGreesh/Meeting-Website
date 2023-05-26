using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

public class ImageController : Controller
{
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ImageController(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpPost("/upload/image")]
    public async Task<IActionResult> UploadImage(IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            return BadRequest(new { message = "No image uploaded" });
        }

        // Генерируем уникальное имя файла
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);


        // Полный путь к папке wwwroot/uploads, где будут сохраняться изображения
        var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

        // Создаем папку uploads, если она не существует
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        // Сохраняем файл изображения в папке uploads
        var filePath = Path.Combine(uploadsFolder, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        return Ok(new { fileName });
    }
}
