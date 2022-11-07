using DataVerse.Dal;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace DataVerse
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<ICustomer_DAL, Customer_DAL>();
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}