using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaInteracciones : MonoBehaviour
{
    private Camera cam;

    [SerializeField] ArmaSO misDatos;
    [SerializeField] ArmaSO misDatosM4;

    [SerializeField] private float distanciaInteraccion;
    private Transform interactuableActual;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, distanciaInteraccion))
        {
            if(hit.transform.TryGetComponent(out CajaMunicion scriptCaja))
            {
                //Activar outline de la caja.
                interactuableActual = hit.transform;
                interactuableActual.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    scriptCaja.Abrir();
                    misDatos.balasCargador+= 15;
                    misDatosM4.balasCargador += 30;

                }
            }
            
        }
        else if (interactuableActual)//Si tenia un interactuable
        {
            //Lo apago
            interactuableActual.GetComponent<Outline>().enabled = false;
            //Lo anulo
            interactuableActual = null;
        }

    }
}
