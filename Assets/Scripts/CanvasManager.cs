using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CargarLore()
    {
        SceneManager.LoadScene(2);
    }

    public void TerminarJuego()
    {
        Application.Quit();
    }

    public void CargarJuego()
    {
        SceneManager.LoadScene(0);
    }
    public void MenuPrinicpal()
    {
        SceneManager.LoadScene(1);
    }
}
