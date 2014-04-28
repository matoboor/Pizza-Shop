using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models.Catalog
{
    public class Cart
    {
        public List<CartItem> Items { get; private set; }

        public static readonly Cart Instance;

        public double Total
        {
            get
            {
                double _total = 0;
                foreach (var item in Items)
                {
                    _total += item.Total;
                }
                return _total;
            }
        }

        static Cart()
        {
            if (HttpContext.Current.Session["Cart"] == null)
            {
                Instance = new Cart();
                Instance.Items = new List<CartItem>();
                HttpContext.Current.Session["Cart"] = Instance;
            }
            else
            {
                Instance = (Cart)HttpContext.Current.Session["Cart"];
            }
        }

        protected Cart() { }

        public CartItem GetItem(int index)
        {
            return Items.ElementAt(index);
        }

        public void UpdateItem(int index, CartItem item)
        {
            Items.RemoveAt(index);
            Items.Add(item);
        }

        public void AddItem(Product product)
        {

            CartItem newItem = new CartItem();
            newItem.Options = new List<CartItemOptionValue>();
            newItem.Product = product;
            foreach (var option in product.Options)
            {
                CartItemOptionValue cartItemValue = new CartItemOptionValue();
                cartItemValue.Option = option;
                cartItemValue.Value = option.Values.FirstOrDefault(v => v.Default==true);
                newItem.Options.Add(cartItemValue);
            }
            Items.Add(newItem);
        }

        public void AddItem(Product product, IEnumerable<ProductOptionValue> options, string comment)
        {
            CartItem newItem = new CartItem();
            newItem.Options = new List<CartItemOptionValue>();
            newItem.Product = product;
            newItem.Comment = comment;
            foreach (var option in options)
            {
                CartItemOptionValue cartItemValue = new CartItemOptionValue();
                cartItemValue.Option = option.ProductOption;
                cartItemValue.Value = option;
                newItem.Options.Add(cartItemValue);
            }
            Items.Add(newItem);
        }

        public void RemoveItem(int id)
        {
            Items.RemoveAt(id);
        }

        public void ClearCart()
        {
            Items.Clear();
        }

        public Order CreateOrder(Order order)
        {
            foreach (var item in Items)
            {
                var orderItem = new OrderItem();

                orderItem.Name = item.Product.Name;
                orderItem.Price = item.Product.Price;
                orderItem.Total = item.Total;
                orderItem.Wish = item.Comment;
                orderItem.ArchivedProductId = item.Product.ProductId;
                foreach (var option in item.Options)
                {
                    var value = new OrderItemOptionValue();
                    value.Name = option.Option.Name;
                    value.Value = option.Value.Name;
                    value.Price = option.Value.Price;
                    value.ArchivedProductOptionId = option.Option.ProductOptionId;
                    value.ArchivedProductOptionValueId = option.Value.ProductOptionValueId;

                    orderItem.Options.Add(value);
                }
                order.Items.Add(orderItem);

            }
            order.Total = this.Total;
            order.DateAdded = DateTime.Now;
            return order;
        }
    }
}