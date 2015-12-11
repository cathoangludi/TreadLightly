using UnityEngine;
using System.Collections;

public class Pistol : Item {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    override
    public void Collect() {
        player.hasGun = true;
        player.updateAmmoText();
        base.Collect();
    }
}
