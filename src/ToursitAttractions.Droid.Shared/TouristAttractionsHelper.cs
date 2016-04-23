using System;
using System.Collections.Generic;
using Android.Gms.Location;
using Android.Gms.Maps.Model;
using ToursitAttractions.Droid.Shared;

namespace TouristAttractions
{
	public static class TouristAttractionsHelper
	{
		private static readonly string localCity = "Orlando";

		private static readonly string testCity = LocalCity;

		private static readonly float triggerRadius = 2000; // 2KM
		private static readonly int triggerTransition = Geofence.GeofenceTransitionEnter |
		                                                         Geofence.GeofenceTransitionExit;
		private static readonly long expirationDuration = Geofence.NeverExpire;

		private static readonly Dictionary<string, LatLng> cityLocations = new Dictionary<string, LatLng>();

		private static readonly Dictionary<string, List<Attraction>> attractions = new Dictionary<string, List<Attraction>>();

		public static string LocalCity
		{
			get
			{
				return localCity;
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
			cityLocations.Add(localCity, new LatLng(28.4810971, -81.5088354));

			var attaractions = new List<Attraction>();

			attaractions.Add(new Attraction()
			{
				Name = "Universal Orlando",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://evolve.xamarin.com/universal-logo.96c913e64fda4c4947f4695b9155643b.png"),
				SecondaryImageUrl = new Uri("https://evolve.xamarin.com/universal-logo.96c913e64fda4c4947f4695b9155643b.png"),
				Location = new LatLng(28.4743207, -81.470008),
				City = localCity
			});

			///**
			// * All photos used with permission under the Creative Commons Attribution-ShareAlike License.
			// */
			attaractions.Add(new Attraction()
			{
				Name = "Magic Kingdom",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://lh6.googleusercontent.com/-uss3kffjlik/AAAAAAAAAAI/AAAAAAAAAfA/Z6qmNHnIR-8/s0-c-k-no-ns/photo.jpg"),
				SecondaryImageUrl = new Uri("https://lh3.googleusercontent.com/-EFEw6s7mT6I/VGLkCH4Xt4I/AAAAAAAAADY/ZlznhaQvb8E/w600-no/DSC_2775.JPG"),
				Location = new LatLng(28.417663, -81.581212),
				City = localCity
			});

			attaractions.Add(new Attraction()
			{
				Name = "Epcot",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://lh6.googleusercontent.com/-dGaZnwHsWGg/AAAAAAAAAAI/AAAAAAAAAF8/KPj5loSe0u0/s0-c-k-no-ns/photo.jpg"),
				SecondaryImageUrl = new Uri("https://lh6.googleusercontent.com/-dGaZnwHsWGg/AAAAAAAAAAI/AAAAAAAAAF8/KPj5loSe0u0/s0-c-k-no-ns/photo.jpg"),
				Location = new LatLng(28.374694, -81.549404),
				City = localCity
			});

			attaractions.Add(new Attraction()
			{
				Name = "Islands of Adventure",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://www.universalorlando.com/Images/IOA_Header_tcm13-10617.jpg"),
				SecondaryImageUrl = new Uri("https://www.universalorlando.com/Images/IOA_Header_tcm13-10617.jpg"),
				Location = new LatLng(28.4711402, -81.4715651),
				City = localCity
			});

			attaractions.Add(new Attraction()
			{
				Name = "SeaWorld Orlando",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://lh4.googleusercontent.com/-wbNgVdUkBiE/VHe99hGVtNI/AAAAAAAAAFY/fAHfhchNLJw/w600-no/IMG_20141124_143747.jpg"),
				SecondaryImageUrl = new Uri("https://lh6.googleusercontent.com/-sjY_xlEOic4/VHe9-I4DD9I/AAAAAAAAAFI/Mt0VnjU7SxQ/w600-no/IMG_20141124_144008.jpg"),
				Location = new LatLng(28.4114555, -81.4617047),
				City = localCity
			});

			attaractions.Add(new Attraction()
			{
				Name = "Disney's Animal Kingdom",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti.",
				LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae bibendum justo, vitae cursus velit. Suspendisse potenti. Suspendisse scelerisque risus justo, non tincidunt nibh blandit et. Vivamus elit lacus, luctus nec erat in, pharetra semper turpis. Quisque viverra nulla ligula, non pulvinar ante dictum sit amet. Vestibulum aliquet tortor mauris, vel suscipit nisl malesuada eget. Aliquam maximus dictum euismod. Maecenas leo quam, volutpat id diam eget, placerat fringilla ipsum. Nam pretium vehicula augue quis euismod.\n\nNam sed blandit magna. Vestibulum a fermentum arcu. Vestibulum et ligula at nisi luctus facilisis. Proin fermentum enim a nibh commodo finibus. Suspendisse justo elit, vulputate ut ipsum at, pellentesque auctor massa. Praesent vestibulum erat interdum imperdiet dapibus. In hac habitasse platea dictumst. Proin varius orci vitae tempor vulputate.\n\nEtiam sed mollis orci. Integer et ex sed tortor scelerisque blandit semper id libero. Nulla facilisi. Pellentesque tempor magna eget massa ultrices, et efficitur lectus finibus.",
				ImageUrl = new Uri("https://lh4.googleusercontent.com/-xA_By7cT1_o/AAAAAAAAAAI/AAAAAAAAAH0/hnPERfqMVF8/s0-c-k-no-ns/photo.jpg"),
				SecondaryImageUrl = new Uri("https://lh4.googleusercontent.com/-xA_By7cT1_o/AAAAAAAAAAI/AAAAAAAAAH0/hnPERfqMVF8/s0-c-k-no-ns/photo.jpg"),
				Location = new LatLng(28.359719, -81.591313),
				City = localCity
			});
			attractions.Add(localCity, attaractions);
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

