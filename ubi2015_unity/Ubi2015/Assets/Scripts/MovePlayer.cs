using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    public int playerID;
    public PlayerController controller;
    private float x, y;
    private bool xDisabled, yDisabled = false;
    
    private string horizName;
    private string verticalName;

    void OnEnable()
    {
        horizName = "Horizontal" + playerID.ToString();
        verticalName = "Vertical" + playerID.ToString();
    }

    void Update()
    {
        CheckJoysticks();
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
