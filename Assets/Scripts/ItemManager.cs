using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] public GameObject[] item;

    private float level = 1;
    private GameObject player;


    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag != "Player")
        {
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.F))
        {
            Buy(player);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (player != null)
        {
            player = null;
        }
    }

    public void Buy(GameObject player)
    {
        // プレイヤーのステータス　所持コスト
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0;  i < item.Length; i++)
        {
            bool IsActive = (i == 0) ? true : false;
            item[i].SetActive(IsActive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
