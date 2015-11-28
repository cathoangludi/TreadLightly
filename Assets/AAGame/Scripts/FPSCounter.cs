using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 The class is used for debugging purposes and displays information about FPS.
 
 Average FPS represents the average time it took to process a frame.
 Min FPS represents the longest time it took to process a frame. 
 Max FPS represents the shortest time it took to process a frame. 
 
 (All of the times are converted into FPS)

 The information is based and updated off the last 60 frames.
 */
public class FPSCounter : MonoBehaviour {
    public Text text;
    const int interval = 60;

    //FPS calculation state
    int updateCounter;
    float averageFPS;
    float minFPS;
    float maxFPS;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        resetState();
        text.text = "FPS-Display"
              + "\nAvg: -"
              + "\nMin: -"
              + "\nMax: -";
	}

    void Update()
    {
        //Calculate 
        float currentFPS = 1 / Time.deltaTime;

        //store running total in averageFPS
        averageFPS += currentFPS;

        //get min
        if (currentFPS < minFPS) {
            minFPS = currentFPS;
        }

        //get max
        if (currentFPS > maxFPS){
            maxFPS = currentFPS;
        }

        //update text here
        if (updateCounter++ >= interval)
        {
            averageFPS /= interval;

            text.text = "FPS-Display"
              + "\nAvg: " + (int)averageFPS
              + "\nMin: " + (int)minFPS
              + "\nMax: " + (int)maxFPS;

            resetState();
        }
    }

    private void resetState()
    {
        averageFPS = 0;
        minFPS = float.MaxValue;
        maxFPS = float.MinValue;
        updateCounter = 0;
    }
}
