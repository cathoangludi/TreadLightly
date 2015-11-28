using UnityEngine;
using System.Collections;

public class Ammo : Item {

    public int amount;
	// Use this for initialization
	void Start () {
        // base.Start();
        if (amount == 0) {
            amount = 3;
        }

        gameObject.name = ".45 Ammo (" + amount + ")";
    }
	
	// Update is called once per frame
	void Update () {
        // base.Update();
	}

    override
    public void Collect() {
        player.AddAmmo(amount);
        base.Collect();
    }
}
