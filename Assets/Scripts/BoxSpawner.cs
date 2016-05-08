using UnityEngine;
using UnityEngine.Networking;

public class BoxSpawner : NetworkBehaviour {

    public GameObject boxPrefab;

    public override void OnStartServer()
    {
        GameObject box = (GameObject)Instantiate(boxPrefab, transform.position, transform.rotation);
        NetworkServer.Spawn(box);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.8f, 0.8f, 0.8f, 1.0F);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
}
