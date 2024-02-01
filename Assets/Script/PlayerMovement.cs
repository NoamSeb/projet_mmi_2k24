using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_Speed = 3.0f;
    [SerializeField] private float m_TurnSmoothTime = 0.1f;

    private CharacterController m_Character;
    private float m_TurnSmoothVelocity;

    private Vector2 m_MoveVector;

    #region Initialization
    private void OnEnable()
    {
        m_Character = gameObject.AddComponent<CharacterController>();
        m_Character.radius = 0.4f;
    }

    private void OnDisable()
    {
        Destroy(m_Character);
    }
    #endregion

    private void FixedUpdate()
    {
        Move();
    }

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        m_MoveVector = context.ReadValue<Vector2>();
        Debug.Log(m_MoveVector);
    }

   private void Move()
{
    // Find the direction
    Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y).normalized;
    Debug.Log("Direction :");
    Debug.Log(direction);

    if (direction.magnitude >= 0.1f)
    {
        // Use the world space direction directly
        Vector3 moveDirection = new Vector3(direction.x, 0f, m_MoveVector.y);
 
        // Apply movement
        m_Character.Move(moveDirection * m_Speed * Time.deltaTime);
    }
}
}