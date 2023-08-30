using UnityEngine;

public class RunningState : BaseState
{

    public override void Construct()
    {
        motor.verticalVelocity = 0;
        
    }
    public override Vector3 ProcessMotion()
    {
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1.0f;
        m.z = motor.baseRunSpeed;

        return m; 
    }

    public override void Transition()
        
    {
        
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
        if (InputManager.Instance.SwipeUp && motor.isGrounded)
        {
            //change to jumping
            motor.ChangeState(GetComponent<JumpingState>());
        }
        if(!motor.isGrounded)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }

        if(InputManager.Instance.SwipeDown)
        {
            motor.ChangeState(GetComponent<SlidingState>());
        }
        if(Time.timeScale==0)
        {
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStatePause>());
        }
    }
}
