using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using System.Collections;

public class Acercamiento : MonoBehaviour
{
    public float detectionRange;
    public float fadeSpeed;
    public Camera playerCam;
    public Camera battleCam;
    public Image image;


    private string enemyTag = "Enemigo";
    public GameObject target;

    void Update()
    {
        FindNearestEnemy();

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= detectionRange)
            {
                StartCoroutine(FadeCoroutine());
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

        if (target != null)
        {
            Debug.Log("Enemigo más cercano: " + target.name + " a " + minDistance + " unidades.");
        }
    }
    private IEnumerator FadeCoroutine()
    {
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
            battleCam.enabled = true;
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
