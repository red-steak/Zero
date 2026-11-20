using DiaryApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
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

        public async Task<ICollection<FileData>> LoadFilesAsync()
        {
            var settings = await _service.LoadSettingsAsync();
            if (!Directory.Exists(settings.Folder))
            {
                return [];
            }
            var files = Directory.GetFiles(settings.Folder, "*.day");
            var fileData = new List<FileData>();
            foreach (var file in files)
            {
                var json = await File.ReadAllTextAsync(file);
                fileData.Add(JsonSerializer.Deserialize<FileData>(json, JsonSerializerOptions.Strict) ?? new FileData());
            }
            return fileData;
        }

        public async Task<(bool, Exception?)> SaveFilesAsync(ICollection<FileData> fileInfos)
        {
            var settings = await _service.LoadSettingsAsync();
            
            if (!Directory.Exists(settings.Folder))
            {
                return (false, new DirectoryNotFoundException("The specified folder does not exist."));            
            }
            try
            {
                foreach (var file in fileInfos)
                {
                    var filePath = Path.Combine(settings.Folder, file.FileName.TrimStart('\\'));
                    var json = JsonSerializer.Serialize(file, JsonSerializerOptions.Strict);
                    await File.WriteAllTextAsync(filePath, json);
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
