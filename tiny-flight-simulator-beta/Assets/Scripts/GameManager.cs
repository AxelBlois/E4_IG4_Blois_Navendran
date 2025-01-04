using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Progression du jeu")]
    public Slider progressBar;
    public int treeNumber = 15;
    private int extinguishedTrees = 0;

    [Header("Ecran préjeu")]
    public GameObject panel;
    public TextMeshProUGUI[] texts;
    public float blinkSpeed = 0.75f;
    private bool gameStarted = false;

    void Start()
    {
        // Initialisation de la jauge et du jeu
        progressBar.maxValue = treeNumber;
        progressBar.value = 0;
        Time.timeScale = 0f;
        StartCoroutine(BlinkText());
    }

    private void Update()
    {
        // Démarrage du jeu à la première touche pressée
        if (!gameStarted && Input.anyKeyDown)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    public void TreeExtinguished()
    {
        extinguishedTrees++;
        progressBar.value = extinguishedTrees;

        if (extinguishedTrees >= treeNumber)
        {
            Victory();
        }
    }

    public void Victory()
    {
        Debug.Log("Vous avez gagné !");
        SceneManager.LoadScene("EndScreen");
    }

    IEnumerator BlinkText()
    {
        float alpha = 1f;
        bool fadingOut = true;

        while (!gameStarted)
        {
            // Clignotement plus fluide et sécurisé
            alpha += (fadingOut ? -1 : 1) * Time.unscaledDeltaTime * blinkSpeed;
            alpha = Mathf.Clamp01(alpha); // Empêche les valeurs hors limites

            // Appliquer l'effet sur chaque texte
            foreach (TextMeshProUGUI text in texts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            }

            // Inversion au bon moment
            if (alpha <= 0f) fadingOut = false;
            if (alpha >= 1f) fadingOut = true;

            yield return null;
        }
    }
}
