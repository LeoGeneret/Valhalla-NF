using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Header("Paramètres de spawn")]
    public float facteurSpawnRate = 10f; // permet d'ajuster le spawnRate

    [Header("Les différents groupes d'ennemis possibeles (PREFABS)")]
    public GameObject group1;
    public GameObject group2;
    public GameObject group3;
    public GameObject group4;
    public GameObject group5;

    // mettre les groupes de prefabs sous forme de liste pour y accéder aléatoirement
    private List<GameObject> groupsList = new List<GameObject>();
    static System.Random rnd = new System.Random();

    private float timeSinceLastSpawned; // temps écoulé depuis le dernier spawn
    private Vector2 spawnPosition = new Vector2(8, 0); // position de spawn des groupes d'ennemis
    private float spawnRate;

    // Use this for initialization
    void Start () {
        timeSinceLastSpawned = 0f; // mise à zéro au départ
        spawnRate = Mathf.Abs(facteurSpawnRate / GameControl.instance.gameSpeed); // calcule le spawnRatwe par rapport à la vitesse du jeu

        // ajouter les groupes de prefabs à la liste
        groupsList.Add(group1);
        groupsList.Add(group2);
        groupsList.Add(group3);
        groupsList.Add(group4);
        groupsList.Add(group5);
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceLastSpawned += Time.deltaTime; // incrémentation du temps écoulé

        if (!GameControl.instance.gameOver && timeSinceLastSpawned >= spawnRate)
        {
            //Debug.Log("timesincelastspawned : " + timeSinceLastSpawned + " --- spawn rate : " + spawnRate);
            timeSinceLastSpawned = 0f; // remise à zéro

            int r = rnd.Next(groupsList.Count); // choisir un groupe aléatoirement dans la liste
            Instantiate(groupsList[r], spawnPosition, Quaternion.identity); // spawn d'un groupe d'ennemi
        }
	}
}
