﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:
// Correct work with combat subphase
// What if there is no another frienly ships&
// What if I do not want assign token
// What revert should be done if selected ship does not fulfill all requirements

namespace Ship
{
    namespace XWing
    {
        public class GarvenDreis : XWing
        {
            public GarvenDreis() : base()
            {
                PilotName = "Garven Dreis";
                ImageUrl = "https://vignette3.wikia.nocookie.net/xwing-miniatures/images/f/f8/Garven-dreis.png";
                IsUnique = true;
                PilotSkill = 6;
                Cost = 26;
            }

            public override void InitializePilot()
            {
                base.InitializePilot();
                AfterTokenIsSpent += GarvenDreisPilotAbility;
            }

            private void GarvenDreisPilotAbility(System.Type type)
            {
                if (type == typeof(Tokens.FocusToken))
                {
                    Debug.Log("Garven Dreis Trigger!");
                    Selection.ActiveShip = Selection.ThisShip;
                    Phases.StartTemporarySubPhase("Place Focus token to another friendly ship at range 1-2", typeof(SubPhases.GarvenDreisAbilitySubPhase));
                }
            }

        }
    }
}

namespace SubPhases
{

    public class GarvenDreisAbilitySubPhase : SelectShipSubPhase
    {

        public override void Prepare()
        {
            isFriendlyAllowed = true;
            maxRange = 2;

            finishAction = AssignFocusToken;
        }

        private void AssignFocusToken()
        {
            TargetShip.AssignToken(new Tokens.FocusToken());
        }

        protected override void RevertSubPhase()
        {
            Selection.ThisShip.RemoveAlreadyExecutedAction(typeof(ActionsList.TargetLockAction));
            base.RevertSubPhase();
            Actions.ShowActionsPanel();
        }

    }

}