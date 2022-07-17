using UnityEngine;

public class testMovement : MonoBehaviour
{
    
    void Update()
    {
        var movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(movementVector * Time.deltaTime * 3);
    }
}
