using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlacer : MonoBehaviour
{
    public GameObject treePrefab;      // Le prefab d'arbre
    public float minHeight = 0.1f;     // Hauteur minimale (�viter l'eau)
    public float maxHeight = 10f;      // Hauteur maximale (�viter les sommets trop hauts)
    public int maxTrees = 100;         // Nombre maximum d'arbres
    public Transform meshHolder;       // L'objet qui contient le terrain g�n�r�
    public Transform spawnPoint;       // Point autour duquel les arbres sont g�n�r�s
    public float spawnRadius = 50f;    // Rayon autour du spawnPoint pour g�n�rer les arbres
    public ParticleSystem firePrefab;  // Pr�fabriqu� des flammes

    void Start()
    {
        PlaceTrees();
    }

    void PlaceTrees()
    {
        if (meshHolder == null || treePrefab == null || spawnPoint == null)
        {
            return;
        }

        MeshFilter meshFilter = meshHolder.GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            return;
        }

        Mesh mesh = meshFilter.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        int placedTrees = 0;

        // Placer les arbres jusqu'� atteindre le nombre maximal
        while (placedTrees < maxTrees)
        {
            // Choisir un triangle al�atoire sur le mesh
            int randomTriangleIndex = Random.Range(0, triangles.Length / 3) * 3;
            Vector3 v0 = vertices[triangles[randomTriangleIndex]];
            Vector3 v1 = vertices[triangles[randomTriangleIndex + 1]];
            Vector3 v2 = vertices[triangles[randomTriangleIndex + 2]];

            // G�n�rer une position al�atoire dans le triangle
            Vector3 randomPoint = RandomPointInTriangle(v0, v1, v2);
            float height = randomPoint.y;

            // Convertir la position locale en position mondiale
            Vector3 worldPosition = meshHolder.TransformPoint(randomPoint);

            // V�rifier si la position est dans le rayon autour du spawnPoint
            if (Vector3.Distance(worldPosition, spawnPoint.position) > spawnRadius)
                continue;

            // V�rifier si la hauteur est dans les limites
            if (height < minHeight || height > maxHeight)
                continue;

            // Placer l'arbre
            GameObject tree = Instantiate(treePrefab, worldPosition, Quaternion.identity, meshHolder);

            TreeFire treeFire = tree.AddComponent<TreeFire>();
            if(treeFire != null)
            {
                treeFire.Ignite(firePrefab);
            }


            placedTrees++;
        }

        Debug.Log($"Nombre total d'arbres plac�s : {placedTrees}");
    }

    // M�thode pour g�n�rer une position al�atoire dans un triangle
    Vector3 RandomPointInTriangle(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        float r1 = Mathf.Sqrt(Random.value);
        float r2 = Random.value;
        Vector3 point = (1 - r1) * v0 + (r1 * (1 - r2)) * v1 + (r1 * r2) * v2;
        return point;
    }

}
