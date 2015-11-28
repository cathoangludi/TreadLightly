using UnityEngine;
using UnityEngine.UI;
using System.Collections;

internal class TLUI {

    Text textSubtitles;
    Text textHint;
    private static Animator blackScreen;

    public delegate void Transition();
    public static Transition transition;

    public bool isEntering;
    // Use this for initialization
    public TLUI (Text hintUI, Text textUI, Animator blackScreenAnimator ) {
        textHint = hintUI;
        textSubtitles = textUI;
        blackScreen = blackScreenAnimator;
        isEntering = false;
    }

    public static void FadeInBlack() { blackScreen.Play("FadeInB"); }

    public static void FadeOutBlack() { blackScreen.Play("FadeOutB"); }

    public void Subtitles(string text) {
        textSubtitles.text = text;
    }

    public void Hint(string text) {
        textHint.text = text;
    }
}

