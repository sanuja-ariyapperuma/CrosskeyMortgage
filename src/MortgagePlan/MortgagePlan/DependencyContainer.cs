using Autofac;
using Autofac.Integration.Mvc;
using MortgagePlan.Business;
using MortgagePlan.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MortgagePlan
{
    public class DependencyContainer
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MortgageCalculation>().As<IMortgageCalculation>();
            builder.RegisterType<ProspectFileReader>().As<IProspectFileReader>();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}