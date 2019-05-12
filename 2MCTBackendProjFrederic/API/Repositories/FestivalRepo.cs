using API.Data;
using Microsoft.EntityFrameworkCore;
using Models.BackendDTO;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class FestivalRepo : IFestivalRepo
    {
        readonly BackendProjContext _backendProjContext;
        public FestivalRepo(BackendProjContext backendProjContext)
        {
            _backendProjContext = backendProjContext;
        }

        public async Task<TblFestivals> GetFestivalName(TblFestivalsDTO tblFestivalsDTO)
        {
            try
            {
                var FestivalsResults = _backendProjContext.TblFestivals.Where(a => a.MusicEvenementName == tblFestivalsDTO.MusicEvenementName).Select(x => new TblFestivals
                {
                    MusicEvenementId = x.MusicEvenementId
                }).FirstOrDefault();

                if(FestivalsResults != null)
                {
                    return await Task.FromResult(FestivalsResults);
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                throw new ArgumentException("Musicevement not found");
            }
        }

        public async Task<TblPrice> GetFestivalTypeCategory(TblPriceDTO tblPriceDTO)
        {
            try
            {
                var PriceResult = _backendProjContext.TblTypeCategory.Where(y => y.Name == tblPriceDTO.Name).Select(z => new TblPrice()
                {
                    Type = z.Type
                }).FirstOrDefault();

                if(PriceResult != null)
                {
                    return await Task.FromResult(PriceResult);
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<List<TblFestivalsDTO>> GetFestivals()
        {
            try
            {
                var FestivalsResults = _backendProjContext.TblFestivals.Include(x => x.TblPrice).ThenInclude(y => y.TypeNavigation).OrderBy(x => x.MusicEvenementName).Select(x => new TblFestivalsDTO {
                    MusicEvenementName = x.MusicEvenementName,
                    Name = x.TblPrice.Where(e => e.Priceid == x.PriceListid).Select(xa => xa.TypeNavigation.Name).FirstOrDefault(),
                    Price = x.TblPrice.Where(e => e.Priceid == x.PriceListid).Select(y => y.Price).FirstOrDefault(),
                    MaxSeatNumbers = x.MaxSeatNumbers
                }).ToList();
                if(FestivalsResults != null)
                {
                    return await Task.FromResult(FestivalsResults);
                }
                else
                {
                    return null;
                }

            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<TblPriceDTO> GetPriceofCurrentFestivalType(TblReservation tblReservation)
        {
            try
            {
                var GetPriceFestivalResult = _backendProjContext.TblPrice.Where(a => a.MusicEvenementId == tblReservation.MusicEvementId && a.Type == tblReservation.Price.Type).Select(y => new TblPriceDTO()
                {
                    Name = y.TypeNavigation.Name,
                    Price = y.Price
                }).FirstOrDefault();

                if(GetPriceFestivalResult != null)
                {
                    return await Task.FromResult(GetPriceFestivalResult);
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<TblPrice> GetPrice(TblReservation tblReservation)
        {
            try
            {
                var GetPriceResult = _backendProjContext.TblPrice.Where(a => a.Price == tblReservation.Price.Price && a.MusicEvenementId == tblReservation.MusicEvementId && a.Type == tblReservation.Price.Type).Select(
                    y => new TblPrice()
                    {
                        Priceid = y.Priceid
                    }
                ).FirstOrDefault();

                if(GetPriceResult != null)
                {
                    return await Task.FromResult(GetPriceResult);
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
