using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public delegate void CambioModo();
    public CambioModo modoAtaque2;
    public CambioModo modoAtaque1;
    public bool modoAtaque;
    public Animator animator;
    public Animator idle;
    private bool puedeActivarx = true;
    private bool puedeActivary = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        StartCoroutine(Idle());
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("AtackMode", modoAtaque);
        if (modoAtaque)
        {
            if (Input.GetKeyDown(KeyCode.X) && puedeActivarx)
            {
                StartCoroutine(EsperarAnimacionx());
            }

            if (Input.GetKeyDown(KeyCode.Z) && puedeActivary)
            {
                StartCoroutine(EsperarAnimaciony());
            }
        }
    }

    IEnumerator EsperarAnimacionx()
    {
        puedeActivarx = false;
        animator.SetTrigger("x");
        yield return new WaitForSeconds(2f);

        puedeActivarx = true;
    }

    IEnumerator EsperarAnimaciony()
    {
        puedeActivary = false;
        animator.SetTrigger("z");
        yield return new WaitForSeconds(2f);

        puedeActivary = true;
    }

    IEnumerator Idle()
    {
        while (true)
        {
            float tiempoEspera = Random.Range(5f, 10f);
            yield return new WaitForSeconds(tiempoEspera);

            idle.SetTrigger("Idle");
        }
    }
}
