using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TObjectState {

    public Vector2 Position { get; set; }
    public float DeltaTime { get; set; }
    public float RealPlayTime { get; set; }





    // 시간 되돌리기
    public void ReverseTime(
        TimeState beforeTS,
        TimeState dbBeforeTS, 
        float linear,
        GameObject target,
        Components components
        )
    {
        Debug.Log("이벤트 수행2");
        Vector2 vec2 = Vector2.Lerp(
            dbBeforeTS.TObjectSt.Position, 
            beforeTS.TObjectSt.Position, 
            linear);

        target.transform.position = vec2;
    }

    public void AttachEvent(TimeState timeState)
    {
        timeState.AttachReverseTime(ReverseTime);
    }
}
