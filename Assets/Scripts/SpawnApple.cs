using UnityEngine;
using Mirror;

public class SpawnApple : NetworkBehaviour
{
    public GameObject applePrefab;

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            CmdSpawnApple();
        }
    }

    [Command]
    private void CmdSpawnApple()
    {
        GameObject prefab = Instantiate(applePrefab, transform.position, Quaternion.identity);
        NetworkServer.Spawn(prefab);
    }
}
