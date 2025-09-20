using AutoMapper;
using CryptoApp.Domain.Models;
using CryptoApp.Infrastructure.DTO;

namespace CryptoApp.Infrastructure.Mappers
{
    public sealed class CryptoAppMapper : Profile
    {
        public CryptoAppMapper()
        {
            CreateMap<CoinGeckoCoinDto, CryptoCoin>();
        }
    }
}
