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
    [SerializeField] private GameObject WarnPanel;
    [SerializeField] private TextMeshProUGUI WarnText;
    [SerializeField] private TMP_InputField nameField;
    private MainManager mainManager;

    void Start()
    {
        mainManager = MainManager.instance;
    }
    public void StartGameScene(string saveFileName)
    {
        if (saveFileName == "default")
        {
            if (nameField.text.Length == 0)
            {
                PopWarningText("Set your name for new game");
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

    public void PopWarningText(string warningText)
    {
        WarnText.text = warningText;
        ActiveForSeconds(WarnPanel, 2);
    }
    void ActiveForSeconds(GameObject target, int sec)
    {
        StartCoroutine(ActiveAndDeactive(target, sec));
    }

    IEnumerator ActiveAndDeactive(GameObject target, int sec)
    {
        target.SetActive(true);
        yield return new WaitForSeconds(sec);
        target.SetActive(false);
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
