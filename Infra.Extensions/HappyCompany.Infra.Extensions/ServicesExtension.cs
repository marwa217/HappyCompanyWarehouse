using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using HappyCompany.Domain.Interface.Common;

namespace HappyCompany.Infra.Extensions
{
    public static class ServicesExtension
    {
        public static void InstallServicesAssembly(this IServiceCollection services, IConfiguration configuration)
        {

            List<IInstaller> installers = new List<IInstaller>();

            // hint: this will load used assemblies only
            var appAssembly = Assembly.Load("HappyCompany.API");

            var appAssemplies = appAssembly.GetAppAssemblies();


            foreach (Assembly a  in appAssemplies)
            {

                var assemplyInstaller = a.ExportedTypes.Where(x =>
                               typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                                .Select(Activator.CreateInstance)
                                .Cast<IInstaller>().ToList();

                installers.AddRange(assemplyInstaller);
            }

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
