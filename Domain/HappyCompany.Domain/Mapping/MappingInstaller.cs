using AutoMapper;
using HappyCompany.Domain.Mapping.Profiles;
using HappyCompany.Domain.Interface.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCompany.Domain.Mapping
{
    public class MappingInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                //Add Your Profiles Below
                mc.AddProfile(new PaginationQueryToPaginationFilterProfile());
                mc.AddProfile<UpdateUserDtoToUserProfile>();
             
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
            
        }
    }
}
