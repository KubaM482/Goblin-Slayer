
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Idle,
    Running,
    Attacking
}


public class PlayerMovement : MonoBehaviour
{    
  
     [SerializeField] private float speed;
    [SerializeField] private float cooldownAttack;
    private float lastTimeAttack = 0f;

    private Rigidbody2D rb;
    private Animator anim;
    Vector2 moveInput;
    public static PlayerMovement Instance {get; private set; }
    private PlayerState currentState = PlayerState.Idle;
   



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        lastTimeAttack = 100f;

    }

    void Update()
    {
        HandleMovement();
        HandleState();
        AnimationUpdate();

        if (lastTimeAttack < cooldownAttack)
        {
            lastTimeAttack += Time.deltaTime;    
        }

    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        if (currentState != PlayerState.Attacking && lastTimeAttack >= cooldownAttack)
        {
            currentState = PlayerState.Attacking;
            anim.SetTrigger("Attacking");
            lastTimeAttack = 0f;
        }
    }


    void HandleState()
    {
       

        if (moveInput == Vector2.zero)
        {
            currentState = PlayerState.Idle;
        }
        else
        {
            currentState = PlayerState.Running;
        }
    }

    void HandleMovement()
    {
        bool playerHasHorizontalSpeed = math.abs(rb.linearVelocity.x) > Mathf.Epsilon;
        Vector2 movement = moveInput.normalized * speed;
        rb.linearVelocity = movement;

        if (playerHasHorizontalSpeed && rb.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        else if (playerHasHorizontalSpeed && rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    void AnimationUpdate()
    {
        anim.SetBool("IsRunning", currentState == PlayerState.Running);
    }

}