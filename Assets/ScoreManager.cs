using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] InputField nameInputField;
    [SerializeField] Text scoreDisplay;

    List<(string name, float score)> highScores = new List<(string, float)>();
    const int maxScores = 5;

    public void SubmitScore(string playerName, float score)
    {
        highScores.Add((playerName, score));
        highScores.Sort((a, b) => b.score.CompareTo(a.score)); // 점수 내림차순 정렬

        if (highScores.Count > maxScores)
        {
            highScores.RemoveAt(highScores.Count - 1); // 최하 점수 제거
        }

        DisplayScores();
    }

    void DisplayScores()
    {
        scoreDisplay.text = ""; // 현재 점수 초기화
        foreach (var highScore in highScores)
        {
            scoreDisplay.text += $"{highScore.name}: {highScore.score:F2}\n"; // 점수 형식화
        }
    }

    public void OnSubmit()
    {
        string playerName = nameInputField.text;
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            // 플레이어 이름 입력 시 EndGame 호출
            FindObjectOfType<Timer>().EndGame(playerName);
        }
    }
}
