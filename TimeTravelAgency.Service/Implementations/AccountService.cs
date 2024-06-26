﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.DAL.Interfaces;
using TimeTravelAgency.DAL.Repositories;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;
using TimeTravelAgency.Domain.Helpers;
using TimeTravelAgency.Domain.Response;
using TimeTravelAgency.Domain.ViewModels.Account;
using TimeTravelAgency.Service.Interfaces;

namespace TimeTravelAgency.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository, ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<User>> CreateUser(User user)
        {
            try
            {
                var baseResponse = await GetUserByLogin(user.ULogin);
                if (baseResponse.StatusCode == StatusCode.OK)
                {
                    baseResponse.Description = "В базе уже есть пользователь с таким логином";
                    baseResponse.StatusCode = StatusCode.LoginAlreadyUse;

                    return baseResponse;
                }

                await _userRepository.Create(user);
                baseResponse.StatusCode = StatusCode.OK;

                baseResponse = await GetUserByLogin(user.ULogin);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[CreateUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUserById(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var temp_user = await _userRepository.GetById(id);
                if (temp_user == null)
                {
                    baseResponse.Description = "Обьект не найден";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;

                    return baseResponse;
                }
                await _userRepository.Delete(temp_user);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteUserById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> Edit(int id, User user)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var temp_user = await _userRepository.GetById(id);
                if (temp_user == null)
                {
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    baseResponse.Description = "Пользователь не найден";
                    return baseResponse;
                }

                if (user.ULogin != null)
                {
                    var exist_login = await _userRepository.GetByName(user.ULogin);
                    if (exist_login == null)
                    {
                        temp_user.ULogin = user.ULogin;
                    }
                }
                if (user.HashPassword != null)
                {
                    temp_user.HashPassword = user.HashPassword;
                }
                temp_user.UProfile = user.UProfile;

                await _userRepository.Update(temp_user);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[EditUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> GetUserById(int id)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var temp_user = await _userRepository.GetById(id);
                if (temp_user == null)
                {
                    baseResponse.Description = "Обьект не найден";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;

                    return baseResponse;
                }

                baseResponse.Data = temp_user;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUserById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> GetUserByLogin(string login)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var temp_user = await _userRepository.GetByName(login);
                if (temp_user == null)
                {
                    baseResponse.Description = "Обьект не найден";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;

                    return baseResponse;
                }

                baseResponse.Data = temp_user;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUserByLogin] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<User>>> GetUsers()
        {
            var baseResponse = new BaseResponse<IEnumerable<User>>();
            try
            {
                var users = await _userRepository.SelectAll();
                if (users.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = users;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                _logger.LogInformation("Мы перешли в метод Login");
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.ULogin == model.Login);
                if (user == null)
                {
                    _logger.LogInformation($"Пользователь {model.Login} не найден");
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = $"Пользователь {model.Login} не найден"
                    };
                }

                if (user.HashPassword != HashPasswordHelper.HashPassowrd(model.Password))
                {
                    _logger.LogInformation("Введен неверный пароль или логин");
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или логин"
                    };
                }
                var result = Authenticate(user);
                _logger.LogInformation("Успешная аутентификация пользователя: " + model.Login);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.ULogin == model.Login);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                    };
                }

                user = new User()
                {
                    ULogin = model.Login,
                    URole = Role.User,
                    HashPassword = HashPasswordHelper.HashPassowrd(model.Password),
                };

                await _userRepository.Create(user);

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.ULogin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.URole.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public async Task<IBaseResponse<bool>> AddRangeUsers(IEnumerable<User> users)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                await _userRepository.AddRange(users);

                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[AddRangeUsers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
