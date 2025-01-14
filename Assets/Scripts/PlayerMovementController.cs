using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    public Transform camTransform;
    public bool isGrounded = false;
    public Vector3 inputDir = Vector3.zero;


    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _turnSmoothTime = 0.1f;
    [SerializeField] private float _pushingForce = 50f;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    private float _turnSmoothVelocity;
    private float _gravity = Physics.gravity.y;
    private Vector3 _velocity;


    private void Update()
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = _gravity;
        }

        if (inputDir.magnitude >= 0.1f)
        {
            Move(inputDir);
        }

        AddGravity();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody otherRB = hit.gameObject.GetComponent<Rigidbody>();

        if (otherRB != null)
        {
            otherRB.AddForce(_characterController.velocity.normalized * _pushingForce, ForceMode.Force);
        }
    }


    public void Move(Vector3 movement)
    {
        Vector3 moveDirection = FaceMovementDirection(movement);

        _characterController.Move(moveDirection.normalized * _speed * Time.deltaTime);
    }

    public void Jump()
    {
        _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
    }

    private Vector3 FaceMovementDirection(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        return moveDir;
    }

    private void AddGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.red : Color.green;
        Gizmos.DrawSphere(_groundCheck.position, _groundDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
    }

}
