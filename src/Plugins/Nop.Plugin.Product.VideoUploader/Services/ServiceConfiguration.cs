using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;

namespace Nop.Plugin.Product.VideoUploader.Services
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 1;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<VideoService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ProductResolveService>().AsSelf().InstancePerLifetimeScope();
        }
    }
}