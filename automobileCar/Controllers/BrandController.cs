using automobileCar.Data;
using automobileCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace automobileCar.Controllers
{
    public class BrandController : Controller
    {
        private readonly Applicationdbcontext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(Applicationdbcontext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Brand> brands = _dbContext.Brand.ToList();
            return View(brands);
        }

        [HttpGet]

        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand brand) 
        {
            string webRootPath = _webHostEnvironment.WebRootPath; //image store and access

            var file = HttpContext.Request.Form.Files;

            if (file.Count>0) 
            { 
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\brand"); //creating folder

                var extension = Path.GetExtension(file[0].FileName);

                using(var fileStream = new FileStream(Path.Combine(upload,newFileName+extension), FileMode.Create)) 
                {
                    file[0].CopyTo(fileStream);
                }

                brand.BrandLogo = @"\images\brand\" + newFileName + extension;
            }

            if(ModelState.IsValid) 
            {
                _dbContext.Brand.Add(brand);
                _dbContext.SaveChanges();

                TempData["success"] = "Record Created Successfully";

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            Brand brand = _dbContext.Brand.FirstOrDefault(x=>x.Id == id);

            return View(brand);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Brand brand = _dbContext.Brand.FirstOrDefault(x => x.Id == id);

            return View(brand);
        }

        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            string webRootPath = _webHostEnvironment.WebRootPath; //image store and access

            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\brand"); //creating folder

                var extension = Path.GetExtension(file[0].FileName);

                //deleting old image

                var objFromDb = _dbContext.Brand.AsNoTracking().FirstOrDefault(x=>x.Id == brand.Id);

                if (objFromDb != null)
                {
                      var oldImagePath = Path.Combine(webRootPath, objFromDb.BrandLogo.Trim('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }


                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }

                brand.BrandLogo = @"\images\brand\" + newFileName + extension;
            }

            //update
            if (ModelState.IsValid)

            {
                var objFromDb = _dbContext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);

                objFromDb.Name = brand.Name;
                objFromDb.EstablishedYear = brand.EstablishedYear;

                if(brand.BrandLogo != null)
                {
                    objFromDb.BrandLogo = brand.BrandLogo;
                }
                _dbContext.Brand.Update(objFromDb);
                _dbContext.SaveChanges();

                TempData["warning"] = "Record Updated Successfully";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            Brand brand = _dbContext.Brand.FirstOrDefault(x => x.Id == id);

            return View(brand);
        }

        //delete record

        [HttpPost]
        public IActionResult Delete(Brand brand)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

            if (!string.IsNullOrEmpty(brand.BrandLogo))
            {
                //deleting old record
                var objFromDb = _dbContext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);

                if (objFromDb != null)
                {
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.BrandLogo.Trim('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

            }
            _dbContext.Brand.Remove(brand);
            _dbContext.SaveChanges();

            TempData["error"] = "Record Deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
