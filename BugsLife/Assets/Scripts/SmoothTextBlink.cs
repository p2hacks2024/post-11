using UnityEngine;
using UnityEngine.UI;

public class SmoothTextBlink : MonoBehaviour
{
    public Text targetText;
    public float blinkSpeed = 1f; // 点滅速度

    private bool isIncreasing = true;
    private float t = 0f; // 透明度の遷移割合
    private Color originalColor; // テキストの元の色（RGB部分を維持）

    void Start()
    {
        if (targetText == null)
        {
            targetText = GetComponent<Text>();
        }

        // テキストの元の色を保持（RGBを維持してアルファを変える）
        originalColor = targetText.color;
    }

    void Update()
    {
        // アルファ値を滑らかに変化させる
        t += (isIncreasing ? Time.deltaTime : -Time.deltaTime) * blinkSpeed;

        if (t >= 1f)
        {
            t = 1f;
            isIncreasing = false;
        }
        else if (t <= 0f)
        {
            t = 0f;
            isIncreasing = true;
        }

        // アルファ値だけを変化させて適用
        float alpha = Mathf.Lerp(0f, 1f, t); // 透明（0）から完全表示（1）へ遷移
        targetText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }
}
