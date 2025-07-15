
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

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    private AttackHandler attackHandler;
    public Vector2 moveInput { get; private set; }
    private IPlayerState currenState;
    public static PlayerMovement Instance { get; private set; }

    public bool canAttack => lastTimeAttack >= cooldownAttack;
    public bool isAttackOnCooldown => lastTimeAttack < cooldownAttack;


    private Rigidbody2D rb;
    private Animator anim;
    Vector2 moveInput;
    public static PlayerMovement Instance {get; private set; }
    private PlayerState currentState = PlayerState.Idle;
   



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackHandler = GetComponent<AttackHandler>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        lastTimeAttack = 100f;



    }

    private void Start()
    {
        changeState(new IdleState());

    }

    void LateUpdate()
    {
        bool isMoving = rb.linearVelocity.magnitude > 0.05f;
         anim.SetBool("IsRunning", isMoving);
       
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

            if (currenState is IdleState)
            {
                changeState(new AttackingState());
            }

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