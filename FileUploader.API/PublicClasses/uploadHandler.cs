using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileUploader.API.PublicClasses
{
    public class UploadHandler
    {
        private readonly IWebHostEnvironment _environment;

        public UploadHandler(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string Upload(IFormFile file)
        {
            // Checking if the file is valid and not null 
            if (file == null || file.Length == 0)
            {
                return "No File Uploaded";
            }

            // Allowed file extensions
            List<string> imageExtensions = new List<string> { ".jpg", ".gif", ".png" };
            List<string> pdfExtensions = new List<string> { ".pdf" };

            string extension = Path.GetExtension(file.FileName).ToLower();

            if (!imageExtensions.Contains(extension) && !pdfExtensions.Contains(extension))
            {
                return $"Extension is not valid. Only allowed: {string.Join(", ", imageExtensions)}, {string.Join(", ", pdfExtensions)}";
            }

            // File size limit (10MB)
            if (file.Length > (10 * 1024 * 1024))
            {
                return "File size limit exceeded (Max: 10MB).";
            }

            // Determine the category folder
            string category = imageExtensions.Contains(extension) ? "images" : "pdfs";

            // Define the base upload path in wwwroot/uploads/
            string webRootPath = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadPath = Path.Combine(webRootPath, "uploads", category);

            // Ensure the directory exists
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Generate a unique file name
            string fileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(uploadPath, fileName);

            // Save the file
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Return the relative file path for easy access
            return $"/uploads/{category}/{fileName}";
        }
    }
}
