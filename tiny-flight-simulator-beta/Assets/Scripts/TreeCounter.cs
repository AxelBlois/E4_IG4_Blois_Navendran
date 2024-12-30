using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeCounter : MonoBehaviour
{
    public Text inflamedTreesText;
    public Text extinguisedTreesText;

    private int infTree = 0;
    private int extTree = 0;


    // Start is called before the first frame update
    void Start()
    {
        Update();
    }
    
    public void IncrementalInflamedTree()
    {
        infTree++;
        Update();
    }
    
    public void IncrementalExtinguisedTree()
    {
        extTree++;
        Update();
    }



    private void Update()
    {
        inflamedTreesText.text = $"Arbres enflammés : {infTree}";
        extinguisedTreesText.text = $"Arbres éteints : {extTree}";
    }


}
