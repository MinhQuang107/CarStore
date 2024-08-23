using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarStore.Models;
using CarStore.Interfaces;

namespace CarStore.Areas.Store.Controllers
{
    [Area("Store")]
    public class ImageCarsController : Controller
    {
        private readonly CarStoreContext _context;
        private readonly ICarService _carService;
        private readonly IBufferedFileUploadService _bufferedFileUploadService;

        public ImageCarsController(CarStoreContext context, ICarService carService, IBufferedFileUploadService bufferedFileUploadService)
        {
            _context = context;
            _carService = carService;
            _bufferedFileUploadService = bufferedFileUploadService;
        }

        // GET: Store/ImageCars
        public async Task<IActionResult> Index()
        {
            var carStoreContext = _context.ImageCar.Include(i => i.Car);
            return View(await carStoreContext.ToListAsync());
        }

        // GET: Store/ImageCars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageCar = await _context.ImageCar
                .Include(i => i.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageCar == null)
            {
                return NotFound();
            }

            return View(imageCar);
        }

        // GET: Store/ImageCars/Create
        public IActionResult UploadImagesCar()
        {
            ViewData["Car_Id"] = new SelectList(_context.Car, "ID", "ID");
            return View();
        }

        // POST: Store/ImageCars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImagesCar(long Car_ID, List<IFormFile> images)
        {
            if (images != null && images.Count > 0)
            {
                try
                {
                    foreach (var image in images)
                    {
                        //string wwwPath = this.Environment.WebRootPath;
                        //string contentPath = this.Environment.ContentRootPath;

                        //string path = Path.Combine(this.Environment.WebRootPath, "uploads");
                        //if (!Directory.Exists(path))
                        //{
                        //    Directory.CreateDirectory(path);
                        //}
                        if (image.Length > 0)
                        {

                            //// Tạo một tên file duy nhất để tránh xung đột
                            //string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;

                            //// Đường dẫn tới file ảnh trên máy chủ
                            //string filePath = Path.Combine(uploadDirectory, uniqueFileName);

                            //// Lưu file ảnh vào thư mục lưu trữ
                            //using (var stream = new FileStream(filePath, FileMode.Create))
                            //{   
                            //    await image.CopyToAsync(stream);
                            //}
                            var res = await _bufferedFileUploadService.UploadFile(image, "images/avatar");

                            if (res)
                            {
                                // Lưu đường dẫn hoặc tên file vào cơ sở dữ liệu
                                var car = await _carService.GetCarByIdAsync(Car_ID);
                                if (car != null)
                                {
                                    // Thêm đường dẫn hoặc tên file vào danh sách ảnh của xe
                                    var rlt = await _carService.AddCarImagesAsync(Car_ID, image.FileName);
                                    if (rlt)
                                    {
                                        ViewBag.Message = "Success";
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Fail";
                                        return NotFound("Cannot update to db file!");
                                    }
                                }
                            } else
                            {
                                return NotFound("Cannot create file!");
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Cannot create file!");
                }
            }
            return RedirectToAction(nameof(Index)); // Trả về trang index nếu không có file hoặc có lỗi xảy ra
        }


        // GET: Store/ImageCars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageCar = await _context.ImageCar.FindAsync(id);
            if (imageCar == null)
            {
                return NotFound();
            }
            ViewData["Car_Id"] = new SelectList(_context.Car, "ID", "ID", imageCar.Car_Id);
            return View(imageCar);
        }

        // POST: Store/ImageCars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Car_Id,Image_URL")] ImageCar imageCar)
        {
            if (id != imageCar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageCarExists(imageCar.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Car_Id"] = new SelectList(_context.Car, "ID", "ID", imageCar.Car_Id);
            return View(imageCar);
        }

        // GET: Store/ImageCars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageCar = await _context.ImageCar
                .Include(i => i.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageCar == null)
            {
                return NotFound();
            }

            return View(imageCar);
        }

        // POST: Store/ImageCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageCar = await _context.ImageCar.FindAsync(id);
            if (imageCar != null)
            {
                _context.ImageCar.Remove(imageCar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageCarExists(int id)
        {
            return _context.ImageCar.Any(e => e.Id == id);
        }
    }
}
