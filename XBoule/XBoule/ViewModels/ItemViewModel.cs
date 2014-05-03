using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace XBoule.ViewModels
{
	public class ItemViewModel : INotifyPropertyChanged
	{
		private string lineOne;
		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding.
		/// </summary>
		/// <returns></returns>
		public string LineOne
		{
			get
			{
				return lineOne;
			}
			set
			{
				if (value != lineOne)
				{
					lineOne = value;
					NotifyPropertyChanged("LineOne");
				}
			}
		}

		private string lineTwo;
		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding.
		/// </summary>
		/// <returns></returns>
		public string LineTwo
		{
			get
			{
				return lineTwo;
			}
			set
			{
				if (value != lineTwo)
				{
					lineTwo = value;
					NotifyPropertyChanged("LineTwo");
				}
			}
		}

		private string _lineThree;
		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding.
		/// </summary>
		/// <returns></returns>
		public string LineThree
		{
			get
			{
				return _lineThree;
			}
			set
			{
				if (value != _lineThree)
				{
					_lineThree = value;
					NotifyPropertyChanged("LineThree");
				}
			}
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