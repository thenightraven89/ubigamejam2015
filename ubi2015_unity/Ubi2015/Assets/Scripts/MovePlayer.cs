using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    public int playerID;
    public PlayerController controller;
    public float delta = 0.3f;
    private float x, y;

    private string horizName;
    private string verticalName;

    void OnEnable()
    {
        horizName = "Horizontal" + playerID.ToString();
        verticalName = "Vertical" + playerID.ToString();
    }

    void Update()
    {
        x = Input.GetAxis(horizName);
        y = Input.GetAxis(verticalName);

        Debug.Log(Input.GetJoystickNames().Length);

        if (Mathf.Abs(x) > delta)
        {
            if (x > 0)
                controller.MoveRight();
            else
                controller.MoveLeft();
        }   

        if (Mathf.Abs(y) > delta)
        {
            if (y > 0)
                controller.MoveDown();
            else
                controller.MoveUp();
        }     
    }
}
