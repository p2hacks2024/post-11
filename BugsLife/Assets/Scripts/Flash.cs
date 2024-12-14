using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public int conbo = 0;
    Shutter shutter;
    Charge charge;
    GameManager gamemanager;
    float time = 0f;
    public int scoreAdd;

    void Start()
    {
        shutter = GameObject.Find("Main Camera").GetComponent<Shutter>();
        charge = GameObject.Find("FinalFlashButton").GetComponent<Charge>();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<Renderer>().sortingOrder = 1;
    }

    void Update()
    {
        if(!gamemanager.pause && !gamemanager.clear && !gamemanager.gameover){
            time += Time.deltaTime;
            if(time >= 3f) {
                shutter.conbo = 0;
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision otaku)
    {
        if(this.gameObject.name.Contains(otaku.gameObject.tag)){
            otaku.gameObject.GetComponent<Otaku>().Death();
            Destroy(otaku.gameObject);
            Destroy(gameObject);
            Debug.Log("倒せた");
            shutter.conbo++;
            scoreAdd = 1000*(int)Mathf.Pow(((shutter.conbo/10)+1),2);
            gamemanager.score += scoreAdd;
            charge.power++;
            charge.SE();
            gamemanager.StartCoroutine("ScorePlus", scoreAdd);
        }
        else if(otaku.gameObject.name.Equals("Capsule")){
            Debug.Log("プレイヤーです");
        }
        else if(otaku.gameObject.name.Contains("Otaku") && !this.gameObject.name.Contains(otaku.gameObject.tag)){
            Debug.Log("別のオタクです");
            shutter.conbo = 0;
            Destroy(gameObject);
            shutter.Miss();
        }
        else if(otaku.gameObject.tag.Equals("Flash")){
            Debug.Log("フラッシュです");
        }
        else {
            Debug.Log("倒せない");
        }
    }
}
