using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class that contains the methods for the MainMenu scene
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private string firstLevel;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private AudioSource audioSource;

    private void OnEnable()
    {
        optionsScreen.SetActive(false);
        // start the background music in the main menu 
        audioSource.Play();
    }

    public void StartGame()
    {
        // when starting the game, it loads the game scene
        SceneManager.LoadScene(firstLevel);
    }

    /// <summary>
    /// Function that opens the OptionsMenu
    /// </summary>
    public void OpenOptions() => optionsScreen.SetActive(true);

    /// <summary>
    /// Function that closes the OptionsMenu
    /// </summary>
    public void CloseOptions() => optionsScreen.SetActive(false);

    /// <summary>
    /// Function that quits the game
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
