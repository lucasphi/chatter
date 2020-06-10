using Autofac;
using Chatter.Worker;
using Chatter.Worker.Network;
using MediatR.Extensions.Autofac.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Chatter.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IContainer container = CreateContainer();
            var client = container.Resolve<IClient>();
            await client.Connect("127.0.0.1", 5000);
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            RegisterContainerTypes(builder);
            return builder.Build();
        }

        private static void RegisterContainerTypes(ContainerBuilder builder)
        {
            builder.AddMediatR(typeof(Program).Assembly, typeof(IStream).Assembly);
            builder.RegisterType<Client>().As<IStream>().As<IClient>().SingleInstance();
            builder.RegisterType<PacketWriter>().As<IPacketWriter>();
            builder.RegisterType<PacketReader>().As<IPacketReader>();
            builder.RegisterType<PacketFactory>().As<IPacketFactory>();
        }
    }
}
