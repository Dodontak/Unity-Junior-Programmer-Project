using TMPro;
using UnityEngine;

public class GeneralUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject warnPanel;
    static public GeneralUIHandler instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PopWarningText(string warningText)
    {
        GameObject obj = Instantiate(warnPanel, GameObject.Find("Canvas").transform);
        TextMeshProUGUI tmp = obj.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = warningText;
        Destroy(obj, 2f);
    }
}
