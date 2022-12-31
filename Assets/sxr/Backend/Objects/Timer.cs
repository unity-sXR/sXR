using System;
using System.Diagnostics;
using UnityEngine;


public class Timer
{
    private readonly float startTime;
    private readonly string timerName;
    private readonly float duration; 

    public Timer(string timerName, float duration) {
        startTime = Time.time;
        this.timerName = timerName;
        this.duration = duration; }

    public string GetName() { return timerName; }
    public float GetTimePassed() { return Time.time - startTime; }
    public float GetDuration() { return duration; }
    
}
