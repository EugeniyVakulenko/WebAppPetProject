using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using AutoMapper;
using Infrastructure.Interfaces;
using Domain.DTO;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Domain.Exceptions;


namespace Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private IJwtFactory _jwtFactory;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public AccountService(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork, IJwtFactory jwtFactory)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _unitOfWork = unitOfWork;
        }
        private async Task<User> CreateUser(UserDTO userdto)
        {
            if (await _userManager.FindByEmailAsync(userdto.Email) != null) throw new Exception(); //make exceptions
            if (await _userManager.FindByNameAsync(userdto.UserName) != null) throw new Exception();
            var user = new User
            {
                UserName = userdto.UserName,
                Email = userdto.Email
            };
            var result = await _userManager.CreateAsync(user, userdto.Password);
            if (result.Succeeded) return user;
            else return null;
        }
        public async Task<UserDTO> RegisterRegularUser(UserDTO userDTO)
        {
            var user = await CreateUser(userDTO);
            if (user == null) throw new ArgumentNullException(nameof(user), "Couldn't create user");
            else
            {
                await _userManager.AddToRoleAsync(user, "RegularUser");
            }
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO> RegisterModerator(UserDTO userDTO)
        {
            var user = await CreateUser(userDTO);
            if (user == null) throw new ArgumentNullException(nameof(user), "Couldn't create user");
            else
            {
                await _userManager.AddToRoleAsync(user, "Moderator");
            }
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<IEnumerable<UserDTO>> GetAllRegularUsers()
        {
            List<UserDTO> usersdto = new List<UserDTO>();
            var users = (await _userManager.GetUsersInRoleAsync("RegularUser")).ToList();
            foreach (var item in users)
            {
                usersdto.Add(_mapper.Map<UserDTO>(item));
            }
            return usersdto;
        }
        public async Task<IEnumerable<UserDTO>> GetAllModerators()
        {
            List<UserDTO> usersdto = new List<UserDTO>();
            var users = (await _userManager.GetUsersInRoleAsync("Moderator")).ToList();
            foreach (var item in users)
            {
                usersdto.Add(_mapper.Map<UserDTO>(item));
            }
            return usersdto;
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            List<UserDTO> users = (await GetAllRegularUsers()).ToList();
            users.AddRange(await GetAllModerators());
            return users;
        }
        public async Task<UserDTO> GetUserById(string id, string token)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new ArgumentNullException(nameof(user), "Couldn't find user with this id");
            string claimsId = _jwtFactory.GetUserIdClaim(token);
            if (claimsId == id) return _mapper.Map<UserDTO>(user);
            string claimsRole = _jwtFactory.GetUserRoleClaim(token);
            if (claimsRole == "Moderator")
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Any(r => r == "Moderator" || r == "Admin")) throw new NotEnoughRightsException();
                else return _mapper.Map<UserDTO>(user);
            }
            else if (claimsRole == "Admin")
            {
                return _mapper.Map<UserDTO>(user);
            }
            else throw new NotEnoughRightsException();
        }
        public async Task<bool> DeleteUser(string id, string token)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (token == null) throw new ArgumentNullException(nameof(token));
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new ArgumentNullException(nameof(user), "Couldn't find user with this id");
            var claimsId = _jwtFactory.GetUserIdClaim(token);
            if (claimsId == id) return (await _userManager.DeleteAsync(user)).Succeeded;
            var claimsRole = _jwtFactory.GetUserRoleClaim(token);
            if (claimsRole == "Moderator")
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Any(r => r == "Moderator" || r == "Admin")) throw new NotEnoughRightsException();
                else return (await _userManager.DeleteAsync(user)).Succeeded;
            }
            else if (claimsRole == "Admin")
            {
                return (await _userManager.DeleteAsync(user)).Succeeded;
            }

            else throw new NotEnoughRightsException();
        }

        public async Task UpdateUser(string id, UserDTO user, string token)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user.UserName == null && user.Email == null) throw new ArgumentNullException(nameof(user));
            var userEntity = await _userManager.FindByIdAsync(id);
            string claimsId = _jwtFactory.GetUserIdClaim(token);
            if (userEntity == null) throw new ArgumentNullException(nameof(userEntity), "Couldn't find user with this id");

            if (claimsId == id)
            {
                if (user.UserName != null && userEntity.UserName.CompareTo(user.UserName) != 0)
                {
                    var checkIfNameIsTaken = await _userManager.FindByNameAsync(user.UserName);
                    if (checkIfNameIsTaken != null) throw new NameIsAlreadyTakenException();
                    userEntity.UserName = user.UserName;
                }
                else if (user.Email != null && userEntity.Email.CompareTo(user.Email) != 0)
                {
                    var checkIfNameIsTaken = await _userManager.FindByEmailAsync(user.Email);
                    if (checkIfNameIsTaken != null) throw new NameIsAlreadyTakenException();
                    userEntity.Email = user.Email;
                }
                await _userManager.UpdateAsync(userEntity);
            }
            else throw new NotEnoughRightsException();
        }
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersByUserId(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var userEntity = await _userManager.FindByIdAsync(id);
            if (userEntity == null) throw new ArgumentNullException(nameof(userEntity),"No user with such id");
            var orders = _unitOfWork.Orders.Find(i => i.User.Id == id);
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
       

        public static string SaveImage(IFormFile image)
        {
            var randomName = $"{Guid.NewGuid()}." + image.ContentType.Substring(6);
            string path = "\\Upload\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fileStream = System.IO.File.Create(path + randomName))
            {
                image.CopyTo(fileStream);
                fileStream.Flush();
                return "\\Upload\\" + randomName;
            }
        }
    }
}
