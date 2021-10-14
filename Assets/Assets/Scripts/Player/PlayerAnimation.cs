using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    [SerializeField]
    private Animator _swordanim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public void jumping(bool jump) {
        _anim.SetBool("Jumping", jump);

    }
    public void StopAttacking() {
        _anim.ResetTrigger("Attacking");
    }
    public void Attacking() {
        _anim.SetTrigger("Attacking");
        _swordanim.SetTrigger("Attacking");

    }
    public void Move(float move)
    {
        _anim.SetFloat("MoveSpeed", Mathf.Abs(move));
        //set move to movespeed from animator
    }
    public void Hit()
    {
        _anim.SetTrigger("Hit");
    }
    public  AnimatorStateInfo GetAnimationsStateInfo() {
        return _anim.GetCurrentAnimatorStateInfo(0);
    }
}
