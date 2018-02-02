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
	internal class DistanceMatrixResponse
	{
#pragma warning disable 0649
		public DistanceMatrixStatusCode Status;
		public string Error_Message;
		public IList<Row> Rows;
#pragma warning restore 0649

		internal class Row
		{
#pragma warning disable 0649
			public IList<Element> Elements;
#pragma warning restore 0649

			internal class Element
			{
#pragma warning disable 0649
				public ElementStatus Status;
				public Tuple Distance;
				public Tuple Duration;
#pragma warning restore 0649

				internal enum ElementStatus
				{
					/// <summary>
					/// Indicates the response contains a valid result.
					/// </summary>
					OK,
					/// <summary>
					/// Indicates that the origin and/or destination of this pairing could not be geocoded.
					/// </summary>
					NOT_FOUND,
					/// <summary>
					/// Indicates no route could be found between the origin and destination.
					/// </summary>
					ZERO_RESULTS,
					/// <summary>
					/// Indicates the requested route is too long and cannot be processed.
					/// </summary>
					MAX_ROUTE_LENGTH_EXCEEDED
				}

				internal class Tuple
				{
#pragma warning disable 0649
					public string Text;
					public int Value;
#pragma warning restore 0649
				}
			}
		}
	}
}
