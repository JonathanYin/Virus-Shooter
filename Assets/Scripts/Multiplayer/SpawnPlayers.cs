using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
// using PlayerScript;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameManager manager;


    public float minX, maxX, minY, maxY;

    private void Start(){
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        PlayerScript playerscript = player.GetComponent<PlayerScript>();
        manager.AddPlayer(playerscript);
        playerscript.Color(GameManager.players, manager);
        
        Debug.LogWarning(GameManager.players);
        GameManager.players += 1;
    }
}
