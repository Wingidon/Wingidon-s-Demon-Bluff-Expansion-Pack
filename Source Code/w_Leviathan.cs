using HarmonyLib;
using Il2Cpp;
using Il2CppFIMSpace.Basics;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using Il2CppTMPro;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Diagnostics;
using static Il2CppSystem.Collections.SortedList;
using static MelonLoader.MelonLogger;
using static UnityEngine.GraphicsBuffer;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Leviathan : Role
{
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
    }
    public w_Leviathan() : base(ClassInjector.DerivedConstructorPointer<w_Leviathan>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Leviathan(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

        return bluff;
    }
}

[HarmonyPatch(typeof(Character), nameof(Character.Act))]
public static class leviathanCheckExecution
{
    public static ECharacterStatus leviathanExecTarg = (ECharacterStatus)1252291208;
    public static void Postfix(Character __instance, ETriggerPhase trigger)
    {
        if (trigger == ETriggerPhase.OnExecuted && !__instance.statuses.Contains(leviathanExecTarg))
        {
            UnityEngine.Debug.Log($"Trigger: {trigger.ToString()}");
            UnityEngine.Debug.Log($"Type: {__instance.dataRef.type.ToString()}");
            UnityEngine.Debug.Log($"Alignment: {__instance.alignment.ToString()}");
            // LeviathanLoss.checkExe(__instance);
            bool leviathanInPlay = false;
            Character leviathanRef = null;

            if (trigger == ETriggerPhase.OnExecuted && __instance.dataRef.type == ECharacterType.Villager && __instance.alignment == EAlignment.Good)
            {
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (character.dataRef.name.ToString() == "Leviathan" && character.state != ECharacterState.Dead)
                    {
                        leviathanInPlay = true;
                        leviathanRef = character;
                    }
                }
                if (leviathanInPlay)
                {
                    foreach (Character character in Gameplay.CurrentCharacters)
                    {
                        if (character.dataRef.name.ToString() == "Leviathan")
                        {
                            character.RevealReal();
                            Il2CppSystem.Collections.Generic.List<string> possibleRemarks = new Il2CppSystem.Collections.Generic.List<string>();
                            possibleRemarks.Add("This ends here.");
                            possibleRemarks.Add("Our time together now comes to an end.");
                            possibleRemarks.Add("This is where we part ways.");
                            possibleRemarks.Add("It is done.");
                            possibleRemarks.Add("This charade ends here.");
                            possibleRemarks.Add("Our time together has come to an end.");
                            possibleRemarks.Add("Kneel before me now, Executioner.");
                            possibleRemarks.Add("Your final mistake.");
                            possibleRemarks.Add("The world trembles before me. Perhaps you should too, Executioner.");
                            possibleRemarks.Add("You cannot use a knife to repel a flood.");
                            possibleRemarks.Add("Tch.");
                            possibleRemarks.Add("You have extended far beyond what any mortal was supposed to see.");
                            possibleRemarks.Add("Put your caution before your audacity and see that we do not meet again, Executioner.");
                            possibleRemarks.Add("<i>The more he withdrew from the world around him, the more powerful became his dreams.</i>");
                            possibleRemarks.Add("<i>With strange aeons, even death may die.</i>\n\nYour time will come soon, Executioner.");
                            string remark = possibleRemarks[UnityEngine.Random.RandomRangeInt(0, possibleRemarks.Count)];
                            character.actedInfos.Add(new ActedInfo(remark, null));
                            character.ShowActed(character.actedInfos[character.actedInfos.Count - 1], ETriggerPhase.Day);
                        }
                        if (character.alignment == EAlignment.Good && character.state != ECharacterState.Dead)
                        {
                            character.statuses.AddStatus(leviathanExecTarg, __instance, character, true);
                        }
                    }
                    foreach (Character character in Gameplay.CurrentCharacters)
                    {
                        if (character.statuses.Contains(leviathanExecTarg))
                        {
                            // character.ExecuteAndReveal();
                            character.RevealReal();
                            character.KillByDemon(leviathanRef);
                            character.statuses.statuses.Remove(ECharacterStatus.KilledByEvil);
                            character.killedByDemon = false;
                        }
                    }
                    Health health = PlayerController.PlayerInfo.health;
                    health.Damage(1000);
                }
            }
        }
    }
}
/*
public static class LeviathanLoss
{
    public static WinConditions winConditions;
    public static GameObject leviathanLoss;
    public static void getWinCons()
    {
        GameObject winCon = GameObject.Find("Game/Gameplay/Content/WinConditions");
        winConditions = winCon.GetComponent<WinConditions>();
        System.Action<Character> action = new System.Action<Character>(checkExe);
        GameplayEvents.OnCharacterKilled += action;
        leviathanLoss = GameObject.Instantiate(winConditions.autoLose);
        GameObject leviathanLossNote = leviathanLoss.transform.FindChild("Note/Text (TMP)").gameObject;
        TextMeshProUGUI leviathanLossText = leviathanLossNote.GetComponent<TextMeshProUGUI>();
        leviathanLossText.text = "<color=red>Leviathan Wins</color>\n\nYou Executed a Villager.";
    }
    public static void checkExe(Character ch)
    {
        UnityEngine.Debug.Log("Checking if Good should lose due to Leviathan...");
        bool leviathanInPlay = false;
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.dataRef.name.ToString() == "Leviathan" && character.state != ECharacterState.Dead)
            {
                leviathanInPlay = true;
            }
        }
        if (leviathanInPlay && ch.dataRef.type == ECharacterType.Villager && ch.alignment == EAlignment.Good)
        {
            UnityEngine.Debug.Log("Villager was Executed. Initiating Leviathan loss.");
            ch.RevealAllReal();
            getWinCons();
            leviathanLose();
        }
    }
    public static void leviathanLose()
    {
        leviathanLoss.SetActive(true);
        winConditions.Lose();
    }
}
*/


