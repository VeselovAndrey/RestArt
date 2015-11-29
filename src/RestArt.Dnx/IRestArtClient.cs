// -----------------------------------------------------------------------
// <copyright file="IRestArtClient.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt
{
    using System;
    using System.Threading.Tasks;
    using RestArt.Requests;

    public interface IRestArtClient
    {
        void AddOrUpdatePersistentHeader(string key, string value);

        void RemovePersistentHeader(string key);

        void ClearPersistentHeaders();


        void SetErrorType(Type errorType);


        Task<RestResponse<TResponse>> ExecuteAsync<TResponse>(IRestRequest request)
            where TResponse : class;

        Task<RestResponse<TResponse>> ExecuteAsync<TResponse>(IRestRequest request, Type errorType)
            where TResponse : class;
    }
}