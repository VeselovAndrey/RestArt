// -----------------------------------------------------------------------
// <copyright file="InvalidJsonFormat.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt
{
    public class InvalidJsonFormat
    {
        public string ErrorMessage { get; }

        public InvalidJsonFormat(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}