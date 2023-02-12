using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum H_TeamColor
{
    Black, White
}

public enum H_PieceType
{
    H_Pawn, H_Bishop, H_Knight, H_Rook, H_Queen, H_King
}
[CreateAssetMenu(menuName = "Scriptable Objects/Board/H_Layout")]
public class H_BoardLayout : ScriptableObject
{
    [Serializable]
    private class H_BoardSquareSetup
    {
        public Vector2Int position;
        public H_PieceType pieceType;
        public H_TeamColor teamColor;
    }

    [SerializeField] private H_BoardSquareSetup[] boardSquares;

    public int GetPiecesCount()
    {
        return boardSquares.Length;
    }


    public Vector2Int GetSquareCoordsAtIndex(int index)
    {
        return new Vector2Int(boardSquares[index].position.x - 1, boardSquares[index].position.y - 1);
    }

    public string GetSquarePieceNameAtIndex(int index)
    {
        return boardSquares[index].pieceType.ToString();
    }

    public H_TeamColor GetSquareTeamColorAtIndex(int index)
    {
        return boardSquares[index].teamColor;
    }
}
