﻿/*
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

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GM.Utility.Net;
using Newtonsoft.Json;

namespace GM.Tools.Google.API
{
    internal static class GoogleAPIHelper
    {
		public static async Task<T> GetResponse<T>(string url, IEnumerable<KeyValuePair<string, string>> values, CancellationToken ct, HttpClient httpClient = null)
		{
			string jsonResult;
			using(var webClient = new GMHttpClient(httpClient, disposeHttpClient: httpClient != null)) {
				jsonResult = await webClient.UploadValuesAsync(url, values, System.Net.Http.HttpMethod.Get, ct);
			}

			T response = JsonConvert.DeserializeObject<T>(jsonResult);

			return response;
		}
	}
}
