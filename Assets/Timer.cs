using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text text; // UI �ؽ�Ʈ
    float time; // Ÿ�̸� �ð�
    bool isGameOver = false; // ���� ���� �÷���
    ScoreManager scoreManager; // ���� �Ŵ���

    void Start()
    {
        time = 0; // �ð� �ʱ�ȭ
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (!isGameOver)
        {
            time += Time.deltaTime; // �ð� ����
            text.text = time.ToString("F2"); // �ؽ�Ʈ ������Ʈ
        }
    }

    // Ÿ�̸� ���߱�
    public void StopTimer()
    {
        isGameOver = true; // ���� ���� ���·� ����
    }

    // ���� �ð��� ��ȯ�ϴ� �޼ҵ�
    public float GetTime()
    {
        return time;
    }
    public void EndGame(string playerName)
    {
        isGameOver = true;
        scoreManager.SubmitScore(playerName, time);
    }
}