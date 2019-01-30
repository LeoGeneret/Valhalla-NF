using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    /// <summary>
    /// Ce script donne la vitesse aux ennemis et à l'environnement
    /// </summary>

    private Rigidbody2D rb2d;
    public float facteurDeVitesse = 1f; // permet d'avoir une vitesse différente pour la parallaxe de l'environnement
    public bool stopOnDeath = false; // se règle dans l'inspecteur, permet de stopper les décors lorsque le joueur meurt

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(GameControl.instance.gameSpeed * facteurDeVitesse, 0);
	}

    private void Update()
    {
        // stopper le sprite lorsque le joueur meurt
        if (!GameControl.instance.gameOver)
        {
            return;
        }
        else if (stopOnDeath)
        {
            rb2d.velocity = Vector2.zero;
        }
    }
}
