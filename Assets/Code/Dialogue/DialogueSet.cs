using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpeakerPortrait {
    portrait0,
    portrait1
}




[System.Serializable]
public class DialogueLine {
    public SpeakerPortrait portrait;
    public int lineIndex;
    public string speaker;
    public string speakerLines;
    public bool canShow;
}

