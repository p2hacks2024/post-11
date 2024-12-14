using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idol : MonoBehaviour
{
    [SerializeField] GameManager gamemanager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider otaku)
    {
        if(otaku.gameObject.name.Contains("Otaku") && !gamemanager.clear) gamemanager.StartCoroutine("GameOver");
        else Debug.Log("Otakuは文章の中に含まれていません。");
    }
}
