using UnityEngine;
using UnityEngine.EventSystems; // �K�v: UI�C�x���g�V�X�e���̑���Ɏg�p

public class ActivateParentAndDeactivateText : MonoBehaviour
{
    public GameObject parentObject; // �e�I�u�W�F�N�g�i��A�N�e�B�u�ɂ����I�u�W�F�N�g�j
    public GameObject textObject;   // ��A�N�e�B�u�ɂ���Text�I�u�W�F�N�g
    public GameObject[] buttonObjects; // �����̃{�^���I�u�W�F�N�g
    [SerializeField] PanelManager panel;
    [SerializeField] AudioClip se;
    [SerializeField] AudioSource AS;
    [SerializeField] SoundManager soundmanager;
    public void OpenSlido()
    {
        if(!panel.score && !panel.sound) {
            soundmanager = GameObject.Find("Manager").GetComponent<SoundManager>();
            AS.volume = soundmanager.master * soundmanager.se;
            if(!parentObject.activeSelf){
                AS.PlayOneShot(se);
                textObject.SetActive(false);
            }
            else textObject.SetActive(true);
            parentObject.SetActive(!parentObject.activeSelf);
        }
    }
}
