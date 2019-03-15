using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveableEnemy : Enemy
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("Movement speed of the enemey, in unit 1 unit length per second")]
    protected float m_speed = 5;
    #endregion
    
    #region Public Read-only Properties
    public bool IsIdle => !IsMoving;
    abstract public bool IsMoving { get; }
    #endregion

    #region Protected Methods
    protected virtual void move(Vector2 destination, float tolerance)
    {
        Vector2 dir = destination - (Vector2)transform.position;
        if (dir.magnitude < tolerance)
        {
            dir = Vector2.zero;
        }
        else
        {
            dir = dir.normalized;
        }
        transform.position = (Vector2)transform.position + dir * m_speed * Time.deltaTime;
    }
    #endregion
}
