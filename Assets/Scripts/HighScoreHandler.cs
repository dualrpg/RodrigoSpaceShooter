using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreHandler : MonoBehaviour {
    List<HighScoreElement> highScoreList = new List<HighScoreElement> ();
    [SerializeField] int maxCount = 5;
    [SerializeField] string filename;

    public delegate void OnHighScoreListChanged (List<HighScoreElement> list);
    public static event OnHighScoreListChanged onHighScoreListChanged;

    private void Start () {
        LoadHighScores ();
    }

    private void LoadHighScores () {
        highScoreList = FileHandler.ReadListFromJSON<HighScoreElement> (filename);

        while (highScoreList.Count > maxCount) {
            highScoreList.RemoveAt (maxCount);
        }

        if(onHighScoreListChanged != null){
            onHighScoreListChanged.Invoke (highScoreList);
        }
    }

    private void SaveHighScores () {
        FileHandler.SaveToJSON<HighScoreElement> (highScoreList, filename);
    }

    public void AddHighScoreIfPossible (HighScoreElement element) {
        for(int i = 0; i < maxCount; i ++) {
            if(i >= highScoreList.Count || element.score > highScoreList[i].score) {
                highScoreList.Insert (i, element);
            }

            while (highScoreList.Count > maxCount) {
                highScoreList.RemoveAt (maxCount);
            }

            SaveHighScores ();

            if(onHighScoreListChanged != null){
                onHighScoreListChanged.Invoke (highScoreList);
            }

            break;
        }
    }
}
