using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// This script contains functions that let you manipulate media from file.
/// Created by: Adam Chandler
/// </summary>
namespace ACDev.Media
{
    public static class MediaManagement
    {
        //TODO find a way to make this a coroutine, so that loading doesn't hold up the thread
        public static Texture2D GetTextureFromFile(string fileFullPath)
        {
            Texture2D texture = null;      // create our texture and initialize null for debugging
            byte[] fileData;       // texture data
            // if a texture exists at the following location
            if (File.Exists(fileFullPath))
            {
                // make sure it's not a junk file, or a .meta data file
                if (!fileFullPath.EndsWith(".meta") && !string.IsNullOrEmpty(fileFullPath))
                {

                    fileData = File.ReadAllBytes(fileFullPath);   // read in the texture data
                    texture = new Texture2D(2, 2, TextureFormat.BGRA32, false);     // create a new Texture2D
                    texture.LoadImage(fileData);      // auto-resize texture dimensions
                }
            }
            // search the path for a file of the given name
            return texture;
        }
    }
}

