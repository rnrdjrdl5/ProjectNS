using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/****************************************************
 * 
 * 오브젝트 매니저
 * 
 *  - 모든 오브젝트들의 gameObject와 componenets를 가진다.
 *  
 *  - 시간 되돌리기의 모든 대상을 사용하기 위해 필요
 *  
 *  ***************************************************/

public class ObjectManager : MonoBehaviour {

    static private ObjectManager actorManager;
    static public ObjectManager GetInstance()
    {
        if (actorManager == null) return null;
        return actorManager;
    }
    
    
    public List<GameObject> Actors;
    public List<Components> Componentss;

    // Use this for initialization
    private void Awake()
    {
        Actors = new List<GameObject>();
        Componentss = new List<Components>();

        actorManager = this;
    }
}
