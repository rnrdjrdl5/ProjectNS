using System.Collections;
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
 *  - 컴포넌트 당 해당 컴포넌트의 변수, 타입 등을 기록하기 때문에
 *    기록 종류는 컴포넌트의 종류와 일치할 듯 함
 *    --> 즉, Components의 Properties 만큼 기록하면 된다
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

        rb2D = GetComponent<Rigidbody2D>();
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


    /***********************************************************************
     * 
     * - 등록할 때 Component 타입 내에서 여러 타입으로 나뉘는 것들은 
     *   Event로 대신 처리시킨다.
     *   
     * - 예시) Object별 Animator 내 변수.  각 변수 종류가 무궁무진하기 때문
     * 
     * ********************************************************************/

    public delegate void DeleNoPara();
    event DeleNoPara OtherRegisterEvent;
    public void AttachOtherRegisterEvent(DeleNoPara dor)
    {
        OtherRegisterEvent += dor;
    }

    // 시간과 관련된 정보를 등록한다.
    void RegisterTimeState()
    {

        // 상속을 사용해서 해결해본다?


        TimeState timeState = new TimeState();

        // 이동 관련 정보
        Vector2 vec2Pos = new Vector2(transform.position.x, transform.position.y);


        for (int i = 0; i < components.Properties.Length; i++)
        {
            switch (components.Properties[i])
            {
                case Components.EnumProperty.OBJECT:
                    TObjectState tos = new TObjectState
                    {
                        Position = vec2Pos,           
                        DeltaTime = Time.deltaTime,      
                        RealPlayTime = Time.realtimeSinceStartup 
                        
                    };
                    timeState.TObjectSt = tos;

                    tos.AttachEvent(timeState);
                    break;

                case Components.EnumProperty.ACTOR:
                    TActorState tas = new TActorState
                    {
                        JumpHeight = rb2D.velocity.y,
                        IsMoveAnimator = components.GetAnimator().GetBool("isMove"),
                        RotateYFloat = transform.rotation.eulerAngles.y
                    };
                    
                    timeState.TActorSt = tas;

                    tas.AttachEvent(timeState);
                    break;
            }
        }
        if(OtherRegisterEvent != null) OtherRegisterEvent();
        TimeStates.Add(timeState);
    }


     /***********************************************************************
     * 
     * - 체크도 Event로 대신 처리시킨다.
     *   
     * - 예시) Object별 Animator 내 변수.  각 변수 종류가 무궁무진하기 때문
     * 
     * ********************************************************************/
    event DeleNoPara OtherCheckTimeEvent;
    public void AttachOtherCheckTimeEvent(DeleNoPara dnp)
    {
        OtherCheckTimeEvent = dnp;
    }

    // 최대 저장가능한 정보를 판단하고 삭제한다
    void CheckTime()
    {
        // 현재시간의 offset보다 벌어진 경우
        int count = TimeStates.Count;

        for (int i = count-1; i >=0; i--)
        {

            // 이전으로는 다 버려버린다.
            if (Time.realtimeSinceStartup - maxTimeStates > TimeStates[i].TObjectSt.RealPlayTime)
            {
                for (int j = i; j >= 0; j--)
                    TimeStates.RemoveAt(j);

                break;
            }
        }

        if(OtherCheckTimeEvent!= null) OtherCheckTimeEvent();
    }



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
     *  
     *  - 등록 범위가 무궁무진한 Animator 변수같은 경우는
     *  
     *  - Event로 알아서 처리하라고 해야한다.
     *  *********************************************************/

    event DeleNoPara OtherReverseTimeEvent;
    public void AttachOtherReverseTimeEvent(DeleNoPara dnp)
    {
        OtherReverseTimeEvent = dnp;
    }
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



        // loop를 돌며 적정 시간대를 찾아낸다
        while (true)
        {

            // 값이 크면 삭제하면서 다음 위치로
            if (beforeTS.TObjectSt.DeltaTime < deltaTime)
            {

                deltaTime -= beforeTS.TObjectSt.DeltaTime;

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
            else if (beforeTS.TObjectSt.DeltaTime >= deltaTime)
            {

                // 보간 사용할 시간 지정
                deltaTime = beforeTS.TObjectSt.DeltaTime - deltaTime;
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




        // Linear 구하기
        float beforeDeltaTime = beforeTS.TObjectSt.DeltaTime;
        float linear = 0.0f;

        if (deltaTime > 0)
        {
            linear = deltaTime / beforeDeltaTime;
        }



        /**********************************************************
         * 
         * 시간 정보에 Event를 위임해서 사용한다.
         * 
         *  - 많은 State들은 각자 Event를 가진다.
         *  
         *  - 해당 State 원소에는 각 오브젝트 속성 메서드가 붙어있다. 
         *  
         * **********************************************************/
        beforeTS.CallReverseTimeEvent(beforeTS, dbBeforeTS, linear, gameObject, components);

        if(OtherReverseTimeEvent!=null) OtherReverseTimeEvent();

        // 사용 후 지금 위치의 이벤트 삭제
        TimeStates.RemoveAt(count - 1);
    }

    void CancelReverse()
    {
        IsForwardTime = true;
    }
}
