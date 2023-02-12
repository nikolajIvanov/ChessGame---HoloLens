using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class H_ChessGameController : MonoBehaviour
{
     private enum H_GameState
    {
        Init, Play, Finished
    }

    [SerializeField] private H_BoardLayout startingBoardLayout;
    [SerializeField] private H_Board board;
    //[SerializeField] private ChessUIManager UIManager;

    private H_PiecesCreator pieceCreator;
    private H_ChessPlayer whitePlayer;
    private H_ChessPlayer blackPlayer;
    private H_ChessPlayer activePlayer;

    private H_GameState state;

    private void Awake()
    {
        SetDependencies();
        CreatePlayers();
    }

    private void SetDependencies()
    {
        pieceCreator = GetComponent<H_PiecesCreator>();
    }

    private void CreatePlayers()
    {
        whitePlayer = new H_ChessPlayer(H_TeamColor.White, board);
        blackPlayer = new H_ChessPlayer(H_TeamColor.Black, board);
    }

    private void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        SetGameState(H_GameState.Init);
        //UIManager.HideUI();
        board.SetDependencies(this);
        CreatePiecesFromLayout(startingBoardLayout);
        activePlayer = whitePlayer;
        GenerateAllPossiblePlayerMoves(activePlayer);
        SetGameState(H_GameState.Play);
    }
    private void SetGameState(H_GameState state)
    {
        this.state = state;
    }

    internal bool IsGameInProgress()
    {
        return state == H_GameState.Play;
    }



    private void CreatePiecesFromLayout(H_BoardLayout layout)
    {
        for (int i = 0; i < layout.GetPiecesCount(); i++)
        {
            Vector2Int squareCoords = layout.GetSquareCoordsAtIndex(i);
            H_TeamColor team = layout.GetSquareTeamColorAtIndex(i);
            string typeName = layout.GetSquarePieceNameAtIndex(i);

            Type type = Type.GetType(typeName);
            CreatePieceAndInitialize(squareCoords, team, type);
        }
    }



    public void CreatePieceAndInitialize(Vector2Int squareCoords, H_TeamColor team, Type type)
    {
        H_Piece newPiece = pieceCreator.CreatePiece(type).GetComponent<H_Piece>();
        newPiece.SetData(squareCoords, team, board);

        Material teamMaterial = pieceCreator.GetTeamMaterial(team);
        newPiece.SetMaterial(teamMaterial);

        board.SetPieceOnBoard(squareCoords, newPiece);

        H_ChessPlayer currentPlayer = team == H_TeamColor.White ? whitePlayer : blackPlayer;
        currentPlayer.AddPiece(newPiece);
    }

    private void GenerateAllPossiblePlayerMoves(H_ChessPlayer player)
    {
        player.GenerateAllPossibleMoves();
    }

    public bool IsTeamTurnActive(H_TeamColor team)
    {
        return activePlayer.team == team;
    }

    public void EndTurn()
    {
        GenerateAllPossiblePlayerMoves(activePlayer);
        GenerateAllPossiblePlayerMoves(GetOpponentToPlayer(activePlayer));
        if (CheckIfGameIsFinished())
        {
            EndGame();
        }
        else
        {
            ChangeActiveTeam();
        }
    }

    private bool CheckIfGameIsFinished()
    {
        H_Piece[] kingAttackingPieces = activePlayer.GetPieceAtackingOppositePiceOfType<H_King>();
        if (kingAttackingPieces.Length > 0)
        {
            H_ChessPlayer oppositePlayer = GetOpponentToPlayer(activePlayer);
            H_Piece attackedKing = oppositePlayer.GetPiecesOfType<H_King>().FirstOrDefault();
            oppositePlayer.RemoveMovesEnablingAttakOnPieceOfType<H_King>(activePlayer, attackedKing);

            int avaliableKingMoves = attackedKing.avaliableMoves.Count;
            if (avaliableKingMoves == 0)
            {
                bool canCoverKing = oppositePlayer.CanHidePieceFromAttack<H_King>(activePlayer);
                if (!canCoverKing)
                    return true;
            }
        }
        return false;
    }

    private void EndGame()
    {
        SetGameState(H_GameState.Finished);
        //UIManager.OnGameFinished(activePlayer.team.ToString());
    }

    public void RestartGame()
    {
        DestroyPieces();
        board.OnGameRestarted();
        whitePlayer.OnGameRestarted();
        blackPlayer.OnGameRestarted();
        StartNewGame();
    }

    private void DestroyPieces()
    {
        whitePlayer.activePieces.ForEach(p => Destroy(p.gameObject));
        blackPlayer.activePieces.ForEach(p => Destroy(p.gameObject));
    }

    private void ChangeActiveTeam()
    {
        activePlayer = activePlayer == whitePlayer ? blackPlayer : whitePlayer;
    }

    private H_ChessPlayer GetOpponentToPlayer(H_ChessPlayer player)
    {
        return player == whitePlayer ? blackPlayer : whitePlayer;
    }

    internal void OnPieceRemoved(H_Piece piece)
    {
        H_ChessPlayer pieceOwner = (piece.team == H_TeamColor.White) ? whitePlayer : blackPlayer;
        pieceOwner.RemovePiece(piece);
    }

    internal void RemoveMovesEnablingAttakOnPieceOfType<T>(H_Piece piece) where T : H_Piece
    {
        activePlayer.RemoveMovesEnablingAttakOnPieceOfType<T>(GetOpponentToPlayer(activePlayer), piece);
    }
}
