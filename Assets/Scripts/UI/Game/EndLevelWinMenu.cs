using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelWinMenu : MonoBehaviour
{
    [SerializeField] private GameObject winMenu;

    private void OnEnable()
    {
        EndLevelTimer.Complite += ShowMenu;
    }

    private void OnDisable()
    {
        EndLevelTimer.Complite -= ShowMenu;
    }

    private void ShowMenu()
    {
        StartCoroutine(ShowMenuCorutine());
    }

    private IEnumerator ShowMenuCorutine()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(7f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
