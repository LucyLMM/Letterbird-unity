using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Startup_Countdown : MonoBehaviour
{

    
    public int local_rpm;
    public int timer = 0;
    public TextMeshProUGUI countdown;
    private ConnectErgometer rpm_script;
    public GameObject tooSlow;
    public GameObject tooFast;
    public GameObject rightSpeed;

    private void Awake()
    {
        countdown.text = "5";

        // Find a GameObject with the specified tag
        GameObject ergometerManager = GameObject.FindWithTag("ergometer");

        // Check if the GameObject was found
        if (ergometerManager != null)
        {
            // Do something with the found GameObject
            Debug.Log("Found GameObject with tag: " + ergometerManager.name);
            rpm_script = ergometerManager.GetComponent<ConnectErgometer>();
            if (rpm_script != null)
            {
                Debug.Log("rpm_script found");
            }
            else
            {
                Debug.Log("SCRIPT NOT FOUND");
            }
        }
        else
        {
            // Handle the case when the GameObject with the specified tag is not found
            Debug.LogError("No GameObject found with tag: YourTagName");
        }

        float bmi = PlayerPrefs.GetInt("playerWeight") / ((PlayerPrefs.GetInt("playerHeight")/ 100) * (PlayerPrefs.GetInt("playerHeight") / 100));

        if (bmi < 25 || (PlayerPrefs.GetInt("playerPAL") == 1))
            {
            rpm_script.bikeRes = PlayerPrefs.GetInt("playerWeight") / 2;
        }
        else
        {
            rpm_script.bikeRes = PlayerPrefs.GetInt("playerWeight");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(PlayerPrefs.GetInt("Demo") == 0)
            {
                //here if for stages 0,1 and 2 (road, glimmerwood, blackmire)

                int stage = PlayerPrefs.GetInt("Stage");

                if(stage == 0)
                {
                    SceneManager.LoadScene("GameRun");
                }

                if(stage == 1)
                {
                    SceneManager.LoadScene("GlimmerwoodRun");
                }
                if(stage == 2)
                {
                    SceneManager.LoadScene("BlackmireRun");
                }

            }
            else
            {
                SceneManager.LoadScene("DemoRun");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        local_rpm = rpm_script.rpm;

        if (local_rpm >= 60 && local_rpm <= 80)
        {
            timer++;
            rightSpeed.SetActive(true);
            tooFast.SetActive(false);
            tooSlow.SetActive(false);
        }

        if (local_rpm < 60)
        {
            timer = 0;
            countdown.text = "5";
            rightSpeed.SetActive(false);
            tooFast.SetActive(false);
            tooSlow.SetActive(true);

        }

        if(local_rpm > 80)
        {
            timer = 0;
            countdown.text = "5";
            rightSpeed.SetActive(false);
            tooFast.SetActive(true);
            tooSlow.SetActive(false);
        }

        if (timer >= 300)
        {
            if (PlayerPrefs.GetInt("Demo") == 0)
            {
                SceneManager.LoadScene("GameRun");
            }
            else
            {
                SceneManager.LoadScene("DemoRun");
            }
        }

        if(timer >= 250)
        {
            countdown.text = "0";
        }else if (timer >= 200)
        {
            countdown.text = "1";
        }
        else if (timer >= 150)
        {
            countdown.text = "2";
        }
        else if (timer >= 100)
        {
            countdown.text = "3";
        }
        else if (timer >= 50) 
        {
            countdown.text = "4";
        }
    }
}
