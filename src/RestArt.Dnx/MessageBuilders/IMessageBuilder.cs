// -----------------------------------------------------------------------
// <copyright file="IMessageBuilder.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
 
namespace RestArt.MessageBuilders
{
    using System.Collections.Generic;
    using System.Net.Http;
    using RestArt.Requests;

    internal interface IMessageBuilder
    {
        // Should return HttpRequestMessage or null if unable to build it 
        HttpRequestMessage Build(IRestRequest request, IDictionary<string, string> persistentHeaders);
    }
}