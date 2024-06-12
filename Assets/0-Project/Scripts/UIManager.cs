using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Transform tickObject;
    public bool lastObjectTick;
    public GameObject panel1, panel2, MainPanel;
    public int panelCounter;
    public TimerManager timerManager;
    private void Awake()
    {
        Instance = this;
    }

    public void GetTick(Vector3 tickPos)
    {
        if (!lastObjectTick)
        {
            lastObjectTick = true;
            tickObject.transform.position = tickPos;
            tickObject.DOScale(Vector3.one, .2f).From(Vector3.zero).SetEase(Ease.OutBack);
            DOVirtual.DelayedCall(.5f, () =>
            {
                tickObject.DOScale(Vector3.zero, .1f).From(Vector3.one);
            }).OnComplete(() => lastObjectTick = false);
        }
    }
    public void OutTick(Vector3 tickPos)
    {
        tickObject.transform.position = tickPos;
        tickObject.DOScale(Vector3.zero, .1f).From(Vector3.one);
    }
    public void PanelManager()
    {
        if (panelCounter == 0)
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            panelCounter++;
        }
        else if (panelCounter == 1)
        {
            panel2.SetActive(false);
            MainPanel.SetActive(false);
            panelCounter++;
            timerManager.startTimer = true;
        }
       
    }
    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
}
