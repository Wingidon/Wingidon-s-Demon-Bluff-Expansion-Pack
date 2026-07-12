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

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Cardshark : Role
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
            return "Picks 3, heals or damages based on Villagers picked";
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
        CharacterPicker.Instance.StartPickCharacters(3, charRef);
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
        CharacterPicker.Instance.StartPickCharacters(3, charRef);
        CharacterPicker.OnCharactersPicked += action3;
        CharacterPicker.OnStopPick += action2;
    }
    private void CharacterPicked()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> pickedCharsInOrder = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(CharacterPicker.PickedCharacters[0]);
        selection.Add(CharacterPicker.PickedCharacters[1]);
        selection.Add(CharacterPicker.PickedCharacters[2]);
        sortByID(selection, pickedCharsInOrder);
        Health health = PlayerController.PlayerInfo.health;
        int netHealth = 0;
        int villagersFound = 0;
        foreach (Character character in pickedCharsInOrder)
        {
            if (character.GetCharacterType() == ECharacterType.Villager)
            {
                netHealth -= 1;
                villagersFound++;
            }
            else
            {
                netHealth += 1;
            }
        }
        if (netHealth < 0)
        {
            health.Damage(netHealth * -1);
        }
        else
        {
            health.Heal(netHealth);
        }
        string info = ConjureInfo(pickedCharsInOrder, netHealth, villagersFound);
        onActed?.Invoke(new ActedInfo(info, selection));
        Debug.Log($"{info}");
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> pickedCharsInOrder = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(CharacterPicker.PickedCharacters[0]);
        selection.Add(CharacterPicker.PickedCharacters[1]);
        selection.Add(CharacterPicker.PickedCharacters[2]);
        sortByID(selection, pickedCharsInOrder);
        Health health = PlayerController.PlayerInfo.health;
        int netHealth = 0;
        int villagersFound = 0;
        foreach (Character character in pickedCharsInOrder)
        {
            if (character.GetCharacterType() == ECharacterType.Villager)
            {
                villagersFound++;
            }
        }
        Il2CppSystem.Collections.Generic.List<int> lieNumbers = new Il2CppSystem.Collections.Generic.List<int>();
        if (villagersFound != 0)
        {
            lieNumbers.Add(0);
            lieNumbers.Add(0);
        }
        if (villagersFound != 1)
        {
            lieNumbers.Add(1);
            lieNumbers.Add(1);
            lieNumbers.Add(1);
            lieNumbers.Add(1);
        }
        if (villagersFound != 2)
        {
            lieNumbers.Add(2);
            lieNumbers.Add(2);
            lieNumbers.Add(2);
            lieNumbers.Add(2);
            lieNumbers.Add(2);
            lieNumbers.Add(2);
            lieNumbers.Add(2);
        }
        if (villagersFound != 3)
        {
            lieNumbers.Add(3);
            lieNumbers.Add(3);
        }
        int myLieNumber = lieNumbers[UnityEngine.Random.RandomRangeInt(0, lieNumbers.Count)];
        switch (myLieNumber)
        {
            case 0:
                health.Heal(3);
                netHealth = 3;
                break;
            case 1:
                health.Heal(1);
                netHealth = 1;
                break;
            case 2:
                health.Damage(1);
                netHealth = -1;
                break;
            case 3:
                health.Damage(3);
                netHealth = -3;
                break;
            default:
                health.Damage(3);
                netHealth = -3;
                break;
        }
        string info = ConjureInfo(pickedCharsInOrder, netHealth, myLieNumber);
        onActed?.Invoke(new ActedInfo(info, selection));
        Debug.Log($"{info}");
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_Cardshark() : base(ClassInjector.DerivedConstructorPointer<w_Cardshark>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_Cardshark(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }

    public Il2CppSystem.Collections.Generic.List<Character> sortByID(Il2CppSystem.Collections.Generic.List<Character> inputList, Il2CppSystem.Collections.Generic.List<Character> outputList)
    {
        int checkedID = 0;
        int failsafeLoop = 0;
        while (outputList.Count != inputList.Count && failsafeLoop < 100)
        {
            checkedID++;
            foreach (Character character in inputList)
            {
                if (checkedID == character.id) outputList.Add(character);
            }
        }
        return outputList;
    }

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> sortedChars, int netHealth, int villagerCount)
    {
        string info = "Cardshark's ConjureInfo isn't returning the correct value, please report this to Wingidon.";
        string healthChangeInfo = "Cardshark's healthChangeInfo isn't returning the correct value, please report this to Wingidon.";
        if (netHealth > 0)
        {
            healthChangeInfo = string.Format("I healed you for {0} health", netHealth);
        }
        else
        {
            healthChangeInfo = string.Format("I dealt {0} damage to you", netHealth * -1);
        }
        if (villagerCount == 1)
        {
            info = string.Format("Among #{0}, #{1}, #{2}, there is:\n{3} Villager\n\n{4}", sortedChars[0].id, sortedChars[1].id, sortedChars[2].id, villagerCount, healthChangeInfo);
        }
        else
        {
            info = string.Format("Among #{0}, #{1}, #{2}, there are:\n{3} Villagers\n\n{4}", sortedChars[0].id, sortedChars[1].id, sortedChars[2].id, villagerCount, healthChangeInfo);
        }
        return info;
    }
}