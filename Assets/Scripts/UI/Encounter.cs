using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    [SerializeField]
    List<ActionButton> buttons;
    [SerializeField]
    GameObject content_parent;
    GameMaster gameMaster;

    private void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    public void StartEncounter()
    {
        foreach (var button in buttons)
        {
            bool showButton = true;
            for (int i = 0; i < button.flag_names.Count; i++)
            {
                int index = gameMaster.flag_names.IndexOf(button.flag_names[i]);

                if (gameMaster.flag_values[index] != button.flag_values[i])
                {
                    showButton = false;
                }
            }
            if(showButton)
            {
                Instantiate(button, content_parent.transform);
            }    
        }
    }

    public void StopEncounter()
    {
        ;
    }
}

