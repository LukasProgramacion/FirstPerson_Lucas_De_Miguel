using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FirstPerson : MonoBehaviour
{

    [SerializeField] private float vidas; 
    
    [SerializeField] private float velocidadMovimiento;
    

    private CharacterController controller;

    [SerializeField] TMP_Text textoMunicion;
    [SerializeField] TMP_Text textoMunicionM4;
    [SerializeField] ArmaSO misDatos;
    [SerializeField] ArmaSO misDatosM4;

    [SerializeField] TMP_Text textoVida;

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
        misDatos.balasCargador = 10;
        misDatosM4.balasCargador = 60;

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
        transform.eulerAngles = new Vector3 (0, cam.transform.eulerAngles.y, 0);

        //Si existe input...
        if (input.sqrMagnitude > 0) 
        {
            //Se calcula el �nguulo al que tengo que rotarme en funci�n de los inputs y orientaci�n de la camara.
            float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(0, anguloRotacion, 0);

            Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;
            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);
        }

        AplicarGravedad();
        DeteccionSuelo();
        
        textoMunicion.SetText("Municion pistola: " + misDatos.balasCargador);
        textoMunicionM4.SetText("Municion m4: " + misDatosM4.balasCargador);
        textoVida.SetText("Vida: " + vidas);
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


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("ParteEnemigo"))
        {
            Rigidbody rbEnemigo = hit.gameObject.GetComponent<Rigidbody>();
            Vector3 direccionFuerza = hit.transform.position - transform.position;
            rbEnemigo.AddForce(direccionFuerza.normalized * 50, ForceMode.Impulse);
        }
    }

    public void RecibirDanho(float recibirDanho)
    {
        vidas -= recibirDanho;

        if(vidas <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Caja Final")
        {
            SceneManager.LoadScene(4);
        }
    }

}
