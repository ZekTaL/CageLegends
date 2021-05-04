using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameSettingsTab;
    [SerializeField] private GameObject graphicsTab;
    [SerializeField] private GameObject audioTab;

    private List<Toggle> toggleList = new List<Toggle>();

    private void Awake()
    {
        toggleList = GetComponentsInChildren<Toggle>(true).ToList();
    }

    private void OnEnable()
    {
        float matchTime = PlayerPrefs.GetFloat("matchTime", 1f);
        int maxScore = PlayerPrefs.GetInt("maxScore", 2);

        switch (matchTime)
        {
            case 1f: toggleList.FirstOrDefault(toggle => toggle.name == "1minToggle").isOn = true; break;
            case 2f: toggleList.FirstOrDefault(toggle => toggle.name == "2minToggle").isOn = true; break;
            case 3f: toggleList.FirstOrDefault(toggle => toggle.name == "3minToggle").isOn = true; break;
            case 5f: toggleList.FirstOrDefault(toggle => toggle.name == "5minToggle").isOn = true; break;
            case 10f: toggleList.FirstOrDefault(toggle => toggle.name == "10minToggle").isOn = true; break;
            case -1f: toggleList.FirstOrDefault(toggle => toggle.name == "NoMinLimitToggle").isOn = true; break;
        }

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

    public void SelectGameSettingsTab()
    {
        gameSettingsTab.SetActive(true);
        graphicsTab.SetActive(false);
        audioTab.SetActive(false);
    }

    public void SelectGraphicsTab()
    {
        gameSettingsTab.SetActive(false);
        graphicsTab.SetActive(true);
        audioTab.SetActive(false);
    }

    public void SelectAudioTab()
    {
        gameSettingsTab.SetActive(false);
        graphicsTab.SetActive(false);
        audioTab.SetActive(true);
    }


    #region Game Settings

    #region MatchTimeToggles

    public void SetMatchTimeOne(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = 1f;
            PlayerPrefs.SetFloat("matchTime", 1f);
        }
    }

    public void SetMatchTimeTwo(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = 2f;
            PlayerPrefs.SetFloat("matchTime", 2f);
        }
    }

    public void SetMatchTimeThree(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = 3f;
            PlayerPrefs.SetFloat("matchTime", 3f);
        }
    }

    public void SetMatchTimeFive(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = 5f;
            PlayerPrefs.SetFloat("matchTime", 5f);
        }
    }

    public void SetMatchTimeTen(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = 10f;
            PlayerPrefs.SetFloat("matchTime", 10f);
        }
    }

    public void SetMatchTimeNoLimit(bool isOn)
    {
        if (isOn)
        {
            Game.matchTime = -1f;
            PlayerPrefs.SetFloat("matchTime", -1f);
        }
    }

    #endregion

    #region ScoreLimitToggles

    public void SetMaxScoreOne(bool isOn)
    {
        if (isOn)
        {
            Game.maxScore = 1;
            PlayerPrefs.SetInt("maxScore", 1);
        }
    }

    public void SetMaxScoreTwo(bool isOn)
    {
        if (isOn)
        {
            Game.maxScore = 2;
            PlayerPrefs.SetInt("maxScore", 2);
        }
    }

    public void SetMaxScoreThree(bool isOn)
    {
        if (isOn)
        {
            Game.maxScore = 3;
            PlayerPrefs.SetInt("maxScore", 3);
        }
    }

    public void SetMaxScoreFive(bool isOn)
    {
        if (isOn)
        {
            Game.maxScore = 5;
            PlayerPrefs.SetInt("maxScore", 5);
        }
    }

    public void SetMaxScoreTen(bool isOn)
    {
        if (isOn)
        {
            Game.maxScore = 10;
            PlayerPrefs.SetInt("maxScore", 10);
        }
    }

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
