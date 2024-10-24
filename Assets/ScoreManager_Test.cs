using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System;

public class GameRanking : MonoBehaviour
{
    public InputField nameInputField; // 사용자가 이름을 입력하는 InputField
    public Button submitButton; // 제출 버튼
    public Text rankingText; // 순위를 표시할 Text
    private Timer timer; // 타이머 객체를 위한 변수
    private List<PlayerRecord> playerRecords = new List<PlayerRecord>(); // 플레이어 기록 리스트
    private string filePath; // JSON 파일 경로

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/rankingData5.json"; // JSON 파일 경로 설정
        nameInputField.characterLimit = 3; // 이름 입력 필드에 최대 3자 제한 추가
        LoadRankingData(); // 기존에 저장된 랭킹 데이터 불러오기
        submitButton.onClick.AddListener(SubmitNameAndEndGame); // 버튼 클릭 리스너 추가
        timer = FindObjectOfType<Timer>(); // 타이머 객체를 찾음
        DisplayTopFiveRankings(); // 게임 시작 시 랭킹 표시
    }

    // 이름을 제출하고 게임이 끝났을 때 시간을 기록하는 함수
    void SubmitNameAndEndGame()
    {
        string playerName = nameInputField.text; // 플레이어 이름 가져오기
        float gameEndTime = timer.GetTime(); // 타이머에서 시간을 가져옴

        // 플레이어 기록 저장
        playerRecords.Add(new PlayerRecord(playerName, gameEndTime));

        // JSON 파일로 저장
        SaveRankingData();

        // 타이머 멈추고 리셋
        timer.StopTimer();
        timer.ResetTimer();

        // 입력을 한 번만 가능하게 하기 위해 버튼과 입력 필드 비활성화
        submitButton.interactable = false; // 버튼 비활성화
        nameInputField.interactable = false; // InputField 비활성화

        // 순위 텍스트 업데이트
        DisplayTopFiveRankings(); // 새로운 순위를 표시
    }

    // 상위 5개 순위를 내림차순으로 정렬하여 출력하는 함수
    void DisplayTopFiveRankings()
    {
        // 게임 종료 시간을 기준으로 내림차순 정렬 후 상위 5개만 가져오기
        var topFive = playerRecords.OrderByDescending(record => record.gameEndTime).Take(5).ToList();

        // 출력할 순위 텍스트 작성
        rankingText.text = "     Top 5 Rankings\n\n";
        rankingText.text += $"{"순위",-5}{"이름",-10}{" 점수",-15}\n"; // 헤더 추가

        // 이름의 최대 길이에 맞춰서 정렬
        for (int i = 0; i < topFive.Count; i++)
        {
            rankingText.text += $" {i + 1,-5} {topFive[i].playerName,-10}{topFive[i].gameEndTime:F2}\n";
        }
    }

    // 랭킹 데이터를 JSON 파일로 저장하는 함수
    void SaveRankingData()
    {
        try
        {
            string json = JsonUtility.ToJson(new PlayerRecordList(playerRecords), true); // 리스트를 JSON 형식으로 변환
            File.WriteAllText(filePath, json); // JSON 데이터를 파일에 저장
            Debug.Log("Ranking data saved to " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save ranking data: " + e.Message);
        }
    }

    // 저장된 랭킹 데이터를 불러오는 함수
    void LoadRankingData()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath); // 파일에서 JSON 데이터 읽기
                PlayerRecordList recordList = JsonUtility.FromJson<PlayerRecordList>(json); // JSON 데이터를 리스트로 변환
                playerRecords = recordList.playerRecords; // 리스트 업데이트
                Debug.Log("Ranking data loaded from " + filePath);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load ranking data: " + e.Message);
            }
        }
    }
}

// 플레이어 기록을 저장할 클래스
[System.Serializable]
public class PlayerRecord
{
    public string playerName; // 플레이어 이름
    public float gameEndTime; // 게임 종료 시각

    public PlayerRecord(string name, float time)
    {
        playerName = name;
        gameEndTime = time;
    }
}

// JSON 저장을 위한 래퍼 클래스
[System.Serializable]
public class PlayerRecordList
{
    public List<PlayerRecord> playerRecords; // 플레이어 기록 리스트

    public PlayerRecordList(List<PlayerRecord> records)
    {
        playerRecords = records;
    }
}
