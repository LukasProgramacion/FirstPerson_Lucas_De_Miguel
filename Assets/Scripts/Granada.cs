using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float fuerzaImpulso;

    [SerializeField] private float tiempoVida;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float radioExplosion;

    [SerializeField] AudioClip sonidoExplosionGranada;

    [SerializeField] private LayerMask queEsDanhable;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * fuerzaImpulso, ForceMode.Impulse);
        Destroy(gameObject, tiempoVida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Explotar();
    }

    public void Explotar()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        AudioManager.Instance.EjecutarSonido(sonidoExplosionGranada);

        Collider[] collsDetectados = Physics.OverlapSphere(transform.position, radioExplosion, queEsDanhable);

        if (collsDetectados.Length > 0)
        {
            foreach (Collider coll in collsDetectados)
            {
                coll.GetComponent<ParteDeEnemigo>().Explotar();
                coll.GetComponent<Rigidbody>().isKinematic = false;
                coll.GetComponent<Rigidbody>().AddExplosionForce(80, transform.position, radioExplosion, 3.5f, ForceMode.Impulse);
            }
        }
    }

}


