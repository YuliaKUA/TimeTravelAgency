using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    public class TourService : ITourService
    {
        private readonly IBaseRepository<Tour> _tourRepository;

        public TourService(IBaseRepository<Tour> tourRepository)
        {
            _tourRepository = tourRepository;
        }
        public async Task<IBaseResponse<Tour>> CreateTour(Tour tour)
        {
            var baseResponse = new BaseResponse<Tour>();
            try
            {
                //var temp_tour = new Tour()
                //{
                //    TypeTour = (TypeTour)Convert.ToInt32(tour.TypeTour),
                //    Title = tour.Title,
                //    DateStart = Convert.ToDateTime(tour.DateStart),
                //    DateEnd = Convert.ToDateTime(tour.DateEnd),
                //    Descriptions = tour.Descriptions,
                //    Price = Convert.ToDouble(tour.Price),
                //    NumberOfPlaces = Convert.ToInt32(tour.NumberOfPlaces)
                //};

                await _tourRepository.Create(tour);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Tour>()
                {
                    Description = $"[CreateTour] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteTourById(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var temp_tour = await _tourRepository.GetById(id);
                if (temp_tour == null)
                {
                    baseResponse.Description = "Обьект не найден";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;

                    return baseResponse;
                }
                await _tourRepository.Delete(temp_tour);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteTourById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Tour>> Edit(int id, Tour tour)
        {
            var baseResponse = new BaseResponse<Tour>();
            try
            {
                var temp_tour = await _tourRepository.GetById(id);
                if (temp_tour == null)
                {
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    baseResponse.Description = "Тур не найден";
                    return baseResponse;
                }

                temp_tour.TypeTour = tour.TypeTour;
                temp_tour.Title = tour.Title;
                temp_tour.DateStart = Convert.ToDateTime(tour.DateStart);
                temp_tour.DateEnd = Convert.ToDateTime(tour.DateEnd);
                temp_tour.Descriptions = tour.Descriptions;
                temp_tour.Price = Convert.ToDouble(tour.Price);
                temp_tour.NumberOfPlaces = Convert.ToInt32(tour.NumberOfPlaces);

                await _tourRepository.Update(temp_tour);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<Tour>()
                {
                    Description = $"[EditTour] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Tour>> Edit(string name, Tour tour)
        {
            var baseResponse = new BaseResponse<Tour>();
            try
            {
                var temp_tour = await _tourRepository.GetByName(name);
                if (temp_tour == null)
                {
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    baseResponse.Description = "Тур не найден";
                    return baseResponse;
                }

                temp_tour.TypeTour = tour.TypeTour;
                temp_tour.Title = tour.Title;
                temp_tour.DateStart = Convert.ToDateTime(tour.DateStart);
                temp_tour.DateEnd = Convert.ToDateTime(tour.DateEnd);
                temp_tour.Descriptions = tour.Descriptions;
                temp_tour.Price = Convert.ToDouble(tour.Price);
                temp_tour.NumberOfPlaces = Convert.ToInt32(tour.NumberOfPlaces);

                await _tourRepository.Update(temp_tour);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<Tour>()
                {
                    Description = $"[EditTour] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Tour>> GetTourById(int id)
        {
            var baseResponse = new BaseResponse<Tour>();
            try
            {
                //var temp_tour = _tourRepository.GetAll().Where(x => x.Price > 200);
                var temp_tour = await _tourRepository.GetById(id);
                if (temp_tour == null)
                {
                    baseResponse.Description = "Обьект не найден";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;

                    return baseResponse;
                }

                baseResponse.Data = temp_tour;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Tour>()
                {
                    Description = $"[GetTourById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Tour>> GetTourByTitle(string title)
        {
            var baseResponse = new BaseResponse<Tour>();
            try
            {
                //var temp_tour = _tourRepository.GetAll().Where(x => x.Price > 200);
                var temp_tour = await _tourRepository.GetByName(title);
                if (temp_tour == null)
                {
                    baseResponse.Description = "Обьект не найден";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;

                    return baseResponse;
                }

                baseResponse.Data = temp_tour;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Tour>()
                {
                    Description = $"[GetTourByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Tour>>> GetTours()
        {
            var baseResponse = new BaseResponse<IEnumerable<Tour>>();
            try
            {
                var tours = await _tourRepository.SelectAll();
                if (tours.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = tours;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Tour>>()
                {
                    Description = $"[GetTours] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
