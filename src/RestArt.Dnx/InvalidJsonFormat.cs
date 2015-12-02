// -----------------------------------------------------------------------
// <copyright file="InvalidJsonFormat.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt
{
    /// <summary>Invalid JSON format error description.</summary>
    public class InvalidJsonFormat
    {
        /// <summary>The error description.</summary>
        public string ErrorMessage { get; }

        internal InvalidJsonFormat(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}