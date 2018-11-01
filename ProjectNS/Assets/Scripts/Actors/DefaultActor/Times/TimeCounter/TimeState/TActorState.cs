using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TActorState{
    public Rigidbody2D Rb2D { get; set; }

    public float JumpHeight { get; set; }

    // 시간 되돌리기
    public void ReverseTime(
        TimeState beforeTS,
        TimeState dbBeforeTS,
        float linear,
        GameObject target,
        Components components
        )
    {

        Debug.Log("이벤트 수행3");

        components.GetRigidBody2D().velocity =
            Vector2.up * Mathf.Lerp(dbBeforeTS.TActorSt.JumpHeight, beforeTS.TActorSt.JumpHeight, linear);
    }

    public void AttachEvent(TimeState timeState)
    {
        timeState.AttachReverseTime(ReverseTime);
    }

}
