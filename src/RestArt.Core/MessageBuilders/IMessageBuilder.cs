// -----------------------------------------------------------------------
// <copyright file="IMessageBuilder.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Core.MessageBuilders
{
    using System.Collections.Generic;
    using System.Net.Http;

    internal interface IMessageBuilder
    {
        // Should return HttpRequestMessage or null if unable to build it 
        HttpRequestMessage Build(IRestRequest request, IDictionary<string, string> persistentHeaders);
    }
}