using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    /*
     * this feels like a really crappy way of implementing hitboxes and attacks
     * TODO rework with raycasting (?) when not ded
     */

    [HideInInspector]
    public bool damageApplied;
    [HideInInspector]
    public float damageValue;

    // Start is called before the first frame update
    void Start()
    {
        damageApplied = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if (!damageApplied && other.CompareTag("Enemy"))
        {
            damageApplied = true;
            other.GetComponent<Enemy>().ReceiveDamage(damageValue);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(other.transform.position.x - transform.position.x, other.transform.position.y - transform.position.y).normalized, ForceMode2D.Impulse);
            Debug.Log("hit enemy");
        }
    }

}

