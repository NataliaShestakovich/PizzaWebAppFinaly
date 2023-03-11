﻿namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class CartItem
    {
        public Guid ID { get; set; }

        public Guid PizzaId { get; set; }

        public int Quantity { get; set; }

        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Pizza Pizza { get; set; }
    }
}
