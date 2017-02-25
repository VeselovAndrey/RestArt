// -----------------------------------------------------------------------
// <copyright file="RestRequest.cs">
// Copyright (c) 2016-2017 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Core.Requests
{
    using System.Collections.Generic;
    using System.Reflection;
    using RestArt.Core.Convertors;

    public class RestRequest : IRestRequest
    {
        private static readonly ObjectConvertor _objectConvertor = new ObjectConvertor();
        private static readonly TypeInfo _headersDictionaryTypeInfo = typeof (IDictionary<string, string>).GetTypeInfo();
        private static readonly TypeInfo _parametersDictionaryTypeInfo = typeof(IDictionary<string, object>).GetTypeInfo();

        public HttpVerb Verb { get; }

        public string Command { get; }

        public IDictionary<string, string> Headers { get; }

        public IDictionary<string, object> Parameters { get; }

        public RestRequest(HttpVerb verb, string command, IDictionary<string, string> headers, IDictionary<string, object> parameters)
        {
            this.Verb = verb;
            this.Command = command;
            this.Headers = headers;
            this.Parameters = parameters;
        }

        public RestRequest(HttpVerb verb, string command, object headers, object parameters)
        {
            this.Verb = verb;
            this.Command = command;

            this.Headers = RestRequest._headersDictionaryTypeInfo.IsAssignableFrom(headers.GetType().GetTypeInfo()) ?
                (IDictionary<string, string>)headers :
                RestRequest._objectConvertor.ToStringDictionary(headers);

            this.Parameters = RestRequest._parametersDictionaryTypeInfo.IsAssignableFrom(parameters.GetType().GetTypeInfo()) ?
                (IDictionary<string, object>)parameters : 
                RestRequest._objectConvertor.ToObjectDictionary(parameters);
        }
    }
}