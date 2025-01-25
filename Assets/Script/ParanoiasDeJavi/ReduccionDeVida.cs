using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReduccionDeVida : MonoBehaviour
{
    public Image targetImage;
    public float shrinkSpeed;

    private RectTransform imageRect;

    void Start()
    {
        imageRect = targetImage.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (GameManager.instance.modoAtaque)
        {
            float newWidth = imageRect.sizeDelta.x - (shrinkSpeed * Time.deltaTime);
            newWidth = Mathf.Max(newWidth, 0);

            imageRect.sizeDelta = new Vector2(newWidth, imageRect.sizeDelta.y);
        }
    }
}
