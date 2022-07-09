using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] private Camera _camera;


    // Update is called once per frame
    private void Update()
    {
        var worldMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        var perpendicular = Vector3.Cross(transform.position - worldMousePosition, Vector3.forward);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }
}