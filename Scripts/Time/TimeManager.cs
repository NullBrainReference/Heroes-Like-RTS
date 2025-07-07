using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private TextMeshProUGUI _weekText;
    [SerializeField] private Slider _timeSlider;

    [SerializeField] 
    private float _timeSpeed;

    private TimeModel _time;

    public TimeModel TimeModel { get { return _time; }
        set
        {
            _time = value;
            DayUpdate();
        }
    }

    private void Awake()
    {
        _time = new TimeModel();

        DayUpdate();
    }

    private void Update()
    {
        _time.DayProgress += _timeSpeed * Time.deltaTime;

        if (_time.DayProgress >= TimeModel.ProgressTarget)
        {
            _time.DayProgress = 0;
            _time.Day++;

            if (_time.Day % 7 == 0)
                EventBus.Instance.Invoke(EventType.NewDay);

            DayUpdate();
        }

        ProgressBarUpdate();
    }

    private void ProgressBarUpdate()
    {
        _timeSlider.value = _time.DayProgress;
    }

    private void DayUpdate()
    {
        _dayText.text = $"Day\n{_time.DayOfWeek}";
        _weekText.text = $"Week {_time.Week}";
    }
}
