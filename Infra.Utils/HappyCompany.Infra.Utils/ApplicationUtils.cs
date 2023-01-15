using System.Reflection;

namespace HappyCompany.Infra.Utils
{
    public class ApplicationUtils
    {
        List<Assembly> assemblies = new List<Assembly>();

        private AssemblyName[] GetHappyCompanyAssemplies(Assembly assemply)
        {
            var referencedAssemplies = assemply.GetReferencedAssemblies();
            return referencedAssemplies.Where(x => x.FullName.Contains("Zid")).ToArray();
        }
        public List<Assembly> GetAppAssemblies(Assembly appAssembly)
        {
            GetReferencedAssemblies(appAssembly);
            return assemblies;
        }
        private void GetReferencedAssemblies(Assembly assemply)
        {
            AddAssemply(assemply);

            var foodicsReferencedAssemplies = GetHappyCompanyAssemplies(assemply);

            foreach (AssemblyName assemblyName in foodicsReferencedAssemplies)
            {
                var refAssembly = Assembly.Load(assemblyName);
                var isAdded = AddAssemply(refAssembly);

                if (isAdded)
                    GetAppAssemblies(refAssembly);
            }
        }
        private bool AddAssemply(Assembly assembly)
        {
            if (!assemblies.Select(s => s.FullName).Contains(assembly.FullName))
            {
                assemblies.Add(assembly);
                return true;
            }
            return false;
        }
    }
}