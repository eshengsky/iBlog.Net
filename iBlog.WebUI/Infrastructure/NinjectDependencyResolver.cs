using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using iBlog.Domain.Abstract;
using iBlog.Domain.Concrete;

namespace iBlog.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            _kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            _kernel.Bind<IPostRepository>().To<PostRepository>();
        }
    }
}
