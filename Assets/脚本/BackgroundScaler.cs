using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackgroundScaler : MonoBehaviour
{
    private RectTransform _rect;
    private Image _image;

    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        AdjustSize();
    }

    void Update()
    {
        if (_rect != null && (Mathf.Abs(_rect.rect.width - Screen.width) > 1 || Mathf.Abs(_rect.rect.height - Screen.height) > 1))
        {
            AdjustSize();
        }
    }

    private void AdjustSize()
    {
        if (_image.sprite == null) return;

        float screenRatio = (float)Screen.width / Screen.height;
        float imageRatio = (float)_image.sprite.texture.width / _image.sprite.texture.height;

        if (screenRatio > imageRatio)
        {
            _rect.sizeDelta = new Vector2(Screen.width, Screen.width / imageRatio);
        }
        else
        {
            _rect.sizeDelta = new Vector2(Screen.height * imageRatio, Screen.height);
        }
        _rect.anchoredPosition = Vector2.zero; // 强制居中
    }
}