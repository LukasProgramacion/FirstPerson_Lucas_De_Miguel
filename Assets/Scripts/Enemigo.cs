using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    private NavMeshAgent agent;
    //[SerializeField] private GameObject player;

    private FirstPerson player;
    // Start is called before the first frame update

    private Animator anim;
    private Rigidbody[] huesos;

    private bool ventanaAbierta;

    [SerializeField] private float danhoAtaque;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radioAtaque;
    [SerializeField] private LayerMask queEsDanhable;
    private bool danhoRealizado;

    [SerializeField] private float vidas;

    public float Vidas { get => vidas; set => vidas = value; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindObjectOfType<FirstPerson>();

        anim = GetComponent<Animator>();

        huesos  = GetComponentsInChildren<Rigidbody>();

        CambiarEstadoHuesos(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Tengo que definir como destino la posicion del player
        agent.SetDestination(player.transform.position);
        Debug.Log(player.name);
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
            anim.SetBool("Atacking", true);
        }

        if (ventanaAbierta && danhoRealizado == false)
        {
            DetectarJugador();
        }
        
    }

    
    private void DetectarJugador()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(attackPoint.position, radioAtaque, queEsDanhable);  
        
        if (collsDetectados.Length > 0)
        {
            for (int i = 0; i < collsDetectados.Length; i++)
            {
                collsDetectados[i].GetComponent<FirstPerson>().RecibirDanho(danhoAtaque);
            }
            danhoRealizado = true;  
        }
    }
    
    

    public void Morir ()
    {
        agent.enabled = false;
        anim.enabled = false;
        CambiarEstadoHuesos(false);
        Destroy(gameObject, 10);
    }

    private void CambiarEstadoHuesos(bool estado)
    {
        for (int i = 0; i < huesos.Length; i++)
        {
            huesos[i].isKinematic = estado;
        }
    }

    //Evento de animacion
    private void FinAtaque()
    {
        agent.isStopped = false;
        anim.SetBool("Atacking", false);
        danhoRealizado = false;
        
    }

    
    private void AbrirVentanaAtaque()
    {
        ventanaAbierta = true;
    }

    private void CerrarVentanaAtaque()
    {
        ventanaAbierta = false;
    }
    
}
