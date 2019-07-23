using System.Web.Mvc;
using TreeEditTestApp.API;
using TreeEditTestApp.Contracts.Data;
using TreeEditTestApp.Contracts.Services;
using TreeEditTestApp.Data.DataProviders;
using TreeEditTestApp.DataModel;
using TreeEditTestApp.Services.TreeItems;
using Unity;
using Unity.Mvc5;

namespace TreeEditTestApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IDataProvider<TreeItem>, TreeItemsDataProvider>();
            container.RegisterType<ITreeItemService, TreeItemService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}