using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pour que les groupes d'ennemis aient une durée de vie limitée
/// </summary>

public class SelfDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 10f);
	}
}
