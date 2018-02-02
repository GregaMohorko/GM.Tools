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
using System.Collections.Specialized;
using System.Globalization;
using System.Text;

namespace GM.Tools.Google.API.Maps.DistanceMatrix
{
	/// <summary>
	/// Provides travel distance and time for a matrix of origins and destinations.
	/// <para>
	/// Returns information based on the recommended route between start and end points, as calculated by the Google Maps API, and consists of rows containing duration and distance values for each pair.
	/// </para>
	/// </summary>
	public class DistanceMatrixClient
	{
		// Documentation: https://developers.google.com/maps/documentation/distance-matrix/intro

		private const string URL = MapsClient.URL + "distancematrix/json";

		private readonly string apiKey;

		internal DistanceMatrixClient(string apiKey)
		{
			this.apiKey = apiKey;
		}

		/// <param name="start">The starting point for calculating travel distance and time.</param>
		/// <param name="end">Location to use as the finishing point for calculating.</param>
		/// <param name="mode">Mode of transport to use when calculating distance.</param>
		/// <param name="avoid">Restriction to the route.</param>
		public DistanceMatrixResult Get(Address start, Address end, TransitMode mode = TransitMode.driving, Restriction avoid = Restriction.NONE)
		{
			List<string> components = new List<string>();
			if(!string.IsNullOrWhiteSpace(start.StreetAddress))
				components.Add(start.StreetAddress);
			if(!string.IsNullOrWhiteSpace(start.PostCode)) {
				string postAndcity = start.PostCode;
				if(!string.IsNullOrWhiteSpace(start.City))
					postAndcity += $" {start.City}";
				components.Add(postAndcity);
			} else if(!string.IsNullOrWhiteSpace(start.City))
				components.Add(start.City);
			if(!string.IsNullOrWhiteSpace(start.Country))
				components.Add(start.Country);
			string startAddress = string.Join(",", components);

			components = new List<string>();
			if(!string.IsNullOrWhiteSpace(end.StreetAddress))
				components.Add(end.StreetAddress);
			if(!string.IsNullOrWhiteSpace(end.PostCode)) {
				string postAndcity = end.PostCode;
				if(!string.IsNullOrWhiteSpace(end.City))
					postAndcity += $" {end.City}";
				components.Add(postAndcity);
			} else if(!string.IsNullOrWhiteSpace(end.City))
				components.Add(end.City);
			if(!string.IsNullOrWhiteSpace(end.Country))
				components.Add(end.Country);
			string endAddress = string.Join(",", components);

			return Get(startAddress, endAddress, mode, avoid);
		}

		/// <param name="startAddress">The starting point for calculating travel distance and time.</param>
		/// <param name="endAddress">Location to use as the finishing point for calculating.</param>
		/// <param name="mode">Mode of transport to use when calculating distance.</param>
		/// <param name="avoid">Restriction to the route.</param>
		public DistanceMatrixResult Get(string startAddress,string endAddress,TransitMode mode=TransitMode.driving,Restriction avoid=Restriction.NONE)
		{
			return GetImpl(startAddress, endAddress, mode,avoid);
		}

		/// <param name="start">The starting point for calculating travel distance and time.</param>
		/// <param name="end">Location to use as the finishing point for calculating.</param>
		/// <param name="mode">Mode of transport to use when calculating distance.</param>
		/// <param name="avoid">Restriction to the route.</param>
		public DistanceMatrixResult Get(LatLng start,LatLng end, TransitMode mode = TransitMode.driving, Restriction avoid = Restriction.NONE)
		{
			string origin = $"{start.Latitude.ToString(CultureInfo.InvariantCulture)},{start.Longitude.ToString(CultureInfo.InvariantCulture)}";
			string destination = $"{end.Latitude.ToString(CultureInfo.InvariantCulture)},{end.Longitude.ToString(CultureInfo.InvariantCulture)}";

			return GetImpl(origin, destination, mode,avoid);
		}

		private DistanceMatrixResult GetImpl(string origin,string destination,TransitMode mode,Restriction avoid)
		{
			var values = new NameValueCollection
			{
				{ "origins", origin },
				{ "destinations", destination },
				{ "mode", mode.ToString() },
				{ "units", "metric" },
				{ "key", apiKey }
			};
			if(avoid != Restriction.NONE) {
				values.Add("avoid", avoid.ToString());
			}
			
			DistanceMatrixResponse response = GoogleAPIHelper.GetResponse<DistanceMatrixResponse>(URL, values);

			var result = new DistanceMatrixResult
			{
				Status = response.Status
			};

			if(result.Status != DistanceMatrixStatusCode.OK) {
				result.ErrorMessage = response.Error_Message;
				return result;
			}

			int meters = response.Rows[0].Elements[0].Distance.Value;
			int seconds = response.Rows[0].Elements[0].Duration.Value;

			result.Distance = meters;
			result.Duration = TimeSpan.FromSeconds(seconds);
			
			return result;
		}
	}
}
