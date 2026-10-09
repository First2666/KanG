using KanG.Models;

namespace KanG.Services.IService
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagByIdAsync(int id);
        Task<Tag> CreateTagAsync(Tag tag);
        Task<bool> DeleteTagAsync(int id);
    }
}
