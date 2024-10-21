using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;

    CharacterController controller;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //Vector3 movimiento = new Vector3(h, 0, v).normalized;

        Vector2 input = new Vector2(h, v).normalized;

        //Si existe input...
        if (input.sqrMagnitude > 0) 
        {
            //Se calcula el ánguulo al que tengo que rotarme en función de los inputs y orientación de la camara.
            float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(0, anguloRotacion, 0);

            Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;
            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);
        }
       

        
    }
}
