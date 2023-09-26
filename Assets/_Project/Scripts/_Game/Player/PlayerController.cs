using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public Vector3 moveDirection;
    private Vector3 initMousePos;
    private Vector3 initPos;
    private Vector3 moveVector;

    public float xClamp;
    public float mouseSensitivity;
    public float movementSpeed;

    public bool isMoving;
    private float initPosY;

    private void Start()
    {
        initPosY = rb.position.y;

        moveDirection = Vector3.zero;
    }

    private void OnEnable()
    {
        EventSystem.OnStageEnter += Stop;
        EventSystem.OnStageExit += Continue;
        EventSystem.OnStartGame += OnGameStart;
    }

    private void OnDisable()
    {
        EventSystem.OnStageEnter -= Stop;
        EventSystem.OnStageExit -= Continue;
        EventSystem.OnStartGame -= OnGameStart;
    }

    void FixedUpdate()
    {
        if (!isMoving)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        rb.velocity = moveDirection;
        if (rb.position.y != initPosY)
        {
            rb.MovePosition(new Vector3(rb.position.x, initPosY, rb.position.z));
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

    private void OnGameStart()
    {
        DOVirtual.DelayedCall(0.5f, () =>
        {
            isMoving = true;
            moveDirection = new Vector3(0f, 0f, movementSpeed);
        });
    }
}
