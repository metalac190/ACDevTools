using UnityEngine;
using System.IO;

/// <summary>
/// This class is responsible for:
/// - Storing SavaData for the various game types
/// - Reading json from file
/// - Saving json to file
/// Created by: Adam Chandler
/// </summary>
namespace ACDev.Utility
{
    [System.Serializable]
    public class SaveData
    {
        [SerializeField] private string _fileName = "SaveData";
        public string FileName
        {
            get { return _fileName; }
            private set { _fileName = value; }
        }

        /// <summary>
        /// This function writes this class instance to file, in JSON format
        /// </summary>
        /// <param name="filePath"></param>
        public void WriteToFile(string filePath)
        {
            // write this class instance to file as json
            string dataAsJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(filePath, dataAsJson);
        }

        /// <summary>
        /// This function reads a json file at the given path, and returns it as a SavaData object
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static SaveData ReadFromFile(string filePath)
        {
            // validate
            if (!File.Exists(filePath))
            {
                Debug.LogError("Specified file could not be found, returning new object");
                // if we couldn't find it, return a new default value save object so that it will still run
                return new SaveData();
            }
            // we found our Save file, read it from json
            string dataAsJson = File.ReadAllText(filePath);
            // ensure it's not empty or filled with junk
            if (string.IsNullOrEmpty(dataAsJson))
            {
                Debug.LogError("Target file is empty or invalid. Returning default data");
                return new SaveData();
            }

            // we're good to go. Read json data into SavaData object and return it
            return JsonUtility.FromJson<SaveData>(dataAsJson);
        }
    }
}

