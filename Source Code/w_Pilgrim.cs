using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static Il2CppSystem.Collections.SortedList;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Pilgrim : Role
{
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
        {
            if (charRef.dataRef.characterId != "Pilgrim_WING") return;
            Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
            System.Collections.Generic.List<Character> newList = new System.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
            Characters charInst = Characters.Instance;
            foreach (Character character in characters)
            {
                if (!character.statuses.Contains(w_Illusionist.EmenveraxIllusion.emenveraxIllusion) && character != charRef)
                {
                    Debug.Log(string.Format("Found valid Pilgrim target at #{0}", character.id));
                    newList.Add(character);
                }
            }
            Character targetChar = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            CharacterData bluff = charRef.dataRef;
            Debug.Log(string.Format("Created Pilgrim clone at #{0} using bluff of {1}", targetChar.id, bluff.characterId));
            targetChar.GiveBluff(bluff);
            targetChar.statuses.AddStatus(PilgrimIllusion.pilgrimConvert, charRef);
            targetChar.statuses.AddStatus(ECharacterStatus.Silenced, charRef);
            charRef.statuses.AddStatus(ECharacterStatus.Silenced, charRef);
        }
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, new ActedInfo("I have nothing to tell you"));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, new ActedInfo("I have nothing to tell you"));
        }
    }
    public w_Pilgrim() : base(ClassInjector.DerivedConstructorPointer<w_Pilgrim>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Pilgrim(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        return null;
    }
    public static class PilgrimIllusion // Used to prevent Pilgrim and Emenverax from affecting the same character.
    {
        public static ECharacterStatus pilgrimConvert = (ECharacterStatus)543;
    }
}


