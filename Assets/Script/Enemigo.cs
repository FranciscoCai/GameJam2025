using UnityEngine;
using UnityEngine.AI;

public enum EstadoEnemigo
{
    NoPerseguir, Perseguir
}
public class Enemigo : MonoBehaviour
{
    public float timeUntilAttack;
    public float countdownDuration;
    public float pumpDuration;
    EstadoEnemigo eEnemy;
    private NavMeshAgent _agent;
    public Transform _player;
    private Animator _animator;
    public string[] textoCompleto;
    [HideInInspector]public Sprite orSprite;
    public Sprite newSprite;
    public AudioSource soudtrack;

    void Start()
    {
        orSprite = GetComponent<SpriteRenderer>().sprite;
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player").transform;
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.modoAtaque2 += CambioQuieto;
            GameManager.instance.modoAtaque1 += CambioVolver;
        }
    }
    private void OnDisable()
    {
        GameManager.instance.modoAtaque2 -= CambioQuieto;
        GameManager.instance.modoAtaque1 -= CambioVolver;
    }
    private void CambioQuieto()
    {
        eEnemy = EstadoEnemigo.Perseguir;
        _animator.SetTrigger("ToStop");
    }
    private void CambioVolver()
    {
        if(eEnemy == EstadoEnemigo.NoPerseguir)
        {
            _animator.SetTrigger("ToWaiting");
        }
        else if(eEnemy == EstadoEnemigo.Perseguir)
            {
            _animator.SetTrigger("ToMove");
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
