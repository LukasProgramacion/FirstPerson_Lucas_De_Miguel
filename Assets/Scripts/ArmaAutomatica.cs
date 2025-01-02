using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class ArmaAutomatica : MonoBehaviour
{
    [SerializeField] private ParticleSystem system;

    [SerializeField] private ArmaSO misDatos;

    private Camera cam;

    private float timer;

    public Granada granada;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        timer = misDatos.cadenciaAtaque;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;

        if(Input.GetMouseButton(0) && timer >= misDatos.cadenciaAtaque)
        {
            system.Play();
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, misDatos.distanciaAtaque))
            {
                // te dice el nobmre de lo q has tocado
                if (hitInfo.transform.CompareTag("ParteEnemigo"))
                {
                    //Debug.Log(hitInfo.transform.name);
                    hitInfo.transform.GetComponent<ParteDeEnemigo>().RecibirDanho(misDatos.danhoAtaque);
                }

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

            timer = 0;
        }

    }
}
