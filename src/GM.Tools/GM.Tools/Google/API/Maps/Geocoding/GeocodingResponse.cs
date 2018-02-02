/*
MIT License

Copyright (c) 2018 Grega Mohorko

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Project: GM.Tools
Created: 2018-2-1
Author: GregaMohorko
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Tools.Google.API.Maps.Geocoding
{
	internal class GeocodingResponse
	{
#pragma warning disable 0649
		public GeocodingStatusCode Status;
		public string Error_Message;
		public IList<Result> Results;
#pragma warning restore 0649

		internal class Result
		{
#pragma warning disable 0649
			public IList<AddressComponent> Address_Components;

			/// <summary>
			/// A string containing the human-readable address of this location.
			/// </summary>
			public string Formatted_Address;
			
			public GeometryType Geometry;
#pragma warning restore 0649

			internal class AddressComponent
			{
#pragma warning disable 0649
				/// <summary>
				/// The full text description or name of the address component as returned by the Geocoder.
				/// </summary>
				public string Long_Name;
				/// <summary>
				/// An array indicating the type of the address component.
				/// </summary>
				public IList<string> types;
#pragma warning restore 0649
			}

			internal class GeometryType
			{
#pragma warning disable 0649
				/// <summary>
				/// Contains the geocoded latitude,longitude value. For normal address lookups, this field is typically the most important.
				/// </summary>
				public LocationType Location;
#pragma warning restore 0649

				internal class LocationType
				{
#pragma warning disable 0649
					public double Lat;
					public double Lng;
#pragma warning restore 0649
				}
			}
		}
	}
}
