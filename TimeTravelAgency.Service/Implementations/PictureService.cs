using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.DAL.Interfaces;
using TimeTravelAgency.DAL.Repositories;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;
using TimeTravelAgency.Domain.Response;
using TimeTravelAgency.Service.Interfaces;

namespace TimeTravelAgency.Service.Implementations
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;

        public PictureService(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public async Task<IBaseResponse<Picture>> CreatePicture(Picture picture)
        {
            var baseResponse = new BaseResponse<Picture>();
            try
            {
                await _pictureRepository.Create(picture);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Picture>()
                {
                    Description = $"[CreatePicture] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeletePictureByTitle(string title)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var temp_picture = await _pictureRepository.GetByTitle(title);
                if (temp_picture == null)
                {
                    baseResponse.Description = "Обьект не найден";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;

                    return baseResponse;
                }
                await _pictureRepository.Delete(temp_picture);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeletePictureByTitle] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Picture>> Edit(int id, Picture picture)
        {
            var baseResponse = new BaseResponse<Picture>();
            try
            {
                var temp_picture = await _pictureRepository.GetById(id);
                if (temp_picture == null)
                {
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    baseResponse.Description = "Картинка не найдена";
                    return baseResponse;
                }

                temp_picture.ViewName = picture.ViewName;
                temp_picture.Title = picture.Title;
                temp_picture.Href = picture.Href;
                temp_picture.Image = picture.Image;

                await _pictureRepository.Update(temp_picture);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<Picture>()
                {
                    Description = $"[EditPicture] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Picture>> GetPictureByTitle(string title)
        {
            var baseResponse = new BaseResponse<Picture>();
            try
            {
                var temp_picture = await _pictureRepository.GetByTitle(title);
                if (temp_picture == null)
                {
                    baseResponse.Description = "Обьект не найден";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;

                    return baseResponse;
                }

                baseResponse.Data = temp_picture;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Picture>()
                {
                    Description = $"[GetPictureByTitle] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Picture>>> GetPictures(ViewName viewName)
        {
            var baseResponse = new BaseResponse<IEnumerable<Picture>>();
            try
            {
                var pictures = await _pictureRepository.SelectPictures(viewName);
                if (pictures.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = pictures;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Picture>>()
                {
                    Description = $"[GetPictures] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            };
        }
    }
}
