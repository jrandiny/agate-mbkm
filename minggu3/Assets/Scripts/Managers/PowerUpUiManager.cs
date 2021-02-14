using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PowerUpUiManager : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private CanvasGroup canvasGroup;

    private Slider _slider;
    private float _currentTime;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        _slider = GetComponentInChildren<Slider>();

        canvasGroup.alpha = 0;
    }

    public void ShowTimer(float time)
    {
        _currentTime = time;
    }

    private void Update()
    {
        if (_currentTime <= 0)
        {
            canvasGroup.alpha = 0;
            return;
        }

        canvasGroup.alpha = 1;

        _currentTime -= Time.deltaTime;
        _slider.value = _currentTime / maxTime * _slider.maxValue;
    }
}