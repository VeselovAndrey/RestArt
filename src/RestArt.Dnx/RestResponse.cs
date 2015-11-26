// -----------------------------------------------------------------------
// <copyright file="RestResponse.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
 
namespace RestArt
{
    using System.Net;

    public class RestResponse<T>
        where T : class
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Raw { get; set; }

        public T Value { get; set; }
    }
}
