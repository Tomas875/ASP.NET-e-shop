using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Models
{
    public class CartItem
    {
        public CartItem()
        {

        }
        public CartItem(Products item)
        {
            ProductItem = item;
            Quantity = 1;
        }
        public Products ProductItem { get; set; }
        public int Quantity { get; set; }

        public int GetItemId()
        {
            return ProductItem.Id;
        }

        public string GetItemName()
        {
            return ProductItem.ItemName;
        }

        public void IncrementItemQuantity()
        {
            Quantity = Quantity + 1;
        }


        public double GetPrice()
        {
            return ProductItem.Price;
        }

        public double GetTotalCost()
        {
            return Quantity * GetPrice();
        }
    }
}
