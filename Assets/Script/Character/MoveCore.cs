using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCore : MonoBehaviour, IMoveable
{
    [SerializeField] protected float speed = 5;
    [SerializeField] public Transform mainCamera;

    protected Vector3 direction;
    private Vector3 dir; // 카메라 좌표계를 받은 벡터


    public static float normalSpeed = 5f;
    public static float dashSpeed = 8f;

    protected virtual void Awake()
    {

    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        dir = direction.z * mainCamera.forward.normalized;
        dir += direction.x * mainCamera.right.normalized;

        if (direction != Vector3.zero)
        {
            transform.Translate(dir * speed * Time.deltaTime);
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
}
