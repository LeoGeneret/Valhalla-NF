using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    [HideInInspector]
    public static GameControl instance; // pour le singleton pattern

    [Header("Player object")] // les textes de l'interface
    public GameObject player;

    [Header("Textes")] // les textes de l'interface
    public Text scoreText;
    public Text gameOverText;
    public Text playerWonText;

    private int score = 0;
    [HideInInspector]
    public bool gameOver = false;
    [HideInInspector]
    public bool playerHasWon = false;

    [Header("Vitesse initiale du jeu")]
    public float gameSpeed = -1.5f; // vitesse de défilement du jeu

    [Header("Sprite du halo")]
    public GameObject haloSprite;
    [Range(0f, 1f)]
    public float haloOpacity = 1f;

    private AudioSource music;

    void Awake()
    {
        // singleton pattern, créer une suele instance du GameControl
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // GetComponents
        music = GetComponent<AudioSource>();

        // désactiver le texte de game over
        gameOverText.enabled = false;
        playerWonText.enabled = false;

        // opacité du sprite halo
        haloSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, haloOpacity);

        // arrêter la musique du menu
        if (SoundPlayer.instance) SoundPlayer.instance.StopMenuMusic();
    }

    // Update is called once per frame
    void Update () {

        if (gameOver && Input.GetKeyDown("return")) // Si la partie est terminée et que le joueur appui sur entrée
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // recharger la scène courante
        }

        // si le joueur meurt baisser le son de la musique;
        if (gameOver && music.volume != 0f) music.volume -= Time.deltaTime*0.5f;

        // si le joueur arrive au bout de la musique
        if (!music.isPlaying && !gameOver)
        {
            PlayerWon();
        }
    }

    public void PlayerScored() // si le joueur tue un monstre, son score augmente
    {
        score++;
        scoreText.text = "Score : " + score.ToString();
    }

    public void PlayerMissed() // si le joueur se trompe de touche, son score diminue
    {
        score--;
        scoreText.text = "Score : " + score.ToString();
    }

    public void PlayerDied() // si le joueur perd la partie
    {
        gameOverText.enabled = true; // le texte de game over s'affiche
        gameOver = true; // état de game over
        if (SoundPlayer.instance) SoundPlayer.instance.PlayMenuMusic(); // jouer la musique des menus
    }

    public void PlayerWon()
    {
        if (playerHasWon) return; // pour éviter d'appeler la fonction plusieurs fois

        gameOver = true;
        playerHasWon = true;

        playerWonText.enabled = true; // afficher le texte de fin de jeu
        if (SoundPlayer.instance) SoundPlayer.instance.PlayMenuMusic(); // jouer la musique des menus

        // faire déplacer le héros vers la droite de l'écran
        MovingObject mo = player.AddComponent<MovingObject>();
        mo.facteurDeVitesse = -1f;
        player.AddComponent<SelfDestruct>();
    }
}
