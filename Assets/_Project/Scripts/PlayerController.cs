using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public BoxCollider bottomCollider;

    public Vector3 moveDirection;
    private Vector3 initMousePos;
    private Vector3 initPos;
    private Vector3 moveVector;

    public float xClamp;
    public float mouseSensitivity;

    public bool isMoving;
    private float initPosY;

    private void Start()
    {
        isMoving = true;

        initPosY = rb.position.y;
    }

    //private void OnEnable()
    //{
    //    LetterBox.OnCheckComplete += Continue;

    //}

    //private void OnDisable()
    //{
    //    LetterBox.OnCheckComplete -= Continue;
    //}

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = moveDirection;
            if (rb.position.y != initPosY)
            {
                rb.MovePosition(new Vector3(rb.position.x, initPosY, rb.position.z));
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (Input.GetMouseButtonDown(0))
        {
            initMousePos = Input.mousePosition;
            initPos = rb.position;
        }

        else if (Input.GetMouseButton(0))
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 currentMousePos = Input.mousePosition;

        moveVector = (currentMousePos - initMousePos) * mouseSensitivity;

        float clampedX = Mathf.Clamp(initPos.x + moveVector.x, -xClamp, xClamp);

        rb.MovePosition(Vector3.Lerp(rb.position, new Vector3(clampedX, rb.position.y, rb.position.z), Time.fixedDeltaTime * 10f));
    }

    public void Stop()
    {
        isMoving = false;
    }

    public void Continue()
    {
        isMoving = true;
    }
}
