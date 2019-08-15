using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public Animator anim;
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    #region Attack
    public void NormalAttack(int type, bool isAttack)
    {
        anim.SetBool("isAttack", true);
        anim.SetBool("Attack" + type, isAttack);
    }
    public void Skill(int type, bool isSkill)
    {
        anim.SetBool("isAttack", true);
        anim.SetBool("Skill" + type, isSkill);
    }
    public void Attack(bool isAttack)
    {
        anim.SetBool("isAttack", isAttack);
    }

    public bool isAttacking(int type)
    {
        return anim.GetCurrentAnimatorStateInfo(1).IsName("Attack" + type);
    }

    public void AnimationTime(int type)
    {
        anim.SetFloat("Attack" + type + "Time", anim.GetCurrentAnimatorStateInfo(1).normalizedTime);
    }

    #endregion

    #region Movement
    public void Movement(string type, float speed)
    {
        anim.SetFloat(type, speed);
    }
    public void Movement(string type, bool isAction)
    {
        anim.SetBool(type, isAction);
    }
    #endregion

}
