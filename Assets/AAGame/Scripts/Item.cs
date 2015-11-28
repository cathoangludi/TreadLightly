using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public static Player player;

    // Use this for initialization

    protected virtual void Awake() {
        if (player == null) {
            player = GameObject.Find("/FPSController/FirstPersonCharacter").GetComponent<Player>();
        }
    }
    protected virtual void Start () {

	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}


    public virtual void Collect() {
        Destroy(gameObject);
    }

}
