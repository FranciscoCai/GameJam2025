using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class Acercamiento : MonoBehaviour
{
    public float detectionRange;
    public float fadeSpeed;
    public Camera playerCam;
    public GameObject battleCam;
    public Image image;


    private string enemyTag = "Enemigo";
    public GameObject target;
    [SerializeField] private DialogoCombate canvas;

    public AudioSource overworld;
    public AudioSource theme;

    void Update()
    {
        FindNearestEnemy();

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= detectionRange)
            {
                canvas.textoCompleto = target.GetComponent<Enemigo>().textoCompleto;
                StartCoroutine(FadeCoroutine());
                NpcManager.Instance.timeUntilAttack = target.GetComponent<Enemigo>().timeUntilAttack;
                NpcManager.Instance.countdownDuration = target.GetComponent<Enemigo>().countdownDuration;
                NpcManager.Instance.pumpDuration = target.GetComponent<Enemigo>().pumpDuration;
                NpcManager.Instance.textoCompleto = target.GetComponent<Enemigo>().textoCompleto;
                NpcManager.Instance.orSprite = target.GetComponent<Enemigo>().orSprite;
                NpcManager.Instance.newSprite = target.GetComponent<Enemigo>().newSprite;
                if(target.GetComponent<Enemigo>().Ataque != null)
                { AudioManager.Instances.Ataque = target.GetComponent<Enemigo>().Ataque; }
                if (target.GetComponent<Enemigo>().Dano != null)
                {
                    AudioManager.Instances.Dano = target.GetComponent<Enemigo>().Dano;
                }
                if (target.GetComponent<Enemigo>().Hablar != null)
                {
                    AudioManager.Instances.Hablar = target.GetComponent<Enemigo>().Hablar;
                }
                Destroy(target);
            }

        }
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float minDistance = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        target = closest;
    }
    private IEnumerator FadeCoroutine()
    {
        AudioManager.Instances.CrossfadeAudio(overworld, theme, 1.5f, 1.5f);
        if (GameManager.instance.modoAtaque2 != null)
        {
            GameManager.instance.modoAtaque2();
        }
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        while (image.color.a < 1f)
        {
            color.a += fadeSpeed * Time.deltaTime;
            image.color = color;
            yield return null;
        }

        color.a = 1f;
        image.color = color;

        if (battleCam != null)
        {
            battleCam.SetActive(true);
        }
        
        if (playerCam != null)
        {
            playerCam.enabled = false; ;
        }

        while (image.color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            image.color = color;
            yield return null;
        }

        color.a = 0f;
        image.color = color;
    }
}
