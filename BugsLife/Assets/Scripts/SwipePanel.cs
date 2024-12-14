using UnityEngine;
using UnityEngine.UI;

public class SwipePanel : MonoBehaviour
{
    public GameObject scrollbar; // Scrollbar�I�u�W�F�N�g
    private float scrollPos = 0f; // ���݂̃X�N���[���ʒu
    private float[] positions;   // �q�I�u�W�F�N�g���Ƃ̃^�[�Q�b�g�ʒu
    private bool isDragging = false; // �h���b�O�����ǂ����𔻒�

    void Start()
    {
        // �q�I�u�W�F�N�g���Ƃ̃^�[�Q�b�g�ʒu��������
        InitializePositions();
        scrollPos = scrollbar.GetComponent<Scrollbar>().value; // �����ʒu��ݒ�
    }

    void Update()
    {
        if (isDragging)
        {
            scrollPos = scrollbar.GetComponent<Scrollbar>().value; // ���݂̃X�N���[���ʒu���X�V
        }
        else
        {
            // ���݂̈ʒu���^�[�Q�b�g�ʒu�ɃX�i�b�v
            SnapToPosition();
        }
    }

    private void InitializePositions()
    {
        int childCount = transform.childCount;
        positions = new float[childCount];

        // �q�I�u�W�F�N�g���Ƃ̐��K�����ꂽ�ʒu���v�Z
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

        // ���݂̃X�N���[���ʒu�ɍł��߂��^�[�Q�b�g�ʒu���v�Z
        for (int i = 0; i < positions.Length; i++)
        {
            float distance = Mathf.Abs(scrollPos - positions[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        // �ł��߂��^�[�Q�b�g�ʒu�ɃX�i�b�v
        scrollPos = positions[closestIndex];

        // �X���[�Y�ɃX�N���[���ʒu���ړ�
        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(
            scrollbar.GetComponent<Scrollbar>().value,
            scrollPos,
            0.1f // �X���[�Y�ȃA�j���[�V�������x
        );
    }

    public void OnBeginDrag()
    {
        isDragging = true; // �h���b�O���J�n
    }

    public void OnEndDrag()
    {
        isDragging = false; // �h���b�O���I��
    }
}
