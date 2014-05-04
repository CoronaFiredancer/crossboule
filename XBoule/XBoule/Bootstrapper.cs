using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using XBoule.Services;
using XBoule.ViewModels;

namespace XBoule
{
	public class Bootstrapper : PhoneBootstrapper
	{
		private PhoneContainer container;

		protected override void Configure()
		{
			container = new PhoneContainer();
			container.RegisterPhoneServices(RootFrame);
			container.PerRequest<MainPageViewModel>();
			container.PerRequest<ItemViewModel>();

			container.PerRequest<IFileStorageService, PhoneFileStorageService>();

		}
		protected override object GetInstance(Type service, string key)
		{
			return container.GetInstance(service, key);
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			container.BuildUp(instance);
		}
	}
}
