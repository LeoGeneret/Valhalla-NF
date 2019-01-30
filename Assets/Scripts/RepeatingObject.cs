using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingObject : MonoBehaviour {

    /// <summary>
    /// Répète les objets comme le background, le sol, etc.
    /// </summary>
    /// 

    private SpriteRenderer spr;
    private float actualWidth; // la taille du sprite une fois rescalé

	// Use this for initialization
	void Start () {
        spr = GetComponent<SpriteRenderer>();
        actualWidth = spr.size.x * transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position.x < -actualWidth) // si le sprite a été déplacé de sa largeur
        {
            //Debug.Log("repeated");
            Vector2 offset = new Vector2(actualWidth * 2f, 0); // on calcule le vecteur de repositionnement
            transform.position = (Vector2)transform.position + offset; // on repositionne le sprite en fonction du vecteur calculé
        }
	}
}
