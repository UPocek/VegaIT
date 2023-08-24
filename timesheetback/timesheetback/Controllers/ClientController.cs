using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using timesheetback.Services;
using timesheetback.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace timesheetback.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
		{
            _clientService = clientService;
		}

        [HttpGet("cities")]
        public async Task<List<CityDTO>> GetAllCities()
        {
            return await _clientService.GetAllCitiesAsync();
        }

        [HttpGet("countries")]
        public async Task<List<CountryDTO>> GetAllCountries()
        {
            return await _clientService.GetAllCountriesAsync();
        }

        [HttpGet("all")]
        public async Task<List<ClientDTO>> GetAllClients()
        {
            return await _clientService.GetAllClientsAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> CreateNewClient(CreateClientCredentialsDTO clientCredentials)
        {
            try {
                return await _clientService.CreateClientAsync(clientCredentials);
            }
            catch (Exception) {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClientDTO>> UpdateClient(long id, CreateClientCredentialsDTO clientCredentials)
        {
            try {
                return await _clientService.UpdateClientAsync(id, clientCredentials);
            }
            catch {
                return NotFound();
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(long id)
        {
            try {
                await _clientService.DeleteClientAsync(id);
            }catch (Exception) {
                return BadRequest();
            }
            
            return Ok();
        }

    }
}

