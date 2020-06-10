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
            var client = container.Resolve<Client>();
            await client.Connect("127.0.0.1", 5000);

            var text = Console.ReadLine();
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
            builder.RegisterType<Client>().SingleInstance().AsSelf().As<IStream>();
            builder.RegisterType<PacketWriter>().As<IPacketWriter>();
            builder.RegisterType<PacketReader>().As<IPacketReader>();
            builder.RegisterType<PacketFactory>().As<IPacketFactory>();
        }
    }
}
