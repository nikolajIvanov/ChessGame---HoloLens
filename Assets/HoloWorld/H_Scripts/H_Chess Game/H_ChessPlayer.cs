using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class H_ChessPlayer
{
	public H_TeamColor team { get; set; }
	public H_Board board { get; set; }
	public List<H_Piece> activePieces { get; private set; }

	public H_ChessPlayer(H_TeamColor team, H_Board board)
	{
		activePieces = new List<H_Piece>();
		this.board = board;
		this.team = team;
	}

	public void AddPiece(H_Piece piece)
	{
		if (!activePieces.Contains(piece))
			activePieces.Add(piece);
	}

	public void RemovePiece(H_Piece piece)
	{
		if (activePieces.Contains(piece))
			activePieces.Remove(piece);
	}

	public void GenerateAllPossibleMoves()
	{
		foreach (var piece in activePieces)
		{
			if(board.HasPiece(piece))
				piece.SelectAvaliableSquares();
		}
	}

	public H_Piece[] GetPieceAtackingOppositePiceOfType<T>() where T : H_Piece
	{
		return activePieces.Where(p => p.IsAttackingPieceOfType<T>()).ToArray();
	}

	public H_Piece[] GetPiecesOfType<T>() where T : H_Piece
	{
		return activePieces.Where(p => p is T).ToArray();
	}

	public void RemoveMovesEnablingAttakOnPieceOfType<T>(H_ChessPlayer opponent, H_Piece selectedPiece) where T : H_Piece
	{
		List<Vector2Int> coordsToRemove = new List<Vector2Int>();

		coordsToRemove.Clear();
		foreach (var coords in selectedPiece.avaliableMoves)
		{
			H_Piece pieceOnCoords = board.GetPieceOnSquare(coords);
			board.UpdateBoardOnPieceMove(coords, selectedPiece.occupiedSquare, selectedPiece, null);
			opponent.GenerateAllPossibleMoves();
			if (opponent.CheckIfIsAttacigPiece<T>())
				coordsToRemove.Add(coords);
			board.UpdateBoardOnPieceMove(selectedPiece.occupiedSquare, coords, selectedPiece, pieceOnCoords);
		}
		foreach (var coords in coordsToRemove)
		{
			selectedPiece.avaliableMoves.Remove(coords);
		}

	}

	internal bool CheckIfIsAttacigPiece<T>() where T : H_Piece
	{
		foreach (var piece in activePieces)
		{
			if (board.HasPiece(piece) && piece.IsAttackingPieceOfType<T>())
				return true;
		}
		return false;
	}

	public bool CanHidePieceFromAttack<T>(H_ChessPlayer opponent) where T : H_Piece
	{
		foreach (var piece in activePieces)
		{
			foreach (var coords in piece.avaliableMoves)
			{
				H_Piece pieceOnCoords = board.GetPieceOnSquare(coords);
				board.UpdateBoardOnPieceMove(coords, piece.occupiedSquare, piece, null);
				opponent.GenerateAllPossibleMoves();
				if (!opponent.CheckIfIsAttacigPiece<T>())
				{
					board.UpdateBoardOnPieceMove(piece.occupiedSquare, coords, piece, pieceOnCoords);
					return true;
				}
				board.UpdateBoardOnPieceMove(piece.occupiedSquare, coords, piece, pieceOnCoords);
			}
		}
		return false;
	}

	internal void OnGameRestarted()
	{
		activePieces.Clear();
	}
}
