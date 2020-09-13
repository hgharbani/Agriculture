using System;
using System.Threading;
using Agriculture.Data;
using StructureMap;
using StructureMap.Pipeline;

namespace MyCmsWebsite
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> ContainerBuilder =
            new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return ContainerBuilder.Value; }
        }

        private static Container DefaultContainer()
        {
            var container = new Container(x =>
            {
                
                x.For<IUnitOfWork>().Use(new AgricultureContext());
                x.Scan(scan =>
                {
                   
                    scan.AssemblyContainingType<IUnitOfWork>();
                    scan.WithDefaultConventions();
                });
            });

            return container;
        }
    }
}