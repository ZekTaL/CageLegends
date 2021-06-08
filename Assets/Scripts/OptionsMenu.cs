using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that contains the methods for the OptionsScreen
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameSettingsTab;
    [SerializeField] private GameObject graphicsTab;
    [SerializeField] private GameObject audioTab;

    private List<Toggle> toggleList = new List<Toggle>();

    private void Awake()
    {
        // Fill the list with all the toggles (active and inactives) in the options menu
        toggleList = GetComponentsInChildren<Toggle>(true).ToList();
    }

    private void OnEnable()
    {
        // Check if there are already saved values in the system, if not use a default value
        float matchTime = PlayerPrefs.GetFloat("matchTime", 1f);
        int maxScore = PlayerPrefs.GetInt("maxScore", 2);

        // select the toggle based on the matchtime value and toggle it on
        switch (matchTime)
        {
            case 1f: toggleList.FirstOrDefault(toggle => toggle.name == "1minToggle").isOn = true; break;
            case 2f: toggleList.FirstOrDefault(toggle => toggle.name == "2minToggle").isOn = true; break;
            case 3f: toggleList.FirstOrDefault(toggle => toggle.name == "3minToggle").isOn = true; break;
            case 5f: toggleList.FirstOrDefault(toggle => toggle.name == "5minToggle").isOn = true; break;
            case 10f: toggleList.FirstOrDefault(toggle => toggle.name == "10minToggle").isOn = true; break;
            case -1f: toggleList.FirstOrDefault(toggle => toggle.name == "NoMinLimitToggle").isOn = true; break;
        }

        // select the toggle based on the maxScore value and toggle it on
        switch (maxScore)
        {
            case 1: toggleList.FirstOrDefault(toggle => toggle.name == "1GoalToggle").isOn = true; break;
            case 2: toggleList.FirstOrDefault(toggle => toggle.name == "2GoalToggle").isOn = true; break;
            case 3: toggleList.FirstOrDefault(toggle => toggle.name == "3GoalToggle").isOn = true; break;
            case 5: toggleList.FirstOrDefault(toggle => toggle.name == "5GoalToggle").isOn = true; break;
            case 10: toggleList.FirstOrDefault(toggle => toggle.name == "10GoalToggle").isOn = true; break;
            case -1: toggleList.FirstOrDefault(toggle => toggle.name == "NoGoalLimitToggle").isOn = true; break;
        }

        SelectGameSettingsTab();            
    }

    /// <summary>
    /// Set active the GameSettings tab
    /// </summary>
    public void SelectGameSettingsTab()
    {
        gameSettingsTab.SetActive(true);
        graphicsTab.SetActive(false);
        audioTab.SetActive(false);
    }

    /// <summary>
    /// Set active the GraphicsSettings tab
    /// </summary>
    public void SelectGraphicsTab()
    {
        gameSettingsTab.SetActive(false);
        graphicsTab.SetActive(true);
        audioTab.SetActive(false);
    }

    /// <summary>
    /// Set active the AudioSettings tab
    /// </summary>
    public void SelectAudioTab()
    {
        gameSettingsTab.SetActive(false);
        graphicsTab.SetActive(false);
        audioTab.SetActive(true);
    }

    // Region containing the GameSettings
    #region Game Settings
    
    // Region containing the functions to update the matchtime toggles
    #region MatchTimeToggles

    /// <summary>
    /// Set the match time to 1 minute
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMatchTimeOne(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = 1f;
            PlayerPrefs.SetFloat("matchTime", 1f);
        }
    }

    /// <summary>
    /// Set the match time to 2 minutes
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMatchTimeTwo(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = 2f;
            PlayerPrefs.SetFloat("matchTime", 2f);
        }
    }

    /// <summary>
    /// Set the match time to 3 minutes
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMatchTimeThree(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = 3f;
            PlayerPrefs.SetFloat("matchTime", 3f);
        }
    }

    /// <summary>
    /// Set the match time to 5 minutes
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMatchTimeFive(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = 5f;
            PlayerPrefs.SetFloat("matchTime", 5f);
        }
    }

    /// <summary>
    /// Set the match time to 10 minutes
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMatchTimeTen(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = 10f;
            PlayerPrefs.SetFloat("matchTime", 10f);
        }
    }

    /// <summary>
    /// Set the match time without limit
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMatchTimeNoLimit(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = -1f;
            PlayerPrefs.SetFloat("matchTime", -1f);
        }
    }

    #endregion

    // Region containing the functions to update the goal score limit toggles
    #region ScoreLimitToggles

    /// <summary>
    /// Set the goal score limit to 1
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMaxScoreOne(bool isOn)
    {
        if (isOn)
        {
            Game.maxScore = 1;
            PlayerPrefs.SetInt("maxScore", 1);
        }
    }

    /// <summary>
    /// Set the goal score limit to 2
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMaxScoreTwo(bool isOn)
    {
        if (isOn)
        {
            Game.maxScore = 2;
            PlayerPrefs.SetInt("maxScore", 2);
        }
    }

    /// <summary>
    /// Set the goal score limit to 3
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMaxScoreThree(bool isOn)
    {
        if (isOn)
        {
            Game.maxScore = 3;
            PlayerPrefs.SetInt("maxScore", 3);
        }
    }

    /// <summary>
    /// Set the goal score limit to 5
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMaxScoreFive(bool isOn)
    {
        if (isOn)
        {
            Game.maxScore = 5;
            PlayerPrefs.SetInt("maxScore", 5);
        }
    }

    /// <summary>
    /// Set the goal score limit to 10
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMaxScoreTen(bool isOn)
    {
        if (isOn)
        {
            Game.maxScore = 10;
            PlayerPrefs.SetInt("maxScore", 10);
        }
    }

    /// <summary>
    /// Set the goal score limit to unlimited
    /// </summary>
    /// <param name="isOn">bool value of the toggle</param>
    public void SetMaxScoreNoLimit(bool isOn)
    {
        if (isOn)
        {
            Game.maxScore = -1;
            PlayerPrefs.SetInt("maxScore", -1);
        }
    }

    #endregion

    #endregion
}
