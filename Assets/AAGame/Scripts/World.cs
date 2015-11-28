using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class World : MonoBehaviour {

    Player player;
    TLUI gameUI;

    public GameObject ghoul;
    ArrayList enemies;


    void Awake() {
        Application.targetFrameRate = 60;
        player = GameObject.Find("/FPSController/FirstPersonCharacter").GetComponent<Player>();
        gameUI = new TLUI(GameObject.Find("/Canvas/Hint").GetComponent<Text>(),
                          GameObject.Find("/Canvas/Subtitles").GetComponent<Text>(),
                          GameObject.Find("/Canvas/BlackScreen").GetComponent<Animator>());
        Ghoul.player = player.transform;

    }
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        enemies = new ArrayList();

        // Subtitles("Huh? Where am I?");
        // Hint("Press [E] to open Door");
        // gameUI.FadeInBlack();

    }

	void Update () {

       // Sample of event... I leave it to you..
       if (Time.timeSinceLevelLoad > 5 && Time.timeSinceLevelLoad < 10) {
            Subtitles("Huh?... what am I doing here?");
       } else if (Time.timeSinceLevelLoad > 15 && Time.timeSinceLevelLoad < 20) {
            Subtitles("Those pesky programmers must be at it again..");
       } else {
            Subtitles("");
       }
       

	
	}

    void FixedUpdate() {
        if (player.CheckRay() != null) {
            switch (player.CheckRay().gameObject.tag) {
                case "Item":
                    gameUI.Hint("Press [E] to pickup " + player.CheckRay().gameObject.name);
                    break;
                case "Portal":
                    gameUI.Hint("Press [E] to Enter");
                    break;
                default:
                    gameUI.Hint("");
                    break;
            }
        } else {
            gameUI.Hint("");
        }
    }

    void Subtitles(string text) {
        gameUI.Subtitles(text);
    }

    void Hint(string text) {
        gameUI.Hint(text);
    }

    public void FadeInBlack() {
        TLUI.FadeInBlack();
    }

    public void FadeOutBlack() {
        TLUI.FadeOutBlack();
    }


    void SpawnGhoul(float x_, float y_, float z_, Quaternion rotation_) {
        GameObject tmpGhoul = Instantiate(ghoul, new Vector3(x_, y_, z_), rotation_) as GameObject;
        enemies.Add(tmpGhoul);
    }
}
