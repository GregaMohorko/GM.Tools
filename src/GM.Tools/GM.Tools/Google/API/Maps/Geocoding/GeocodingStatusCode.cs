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
	/// <summary>
	/// Geocoding status code.
	/// </summary>
	public enum GeocodingStatusCode
	{
		/// <summary>
		/// Indicates that the request could not be processed due to a server error. The request may succeed if you try again.
		/// </summary>
		UNKNOWN_ERROR,
		/// <summary>
		/// Indicates that no errors occurred; the address was successfully parsed and at least one geocode was returned.
		/// </summary>
		OK,
		/// <summary>
		/// Indicates that the geocode was successful but returned no results. This may occur if the geocoder was passed a non-existent address.
		/// </summary>
		ZERO_RESULTS,
		/// <summary>
		/// Indicates that you are over your quota.
		/// </summary>
		OVER_QUERY_LIMIT,
		/// <summary>
		/// Indicates that your request was denied.
		/// </summary>
		REQUEST_DENIED,
		/// <summary>
		/// Generally indicates that the query (address, components or latlng) is missing.
		/// </summary>
		INVALID_REQUEST
	}
}
