using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/***********************************************
 * 
 * 플레이어 컨트롤러
 * 
 *  - 특수한 컨트롤러에 컴포넌트로 존재함
 *  
 *  - 컨트롤러는 유저 수만큼 존재 ( 싱글 ) 
 *  
 *  - 컨트롤러를 붙이면 플레이어가 된다. 
 *  **********************************************/

public class PlayerController : MonoBehaviour {

    private void Start()
    {
        isUseAttach = false;
    }

    public Components components { get; set; }
   



    public void AttachPlayerController()
    {
        
        components = transform.parent.GetComponent<Components>();
        if (components == null) return;

        components.playerController = this;
        components.aIController = null;
    }

    public void ReleasePlayerController()
    {
        if (components == null) return;

        components.playerController = null;
        components.aIController = components.tempAIController;
    }

    bool isUseAttach;       // 초기값 false 설정

    // 더미, 붙였다 뗐다
    public void Update()
    {


        if (Input.GetKeyDown(KeyCode.Z))
        {
            AttachPlayerController();
            isUseAttach = true;

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ReleasePlayerController();
            isUseAttach = false;
        }


        if (isUseAttach)
        {

           
            if (components == null)
            {
                isUseAttach = false;
                return;
            }


            // 키입력 받기
            float xPosition = Input.GetAxisRaw("Horizontal");
            //float yPosition = Input.GetAxisRaw("Vertical");

            components.GetMoveState().UpdateMove(xPosition,0);


            if (Input.GetKeyDown(KeyCode.Q))
            {
                components.GetTimeCounter().IsForwardTime = false;
            }
        }
    }


}
