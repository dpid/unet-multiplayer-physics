using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Ball : NetworkBehaviour {

    new private Rigidbody rigidbody;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

	void Update () {
        float speed = rigidbody.velocity.magnitude;
        if (speed <= 0)
        {
            CmdDestroy();
        }
	}

    [Command]
    private void CmdDestroy()
    {
        Destroy(gameObject);
    }
}
