using DiaryApp.Models;
using System.Threading.Tasks;

namespace DiaryApp.Servicies
{
    public interface ISettingsService
    {
        Task<Settings> LoadSettingsAsync();
        Task SaveSettingsAsync(Settings settings);
    }
}