using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Entities;
using PCL.Domain.Enums;

namespace PCL.Application.Mapper
{
    public class ConfigurationMapping : Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<Breed, BreedDto>().ReverseMap();

            CreateMap<Gender, GenderDto>().ReverseMap();

            CreateMap<TypeAnimal, TypeAnimalDto>().ReverseMap();

            CreateMap<Pet, PetDto>()
            .ForMember(dest => dest.TypeAnimalName, opt => opt.MapFrom(src => src.Breed.TypeAnimal.Name))
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.Name))
            .ForMember(dest => dest.BreedName, opt => opt.MapFrom(src => src.Breed.Name))           
            .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.Name))
            .ReverseMap();

            CreateMap<Owner, OwnerDto>().ReverseMap();

            CreateMap<Veterinarian, VeterinarianDto>().ReverseMap();

            CreateMap<MedicalRecord, MedicalRecordDto>().ReverseMap();

            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.AppointmentStatus, opt => opt.MapFrom(src => ((AppointmentStatus)src.AppointmentStatus).ToString()))
                .ReverseMap()
                .ForMember(dest => dest.AppointmentStatus, opt => opt.MapFrom(src => (int)Enum.Parse(typeof(AppointmentStatus), src.AppointmentStatus)));

            CreateMap<Examination, ExaminationDto>().ReverseMap();

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => ((ServiceType)src.ServiceType).ToString()))
                .ReverseMap()
                .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => (int)Enum.Parse(typeof(ServiceType), src.ServiceType)));

            CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ((ServiceType)src.Type).ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)Enum.Parse(typeof(ServiceType), src.Type)));

            CreateMap<Trainer, TrainerDto>().ReverseMap();

            CreateMap<Training, TrainingDto>().ReverseMap();

            CreateMap<TrainingRecord, TrainingRecordDto>().ReverseMap();

            CreateMap<TrainingResult, TrainingResultDto>().ReverseMap();

            CreateMap<Permission, PermissionDto>().ReverseMap();

            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
