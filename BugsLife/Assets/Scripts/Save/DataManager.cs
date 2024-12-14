using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour 
{
    [HideInInspector] public SaveData data;
    string filepath;
    string fileName = "Data.json";
    [SerializeField] Ranking ranking;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
#if UNITY_EDITOR
        filepath = Application.dataPath + "/" + fileName;
#elif UNITY_IOS || UNITY_ANDROID
        filepath = Application.persistentDataPath+ "/" + fileName;
#endif   

        if (!File.Exists(filepath)) {
            Save(data);
        }

        data = Load(filepath);   
        
        ranking.DataLoad();       
    }

    void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        StreamWriter wr = new StreamWriter(filepath, false);
        wr.WriteLine(json);
        wr.Close();
    }

    SaveData Load(string path)
    {
        StreamReader rd = new StreamReader(path);
        string json = rd.ReadToEnd();
        rd.Close();
                                                                
        return JsonUtility.FromJson<SaveData>(json);
    }

    void OnDestroy()
    {
        Save(data);
    }
}