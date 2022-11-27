using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Manager : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void SpawnPlayer()
    {
        PhotonNetwork.Instantiate("Player", playerPrefab.transform.position, playerPrefab.transform.rotation);
    }
}
