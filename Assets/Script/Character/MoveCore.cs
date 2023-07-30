using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MoveCore : MonoBehaviour, IMoveable
{
    [SerializeField] protected float speed = 5;
    [SerializeField] float turnSpeed;
    [SerializeField] public GameObject mainCamera;
    [SerializeField] float turnTime;
    [SerializeField] public BoolReactiveProperty isTurn = new BoolReactiveProperty();

    public Vector3 direction; 
    private Vector3 dir; // 카메라 좌표계를 받은 벡터
    private Vector3 playerLookVector;

    public static float normalSpeed = 16f;
    public static float dashSpeed = 8f;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        direction = Vector3.back;
        mainCamera = Camera.main.gameObject;
    }

    protected virtual void FixedUpdate()
    {
        Move();
        Vector3 pointToLook = mainCamera.transform.position;
        playerLookVector = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
        transform.LookAt(playerLookVector);
    }

    public virtual void Move()
    {
        if (direction != Vector3.zero)
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }
        dir = direction;
        /*
        if (isTurn.Value)
        {
            this.speed = 12f;
            dir = direction;
        }
        else
        {
            this.speed = 5f;
            dir = direction.z * mainCamera.transform.forward.normalized;
            dir += direction.x * mainCamera.transform.right.normalized;
        }
        */
    }

    public virtual void SetDirection(Vector3 direction)
    {
        if (isTurn.Value) return;
        this.direction = direction;
    }

    public void Dash(float val)
    {
        this.speed = val;
    }

    public void Turn(float val)
    {
        // transform.Rotate(0f, -val * turnSpeed, 0f, Space.World);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    protected void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
