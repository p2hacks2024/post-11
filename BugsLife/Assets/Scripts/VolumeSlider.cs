using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider; // BGM�X���C�_�[
    [SerializeField] private Slider mainSlider; // Main�X���C�_�[
    [SerializeField] private Slider seSlider; // SE�X���C�_�[
    [SerializeField] private Image bgmSoundIcon; // BGM�T�E���h�A�C�R��
    [SerializeField] private Image mainSoundIcon; // Main�T�E���h�A�C�R��
    [SerializeField] private Image seSoundIcon; // SE�T�E���h�A�C�R��
    [SerializeField] private Sprite muteIcon; // �~���[�g�A�C�R��
    [SerializeField] private Sprite undoIcon; // �~���[�g�����A�C�R��
    SoundManager soundManager;
    [SerializeField] AudioSource AS_BGM;
    [SerializeField] GameManager gamemanager;

    void Start()
    {
        // �X���C�_�[�̏����l�ݒ�ƃ��X�i�[�o�^
        bgmSlider.onValueChanged.AddListener(value => OnVolumeChanged(value, bgmSoundIcon));
        mainSlider.onValueChanged.AddListener(value => OnVolumeChanged(value, mainSoundIcon));
        seSlider.onValueChanged.AddListener(value => OnVolumeChanged(value, seSoundIcon));

        // �����A�C�R���̍X�V
        OnVolumeChanged(bgmSlider.value, bgmSoundIcon);
        OnVolumeChanged(mainSlider.value, mainSoundIcon);
        OnVolumeChanged(seSlider.value, seSoundIcon);

        soundManager = GameObject.Find("Manager").GetComponent<SoundManager>();

        mainSlider.value = soundManager.master;
        seSlider.value = soundManager.se;
        bgmSlider.value = soundManager.bgm;
    }

    void Update()
    {
        soundManager.master = mainSlider.value;
        soundManager.se = seSlider.value;
        soundManager.bgm = bgmSlider.value;

        /*if(SceneManager.GetActiveScene().name.Equals("MainScene")){
            if(!gamemanager.pause) {
                AS_BGM.volume = mainSlider.value * bgmSlider.value;
                AS_BGM.UnPause();
            }
            else AS_BGM.Pause();
        }
        else*/ AS_BGM.volume = mainSlider.value * bgmSlider.value;

        if(SceneManager.GetActiveScene().name.Equals("MainScene")){
            if(gamemanager.pause) {
                AS_BGM.Pause();
                Debug.Log("BGM停止");
            }
            else AS_BGM.UnPause();

            if(gamemanager.gameover || gamemanager.clear) AS_BGM.Stop();
        }
    }

    private void OnVolumeChanged(float value, Image soundIcon)
    {
        if (value == 0)
        {
            // �~���[�g��Ԃɂ���
            soundIcon.sprite = muteIcon;
        }
        else
        {
            // �~���[�g������Ԃɂ���
            soundIcon.sprite = undoIcon;
        }
    }
}
