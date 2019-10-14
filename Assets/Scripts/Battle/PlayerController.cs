using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float MaxMoveSpeed;
    public float MoveSpeedIncrement;
    private float CurrentMoveSpeed;

    public float MaxTurnSpeed;
    public float TurnSpeedIncrement;
    private float CurrentTurnSpeed;

    private Rigidbody BoatRb;
    private bool idle;
    private bool leftTurning;
    private bool rightTurning;

    void Start()
    {
        BoatRb = GetComponent<Rigidbody>();

        idle = true;
        leftTurning = false;
        rightTurning = false;

        CurrentMoveSpeed = 0;
        CurrentTurnSpeed = 0;
    }

    private void FixedUpdate()
    {
        HandleMoving();
        if (Input.GetKeyDown(KeyCode.W))
        {
            idle = false;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            idle = true;
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            rightTurning = true;
            leftTurning = false;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            rightTurning = false;
            leftTurning = true;
        }
    }

    void HandleMoving()
    {
        if (!idle)
        {
            Move();
        }
        else
        {
            Stopping();
        }

        if (leftTurning)
        {
            TurnLeft();
        }
        else if (rightTurning)
        {
            TurnRight();
        }
    }

    public void Moving()
    {
        if (idle)
        {
            idle = false;
        }
        else
        {
            idle = true;
        }
    }

    public void Idle()
    {
        leftTurning = false;
        rightTurning = false;

        CurrentTurnSpeed = 0;
    }

    public void TurningLeft()
    {
        leftTurning = true;
        rightTurning = false;
    }

    public void TurningRight()
    {
        leftTurning = false;
        rightTurning = true;
    }

    private void Move()
    {
        Vector3 movement = transform.forward * CurrentMoveSpeed * Time.deltaTime;
        BoatRb.MovePosition(BoatRb.position + movement);

        if (CurrentMoveSpeed < MaxMoveSpeed)
        {
            CurrentMoveSpeed += MoveSpeedIncrement;
        }
    }

    private void Stopping()
    {
        Vector3 movement = transform.forward * CurrentMoveSpeed * Time.deltaTime;
        BoatRb.MovePosition(BoatRb.position + movement);

        if (CurrentMoveSpeed > 0)
        {
            CurrentMoveSpeed -= 0.03f;
        }
    }

    private void Slowing()
    {
        Vector3 movement = transform.forward * CurrentMoveSpeed * Time.deltaTime;
        BoatRb.MovePosition(BoatRb.position + movement);

        if (CurrentMoveSpeed > 0)
        {
            CurrentMoveSpeed -= 3.0f;
        }
    }

    private void TurnLeft()
    {
        float turn = -CurrentTurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        BoatRb.MoveRotation(BoatRb.rotation * turnRotation);

        if (CurrentTurnSpeed < MaxTurnSpeed)
        {
            CurrentTurnSpeed += TurnSpeedIncrement;
        }
    }

    private void TurnRight()
    {
        float turn = CurrentTurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        BoatRb.MoveRotation(BoatRb.rotation * turnRotation);

        if (CurrentTurnSpeed < MaxTurnSpeed)
        {
            CurrentTurnSpeed += TurnSpeedIncrement;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet")
        {
            Slowing();
        }

    }

}
