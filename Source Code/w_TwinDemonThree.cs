using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_TwinDemonThree : Demon
{
    bool haveActed = false;
    // public bool firstNight = true;
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
            haveActed = false;
        }
        if (trigger == ETriggerPhase.Start)
        {
            if (haveActed) return;
            new w_Saboteur().Act(trigger, charRef);
            charRef.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
        }
        /*      if (trigger == ETriggerPhase.Start)
                {
                    firstNight = true;
                }
                if (trigger == ETriggerPhase.Night)
                {
                    if (charRef.state == ECharacterState.Dead) return;
                    Health health = PlayerController.PlayerInfo.health;
                    if (firstNight)
                    {
                        health.Damage(2);
                        firstNight = false;
                    }
                    else
                    {
                        health.Damage(1);
                    }
                }
        */
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        Act(trigger, charRef);
    }
    public override void ActOnDied(Character charRef)
    {
        /* Old ability, uncomment if necessary
        Health health = PlayerController.PlayerInfo.health;
        Il2CppSystem.Collections.Generic.List<Character> aliveEvils = new Il2CppSystem.Collections.Generic.List<Character>();
        aliveEvils = Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters);
        aliveEvils = Characters.Instance.FilterRealAlignmentCharacters(aliveEvils, EAlignment.Evil);
        aliveEvils.Remove(charRef);
        Character undying = null;
        foreach (Character character in aliveEvils)
        {
            if (character.dataRef.characterId == "Undying_WING")
            {
                health.Damage(-1); // Use Damage instead of Heal to avoid situations where you still take damage because you were at max health.
                break; // Only do this a max of once, if there's more than one Undying then we have problems.
            }
        }
        health.Damage(aliveEvils.Count);
        if (aliveEvils.Count != 0) health.Damage(1);
        */
    }
    public w_TwinDemonThree() : base(ClassInjector.DerivedConstructorPointer<w_TwinDemonThree>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_TwinDemonThree(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

        return bluff;
    }
}


