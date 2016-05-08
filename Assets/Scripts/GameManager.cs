using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public bool isAutoMatchMaking = true;
    public AutoMatchMaker autoMatchMaker;
    public GameObject CameraContainer;
    public Material alternatePlayerMaterial;

    public void Awake ()
    {
        GameManager.instance = this;
    }

    public void Start ()
    {
        if (isAutoMatchMaking)
        {
            autoMatchMaker.StartAutoMatch();
        } 
    }

    public void RegisterPlayer(PlayerController playerController, bool isLocalPlayer)
    {
        if (playerController.transform.position.x < 0)
        {

            if (isLocalPlayer)
            {
                CameraContainer.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }

            playerController.gameObject.GetComponent<MeshRenderer>().material = alternatePlayerMaterial; 
        }

    }
}
