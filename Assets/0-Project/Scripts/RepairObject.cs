using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairObject : MonoBehaviour
{
    public bool isRepaired;
    public int ObjectId;
    public Transform interactionObject;
    public int hammerCounter=0;

    public Outline outline;

    private void Awake()
    {
        outline = interactionObject.GetComponent<Outline>();
        outline.OutlineWidth = 3;
        hammerCounter = Random.Range(2, 7);
    }

    public void HammerAction() 
    {
        //cekic vuruldugunda cagirilacak fonksiyon
        hammerCounter--;
        if (hammerCounter <= 0)
        {
            isRepaired = true;
            outline.enabled = false;
            TopDownCharacterController.Instance.ClearRepairState();
        }
    }
}
