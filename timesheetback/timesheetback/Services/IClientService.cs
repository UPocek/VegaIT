using System;
using timesheetback.DTOs;

namespace timesheetback.Services
{
	public interface IClientService
	{

		List<CountryDTO> GetAllCountries();
		Task<List<CountryDTO>> GetAllCountriesAsync();

		List<CityDTO> GetAllCities();
		Task<List<CityDTO>> GetAllCitiesAsync();

        ClientDTO CreateClient(CreateClientCredentialsDTO clientCredentials);
        Task<ClientDTO> CreateClientAsync(CreateClientCredentialsDTO clientCredentials);

        ClientDTO UpdateClient(long id, CreateClientCredentialsDTO clientCredentials);
        Task<ClientDTO> UpdateClientAsync(long id, CreateClientCredentialsDTO clientCredentials);

		List<ClientDTO> GetAllClients();
		Task<List<ClientDTO>> GetAllClientsAsync();

        void DeleteClient(long id);
		Task DeleteClientAsync(long id);

    }
}

