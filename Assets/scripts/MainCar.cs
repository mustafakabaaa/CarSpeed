using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class MainCar : MonoBehaviour
{
    public AudioSource[] butunSesler;

    public delegate void GamePauseEvent();
    public static event GamePauseEvent OnGamePause;

    public delegate void GameResumeEvent();
    public static event GameResumeEvent OnGameResume;

    public float dikeyHareket = 5;
    public float varsayilanHareket = 250;

    [Range(0f, 3f)]
    public float yatay_hiz = 10f;

    public float hizArtisMiktari = 0.1f;
    public TMP_Text textMeshPro;
    public int skor1 = 0;

    private const string HighScoreKey = "HighScore";


    public int SkoraG�reLevel = 0;
    public bool levelGecildiMi=false;
    public bool isCrashed = false;

    public delegate void CarCrashEvent();
    public static event CarCrashEvent OnCarCrash;

    [SerializeField] private AudioSource crashSource;
    Rigidbody2D rigidbody2;

    public TextMeshProUGUI skorTXT;
    public TextMeshProUGUI hizTXT;
    public GameObject PausePanel;

   

    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        PausePanel.SetActive(false);
        //startTime = Time.time;
        //audioSource.Play();
    }

    void FixedUpdate()
    {
        HareketFNC();
       

    }
    void Update()
    {
        Skor();
    }
    //Kaza yap�nca 2. sahneyi y�kle
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("CarNPC") && !isCrashed)
        {

            isCrashed = true;
            KazaSesiFNC();

            if (OnCarCrash != null)
            {
                OnCarCrash();
                StartCoroutine(WaitAndLoadScene(0.5f)); // 2 saniye bekle ve sahne y�kle


            }

        }

    }
    //En yuksek skoru kaydetme
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public static void SetHighScore(int skor)
    {

        int currentHighScore = GetHighScore();
        if (skor > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, skor);
            PlayerPrefs.Save();
        }
    }


    //2. sahmeyi y�klemeden �nce kaza efekti i�in bekle.
    private IEnumerator WaitAndLoadScene(float waitTime)
    {
        print("WaitAndLoadScene Coroutine started");
        yield return new WaitForSeconds(waitTime);
        print("WaitAndLoadScene Coroutine finished");
        SceneManager.LoadScene(2);

    }
    
    public void oyunudurdur()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        
        if(OnGamePause != null)
        {
            OnGamePause();
        }
    }
    
    //ses kapatma buttonu
    public void SesiDurdurFNC()
    {
        foreach (AudioSource audioSource  in butunSesler)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            
        }
    }
    //Ses acma button
    public void sesiBaslatFNC()
    {
        foreach(AudioSource audioSource in butunSesler)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
        }
    }
    public void OyunaDevamFNC()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;

        if (OnGameResume != null)
        {
            OnGameResume();
        }
    }
    private void HareketFNC()
    {
        Vector2 input1 = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));

        float xVelocity = input1.x * yatay_hiz;

        // Sa� s�n�ra ula��ld���nda veya sola hareket ederken s�n�ra ula��ld���nda hareketi tersine �evir
        if ((transform.position.x > 1.6f && xVelocity > 0) || (transform.position.x < -1.5f && xVelocity < 0))
        {
            xVelocity = -xVelocity;
        }

        rigidbody2.velocity = new Vector3(xVelocity, varsayilanHareket + dikeyHareket * Time.deltaTime);
        HizFNC();
        ZorlukFNC();
    }

   

    private void Skor()
    {
        int enYuksekSkor=GetHighScore();

        float y�kseklik = transform.position.y;

        // Y�kseklik de�erine g�re skor belirleme
        if (y�kseklik >= 0)
        {
            // Y�kseklik de�eri artt�k�a skoru artt�r
            skor1 = (int) Mathf.Floor(y�kseklik / 10) ;
        }
        else
        {
            // Y�kseklik de�eri s�f�rdan k���k oldu�unda skoru s�f�rla
            skor1 = 0;
        }
        // E�er anl�k skor en y�ksek skoru ge�erse, en y�ksek skoru g�ncelle
        if (skor1 > enYuksekSkor)
        {
            enYuksekSkor=(int) Mathf.Floor(skor1) ;
            SetHighScore(enYuksekSkor);
        }
        // Skoru ekrana yazd�rma
        if (textMeshPro != null)
        {
            int skorYaz=Convert.ToInt32(skor1);
            skorTXT.text = BasaSifiEkleme(skorYaz, 5);
        }
    }
    private void HizFNC()
    {
        float hiz = 20*varsayilanHareket;
        int hizYaz=Convert.ToInt32(hiz);
        hizTXT.text = BasaSifiEkleme(hizYaz,3);
    }
    private void ZorlukFNC()
    {
        // Skor her 5 artt���nda varsay�lanHareket de�erine h�zArtisMiktari kadar ekleyece�iz.
        int skorInt = Convert.ToInt32(skor1);

        if (skorInt % 3 == 0 && !levelGecildiMi)
        {
            levelGecildiMi = true; // levelGecildiMi bayra��n� true olarak ayarla.
            varsayilanHareket += hizArtisMiktari;
        }

        // levelGecildiMi bayra�� false oldu�unda tekrar 5'in kat� oldu�unda h�z� artt�rmak i�in haz�r oluruz.
        if (skorInt % 3 != 0)
        {
            levelGecildiMi = false;
        }

    }
    string BasaSifiEkleme(int skor, int rakamSayisi)
    {
        string skorSTR = skor.ToString();
        while (skorSTR.Length < rakamSayisi)
        {
            skorSTR = "0" + skorSTR;
        }
        return skorSTR;
    }
    public void KazaSesiFNC()
    {
        crashSource.Play();
    }
}
