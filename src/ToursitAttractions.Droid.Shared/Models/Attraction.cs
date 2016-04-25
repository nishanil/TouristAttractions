using System;
using System.Collections;
using Android.Gms.Maps.Model;
using Android.Graphics;

namespace TouristAttractions
{
	public class Attraction : IComparable
	{
		private class AttractionComparer : IComparer
		{
			int IComparer.Compare(object a, object b)
			{
				Attraction a1 = (Attraction)a;
				Attraction a2 = (Attraction)b;
				if (a1.DistanceInMi > a2.DistanceInMi)
					return 1;
				if (a1.DistanceInMi < a2.DistanceInMi)
					return -1;
				else
					return 0;
			}
		}

		private string name;
		private string description;
		private string longDescription;
		private Uri imageUrl;
		private Uri secondaryImageUrl;
		private LatLng location;
		private String city;

		private Bitmap image;
		private Bitmap secondaryImage;
		private String distance;

		private double distanceInMi;

		public double DistanceInMi
		{
			get { return distanceInMi; }
			set { distanceInMi = value; }
		}

		public string Name
		{
			get
			{
				return name;
			}

			set
			{
				name = value;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}

			set
			{
				description = value;
			}
		}

		public string LongDescription
		{
			get
			{
				return longDescription;
			}

			set
			{
				longDescription = value;
			}
		}

		public Uri ImageUrl
		{
			get
			{
				return imageUrl;
			}

			set
			{
				imageUrl = value;
			}
		}

		public Uri SecondaryImageUrl
		{
			get
			{
				return secondaryImageUrl;
			}

			set
			{
				secondaryImageUrl = value;
			}
		}

		public LatLng Location
		{
			get
			{
				return location;
			}

			set
			{
				location = value;
			}
		}

		public string City
		{
			get
			{
				return city;
			}

			set
			{
				city = value;
			}
		}

		public Bitmap Image
		{
			get
			{
				return image;
			}

			set
			{
				image = value;
			}
		}

		public Bitmap SecondaryImage
		{
			get
			{
				return secondaryImage;
			}

			set
			{
				secondaryImage = value;
			}
		}

		public string Distance
		{
			get
			{
				return distance;
			}

			set
			{
				distance = value;
			}
		}



		public static IComparer SortByDistance()
		{
			return (IComparer)new AttractionComparer();
		}

		int IComparable.CompareTo(object obj)
		{
			Attraction a = (Attraction)obj;
			return String.Compare(this.Name, a.Name);
		}

	}

}

