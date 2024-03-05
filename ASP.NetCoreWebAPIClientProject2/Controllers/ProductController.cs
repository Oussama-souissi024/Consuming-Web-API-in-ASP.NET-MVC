using ASP.NetCoreWebAPIClientProject2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft;
using System.Text;

namespace ASP.NetCoreWebAPIClientProject2.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            List<Product> Products = new List<Product>();
            using (var client = new HttpClient())
            {
                using(var response = await client.GetAsync("http://localhost:5099/api/Products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(Products);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Product product = new Product();
            using (var client = new HttpClient())
            {
                using (var responce = await client.GetAsync($"http://localhost:5099/api/Products/{id}"))
                {
                    string apiResponce = await responce.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponce);
                }
            }
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product newProduct)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(newProduct), Encoding.UTF8, "application/json");
                    using (var responce = await client.PostAsync("http://localhost:5099/api/Products", jsonContent))
                    {
                        responce.EnsureSuccessStatusCode();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch
            {              
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Product product = new Product();
            using (var client = new HttpClient())
            {
                using (var responce = await client.GetAsync($"http://localhost:5099/api/Products/{id}"))
                {
                    string apiResponce = await responce.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponce);
                }
            }
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product ProductAfterEdit)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(ProductAfterEdit), Encoding.UTF8, "application/json");
                    using (var responce = await client.PutAsync($"http://localhost:5099/api/Products/{ProductAfterEdit.ProductID}", jsonContent))
                    {
                        responce.EnsureSuccessStatusCode();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Product product = new Product();
                using (var client = new HttpClient())
                {
                    using (var responce = await client.GetAsync($"http://localhost:5099/api/Products/{id}"))
                    {
                        string apiResponce = await responce.Content.ReadAsStringAsync();
                        product = JsonConvert.DeserializeObject<Product>(apiResponce);
                    }
                }
                return View(product);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    using (var responce = await client.DeleteAsync($"http://localhost:5099/api/Products?ID={id}"))
                    {
                        responce.EnsureSuccessStatusCode();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View(id);
            }
        }
    }
}
