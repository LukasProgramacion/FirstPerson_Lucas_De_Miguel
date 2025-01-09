using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSonidoPistola;
    [SerializeField] AudioSource SFXSonidoM4;
    [SerializeField] AudioSource SFXGranada;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SonidoPistola (AudioClip clip)
    {
        SFXSonidoPistola.PlayOneShot(clip);
    }

    public void SonidoM4 (AudioClip clip)
    {
        SFXSonidoM4.PlayOneShot(clip);
    }
    public void SonidoGranada (AudioClip clip)
    {
        SFXGranada.PlayOneShot(clip);
    }
}
