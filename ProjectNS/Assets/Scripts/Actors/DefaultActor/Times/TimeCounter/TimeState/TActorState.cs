using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TActorState{
    public Rigidbody2D Rb2D { get; set; }
    public float JumpHeight { get; set; }
    public float RotateYFloat { get; set; }

    public bool IsMoveAnimator { get; set; }

    // 시간 되돌리기
    public void ReverseTime(
        TimeState beforeTS,
        TimeState dbBeforeTS,
        float linear,
        GameObject target,
        Components components
        )
    {


        components.GetRigidBody2D().velocity =
            Vector2.up * Mathf.Lerp(dbBeforeTS.TActorSt.JumpHeight, beforeTS.TActorSt.JumpHeight, linear);

        components.GetAnimator().SetBool("isMove",
            beforeTS.TActorSt.IsMoveAnimator);

        target.transform.rotation = Quaternion.Euler(0, RotateYFloat, 0);

    }

    public void AttachEvent(TimeState timeState)
    {
        timeState.AttachReverseTime(ReverseTime);
    }

}
