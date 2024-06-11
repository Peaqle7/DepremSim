using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairObjectManager : MonoBehaviour
{
    public List<RepairObject> repairObjects = new List<RepairObject>();
    public int counter = 1;
    private void Awake()
    {
        foreach (var item in repairObjects)
        {
            item.ObjectId = counter;
            counter++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            foreach (var item in repairObjects)
            {
                if (!item.isRepaired) 
                {
                    item.interactionObject.gameObject.AddComponent<Rigidbody>().AddForce(new Vector3(0, 13, Random.Range(0, 6)), ForceMode.Impulse);
                }
            }
        }
    }
}
