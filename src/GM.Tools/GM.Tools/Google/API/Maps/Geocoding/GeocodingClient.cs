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
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GM.Tools.Google.API.Maps.Geocoding
{
	/// <summary>
	/// Provides geocoding and reverse geocoding of addresses.
	/// <para>
	///	Geocoding is the process of converting addresses (like a street address) into geographic coordinates (like latitude and longitude), which you can use to place markers on a map, or position the map.
	/// </para>
	/// <para>
	/// Reverse geocoding is the process of converting geographic coordinates into a human-readable address.
	/// </para>
	/// </summary>
	public class GeocodingClient
	{
		// Documentation: https://developers.google.com/maps/documentation/geocoding/intro

		private const string URL = MapsClient.URL+"geocode/json";

		private readonly string apiKey;

		internal GeocodingClient(string apiKey)
		{
			this.apiKey = apiKey;
		}

		/// <summary>
		/// Gets the geocoding information for the specified address.
		/// </summary>
		/// <param name="address">The address.</param>
		/// <param name="ct"></param>
		/// <param name="httpClient"></param>
		public async Task<GeocodingResult<LatLng>> GetLatLng(string address, CancellationToken ct, HttpClient httpClient = null)
		{
			return await GetLatLng(new Address(address), ct, httpClient);
		}

		/// <summary>
		/// Gets the geocoding information for the specified address.
		/// </summary>
		/// <param name="address">The address.</param>
		/// <param name="ct"></param>
		/// <param name="httpClient"></param>
		public async Task<GeocodingResult<LatLng>> GetLatLng(Address address, CancellationToken ct, HttpClient httpClient = null)
		{
			var values = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("address", address.StreetAddress),
				new KeyValuePair<string, string>("key", apiKey)
			};
			// components
			{
				var components = new List<Tuple<string,string>>();
				if(address.City != null)
					components.Add(Tuple.Create("administrative_area",address.City));
				if(address.PostCode != null)
					components.Add(Tuple.Create("postal_code", address.PostCode));
				if(address.Country != null)
					components.Add(Tuple.Create("country", address.Country));

				if(components.Any()) {
					values.Add(new KeyValuePair<string, string>("components", string.Join("|", components.Select(t => $"{t.Item1}:{t.Item2}"))));
				}
			}

			GeocodingResponse response = await GoogleAPIHelper.GetResponse<GeocodingResponse>(URL, values, ct, httpClient);

			var result = new GeocodingResult<LatLng>
			{
				Status = response.Status
			};

			if(result.Status != GeocodingStatusCode.OK) {
				result.ErrorMessage = response.Error_Message;
				return result;
			}

			var latLng = new LatLng
			{
				Latitude = response.Results[0].Geometry.Location.Lat,
				Longitude = response.Results[0].Geometry.Location.Lng
			};
			result.Value = latLng;

			return result;
		}

		/// <summary>
		/// Gets the address for the specified latitude and longitude.
		/// </summary>
		/// <param name="latitude">The latitude.</param>
		/// <param name="longitude">The longitude.</param>
		/// <param name="ct"></param>
		/// <param name="httpClient"></param>
		public async Task<GeocodingResult<Address>> GetAddress(long latitude,long longitude, CancellationToken ct, HttpClient httpClient = null)
		{
			return await GetAddress(new LatLng(latitude, longitude), ct, httpClient);
		}

		/// <summary>
		/// Gets the address for the specified latitude-longitude pair.
		/// </summary>
		/// <param name="latLng">The latitude-longitude pair.</param>
		/// <param name="ct"></param>
		/// <param name="httpClient"></param>
		public async Task<GeocodingResult<Address>> GetAddress(LatLng latLng, CancellationToken ct, HttpClient httpClient = null)
		{
			var values = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("latlng", $"{latLng.Latitude.ToString(CultureInfo.InvariantCulture)},{latLng.Longitude.ToString(CultureInfo.InvariantCulture)}"),
				new KeyValuePair<string, string>("key", apiKey)
			};

			GeocodingResponse response = await GoogleAPIHelper.GetResponse<GeocodingResponse>(URL, values, ct, httpClient);

			var result = new GeocodingResult<Address>
			{
				Status = response.Status
			};

			if(result.Status != GeocodingStatusCode.OK) {
				result.ErrorMessage = response.Error_Message;
				return result;
			}

			GeocodingResponse.Result result1=response.Results[0];
			var address = new Address(result1.Formatted_Address);
			result.Value = address;

			return result;
		}
	}
}
