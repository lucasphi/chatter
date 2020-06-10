using Autofac;
using Chatter.Worker;
using Chatter.Worker.Network;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace Chatter.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = CreateContainer();
            var server = container.Resolve<Server>();
            server.Run("127.0.0.1", 5000);
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            RegisterContainerTypes(builder);
            return builder.Build();
        }

        private static void RegisterContainerTypes(ContainerBuilder builder)
        {
            builder.AddMediatR(typeof(IStream).Assembly);
            builder.RegisterType<Server>().SingleInstance().AsSelf();
            builder.RegisterType<PacketWriter>().As<IPacketWriter>();
            builder.RegisterType<PacketReader>().As<IPacketReader>();
            builder.RegisterType<PacketFactory>().As<IPacketFactory>();
        }
    }
}
