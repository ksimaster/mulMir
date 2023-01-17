using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player : NetworkBehaviour
{
    public GameManager manager;

    public int money;

    private Rigidbody2D rb;
    private Camera mainCam;
    private Vector2 input;
    public float speed;
 
    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        manager.globalMoneyText.text = "Global Money: " + manager.globalMoney;
        manager.moneyText.text = "Money: " + money;

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Flip(); CameraMovement();
    }

    private void CameraMovement()
    {
        mainCam.transform.localPosition = new Vector3(transform.position.x, transform.position.y, -20f);
        transform.position = Vector2.MoveTowards(transform.position, mainCam.transform.localPosition, Time.deltaTime);
    }

    private void Flip()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed / 100);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Money")
        {
            Destroy(collider.gameObject);
            money += 1;

            RpcGlobalMoney();
        }
    }

    [ClientRpc]
    public void RpcGlobalMoney()
    {
        manager.globalMoney += 1;
    }
}
