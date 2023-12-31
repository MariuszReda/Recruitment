﻿using CityService.Dto;
using CityService.Domain;
using Microsoft.AspNetCore.Mvc;
using CityService;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public ActionResult<List<CityDTO>> GetAllCities()
    {
        var cities = _cityService.GetAllCities();
        return Ok(cities);
    }

    [HttpGet("{id}")]
    public ActionResult<CityDTO> GetCityById(int id)
    {
        var city = _cityService.GetCityById(id);
        if (city == null)
        {
            return NotFound();
        }
        return Ok(city);
    }

    [HttpPost]
    public ActionResult<CityDTO> AddCity([FromBody] RequestAddCity city)
    {
        var addedCity = _cityService.AddCity1(city.City,city.Region);
        return CreatedAtAction(nameof(GetCityById), new { id = addedCity.Id }, addedCity);
    }

    [HttpGet("random")]
    public ActionResult<CityDTO> GetRandomCity()
    {
        var city = _cityService.GetRandomCity();
        return Ok(city);
    }

    [HttpGet("region/{region}")]
    public ActionResult<List<CityDTO>> GetCitiesByRegion(string region)
    {
        var cities = _cityService.GetCitiesByRegion(region);
        return Ok(cities);
    }
}
