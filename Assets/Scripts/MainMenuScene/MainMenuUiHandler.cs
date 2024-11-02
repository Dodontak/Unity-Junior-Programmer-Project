using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;

public class MainMenuUiHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private GameObject[] highScoreRecordPanels;
    private GeneralUIHandler generalUI;

    private MainManager mainManager;

    void Start()
    {
        mainManager = MainManager.instance;
        generalUI = GeneralUIHandler.instance;
    }
    public void StartGameScene(string saveFileName)
    {
        if (saveFileName == "default")
        {
            if (nameField.text.Length == 0)
            {
                generalUI.PopWarningText("Set your name for new game");
            }
            else
            {
                mainManager.playerName = nameField.text;
                mainManager.Load(saveFileName);
            }
        }
        else
        {
            mainManager.Load(saveFileName);
        }
    }
    public void TogleGameObject(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void FillHighScoreRecord()
    {
        string recordFile = Application.persistentDataPath + "/ranking.json";
        if (!File.Exists(recordFile)) return;
        string rankingJson = File.ReadAllText(recordFile);
        List<ClearTimeRecord> rankings = JsonUtility.FromJson<ClearSpeedRanking>(rankingJson).ranking;
        for (int i = 0; i < rankings.Count; ++i)
        {
            FillARankingPanel(highScoreRecordPanels[i], rankings[i]);
        }
    }

    private void FillARankingPanel(GameObject rankingPanel, ClearTimeRecord record)
    {
        TextMeshProUGUI[] tmps = rankingPanel.GetComponentsInChildren<TextMeshProUGUI>();
        //index: 0 = Rank string(1st, 2nd..), 1 = name string, 3 = clear time string
        tmps[1].text = record.playerName;
        tmps[2].text = TimeSpan.Parse(record.clearTime).Seconds.ToString() + "s";
    }
}
