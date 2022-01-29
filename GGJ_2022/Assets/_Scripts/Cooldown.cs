using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    float timerValue;
    float currentValue;

    bool isRunning = false;    
    public float setTimer { set => timerValue = value; }
    public bool IsTimerRunning { get => isRunning; }
    public float CurrentValue { get => currentValue; set => currentValue = value; }

    //Constructor
    public Cooldown(float startValue, bool startImediately)
    {
        this.timerValue = startValue;
        this.currentValue = timerValue;

        if (startImediately)
        {
            ToggleTimer();
        }
    }

    public void Tick() 
    { 
        if(isRunning && currentValue >= 0)
        {
            currentValue -= Time.deltaTime;
        }
    }

    //Toggle Timer On/Off
    public void ToggleTimer() => isRunning = !isRunning;
    public void ResetTimer() => currentValue = timerValue;
}
