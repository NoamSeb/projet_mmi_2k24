using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float m_remainingTime;

    void Update()
    {
        UpdateTimer();
        CheckRemainingTime();
    }

    // CoolDown function from 150 in our case to 0
    void UpdateTimer()
    {
        if(m_remainingTime > 0){
            m_remainingTime -= Time.deltaTime;
        } else if (m_remainingTime < 0){
            m_remainingTime = 0;
            timerText.color = Color.red;
        }

        int minutes = Mathf.FloorToInt(m_remainingTime / 60);
        int seconds = Mathf.FloorToInt(m_remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void CheckRemainingTime()
    {
        if (m_remainingTime == 0)
        {
            // re render the scene if m_remainingTime == 0
            SceneManager.LoadScene(1);
        }
    }
}
