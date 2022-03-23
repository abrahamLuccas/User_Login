using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserProfile.Models;

namespace UserProfile.Controllers
{
    public class UserController : Controller
    {
        private readonly UserProfile.Data.AppContext _appContext;

        public UserController(UserProfile.Data.AppContext appContext)
        {
            _appContext = appContext;  
        }

        // GET: UserController

        public async Task<IActionResult> Index()
        {
            var allUsers = await _appContext.Users.ToListAsync();
            return View(allUsers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, IList<IFormFile> Img)
        {
            //Verificar tamanho da imagem
            IFormFile uploadedImage = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (Img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                user.Picture = ms.ToArray();
            }

            _appContext.Users.Add(user);
            _appContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details (Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _appContext.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _appContext.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            return View(user);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async  Task<IActionResult> Edit(Guid? id, User user, IList<IFormFile> Img)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Oldvalues = _appContext.Users.AsNoTracking().FirstOrDefault(p => p.Id == id);

            IFormFile uploadedImage = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (Img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                user.Picture = ms.ToArray();
            }
            else
            {
                user.Picture = Oldvalues.Picture;
            }
            if (ModelState.IsValid)
            {
                _appContext.Update(user);
                await _appContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);

        }
       
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _appContext.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            var user = await _appContext.Users.FindAsync(id);
            _appContext.Users.Remove(user);
            await _appContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
