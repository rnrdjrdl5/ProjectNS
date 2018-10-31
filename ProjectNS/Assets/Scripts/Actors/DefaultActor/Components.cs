﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Components : MonoBehaviour {

    ActorManager actorManager;
    TimeManager timeManager;



    public enum EnumActor { PLAYER, OTHERS }
    public EnumActor ActorType;

    private TimeSkill timeSkill;
    public TimeSkill GetTimeReserver() {
        if (timeSkill == null) return null;
        return timeSkill;
    }

    private ActorState actorState;
    public ActorState GetActorState()
    {
        if (actorState == null) return null;
        return actorState;
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


    public PlayerController playerController;
    public PlayerController GetPlayerController()
    {
        if (playerController == null) return null;
        return playerController;
    }

    public AIController aIController;
    public AIController GetAIController()
    {
        if (aIController == null) return null;
        return aIController;
    }
    public AIController tempAIController;

    private Rigidbody2D rb2D;
    public Rigidbody2D GetRigidBody2D()
    {
        if (rb2D == null) return null;
        return rb2D;
    }

    

    // Use this for initialization
    void Awake () {

        DefaultComponents();

        switch (ActorType)
        {
            case EnumActor.PLAYER:
                SetPlayerComponenets();
                break;

            case EnumActor.OTHERS:
                break;
        }
    }

    private void Start()
    {
        actorManager = ActorManager.GetInstance();
        timeManager = TimeManager.GetInstance();

        actorManager.Actors.Add(gameObject);
        actorManager.Componentss.Add(this);
    }

    public void SetPlayerComponenets()
    {
        timeSkill = GetComponent<TimeSkill>();
    }

    public void DefaultComponents()
    {
        rb2D = GetComponent<Rigidbody2D>();

        moveState = GetComponent<MoveState>();
        playerController = GetComponent<PlayerController>();
        aIController = GetComponent<AIController>();
        timeCounter = GetComponent<TimeCounter>();
        actorState = GetComponent<ActorState>();
    }
}
