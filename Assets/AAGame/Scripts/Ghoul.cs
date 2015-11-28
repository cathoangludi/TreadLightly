using UnityEngine;
using System.Collections;

public class Ghoul : MonoBehaviour {

    /* not yet used.. maybe someday..
    private enum GhoulState {
        IDLE,
        WANDER,
        CHASE,
        ATTACK
    }
    */

    public static Transform player;
    private Player playerScript;
    private static float rotationDamping = 2.0f;
    private static float moveSpeed = 2.0f;
    private Animator anim;
    private int hp;
    private bool isDead;
    private AudioSource sfx;
    private float stepDelay;
    private float playerDistance;

    [SerializeField] private AudioClip m_Moan;          
    [SerializeField] private AudioClip m_Attack;
    [SerializeField] private AudioClip m_Footsteps;


    // private GhoulState currentState;

	// Use this for initialization
	void Start () {
        // currentState = GhoulState.IDLE;
        anim = GetComponent<Animator>();
        hp = 2;
        isDead = false;
        sfx = GetComponent<AudioSource>();
        stepDelay = 1.0f;
        playerScript = player.gameObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        stepDelay -= Time.deltaTime;
        if (!isDead) {
            StickToGround();
            playerDistance = Vector3.Distance(player.position, transform.position);
            if (playerDistance < 15.0f) {
                Glare();
            }

            if (playerDistance < 12.0f && playerDistance > 3.0f) {
                Stalk();
            } else if (playerDistance <= 3.0f) {
                Attack();
            }
        }



    }

    void StickToGround() {
        // LOL im not sure if theres a better way
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 100)) {
            transform.position = new Vector3(transform.position.x, transform.position.y - hit.distance, transform.position.z);
        }

    }
    public void Hit() {
        hp--;
        if (hp <= 0) {
            if(!isDead) { 
                Die();
            }
        }
    }

    void Glare() {
        Vector3 adjust = new Vector3(0, transform.position.y - player.position.y, 0); // because player center is on head, ghoul center is on hip .. -.-
        Quaternion rotation = Quaternion.LookRotation(player.position + adjust - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    void Stalk() {
        anim.Play("walk");
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (stepDelay <= 0) {
            sfx.clip = m_Footsteps;
            sfx.PlayOneShot(sfx.clip);
            stepDelay = 0.7f;
        }
       
    }

    void Attack() {
        anim.Play("attack1");
    }

    void Die() {
        isDead = true;
        sfx.clip = m_Moan;
        sfx.Play();
        anim.Play("die");
    }

    public void Damage() {
        if (playerDistance <= 3.0f) {
            playerScript.Damage(3);
        }

    }
}
