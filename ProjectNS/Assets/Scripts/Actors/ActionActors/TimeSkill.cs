using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSkill : MonoBehaviour {

    Components components;

    TimeCounter timeCounter;

    // Use this for initialization
    private void Awake()
    {
        components = GetComponent<Components>();
    }

    public void Start()
    {
        timeCounter = components.GetTimeCounter();
    }



    // Update is called once per frame
    void Update () {

        if (components.playerController == null) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseReverseTimer();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("플레이어 가속");
            TimeManager.GetInstance().PlayerSecondScale = 3;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("플레이어 제외 감속");
            TimeManager.GetInstance().ActorSecondScale = 0.3f;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("리셋");
            TimeManager.GetInstance().ActorSecondScale = 1.0f;
            TimeManager.GetInstance().PlayerSecondScale = 1.0f;
        }
    }

    void UseReverseTimer()
    {

        // 시간 정보 등록 못하도록
        //timeCounter.IsForwardTime = !timeCounter.IsForwardTime;

        ActorManager am = ActorManager.GetInstance();

        int count = am.Actors.Count;
        for (int i = 0; i < count; i++)
        {
            if (am.Actors[i] == null || am.Componentss[i] == null)
            {
                continue;
            }

            am.Componentss[i].GetTimeCounter().IsForwardTime = false;
        }

        // 모든 객체들은 움직이지 못한다. 시간을 거스를 수 없다.
    }
}
