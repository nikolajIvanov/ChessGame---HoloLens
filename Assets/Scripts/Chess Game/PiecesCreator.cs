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
    [SerializeField] public GameObject level;
    [SerializeField] public float localScale;
    /// <summary>
    /// Dict in dem die Namen der Figuren mit dem jeweiligen GameObject als Value hinterlegt werden
    /// </summary>
    private Dictionary<string, GameObject> nameToPieceDict = new Dictionary<string, GameObject>();
    
    /// <summary>
    /// Es werden alle Awake() Methoden vor dem Start des Spiels ausgeführt.
    /// Das Spiel startet in der Klasse ChessGameController mit der Methode Start()
    /// Klassen die eine Awake Methode haben:
    /// ChessGameController
    /// Pieces Creator
    /// Board
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
        // Es wird überprüft ob es die Figur gibt
        if (prefab)
        {
            // Hier wird die Figur erstellt
            GameObject newPiece = Instantiate(prefab);
            newPiece.transform.localScale = new Vector3(localScale, localScale, localScale);
            newPiece.transform.parent = level.transform;
            return newPiece;
        }
        return null;
    }
    
    public Material GetTeamMaterial(TeamColor team)
    {
        return team == TeamColor.White ? whiteMaterial : blackMaterial;
    }
}
