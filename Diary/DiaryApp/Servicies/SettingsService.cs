using DiaryApp.Models;
using System.IO;
using System.Threading.Tasks;

namespace DiaryApp.Servicies
{
    internal class SettingsService : ISettingsService
    {
        private readonly string _settingsFilePath = "settings.json";

        public SettingsService()
        {
        }

        public async Task<Settings> LoadSettingsAsync()
        {
            if (!File.Exists(_settingsFilePath))
            {
                return new Settings();
            }
            var json = await File.ReadAllTextAsync(_settingsFilePath);
            return System.Text.Json.JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
        }
        public async Task SaveSettingsAsync(Settings settings)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(settings, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_settingsFilePath, json);
        }
    }
}
