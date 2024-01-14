using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointBasedUI : MonoBehaviour
{
    public GameObject OneStar;
    public GameObject TwoStar;
    public GameObject ThreeStar;

    public HighscoreManager HighscoreManager;

    public TextMeshProUGUI pointsText;
    public TMP_InputField nameText;
    private int points = 0;
    private int maxPoints = 27000;

    private void Awake()
    {
        points = PlayerPrefs.GetInt("Points");

        if (PlayerPrefs.GetInt("Demo") != 1)
        {
            if (points > (maxPoints * 0.7))
            {
                ThreeStar.SetActive(true);
                TwoStar.SetActive(false);
                OneStar.SetActive(false);
            }
            else
            {
                if (points > (maxPoints * 0.4))
                {
                    ThreeStar.SetActive(false);
                    TwoStar.SetActive(true);
                    OneStar.SetActive(false);
                }
                else
                {
                    ThreeStar.SetActive(false);
                    TwoStar.SetActive(false);
                    OneStar.SetActive(true);
                }
            }
        }

        pointsText.text = points.ToString() + " Points";
    }

    public void saveHS()
    {
        string playerName = nameText.text;
        int newHighscore = points;

        HighscoreManager.SaveHighscore(playerName, newHighscore);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("WorldMap");
    }

}
