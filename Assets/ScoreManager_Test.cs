using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class NameInputHandler : MonoBehaviour
{
    public InputField nameInputField; // 입력 필드
    public Text displayNameText;       // 디스플레이 텍스트
    private Timer timer;               // 타이머 클래스 참조
    private List<NameEntry> nameEntries; // 이름과 타이머 값을 저장할 리스트
    private const int maxNames = 5;    // 최대 이름 개수
    private string filePath;            // JSON 저장 및 로드 경로

    private void Start()
    {
        // 타이머 컴포넌트를 찾아서 연결
        timer = FindObjectOfType<Timer>();
        nameEntries = new List<NameEntry>(); // 리스트 초기화

        // 저장할 파일 경로 설정
        filePath = Path.Combine(Application.persistentDataPath, "nameEntries.json");

        // 기존 항목 로드
        LoadEntries();
    }

    public void SubmitName()
    {
        // 입력된 이름 가져오기
        string name = nameInputField.text;

        // 현재 타이머 값 가져오기
        float timerValue = timer.GetTime();

        // 이름이 비어있지 않으면 리스트에 추가
        if (!string.IsNullOrEmpty(name))
        {
            // 최대 개수에 도달하면 가장 느린 이름 제거
            if (nameEntries.Count >= maxNames)
            {
                RemoveSlowestEntry(); // 가장 느린 이름 제거
            }

            nameEntries.Add(new NameEntry(name, timerValue)); // 새 이름과 타이머 값 추가
            nameEntries.Sort((a, b) => b.timerValue.CompareTo(a.timerValue)); // 내림차순 정렬
            UpdateDisplayText();
            SaveEntries(); // 제출 후 항목 저장
        }

        // 입력 필드 비우기
        nameInputField.text = string.Empty;
    }

    private void RemoveSlowestEntry()
    {
        int slowestIndex = 0;

        // 가장 느린 타이머 값을 가진 인덱스 찾기
        for (int i = 1; i < nameEntries.Count; i++)
        {
            if (nameEntries[i].timerValue > nameEntries[slowestIndex].timerValue)
            {
                slowestIndex = i;
            }
        }

        // 가장 느린 이름 제거
        nameEntries.RemoveAt(slowestIndex);
    }

    private void UpdateDisplayText()
    {
        displayNameText.text = ""; // 기존 텍스트 초기화
        foreach (var entry in nameEntries)
        {
            displayNameText.text += entry.name + "! 타이머: " + entry.timerValue.ToString("F2") + " 초\n";
        }
    }

    // JSON으로 이름 항목 저장
    private void SaveEntries()
    {
        string json = JsonUtility.ToJson(new NameEntryList(nameEntries));
        File.WriteAllText(filePath, json);
    }

    // JSON으로부터 이름 항목 로드
    private void LoadEntries()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            NameEntryList loadedEntries = JsonUtility.FromJson<NameEntryList>(json);
            nameEntries = loadedEntries.entries;
            UpdateDisplayText();
        }
    }

    // JSON 직렬화를 위한 이름 항목 리스트 클래스
    [System.Serializable]
    private class NameEntryList
    {
        public List<NameEntry> entries;

        public NameEntryList(List<NameEntry> entries)
        {
            this.entries = entries;
        }
    }

    // 이름과 타이머 값을 저장할 클래스
    [System.Serializable]
    private class NameEntry
    {
        public string name;
        public float timerValue;

        public NameEntry(string name, float timerValue)
        {
            this.name = name;
            this.timerValue = timerValue;
        }
    }
}
