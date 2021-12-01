using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursinis.Models;
using Kursinis.Enums;

namespace Kursinis.Data
{
	public interface IOrder
	{
		void CreateOrder(Order order);
		Order GetById(int orderId);

		IEnumerable<Order> GetAll();

	}
}