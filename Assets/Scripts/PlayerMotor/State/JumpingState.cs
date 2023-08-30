
using UnityEngine;

public class JumpingState : BaseState
{

    public float jumpForce = 7.0f;
    public override void Construct()
    {
        motor.verticalVelocity = jumpForce;
        motor.anim.SetTrigger("Jump");
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
        if (motor.verticalVelocity < 0)
            motor.ChangeState(GetComponent<FallingState>());
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
