using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/***********************************************************************
 * AI 컨트롤러 담당
 * 
 *  - 모든 액터는 AI컨트롤러를 가지고 있다.
 *  
 *  - 액터 중 Player컨트롤러가 붙은 오브젝트는 AI의 영향을 받지 않는다.
 *  
 *  *********************************************************************/

public class AIController : MonoBehaviour {


    Components components;

    public void AttachAIController()
    {
        components = transform.root.gameObject.GetComponent<Components>();

        components.aIController = this;
    }

    public void ReleasePlayerController()
    {
        if (components == null) return;

        components.aIController = null;
    }

    
    private void Start()
    {
        components = GetComponent<Components>();

        if (components == null) return;

        components.aIController = this;
        components.tempAIController = this;
    }
}
