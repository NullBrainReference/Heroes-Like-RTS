using UnityEngine;

[System.Serializable]
public class TimeModel
{
    public const float ProgressTarget = 1f;

    public float DayProgress;

    public int Day;

    public int DayOfWeek => Day % 7 + 1;
    public int Week => Day / 7 + 1;
}
