using UnityEngine;
using UnityEngine.SceneManagement; // シーン操作に必要

public class SceneChanger : MonoBehaviour
{
    // 切り替えたいシーン名をInspectorで設定できるようにする
    public string sceneName;

    // ボタンが押されたときに呼び出す関数
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("シーン名が設定されていません！");
        }
    }
}
