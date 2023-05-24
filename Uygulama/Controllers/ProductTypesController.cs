using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uygulama.Data;
using Uygulama.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace Uygulama.Controllers
{
    public class ProductTypesController : Controller
    {
        private UygulamaContext db = new UygulamaContext();

        string baseURL = "https://localhost:44309/";
        // GET: ProductTypes
        public async Task<ActionResult> Index()
        {
            DataTable dt = new DataTable();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appication/json"));

                HttpResponseMessage getData = await client.GetAsync("Api/Type");

                if (getData.IsSuccessStatusCode) 
                {
                 string results=getData.Content.ReadAsStringAsync().Result;
                    dt=JsonConvert.DeserializeObject<DataTable>(results);
                } else 
                {
                    return View(dt);
                }



                return View(dt);
            }
        }
        // GET: ProductTypes/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = new ProductType();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appication/json"));

                HttpResponseMessage getData = await client.GetAsync("Api/Type/" + id);

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    productType = JsonConvert.DeserializeObject<ProductType>(results);
                }
                else
                {
                    return RedirectToAction("Index");
                }



                return View(productType);

            }
        }

        // GET: ProductTypes/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public async Task<ActionResult> Create( ProductType productType)
        {

            ProductType cProductType = new ProductType() {
            Brand=productType.Brand,
            CapacityKG=productType.CapacityKG,
            CapacityM3=productType.CapacityM3,
            Model=productType.Model
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL );
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appication/json"));

                var getData =  client.PostAsJsonAsync("Api/Type", cProductType).Result;

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                    // dt = JsonConvert.DeserializeObject<DataTable>(results);
                }
                else
                {
                    return View();
                }
              
            }
                return RedirectToAction("Index");
              
            

         
        }

        // GET: ProductTypes/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = new ProductType();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appication/json"));

                HttpResponseMessage getData = await client.GetAsync("Api/Type/" + id);

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    productType = JsonConvert.DeserializeObject<ProductType>(results);
                }
                else
                {
                    return RedirectToAction("Index");
                }



                return View(productType);
            }
        }
        // POST: ProductTypes/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IsDelete,IsActive,Brand,Model,CapacityKG,CapacityM3")] ProductType productType)
        {
            if (productType.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType eproductType = new ProductType();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appication/json"));

                HttpResponseMessage getData = await client.PutAsJsonAsync("Api/Type/" + productType.Id, productType);
            
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    productType = JsonConvert.DeserializeObject<ProductType>(results);
                }
                else
                {
                    return View(productType);
                 
                }

                return RedirectToAction("Index");

                
            }
        }

        // GET: ProductTypes/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
      

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appication/json"));

                HttpResponseMessage getData = await client.DeleteAsync("Api/Type/" + id);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }



           

            }
           
        }

        // POST: ProductTypes/Delete/5


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
