using System.Linq;
using backend.Data;
using backend.Models;

namespace backend.Repository;

public class ListsRepository : IListsRepository
{

    private TopTenContext _context;


    public ListsRepository(TopTenContext context)
    {
        _context = context;
    }
    public Lists GetTopTens(int userId)
    {
        return _context.TopTens.FirstOrDefault(x => x.UserId == userId);
    }


    public List<string> UpdateList(AddItemRequest request)
    {

        var lists = _context.TopTens.FirstOrDefault(x => x.UserId == request.userId);

        var category = GetCategory(lists, request.category);

        if (request.action == "add")
        {
            category[request.position] = request.item;
        }

        if (request.action == "remove")
        {
            category[request.position] = null;
        }

        return category;
    }

    public List<string> GetCategory(Lists lists, string category)
    {
        switch (category)
        {
            case "Movies":
                return lists.Movies;


            case "TV":
                return lists.TV;


            case "Music":
                return lists.Music;


            case "Books":
                return lists.Books;


            default:
                throw new ArgumentException("Invalid category.");
        }

    }

    public void Save() => _context.SaveChanges();

}