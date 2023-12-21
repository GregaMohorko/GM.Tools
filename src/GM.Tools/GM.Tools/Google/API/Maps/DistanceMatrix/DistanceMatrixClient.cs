/*
MIT License

Copyright (c) 2023 Gregor Mohorko

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
Author: Gregor Mohorko
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
		/// <param name="ct"></param>
		/// <param name="mode">Mode of transport to use when calculating distance.</param>
		/// <param name="avoid">Restriction to the route.</param>
		/// <param name="httpClient"></param>
		public async Task<DistanceMatrixResult> Get(Address start, Address end, CancellationToken ct, TransitMode mode = TransitMode.driving, Restriction avoid = Restriction.NONE, HttpClient httpClient = null)
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

			return await Get(startAddress, endAddress, ct, mode, avoid, httpClient);
		}

		/// <param name="startAddress">The starting point for calculating travel distance and time.</param>
		/// <param name="endAddress">Location to use as the finishing point for calculating.</param>
		/// <param name="ct"></param>
		/// <param name="mode">Mode of transport to use when calculating distance.</param>
		/// <param name="avoid">Restriction to the route.</param>
		/// <param name="httpClient"></param>
		public async Task<DistanceMatrixResult> Get(string startAddress,string endAddress, CancellationToken ct, TransitMode mode=TransitMode.driving,Restriction avoid=Restriction.NONE, HttpClient httpClient = null)
		{
			return await GetImpl(startAddress, endAddress, mode,avoid, ct, httpClient);
		}

		/// <param name="start">The starting point for calculating travel distance and time.</param>
		/// <param name="end">Location to use as the finishing point for calculating.</param>
		/// <param name="ct"></param>
		/// <param name="mode">Mode of transport to use when calculating distance.</param>
		/// <param name="avoid">Restriction to the route.</param>
		/// <param name="httpClient"></param>
		public async Task<DistanceMatrixResult> Get(LatLng start,LatLng end, CancellationToken ct, TransitMode mode = TransitMode.driving, Restriction avoid = Restriction.NONE, HttpClient httpClient = null)
		{
			string origin = $"{start.Latitude.ToString(CultureInfo.InvariantCulture)},{start.Longitude.ToString(CultureInfo.InvariantCulture)}";
			string destination = $"{end.Latitude.ToString(CultureInfo.InvariantCulture)},{end.Longitude.ToString(CultureInfo.InvariantCulture)}";

			return await GetImpl(origin, destination, mode,avoid, ct, httpClient);
		}

		private async Task<DistanceMatrixResult> GetImpl(string origin,string destination,TransitMode mode,Restriction avoid, CancellationToken ct, HttpClient httpClient = null)
		{
			var values = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("origins", origin),
				new KeyValuePair<string, string>("destinations", destination),
				new KeyValuePair<string, string>("mode", mode.ToString()),
				new KeyValuePair<string, string>("units", "metric"),
				new KeyValuePair<string, string>("key", apiKey),
			};
			if(avoid != Restriction.NONE) {
				values.Add(new KeyValuePair<string, string>("avoid", avoid.ToString()));
			}
			
			DistanceMatrixResponse response = await GoogleAPIHelper.GetResponse<DistanceMatrixResponse>(URL, values, ct, httpClient);

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
