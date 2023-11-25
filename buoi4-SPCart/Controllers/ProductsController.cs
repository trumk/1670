using buoi4_SPCart.Data;
using Microsoft.AspNetCore.Mvc;
using buoi4_SPCart.Models;
using Newtonsoft.Json;

namespace buoi4_SPCart.Controllers
{
    public class ProductsController : Controller
    {
        private buoi4_SPCartContext _db;
        public ProductsController(buoi4_SPCartContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            var _products = GetAllProducts();
            ViewBag.products = _products;
            return View();
        }
        public List<Product> GetAllProducts()
        {
            return _db.Product.ToList();
        }
        public Product GetDetailProduct(int id)
        {
            var product = _db.Product.Find(id);
            return product;
        }
        public IActionResult AddCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart == null)
            {
                var product = GetDetailProduct(id);
                List<Cart> ListCart = new List<Cart>()
                {
                    new Cart
                    {
                        Product = product,
                        Quantity = 1
                    }
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(ListCart));
            }
            else
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                bool check = true;
                for(int i = 0; i<dataCart.Count; i++)
                {
                    if (dataCart[i].Product.ProductID == id)
                    {
                        dataCart[i].Quantity++;
                        check = false; 
                    }
                }
                if (check)
                {
                    dataCart.Add(new Cart
                    {
                        Product = GetDetailProduct(id),
                        Quantity = 1
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
            }
            return RedirectToAction("Index");
        }
        public IActionResult UpdateCart(int id, int quantity)
        {
            var cart = HttpContext.Session.GetString("cart");
            if(cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if(quantity > 0)
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].Product.ProductID == id)
                        {
                            dataCart[i].Quantity = quantity;
                        }
                    }
                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                }
                return Ok(quantity);
            }
            return BadRequest();
        }
    }
}
