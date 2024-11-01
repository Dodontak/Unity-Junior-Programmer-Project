using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuUiHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private TMP_InputField nameField;
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
}
