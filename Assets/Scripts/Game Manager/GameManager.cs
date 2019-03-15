using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool inventoryOpen;
    public GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);
        inventoryOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen)
            {
                inventory.SetActive(false);
                inventoryOpen = false;
            } else
            {
                inventory.SetActive(true);
                inventoryOpen = true;
            }
        }
    }
}
