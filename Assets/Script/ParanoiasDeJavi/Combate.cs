using UnityEngine;
using System.Collections;

public class Combate : MonoBehaviour
{
    public float timeUntilAttack;
    public float countdownDuration;
    public float reactionTime;

    private string requiredKey;
    private bool waitingForInput = false;

    void Start()
    {
        GameManager.instance.modoAtaque = true;
        StartCoroutine(StartCombat());
    }

    IEnumerator StartCombat()
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeUntilAttack);
            yield return Countdown(); // Inicia la cuenta atrás

            // Determina aleatoriamente si la tecla a presionar es "Z" o "X"
            requiredKey = (Random.value > 0.5f) ? "z" : "x";
            Debug.Log("¡Presiona la tecla: " + requiredKey.ToUpper() + "!");

            waitingForInput = true;
            float timer = 0f;

            while (timer < reactionTime) // Espera la entrada del jugador
            {
                if (Input.GetKeyDown(requiredKey))
                {
                    Debug.Log("✅ ¡Correcto! Pulsaste la tecla correcta.");
                    waitingForInput = false;
                    break;
                }
                timer += Time.deltaTime;
                yield return null;
            }

            if (waitingForInput)
            {
                Debug.Log("❌ Fallaste. No presionaste la tecla correcta a tiempo.");
            }

            waitingForInput = false;
        }
    }

    IEnumerator Countdown()
    {
        float timer = countdownDuration;
        while (timer > 0)
        {
            Debug.Log("Cuenta atrás: " + Mathf.Ceil(timer));
            timer -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("¡GO!");
    }


}
