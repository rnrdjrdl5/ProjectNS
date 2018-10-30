using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
 * 
 * 이동 컴포넌트
 * 
 * - MoveState 컴포넌트가 모든 이동을 합쳐 수행한다. 
 * 
 * - 이 컴포넌트에서는 AI 이동이나 플레이어 조작이동을 다룬다
 * 
 * *************************************************/

public class MoveAction : MonoBehaviour {

    Components components;

    MoveState moveState;

    public void Start()
    {
        components = GetComponent<Components>();
        moveState = components.GetMoveState();
        if (moveState != null) moveState.AttachMoveEvent(MoveEvent);
    }


    // MoveState에서 발생하는 이벤트
    public void MoveEvent()
    {
        Vector2 moveDir = Vector2.zero;

        

        /******************************************
         * 
         * MoveState에 Vector2를 전달한다.
         * 
         * AI 와 플레이어 로 구분지어서 사용한다.
         * 
         * ****************************************/
        
        if (components.playerController != null)
        {
            moveDir = PlayerMove();
        }

        else if (components.aIController != null)
        {
            moveDir = AIMove();
        }

        moveState.AddMoveDir(moveDir);
    }

    public Vector2 PlayerMove()
    {
        float right = Input.GetAxisRaw("Horizontal");
        float up = Input.GetAxisRaw("Vertical");


        return new Vector2(right, up);
    }

    // 더미, 추후 AI 사용 시 AI 컴포넌트로 빼야 할 것
    public Vector2 AIMove()
    {
        return Vector2.up;
    }

}
