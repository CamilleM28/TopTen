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

    public List<string> UpdateList(UpdateListRequest request)
    {

        var lists = _context.TopTens.FirstOrDefault(x => x.UserId == request.UserId) ?? throw new ArgumentException("User has no lists");

        var category = GetCategory(lists, request.Category);

        if (request.Action == "add")
        {
            if (category[request.Position] != null)
            {
                throw new Exception("item already in this place");
            }
            category[request.Position] = request.Item;
        }

        else if (request.Action == "remove")
        {
            category[request.Position] = null;
        }

        else
        {
            throw new ArgumentException("Invalid Action");
        }

        return category;
    }

    public List<string> GetCategory(Lists lists, string category)
    {
        switch (category)
        {
            case "movies":
                return lists.Movies;


            case "tv":
                return lists.TV;


            case "music":
                return lists.Music;


            case "books":
                return lists.Books;


            default:
                throw new ArgumentException("Invalid category.");
        }

    }

    public void Save() => _context.SaveChanges();

}