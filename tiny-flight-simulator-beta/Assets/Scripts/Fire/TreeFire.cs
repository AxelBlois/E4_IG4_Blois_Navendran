using System.Collections;
using UnityEngine;

public class TreeFire : MonoBehaviour
{
    private ParticleSystem fireInstance;
    private TreeCounter treeCounter; 
    private bool isExtinguished = false;
    private GameManager gameManager;



    public void Start()
    {
        treeCounter = FindObjectOfType<TreeCounter>();
        gameManager = FindObjectOfType<GameManager>();


        if (treeCounter != null )
        {
            treeCounter.IncrementalInflamedTree();
        }
    }


    public void Ignite(ParticleSystem firePrefab)
    {
        if(fireInstance == null && firePrefab != null)
        {
            fireInstance = Instantiate(firePrefab, transform);
            fireInstance.transform.localPosition = Vector3.zero;
        }
    }

    public void Extinguish()
    {
        if (isExtinguished) return;

        if(fireInstance != null)
        {
            fireInstance.Stop();
            Destroy(fireInstance.gameObject, 0.5f);
            fireInstance = null;
            isExtinguished = true;

            if (treeCounter !=null)
            {
                treeCounter.IncrementalExtinguisedTree();
                gameManager.TreeExtinguished();
            }
            
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("WaterPart"))
        {
            Extinguish();
        }

    }

}
