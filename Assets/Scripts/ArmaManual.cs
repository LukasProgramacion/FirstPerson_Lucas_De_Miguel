using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaManual : MonoBehaviour
{
    [SerializeField] ArmaSO misDatos;
    [SerializeField] ParticleSystem system;

    private Camera cam;

    public Granada granada;
    // Start is called before the first frame update
    void Start()
    {
        //cam es la camara principal de la escena "MainCamera"
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            system.Play();
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, misDatos.distanciaAtaque))
            {
                // te dice el nobmre de lo q has tocado
                if (hitInfo.transform.CompareTag("ParteEnemigo"))
                {
                    //Debug.Log(hitInfo.transform.name);
                    hitInfo.transform.GetComponent<ParteDeEnemigo>().RecibirDanho(misDatos.danhoAtaque);
                }
                if (hitInfo.transform.CompareTag("Granada"))
                {
                    Granada granada = hitInfo.transform.GetComponent<Granada>();
                    if (granada != null)
                    {
                        granada.Explotar(); 
                        Destroy(granada.gameObject); 
                    }
                }

                
            }
        }
    }
}
