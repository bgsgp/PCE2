using UnityEngine;
using System.Collections.Generic;
using System.Linq; // 用于洗牌

public class XiangqiDemo : MonoBehaviour
{
    [SerializeField] private BoardView boardView;

    private int _flippedCount = 0;
    private Color _playerColor;
    private bool _hasChosenSide = false;

    private void Start()
    {
        if (boardView == null)
        {
            Debug.LogError("XiangqiDemo：请把「棋盘」拖到 Board View 槽里。");
            return;
        }

        boardView.Build();
        SpawnRandomPieces(); // 改为随机填满
    }

    private Sprite LoadPieceSprite(string fileName)
    {
        Sprite sprite = Resources.Load<Sprite>($"棋子/{fileName}");
        if (sprite == null)
        {
            Debug.LogError($"找不到图片：Assets/Resources/棋子/{fileName}.png");
        }
        return sprite;
    }

    private void SpawnRandomPieces()
    {
        // 1. 收集 9x5 所有 45 个格子的坐标
        List<Vector2Int> allCoords = new List<Vector2Int>();
        for (int f = 0; f <= BoardCoord.FileMax; f++)
        {
            for (int r = 0; r <= BoardCoord.RankMax; r++)
            {
                allCoords.Add(new Vector2Int(f, r));
            }
        }

        // 2. 随机打乱坐标顺序（洗牌）
        System.Random rand = new System.Random();
        allCoords = allCoords.OrderBy(x => rand.Next()).ToList();

        // 3. 定义红绿双方的棋子池（由于要填满45格，基础32个棋子不够，我们按比例重复使用）
        string[] redNames = { "帅", "士", "士", "相", "相", "马", "马", "车", "车", "炮", "炮", "兵", "兵", "兵", "兵", "兵" };
        string[] redFiles = { "red_shuai", "red_shi", "red_shi", "red_xiang", "red_xiang", "red_ma", "red_ma", "red_ju", "red_ju", "red_pao", "red_pao", "red_bing", "red_bing", "red_bing", "red_bing", "red_bing" };

        string[] greenNames = { "将", "士", "士", "象", "象", "马", "马", "车", "车", "炮", "炮", "卒", "卒", "卒", "卒", "卒" };
        string[] greenFiles = { "green_jiang", "green_shi", "green_shi", "green_xiang", "green_xiang", "green_ma", "green_ma", "green_ju", "green_ju", "green_pao", "green_pao", "green_zu", "green_zu", "green_zu", "green_zu", "green_zu" };

        // 4. 计算数量：45 格，红方 23 个，绿方 22 个（或反过来）
        int totalPieces = allCoords.Count; // 45
        int redCount = totalPieces / 2;    // 22
        int greenCount = totalPieces - redCount; // 23

        // 5. 开始随机生成
        int pieceIndex = 0;

        // 生成红方
        for (int i = 0; i < redCount; i++)
        {
            Vector2Int coord = allCoords[pieceIndex++];
            int randIndex = rand.Next(redNames.Length);
            SpawnPiece(coord.x, coord.y, redNames[randIndex], Color.red, LoadPieceSprite(redFiles[randIndex]));
        }

        // 生成绿方
        for (int i = 0; i < greenCount; i++)
        {
            Vector2Int coord = allCoords[pieceIndex++];
            int randIndex = rand.Next(greenNames.Length);
            SpawnPiece(coord.x, coord.y, greenNames[randIndex], Color.black, LoadPieceSprite(greenFiles[randIndex]));
        }

        Debug.Log($"v0.3 启动成功。棋盘 {BoardCoord.FileCount}x{BoardCoord.RankCount}，已随机填满 {totalPieces} 个棋子（红 {redCount} / 绿 {greenCount}）。");
    }

    private void SpawnPiece(int file, int rank, string name, Color color, Sprite sprite)
    {
        // 名字加入坐标防止重名报错
        GameObject pieceObject = new GameObject($"待翻_{name}_{file}_{rank}", typeof(RectTransform), typeof(UnityEngine.UI.Image));
        PieceView piece = pieceObject.AddComponent<PieceView>();

        // 默认是暗棋（isFlipped = false）
        piece.Setup(file, rank, name, color, sprite, false);
        piece.PlaceOn(boardView);

        piece.OnPieceFlipped += HandlePieceFlipped;
    }

    private void HandlePieceFlipped(PieceView piece)
    {
        if (_flippedCount >= 5) return;

        _flippedCount++;

        if (!_hasChosenSide)
        {
            _playerColor = piece.PieceColor;
            _hasChosenSide = true;
            string colorName = _playerColor == Color.red ? "红方" : "绿方";
            Debug.Log($"<color=#00FF00>【阵营确定】</color> 第一个棋子翻开，颜色是 {colorName}，玩家执 {colorName}！");
        }

        Debug.Log($"已翻开 {_flippedCount}/5 个棋子。当前翻开：{piece.gameObject.name}");

        if (_flippedCount >= 5)
        {
            Debug.Log("<color=#00FFFF>【开局阶段结束】</color> 5个棋子已翻完，可以开始走棋了。");
        }
    }
}