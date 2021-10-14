using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D rigidbody2D;
    //private Transform transform;
    [SerializeField]
    private static float force = 20.0f;
    [SerializeField]
    private static float _basespeed = 4.0f;
    [SerializeField]
    private static float speed = _basespeed;
    private Vector2 jumpForce = new Vector2(0, force);

    [SerializeField]
    private float max_depth = 0.6f;
    private Collider2D collider2D;
    [SerializeField]
    private LayerMask layerMask;
    private bool resetJump = true;
    private PlayerAnimation _playeranim;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _swordArc;
    private Animator animator;

    public int Health { get; set; }


    // Start is called before the first frame update
    public void Damage(int damage)
    {
        if (_playeranim.GetAnimationsStateInfo().IsName("Hit")) return;
        Health -= damage;
        _playeranim.Hit();
        if (Health <= 0)
        {
            Debug.Log("Player Has Died");
            Destroy(gameObject);
        }
    }


void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        //transform = GetComponent<Transform>();
        collider2D = GetComponent<Collider2D>();
        _playeranim = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _swordArc = transform.GetChild(1).GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        Health = 100;

    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;
        Movement();
        Attack();
        SprintPlayer();
        //Debug.Log(transform.position);
        Debug.DrawRay(transform.position, Vector2.down * max_depth, Color.green);


    }

    void Attack()
    {
        if (IsGrounded() && Input.GetMouseButtonDown(0))
        {
            //attack starts 

            _playeranim.Attacking();
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            //if(speed!=0)
            //startcoroutine(cooldown(0.7f));
        }
        //_playeranim.StopAttacking();
    }
    void SprintPlayer()
    {
        //Debug.Log("Player is moving with a speed : " + speed);
        if (IsGrounded() && Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 1.5f * _basespeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = _basespeed;
        }
    }
    IEnumerator CoolDown(float time)
    {
        var temp_speed = speed;
        speed = 0;
        yield return new WaitForSeconds(time);
        speed = temp_speed;
    }
    bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, max_depth, layerMask);
        if (raycastHit2D.collider != null)
        {
            if (resetJump)
                return true;
            //Debug.Log("Hitted : " + raycastHit2D.collider.name);
            return false;
        }
        else return false;

    }
    void FlipPlayer(float move)
    {
        if (move < 0)
        {
            _spriteRenderer.flipX = true;
            _swordArc.flipX = true;
            _swordArc.flipY = true;
            Vector3 newpos = _swordArc.transform.localPosition;
            newpos.x = -1.01f;
            _swordArc.transform.localPosition = newpos;
        }
        if (move > 0)
        {
            _spriteRenderer.flipX = false;
            _swordArc.flipX = false;
            _swordArc.flipY = false;
            Vector3 newpos = _swordArc.transform.localPosition;
            newpos.x = 1.01f;
            _swordArc.transform.localPosition = newpos;
        }
    }


    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, force);
            _playeranim.jumping(!IsGrounded());
            resetJump = false;
            StartCoroutine(ResetJumpNeeded());
        }
        _playeranim.jumping(!IsGrounded());
        rigidbody2D.velocity = new Vector2(move * speed, rigidbody2D.velocity.y);
        FlipPlayer(move);
        _playeranim.Move(move);

    }

    IEnumerator ResetJumpNeeded()
    {
        yield return new WaitForSeconds(0.1f);
        resetJump = true;
    }
}
