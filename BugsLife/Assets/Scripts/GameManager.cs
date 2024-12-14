using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject ScorePanel;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI ScoreAddText;
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] TextMeshProUGUI FinishText;
    [SerializeField] TextMeshProUGUI GameOverText;
    [SerializeField] TextMeshProUGUI ResultScoreText;
    [SerializeField] TextMeshProUGUI StreetNameText;
    [SerializeField] GameObject[] Otaku = new GameObject[3];
    [SerializeField] GameObject[] ScoreScreen = new GameObject[2];
    [SerializeField] Sprite[] Back = new Sprite[2];
    [SerializeField] Image BackImage;
    [SerializeField] GameObject Finish;
    [SerializeField] Sprite[] ResultScore = new Sprite[3];
    [SerializeField] GameObject ResultScoreImage;
    [SerializeField] GameObject StreetNameImage;
    [SerializeField] GameObject MenuButton;
    public GameObject OtakuGenerater;
    float time = 0f;
    float timer = 78f;
    public bool pause;
    public bool clear;
    public bool gameover;
    public int score;
    public AudioSource audiosource_SE;
    [SerializeField] AudioSource audiosource_ResultBGM;
    SoundManager soundmanager;
    [SerializeField] AudioClip PauseSE;
    [SerializeField] AudioClip CancelSE;
    [SerializeField] AudioClip SuccessSE;
    [SerializeField] AudioClip FailedSE;
    [SerializeField] Slider Master;
    [SerializeField] Slider SE;
    [SerializeField] Slider BGM;

    void Start()
    {
        soundmanager = GameObject.Find("Manager").GetComponent<SoundManager>();
        Master.value = soundmanager.master;
        SE.value = soundmanager.se;
        BGM.value = soundmanager.bgm;
    }

    void Update()
    {
        if(!pause && !clear && !gameover){
            time += Time.deltaTime;
            timer -= Time.deltaTime;
            if(time >= 1.5f) Otaku_Generate();
        }

        ScoreText.text = score.ToString("D8");
        TimerText.text = ((int)timer/60).ToString() + ":" + ((int)(timer%60) + 1).ToString("D2");

        if(timer < 0f && !clear && !gameover) {
            TimerText.text = "0:00";
            StartCoroutine("GameClear");
        }

        if(!pause){
            Master.value = soundmanager.master;
            SE.value = soundmanager.se;
            BGM.value = soundmanager.bgm;
        }

        audiosource_SE.volume = SE.value * Master.value;
        audiosource_ResultBGM.volume = BGM.value * Master.value;
    }

    void Otaku_Generate()
    {
        int otaku_kazu = 3 - (int)(timer/26);
        Debug.Log(otaku_kazu);

        List<int> num = new List<int>();
        for (int i = 0; i <= 10; i++) {
            num.Add(i);
        }
        
        for(var i = 0; i < otaku_kazu; i++){
            int otaku_color = Random.Range(0, 3);

            int index = Random.Range(0, num.Count);
            int rnd = num[index];

            Instantiate(Otaku[otaku_color], OtakuGenerater.transform.GetChild(rnd).transform.position, Quaternion.identity);

            num.RemoveAt(index);
        }

        num.Clear();

        time = 0f;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pause = true;
        audiosource_SE.PlayOneShot(PauseSE);
        PausePanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        pause = false;
        PausePanel.SetActive(false);
        audiosource_SE.PlayOneShot(CancelSE);
    }

    IEnumerator GameClear()
    {
        clear = true;
        FinishText.text = "LIVE SUCCESS";
        Finish.SetActive(true);
        audiosource_SE.PlayOneShot(SuccessSE);
        yield return new WaitForSeconds(1f);
        audiosource_ResultBGM.Play();
        yield return new WaitForSeconds(3f);
        ResultScoreText.text = "Score " + score.ToString("D8");
        if(score >= 0 && score <= 100000){
            StreetNameText.text = "かけだしアイドル";
            BackImage.sprite = Back[1];
            ResultScoreImage.SetActive(false);
            StreetNameImage.SetActive(false);
        }
        else if(score > 100000 && score <= 500000){
            StreetNameText.text = "いちにんまえアイドル";
            BackImage.sprite = Back[0];
            ResultScoreImage.GetComponent<Image>().sprite = ResultScore[1];
            ResultScoreImage.SetActive(true);
            StreetNameImage.GetComponent<Image>().sprite = ResultScore[1];
            StreetNameImage.SetActive(true);
        }
        else if(score > 500000 && score <= 1000000){
            StreetNameText.text = "じゅくれんアイドル";
            BackImage.sprite = Back[0];
            ResultScoreImage.GetComponent<Image>().sprite = ResultScore[2];
            ResultScoreImage.SetActive(true);
            StreetNameImage.GetComponent<Image>().sprite = ResultScore[2];
            StreetNameImage.SetActive(true);
        }
        else if (score > 1000000){
            StreetNameText.text = "えいえんのアイドル";
            BackImage.sprite = Back[0];
            ResultScoreImage.GetComponent<Image>().sprite = ResultScore[0];
            ResultScoreImage.SetActive(true);
            StreetNameImage.GetComponent<Image>().sprite = ResultScore[0];
            StreetNameImage.SetActive(true);
        }
        ScorePanel.SetActive(true);
        GameObject.Find("Manager").GetComponent<Ranking>().SetRank(score);
        yield return null;
    }
    public IEnumerator GameOver()
    {
        gameover = true;
        FinishText.text = "LIVE FAILED";
        Finish.SetActive(true);
        audiosource_SE.PlayOneShot(FailedSE);
        yield return new WaitForSeconds(1f);
        audiosource_ResultBGM.Play();
        yield return new WaitForSeconds(3f);
        GameOverText.text = "Failed…";
        ResultScoreText.text = "Score " + score.ToString("D8");
        if(score >= 0 && score <= 100000){
            StreetNameText.text = "かけだしアイドル";
            BackImage.sprite = Back[1];
            ResultScoreImage.SetActive(false);
            StreetNameImage.SetActive(false);
        }
        else if(score > 100000 && score <= 500000){
            StreetNameText.text = "いちにんまえアイドル";
            BackImage.sprite = Back[0];
            ResultScoreImage.GetComponent<Image>().sprite = ResultScore[1];
            ResultScoreImage.SetActive(true);
            StreetNameImage.GetComponent<Image>().sprite = ResultScore[1];
            StreetNameImage.SetActive(true);
        }
        else if(score > 500000 && score <= 1000000){
            StreetNameText.text = "じゅくれんアイドル";
            BackImage.sprite = Back[0];
            ResultScoreImage.GetComponent<Image>().sprite = ResultScore[2];
            ResultScoreImage.SetActive(true);
            StreetNameImage.GetComponent<Image>().sprite = ResultScore[2];
            StreetNameImage.SetActive(true);
        }
        else if (score > 1000000){
            StreetNameText.text = "えいえんのアイドル";
            BackImage.sprite = Back[0];
            ResultScoreImage.GetComponent<Image>().sprite = ResultScore[0];
            ResultScoreImage.SetActive(true);
            StreetNameImage.GetComponent<Image>().sprite = ResultScore[0];
            StreetNameImage.SetActive(true);
        }
        ScorePanel.SetActive(true);
        yield return null;
    }

    public IEnumerator ScorePlus(int scoreAdd)
    {
        var scorecheck = score;
        ScoreAddText.text = "+" + scoreAdd.ToString();
        yield return new WaitForSeconds(0.1f);
        if(scorecheck == score)ScoreAddText.text = null;
    }

    public void Menu()
    {
        ScoreScreen[0].SetActive(false);
        ScoreScreen[1].SetActive(true);
        MenuButton.SetActive(false);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }

    public void BackTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }
}
