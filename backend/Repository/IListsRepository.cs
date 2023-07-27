using backend.Models;

namespace backend.Repository;

public interface IListsRepository
{
    Lists GetTopTens(int userId);

    List<string> UpdateList(AddItemRequest request);

    List<string> GetCategory(Lists lists, string category);

    void Save();
}