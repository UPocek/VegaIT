using System;
using Microsoft.EntityFrameworkCore;
using timesheetback.DTOs;
using timesheetback.Models;

namespace timesheetback.Repositories
{
	public class ClientRepository : IClientRepository
	{
        private readonly TimeSheetContext _context;

        public ClientRepository(TimeSheetContext context)
		{
            _context = context;
        }

        public void DeleteClient(long id)
        {
            var clientToDelete = _context.Clients.Find(id) ?? throw new Exception("client with that id does not exist");
            _context.Clients.Remove(clientToDelete);
            _context.SaveChanges();
        }

        public async Task DeleteClientAsync(long id)
        {
            var clientToDelete = await _context.Clients.FirstOrDefaultAsync(client => client.Id == id) ?? throw new Exception("client with that id does not exist");
            _context.Clients.Remove(clientToDelete);
            _context.SaveChanges();
        }

        public List<City> GetAllCities()
        {
            return _context.Cities.ToList();
        }

        public Task<List<City>> GetAllCitiesAsync()
        {
            return _context.Cities.ToListAsync();
        }

        public List<Client> GetAllClients()
        {
            return _context.Clients.ToList();
        }

        public Task<List<Client>> GetAllClientsAsync()
        {
            return _context.Clients.ToListAsync();
        }

        public List<Country> GetAllCountries()
        {
            return _context.Countries.ToList();
        }

        public Task<List<Country>> GetAllCountriesAsync()
        {
            return _context.Countries.ToListAsync();
        }

        public Client? GetClientById(long id)
        {
            return _context.Clients.Find(id);
        }

        public Task<Client?> GetClientByIdAsync(long id)
        {
            return _context.Clients.FirstOrDefaultAsync(client => client.Id == id);
        }

        public Client? GetClientByName(string clientName)
        {
            return _context.Clients.FirstOrDefault(client => client.Name == clientName);
        }

        public Task<Client?> GetClientByNameAsync(string clientName)
        {
            return _context.Clients.FirstOrDefaultAsync(client => client.Name == clientName);
        }

        public Client SaveClient(Client newClient)
        {
            _context.Clients.Add(newClient);
            _context.SaveChanges();
            return newClient;
        }

        public Client UpdateClient(Client clientToUpdate, CreateClientCredentialsDTO clientCredentials)
        {
            clientToUpdate.Name = clientCredentials.Name;
            clientToUpdate.Address = clientCredentials.Address;
            clientToUpdate.CountryId = clientCredentials.CountryId;
            clientToUpdate.CityId = clientCredentials.CityId;

            _context.SaveChanges();

            return clientToUpdate;
        }
    }
}

