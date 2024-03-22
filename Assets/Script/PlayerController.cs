using UnityEngine;
using UnityEngine.InputSystem;
using DialogueEditor;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_Speed = 3.0f;
    [SerializeField] private float m_TurnSmoothTime = 0.1f;
    private Animator m_Animator;
    private string collidedObject;

    private CharacterController m_Character;
    private float m_TurnSmoothVelocity;

    private Vector2 m_MoveVector;

    [SerializeField] ObjectDisplay m_InteractivObjects;

    #region Initialization
    private void OnEnable()
    {
        m_Character = gameObject.AddComponent<CharacterController>();
        m_Character.radius = 0.4f;
        m_Character.detectCollisions = true;

        // Find the character object and get its Animator component
        Transform childObject = transform.Find("character");
        if (childObject != null)
        {
            m_Animator = childObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Child object not found!");
        }
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
            // Get direction angle from direction vector
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Add π/2 (90 degrés) to rotation angle
            targetAngle += 90f;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, m_TurnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Use normalized vector to move the character
            Vector3 moveDirection = direction;

            // Apply the movement
            m_Character.Move(moveDirection.normalized * m_Speed * Time.deltaTime);
            m_Animator.SetBool("isWalkin", true);
        }
        else
        {
            // If the character don't move, set the isWalkin parameter to false
            m_Animator.SetBool("isWalkin", false);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "interactiv")
        {
            Debug.Log("Collision with: " + collider.gameObject.name);
            collidedObject = collider.gameObject.name;
            m_InteractivObjects.TriggerDialog(collidedObject);
        }
    }
}