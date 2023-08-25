using System;
using System.Linq;
using timesheetback.DTOs;
using timesheetback.Models;
using timesheetback.Repositories;

namespace timesheetback.Services
{
	public class ClientService : IClientService
	{
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
		{
            _clientRepository = clientRepository;
		}

        public ClientDTO CreateClient(CreateClientCredentialsDTO clientCredentials)
        {
            if (_clientRepository.GetClientByName(clientCredentials.Name) != null)
            {
                throw new Exception("Client with that name already exists");
            }

            var newClient = new Client(clientCredentials);

            return new ClientDTO(_clientRepository.SaveClient(newClient));
        }

        public async Task<ClientDTO> CreateClientAsync(CreateClientCredentialsDTO clientCredentials)
        {
            if (await _clientRepository.GetClientByNameAsync(clientCredentials.Name) != null)
            {
                throw new Exception("Client with that name already exists");
            }

            var newClient = new Client(clientCredentials);

            return new ClientDTO(_clientRepository.SaveClient(newClient));
        }

        public void DeleteClient(long id)
        {
            _clientRepository.DeleteClient(id);
        }

        public async Task DeleteClientAsync(long id)
        {
            await _clientRepository.DeleteClientAsync(id);
        }

        public List<CityDTO> GetAllCities()
        {
            List<City> allCities = _clientRepository.GetAllCities();
            return allCities.Select(city => new CityDTO(city)).ToList();
        }

        public async Task<List<CityDTO>> GetAllCitiesAsync()
        {
            List<City> allCities = await _clientRepository.GetAllCitiesAsync();
            return allCities.Select(city => new CityDTO(city)).ToList();
        }

        public List<ClientDTO> GetAllClients()
        {
            List<Client> allClients = _clientRepository.GetAllClients();
            return allClients.Select(client => new ClientDTO(client)).ToList();
        }

        public async Task<List<ClientDTO>> GetAllClientsAsync()
        {
            List<Client> allClients = await _clientRepository.GetAllClientsAsync();
            return allClients.Select(client => new ClientDTO(client)).ToList();
        }

        public List<ClientMinimalDTO> GetAllClientsMinimal()
        {
            List<Client> allClients = _clientRepository.GetAllClients();
            return allClients.Select(client => new ClientMinimalDTO(client)).ToList();
        }

        public async Task<List<ClientMinimalDTO>> GetAllClientsMinimalAsync()
        {
            List<Client> allClients = await _clientRepository.GetAllClientsAsync();
            return allClients.Select(client => new ClientMinimalDTO(client)).ToList();
        }

        public List<CountryDTO> GetAllCountries()
        {
            List<Country> allCountries = _clientRepository.GetAllCountries();
            return allCountries.Select(country => new CountryDTO(country)).ToList();
        }

        public async Task<List<CountryDTO>> GetAllCountriesAsync()
        {
            List<Country> allCountries = await _clientRepository.GetAllCountriesAsync();
            return allCountries.Select(country => new CountryDTO(country)).ToList();
        }

        public ClientDTO UpdateClient(long id, CreateClientCredentialsDTO clientCredentials)
        {
            Client clientToUpdate = _clientRepository.GetClientById(id) ?? throw new Exception("Client with that id does not exist");
            return new ClientDTO(_clientRepository.UpdateClient(clientToUpdate, clientCredentials));
        }

        public async Task<ClientDTO> UpdateClientAsync(long id, CreateClientCredentialsDTO clientCredentials)
        {
            Client clientToUpdate = await _clientRepository.GetClientByIdAsync(id) ?? throw new Exception("Client with that id does not exist");
            return new ClientDTO(_clientRepository.UpdateClient(clientToUpdate, clientCredentials));
        }
    }
}

