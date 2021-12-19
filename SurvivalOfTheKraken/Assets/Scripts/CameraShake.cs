using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region Fields
    private float _currentShakeTime = 1f;
    private readonly float _startingShakeTime = 1f;
    private Vector3 _startLocation;
    public bool Shake = false;
    public float Power = 0.05f;
    public bool StartShakeTimer = false;
    #endregion
    #region Methods
    private void Start()
    {
        _startLocation = transform.localPosition;
    }
    private void Update()
    {
        ShakeTimer();
        if (Shake)
        {
            transform.localPosition = transform.localPosition + Random.insideUnitSphere * Power;
            StartShakeTimer = true;
        }
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition
                                                   , _startLocation
                                                   , 1 * Time.deltaTime);
    }
    private void ShakeTimer()
    {
        if (StartShakeTimer)                                                                 //Timer
        {
            _currentShakeTime -= 1 * Time.fixedDeltaTime;
        }
        if (_currentShakeTime <= 0)
        {
            StartShakeTimer = false;
            _currentShakeTime = _startingShakeTime;
            Shake = false;
        }
    }
    #endregion
}
