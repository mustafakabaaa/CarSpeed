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


    public int SkoraGöreLevel = 0;
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
    //Kaza yapýnca 2. sahneyi yükle
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("CarNPC") && !isCrashed)
        {

            isCrashed = true;
            KazaSesiFNC();

            if (OnCarCrash != null)
            {
                OnCarCrash();
                StartCoroutine(WaitAndLoadScene(0.5f)); // 2 saniye bekle ve sahne yükle


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


    //2. sahmeyi yüklemeden önce kaza efekti için bekle.
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

        // Sað sýnýra ulaþýldýðýnda veya sola hareket ederken sýnýra ulaþýldýðýnda hareketi tersine çevir
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

        float yükseklik = transform.position.y;

        // Yükseklik deðerine göre skor belirleme
        if (yükseklik >= 0)
        {
            // Yükseklik deðeri arttýkça skoru arttýr
            skor1 = (int) Mathf.Floor(yükseklik / 10) ;
        }
        else
        {
            // Yükseklik deðeri sýfýrdan küçük olduðunda skoru sýfýrla
            skor1 = 0;
        }
        // Eðer anlýk skor en yüksek skoru geçerse, en yüksek skoru güncelle
        if (skor1 > enYuksekSkor)
        {
            enYuksekSkor=(int) Mathf.Floor(skor1) ;
            SetHighScore(enYuksekSkor);
        }
        // Skoru ekrana yazdýrma
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
        // Skor her 5 arttýðýnda varsayýlanHareket deðerine hýzArtisMiktari kadar ekleyeceðiz.
        int skorInt = Convert.ToInt32(skor1);

        if (skorInt % 3 == 0 && !levelGecildiMi)
        {
            levelGecildiMi = true; // levelGecildiMi bayraðýný true olarak ayarla.
            varsayilanHareket += hizArtisMiktari;
        }

        // levelGecildiMi bayraðý false olduðunda tekrar 5'in katý olduðunda hýzý arttýrmak için hazýr oluruz.
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
