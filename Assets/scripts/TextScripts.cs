using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScripts : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public TMP_Text Hiz;
    float skorlar;
    float hiz;

    public TMP_Text enYuksekSkorText; // Inspector üzerinden TextMeshPro nesnesini sürükleyip býrakýn

    void Start()
    {
        //int enYuksekSkor = MainCar.GetHighScore(); // En yüksek skoru al
        //enYuksekSkorText.text = enYuksekSkor.ToString(); // TextMeshPro nesnesine ata
    
        if (textMeshPro == null)
        {
            int enyuksekSkor = MainCar.GetHighScore();
            enYuksekSkorText.text=BasaSifiEkleme(MainCar.GetHighScore(),5);
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
}
