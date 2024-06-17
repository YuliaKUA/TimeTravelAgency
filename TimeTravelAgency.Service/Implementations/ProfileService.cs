using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.DAL.Interfaces;
using TimeTravelAgency.DAL.Repositories;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;
using TimeTravelAgency.Domain.Response;
using TimeTravelAgency.Domain.ViewModels.Profile;
using TimeTravelAgency.Service.Interfaces;

namespace TimeTravelAgency.Service.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<IBaseResponse<bool>> CreateProfileById(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                await _profileRepository.Create(new Uprofile { Id = id });
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateProfile] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteProfileById(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var temp_profile = await _profileRepository.GetById(id);
                if (temp_profile == null)
                {
                    baseResponse.Description = "Обьект не найден";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;

                    return baseResponse;
                }
                await _profileRepository.Delete(temp_profile);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteProfileById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Uprofile>> Edit(int id, Uprofile profile)
        {
            var baseResponse = new BaseResponse<Uprofile>();
            try
            {
                var temp_profile = await _profileRepository.GetById(id);
                if (temp_profile == null)
                {
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    baseResponse.Description = "Профиль не найден";
                    return baseResponse;
                }

                temp_profile.FirstName = profile.FirstName;
                temp_profile.LastName = profile.LastName;
                temp_profile.Age = profile.Age;
                temp_profile.Email = profile.Email;
                temp_profile.Phone = profile.Phone;
                temp_profile.Uaddress = profile.Uaddress;

                await _profileRepository.Update(temp_profile);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<Uprofile>()
                {
                    Description = $"[EditProfile] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ProfileViewModel>> GetProfileById(int id)
        {
            var baseResponse = new BaseResponse<ProfileViewModel>();
            try
            {
                var temp_profile = await _profileRepository.GetById(id);
                if (temp_profile == null)
                {
                    baseResponse.Description = "Обьект не найден";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;

                    return baseResponse;
                }

                baseResponse.Data = new ProfileViewModel
                {
                    FirstName = temp_profile.FirstName,
                    LastName = temp_profile.LastName,
                    Age = temp_profile.Age,
                    Email = temp_profile.Email,
                    Phone = temp_profile.Phone,
                    Uaddress = temp_profile.Uaddress
                };
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProfileViewModel>()
                {
                    Description = $"[GetProfileById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Uprofile>>> GetProfiles()
        {
            var baseResponse = new BaseResponse<IEnumerable<Uprofile>>();
            try
            {
                var profiles = await _profileRepository.SelectAll();
                if (profiles.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = profiles;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Uprofile>>()
                {
                    Description = $"[GetProfiles] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> AddRangeProfiles(IEnumerable<Uprofile> profiles)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                await _profileRepository.AddRange(profiles);

                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[AddRangeProfiles] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
