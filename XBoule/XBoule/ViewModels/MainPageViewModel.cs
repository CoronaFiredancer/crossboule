using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GoogleAnalytics;
using Microsoft.Phone.Controls;
using XBoule.DataAccess;
using XBoule.Features.Interfaces;
using XBoule.Resources;
using XBoule.Services;

namespace XBoule.ViewModels
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		private VisioClient visioClient;
		private IFileStorageService fileStorageService = new PhoneFileStorageService();
		private IGoogleAnalytics analytics;

		public MainPageViewModel(IFileStorageService fileStorageService, IGoogleAnalytics analytics)
		{
			this.fileStorageService = fileStorageService;
			this.analytics = analytics;

			Items = new ObservableCollection<ItemViewModel>();

			if (!IsDataLoaded)
			{
				LoadData();
				IsDataLoaded = true;
			}
			visioClient = new VisioClient(this.fileStorageService);
		}

		private int counter;
		private bool modified;
		private string remoteResource = "http://device.e-pages.dk/data/karjalainen/959/vector/t1.jpg";
		private string localStoragePath = "\\covers\\karjalainen_959.jpg";

		/// <summary>
		/// A collection for ItemViewModel objects.
		/// </summary>
		public ObservableCollection<ItemViewModel> Items { get; private set; }

		public void PanoramaAction(SelectionChangedEventArgs e) // use as matteo would
		{
			if (e.AddedItems.Count < 1) return;
			if (!(e.AddedItems[0] is PanoramaItem)) return;

			PanoramaItem selectedItem = (PanoramaItem)e.AddedItems[0];

			string strTag = (string)selectedItem.Tag;
			if (strTag.Equals("one")) {
				analytics.TrackView("one");
			}
			// Do places stuff
			else if (strTag.Equals("two")) {
				analytics.TrackView("two");
			}
			// Do routes stuff
			else if (strTag.Equals("three")) {
				analytics.TrackView("three");
			}

		}

		private string _sampleProperty = "Sample Runtime Property Value";
		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding
		/// </summary>
		/// <returns></returns>
		public string SampleProperty
		{
			get
			{
				return _sampleProperty;
			}
			set
			{
				if (value != _sampleProperty)
				{
					_sampleProperty = value;
					NotifyPropertyChanged("SampleProperty");
				}
			}
		}

		/// <summary>
		/// Sample property that returns a localized string
		/// </summary>
		public string LocalizedSampleProperty
		{
			get
			{
				return AppResources.SampleProperty;
			}
		}

		public bool IsDataLoaded
		{
			get;
			private set;
		}

		public void AddOneAction()
		{
			Items.Add(new ItemViewModel
				{
					LineOne = "Dynamically added " + counter,
					LineTwo = "Some words",
					LineThree = "More Words"
				});
			counter++;
		}

		public void RemoveLastActionss()
		{
			if (counter <= 0) return;

			Items.RemoveAt(Items.Count - 1);
			counter--;
		}

		public async void ModifyLastAction()
		{
			var item = Items.First();
			if (!modified)
			{
				item.LineTwo = "Dynamically modified";
				var img = await visioClient.GetImage(remoteResource, localStoragePath);
				item.Image = img;
			}
			else
			{
				item.LineTwo = "Reverted";
				item.Image = "/Assets/PS_99x99.png";
			}
			modified = !modified;
		}



		/// <summary>
		/// Creates and adds a few ItemViewModel objects into the Items collection.
		/// </summary>
		public void LoadData()
		{
			// Sample data; replace with real data
			Items.Add(new ItemViewModel() { LineOne = "runtime one", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu", Image = "/Assets/PS_99x99.png" });
			Items.Add(new ItemViewModel() { LineOne = "runtime two", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
			Items.Add(new ItemViewModel() { LineOne = "runtime three", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
			Items.Add(new ItemViewModel() { LineOne = "runtime four", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
			Items.Add(new ItemViewModel() { LineOne = "runtime five", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
			Items.Add(new ItemViewModel() { LineOne = "runtime six", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
			Items.Add(new ItemViewModel() { LineOne = "runtime seven", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
			Items.Add(new ItemViewModel() { LineOne = "runtime eight", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });
			Items.Add(new ItemViewModel() { LineOne = "runtime nine", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
			Items.Add(new ItemViewModel() { LineOne = "runtime ten", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
			Items.Add(new ItemViewModel() { LineOne = "runtime eleven", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
			Items.Add(new ItemViewModel() { LineOne = "runtime twelve", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
			Items.Add(new ItemViewModel() { LineOne = "runtime thirteen", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
			Items.Add(new ItemViewModel() { LineOne = "runtime fourteen", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
			Items.Add(new ItemViewModel() { LineOne = "runtime fifteen", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
			Items.Add(new ItemViewModel() { LineOne = "runtime sixteen", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });

			IsDataLoaded = true;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(String propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (null != handler)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}