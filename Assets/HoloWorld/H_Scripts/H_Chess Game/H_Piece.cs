using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(H_MaterialSetter))]
[RequireComponent(typeof(H_IObjectTweener))]

public abstract class H_Piece : MonoBehaviour
{
	[SerializeField] private H_MaterialSetter materialSetter;
	public H_Board board { protected get; set; }
	public Vector2Int occupiedSquare { get; set; }
	public H_TeamColor team { get; set; }
	public bool hasMoved { get; private set; }
	public List<Vector2Int> avaliableMoves;

	private H_IObjectTweener tweener;

	public abstract List<Vector2Int> SelectAvaliableSquares();

	private void Awake()
	{
		avaliableMoves = new List<Vector2Int>();
		tweener = GetComponent<H_IObjectTweener>();
		materialSetter = GetComponent<H_MaterialSetter>();
		hasMoved = false;
	}

	public void SetMaterial(Material selectedMaterial)
	{
		materialSetter.SetSingleMaterial(selectedMaterial);
	}

	public bool IsFromSameTeam(H_Piece piece)
	{
		return team == piece.team;
	}

	public bool CanMoveTo(Vector2Int coords)
	{
		return avaliableMoves.Contains(coords);
	}

	public virtual void MovePiece(Vector2Int coords)
	{
		Vector3 targetPosition = board.CalculatePositionFromCoords(coords);
		occupiedSquare = coords;
		hasMoved = true;
		tweener.MoveTo(transform, targetPosition);
	}


	protected void TryToAddMove(Vector2Int coords)
	{
		avaliableMoves.Add(coords);
	}

	public void SetData(Vector2Int coords, H_TeamColor team, H_Board board)
	{
		this.team = team;
		occupiedSquare = coords;
		this.board = board;
		transform.position = board.CalculatePositionFromCoords(coords);
	}

	public bool IsAttackingPieceOfType<T>() where T : H_Piece
	{
		foreach (var square in avaliableMoves)
		{
			if (board.GetPieceOnSquare(square) is T)
				return true;
		}
		return false;
	}
}
