using UnityEngine;
using System.Collections;

public class HealthFollow : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 wantedPos = Camera.main.WorldToScreenPoint(target.position);
        wantedPos.y += 30;
        transform.position = wantedPos;
	}
}
