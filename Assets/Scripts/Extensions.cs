using UnityEngine;

public static class Extensions
{
    public static void LookAtMouse(this Transform transform, Camera camera)
    {
        var dir = camera.ScreenToWorldPoint (Input.mousePosition) - transform.position;
        transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg);
    }
    
    public static void LookAt2D(this Transform transform, Vector2 direction)
    {
        transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg);
    }
}