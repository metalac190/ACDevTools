using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// This class contains static functions that help with file management. 
/// Most are modified version of System.IO functions.
/// Created by: Adam Chandler
/// </summary>

public static class FileManagement
{
    #region File Copying
    /// <summary>
    /// Copies files from one directory to the next
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <param name="targetFilePath"></param>
    /// <param name="overWriteExisting"></param>
    public static void CopyFile(string sourceFilePath, string targetFilePath, bool overWriteExisting)
    {
        File.Copy(sourceFilePath, targetFilePath, overWriteExisting);
    }
    /// <summary>
    /// Copies a file from the filepath to the given directory. Different from CopyFile, because
    /// this version only needs a destination directory, not a destination file path
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <param name="targetDirectory"></param>
    /// <param name="overWriteExisting"></param>
    public static void CopyFileToDirectory(string sourceFilePath, string targetDirectory, bool overWriteExisting)
    {
        // get the directory from the target path (without file name)
        string sourceDirectory = Path.GetDirectoryName(sourceFilePath);
        string sourceFileName = Path.GetFileName(sourceFilePath);
        // if the target path doesn't exist, create folders to ensure path structure
        if (!Directory.Exists(targetDirectory))
        {
            Directory.CreateDirectory(targetDirectory);
        }
        // copy to new location; overwrite existing file if it already exists
        File.Copy(sourceFilePath, Path.Combine(targetDirectory, sourceFileName), overWriteExisting);
    }
    #endregion

    #region File Retrieval
    /// <summary>
    /// Returns all folder directory paths inside of the given search path. Mostly
    /// just a wrapper class for the Directory.GetDirectories() call cause I 
    /// forget that it exists.
    /// </summary>
    /// <param name="targetDirectory"></param>
    /// <returns></returns>
    public static string[] GetSubDirectories(string targetDirectory)
    {
        // get all subdirectories from the desired search path
        string[] subDirectoryEntries = Directory.GetDirectories(targetDirectory);

        return subDirectoryEntries;
    }
    /// <summary>
    /// Returns the first file name of the requested extension it finds in the file path.
    /// </summary>
    /// <param name="targetDirectory"></param>
    /// <param name="extension"></param>
    /// <returns></returns>
    public static string GetFileName(string targetDirectory, string extension)
    {
        EnsureExtensionHasDot(extension);

        // store all located json files in the search path
        string[] fileNames = Directory.GetFiles(targetDirectory, "*" + extension);
        // validate the file
        if (string.IsNullOrEmpty(fileNames[0]))
        {
            Debug.LogWarning("File data is empty");
        }
        // ignore multiples, only return the first one we've validated
        return fileNames[0];
    }

    /// <summary>
    /// returns a string list of filenames of usable files with the given extension.
    /// NOTE: make sure to include "." in front of the extension
    /// </summary>
    /// <param name="targetDirectory"></param>
    /// <returns></returns>
    public static List<string> GetFileNames(string targetDirectory, string extension)
    {
        EnsureExtensionHasDot(extension);

        string[] targetFileNames = Directory.GetFiles(targetDirectory, "*" + extension);
        // validate our files. we only care about files with extenion, and that aren't junk data
        List<string> usableFileNames = new List<string>();
        foreach (string fileName in targetFileNames)
        {
            if (string.IsNullOrEmpty(fileName) == false)
            {
                usableFileNames.Add(fileName);
            }
        }

        return usableFileNames;
    }

    /// <summary>
    /// returns a string list of filenames of usable files with the given extensions.
    /// </summary>
    /// <param name="targetDirectory"></param>
    /// <returns></returns>
    public static List<string> GetFileNames(string targetDirectory, string[] extensions)
    {
        // validate extensions
        List<string> validFileNames = new List<string>();
        foreach (string extension in extensions)
        {
            EnsureExtensionHasDot(extension);
            string[] targetFileNames = Directory.GetFiles(targetDirectory, "*" + extension);
            // validate and update our validFilenames
            foreach (string fileName in targetFileNames)
            {
                validFileNames.Add(fileName);
            }
        }

        return validFileNames;
    }
    #endregion

