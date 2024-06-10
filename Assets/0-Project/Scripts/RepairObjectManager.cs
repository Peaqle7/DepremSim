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
}
