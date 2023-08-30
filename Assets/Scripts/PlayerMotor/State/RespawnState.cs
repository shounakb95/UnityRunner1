using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] private float verticalDistance = 25.0f;
    [SerializeField] private float immunityTime = 1f;


    private float startTime;
    public override void Construct()
    {

        startTime = Time.time;
        motor.controller.enabled = false;
        motor.transform.position = new Vector3(0,verticalDistance,motor.transform.position.z);
        motor.controller.enabled = true;

        motor.verticalVelocity = 0.0f;
        motor.currentLane = 0;
        motor.anim?.SetTrigger("Respawn");
        
       
    }

    public override void Destruct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Game);
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
        if (motor.isGrounded && Time.time-startTime>immunityTime)
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
        if (Time.timeScale == 0)
        {
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStatePause>());
        }
    }
}
