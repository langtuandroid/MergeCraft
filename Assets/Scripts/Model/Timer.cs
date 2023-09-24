using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public event UnityAction<float> TimerUpdated;
    public event UnityAction TimerFinished;

    [SerializeField] private int _timerSeconds;

    private float _remainingTime = 0f;

    public void ActivateTimer()
    {
        _remainingTime = _timerSeconds;
        TimerUpdated?.Invoke(_remainingTime);
        StartCoroutine(ActivateCoroutineTimer());
    }

    private IEnumerator ActivateCoroutineTimer()
    {
        while (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
            TimerUpdated?.Invoke(_remainingTime / _timerSeconds);
            yield return null;
        }

        if (_remainingTime <= 0)
            TimerFinished?.Invoke();
    }
}
