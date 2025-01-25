using UnityEngine;
using UnityEngine.AI;

public enum EstadoEnemigo
{
    NoPerseguir, Perseguir
}
public class Enemigo : MonoBehaviour
{
    EstadoEnemigo eEnemy;
    private NavMeshAgent _agent;
    public Transform _player;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player").transform;
        
    }
    private void OnEnable()
    {
        GameManager.instance.modoAtaque2 += CambioQuieto;
        GameManager.instance.modoAtaque1 += CambioVolver;
    }
    private void OnDisable()
    {
        GameManager.instance.modoAtaque2 -= CambioQuieto;
        GameManager.instance.modoAtaque1 -= CambioVolver;
    }
    private void CambioQuieto()
    {
        eEnemy = EstadoEnemigo.Perseguir;
        gameObject.SetActive(false);
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
