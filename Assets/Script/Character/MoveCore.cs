using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCore : MonoBehaviour, IMoveable
{
    [SerializeField] protected float speed = 5;
    [SerializeField] protected Vector3 direction;


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
        if (direction != Vector3.zero)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {

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
