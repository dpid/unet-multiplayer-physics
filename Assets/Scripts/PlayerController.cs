using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public const float SPEED = -3.0f;

    public GameObject ballPrefab;
    public Transform ballSpawnPoint;

    public void Start ()
    {
        GameManager.instance.RegisterPlayer(this, isLocalPlayer);
    }

    void Update ()
    {
        if (!isLocalPlayer) return;

        var z = Input.GetAxis("Horizontal") * Time.deltaTime * SPEED;
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    public void OnCollisionEnter (Collision collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Ball")
        {
            float difZ = collision.transform.position.z - transform.position.z;
            float angleY = difZ * -30.0f;
            
            if (transform.localRotation.y > 0.0f)
            {
                angleY *= -1.0f;
            }

            CmdAddForce(collisionGameObject, angleY);
        }
    }

    [Command]
    private void CmdAddForce (GameObject ball, float angleY)
    {
        ball.transform.rotation = ballSpawnPoint.rotation;
        Vector3 eulerAngles = ball.transform.eulerAngles;
        eulerAngles.y += angleY;
        ball.transform.eulerAngles = eulerAngles;
        ball.GetComponent<Rigidbody>().velocity = ball.transform.forward * 9.0f;
    }

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var ball = (GameObject)Instantiate(
            ballPrefab,
            ballSpawnPoint.position,
            ballSpawnPoint.rotation);

        // Add velocity to the bullet
        ball.GetComponent<Rigidbody>().velocity = ball.transform.forward * 9.0f;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(ball);
    }

}