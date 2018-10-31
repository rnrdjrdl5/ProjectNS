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
    private float objectSecond;               // 액터들 사용 배율
    public float GetObjectSecond() { return objectSecond; }

    private float playerSecond;     // 플레이어 사용 배율
    public float GetPlayerSecond() { return playerSecond; }


    public float ObjectSecondScale { get; set; }          // 일반 사용 배율
    public float PlayerSecondScale { get; set; }        // 플레이어 사용 배율



    

    private void Awake()
    {
        timeManager = this;
        objectSecond = 0.0f;
        playerSecond = 0.0f;

        ObjectSecondScale = 1.0f;
        PlayerSecondScale = 1.0f;
    }

    // Use this for initialization
    void Start () {

        objectSecond = 0.0f;
        beforeRealTime = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {

        /* Second = (Time.realtimeSinceStartup - beforeRealTime) * SecondScale;
         beforeRealTime = Time.realtimeSinceStartup;*/


        objectSecond = Time.deltaTime * ObjectSecondScale;
        playerSecond = Time.deltaTime * PlayerSecondScale;
        
	}
}
