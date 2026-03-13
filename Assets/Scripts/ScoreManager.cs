using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance = new ScoreManager();
    
    public int score = 0;

    public static ScoreManager Instance
    {
        get => _instance;
    }
}
