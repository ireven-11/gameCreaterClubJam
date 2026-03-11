using UnityEngine;

public class Title : MonoBehaviour
{
    private bool firstPush=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PressStart()
    {
        Debug.Log("Press Start");
        if(firstPush==false)
        {
            Debug.Log("GoNext");
            firstPush = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
