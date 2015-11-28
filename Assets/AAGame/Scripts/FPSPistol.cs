using UnityEngine;
using System.Collections;

public class FPSPistol : MonoBehaviour {

    public enum PistolState {
        NORMAL,
        AIMING,
        SHOOTING,
        RELEASING,
        AIMED
    }

    public PistolState currentState;
    Animator anim;
    SpriteRenderer sprite;
    Light flash;
    AudioSource sfx;

    private float firingDelay;


	// Use this for initialization
	void Start () {
        currentState = PistolState.NORMAL;
        //firingDelay = 2.0f;
        anim = GetComponent<Animator>();
        flash = GetComponent<Light>();
        sfx = GetComponent<AudioSource>();
        sprite = GameObject.Find("/FPSController/FirstPersonCharacter/FPSPistol/MuzzleFlash").GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
	    firingDelay -= Time.deltaTime;
	}

    void FixedUpdate() {
        
    }

    public void Aim() {
        if (currentState == PistolState.AIMED) return;
        anim.Play("Aim");
        currentState = PistolState.AIMING;
    }

    public void Release() {
        anim.Play("Release");
        currentState = PistolState.RELEASING;
    }

    public void TriggerReady() {
        anim.Play("Aimed");
        currentState = PistolState.AIMED;
    }

    public bool Shoot() {
        if (firingDelay > 0) {
            return false;
        }
        currentState = PistolState.SHOOTING;

        ResetMuzzleFlash();
        //anim.Stop();
        
        anim.Play("Recoil"); // animate
        sfx.Play(); // play sound
        // Show muzzleflash image
        Color color = Color.white;
        color.a = 1;
        sprite.color = color;
        // AoE lighting
        flash.range = 10;
    
        // Debug.Log("BANG!");
        firingDelay = 1.2f;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 500)) { // -forward b/c for some reason the model's z is facing the other way
            if (hit.collider.gameObject.tag == "Enemy") {
                hit.collider.gameObject.GetComponent<Ghoul>().Hit();
            }
        }

        return true;
    }

    public void ResetMuzzleFlash() {
        Color color = Color.white;
        color.a = 0;
        sprite.color = color;
        flash.range = 0;
    }

    public void ResetAnim() {
        currentState = PistolState.NORMAL;
        //anim.Stop();
        anim.Play("Normal");
    }

    public void Aimed() {
        currentState = PistolState.AIMED;
        anim.Play("Aimed");
    }
}
