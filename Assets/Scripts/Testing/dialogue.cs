using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue : MonoBehaviour
{
    DialogueSystem dialoguetest;

    public class characters {
        public string name;
        public string[] testDialogue;
    }

    characters Tomoya = new characters();
    characters Kyou = new characters();

    // Start is called before the first frame update
    void Start()
    {
        dialoguetest = DialogueSystem.instance;   

        Tomoya.name = "Tomoya";
        Tomoya.testDialogue = new string[] {
            "I hate this town. It’s too filled with memories I’d rather forget. " + 
            "I go to school every day, hang out with my friends, and then go home. " +
            "There’s no place I’d rather not go ever again. I wonder if anything will ever change? Will that day ever come?",
            "If I’m around you, I don’t think I’ll ever be bored."
        };

        Kyou.name = "Kyou";
        Kyou.testDialogue = new string[] {
            "Isn’t there something strange in becoming friends because you’re asked? Friends aren’t given; you’re supposed to make them",
            "If the results come true, it’s as if there’s only one future. If it fails, we can think that other futures exist… I want to believe that in our future there are many possibilities waiting."
        };

    }

    public int index = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(!dialoguetest.isTalking || dialoguetest.isWaitingForUser) {
                if( index >= 2) {
                    return;
                }
            }
            dialoguetest.Say(Tomoya.testDialogue[index], Tomoya.name);
            index++;
        }
    }
}
