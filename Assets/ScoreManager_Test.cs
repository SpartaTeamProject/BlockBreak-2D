using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System;

public class GameRanking : MonoBehaviour
{
    public InputField nameInputField; // ����ڰ� �̸��� �Է��ϴ� InputField
    public Button submitButton; // ���� ��ư
    public Text rankingText; // ������ ǥ���� Text
    private Timer timer; // Ÿ�̸� ��ü�� ���� ����
    private List<PlayerRecord> playerRecords = new List<PlayerRecord>(); // �÷��̾� ��� ����Ʈ
    private string filePath; // JSON ���� ���

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/rankingData5.json"; // JSON ���� ��� ����
        nameInputField.characterLimit = 3; // �̸� �Է� �ʵ忡 �ִ� 3�� ���� �߰�
        LoadRankingData(); // ������ ����� ��ŷ ������ �ҷ�����
        submitButton.onClick.AddListener(SubmitNameAndEndGame); // ��ư Ŭ�� ������ �߰�
        timer = FindObjectOfType<Timer>(); // Ÿ�̸� ��ü�� ã��
        DisplayTopFiveRankings(); // ���� ���� �� ��ŷ ǥ��
    }

    // �̸��� �����ϰ� ������ ������ �� �ð��� ����ϴ� �Լ�
    void SubmitNameAndEndGame()
    {
        string playerName = nameInputField.text; // �÷��̾� �̸� ��������
        float gameEndTime = timer.GetTime(); // Ÿ�̸ӿ��� �ð��� ������

        // �÷��̾� ��� ����
        playerRecords.Add(new PlayerRecord(playerName, gameEndTime));

        // JSON ���Ϸ� ����
        SaveRankingData();

        // Ÿ�̸� ���߰� ����
        timer.StopTimer();
        timer.ResetTimer();

        // �Է��� �� ���� �����ϰ� �ϱ� ���� ��ư�� �Է� �ʵ� ��Ȱ��ȭ
        submitButton.interactable = false; // ��ư ��Ȱ��ȭ
        nameInputField.interactable = false; // InputField ��Ȱ��ȭ

        // ���� �ؽ�Ʈ ������Ʈ
        DisplayTopFiveRankings(); // ���ο� ������ ǥ��
    }

    // ���� 5�� ������ ������������ �����Ͽ� ����ϴ� �Լ�
    void DisplayTopFiveRankings()
    {
        // ���� ���� �ð��� �������� �������� ���� �� ���� 5���� ��������
        var topFive = playerRecords.OrderByDescending(record => record.gameEndTime).Take(5).ToList();

        // ����� ���� �ؽ�Ʈ �ۼ�
        rankingText.text = "     Top 5 Rankings\n\n";
        rankingText.text += $"{"����",-5}{"�̸�",-10}{" ����",-15}\n"; // ��� �߰�

        // �̸��� �ִ� ���̿� ���缭 ����
        for (int i = 0; i < topFive.Count; i++)
        {
            rankingText.text += $" {i + 1,-5} {topFive[i].playerName,-10}{topFive[i].gameEndTime:F2}\n";
        }
    }

    // ��ŷ �����͸� JSON ���Ϸ� �����ϴ� �Լ�
    void SaveRankingData()
    {
        try
        {
            string json = JsonUtility.ToJson(new PlayerRecordList(playerRecords), true); // ����Ʈ�� JSON �������� ��ȯ
            File.WriteAllText(filePath, json); // JSON �����͸� ���Ͽ� ����
            Debug.Log("Ranking data saved to " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save ranking data: " + e.Message);
        }
    }

    // ����� ��ŷ �����͸� �ҷ����� �Լ�
    void LoadRankingData()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath); // ���Ͽ��� JSON ������ �б�
                PlayerRecordList recordList = JsonUtility.FromJson<PlayerRecordList>(json); // JSON �����͸� ����Ʈ�� ��ȯ
                playerRecords = recordList.playerRecords; // ����Ʈ ������Ʈ
                Debug.Log("Ranking data loaded from " + filePath);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load ranking data: " + e.Message);
            }
        }
    }
}

// �÷��̾� ����� ������ Ŭ����
[System.Serializable]
public class PlayerRecord
{
    public string playerName; // �÷��̾� �̸�
    public float gameEndTime; // ���� ���� �ð�

    public PlayerRecord(string name, float time)
    {
        playerName = name;
        gameEndTime = time;
    }
}

// JSON ������ ���� ���� Ŭ����
[System.Serializable]
public class PlayerRecordList
{
    public List<PlayerRecord> playerRecords; // �÷��̾� ��� ����Ʈ

    public PlayerRecordList(List<PlayerRecord> records)
    {
        playerRecords = records;
    }
}
