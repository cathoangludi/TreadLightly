using UnityEngine;
using System.Collections;

public class FOTrans : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Go() {
        TLUI.transition();
        TLUI.FadeInBlack();
    }
}
