using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TimerManager : MonoBehaviour
{
    public float timer;
    public bool startTimer, gameEnded;


    public GameObject player;
    public GameObject cam1, cam2;

    public TextMeshProUGUI timerText, SkorText;
    public GameObject depremButonu;
    public CinemachineShake cinemachineShake;

    public GameObject SkorPaneli;

    private void Update()
    {
        if (gameEnded)
            return;
        if (startTimer && timer > 0)
        {
            timer -= Time.deltaTime;
            DisplayTime(timer);
        }
        if (timer <= 0)
        {
            gameEnded = true;
            TimerEnded();
            // timerText.text = "S�re Bitti!";
            timerText.gameObject.SetActive(false);
        }
    }
    public void TimerEnded()
    {
        player.SetActive(false);
        cam1.SetActive(false);
        cam2.SetActive(true);
        //if (RepairObjectManager.Instance.repairObjectCount <= 0) 
        //{

        //}

        DOVirtual.DelayedCall(2, () => depremButonu.SetActive(true));
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = "Kalan S�re: " + "<br>" + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void DepremiBaslat()
    {
        depremButonu.SetActive(false);
        cinemachineShake.ShakeCamera(4f, 5f);
        DOVirtual.DelayedCall(2.5f, () => RepairObjectManager.Instance.Deprem());
        DOVirtual.DelayedCall(5f, () =>
        {
            SkorPaneli.gameObject.SetActive(true);
            SkorText.gameObject.SetActive(true);
            SkorText.text = "Skorun: " + Mathf.FloorToInt(RepairObjectManager.Instance.SkorHesapla()) + "<br>" + "Tebrikler, Kahraman!" + "<br>" +
           "Deprem sim�lasyonu tamamland�! ��te sonu�lar:" + "<br>" + "Harika i� ��kard�n! Sabitledi�in e�yalar, deprem s�ras�nda g�venle yerinde kald�." + "<br>" +
           " Ancak, sabitlemedi�in e�yalar salland� ve etrafa u�u�tu." + "<br>" +
           "Her seferinde daha da iyisini yapabilirsin!Bu deneyimle, bir sonraki oyunda daha �ok e�yay� sabitleyerek evimizi daha g�venli hale getirebilirsin." + "<br>" +
           "Unutma, bilgili olmak ve haz�rl�kl� olmak, ger�ek bir kahraman yapar!�imdi tekrar denemeye haz�r m�s�n?";
        });
    }
}
