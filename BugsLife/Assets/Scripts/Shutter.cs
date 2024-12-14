using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shutter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Conbo;
    [SerializeField] Image Conbo_Back;
    [SerializeField] Image Filter;
    [SerializeField] Image ReloadImage;
    [SerializeField] Slider ScoreCharge;
    public int conbo = 0;
    int conbo_check = 0;
    float Speed = 10f;
    float chargeTime = 0f;
    [SerializeField] Charge charge;
    [SerializeField] GameManager gamemanager;
    public bool flash = false;
    public bool flashattack = false;
    bool reload = false;
    [SerializeField] Light FlashLight;
    [SerializeField] AudioClip SkillSE;
    [SerializeField] AudioClip FinalFlasSE;
    [SerializeField] AudioClip MissSE;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        
    }

    void Update()
    {
        if(conbo != 0){
            Conbo_Back.GetComponent<RectTransform>().anchoredPosition = new Vector2(-290 + (((int)Mathf.Log10(conbo))*70), 0f);
            Conbo.text = "<size=120>" + conbo + "</size><size=80>COMBO</size>";
        }
        else Conbo.text = null;

        if(!gamemanager.pause){
            if(flash){
                FlashLight.intensity -= 40*Time.deltaTime;;
                if(FlashLight.intensity == 0f) flash = false;
            }
            else if(flashattack){
                if(!flash){
                    FlashLight.intensity -= 80*Time.deltaTime;;
                    if(FlashLight.intensity == 0f) flashattack = false;
                }
            }

            if(chargeTime > 0f){
                chargeTime -= Time.deltaTime;
                ReloadImage.fillAmount = chargeTime;
            }
            else reload = true;
        }

        ScoreCharge.value = (int)(conbo%10);

        audioSource.volume = gamemanager.audiosource_SE.volume;
    }

    public void Shutter_Moment(GameObject Flash_Color)
    {
        GameObject f = Instantiate(Flash_Color, this.transform.position, this.transform.rotation);
        Rigidbody rb = f.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * Speed, ForceMode.Impulse); 
        flashattack = true;
        FlashLight.intensity = 40;
        audioSource.PlayOneShot(SkillSE);
    }

    public void FinalFlash_Button(GameObject finalflash)
    {
        if(reload){
            flash = true;
        
            FlashLight.intensity = 40;

            if(charge.FinalFlashGage.fillAmount == 1) {
                StartCoroutine( FinalFlash(finalflash));
                audioSource.PlayOneShot(FinalFlasSE);
                charge.power = 0;
            }

            reload = false;
            chargeTime = 1f;
        }
    }
    IEnumerator FinalFlash(GameObject finalflash)
    {
        GameObject f = Instantiate(finalflash, this.transform.position, this.transform.rotation);
        Rigidbody rb = f.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Speed;
        yield return new WaitForSeconds(3f);
        Destroy(f);
    }

    public void Miss()
    {
        audioSource.PlayOneShot(MissSE);
    }
}
