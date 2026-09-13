using UnityEngine;

public static class BoardCoord
{
    public const int FileCount = 9;
    public const int RankCount = 5;
    public const int FileMax = FileCount - 1;
    public const int RankMax = RankCount - 1;

    public static bool IsInside(int file, int rank)
    {
        return file >= 0 && file <= FileMax && rank >= 0 && rank <= RankMax;
    }

    public static Vector2 ToLocal(int file, int rank, float cellSize)
    {
        float x = (file - FileMax * 0.5f) * cellSize;
        float y = (rank - RankMax * 0.5f) * cellSize;
        return new Vector2(x, y);
    }

    public static bool TryFromLocal(Vector2 local, float cellSize, out int file, out int rank)
    {
        float fx = local.x / cellSize + FileMax * 0.5f;
        float fy = local.y / cellSize + RankMax * 0.5f;
        file = Mathf.RoundToInt(fx);
        rank = Mathf.RoundToInt(fy);
        return IsInside(file, rank);
    }
}