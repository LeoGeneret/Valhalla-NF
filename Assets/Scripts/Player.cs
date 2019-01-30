using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // les explosions
    public GameObject exploBlue;
    public GameObject exploGreen;
    public GameObject exploRed;
    private Dictionary<string, GameObject> explos = new Dictionary<string, GameObject>();

    public GameObject exploHero;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        // construire le dictionnaire des explosions
        explos.Add("Blue", exploBlue);
        explos.Add("Green", exploGreen);
        explos.Add("Red", exploRed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (GameControl.instance.gameOver) return;

        if ( // conditions dans lesquelles la touche appuyée correspond au type d'ennemi qui arrive
            (Input.GetKey("left") && collision.gameObject.tag == "Blue" && !Input.GetKey("up") && !Input.GetKey("right") ||
            Input.GetKey("up") && collision.gameObject.tag == "Green" && !Input.GetKey("left") && !Input.GetKey("right") ||
            Input.GetKey("right") && collision.gameObject.tag == "Red" && !Input.GetKey("up") && !Input.GetKey("left")) && !GameControl.instance.gameOver ||
            GameControl.instance.playerHasWon /* ou que le joueur a atteint la fin de la musique */
            )
        {
            KillEnnemy(collision);
        } else if ((Input.GetKey("left") || Input.GetKey("up") || Input.GetKey("right")) && !GameControl.instance.gameOver) // si le joueur appuie sur une mauvaise touche, il perd des points
        {
            MissEnnemy();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // jouer l'explosion du heros
        exploHero.SetActive(true); // enable l'explosion
        exploHero.GetComponent<Animator>().Play("hero-explo", -1, 0); // faire commencer son animation à 0

        Destroy(this.gameObject); // détruire le joueur
        GameControl.instance.PlayerDied();
        Destroy(collision.gameObject.transform.parent.gameObject, 4f); // tuer le groupe de monstres pour éviter trop de calculs
    }

    private void KillEnnemy(Collider2D collision) // fonction pour tuer un ennemi
    {
        string enemyType = collision.gameObject.tag;

        anim.SetTrigger(enemyType); // animation d'attaque basée sur le tag de l'ennemi

        if (!GameControl.instance.gameOver)
        {
            explos[enemyType].SetActive(true); // enable l'explosion
            explos[enemyType].GetComponent<Animator>().Play(enemyType, -1, 0); // faire commencer son animation à 0
        }
        
        Destroy(collision.gameObject.GetComponent<Collider2D>()); // détruire le collider pour ne pas réentrer dans la boucle
        Destroy(collision.gameObject, 0.12f); // tuer le monstre. On peut ajouter un délai en float comme 2ème paramètre
        Destroy(collision.gameObject.transform.parent.gameObject, 10f); // détruire le parent pour éviter les calculs

        GameControl.instance.PlayerScored();
    }

    private void MissEnnemy()
    {
        GameControl.instance.PlayerMissed();
    }
}
