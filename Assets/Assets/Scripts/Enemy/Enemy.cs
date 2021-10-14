using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour,IDamageable
{
    private Vector3  target;
    [SerializeField]
    protected int health=100;
    [SerializeField]
    protected float moveSpeed=1.0f;
    [SerializeField]
    protected int gems=10;
    [SerializeField]
    protected Transform pointA, pointB;
    protected Animator _anim;
    protected SpriteRenderer spriteRenderer;
    protected bool reverse = false;
    protected bool isHit = false;
    Transform Player;
    public int Health { get; set; }
    public void Damage(int damageAmount)
    {
        _anim.SetTrigger("Hit");
        _anim.SetBool("InCombat", true);
        Health -= damageAmount;
        Debug.Log(gameObject.name + " Took a damage of:" + damageAmount);
        isHit = true;
        if (Health <= 0)
        {
            Debug.Log(gameObject.name + " is dead ");
            Destroy(gameObject);
        }
    }
    protected abstract void Attack();
    protected virtual void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = pointB.position;
        Debug.Log(spriteRenderer.name);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected virtual void Update() {
        isHit = _anim.GetBool("InCombat");
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")&&_anim.GetBool("InCombat")== false) return;
        Walk();
        
        if (_anim.GetBool("InCombat") == true) {
            float direction = Player.localPosition.x - transform.localPosition.x;
            if (direction < 0) { spriteRenderer.flipX = true; }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }
    protected virtual void Walk() {
        spriteRenderer.flipX = reverse;
        if (transform.position == pointA.position)
        {
            _anim.SetTrigger("Idle");
            reverse = false;
            target = pointB.position;
        }
        if (transform.position == pointB.position)
        {
            _anim.SetTrigger("Idle");
            reverse = true;
            target = pointA.position;
        }
        var speed = moveSpeed * Time.deltaTime;
        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }
        if (isHit)
        {
            _anim.SetBool("InCombat", true);
            Debug.Log(Vector2.Distance(Player.position, transform.position));
            if (Vector2.Distance(Player.position, transform.position) > 2) {
                _anim.SetBool("InCombat", false);
                isHit = false;
            }
        }


    }
}

