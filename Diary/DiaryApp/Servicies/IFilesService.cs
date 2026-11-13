using DiaryApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.Servicies
{
    public interface IFilesService
    {
        Task<ICollection<MdFile>> LoadFilesAsync();
        Task<(bool, Exception?) > SaveFilesAsync(ICollection<MdFile> files);
    }
}