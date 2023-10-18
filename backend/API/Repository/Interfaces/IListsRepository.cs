using API.Models;

namespace API.Repository;

public interface IListsRepository
{
    Lists? GetTopTens(int userId);

    void UpdateList(UpdateListRequest request);

    List<string> GetCategory(Lists lists, string category);

    void Save();
}