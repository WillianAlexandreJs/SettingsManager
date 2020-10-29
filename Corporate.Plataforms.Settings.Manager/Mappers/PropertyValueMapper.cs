using AutoMapper;
using Corporate.Plataforms.Settings.Manager.Entities;
using Corporate.Plataforms.Settings.Manager.Models;
using Corporate.Plataforms.Settings.Model;

namespace Corporate.Plataforms.Settings.Manager.Mappers
{
    public class PropertyValueMapper : Profile
    {
        /// <summary>
        /// Método construtor
        /// </summary>
        public PropertyValueMapper()
        {
            CreateMap<PropertyData, PropertyValue>()
            .ForMember(dto => dto.Name, opt => opt.MapFrom(model => model.PropertyName))
            .ForMember(dto => dto.SettingReference, opt => opt.MapFrom(model => model.SettingReference))
            .ForMember(dto => dto.Type, opt => opt.MapFrom(model => model.PropertyType))
            .ForMember(dto => dto.Value, opt => opt.MapFrom(model => model.PropertyValue))
            .ReverseMap()
            .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}