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
        highScores.Sort((a, b) => b.score.CompareTo(a.score)); // ���� �������� ����

        if (highScores.Count > maxScores)
        {
            highScores.RemoveAt(highScores.Count - 1); // ���� ���� ����
        }

        DisplayScores();
    }

    void DisplayScores()
    {
        scoreDisplay.text = ""; // ���� ���� �ʱ�ȭ
        foreach (var highScore in highScores)
        {
            scoreDisplay.text += $"{highScore.name}: {highScore.score:F2}\n"; // ���� ����ȭ
        }
    }

    public void OnSubmit()
    {
        string playerName = nameInputField.text;
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            // �÷��̾� �̸� �Է� �� EndGame ȣ��
            FindObjectOfType<Timer>().EndGame(playerName);
        }
    }
}