    #region Name Manipulation
    /// <summary>
    /// Start counting characters from the beginning, trim off the excess from the end.
    /// </summary>
    /// <param name="stringToTrim"></param>
    /// <param name="characterLimit"></param>
    /// <param name="returnWithElipses"></param>
    /// <returns></returns>
    public static string TrimOffEnd(string stringToTrim, int characterLimit, bool returnWithElipses)
    {
        // if our we haven't passed the trim threshold, just return what we have
        if (stringToTrim.Length < characterLimit)
            return stringToTrim;

        // count from the end, and work towards the end to make sure we don't go over our limit
        string shortenedString = stringToTrim.Substring(0, characterLimit);
        // account for elipses
        if (returnWithElipses)
        {
            shortenedString = shortenedString + "...";
        }

        return shortenedString;
    }

    /// <summary>
    /// Start counting characters from the end, trim off the excess from the beginning. 
    /// </summary>
    /// <param name="stringToTrim"></param>
    /// <param name="characterLimit"></param>
    /// <returns></returns>
    public static string TrimOffBeginning(string stringToTrim, int characterLimit, bool returnWithElipses)
    {
        // if our we haven't passed the trim threshold, just return what we have
        if (stringToTrim.Length < characterLimit)
            return stringToTrim;

        // count from the end working backwards, and work towards the end to make sure we don't go over our limit
        string shortenedString = stringToTrim.Substring(stringToTrim.Length - characterLimit, characterLimit);
        // account for elpises
        if (returnWithElipses)
        {
            shortenedString.Insert(0, "...");
        }

        return shortenedString;
    }
    //TODO add functionality to increment filename if file already exists
    /// <summary>
    /// Rename the file. Must include the new name and extension separately
    /// </summary>
    /// <param name="_fileNameCurrent"></param>
    /// <param name="fileNameNew"></param>
    /// <param name="targetFilePath"></param>
    public static void RenameFile(string targetFilePath, string fileNameNew)
    {
        // to create a new file path, add the new name to just the directory path of original file
        string targetDirectory = Path.GetDirectoryName(targetFilePath);
        string newFilePath = Path.Combine(targetDirectory, fileNameNew);
        // if the directory exists, make our file
        if (Directory.Exists(targetDirectory))
        {
            // if the file already exists, don't allow the rename
            if (File.Exists(newFilePath))
            {
                Debug.LogWarning("Cannot rename, already a file with that name");
            }
            else
            {
                // moving a file is just a shortcut to rename. Old -> New
                File.Move(targetFilePath, newFilePath);
            }
        }
    }

    /// <summary>
    /// Wrapper class that allows us to easily extract a file name from a path
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="includeExtension"></param>
    /// <returns></returns>
    public static string ExtractFileNameFromPath(string filePath, bool includeExtension)
    {
        if (includeExtension)
        {
            return Path.GetFileName(filePath);
        }
        else
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }
    }

    /// <summary>
    /// Extracts and returns the given filePath's extension.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string ExtractFileExtension(string filePath)
    {
        return Path.GetExtension(filePath);
    }

    /// <summary>
    /// Give it a string, and it returns it with a '.' at the front, if it 
    /// doesn't already have one
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    public static string EnsureExtensionHasDot(string extension)
    {
        if (extension[0] != '.')
        {
            extension.Insert(0, ".");
        }
        return extension;
    }
    #endregion

    #region File Deletion
    /// <summary>
    /// Deletes the file if it exists. NOTE: you need the
    /// full name with the '.extension' at the end
    /// </summary>
    /// <param name="filePath"></param>
    public static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        else
        {
            Debug.LogError("Cannot Delete, file " + filePath + " - Does not exist!");
        }
    }

    /// <summary>
    /// Deletes all files in directories in the specified target directory
    /// </summary>
    /// <param name="directoryPath"></param>
    public static void DeleteAllFilesInDirectory(string targetDirectory)
    {
        // if the directory is invalid, return so as to minimize risk of deleting uninentionally
        if (!Directory.Exists(targetDirectory))
            return;

        DirectoryInfo directoryToEmpty = new DirectoryInfo(targetDirectory);
        // delete all files
        foreach (FileInfo file in directoryToEmpty.GetFiles())
        {
            file.Delete();
        }
        // recursive -> delete all sub directories as well
        foreach (DirectoryInfo directory in directoryToEmpty.GetDirectories())
        {
            directory.Delete(true);
        }
    }
    #endregion
}

