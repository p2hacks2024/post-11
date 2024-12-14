using UnityEngine;
using UnityEngine.UI;

public class SwipePanel : MonoBehaviour
{
    public GameObject scrollbar; // Scrollbarオブジェクト
    private float scrollPos = 0f; // 現在のスクロール位置
    private float[] positions;   // 子オブジェクトごとのターゲット位置
    private bool isDragging = false; // ドラッグ中かどうかを判定

    void Start()
    {
        // 子オブジェクトごとのターゲット位置を初期化
        InitializePositions();
        scrollPos = scrollbar.GetComponent<Scrollbar>().value; // 初期位置を設定
    }

    void Update()
    {
        if (isDragging)
        {
            scrollPos = scrollbar.GetComponent<Scrollbar>().value; // 現在のスクロール位置を更新
        }
        else
        {
            // 現在の位置をターゲット位置にスナップ
            SnapToPosition();
        }
    }

    private void InitializePositions()
    {
        int childCount = transform.childCount;
        positions = new float[childCount];

        // 子オブジェクトごとの正規化された位置を計算
        float distance = 1f / (childCount - 1f);
        for (int i = 0; i < childCount; i++)
        {
            positions[i] = distance * i;
        }
    }

    private void SnapToPosition()
    {
        float closestDistance = float.MaxValue;
        int closestIndex = 0;

        // 現在のスクロール位置に最も近いターゲット位置を計算
        for (int i = 0; i < positions.Length; i++)
        {
            float distance = Mathf.Abs(scrollPos - positions[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        // 最も近いターゲット位置にスナップ
        scrollPos = positions[closestIndex];

        // スムーズにスクロール位置を移動
        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(
            scrollbar.GetComponent<Scrollbar>().value,
            scrollPos,
            0.1f // スムーズなアニメーション速度
        );
    }

    public void OnBeginDrag()
    {
        isDragging = true; // ドラッグを開始
    }

    public void OnEndDrag()
    {
        isDragging = false; // ドラッグを終了
    }
}
