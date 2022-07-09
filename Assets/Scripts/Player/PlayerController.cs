using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;

    [SerializeField] private float JumpForce = 300f;

    //! добавить тег Ground на землю
    [SerializeField] private bool _isGrounded;

    private Vector2 _direction;
    private Rigidbody2D _rb;

    #region Singleton

    public static PlayerController Instance { get; private set; }

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovementLogic();
        JumpLogic();
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void MovementLogic()
    {
        _direction.x = Input.GetAxis("Horizontal");

        _direction.y = Input.GetAxis("Vertical");

        _rb.velocity = _direction * Speed;
    }

    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
            if (_isGrounded)
                _rb.AddForce(Vector3.up * JumpForce);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == "Ground") _isGrounded = value;
    }
}