using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines media types. Useful for making media-oriented code
/// more readable
/// Created by: Adam Chandler
/// </summary>
namespace ACDev.Media
{
    public enum MediaType { Image, Video, Audio, Unsupported, None }

    public class MediaFile
    {
        public string FileName { get; private set; } = "";
        public string FullFilePath { get; private set; } = "";
        public MediaType MediaType { get; private set; } = MediaType.Unsupported;

        public MediaFile(string fullFilePath, string fileName, MediaType mediaType)
        {
            this.FullFilePath = fullFilePath;
            this.FileName = fileName;
            this.MediaType = mediaType;
        }
    }
}

