using System;
using timesheetback.DTOs;
using timesheetback.Models;

namespace timesheetback.Repositories
{
	public interface IClientRepository
	{

        List<City> GetAllCities();

        Task<List<City>> GetAllCitiesAsync();

        List<Country> GetAllCountries();

        Task<List<Country>> GetAllCountriesAsync();

        Client? GetClientByName(string name);

        Task<Client?> GetClientByNameAsync(string name);

        Client? GetClientById(long id);

        Task<Client?> GetClientByIdAsync(long id);

        Client SaveClient(Client newClient);

        Client UpdateClient(Client clientToUpdate, CreateClientCredentialsDTO clientCredentials);

        void DeleteClient(long id);

        Task DeleteClientAsync(long id);

        List<Client> GetAllClients();

        Task<List<Client>> GetAllClientsAsync();

    }
}

