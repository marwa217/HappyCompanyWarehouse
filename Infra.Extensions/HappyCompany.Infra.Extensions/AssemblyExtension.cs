using HappyCompany.Infra.Utils;
using System.Reflection;

namespace HappyCompany.Infra.Extensions
{
    public static class AssemblyExtension
    {
        private static ApplicationUtils ApplicationUtils = new ApplicationUtils();

        public static List<Assembly> GetAppAssemblies(this Assembly assembly)
        {
            return ApplicationUtils.GetAppAssemblies(assembly);
        }

    }
}
