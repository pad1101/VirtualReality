using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    public bool stopwatchActive = false;
    public Text timeText;
    private float time = 0f;

    public void Start(){
        StartTimer();
    }

    public void Update(){
        if (stopwatchActive) {
            time += Time.deltaTime;
            timeText.text = "Vergangene Zeit: " +"\n"+ time;
        }
    }

    public void StartTimer() {
        stopwatchActive = true;
    }

    public void EndTimer() {
        stopwatchActive = false;
    }
}
