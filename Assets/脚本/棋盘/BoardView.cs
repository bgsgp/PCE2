using UnityEngine;

public class BoardView : MonoBehaviour
{
    [Header("棋盘尺寸")]
    [SerializeField] private float cellSize = 80f;
    [SerializeField] private float padding = 36f;

    private RectTransform _boardRect;
    private RectTransform _pieceRoot;

    public float CellSize => cellSize;
    public RectTransform PieceRoot => _pieceRoot;

    public void Build()
    {
        CacheOrCreateRoots();
        ClearChildren(_pieceRoot);
        ResizeBoard(); // 仅计算隐形网格的数学边界
    }

    // 供外部（棋子）获取某个格子的坐标
    public Vector2 GetPieceAnchoredPosition(int file, int rank)
    {
        return BoardCoord.ToLocal(file, rank, cellSize);
    }

    private void CacheOrCreateRoots()
    {
        _boardRect = GetComponent<RectTransform>();
        _pieceRoot = GetOrCreateChild("棋子层");
    }

    private RectTransform GetOrCreateChild(string childName)
    {
        Transform existing = transform.Find(childName);
        if (existing != null) return existing as RectTransform;

        GameObject child = new GameObject(childName, typeof(RectTransform));
        RectTransform rect = child.GetComponent<RectTransform>();
        rect.SetParent(transform, false);
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        rect.pivot = new Vector2(0.5f, 0.5f);
        return rect;
    }

    private void ResizeBoard()
    {
        float width = BoardCoord.FileMax * cellSize + padding * 2f;
        float height = BoardCoord.RankMax * cellSize + padding * 2f;
        _boardRect.sizeDelta = new Vector2(width, height);
        _boardRect.anchorMin = new Vector2(0.5f, 0.5f);
        _boardRect.anchorMax = new Vector2(0.5f, 0.5f);
        _boardRect.pivot = new Vector2(0.5f, 0.5f);
        _boardRect.anchoredPosition = Vector2.zero;
    }

    private static void ClearChildren(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);
            if (Application.isPlaying) Destroy(child.gameObject);
            else DestroyImmediate(child.gameObject);
        }
    }
}