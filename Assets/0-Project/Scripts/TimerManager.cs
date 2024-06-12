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
            // timerText.text = "Süre Bitti!";
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

        timerText.text = "Kalan Süre: " + "<br>" + string.Format("{0:00}:{1:00}", minutes, seconds);
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
           "Deprem simülasyonu tamamlandý! Ýþte sonuçlar:" + "<br>" + "Harika iþ çýkardýn! Sabitlediðin eþyalar, deprem sýrasýnda güvenle yerinde kaldý." + "<br>" +
           " Ancak, sabitlemediðin eþyalar sallandý ve etrafa uçuþtu." + "<br>" +
           "Her seferinde daha da iyisini yapabilirsin!Bu deneyimle, bir sonraki oyunda daha çok eþyayý sabitleyerek evimizi daha güvenli hale getirebilirsin." + "<br>" +
           "Unutma, bilgili olmak ve hazýrlýklý olmak, gerçek bir kahraman yapar!Þimdi tekrar denemeye hazýr mýsýn?";
        });
    }
}
