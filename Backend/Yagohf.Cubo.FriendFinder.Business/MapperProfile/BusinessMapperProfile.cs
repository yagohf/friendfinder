using AutoMapper;
using Yagohf.Cubo.FriendFinder.Model.DTO;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Business.MapperProfile
{
    public class BusinessMapperProfile : Profile
    {
        public BusinessMapperProfile() : this("BusinessMapProfile")
        {
        }

        protected BusinessMapperProfile(string profileName) : base(profileName)
        {
            this.MapearDTOsParaEntidades();
            this.MapearEntidadesParaDTOs();
        }

        private void MapearEntidadesParaDTOs()
        {
            CreateMap<Amigo, AmigoDTO>();
        }

        private void MapearDTOsParaEntidades()
        {
            CreateMap<AmigoDTO, Amigo>();
        }
    }
}
