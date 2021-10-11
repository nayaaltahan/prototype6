using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BananaCollection : MonoBehaviour
{
    private static BananaCollection bananaCollection;
    private int bananas;

    public List<GameObject> bananaList;

    void Start()
    {
        bananaCollection = this;
        foreach(GameObject go in bananaList)
        {
            go.SetActive(false);
        }
    }

    public static BananaCollection Instance => bananaCollection;
    public int Bananas
    {
        set {
            bananaList[bananas].SetActive(true);
            bananas = value;
            if (value == 3)
            {
                SceneManager.LoadScene("Victory");
            }
        }
        get { return bananas; }
    }
}