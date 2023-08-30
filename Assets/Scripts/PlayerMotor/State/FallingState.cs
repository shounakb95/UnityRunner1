
using UnityEngine;

public class FallingState : BaseState
{

    public override void Construct()
    {
        motor.anim.SetTrigger("Fall");
    }
    public override Vector3 ProcessMotion()
    {


        //create gravity
        motor.ApplyGravity();
        //create our return vector
        Vector3 m = Vector3.zero;
        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.baseRunSpeed;

        return m;
    }

    public override void Transition()
    {
        if (motor.isGrounded)
            motor.ChangeState(GetComponent<RunningState>());
        if (InputManager.Instance.SwipeLeft)
        {
            //change lane go left
            motor.ChangeLane(-1);
        }

        if (InputManager.Instance.SwipeRight)
        {
            //change lane go right
            motor.ChangeLane(1);
        }
    }
}
