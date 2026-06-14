using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Gambler : Role
{
    Character chRef;
    private Il2CppSystem.Action action1;
    private Il2CppSystem.Action action2;
    private Il2CppSystem.Action action3;
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo("", null);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("", null);
    }
    public override string Description
    {
        get
        {
            return "Learns if a character is a Villager.";
        }
    }
    private string boringStatement()
    {
        Il2CppSystem.Collections.Generic.List<string> boringStrings = new Il2CppSystem.Collections.Generic.List<string>();
        boringStrings.Add("These odds are boring");
        boringStrings.Add("Where's the fun in these odds?");
        boringStrings.Add("I don't like these odds");
        boringStrings.Add("I don't think I like these odds");
        boringStrings.Add("What are you expecting me to do here?");
        boringStrings.Add("I'm sorry, these odds are awful");
        boringStrings.Add("These aren't good odds");
        boringStrings.Add("Mm, nah, I don't like these odds");
        boringStrings.Add("Leave some of us unrevealed next time, 'kay?");
        boringStrings.Add("<i>Booooriiiing!</i>");
        string boringString = boringStrings[UnityEngine.Random.RandomRangeInt(0, boringStrings.Count)];
        return boringString;
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        Il2CppSystem.Collections.Generic.List<Character> unrevealedChars = new Il2CppSystem.Collections.Generic.List<Character>();
        unrevealedChars = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
        if (unrevealedChars.Count == 0)
        {
            onActed?.Invoke(new ActedInfo(boringStatement(), null));
            return;
        }
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        Il2CppSystem.Collections.Generic.List<Character> unrevealedChars = new Il2CppSystem.Collections.Generic.List<Character>();
        unrevealedChars = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
        if (unrevealedChars.Count == 0)
        {
            onActed?.Invoke(new ActedInfo(boringStatement(), null));
            return;
        }
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action3;
        CharacterPicker.OnStopPick += action2;
    }
    private void CharacterPicked()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> newList2 = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> newList3 = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        newList = Characters.Instance.FilterHiddenCharacters(characters);
        newList = Characters.Instance.FilterAliveCharacters(newList);
        newList2 = Characters.Instance.FilterAliveCharacters(characters);
        newList2.Remove(chRef);

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);

        if (!(newList2.Contains(chars[0])))
        {
            return;
        }

        string info = $"";
        Health health = PlayerController.PlayerInfo.health;
        if (chars[0].GetRegisterAs().type == ECharacterType.Villager)
        {
            health.Damage(newList.Count);
            EAlignment targetAlignment = chars[0].alignment;
            chars[0].ChangeAlignment(EAlignment.Good);
            chars[0].statuses.AddStatus(MainMod.HiddenRoleStatus.hiddenRole, charRef);
            chars[0].KillByDemon(chRef);
            chars[0].ChangeAlignment(targetAlignment);
            // chars[0].statuses.AddStatus(GamblerText.gamblerKill, chRef);
            info = string.Format("#{0} is a Villager.\nI lost {1} health.", chars[0].id, newList.Count);
        }
        else
        {
            health.Heal(newList.Count*2);
            EAlignment targetAlignment = chars[0].alignment;
            chars[0].ChangeAlignment(EAlignment.Good);
            chars[0].statuses.AddStatus(MainMod.HiddenRoleStatus.hiddenRole, charRef);
            chars[0].KillByDemon(chRef);
            chars[0].ChangeAlignment(targetAlignment);
            // chars[0].statuses.AddStatus(GamblerText.gamblerKill, chRef);
            info = string.Format("#{0} is not a Villager.\nI gained {1} health.", chars[0].id, newList.Count*2);
        }

        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> newList2 = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> newList3 = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        newList = Characters.Instance.FilterHiddenCharacters(characters);
        newList = Characters.Instance.FilterAliveCharacters(newList);
        newList2 = Characters.Instance.FilterAliveCharacters(characters);
        newList2.Remove(chRef);

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);

        if (!(newList2.Contains(chars[0])))
        {
            return;
        }

        string info = $"";
        Health health = PlayerController.PlayerInfo.health;
        if (chars[0].GetRegisterAs().type != ECharacterType.Villager)
        {
            health.Damage(newList.Count);
            EAlignment targetAlignment = chars[0].alignment;
            chars[0].ChangeAlignment(EAlignment.Good);
            chars[0].statuses.AddStatus(MainMod.HiddenRoleStatus.hiddenRole, charRef);
            chars[0].KillByDemon(chRef);
            chars[0].ChangeAlignment(targetAlignment);
            // chars[0].statuses.AddStatus(GamblerText.gamblerKill, chRef);
            info = string.Format("#{0} is a Villager.\nI lost {1} health.", chars[0].id, newList.Count);
        }
        else
        {
            health.Heal(newList.Count*2);
            EAlignment targetAlignment = chars[0].alignment;
            chars[0].ChangeAlignment(EAlignment.Good);
            chars[0].statuses.AddStatus(MainMod.HiddenRoleStatus.hiddenRole, charRef);
            chars[0].KillByDemon(chRef);
            chars[0].ChangeAlignment(targetAlignment);
            // chars[0].statuses.AddStatus(GamblerText.gamblerKill, chRef);
            info = string.Format("#{0} is not a Villager.\nI gained {1} health.", chars[0].id, newList.Count*2);
        }

        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_Gambler() : base(ClassInjector.DerivedConstructorPointer<w_Gambler>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_Gambler(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    /*
    public static class GamblerText
    {
        public static ECharacterStatus gamblerKill = (ECharacterStatus)975;
        [HarmonyPatch(typeof(Character), nameof(Character.ShowDescription))]
        public static class ChangeKillByDemonText
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.killedByDemon && __instance.statuses.Contains(gamblerKill))
                {
                    HintInfo info = new HintInfo();
                    info.text = "Killed by the Gambler.\nNo extra info is yielded.";
                    UIEvents.OnShowHint.Invoke(info, __instance.hintPivot);
                }
            }
        }
    }
    */
}