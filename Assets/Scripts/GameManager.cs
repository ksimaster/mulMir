using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameManager : NetworkBehaviour
{
    [SyncVar]
    public int globalMoney;

    public Text globalMoneyText;
    public Text moneyText;
    
    public GameObject moneyPrefab;

    public void Start()
    {
        if (isServer)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
                GameObject prefab = Instantiate(moneyPrefab, position, Quaternion.identity);
                NetworkServer.Spawn(prefab);
            }
        }
    }

    public void StopGame()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
        }
        else if (NetworkServer.active)
        {
            NetworkManager.singleton.StopServer();
        }
    }
}
