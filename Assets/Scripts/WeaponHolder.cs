using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] armas;

    int indiceArmaActual = 0;
    // Start is called before the first frame update
    void Start()
    {
        

    }
    private void Update()
    {
        CambiarArmaConRaton();
        CambiarArmaConTeclado();
    }

    // Update is called once per frame
    private void CambiarArmaConRaton()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if(scrollWheel > 0 ) // Anterior
        {
            CambiarArma(indiceArmaActual - 1);
        }
        else if(scrollWheel < 0 ) // Siguiente
        {
            CambiarArma(indiceArmaActual + 1);
        }
    }
    
    private void CambiarArmaConTeclado()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CambiarArma(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CambiarArma(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CambiarArma(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CambiarArma(3);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            CambiarArma(4);
        }
    }

    private void CambiarArma(int nuevoIndice)
    {
        

        //solo si es un indice valido puedo cambiar de arma
        if(nuevoIndice >= 0 && nuevoIndice < armas.Length)
        {
            armas[indiceArmaActual].SetActive(false);
            indiceArmaActual = nuevoIndice;
            armas[indiceArmaActual].SetActive(true);
        }
        
    }
}
