using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Basic Enemy Super Class
 */
public abstract class Enemy : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The hp of the object at its initialization")]
    private float m_maxHealth;

    [SerializeField]
    [Tooltip("The attack damage of the object at its initialization")]
    private float m_attackDamage;
    #endregion

    #region Protected Properties
    protected float CurrHealth { get; set; }
    #endregion

    #region Public API

    #region Public Read-only Properties
    public Vector2 Position => transform.position;
    public float MaxHealth => m_maxHealth;
    public float AttackDamage => m_attackDamage;
    abstract public bool IsAttacking { get; }
    #endregion

    #region Public Methods
    /**
     * Let Enemy object take damage 
     */
    virtual public void ReceiveDamage(float damage)
    {
        CurrHealth -= damage;
        if (CurrHealth <= 0)
        {
            Kill();
        }
    }

    /**
     * Bring Enemy object to death
     */
    virtual public void Kill()
    {
        Destroy(gameObject);
    }
    #endregion

    #endregion

   
}
