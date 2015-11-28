using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public static Transform player;

    public Portal endPortal;
    [SerializeField] private AudioClip sfxDoor;
	// Use this for initialization
	void Awake () {
        if (player == null) {
            player = GameObject.Find("/FPSController").transform;
        }


    }

    void Start() {
        if (endPortal != null) {
            endPortal.SetEndPortal(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetEndPortal(Portal portal) {
        endPortal = portal;
    }

    public void Enter() {
        TLUI.transition = ChangeLocation;
        AudioSource.PlayClipAtPoint(sfxDoor, player.transform.position, 0.2f);
        TLUI.FadeOutBlack();
    }

    public void ChangeLocation() {
        player.transform.position = endPortal.transform.position;
        player.transform.rotation = endPortal.transform.rotation;
    }
}
