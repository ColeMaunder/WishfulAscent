using UnityEngine;

public class PlayerConfigurer : MonoBehaviour
{
     [SerializeField]
    private Transform [] startPositions;
    private Transform[] players;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Transform playerPerent = GameObject.FindWithTag("Player").transform;
        /*for(int i = 0; i <= 1;i++){
            players[i] = playerPerent.GetChild(1);
        }*/
        for(int i = 0; i <= 1;i++){
           playerPerent.GetChild(i).position = startPositions[i].position;
        }
    }
}
