using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RunGroops.Interfaces;
using RunGroops.Models;
using RunGroops.ViewModels;

namespace RunGroops.Controllers
{
    public class ClubController : Controller
    {
        private IClubRepository _repository;
        private IPhotoService _photoService;
        private IMapper _mapper;

        public ClubController( IClubRepository repository, IPhotoService photoService, IMapper mapper)
        {
            _repository = repository;
            _photoService = photoService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var clubs = await _repository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _repository.GetById(id);
            return View(club);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubViewModel.Image);
                var club = _mapper.Map<Club>(clubViewModel);
                club.Image = result.Url.ToString();
                _repository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubViewModel); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            var club = await _repository.GetById(id);
            if (club == null)
            {
                return View("Error");
            }
            var clubViewModel = _mapper.Map<EditClubViewModel>(club);
            return View(clubViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View("Edit", clubViewModel);
            }

            var userClub = await _repository.GetByIdAsyncNoTracking(id);
            if(userClub != null)
            {
                try
                {
                   await  _photoService.DeletePhotoAsync(userClub.Image);
                }
                catch(Exception) 
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(clubViewModel);
                }
                var photoResult = await _photoService.AddPhotoAsync(clubViewModel.Image);
                var club = _mapper.Map<Club>(clubViewModel);
                club.Id = id;
                club.Image = photoResult.Url.ToString();
      
                _repository.Update(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View(clubViewModel);
            }          
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _repository.GetById(id);
            if (clubDetails == null)
            {
                return View("Error");
            }
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubToDelete = await _repository.GetById(id);   
            if(clubToDelete == null)
            {
                return View("Error");
            }
            _repository.Delete(clubToDelete);
            return RedirectToAction("Index");
        }
              
    }
}
