using Data.Context;
using Data.Models;

namespace Business.Services;

public class CharacteristicService
{
    private SQLiteContext _context = SQLiteContextSingleton.Instance;

    public Characteristic? Update(int productId, Characteristic characteristic)
    {
        Product? product = _context.Products?.Find(productId);
        product!.Characteristic = characteristic;
        _context.SaveChanges();
        return characteristic;
    }
}
