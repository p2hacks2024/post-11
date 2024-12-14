using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingTable : MonoBehaviour
{
    Ranking ranking;
    [SerializeField] TextMeshProUGUI[] rankTexts = new TextMeshProUGUI[5]; 
    // Start is called before the first frame update
    void Start()
    {
        ranking = GameObject.Find("Manager").GetComponent<Ranking>();

        /*for (int i = 0; i < rankTexts.Length; i++) {  
            rankTexts[i].text = data.rank[i].ToString("D8");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
