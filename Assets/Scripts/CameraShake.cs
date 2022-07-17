using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class CameraShake : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Coroutine _cameraShakeRoutine;
    
    private void OnEnable()
    {
        _player.CameraShakeEvent += OnCameraShakeEvent;
    }

    private void OnDisable()
    {
        _player.CameraShakeEvent -= OnCameraShakeEvent;
    }

    private void OnCameraShakeEvent(CameraShakeSettings css)
    {
        if (_cameraShakeRoutine != null)
        {
            StopCoroutine(_cameraShakeRoutine);
        }
        
        StartCoroutine(CameraShakeRoutine(css.Duration, css.Magnitude, css.Noize));
    }

    private IEnumerator CameraShakeRoutine(float duration, float magnitude, float noize)
    {
        var elapsed = 0f;
        Vector3 startPosition = transform.localPosition;
        Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
        Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;

        while (elapsed < duration)
        {
            Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
            Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);
            Vector2 cameraPostionDelta = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y),
                Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));
            cameraPostionDelta *= magnitude;
            transform.localPosition = startPosition + (Vector3) cameraPostionDelta;
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPosition;
    }
}