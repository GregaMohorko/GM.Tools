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

namespace GM.Tools.Google.API.Maps.DistanceMatrix
{
	/// <summary>
	/// Distance matrix status code.
	/// </summary>
	public enum DistanceMatrixStatusCode
	{
		/// <summary>
		/// Indicates a Distance Matrix request could not be processed due to a server error. The request may succeed if you try again.
		/// </summary>
		UNKNOWN_ERROR,
		/// <summary>
		/// Indicates the response contains a valid result.
		/// </summary>
		OK,
		/// <summary>
		/// Indicates that the provided request was invalid.
		/// </summary>
		INVALID_REQUEST,
		/// <summary>
		/// Indicates that the product of origins and destinations exceeds the per-query limit.
		/// </summary>
		MAX_ELEMENTS_EXCEEDED,
		/// <summary>
		/// Indicates the service has received too many requests from your application within the allowed time period.
		/// </summary>
		OVER_QUERY_LIMIT,
		/// <summary>
		/// Indicates that the service denied use of the Distance Matrix service by your application.
		/// </summary>
		REQUEST_DENIED
	}
}
