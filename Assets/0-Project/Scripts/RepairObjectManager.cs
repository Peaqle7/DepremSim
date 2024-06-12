using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairObjectManager : MonoBehaviour
{
    public List<RepairObject> repairObjects = new List<RepairObject>();
    public List<Rigidbody> homeObjects = new List<Rigidbody>();
    public int counter = 1;
    public static RepairObjectManager Instance;
    public int repairObjectCount;
    private void Awake()
    {
        Instance = this;
        foreach (var item in repairObjects)
        {
            item.ObjectId = counter;
            counter++;
        }
        repairObjectCount = counter;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Deprem();
        }
    }
    public void DetectColliders(RepairObject current)
    {
        foreach (var item in repairObjects)
        {
            if (current != item)
            {
                item.GetComponent<Collider>().enabled = false;
                item.GetComponent<Collider>().enabled = true;
            }
        }
    }
    public void Deprem()
    {
        foreach (var item in repairObjects)
        {
            if (!item.isRepaired)
            {
                item.interactionObject.gameObject.AddComponent<Rigidbody>().AddForce(new Vector3(0, 13, Random.Range(0, 6)), ForceMode.Impulse);
            }
        }
        foreach (var item in homeObjects)
        {
            item.AddForce(new Vector3(0, 6, Random.Range(0, 6)), ForceMode.Impulse);
        }
    }
    public float SkorHesapla()
    {
        float skor;
        if (repairObjectCount == 0)
        {
            skor = 100;
        }
        else
        {
            skor = Mathf.FloorToInt((counter - repairObjectCount) * 3.5f);
        }

        return skor;
    }
}
