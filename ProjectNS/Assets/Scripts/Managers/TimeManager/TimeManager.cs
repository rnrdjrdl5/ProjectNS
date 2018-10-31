using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*****************************************************************
 * 
 * 시간을 관리하는 매니저, TimeManager
 * 
 *  - 유니티의 time.deltatime을 사용하지 않고 
 *    scale이 적용 가능한 나만의 second 를 사용
 *    
 *  - 가속, 감속을 사용하려면 scale을 조절해서 사용한다.
 *  
 *  - 이동 이나 프레임 처리를 사용하려면 Second를 곱해서 사용 
 *  
 *  - 플레이어와 다른액터들은 서로 시간대를 다르게 사용한다.
 *  
 *  ****************************************************************/
public class TimeManager : MonoBehaviour {

    static private TimeManager timeManager;
    static public TimeManager GetInstance() { return timeManager; }


    private float beforeRealTime;       // 이전 실제시간
    private float actorSecond;               // 실제 프레임
    public float GetActorSecond() { return actorSecond; }

    private float playerSecond;
    public float GetPlayerSecond() { return playerSecond; }


    public float ActorSecondScale { get; set; }          // 시간에 대한 배율, 슬로우와 가속을 정할 수 있음.
    public float PlayerSecondScale { get; set; }



    

    private void Awake()
    {
        timeManager = this;
        actorSecond = 0.0f;
        playerSecond = 0.0f;

        ActorSecondScale = 1.0f;
        PlayerSecondScale = 1.0f;
    }

    // Use this for initialization
    void Start () {

        actorSecond = 0.0f;
        beforeRealTime = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {

        /* Second = (Time.realtimeSinceStartup - beforeRealTime) * SecondScale;
         beforeRealTime = Time.realtimeSinceStartup;*/


        actorSecond = Time.deltaTime * ActorSecondScale;
        playerSecond = Time.deltaTime * PlayerSecondScale;
        
	}
}
