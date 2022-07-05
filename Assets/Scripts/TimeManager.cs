using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    public static bool stopwatchActive = false;
    public Text timeText;
    private float time = 0f;

    public void Start(){
        StartTimer();
    }

    public void Update(){
        if (stopwatchActive) {
            time += Time.deltaTime;
            timeText.text = "Vergangene Zeit: " +"\n"+ time + " s";
        }
    }

    public void StartTimer() {
        stopwatchActive = true;
    }

    public static void EndTimer() {
        stopwatchActive = false;
    }
}
