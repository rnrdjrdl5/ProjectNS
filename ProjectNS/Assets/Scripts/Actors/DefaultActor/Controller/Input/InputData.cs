using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputData{



    public enum EnumInput { Z, X, C, V };
    public EnumInput InputType;

	public bool CheckInput()
    {
        bool isKeyDown = false;
        switch (InputType)
        {
            case EnumInput.Z:
                isKeyDown = Input.GetKeyDown(KeyCode.Z);
                break;

            case EnumInput.X:
                isKeyDown = Input.GetKeyDown(KeyCode.Z);
                break;

            case EnumInput.C:
                isKeyDown = Input.GetKeyDown(KeyCode.Z);
                break;

            case EnumInput.V:
                isKeyDown = Input.GetKeyDown(KeyCode.Z);
                break;
        }

        return isKeyDown;
    }
}
