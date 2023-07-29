using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MoveCore : MonoBehaviour, IMoveable
{
    [SerializeField] protected float speed = 5;
    [SerializeField] public GameObject mainCamera;
    [SerializeField] float turnTime;
    [SerializeField] public BoolReactiveProperty isTurn = new BoolReactiveProperty();

    public Vector3 direction; 
    private Vector3 dir; // 카메라 좌표계를 받은 벡터


    public static float normalSpeed = 5f;
    public static float dashSpeed = 8f;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        direction = Vector3.forward;
        mainCamera = Camera.main.gameObject;
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        if (direction != Vector3.zero)
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }

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
    }

    public virtual void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void Dash(float val)
    {
        this.speed = val;
    }

    public virtual void Turn()
    {

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
