using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家走路动画播放设置
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimatorControl : MonoBehaviour
{
    public RuntimeAnimatorController[] twoSides;
    private Animator _animator;
   
    public enum AnimationName
    {
        WALK,
        WALKHARD,
        TRAILWIND,
        IDLE,
        HOLD,
        PUTDOWN,
        NONE = -1
    }

    public AnimationName currentAnimation;
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        SetAnimation(AnimationName.TRAILWIND);
    //    }
    //    if (Input.GetKeyUp(KeyCode.A))
    //    {
    //        SetAnimation(AnimationName.WALKHARD);
    //    }
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        SetAnimation(AnimationName.WALK);
    //    }
    //    if (Input.GetKeyUp(KeyCode.B))
    //    {
    //        SetAnimation(AnimationName.IDLE);
    //    }
    //}

    public void SetAnimation(AnimationName animationName)
    {
        _animator = GetComponent<Animator>();
        switch (animationName)
        {
            case AnimationName.WALK:
                _animator.SetTrigger("walk");
                break;
            case AnimationName.WALKHARD:
                _animator.SetTrigger("walkhard");
                break;
            case AnimationName.TRAILWIND:
                _animator.SetTrigger("trailwind");
                break;
            case AnimationName.IDLE:
                _animator.SetTrigger("idle");
                break;
        }
        currentAnimation = animationName;
    }
    public void SetAnimation(AnimationName animationName,float time)
    {
        _animator = GetComponent<Animator>();
        switch (animationName)
        {
            case AnimationName.WALK:
                _animator.CrossFade("Walk", time, 0);
                break;
            case AnimationName.WALKHARD:
                _animator.CrossFade("WalkHard",time,0);
                break;
            case AnimationName.TRAILWIND:
                _animator.CrossFade("TrailWindWalk",time,0);
                break;
            case AnimationName.IDLE:
                _animator.CrossFade("Idle",time,0);
                break;
        }
        currentAnimation = animationName;
    }

    public void StopAnimator()
    {
        _animator.enabled = false;
    }

    public void PartialAnimationInit(AnimationName animationName)
    {
        _animator = GetComponent<Animator>();
        switch (animationName)
        {
            case AnimationName.HOLD:
                _animator.SetBool("hold", true);
                break;
            case AnimationName.PUTDOWN:
                _animator.SetBool("hold", false); ;
                break;
        }
    }

    public void PartialAnimation(AnimationName animationName)
    {
        _animator = GetComponent<Animator>();
        switch (animationName)
        {
            case AnimationName.HOLD:
                _animator.SetBool("hold", true);
                _animator.CrossFade("Hold", 0, 1);
                break;
            case AnimationName.PUTDOWN:
                _animator.SetBool("hold", false);
                _animator.CrossFade("Empty", 0,1); 
                break;
        }
    }
   
    public Animator GetAnimator()
    {
        return _animator;
    }

    public void SetAnimator(int index)
    {
        _animator = GetComponent<Animator>();
        if (index < 0) return;
        _animator.runtimeAnimatorController = twoSides[index];
    }
}
