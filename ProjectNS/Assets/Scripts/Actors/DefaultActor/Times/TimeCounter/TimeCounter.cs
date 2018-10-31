﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/* **********************************************************
 *
 * Time Counter 
 * 
 *  - 오브젝트의 모든 정보를 기록
 *  
 *  - 프레임 당 위치, 애니메이션 정보, 체력 등
 *  
 *  - 시간 되돌리기 사용 시 기록 정보를 이용해서 되돌린다
 *  
 *  **********************************************************/

public class TimeCounter : MonoBehaviour {
        
    private TimeManager timeManager;        // 매니저
    private List<TimeState> TimeStates;           // 시간 당 액터들 정보

    public float maxTimeStates = 1;         // 액터가 정보를 저장 가능한 최대 시간

    private Components components;
    private Rigidbody2D rb2D;

    void Start () {

        IsForwardTime = true;
        timeManager = TimeManager.GetInstance();
        TimeStates = new List<TimeState>();

        components = GetComponent<Components>();
        rb2D = components.GetRigidBody2D();
    }

    public bool IsForwardTime { get; set; }       // 시간의 흐름을 나타냄, true : 나아가는 방향  false : 반대 방향
    void Update () {


        // 기록 가능여부 확인
        if (IsForwardTime)
        {

            // 시간과 관련된 정보를 등록한다.
            RegisterTimeState();

            // 현재 시간과 비교해서 삭제할 리스트를 삭제한다.
            CheckTime();
        }

        // 시간을 다시 되돌린다.
        else if (!IsForwardTime)
        {
            ReverseTime();
        }

    }

    // 시간과 관련된 정보를 등록한다.
    void RegisterTimeState()
    {

        TimeState timeState = new TimeState();

        // 이동 관련 정보
        Vector2 vec2Pos = new Vector2(transform.position.x, transform.position.y);

        timeState.Position = vec2Pos;           //위치정보를 Vector2로 저장
        timeState.DeltaTime = Time.deltaTime;       // 정보 등록 당시 deltaTime
        timeState.RealPlayTime = Time.realtimeSinceStartup; // 정보 등록 당시 realTime
        timeState.JumpHeight = rb2D.velocity.y;     // 중력정보를 저장
        


        TimeStates.Add(timeState);
    }


    // 최대 저장가능한 정보를 판단하고 삭제한다
    void CheckTime()
    {
        // 현재시간의 offset보다 벌어진 경우
        int count = TimeStates.Count;

        for (int i = count-1; i >=0; i--)
        {

            // 이전으로는 다 버려버린다.
            if (Time.realtimeSinceStartup - maxTimeStates > TimeStates[i].RealPlayTime)
            {
                for (int j = i; j >= 0; j--)
                    TimeStates.RemoveAt(j);

                break;
            }
        }
    }

    public delegate void DeleReverseMove(TimeState beforeTS, TimeState dbBeforeTS, float linearData);
    private DeleReverseMove reverseMoveEvent;
    public void AttachReverseMove(DeleReverseMove erm) { reverseMoveEvent = erm; }

    /**********************************************************
     * 
     * 시간 되돌리기
     * 
     *  - 되돌린 시간의 DeltaTime과 현재 DeltaTIme을 비교하면서
     *    되돌린 시간대를 구한다
     *    
     *  - 되돌린 시간대를 구하고 나서는 
     *    보간을 사용해서 위치를 정해준다.
     *  
     *  *********************************************************/
    void ReverseTime()
    {

        int count = TimeStates.Count;

        // 돌아갈 이벤트가 없으면 취소
        if (count <= 0)
        {
            CancelReverse();
            return;
        }

        float deltaTime = Time.deltaTime;

        TimeState beforeTS = TimeStates[count - 1];



        // loop를 돌며 시간대를 찾아낸다
        while (true)
        {

            // 값이 크면 삭제하면서 다음 위치로
            if (beforeTS.DeltaTime < deltaTime)
            {

                deltaTime -= beforeTS.DeltaTime;

                if (count <= 1)
                {
                    count = 1;
                    beforeTS = TimeStates[0];
                    break;
                }

                TimeStates.RemoveAt(count - 1);     // 삭제
                count--;        // 다음으로 이동
                beforeTS = TimeStates[count - 1];       // 다음 지정
            }


            // 해당 위치를 찾았을 때 
            else if (beforeTS.DeltaTime >= deltaTime)
            {

                // 보간 사용할 시간 지정
                deltaTime = beforeTS.DeltaTime - deltaTime;
                if (deltaTime < 0) deltaTime = 0;
                break;
            }
        }

        // 시간대의 이전 시간대를 구해서 보간에 사용
        TimeState dbBeforeTS;

        if (count <= 1)
        {
            dbBeforeTS = beforeTS;
        }

        else
        {
            dbBeforeTS = TimeStates[count - 2];
        }


        // 각종 이벤트 사용
        if(reverseMoveEvent!= null) reverseMoveEvent(beforeTS, dbBeforeTS, deltaTime);


        // 사용 후 지금 위치의 이벤트 삭제
        TimeStates.RemoveAt(count - 1);
    }

    void CancelReverse()
    {
        IsForwardTime = true;
    }
}
