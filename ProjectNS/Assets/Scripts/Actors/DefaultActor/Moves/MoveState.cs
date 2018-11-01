using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/******************************************************
 * 
 * MoveState
 * 
 * - 이동과 관련된 모든 정보를 수집해서 사용
 * 
 * - 키입력 뿐만이 아닌 외부요소로 인한 이동정보도 수집
 * 
 * - 주의사항 : TimeCounter 에서 사용하는 시간되돌리기
 *   기능을 Event로 여기서 처리한다. 
 *   
 *   *****************************************************/

public class MoveState : MonoBehaviour {

    TimeManager timeManager;

    Components components;
    TimeCounter timeCounter;


	// Use this for initialization
	void Start () {

        timeManager = TimeManager.GetInstance();
        components = GetComponent<Components>();

        timeCounter = components.GetTimeCounter();
        if (timeCounter != null) timeCounter.AttachReverseMove(ReverseMove);

    }
	

    private Vector2 moveDir;
    public void AddMoveDir(Vector2 vec2)
    {
        moveDir += vec2;
    }


    public delegate void DeleMove(float xPosition, float yPosition);
    event DeleMove MoveEvent;
    public void AttachMoveEvent(DeleMove dm) { MoveEvent += dm; }
    public void ReleaseMoveEvent(DeleMove dm) { MoveEvent -= dm; }


    public delegate void DeleEnvMove();
    event DeleEnvMove EnvMoveEvent;
    public void AttachEnvMoveEvent(DeleEnvMove dem) { EnvMoveEvent += dem; }
    public void ReleaseEnvMoveEvent(DeleEnvMove dem) { EnvMoveEvent -= dem; }

    public void UpdateMove(float xPosition, float yPosition)
    {

        /* ****************************************************************
         * 
         * 시간 되돌리기가 아닐 때 움직일 수 있다.
         * 
         *  - 단, 특정 대상은 움직일 수 있도록 바꿔야 한다면 수정이 필요하다. 
         *  
         *  ****************************************************************/

        if (timeCounter.IsForwardTime)
        {

            // 각 이벤트를 사용
            moveDir = Vector2.zero;
            if (MoveEvent != null) MoveEvent(xPosition, yPosition);

            if (EnvMoveEvent != null) EnvMoveEvent();



            // 이벤트 사용 후 정보를 토대로 이동
            Vector3 vec3 = new Vector3(moveDir.x, moveDir.y, transform.position.z);

            // 특정 대상을 구분짓고 싶다면 여기서 설정

            if (components.playerController != null)
            {
                transform.Translate(vec3 * timeManager.GetPlayerSecond(), Space.World);
            }

            else if(components.playerController == null)
            {
                transform.Translate(vec3 * timeManager.GetObjectSecond(), Space.World);
            }




        }
    }
}
