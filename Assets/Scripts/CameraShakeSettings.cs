using UnityEngine;

[CreateAssetMenu(fileName = "New Camera Shake Settings", menuName = "Settings/Camera Shake Settings")]
public class CameraShakeSettings : ScriptableObject
{
    [SerializeField] [Range(0, 15)] private float _duration;
    [SerializeField] [Range(0, 10)] private float _magnitude;
    [SerializeField] [Range(0, 5000)] private float _noize;

    public float Duration => _duration;
    public float Magnitude => _magnitude;
    public float Noize => _noize;
}