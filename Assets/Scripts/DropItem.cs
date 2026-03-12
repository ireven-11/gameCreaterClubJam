using UnityEngine;

public class DropItem : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private int acceleration = 0;
    #endregion

    #region メンバ変数群
    float speed = 0.0f;
    Vector2 moveDir = Vector2.zero;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
