using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    
    [SerializeField] private float velocidadMovimiento;
    

    private CharacterController controller;


    private Camera cam;

    [Header("Movimiento")]
    [SerializeField] private float escalaGravedad;
    private Vector3 movimientoVertical;
    [SerializeField] private float alturaSalto;


    [Header ("Suelo")]
    [SerializeField] private Transform pies;
    [SerializeField] private float radioDeteccion;
    [SerializeField] private LayerMask queEsSuelo;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = Camera.main;

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //Vector3 movimiento = new Vector3(h, 0, v).normalized;

        Vector2 input = new Vector2(h, v).normalized;

        //cuerpo rota con camara
        transform.eulerAngles = new Vector3 (0, Camera.main.transform.eulerAngles.y, 0);

        //Si existe input...
        if (input.sqrMagnitude > 0) 
        {
            //Se calcula el ánguulo al que tengo que rotarme en función de los inputs y orientación de la camara.
            float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(0, anguloRotacion, 0);

            Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;
            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);
        }

        AplicarGravedad();
        DeteccionSuelo();
        
    }

    private void AplicarGravedad()
    {
        movimientoVertical.y += escalaGravedad * Time.deltaTime;
        controller.Move(movimientoVertical * Time.deltaTime);
    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movimientoVertical.y = Mathf.Sqrt(-2 * escalaGravedad * alturaSalto);
        }
    }

    private void DeteccionSuelo()
    {
        Collider[] collsDetetados = Physics.OverlapSphere(pies.position, radioDeteccion, queEsSuelo);

        if(collsDetetados.Length > 0)
        {
            movimientoVertical.y = 0;
            Saltar();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pies.position, radioDeteccion);
    }

}
