using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Slime : MoveableEnemy
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("Self bleeding health per second")]
    private float m_bleed = 5;
    #endregion

    #region Private Variables
    SlimeVision sv;
    #endregion

    #region Public API
    public override bool IsMoving => sv.stalking;
    public override bool IsAttacking => false;
    #endregion

    protected override void move(Vector2 destination, float tolerance)
    {
        base.move(destination, tolerance);
        // Reduces self health along the way
        ReceiveDamage(m_bleed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrHealth = MaxHealth;
        sv = GetComponentInChildren<SlimeVision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sv.stalking)
        {
            StartCoroutine(Stalk(sv.target));
        }
    }

    IEnumerator Stalk(GameObject o)
    {
        while (sv.stalking)
        {
            move(o.transform.position, 1.5f);
            yield return null;
        }
    }

}
