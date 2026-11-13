using DiaryApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DiaryApp.Servicies
{
    internal class FilesService : IFilesService
    {
        ISettingsService _service;
        public FilesService(ISettingsService settingsService)
        {
            _service = settingsService;
        }

        public async Task<ICollection<MdFile>> LoadFilesAsync()
        {
            var settings = await _service.LoadSettingsAsync();
            if (!Directory.Exists(settings.Folder))
            {
                return [];
            }
            var files = Directory.GetFiles(settings.Folder, "*.md");
            var mdFiles = new List<MdFile>();
            foreach (var file in files)
            {
                var content = await File.ReadAllTextAsync(file);
                mdFiles.Add(new MdFile
                {
                    FileName = Path.GetFileName(file),
                    Content = content
                });
            }
            return mdFiles;
        }

        public async Task<(bool, Exception?)> SaveFilesAsync(ICollection<MdFile> files)
        {
            var settings = await _service.LoadSettingsAsync();
            if (!Directory.Exists(settings.Folder))
            {
                return (false, new DirectoryNotFoundException("The specified folder does not exist."));            
            }
            try
            {
                foreach (var file in files)
                {
                    await File.WriteAllTextAsync(Path.Combine(settings.Folder, file.FileName), file.Content ?? string.Empty);
                }
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
            return (true, null);
        }
    }
}
