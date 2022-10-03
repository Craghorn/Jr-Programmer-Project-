using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static MainManager Instance;

    public Color TeamColor; // new variable color for forklifts

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor(); // Load color at start
    }

    [System.Serializable]
    class SaveData // new class for saving color
    {
        public Color TeamColor;
    }

    public void SaveColor() // new method for saving color
    {
        SaveData data = new SaveData(); // new object
        data.TeamColor = TeamColor; // write MainManager color in new object variable

        string json = JsonUtility.ToJson(data); // transform data object to JSON

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // save file (good link forlists the actual paths per platform https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html)
    }

    public void LoadColor() // new method for loading color
    {
        string path = Application.persistentDataPath + "/savefile.json"; // variable of path of file
        if (File.Exists(path)) // method Exists to check existence of file
        {
            string json = File.ReadAllText(path); // reading to string
            SaveData data = JsonUtility.FromJson<SaveData>(json);  // converted json to data object

            TeamColor = data.TeamColor; // MainManager TeamColor now is from saved data object color from json
        }
    }
}
