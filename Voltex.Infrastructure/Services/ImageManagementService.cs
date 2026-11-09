using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Services;

namespace Voltex.Infrastructure.Services
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider _fileProvider;
        public ImageManagementService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public void DeleteImageAsync(string src)
        {
            var Fileinfo = _fileProvider.GetFileInfo(src);
            var path = Fileinfo.PhysicalPath;
            if(path is not null)
                File.Delete(path);
        }

        public async Task<List<string>> SaveImageAync(IFormFileCollection files, string src)
        {
            List<string> result = new List<string>();
            var ImagesDirectory = Path.Combine("wwwroot", "Images" , src);
            if (Directory.Exists(ImagesDirectory) != true)
            {
                Directory.CreateDirectory(ImagesDirectory);
            }
            foreach(var file in files)
            {
                if(file.Length > 0)
                {
                    var ImagePath = $@"\Images\{src}\{file.FileName}";
                    var root = Path.Combine(ImagesDirectory, file.FileName);
                    using(FileStream stream = new FileStream(root,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    result.Add(ImagePath);
                }
                
            }
            return result;
        }

    }
}
