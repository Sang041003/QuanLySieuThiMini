using Microsoft.Extensions.Caching.Memory;
using QuanLySieuThiMini.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class CartService
{
    private readonly IMemoryCache _cache;
    private const string CartCacheKey = "Cart";
    private readonly ProductDBContext _dbContext;
    private readonly IServiceProvider _serviceProvider;
    public CartService(IMemoryCache memoryCache, ProductDBContext dbContext, IServiceProvider serviceProvider)
    {
        _cache = memoryCache;
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
    }

    public Cart GetCart()
    {
        if (!_cache.TryGetValue(CartCacheKey, out Cart cart))
        {
            cart = new Cart(_dbContext,_serviceProvider);
            SaveCart(cart);
        }

        return cart;
    }

    public void SaveCart(Cart cart)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Thời gian tồn tại của giỏ hàng trong cache
        };

        _cache.Set(CartCacheKey, cart, cacheEntryOptions);
    }

    public void ClearCart()
    {
        _cache.Remove(CartCacheKey);
    }
}
