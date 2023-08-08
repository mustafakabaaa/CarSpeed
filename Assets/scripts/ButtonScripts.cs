using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour
{
    
   
  
    public void TekrarBaslaFNC()
    {
        SceneManager.LoadScene(1);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
    public void MenuFNC()
    {
        SceneManager.LoadScene(0);
    }
    public void CikisFNC()
    {
        Application.Quit(); 
    }

}
