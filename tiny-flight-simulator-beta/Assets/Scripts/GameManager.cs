using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider progressBar;
    public int treeNumber = 15;

    private int extinguishedTrees = 0;


    // Start is called before the first frame update
    void Start()
    {
        progressBar.maxValue = treeNumber;
        progressBar.value = 0;
    }

    // Update is called once per frame
    public void TreeExtinguished()
    {
        extinguishedTrees++;
        progressBar.value = extinguishedTrees;

        if(extinguishedTrees >= treeNumber)
        {
            Victory();
        }
    }

    public void Victory()
    {
        Debug.Log("Vous avez gagné");
        SceneManager.LoadScene("EndScreen");
    }


}
