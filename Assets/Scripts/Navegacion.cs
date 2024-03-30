using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Navegacion : MonoBehaviour
{
    public int scene;
    public AudioSource audiosource;
    public bool opciones = false;
    public KeyCode specialKey1;
    public KeyCode specialKey2;
    public GameObject options;


    void Update()
    {
        if (Input.GetKeyDown(specialKey1)) 
        {
            CargaEscena();
        }
        
        if (Input.GetKeyDown(specialKey2))
        {
            options.SetActive(true);
        }


    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CargaEscena()
    {
        SceneManager.LoadScene(scene);
    }

    public void Mute()
    {
        audiosource.mute = true;
    }

    public void PlayMusic()
    {
        audiosource.mute = false;
    }

    public void Opciones()
    {
        opciones = true;
    }

}
