using UnityEngine;
using System.Collections;

public class CicloDeAndado : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float flipInterval;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FlipRoutine());
    }

    IEnumerator FlipRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(flipInterval);
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
