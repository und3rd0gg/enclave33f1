using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CameraShakeSettings a;
    [SerializeField] private CameraShakeSettings b;
    [SerializeField] private CameraShakeSettings c;
    [SerializeField] private CameraShakeSettings d;
    
    public event Action<CameraShakeSettings> CameraShakeEvent;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            CameraShakeEvent?.Invoke(a);
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            CameraShakeEvent?.Invoke(b);
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            CameraShakeEvent?.Invoke(c);
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            CameraShakeEvent?.Invoke(d);
        }

    }
}
