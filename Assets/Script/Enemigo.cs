using UnityEngine;

public enum EstadoEnemigo
{
    NoPerseguir, Perseguir
}
public class Enemigo : MonoBehaviour
{
    EstadoEnemigo eEnemy;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        GameManager.instance.modoAtaque2 += CambioQuieto;
        GameManager.instance.modoAtaque1 += CambioVolver;
    }
    private void CambioQuieto()
    {
        eEnemy = EstadoEnemigo.Perseguir;
        Debug.Log("Me Cambio a quieto");
    }
    private void CambioVolver()
    {
        if(eEnemy == EstadoEnemigo.NoPerseguir)
        {
            Debug.Log("Me Cambio no perseguir");
        }
        else if(eEnemy == EstadoEnemigo.Perseguir)
            {
            Debug.Log("Me Cambio perseguir");
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
