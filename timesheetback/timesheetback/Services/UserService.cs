using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using timesheetback.DTOs;
using timesheetback.Models;
using timesheetback.Repositories;

namespace timesheetback.Services
{
	public class UserService : IUserService
	{

		private readonly IUserRepository _userRepository;
        private readonly IHelperService _helperService;
        private readonly IJwtService _jwtService;

        private readonly string _salt = "9003A697CA6F038B5140A9A86D000899E1521C4B29BE5996E452882E2103D2404AEB3F2EB89DECB63310D8F6B3B02FF15323CE8DE4F9F7547641D5A2FFB1F698";
        private const int _keySize = 64;
        private const int _iterations = 350000;
        private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

        public UserService(IUserRepository userRepository, IHelperService helperService, IJwtService jwtService)
		{
			_userRepository = userRepository;
            _helperService = helperService;
            _jwtService = jwtService;
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

        public UserDTO ProccessUserRegistration(RegistrationCredentialsDTO registrationCredentials)
        {
            if (_userRepository.GetUserByEmail(registrationCredentials.Email) != null || registrationCredentials.Password is null)
            {
                throw new Exception("User with that email already exists");
            }
            registrationCredentials.Password = HashPasword(registrationCredentials.Password);
            Role? roleToAssign = _userRepository.GetRoleByName(registrationCredentials.Role) ?? throw new Exception("Invalid role passed");
            var newEmployee = new Employee(registrationCredentials, roleToAssign);

            return new UserDTO(_userRepository.SaveUser(newEmployee));
        }

        public async Task<UserDTO> ProccessUserRegistrationAsync(RegistrationCredentialsDTO registrationCredentials)
        {
            if (await _userRepository.GetUserByEmailAsync(registrationCredentials.Email) != null || registrationCredentials.Password is null) {
                throw new Exception("User with that email already exists");
            }
            registrationCredentials.Password = HashPasword(registrationCredentials.Password);
            Role? roleToAssign = await _userRepository.GetRoleByNameAsync(registrationCredentials.Role) ?? throw new Exception("Invalid role passed");
            var newEmployee = new Employee(registrationCredentials, roleToAssign);

            return new UserDTO(_userRepository.SaveUser(newEmployee));
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

        public List<UserDTO> GetAllUsers()
        {
            List<Employee> allEmployees = _userRepository.GetAllEmployees();
            return allEmployees.Select(employee => new UserDTO(employee)).ToList();
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            List<Employee> allEmployees = await _userRepository.GetAllEmployeesAsync();
            return allEmployees.Select(employee => new UserDTO(employee)).ToList();
        }

        public List<UserMinimalDTO> GetAllUsersMinimal()
        {
            List<Employee> allEmployees = _userRepository.GetAllEmployees();
            return allEmployees.Select(employee => new UserMinimalDTO(employee)).ToList();
        }

        public async Task<List<UserMinimalDTO>> GetAllUsersMinimalAsync()
        {
            List<Employee> allEmployees = await _userRepository.GetAllEmployeesAsync();
            return allEmployees.Select(emmployee => new UserMinimalDTO(emmployee)).ToList();
        }

        public UserDTO UpdateUser(long id, RegistrationCredentialsDTO registrationCredentials)
        {
            Employee employeeToUpdate = _userRepository.GetEmployeeById(id) ?? throw new Exception("Employee with that id does not exist");
            return new UserDTO(_userRepository.UpdateEmployee(employeeToUpdate, registrationCredentials));
        }

        public async Task<UserDTO> UpdateUserAsync(long id, RegistrationCredentialsDTO registrationCredentials)
        {
            Employee employeeToUpdate = await _userRepository.GetEmployeeByIdAsync(id) ?? throw new Exception("Employee with that id does not exist");
            return new UserDTO(_userRepository.UpdateEmployee(employeeToUpdate, registrationCredentials));
        }

        public void DeleteUser(long id)
        {
            _userRepository.DeleteEmployee(id);
        }

        public async Task DeleteUserAsync(long id)
        {
            await _userRepository.DeleteEmployeeAsync(id);
        }

        public void SendRecoveryEmail(ForgotPasswordDTO email)
        {
            Employee? userToLogin = _userRepository.GetUserByEmail(email.Email);
            if (userToLogin == null)
            {
                Thread.Sleep(200);
                return;
            }

            var code = _helperService.GenerateRandom4DigitNumber();

            _helperService.SendForgotPasswordEmail("Reset password - VegaIT", userToLogin.Email, userToLogin.Name, code.ToString());

            _userRepository.SaveForgotPasswordCode(new VerifyCode(code.ToString(), userToLogin.Email, false));
        }

        public async Task SendRecoveryEmailAsync(ForgotPasswordDTO email)
        {
            Employee? userToLogin = await _userRepository.GetUserByEmailAsync(email.Email);
            if (userToLogin == null)
            {
                Thread.Sleep(200);
                return;
            }

            var code = _helperService.GenerateRandomStringCode();

            await _helperService.SendForgotPasswordEmail("Reset password - VegaIT", userToLogin.Email, userToLogin.Name, code);

            _userRepository.SaveForgotPasswordCode(new VerifyCode(code, userToLogin.Email, false));
        }

        public void AssignNewPassword(NewPasswordDTO credentials)
        {
            VerifyCode verifyCode = _userRepository.GetVerificationCode(credentials.Code) ?? throw new Exception("Code inavlide.");
            Employee userToChangePassowrd = _userRepository.GetUserByEmail(verifyCode.Email)!;
            string newPassword = HashPasword(credentials.Password);
            _userRepository.AssignNewPassword(userToChangePassowrd, newPassword, verifyCode);
        }

        public async Task AssignNewPasswordAsync(NewPasswordDTO credentials)
        {
            VerifyCode verifyCode = await _userRepository.GetVerificationCodeAsync(credentials.Code) ?? throw new Exception("Code inavlide.");
            Employee userToChangePassowrd = await _userRepository.GetUserByEmailAsync(verifyCode.Email) ?? throw new Exception("User does not exist.");
            string newPassword = HashPasword(credentials.Password);
            _userRepository.AssignNewPassword(userToChangePassowrd, newPassword, verifyCode);
        }

        public void ChangePassword(ChangePasswordDTO newPassword, string token)
        {
            string userEmail = _jwtService.GetClaimFromJWT(token, "email");
            Employee userToChangePassowrd = _userRepository.GetUserByEmail(userEmail) ?? throw new Exception("User does not exist.");
            string newPasswordToAssign = HashPasword(newPassword.Password);
            _userRepository.AssignNewPassword(userToChangePassowrd, newPasswordToAssign, null);
        }

        public async Task ChangePasswordAsync(ChangePasswordDTO newPassword, string token)
        {
            string userEmail = _jwtService.GetClaimFromJWT(token, "email");
            Employee userToChangePassowrd = await _userRepository.GetUserByEmailAsync(userEmail) ?? throw new Exception("User does not exist.");
            string newPassToAssign = HashPasword(newPassword.Password);
            _userRepository.AssignNewPassword(userToChangePassowrd, newPassToAssign, null);
        }
    }
}

