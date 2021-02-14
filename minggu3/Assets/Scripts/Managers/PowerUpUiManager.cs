using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PowerUpUiManager : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private CanvasGroup _canvasGroup;

    private Slider _slider;
    private float _currentTime;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _slider = GetComponentInChildren<Slider>();

        _canvasGroup.alpha = 0;
    }

    public void ShowTimer(float time)
    {
        _currentTime = time;
    }

    private void Update()
    {
        if (_currentTime <= 0)
        {
            _canvasGroup.alpha = 0;
            return;
        }

        _canvasGroup.alpha = 1;

        _currentTime -= Time.deltaTime;
        _slider.value = _currentTime / maxTime * _slider.maxValue;
    }
}