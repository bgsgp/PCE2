using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PieceView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int file;
    [SerializeField] private int rank;
    [SerializeField] private string displayName = "帅";
    [SerializeField] private Color pieceColor = new Color(0.82f, 0.18f, 0.16f, 1f);
    [SerializeField] private Sprite pieceSprite; // 新增：棋子图片
    [SerializeField] private bool isFlipped = false;

    public Action<PieceView> OnPieceFlipped;

    public int File => file;
    public int Rank => rank;
    public Color PieceColor => pieceColor;
    public bool IsFlipped => isFlipped;

    private RectTransform _rect;
    private Image _image;
    private Text _text;

    public void Setup(int targetFile, int targetRank, string nameText, Color color, Sprite sprite, bool flipped = false)
    {
        file = targetFile;
        rank = targetRank;
        displayName = nameText;
        pieceColor = color;
        pieceSprite = sprite; // 接收图片
        isFlipped = flipped;
        ApplyVisual();
    }

    public void PlaceOn(BoardView board)
    {
        _rect.SetParent(board.PieceRoot, false);
        _rect.anchoredPosition = board.GetPieceAnchoredPosition(file, rank);
        gameObject.name = $"棋子_{displayName}_{file}_{rank}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isFlipped) return;
        Flip();
    }

    public void Flip()
    {
        if (isFlipped) return;
        isFlipped = true;
        ApplyVisual();
        OnPieceFlipped?.Invoke(this);
    }

    private void Awake()
    {
        CacheComponents();
        ApplyVisual();
    }

    private void CacheComponents()
    {
        _rect = GetComponent<RectTransform>();
        if (_rect == null) _rect = gameObject.AddComponent<RectTransform>();

        _image = GetComponent<Image>();
        if (_image == null) _image = gameObject.AddComponent<Image>();

        _text = GetComponentInChildren<Text>();
        if (_text == null)
        {
            GameObject textObj = new GameObject("Text", typeof(RectTransform), typeof(Text));
            textObj.transform.SetParent(transform, false);
            _text = textObj.GetComponent<Text>();
            _text.alignment = TextAnchor.MiddleCenter;
            _text.fontSize = 36;
            _text.fontStyle = FontStyle.Bold;
            _text.raycastTarget = false;
        }
    }

    private void ApplyVisual()
    {
        CacheComponents();
        _rect.sizeDelta = new Vector2(64f, 64f); // 如果棋子图片太大或太小，改这里的 64
        _rect.pivot = new Vector2(0.5f, 0.5f);
        _image.raycastTarget = true;

        if (isFlipped)
        {
            if (pieceSprite != null)
            {
                // 有图片就用图片，隐藏文字
                _image.sprite = pieceSprite;
                _image.color = Color.white;
                _text.text = "";
            }
            else
            {
                // 没图片就退回色块+文字
                _image.sprite = null;
                _image.color = pieceColor;
                _text.text = displayName;
                _text.color = Color.white;
            }
        }
        else
        {
            // 暗棋背面
            _image.sprite = null;
            _image.color = new Color(0.2f, 0.2f, 0.2f, 1f);
            _text.text = "";
        }
    }
}