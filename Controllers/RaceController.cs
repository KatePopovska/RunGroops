﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunGroops.Interfaces;
using RunGroops.Models;
using RunGroops.ViewModels;

namespace RunGroops.Controllers
{
    public class RaceController : Controller
    {
        private IRaceRepository _repository;
        private IPhotoService _photoService;
        private IMapper _mapper;

        public  RaceController(IRaceRepository repository, IPhotoService photoService, IMapper mapper)
        {
            _repository = repository;
            _photoService = photoService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var races = await _repository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _repository.GetById(id);
            return View(race);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceViewModel.Image);
                var race = _mapper.Map<Race>(raceViewModel);
                race.Image = result.Url.ToString();
                _repository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(raceViewModel);
        }
    }
}
