using System.Collections;
using UnityEngine;

public class DayAndNightCyclers : MonoBehaviour
{
    [SerializeField] private Material[] skyboxM;
    public GameParameters gameParameters;
    public Transform starsTransform;

    private float _starsRefreshRate;
    private float _rotationAngleStep;
    private Vector3 _rotationAxis;

    private Material _currentSkybox;
    private Material _targetSkybox;
    private float _transitionDuration = 2.0f; // Duration of skybox transition in seconds
    private float _transitionTimer = 0.0f;

    private void Awake()
    {
        starsTransform.rotation = Quaternion.Euler(
            gameParameters.dayInitialRatio * 360f,
            -30f,
            0f
        );

        _starsRefreshRate = 0.5f;
        _rotationAxis = starsTransform.right;
        _rotationAngleStep = 360f * _starsRefreshRate / gameParameters.dayLengthInSeconds;

        UpdateSkybox();
    }

    private void Start()
    {
        StartCoroutine("_UpdateStars");
    }

    private IEnumerator _UpdateStars()
    {
        while (true)
        {
            starsTransform.Rotate(_rotationAxis, _rotationAngleStep, Space.World);
            _transitionTimer += Time.deltaTime;

            if (_transitionTimer < gameParameters.dayInitialRatio)
            {
                float t = _transitionTimer / gameParameters.dayInitialRatio;
                RenderSettings.skybox.Lerp(_currentSkybox, _targetSkybox, t);
            }
            else
            {
                RenderSettings.skybox = _targetSkybox;
                UpdateSkybox();
            }
            yield return new WaitForSeconds(_starsRefreshRate);
        }
    }

    private void UpdateSkybox()
    {
        float currentTimeRatio = Mathf.Repeat(Time.time / gameParameters.dayLengthInSeconds, 1.0f);

        int currentSkyboxIndex = Mathf.FloorToInt(currentTimeRatio * skyboxM.Length);
        int nextSkyboxIndex = (currentSkyboxIndex + 1) % skyboxM.Length;

        _currentSkybox = skyboxM[currentSkyboxIndex];
        Debug.Log("Current sky box" + _currentSkybox);
        _targetSkybox = skyboxM[nextSkyboxIndex];
        Debug.Log("Target Skybox" + _targetSkybox);
        _transitionTimer = 0.0f;
    }

    //private void Update()
    //{
    //    _transitionTimer += Time.deltaTime;

    //    if (_transitionTimer < gameParameters.dayInitialRatio)
    //    {
    //        float t = _transitionTimer / gameParameters.dayInitialRatio;
    //        RenderSettings.skybox.Lerp(_currentSkybox, _targetSkybox, t);
    //    }
    //    else
    //    {
    //        RenderSettings.skybox = _targetSkybox;
    //        UpdateSkybox();
    //    }
    //}
}
