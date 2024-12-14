using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gyro : MonoBehaviour
{
    Quaternion rot;
    [SerializeField] Text gy_text;
    [SerializeField] GameManager gamemanager;
    bool rot_set = false;

    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        if(!gamemanager.pause && !gamemanager.clear && !gamemanager.gameover && Input.gyro.enabled) StartCoroutine("GyroCamera");
    }

    IEnumerator GyroCamera()
    {

        IEnumerator enumerator = Set_rot();
        yield return enumerator;

        if(!rot_set){
            rot_set = true;
            gamemanager.OtakuGenerater.transform.eulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
        }
    }

    IEnumerator Set_rot()
    {
        var rotRH = Input.gyro.attitude;
        rot = (new Quaternion(-rotRH.x, -rotRH.z, -rotRH.y, rotRH.w)) * Quaternion.Euler(90f, 0f, 0f);
        transform.localRotation = rot;
        yield return null;
    }
}
