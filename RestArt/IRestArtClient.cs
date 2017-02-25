// -----------------------------------------------------------------------
// <copyright file="IRestArtClient.cs">
// Copyright (c) 2016-2017 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt
{
	using System;
	using System.Threading.Tasks;

	/// <summary>REST API client interface.</summary>
	public interface IRestArtClient
	{
		/// <summary>Adds persistent header to client if the key does not already exist,
		/// or updates persistent header if the key already exists.</summary>
		/// <param name="key">The key to be added or whose value should be updated.</param>
		/// <param name="value">The value to be added for an absent key.</param>
		void AddOrUpdatePersistentHeader(string key, string value);

		/// <summary>Removes persistent header that has the specified key from the client.</summary>
		/// <param name="key">The key of the header to remove and return.</param>
		void RemovePersistentHeader(string key);

		/// <summary>Removes all persistent headers from the client.</summary>
		void ClearPersistentHeaders();

		/// <summary>Sets the type of the error object.</summary>
		/// <param name="errorType">The type of the error object. This type should contains same properties as error response JSON.</param>
		void SetErrorType(Type errorType);

		/// <summary>Sends a requests to the server as an asynchronous operation.</summary>
		/// <typeparam name="TResponse">The type of the response object. This type should contains same properties as response JSON.</typeparam>
		/// <param name="request">The request to send.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="RestArtException{TErrorEntity}">The remote server returned an error. The type of the TErrorEntity should be defined with <see cref="IRestArtClient.SetErrorType(Type)" /> method call.</exception>
		/// <exception cref="RestArtException{InvalidJsonFormat}">Response json format doesn't match CatalogItem type (e.g. json object has extra fields).</exception>
		/// <exception cref="RestArtException{InvalidRestResponse}">Client failed to parse the response from the server (in most cases this exception will be thrown if the server didn't returned proper json object).</exception>
		Task<IRestResponse<TResponse>> ExecuteAsync<TResponse>(IRestRequest request)
			where TResponse : class;

		/// <summary>Sends a requests to the server as an asynchronous operation.</summary>
		/// <typeparam name="TResponse">The type of the response object. This type should contains same properties as response JSON.</typeparam>
		/// <param name="request">The request to send.</param>
		/// <param name="errorType">The type of the error object. This will override type that was specified with <see cref="IRestArtClient.SetErrorType(Type)" /> for this call only.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="RestArtException{errorType}">The remote server returned an error.</exception>
		/// <exception cref="RestArtException{InvalidJsonFormat}">Response json format doesn't match CatalogItem type (e.g. json object has extra fields).</exception>
		/// <exception cref="RestArtException{InvalidRestResponse}">Client failed to parse the response from the server (in most cases this exception will be thrown if the server didn't returned proper json object).</exception>
		Task<IRestResponse<TResponse>> ExecuteAsync<TResponse>(IRestRequest request, Type errorType)
			where TResponse : class;
	}
}