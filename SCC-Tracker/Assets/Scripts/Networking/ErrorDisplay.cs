using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ErrorDisplay : MonoBehaviour
{
    public GameObject panel;
    public Text ErrorText;
    public Text FixText;

    public static GameObject _panel;
    public static Text txt_Error;
    public static Text txt_Fix;

    private void Awake()
    {
        _panel = panel;
        txt_Error = ErrorText;
        txt_Fix = FixText;
    }

    public static void OpenErrorMessage()
    {
        _panel.SetActive(true);
    }

    public static void CloseErrorMessage()
    {
        _panel.SetActive(false);
    }

    public static void UpdateErrorMessage(string msg)
    {
        txt_Error.text = msg;
    }

    public static void UpdateErrorFix(string msg)
    {
        txt_Fix.text = msg;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}