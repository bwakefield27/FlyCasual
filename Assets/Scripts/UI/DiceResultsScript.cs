﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Todo: Move to different scripts by menu names

public delegate void DiceModification();

public class DiceResultsScript: MonoBehaviour {

    private GameManagerScript Game;

    public GameObject panelDiceResultsMenu;
    public GameObject prefabDiceModificationButton;

    void Start()
    {
        Game = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    public void ShowDiceResultMenu()
    {
        panelDiceResultsMenu.SetActive(true);
        Game.Selection.ActiveShip.GenerateDiceModificationButtons();
    }

    public void ShowDiceModificationButtons()
    {
        float offset = 0;
        Vector3 defaultPosition = panelDiceResultsMenu.transform.position + new Vector3(5, 195, 0);
        foreach (var diceModification in Game.Selection.ActiveShip.AvailableDiceModifications)
        {
            GameObject newButton = Instantiate(prefabDiceModificationButton, panelDiceResultsMenu.transform);
            newButton.name = "Button" + diceModification.Key;
            newButton.transform.GetComponentInChildren<Text>().text = diceModification.Key;
            newButton.GetComponent<RectTransform>().position = defaultPosition + new Vector3(0, -offset, 0);
            offset += 40;
            newButton.GetComponent<Button>().onClick.AddListener(delegate {
                diceModification.Value.Invoke();
                newButton.GetComponent<Button>().interactable = false;
            });
            newButton.GetComponent<Button>().interactable = true;
            newButton.SetActive(true);
        }

        panelDiceResultsMenu.transform.Find("Confirm").gameObject.SetActive(true);
    }

    public void HideDiceModificationButtons()
    {
        foreach (Transform button in panelDiceResultsMenu.transform)
        {
            if (button.name.StartsWith("Button"))
            {
                MonoBehaviour.Destroy(button.gameObject);
            }
        }
        panelDiceResultsMenu.transform.Find("Confirm").gameObject.SetActive(false);
    }

    public void ConfirmDiceResult()
    {
        HideDiceResultMenu();
        
        if (Game.Combat.AttackStep == CombatStep.Attack)
        {
            Game.Combat.PerformDefence(Game.Selection.ThisShip, Game.Selection.AnotherShip);
        }
        else if ((Game.Combat.AttackStep == CombatStep.Defence))
        {
            //TODO: Show compare results dialog
            Game.Combat.CalculateAttackResults(Game.Selection.ThisShip, Game.Selection.AnotherShip);

            Game.Combat.ReturnRangeRuler();

            if (Game.Roster.NoSamePlayerAndPilotSkillNotAttacked(Game.Selection.ThisShip))
            {
                Game.PhaseManager.CurrentPhase.NextSubPhase();
            }

        }
    }

    private void HideDiceResultMenu()
    {
        panelDiceResultsMenu.SetActive(false);
        HideDiceModificationButtons();
        Game.Combat.CurentDiceRoll.RemoveDiceModels();
    }

}
