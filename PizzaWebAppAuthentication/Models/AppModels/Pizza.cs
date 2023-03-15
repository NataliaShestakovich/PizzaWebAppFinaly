﻿using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Pizza
    {
        public Pizza()
        {
            Ingredients = new List<Ingredient>();
        }
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Enter the name of the pizza")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal Price { get; set; }

        public bool Standart { get; set; }

        public PizzaBase PizzaBase { get; set; }

        public Size Size { get; set; } 

        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile ImageUpload { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }       
    }
}
