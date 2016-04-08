using System;
using Android.Gms.Maps.Model;
using Android.Graphics;

namespace ToursitAttractions
{
	public class Attraction
	{
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
	}
}

