using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    public int playerID;
    public PlayerController controller;
    private float x, y;
    private bool xDisabled, yDisabled = false;
    
    private string horizName;
    private string verticalName;

    private KeyCode downKey;
    private KeyCode leftKey;    
    private KeyCode rightKey;
    private KeyCode upKey; 

    void OnEnable()
    {
        horizName = "Horizontal" + playerID.ToString();
        verticalName = "Vertical" + playerID.ToString();

        downKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + playerID + "Button0");
        leftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + playerID + "Button2");
        rightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + playerID + "Button1");
        upKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + playerID + "Button3");
    }

    void Update()
    {
        CheckJoysticks();
        CheckButtons();
    }

    void CheckButtons()
    {
        if (Input.GetKeyDown(downKey))
        {
            controller.MoveDown();
        }
        if (Input.GetKeyDown(leftKey))
        {
            controller.MoveLeft();
        }
        if (Input.GetKeyDown(rightKey))
        {
            controller.MoveRight();
        }
        if (Input.GetKeyDown(upKey))
        {
            controller.MoveUp();
        }
    }

    void CheckJoysticks()
    {
        x = Input.GetAxis(horizName);
        y = Input.GetAxis(verticalName);

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0)
            {
                controller.MoveRight();
            }
            else
            {
                controller.MoveLeft();
            }
        }
        else if (Mathf.Abs(x) < Mathf.Abs(y))
        {
            if (y > 0)
            {
                controller.MoveDown();
            }
            else
            {
                controller.MoveUp();
            }
        }  
    }
    
}
