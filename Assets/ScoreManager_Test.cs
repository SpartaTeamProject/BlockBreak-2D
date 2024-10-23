using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class NameInputHandler : MonoBehaviour
{
    public InputField nameInputField; // �Է� �ʵ�
    public Text displayNameText;       // ���÷��� �ؽ�Ʈ
    private Timer timer;               // Ÿ�̸� Ŭ���� ����
    private List<NameEntry> nameEntries; // �̸��� Ÿ�̸� ���� ������ ����Ʈ
    private const int maxNames = 5;    // �ִ� �̸� ����
    private string filePath;            // JSON ���� �� �ε� ���

    private void Start()
    {
        // Ÿ�̸� ������Ʈ�� ã�Ƽ� ����
        timer = FindObjectOfType<Timer>();
        nameEntries = new List<NameEntry>(); // ����Ʈ �ʱ�ȭ

        // ������ ���� ��� ����
        filePath = Path.Combine(Application.persistentDataPath, "nameEntries.json");

        // ���� �׸� �ε�
        LoadEntries();
    }

    public void SubmitName()
    {
        // �Էµ� �̸� ��������
        string name = nameInputField.text;

        // ���� Ÿ�̸� �� ��������
        float timerValue = timer.GetTime();

        // �̸��� ������� ������ ����Ʈ�� �߰�
        if (!string.IsNullOrEmpty(name))
        {
            // �ִ� ������ �����ϸ� ���� ���� �̸� ����
            if (nameEntries.Count >= maxNames)
            {
                RemoveSlowestEntry(); // ���� ���� �̸� ����
            }

            nameEntries.Add(new NameEntry(name, timerValue)); // �� �̸��� Ÿ�̸� �� �߰�
            nameEntries.Sort((a, b) => b.timerValue.CompareTo(a.timerValue)); // �������� ����
            UpdateDisplayText();
            SaveEntries(); // ���� �� �׸� ����
        }

        // �Է� �ʵ� ����
        nameInputField.text = string.Empty;
    }

    private void RemoveSlowestEntry()
    {
        int slowestIndex = 0;

        // ���� ���� Ÿ�̸� ���� ���� �ε��� ã��
        for (int i = 1; i < nameEntries.Count; i++)
        {
            if (nameEntries[i].timerValue > nameEntries[slowestIndex].timerValue)
            {
                slowestIndex = i;
            }
        }

        // ���� ���� �̸� ����
        nameEntries.RemoveAt(slowestIndex);
    }

    private void UpdateDisplayText()
    {
        displayNameText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ
        foreach (var entry in nameEntries)
        {
            displayNameText.text += entry.name + "! Ÿ�̸�: " + entry.timerValue.ToString("F2") + " ��\n";
        }
    }

    // JSON���� �̸� �׸� ����
    private void SaveEntries()
    {
        string json = JsonUtility.ToJson(new NameEntryList(nameEntries));
        File.WriteAllText(filePath, json);
    }

    // JSON���κ��� �̸� �׸� �ε�
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

    // JSON ����ȭ�� ���� �̸� �׸� ����Ʈ Ŭ����
    [System.Serializable]
    private class NameEntryList
    {
        public List<NameEntry> entries;

        public NameEntryList(List<NameEntry> entries)
        {
            this.entries = entries;
        }
    }

    // �̸��� Ÿ�̸� ���� ������ Ŭ����
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
