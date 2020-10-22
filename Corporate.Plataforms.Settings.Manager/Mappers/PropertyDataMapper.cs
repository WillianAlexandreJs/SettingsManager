using AutoMapper;
using Corporate.Plataforms.Settings.Manager.Entities;
using Corporate.Plataforms.Settings.Manager.Models;

namespace Corporate.Plataforms.Settings.Manager.Mappers
{
    public class PropertyDataMapper : Profile
    {
        /// <summary>
        /// Método construtor
        /// </summary>
        public PropertyDataMapper()
        {
            CreateMap<PropertyDataEntity, PropertyData>()
            .ForMember(dto => dto.InstanceName, opt => opt.MapFrom(model => model.InstanceName))
            .ForMember(dto => dto.SettingReference, opt => opt.MapFrom(model => model.SettingReference))
            .ForMember(dto => dto.PropertyName, opt => opt.MapFrom(model => model.PropertyName))
            .ForMember(dto => dto.PropertyType, opt => opt.MapFrom(model => model.PropertyType))
            .ForMember(dto => dto.PropertyValue, opt => opt.MapFrom(model => model.PropertyValue))
            .ReverseMap()
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}