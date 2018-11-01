using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/**************************************
 * 
 * Frame마다 오브젝트의 정보를 가진다.
 * 
 * 
 * ************************************/
public class TimeState{

    // 오브젝트 속성들
    public TObjectState TObjectSt { get; set; }
    public TActorState TActorSt { get; set; }




    /* **********************************************************
     *  
     *  - 모든 State들은 해당 이벤트에 붙어야 한다.
     *  
     *  - 해당 이벤트는 시간 역행 사용 시 불러진다.
     *  
     *  - 이벤트에 붙은 State들의 각 시간역행들이 사용되어진다. 
     *  
     * **********************************************************/
    public delegate void DeleReverseTime
    (
        TimeState beforeTS,
        TimeState dbBeforeTS,
        float linear,
        GameObject target,
        Components components
    );

    private event DeleReverseTime reverseTimeEvent;
    public void AttachReverseTime(DeleReverseTime drt) { reverseTimeEvent += drt; }

    public void CallReverseTimeEvent(
        TimeState beforeTS, 
        TimeState dbBeforeTS, 
        float linear,
        GameObject gameObject,
        Components components)
    {


        if (reverseTimeEvent != null)
        {

            Debug.Log("이벤트 수행1");
            reverseTimeEvent(
                beforeTS,
                dbBeforeTS,
                linear,
                gameObject,
                components);
        }
    }
}
