namespace UpSkill.Services.Data.Contracts
{
using System.Threading.Tasks;
    public interface IimagesService
    {
        public Task<string> RemoveImgBackground(string url);
    }
}
