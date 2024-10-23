using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text text; // UI 텍스트
    float time; // 타이머 시간
    bool isGameOver = false; // 게임 오버 플래그
    ScoreManager scoreManager; // 점수 매니저

    void Start()
    {
        time = 0; // 시간 초기화
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (!isGameOver)
        {
            time += Time.deltaTime; // 시간 증가
            text.text = time.ToString("F2"); // 텍스트 업데이트
        }
    }

    // 타이머 멈추기
    public void StopTimer()
    {
        isGameOver = true; // 게임 오버 상태로 변경
    }

    // 현재 시간을 반환하는 메소드
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