using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Ranking : MonoBehaviour
{
    const int   rankCnt = SaveData.rankCnt;

    public TextMeshProUGUI[] rankTexts = new TextMeshProUGUI[rankCnt];
    SaveData    data;

    //-------------------------------------------------------------------
    public void DataLoad()
    {
        SceneManager.LoadScene("TitleScene");
        data = GetComponent<DataManager>().data;
    }

    public void DispRank()
    {
        for (int i = 0; i < rankCnt; i++) {
            Transform rankChilds = GameObject.Find("Ranking_text").transform.GetChild(i);
            rankTexts[i] = rankChilds.GetComponent<TextMeshProUGUI>();   
            rankTexts[i].text = data.rank[i].ToString("D8");
        }
    }

    public void SetRank(int score)
    {
        for (int i = 0; i < rankCnt; i++) {
            if (score > data.rank[i]) {
                var rep = data.rank[i];
                data.rank[i] = score;
                score = rep;
            }
        }
    }
}
