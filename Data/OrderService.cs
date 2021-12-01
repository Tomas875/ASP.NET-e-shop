using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursinis.Data;
using Kursinis.Models;
using Kursinis.Enums;
using Microsoft.EntityFrameworkCore;


namespace Kursinis.Data
{
    public class OrderService : IOrder
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _shoppingCart;

        public OrderService(ApplicationDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _context.Add(order);

            var orderDetails = new List<OrderDetail>(_shoppingCart.ShoppingCartItems.Count());

            foreach (var item in _shoppingCart.ShoppingCartItems)
            {
                orderDetails.Add(
                    new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductsId = item.Products.Id,
                        Amount = item.Amount,
                        Price = item.Products.Price,
                        Products = item.Products
                    });
                _context.Update(item.Products);
                
            }

            _context.OrderDetails.AddRange(orderDetails);
            _context.SaveChanges();
        }

        public Order GetById(int orderId)
        {
            return GetAll()
                .FirstOrDefault(order => order.Id == orderId);
        }

        

        

        private void SetOrderBy(IEnumerable<Order> orders, OrderBy orderBy)
        {
            switch (orderBy)
            {
                case OrderBy.DateDesc:
                    orders = orders.OrderByDescending(order => order.OrderPlaced);
                    break;
                case OrderBy.DateAsc:
                    orders = orders.OrderBy(order => order.OrderPlaced);
                    break;
                case OrderBy.PriceAsc:
                    orders = orders.OrderBy(order => order.OrderTotal);
                    break;
                case OrderBy.PriceDesc:
                    orders = orders.OrderByDescending(order => order.OrderTotal);
                    break;
            }
        }

        

        

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders
                .AsNoTracking()
                .Include(order => order.OrderLines).ThenInclude(line => line.Products);
        }
    }
}
