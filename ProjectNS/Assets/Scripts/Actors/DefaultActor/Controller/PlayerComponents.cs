using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/***********************************
 * 
 * 플레이어만 가지는 컴포넌트.
 * 
 * - 고유 입력을 처리함
 * 
 * 
 * 
 * **********************************/
public class PlayerComponents : MonoBehaviour {

    PlayerController playerController;

    
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();


    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
