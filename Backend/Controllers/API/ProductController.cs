using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Text.Json;

namespace Backend.Controllers.API
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [Route("api/[controller]")]
        public IEnumerable<ProductModel> GetProducts()
        {
            using(DataContext context = new DataContext())
            {
                return context.Products.ToList();
            }

            return null;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public List<ProductModel> CreateProduct(JsonElement productsJsonString)
        {
            List<ProductModel> products = new List<ProductModel>();

            ProductModel product;

            JsonDocument document = JsonDocument.Parse(productsJsonString.ToString());

            JsonElement root = document.RootElement;

            for(int i = 0; i < root.GetArrayLength(); i++)
            {
                product = new ProductModel();

                product.Creator = root[i].GetProperty("creator").ToString();
                product.Date = DateTime.Parse(root[i].GetProperty("date").ToString());
                product.Comments = root[i].GetProperty("comments").ToString();
                product.Num = int.Parse(root[i].GetProperty("num").ToString());
                product.Price = double.Parse(root[i].GetProperty("price").ToString());
                product.Quantity = int.Parse(root[i].GetProperty("quantity").ToString());

                products.Add(product);
            }
            
            using(DataContext context = new DataContext())
            {
                foreach(ProductModel item in products)
                {
                    context.Products.Add(item);
                }

                context.SaveChanges();

                return products;
            }            
        }

        private void log(string file, string description)
        {
            using(DataContext context = new DataContext())
            {
                LogModel log = new LogModel(file, description, DateTime.Now);

                context.AddRange(log);
                context.SaveChanges();
            }
        }
    }
}