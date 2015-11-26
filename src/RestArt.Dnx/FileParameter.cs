// -----------------------------------------------------------------------
// <copyright file="FileParameter.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
 
namespace RestArt
{
    using System.IO;

    public class FileParameter
    {
        public string Name { get; private set; }

        public string ContentType { get; private set; }

        public byte[] Content { get; private set; }

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
