using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonStart;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private int gameSceneIndex;

    private void OnEnable()
    {
        buttonStart.onClick.AddListener(OnButtonStartClick);
        buttonQuit.onClick.AddListener(OnButtonQuitClick);
    }

    private void OnButtonStartClick()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }

    private void OnButtonQuitClick()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        buttonStart.onClick.RemoveAllListeners();
        buttonQuit.onClick.RemoveAllListeners();
    }
}
