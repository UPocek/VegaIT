using System;
using System.Security.Cryptography;
using System.Text;
using timesheetback.DTOs;
using timesheetback.Models;
using timesheetback.Repositories;

namespace timesheetback.Services
{
	public class UserService : IUserService
	{

		private readonly IUserRepository _userRepository;
        private readonly string _salt = "9003A697CA6F038B5140A9A86D000899E1521C4B29BE5996E452882E2103D2404AEB3F2EB89DECB63310D8F6B3B02FF15323CE8DE4F9F7547641D5A2FFB1F698";
        private const int _keySize = 64;
        private const int _iterations = 350000;
        private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

        public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public Employee? ProccessUserLogin(LoginCredentialsDTO loginCredentials)
        {
            Employee userToLogin = _userRepository.GetUserByEmail(loginCredentials.Email) ?? throw new Exception("User with that email does not exists");
            return (VerifyPassword(loginCredentials.Password, userToLogin.Password) && VerifyUserActive(userToLogin)) ? userToLogin : null;
                
        }

		public async Task<Employee?> ProccessUserLoginAsync(LoginCredentialsDTO loginCredentials)
        {
            Employee userToLogin = await _userRepository.GetUserByEmailAsync(loginCredentials.Email) ?? throw new Exception("User with that email does not exists");
            return (VerifyPassword(loginCredentials.Password, userToLogin.Password) && VerifyUserActive(userToLogin)) ? userToLogin : null;
        }

        public void ProccessUserRegistration(RegistrationCredentialsDTO registrationCredentials)
        {
            if (_userRepository.GetUserByEmail(registrationCredentials.Email) != null)
            {
                throw new Exception("User with that email already exists");
            }
            registrationCredentials.Password = HashPasword(registrationCredentials.Password);
            Role? roleToAssign = _userRepository.GetRoleByName(registrationCredentials.Role) ?? throw new Exception("Invalid role passed");
            var newEmployee = new Employee(registrationCredentials, roleToAssign);

            _userRepository.SaveUser(newEmployee);
        }

        public async Task ProccessUserRegistrationAsync(RegistrationCredentialsDTO registrationCredentials)
        {
            if (await _userRepository.GetUserByEmailAsync(registrationCredentials.Email) != null) {
                throw new Exception("User with that email already exists");
            }
            registrationCredentials.Password = HashPasword(registrationCredentials.Password);
            Role? roleToAssign = await _userRepository.GetRoleByNameAsync(registrationCredentials.Role) ?? throw new Exception("Invalid role passed");
            var newEmployee = new Employee(registrationCredentials, roleToAssign);

            _userRepository.SaveUser(newEmployee);
        }

        public List<RoleDTO> GetAllRoles()
        {
            List<Role> allRoles = _userRepository.GetAllRoles();
            return allRoles.Select(role => new RoleDTO(role)).ToList();
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        {
            List<Role> allRoles = await _userRepository.GetAllRolesAsync();
            return allRoles.Select(role => new RoleDTO(role)).ToList();
        }

        private string HashPasword(string password)
        {

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                Encoding.ASCII.GetBytes(_salt),
                _iterations,
                _hashAlgorithm,
                _keySize);

            return Convert.ToHexString(hash);
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, Encoding.ASCII.GetBytes(_salt), _iterations, _hashAlgorithm, _keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        private bool VerifyUserActive(Employee employee) {
            return employee.IsActive;
        }
    }
}

