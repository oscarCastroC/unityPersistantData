using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class scorePlayer : MonoBehaviour
{
    public static scorePlayer Instance;
    public TextMeshProUGUI txtScore;
    public string currentName;
    public string namePlayerSc;
    public int scorePlayerSC;

    [System.Serializable]
    class SaveData
    {
        public string namePlayer;
        public int scorePlayer;
    }

    void Awake()
    {   // This code enables you to access the MainManager object from any other script.  

        if (Instance != null)
        {   // start of new code
            Destroy(gameObject);
            return;
        }   // end of new code

        // You can now call MainManager.Instance from any other script 
        Instance = this;
        // marks the MainManager GameObject attached to this script not to be destroyed when the scene changes
        DontDestroyOnLoad(gameObject);

        LoadPlayerData();
    }

    public string GetHightName()
    {
        return namePlayerSc;
    }

    public int GetHightScore()
    {
        return scorePlayerSC;
    }

    public void SaveplayeData(int scoreP)
    {
        SaveData data = new SaveData();
        data.namePlayer = currentName;
        data.scorePlayer = scoreP;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        
        LoadPlayerData();

    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            namePlayerSc = data.namePlayer;
            scorePlayerSC = data.scorePlayer;
            
        }
        setBestScore();
    }

    public void setBestScore()
    {
        txtScore = GameObject.Find("txt_bestScore").GetComponent<TextMeshProUGUI>();
        txtScore.text = "Best Score - " + namePlayerSc + ": " + scorePlayerSC;
    }

    public bool checkScore(int scoreIn)
    {
        bool isGreatherThan = true;
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if (scoreIn <= data.scorePlayer)
            {
                isGreatherThan = false;
            }
        }

        return isGreatherThan;
    }

    public void setName(string nameIn)
    {
        currentName = nameIn;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);  // main game
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit(); // original code to quit Unity player
        #endif

        if (checkScore(scorePlayerSC))
            scorePlayer.Instance.SaveplayeData(scorePlayerSC);

    }

}
