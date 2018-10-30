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

    Components components;

    public void AttachPlayerController()
    {
        components = transform.parent.GetComponent<Components>();

        components.playerController = this;
        components.aIController = null;
    }

    public void ReleasePlayerController()
    {
        if (components == null) return;

        components.playerController = null;
        components.aIController = components.tempAIController;
    }


    // 더미, 붙였다 뗐다
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            AttachPlayerController();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ReleasePlayerController();
        }
    }


}
