using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour {

    FPSPistol gun;
    FirstPersonController playerScript;
    Animator anim;
    public int health;
    private bool m_Interact;
    private bool m_Shoot;
    private bool m_aim;
    private bool is_dead;
    public bool hasGun;
    public float firingDelay;
    [SerializeField] private int ammo;

    // Use this for initialization
    void Start () {
        gun = GameObject.Find("/FPSController/FirstPersonCharacter/FPSPistol").GetComponent<FPSPistol>();
        playerScript = GameObject.Find("/FPSController").GetComponent<FirstPersonController>();
        anim = GetComponent<Animator>();
        ammo = 5;
        health = 15;
        anim.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        CheckInput();
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 3;
        Debug.DrawRay(transform.position, forward, Color.green);
    }

    void FixedUpdate() {

        if (!hasGun || is_dead) return;
        if ((m_aim && gun.currentState == FPSPistol.PistolState.NORMAL) || (m_aim && gun.currentState == FPSPistol.PistolState.AIMING))  {
            Aim();
        } else if (m_aim && gun.currentState == FPSPistol.PistolState.AIMED ) {
            Aim();
        } else if (!m_aim && gun.currentState == FPSPistol.PistolState.AIMED) {
            Release();
        }
    }

    void CheckInput() {
        m_Interact = CrossPlatformInputManager.GetButtonDown("Interact");
      
        m_Shoot = CrossPlatformInputManager.GetButtonDown("Fire1");

        m_aim = CrossPlatformInputManager.GetAxis("Fire2") > 0; ;
    }


    public Collider CheckRay() {

        // ugh hate this arrow code.. 
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3)) {
            if (m_Interact) { // if E is pressed
                if (hit.collider.gameObject.tag == "Item") {
                    hit.collider.gameObject.GetComponent<Item>().Collect() ;
                    return null;
                }  else if (hit.collider.gameObject.tag == "Portal") {
                    hit.collider.gameObject.GetComponent<Portal>().Enter();
                    return null;
                }
            }
            return hit.collider;
        }
        return null;
    }

    public void Shoot() {
        if (gun.Shoot()) {
            ammo--;
        }
    }

    public void Aim() {
        gun.Aim();
        if (m_Shoot && gun.currentState == FPSPistol.PistolState.AIMED && ammo > 0) {
            Shoot();
        }
    }
    public void Release() {
        gun.Release();
    }

    public void AddAmmo(int amount) {
        ammo += amount;
    }

    public bool isDead() {
        return is_dead;
    }

    public void Damage(int amount) {
        if (is_dead) return;
        anim.enabled = true;
        health -= amount;
        if (health <= 0) {
            is_dead = true;
            anim.Play("Death");
            playerScript.enabled = false;
            return;
        }
        anim.Play("Hurt");


    }

    public void DisableAnim() {
        anim.enabled = false;
    }
}
