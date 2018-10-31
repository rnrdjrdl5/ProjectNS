using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMove : MonoBehaviour {


    public float ActorSpeed;
    Components components;
    private void Start()
    {
        components = gameObject.GetComponent<Components>();
        components.GetMoveState().AttachMoveEvent(SimpleMove);
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void SimpleMove(float xPosition, float yPosition)
    {
        Vector2 vec2 = new Vector2(xPosition, yPosition);
        vec2 *= ActorSpeed;

        components.GetMoveState().AddMoveDir(vec2);
    }
}
