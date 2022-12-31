using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [Singleton] Tracks all timers in the scene to allow for sxr.CheckTimer(name) calls.
/// Use Timer(string timerName, float duration) to instantiate Timer. Once [duration]
/// seconds has passed and a CheckTimer call is made, will return true and destroy the Timer. 
/// </summary>
public class TimerHandler : MonoBehaviour {
    private List<Timer> allTimers = new List<Timer>();

    /// <summary>
    /// Adds a timer to the list of named timers
    /// </summary>
    /// <param name="timerName"></param>
    /// <param name="duration"></param>
    public void AddTimer(string timerName, float duration) { allTimers.Add(new Timer(timerName, duration)); }
    
    /// <summary>
    /// Checks if Timer.duration (seconds) has passed for the named timer.
    /// Returns true and destroys timer after time has passed
    /// </summary>
    /// <param name="name">Name of Timer to find</param>
    /// <returns>true once Timer.duration seconds has passed</returns>
    public bool CheckTimer(string timerName) {
        foreach (var timer in allTimers) {
            if (timer.GetName() == timerName) {
                if (timer.GetTimePassed() > timer.GetDuration()) {
                    allTimers.Remove(timer);
                    return true; }

                return false; } }

        sxr.DebugLog("No timer with name \"" + timerName + "\" found.");
        return false; }

    /// <summary>
    /// Checks how long has passed on the named timer
    /// </summary>
    /// <param name="timerName"></param>
    /// <returns></returns>
    public float GetTimePassed(string timerName) {
        foreach (var timer in allTimers)
            if (timer.GetName() == timerName)
                return timer.GetTimePassed();
        sxr.DebugLog("No timer with name \"" + timerName + "\" found.");
        return 0; }

    

    // Singleton initiated on Awake()  
    public static TimerHandler Instance;
    void Awake() {
        if ( Instance == null) {Instance = this;  DontDestroyOnLoad(gameObject.transform.root);}
        else Destroy(gameObject); }

}
