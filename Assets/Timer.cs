using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField] Text text;
    float time;
    bool isGameOver = false;
    ScoreManager scoreManager;

    void Start()
    {
        time = 0;
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (!isGameOver)
        {
            time += Time.deltaTime;
            text.text = time.ToString("F2");
        }
    }

    // Ÿ�̸� ���߱�
    public void StopTimer()
    {
        isGameOver = true;
    }

    // Ÿ�̸� ����
    public void ResetTimer()
    {
        time = 0;
        isGameOver = false;
        text.text = time.ToString("F2");
    }

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
