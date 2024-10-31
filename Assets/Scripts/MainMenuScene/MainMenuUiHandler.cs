using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUiHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private GameObject WarnPanel;
    [SerializeField] private TextMeshProUGUI WarnText;
    [SerializeField] private TMP_InputField nameField;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        
    }
    public void StartGameScene()
    {
        if (nameField.text.Length == 0)
        {
            PopWarningText("Set your name for new game");
        }
        else
        {
            gameManager.playerName = nameField.text;
            SceneManager.LoadScene(1);
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
}
