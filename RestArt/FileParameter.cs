// -----------------------------------------------------------------------
// <copyright file="FileParameter.cs">
// Copyright (c) 2016-2017 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt
{
    using System.IO;
    using RestArt.Extensions;

    public class FileParameter
    {
        public string Name { get; }

        public string ContentType { get; }

        public byte[] Content { get;  }

        public FileParameter(string name, byte[] content)
        {
            this.Name = name;
            this.ContentType = Path.GetExtension(name).GetMimeType();
            this.Content = content;
        }

        public FileParameter(string name, string contentType, byte[] content)
        {
            this.Name = name;
            this.ContentType = contentType;
            this.Content = content;
        }
    }
}
