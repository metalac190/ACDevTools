using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// This class allows you to create a new copy of a class in memory. This is important if you
/// need a separate instance of a class, but don't want it to reference the original. Basically,
/// if you need to treat a class as a struct.
/// Created by: Adam Chandler
/// </summary>
public static class ClassCopy
{
    // Perform a deep clone on an object, creating new space in memory
    public static T DeepClone<T>(T obj)
    {
        using (var memoryStream = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, obj);
            memoryStream.Position = 0;
            // return the object
            return (T)formatter.Deserialize(memoryStream);
        }
    }
}

