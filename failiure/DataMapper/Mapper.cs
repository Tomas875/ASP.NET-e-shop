/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Kursinis.Models;
using Kursinis.Models.Account;
using Kursinis.Data;


namespace Kursinis.DataMapper
{
    public class Mapper
    {
        public Order OrderIndexModelToOrder(OrderIndexModel model, ApplicationUser user)
        {
            return new Order
            {
                Id = model.Id,
                OrderPlaced = model.OrderPlaced,
                OrderTotal = model.OrderTotal,
                User = user,
                UserId = user.Id,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                ZipCode = model.ZipCode,
                //OrderLines = OrderDetailsListingModelToOrderDetails(model.OrderLines)
            };
        }

        private IEnumerable<OrderDetail> OrderDetailsListingModelToOrderDetails(IEnumerable<OrderDetailListingModel> orderLines)
        {
            return orderLines.Select(line => new OrderDetail
            {
                Amount = line.Amount,
                ProductsId = line.Products.Id,
                Id = line.Id,
                OrderId = line.OrderId,
                Price = line.Price
            });
        }

        public IEnumerable<OrderIndexModel> OrdersToOrderIndexModels(IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderIndexModel
            {
                Id = order.Id,
                Address = order.Address,
                City = order.City,
                Country = order.Country,
                OrderPlaced = order.OrderPlaced,
                OrderTotal = order.OrderTotal,
                UserId = order.UserId,
                ZipCode = order.ZipCode,
                UserFullName = $"{order.User.FirstName} {order.User.LastName}",
                OrderLines = OrderDetailsToOrderDetailsListingModel(order.OrderLines)
            });
        }



        private IEnumerable<OrderDetailListingModel> OrderDetailsToOrderDetailsListingModel(IEnumerable<OrderDetail> orderLines)
        {
            if (orderLines == null)
            {
                orderLines = Enumerable.Empty<OrderDetail>();
            }
            return orderLines.Select(orderLine => new OrderDetailListingModel
            {
                Amount = orderLine.Amount,
                Id = orderLine.Id,
                OrderId = orderLine.OrderId,
                Price = orderLine.Price,
                Products = new Views.Products.ProductsSummaryModel
                {
                    Id = orderLine.ProductsId,
                    ItemName = orderLine.Products.ItemName
                },
                ProductId = orderLine.ProductsId
            });
        }

        public AccountSettingsModel ApplicationUserToAccountSettingsModel(ApplicationUser user, string roleId)
        {
            return new AccountSettingsModel
            {
                Id = user.Id,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                RoleId = roleId
            };
        }

        public ApplicationUser AccountRegisterModelToApplicationUser(AccountRegisterModel login)
        {
            return new ApplicationUser
            {
                FirstName = login.FirstName,
                AddressLine1 = login.AddressLine1,
                AddressLine2 = login.AddressLine2,
                City = login.City,
                Country = login.Country,
                Email = login.Email,
                MemberSince = DateTime.Now,
                
                LastName = login.LastName,
                UserName = login.Email,
                Orders = Enumerable.Empty<Order>(),
                PhoneNumber = login.PhoneNumber,
            };
        }

        public AccountProfileModel ApplicationUserToAccountProfileModel(ApplicationUser user, IOrder orderService, string role)
        {
            return new AccountProfileModel
            {
                Id = user.Id,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MemberSince = user.MemberSince,
                PhoneNumber = user.PhoneNumber,
                OrderCount = orderService.GetByUserId(user.Id).Count(),
                LatestOrders = OrdersToOrderIndexModels(orderService.GetUserLatestOrders(5, user.Id)),
                Role = role,
                TotalSpent = orderService.GetByUserId(user.Id).Sum(order => order.OrderTotal)
            };
        }

        public void AccountSettingsModelToApplicationUser(AccountSettingsModel model, ApplicationUser user)
        {
            user.City = model.City;
            user.AddressLine1 = model.AddressLine1;
            user.AddressLine2 = model.AddressLine2;
            user.Country = model.Country;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
        }

        public async Task<IEnumerable<AccountProfileModel>> ApplicationUsersToAccountProfileModelsAsync(IEnumerable<ApplicationUser> users, IOrder orderService, UserManager<ApplicationUser> userManager)
        {
            List<AccountProfileModel> models = new List<AccountProfileModel>(users.Count());

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                models.Add(ApplicationUserToAccountProfileModel(user, orderService, roles.FirstOrDefault()));
            }

            return models;
        }
    }
}
*/