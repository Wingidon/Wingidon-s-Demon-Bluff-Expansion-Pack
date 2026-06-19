using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using Il2CppTMPro;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Minos : Demon
{
    public bool minosFirstBlood = false;
    public override Il2CppSystem.Collections.Generic.List<SpecialRule> GetRules()
    {
        Il2CppSystem.Collections.Generic.List<SpecialRule> sr = new Il2CppSystem.Collections.Generic.List<SpecialRule>();
        sr.Add(new NightModeRule(2));
        return sr;
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            Health health = PlayerController.PlayerInfo.health;
            health.AddMaxHp(Gameplay.CurrentCharacters.Count * 2);
            health.AddMaxHp(-15);
            health.Heal(100);
            minosFirstBlood = false;
        }
        if (trigger == ETriggerPhase.Night)
        {
            if (charRef.state == ECharacterState.Dead) return;
            Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
            Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> revealedChars = new Il2CppSystem.Collections.Generic.List<Character>();
            Characters charInst = Characters.Instance;
            foreach (Character character in characters)
            {
                newList.Add(character);
            }
            newList.Remove(charRef);
            newList = Characters.Instance.FilterAliveCharacters(newList);
            newList = Characters.Instance.FilterAlignmentCharacters(newList, EAlignment.Good);
            newList = Characters.Instance.FilterCharacterMissingStatus(newList, ECharacterStatus.KilledByEvil);
            revealedChars = Characters.Instance.FilterRevealedCharacters(Gameplay.CurrentCharacters);
            Health health = PlayerController.PlayerInfo.health;
            health.Damage(2);
            if (!(newList.Count == 0))
            {
                Character myTarget = newList[UnityEngine.Random.Range(0, newList.Count)];
                if (minosFirstBlood == false)
                {
                    myTarget.actedInfos.Add(new ActedInfo(minosKillFlavour(), null));
                    myTarget.ShowActed(myTarget.actedInfos[myTarget.actedInfos.Count - 1], ETriggerPhase.Day);
                    // OnActed(ETriggerPhase.Day, myTarget, new ActedInfo(minosKillFlavour(), null));
                    minosFirstBlood = true;
                }
                myTarget.Reveal();
                myTarget.onReveal.Invoke();
                if (!myTarget.statuses.Contains(ECharacterStatus.UnkillableByDemon))
                {
                    myTarget.RevealReal();
                    myTarget.statuses.AddStatus(ECharacterStatus.KilledByEvil, charRef);
                    myTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                    myTarget.statuses.AddStatus(MinosKill.minosKill, charRef);
                    myTarget.KillByDemon(charRef);
                    MinosLoss.checkExe(myTarget);
                    if (myTarget.dataRef.picking)
                    {
                        myTarget.pickable.SetActive(false);
                    }
                }
                else
                {
                    if (myTarget.dataRef.picking)
                    {
                        myTarget.pickable.SetActive(true);
                    }
                    else
                    {
                        myTarget.Act(ETriggerPhase.Day);
                    }
                }
            }
        }
    }
    private string minosKillFlavour()
    {
        Il2CppSystem.Collections.Generic.List<string> possibleRemarks = new Il2CppSystem.Collections.Generic.List<string>();
        /*
        possibleRemarks.Add("Ow!");
        possibleRemarks.Add("Ow, rude!");
        possibleRemarks.Add("Gah!");
        */
        possibleRemarks.Add("Play its game for now...");
        /*
        possibleRemarks.Add("OW!");
        possibleRemarks.Add("The hell...?");
        possibleRemarks.Add("What the-");
        possibleRemarks.Add("That Demon is one sneaky rascal");
        possibleRemarks.Add("Need some help over here!");
        possibleRemarks.Add("Could use some help over here!");
        possibleRemarks.Add("What are you-");
        possibleRemarks.Add("Something's wrong");
        possibleRemarks.Add("Something feels wrong here");
        */
        return possibleRemarks[UnityEngine.Random.RandomRangeInt(0, possibleRemarks.Count)];
    }
    public w_Minos() : base(ClassInjector.DerivedConstructorPointer<w_Minos>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Minos(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

        return bluff;
    }
    
    public static class MinosKill
    {
        public static ECharacterStatus minosKill = (ECharacterStatus)133;
        [HarmonyPatch(typeof(Character), nameof(Character.ShowDescription))]
        public static class ChangeKillByDemonText
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.killedByDemon && __instance.statuses.Contains(minosKill))
                {
                    HintInfo info = new HintInfo();
                    info.text = "Killed by <color=#FF9999>Sanguitaurus</color>\nCannot use abilities.";
                    UIEvents.OnShowHint.Invoke(info, __instance.hintPivot);
                }
            }
        }
    }
    
    
    public static class MinosLoss
    {
        public static WinConditions winConditions;
        public static GameObject minosLoss;
        public static void getWinCons()
        {
            GameObject winCon = GameObject.Find("Game/Gameplay/Content/WinConditions");
            winConditions = winCon.GetComponent<WinConditions>();
            System.Action<Character> action = new System.Action<Character>(checkExe);
            GameplayEvents.OnCharacterKilled += action;
            minosLoss = GameObject.Instantiate(winConditions.autoLose);
            GameObject minosLossNote = minosLoss.transform.FindChild("Note/Text (TMP)").gameObject;
            TextMeshProUGUI minosLossText = minosLossNote.GetComponent<TextMeshProUGUI>();
            minosLossText.text = "<color=red>Sanguitaurus Wins</color>\n\nAll Good characters have died.";
        }
        public static void checkExe(Character ch)
        {
            UnityEngine.Debug.Log("Checking if Good should lose due to Sanguitaurus...");
            bool goodLives = false;
            bool minosInPlay = false;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.alignment == EAlignment.Good && character.state != ECharacterState.Dead && character != ch)
                {
                    UnityEngine.Debug.Log(string.Format("Found an alive Good character at #{0}", character.id));
                    goodLives = true;
                }
                if (character.dataRef.name.ToString() == "Sanguitaurus")
                {
                    minosInPlay = true;
                }
            }
            if (!goodLives && minosInPlay)
            {
                UnityEngine.Debug.Log("No Good characters live. Initiating Sanguitaurus loss.");
                Health health = PlayerController.PlayerInfo.health;
                health.Damage(9999);
                //ch.RevealAllReal();
                //getWinCons();
                //winConditions.Lose();
                //minosLose();
            }
        }
        public static void minosLose()
        {
            minosLoss.SetActive(true);
            winConditions.Lose();
        }
    }

}


