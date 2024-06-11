using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Transform tickObject;
    private void Awake()
    {
        Instance = this;
    }

    public void GetTick(Vector3 tickPos)
    {
        tickObject.transform.position = tickPos;
        tickObject.DOScale(Vector3.one, .2f).From(Vector3.zero).SetEase(Ease.OutBack);
        DOVirtual.DelayedCall(.5f, () =>
        {
            tickObject.DOScale(Vector3.zero, .1f).From(Vector3.one);
        });
    }
    public void OutTick(Vector3 tickPos)
    {
        tickObject.transform.position = tickPos;
        tickObject.DOScale(Vector3.zero, .1f).From(Vector3.one);
    }
}
