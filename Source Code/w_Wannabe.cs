using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Wannabe : Role
{
    bool haveUsedAbility = false;
    public w_Wannabe() : base(ClassInjector.DerivedConstructorPointer<w_Wannabe>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Wannabe(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            haveUsedAbility = false;
        }
        if (trigger == ETriggerPhase.Start)
        {
            if (charRef.dataRef.characterId != "Wannabe_WING") return;
            charRef.statuses.AddStatus(ECharacterStatus.AppearLying, charRef);
            Il2CppSystem.Collections.Generic.List<Character> evilCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.alignment == EAlignment.Evil && character.dataRef.startingAlignment == EAlignment.Evil && character.dataRef.usuallyDisguised == true)
                {
                    evilCharacters.Add(character);
                }
            }
            if (evilCharacters.Count == 0) return;
            haveUsedAbility = true;
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Character chosenTarget = sharedScripts.GetRandomItemOfList(evilCharacters);
            chosenTarget.GiveBluff(charRef.dataRef);
            chosenTarget.statuses.AddStatus(ECharacterStatus.AppearTruthfull, charRef);
        }
        if (trigger == ETriggerPhase.Day)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Il2CppSystem.Collections.Generic.List<Character> totallyRealWannabe = new Il2CppSystem.Collections.Generic.List<Character>();
            totallyRealWannabe.Add(charRef);
            if (charRef.dataRef.characterId != "Wannabe_WING")
            {
                if (charRef.dataRef.startingAlignment != EAlignment.Evil)
                {
                    OnActed(ETriggerPhase.Day, charRef, new ActedInfo("My dreams were crushed", totallyRealWannabe));
                }
                else
                {
                    OnActed(ETriggerPhase.Day, charRef, new ActedInfo($"I am {sharedScripts.CheckIfThe(charRef.dataRef.characterName)}{charRef.dataRef.characterName}", totallyRealWannabe));
                }
            }
            else
            {
                Il2CppSystem.Collections.Generic.List<CharacterData> possibleEvils = new Il2CppSystem.Collections.Generic.List<CharacterData>();
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (character.dataRef.startingAlignment == EAlignment.Evil && character.dataRef.usuallyDisguised)
                    {
                        if (!possibleEvils.Contains(character.dataRef)) possibleEvils.Add(character.dataRef);
                    }
                }
                foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Evil))
                {
                    if (!possibleEvils.Contains(character) && character.usuallyDisguised) possibleEvils.Add(character);
                }
                if (sharedScripts.GetPossibleHiddenRoles().Count != 0)
                {
                    foreach (CharacterData character in sharedScripts.GetPossibleHiddenRoles())
                    {
                        if (!possibleEvils.Contains(character) && character.usuallyDisguised) possibleEvils.Add(character);
                    }
                }
                CharacterData chosenEvil = sharedScripts.GetRandomItemOfList(possibleEvils);
                string chosenEvilName = chosenEvil.characterName;
                OnActed(ETriggerPhase.Day, charRef, new ActedInfo($"I am {sharedScripts.CheckIfThe(chosenEvilName)}{chosenEvilName}", totallyRealWannabe));
            }
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo("");
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("");
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        Act(trigger, charRef);
    }
    public string ConjourInfo()
    {
        return "";
    }

    private void ApplyStatuses(Character charRef)
    {
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        return null;
    }
    public override bool CheckIfCanBeKilled(Character charRef)
    {
        return true;
    }
}


