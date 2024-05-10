using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Game Manager is null!");
            }

            return _instance;
        }
    }

    public int points;
    public int winPoints;

    public bool[] keyChain;

    private void Awake()
    {
        _instance = this;
        
    }


    private void Start()
    {
        keyChain[0] = true; //El valor 0 reservado para transparentar paredes genéricas
    }

    private void Update()
    {
        
    }

    public void AddKey(int keyType)
    {
        keyChain[keyType] = true; //Pasa a true el bool del array keyChain
    }
}
