using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Editor Variables
    /*
     * precondition: Enemy is a Monobehavior child class
     */
    [SerializeField]
    [Tooltip("The Prefabs of enemies being spawned")]
    private Enemy[] enemies;

    [SerializeField]
    [Tooltip("the radius vector indicating the range of spawning")]
    private Vector2 spawnRange = new Vector2(5, 0);

    [SerializeField]
    [Tooltip("time interval between enemies spawn")]
    private float[] spawnCD;
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < enemies.Length; ++i)
           StartCoroutine(spawn(i));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Spawn the i-th enemy at a random location in range spawnRange
     */
    private IEnumerator spawn(int i)
    {
        while (true)
        {
            float degree = Random.Range(0f, 360f);
            float length = Random.Range(0f, 1f);
            Instantiate(enemies[i], gameObject.transform.position + Quaternion.Euler(0, 0, degree) * spawnRange * length, Quaternion.identity);
            yield return new WaitForSeconds(spawnCD[i]);
        }
    }
}
