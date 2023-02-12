using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse wird dem Objekt GameMaster hinzugefügt.
/// Es werden alle GameObject Figuren (Prefabs) in der Variable "piecesPrefabs" gespeichert
/// </summary>
public class PiecesCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] piecesPrefabs; // Type: Liste
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material whiteMaterial;
    /// <summary>
    /// Dict in dem die Namen der Figuren mit dem jeweiligen GameObject als Value hinterlegt werden
    /// </summary>
    private Dictionary<string, GameObject> nameToPieceDict = new Dictionary<string, GameObject>();
    
    /// <summary>
    /// Es werden alle Awake() Methoden vor dem Start des Spiels ausgeführt.
    /// Das Spiel startet in der Klasse ChessGameController mit der Methode Start()
    /// </summary>
    private void Awake()
    {
        foreach (var piece in piecesPrefabs)
        {
            nameToPieceDict.Add(piece.GetComponent<Piece>().GetType().ToString(), piece);
        }
    }

    public GameObject CreatePiece(Type type)
    {
        GameObject prefab = nameToPieceDict[type.ToString()];
        if (prefab)
        {
            GameObject newPiece = Instantiate(prefab);
            return newPiece;
        }
        return null;
    }

    public Material GetTeamMaterial(TeamColor team)
    {
        return team == TeamColor.White ? whiteMaterial : blackMaterial;
    }
}
