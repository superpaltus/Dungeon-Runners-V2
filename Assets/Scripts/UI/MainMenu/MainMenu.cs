using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonStart;
    [SerializeField] private Button buttonControls;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private int gameSceneIndex;
    [SerializeField] private GameObject controlsMenu;

    private void OnEnable()
    {
        buttonStart.onClick.AddListener(OnButtonStartClick);
        buttonControls.onClick.AddListener(OnButtonControlsClick);
        buttonQuit.onClick.AddListener(OnButtonQuitClick);
    }

    private void OnButtonControlsClick()
    {
        controlsMenu.SetActive(true);
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
