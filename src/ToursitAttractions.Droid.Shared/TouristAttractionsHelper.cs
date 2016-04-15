using System;
using System.Collections.Generic;
using Android.Gms.Location;
using Android.Gms.Maps.Model;
using Com.Google.Maps.Android;
using ToursitAttractions.Droid.Shared;

namespace TouristAttractions
{
	public static class TouristAttractionsHelper
	{
		private static readonly string citySydney = "Sydney";

		private static readonly string testCity = CitySydney;

		private static readonly float triggerRadius = 2000; // 2KM
		private static readonly int triggerTransition = Geofence.GeofenceTransitionEnter |
		                                                         Geofence.GeofenceTransitionExit;
		private static readonly long expirationDuration = Geofence.NeverExpire;

		private static readonly Dictionary<string, LatLng> cityLocations = new Dictionary<string, LatLng>();

		private static readonly Dictionary<string, List<Attraction>> attractions = new Dictionary<string, List<Attraction>>();

		public static string CitySydney
		{
			get
			{
				return citySydney;
			}
		}

		public static string TestCity
		{
			get
			{
				return testCity;
			}
		}

		public static Dictionary<string, LatLng> CityLocations
		{
			get
			{
				return cityLocations;
			}
		}

		public static Dictionary<string, List<Attraction>> Attractions
		{
			get
			{
				return attractions;
			}
		}

		static TouristAttractionsHelper()
		{
			cityLocations.Add(citySydney, new LatLng(-33.873651, 151.2068896));

			var attaractions = new List<Attraction>();

			attaractions.Add(new Attraction()
			{
				Name = "Boston Common",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://lh5.googleusercontent.com/-7fb5ybQhUbo/VGLWjIL4RmI/AAAAAAAAACM/2jLe_msj_tk/w600-no/IMG_0049.JPG"),
				SecondaryImageUrl = new Uri("https://lh3.googleusercontent.com/-EFEw6s7mT6I/VGLkCH4Xt4I/AAAAAAAAADY/ZlznhaQvb8E/w600-no/DSC_2775.JPG"),
				Location = new LatLng(42.3551128, -71.0677709),
				City = "Boston"
			});

			/**
			 * All photos used with permission under the Creative Commons Attribution-ShareAlike License.
			 */
			attaractions.Add(new Attraction()
			{
				Name = "Sydney Opera House",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://lh5.googleusercontent.com/-7fb5ybQhUbo/VGLWjIL4RmI/AAAAAAAAACM/2jLe_msj_tk/w600-no/IMG_0049.JPG"),
				SecondaryImageUrl = new Uri("https://lh3.googleusercontent.com/-EFEw6s7mT6I/VGLkCH4Xt4I/AAAAAAAAADY/ZlznhaQvb8E/w600-no/DSC_2775.JPG"),
				Location = new LatLng(-33.858667, 151.214028),
				City = citySydney
			});

			attaractions.Add(new Attraction()
			{
				Name = "Sydney Harbour Bridge",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://lh6.googleusercontent.com/-ORRJtfLQlaw/VGLmQPv3n8I/AAAAAAAAAD8/2TzSCCPzl9k/w600-no/DSC04114.JPG"),
				SecondaryImageUrl = new Uri("https://lh4.googleusercontent.com/-ch9Kk-7pD68/VGLkCNh5niI/AAAAAAAAADc/ztxkRHWX-po/w600-no/DSC_2739.JPG"),
				Location = new LatLng(-33.852222, 151.210556),
				City = citySydney
			});

			attaractions.Add(new Attraction()
			{
				Name = "Darling Harbour",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://lh5.googleusercontent.com/-qX43g6s92LY/VGLaTT3N35I/AAAAAAAAAC8/BbueQmch0Rw/w600-no/68001.jpg"),
				SecondaryImageUrl = new Uri("https://lh6.googleusercontent.com/-SQ6T1Ure6l8/VGLaTg2iGuI/AAAAAAAAACo/m6_RkTW2G1o/w600-no/IMG_20140201_082851.jpg"),
				Location = new LatLng(-33.8723, 151.19896),
				City = citySydney
			});

			attaractions.Add(new Attraction()
			{
				Name = "Bondi Beach",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://lh4.googleusercontent.com/-wbNgVdUkBiE/VHe99hGVtNI/AAAAAAAAAFY/fAHfhchNLJw/w600-no/IMG_20141124_143747.jpg"),
				SecondaryImageUrl = new Uri("https://lh6.googleusercontent.com/-sjY_xlEOic4/VHe9-I4DD9I/AAAAAAAAAFI/Mt0VnjU7SxQ/w600-no/IMG_20141124_144008.jpg"),
				Location = new LatLng(-33.89102, 151.277726),
				City = citySydney
			});

			attaractions.Add(new Attraction()
			{
				Name = "Taronga Zoo",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://lh6.googleusercontent.com/-kypwDfnk674/VGLWpQPm4VI/AAAAAAAAAB0/SrfL0fE9DnE/w500-no/OI000020_2.jpg"),
				SecondaryImageUrl = new Uri("https://lh3.googleusercontent.com/-6_Ioko2ysgU/VHva2PjmRCI/AAAAAAAAAGM/cHjJC7ney4Q/w500-no/PC190054.JPG"),
				Location = new LatLng(-33.843333, 151.241111),
				City = citySydney
			});
			attractions.Add(citySydney, attaractions);
		}

		/// <summary>
		/// Creates a list of geofences based on the city locations
		/// </summary>
		/// <returns>The geofence list.</returns>
		public static List<IGeofence> GetGeofenceList()
		{
			var geofenceList = new List<IGeofence>();
			foreach (string city in CityLocations.Keys)
			{
				LatLng cityLatLng = CityLocations[city];
				geofenceList.Add(new GeofenceBuilder()
				                 .SetCircularRegion(cityLatLng.Latitude, cityLatLng.Longitude, triggerRadius)
								.SetRequestId(city)
								.SetTransitionTypes(triggerTransition)
								.SetExpirationDuration(expirationDuration)
								.Build());
			}
			return geofenceList;
		}

		public static string GetClosestCity(LatLng curLatLng)
		{
			if (curLatLng == null)
			{
				// If location is unknown return test city so some data is shown
				return TestCity;
			}

			double minDistance = 0;
			String closestCity = null;
			foreach (var item in CityLocations)
			{
				//TODO:
				double distance = Utils.ComputeDistanceBetween(curLatLng, item.Value);
				if (minDistance == 0 || distance < minDistance)
				{
					minDistance = distance;
					closestCity = item.Key;
				}
			}
			return closestCity;
		}
	}
}

