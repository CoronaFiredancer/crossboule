using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleAnalytics.Core;
using XBoule.Features.Interfaces;

namespace XBoule.Features
{
	public  class GoogleAnalyticsWrapper : IGoogleAnalytics
	{
		private static Tracker tracker;
		public GoogleAnalyticsWrapper() {
			tracker = GoogleAnalytics.EasyTracker.GetTracker();
		}

		public void TrackView(string view) {
			tracker.SendView(view);
		}
	}
}
