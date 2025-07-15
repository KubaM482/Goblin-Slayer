
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;



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
        lastTimeAttack += Time.deltaTime;
        currenState.Update(this);
        HandleMovement();
    }

    public void changeState(IPlayerState newState)
    {
        currenState?.Exit(this);
        currenState = newState;
        currenState.Enter(this);


    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {

        if (canAttack)
        {
            if (currenState is IdleState)
            {
                changeState(new AttackingState());
            }
        }
    }



    public void HandleMovement()

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


    public void OnAttackAnimationEnd()
    {

        changeState(moveInput == Vector2.zero ? new IdleState() : new RunningState());
    }

    public void AttackResetCooldown()
    {
        lastTimeAttack = 0f;
    }

}

