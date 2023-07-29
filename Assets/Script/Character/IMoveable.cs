using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    public void SetDirection(Vector3 direction);
    public void Dash(float val);

    public void Turn();
}