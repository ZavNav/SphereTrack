using System.IO;
using UnityEngine;

public static class SavePathCreator
{
     public static string GetPath()
     {
#if UNITY_EDITOR
         return Path.Combine(Application.dataPath, "save.json");
#else
         return Path.Combine(Application.persistentDataPath, "save.json");
#endif
     }
}