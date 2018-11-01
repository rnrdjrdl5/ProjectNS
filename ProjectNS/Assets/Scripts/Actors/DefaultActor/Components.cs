using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* ************************************
 * 
 *  - 모든 컴포넌트를 가지는 컴포넌트
 * 
 * - 그 외 기본적인 기능을 가진다.
 * 
 * ************************************/

public class Components : MonoBehaviour {

    ObjectManager objectManager;
    TimeManager timeManager;

    public enum EnumProperty { OBJECT, ACTOR };
    public EnumProperty[] Properties;

    private Animator Anim;
    public Animator GetAnimator()
    {
        if (Anim == null) return null;
        return Anim;
    }


    private TimeCounter timeCounter;
    public TimeCounter GetTimeCounter() {
        if (timeCounter == null) return null;
        return timeCounter;
    }

    private MoveState moveState;
    public MoveState GetMoveState() {
        if (moveState == null) return null;
        return moveState;
    }


    public PlayerController playerController { get; set; }
    public PlayerController GetPlayerController()
    {
        if (playerController == null) return null;
        return playerController;
    }

    public AIController aIController { get; set; }
    public AIController GetAIController()
    {
        if (aIController == null) return null;
        return aIController;
    }
    public AIController tempAIController { get; set; }

    private Rigidbody2D rb2D;
    public Rigidbody2D GetRigidBody2D()
    {
        if (rb2D == null) return null;
        return rb2D;
    }

    

    // Use this for initialization
    void Awake () {

        for (int i = 0; i < Properties.Length; i++)
        {
            switch(Properties[i])
            {
                case EnumProperty.ACTOR:
                    SetActorComponents();
                    break;

                case EnumProperty.OBJECT:
                    SetObjectComponent();
                    break;
            }
        }
    }

    private void Start()
    {
        objectManager = ObjectManager.GetInstance();
        timeManager = TimeManager.GetInstance();

        objectManager.Actors.Add(gameObject);
        objectManager.Componentss.Add(this);
    }






    void SetObjectComponent()
    {
        timeCounter = GetComponent<TimeCounter>();
        Anim = GetComponent<Animator>();
    }

    void SetActorComponents()
    {
        aIController = GetComponent<AIController>();
        tempAIController = aIController;

        rb2D = GetComponent<Rigidbody2D>();

        moveState = GetComponent<MoveState>();

        
    }
}
