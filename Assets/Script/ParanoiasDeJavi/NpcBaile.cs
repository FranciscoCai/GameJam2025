using UnityEngine;
using System.Collections;

public class NpcBaile : MonoBehaviour
{
    public float timeUntilAttack;
    public float countdownDuration;
    public float pumpDuration;
    public float actionDuration;
    public float pumpScaleFactor;
    private Sprite orSprite;
    public Sprite newSprite;

    private Vector3 originalScale;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();

        orSprite = spriteRenderer.sprite;

    }
    private void OnEnable()
    {
        timeUntilAttack = NpcManager.Instance.timeUntilAttack;
        countdownDuration = NpcManager.Instance.countdownDuration;
        pumpDuration = NpcManager.Instance.pumpDuration;
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        while (true)
        {
            spriteRenderer.sprite = orSprite;
            yield return new WaitForSeconds(timeUntilAttack);
            for (int i = 3; i > 0; i--)
            {
                StartCoroutine(PumpEffect());
                yield return new WaitForSeconds(countdownDuration / 3f);
            }

            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            yield return new WaitForSeconds(pumpDuration);
        }
    }

    IEnumerator PumpEffect()
    {
        transform.localScale = originalScale * pumpScaleFactor;
        yield return new WaitForSeconds(actionDuration);
        transform.localScale = originalScale;
    }
}
