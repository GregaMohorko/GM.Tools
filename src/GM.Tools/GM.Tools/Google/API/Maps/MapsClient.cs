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
using System.Threading;
using GM.Tools.Google.API.Maps.DistanceMatrix;
using GM.Tools.Google.API.Maps.Geocoding;

namespace GM.Tools.Google.API.Maps
{
	/// <summary>
	/// A client for Google Maps.
	/// </summary>
	public class MapsClient
	{
		internal const string URL = "https://maps.googleapis.com/maps/api/";

		/// <summary>
		/// The geocoding client.
		/// </summary>
		public GeocodingClient Geocoding => _geocoding.Value;
		private readonly Lazy<GeocodingClient> _geocoding;

		/// <summary>
		/// The distance matrix client.
		/// </summary>
		public DistanceMatrixClient DistanceMatrix => _distanceMatrix.Value;
		private readonly Lazy<DistanceMatrixClient> _distanceMatrix;

		internal MapsClient(string apiKey)
		{
			_geocoding = new Lazy<GeocodingClient>(() => new GeocodingClient(apiKey), LazyThreadSafetyMode.ExecutionAndPublication);
			_distanceMatrix = new Lazy<DistanceMatrixClient>(() => new DistanceMatrixClient(apiKey), LazyThreadSafetyMode.ExecutionAndPublication);
		}
	}
}
