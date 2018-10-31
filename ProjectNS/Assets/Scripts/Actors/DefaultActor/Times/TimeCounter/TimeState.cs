using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeState{

    public Vector2 Position { get; set; }
    public float DeltaTime { get; set; }
    public float RealPlayTime { get; set; }

    public float JumpHeight { get; set; }
}
