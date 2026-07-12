using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using HarmonyLib;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_SlayerRework : Role
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
            return "Kill a character, then Learn their alignment.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger != ETriggerPhase.Day) return;
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

        bool canKill = true;
        if (chars[0].bluff)
        {
            if (chars[0].bluff.characterId == "Slayer_WING")
            {
                canKill = false;
            }
        }
        else if (chars[0].dataRef.characterId == "Slayer_WING")
        {
            canKill = false;
        }

            string info = $"";
        Health health = PlayerController.PlayerInfo.health;
        if (chars[0].GetRegisterAlignment() == EAlignment.Good)
        {
            if (canKill)
            {
                EAlignment targetAlignment = chars[0].alignment;
                chars[0].ChangeAlignment(EAlignment.Good);
                chars[0].statuses.AddStatus(MainMod.HiddenRoleStatus.hiddenRole, charRef);
                chars[0].KillByDemon(chRef);
                // chars[0].ChangeAlignment(targetAlignment); For now leave them Good to prevent redtext
            }
            if (chars[0].state == ECharacterState.Dead)
            {
                // chars[0].statuses.AddStatus(SlayerReworkText.slayerKill, chRef);
                info = string.Format("I killed Good at #{0}", chars[0].id, newList.Count);
                health.Damage(2);
            }
            else
            {
                // chars[0].statuses.statuses.Remove(MainMod.HiddenRoleStatus.hiddenRole);
                info = string.Format("I attacked Good at #{0}", chars[0].id, newList.Count);
                health.Damage(2);
            }
        }
        else
        {
            if (canKill)
            {
                EAlignment targetAlignment = chars[0].alignment;
                chars[0].ChangeAlignment(EAlignment.Good);
                chars[0].statuses.AddStatus(MainMod.HiddenRoleStatus.hiddenRole, charRef);
                chars[0].KillByDemon(chRef);
                chars[0].ChangeAlignment(targetAlignment);
            }
            if (chars[0].state == ECharacterState.Dead)
            {
                // chars[0].statuses.AddStatus(SlayerReworkText.slayerKill, chRef);
                info = string.Format("I killed Evil at #{0}", chars[0].id, newList.Count);
                health.Damage(1);
            }
            else
            {
                chars[0].statuses.statuses.Remove(MainMod.HiddenRoleStatus.hiddenRole);
                info = string.Format("I attacked Evil at #{0}", chars[0].id, newList.Count);
                health.Damage(1);
            }
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

        bool canKill = true;
        if (chars[0].bluff)
        {
            if (chars[0].bluff.characterId == "Slayer_WING")
            {
                canKill = false;
            }
        }
        else if (chars[0].dataRef.characterId == "Slayer_WING")
        {
            canKill = false;
        }

        string info = $"";
        Health health = PlayerController.PlayerInfo.health;
        if (chars[0].GetRegisterAlignment() == EAlignment.Good)
        {
            if (canKill)
            {
                EAlignment targetAlignment = chars[0].alignment;
                chars[0].ChangeAlignment(EAlignment.Good);
                chars[0].statuses.AddStatus(MainMod.HiddenRoleStatus.hiddenRole, charRef);
                chars[0].KillByDemon(chRef);
                // chars[0].ChangeAlignment(targetAlignment); For now leave them Good to prevent redtext
            }
            if (chars[0].state == ECharacterState.Dead)
            {
                // chars[0].statuses.AddStatus(SlayerReworkText.slayerKill, chRef);
                info = string.Format("I killed Evil at #{0}", chars[0].id, newList.Count);
                health.Damage(1);
            }
            else
            {
                info = string.Format("I attacked Evil at #{0}", chars[0].id, newList.Count);
            }
        }
        else
        {
            if (canKill)
            {
                EAlignment targetAlignment = chars[0].alignment;
                chars[0].ChangeAlignment(EAlignment.Good);
                chars[0].statuses.AddStatus(MainMod.HiddenRoleStatus.hiddenRole, charRef);
                chars[0].KillByDemon(chRef);
                chars[0].ChangeAlignment(targetAlignment);
            }
            if (chars[0].state == ECharacterState.Dead)
            {
                // chars[0].statuses.AddStatus(SlayerReworkText.slayerKill, chRef);
                info = string.Format("I killed Good at #{0}", chars[0].id, newList.Count);
                health.Damage(2);
            }
            else
            {
                info = string.Format("I attacked Good at #{0}", chars[0].id, newList.Count);
                health.Damage(1);
            }
        }

        LegionLoss.checkExe(chars[0]);
        w_Minos.MinosLoss.checkExe(chars[0]);

        onActed?.Invoke(new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    private static Character GetRandomGoodCharacter()
    {
        Il2CppSystem.Collections.Generic.List<Character> GoodCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character c in Gameplay.CurrentCharacters)
        {
            if (c.alignment == EAlignment.Good)
            {
                GoodCharacters.Add(c);
            }
        }
        return GoodCharacters[UnityEngine.Random.RandomRangeInt(0, GoodCharacters.Count)];
    }
    public w_SlayerRework() : base(ClassInjector.DerivedConstructorPointer<w_SlayerRework>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_SlayerRework(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    /*
    public static class SlayerReworkText
    {
        public static ECharacterStatus slayerKill = (ECharacterStatus)976;
        [HarmonyPatch(typeof(Character), nameof(Character.ShowDescription))]
        public static class ChangeKillByDemonText
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.killedByDemon && __instance.statuses.Contains(slayerKill))
                {
                    HintInfo info = new HintInfo();
                    info.text = "Killed by the Convict.\nTrue Role is not revealed.";
                    UIEvents.OnShowHint.Invoke(info, __instance.hintPivot);
                }
            }
        }
    }
    */
}