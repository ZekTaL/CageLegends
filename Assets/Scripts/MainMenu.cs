using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string firstLevel;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private AudioSource audioSource;

    private void OnEnable()
    {
        optionsScreen.SetActive(false);
        audioSource.Play();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenOptions() => optionsScreen.SetActive(true);

    public void CloseOptions() => optionsScreen.SetActive(false);

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
