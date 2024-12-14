using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] ActivateParentAndDeactivateText AD;
    public GameObject[] panels; // �����̃p�l�����Ǘ�
    public bool score;
    public bool sound;
    [SerializeField] AudioClip se;
    [SerializeField] AudioSource AS;
    [SerializeField] SoundManager soundmanager;

    public void ShowPanel(int panelIndex)
    {
        // �S�Ẵp�l�����\���ɂ���
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        // �w�肳�ꂽ�p�l����\��
        if (panelIndex >= 0 && panelIndex < panels.Length)
        {
            panels[panelIndex].SetActive(true);
            if(AD.parentObject.activeSelf) AD.parentObject.SetActive(false);
            if (panelIndex == 0) sound = true;
            else if(panelIndex == 1) {
                score = true;
                GameObject.Find("Manager").GetComponent<Ranking>().DispRank();
            }
            soundmanager = GameObject.Find("Manager").GetComponent<SoundManager>();
            AS.volume = soundmanager.master * soundmanager.se;
            AS.PlayOneShot(se);
            Debug.Log($"Panel {panelIndex} activated");
        }
    }
}