using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class beatManager : MonoBehaviour
{
    [SerializeField] private float _bpm;
    [SerializeField] public AudioSource _audioSource;
    [SerializeField] private Intervals[] _intervals;

    private void Update()
    {
        foreach (Intervals interval in _intervals)
        {
            float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * interval.GetIntervalLength(_bpm))) - interval.GetDelayInSeconds(_bpm);
            interval.CheckForNewInterval(sampledTime, _bpm);
        }
    }

}

[System.Serializable]
public class Intervals
{
    [SerializeField] private float _steps;
    [SerializeField] private float _delayInBeats; // Delay w taktach/miarach
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval;

    // Funkcja przeliczaj¹ca opóŸnienie z taktów na sekundy
    public float GetDelayInSeconds(float bpm)
    {return _delayInBeats * (60f / bpm);}

    public float GetIntervalLength(float bpm)
    {return 60f / (bpm * _steps);}

    public void CheckForNewInterval(float interval, float bpm)
    {
        // Aplikujemy opóŸnienie w sekundach do obliczeñ
        float delayInSeconds = GetDelayInSeconds(bpm);
        float adjustedInterval = interval - delayInSeconds;

        if (adjustedInterval >= 0 && Mathf.FloorToInt(adjustedInterval) != _lastInterval)
        {
            _lastInterval = Mathf.FloorToInt(adjustedInterval);
            _trigger.Invoke();
        }
    }
}
