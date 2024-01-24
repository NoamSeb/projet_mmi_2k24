using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_Speed = 5.0f;
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
    }

    private void Move()
    {
        // Find the direction
        Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Convert input direction to world space
            Vector3 moveDirection = Camera.main.transform.TransformDirection(new Vector3(direction.x, 0f, direction.y));

            // Apply movement
            m_Character.Move(moveDirection * m_Speed * Time.deltaTime);
        }
    }
}
