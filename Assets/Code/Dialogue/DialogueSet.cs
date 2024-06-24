using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpeakerPortrait {
    placeholder0,
    placeholder1,
    SelectedPhoto1,
    SelectedPhoto2,
    SelectedPhoto3,
    pato_bravo,
    pato_transtornado,
    pato_questionando,
    pato_derrotado,
    pato_envergonhado,
    pato_neutro,
    pato_neutro_olhando_jogador,
    pato_puto,
    pato_superior,
    pato_tenso,
    pardais_irritados,
    pardal_assustado,
    pardal_irritado,
    pardal_neutro,
    pardal_risada,
    pombo_neutro,
    pombo_vangloriando,
    cisne_agradecida,
    cisne_apaixonada,
    cisne_irritada,
    cisne_medo,
    cisne_neutra,
    narrator,
    rato_desprezo,
    rato_machucado,
    rato_muito_serio_machucado,
    rato_neutro,
    rato_silhueta,
    rato_serio_machucado,
}




[System.Serializable]
public class DialogueLine {
    public SpeakerPortrait portrait;
    public int lineIndex;
    public string speaker;
    public string speakerLines;
    public bool canShow;
}

