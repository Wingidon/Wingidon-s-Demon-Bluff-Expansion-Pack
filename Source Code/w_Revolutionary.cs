using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static Il2CppSystem.Collections.SortedList;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Revolutionary : Role
{
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
    }
    public override void ActOnDied(Character charRef)
    {
        if (charRef.statuses.Contains(ECharacterStatus.KilledByEvil)) return;
        if (charRef.alignment == EAlignment.Evil) return;
        Il2CppSystem.Collections.Generic.List<Character> villagers = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        villagers = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
        villagers = Characters.Instance.FilterCharacterType(villagers, ECharacterType.Villager);
        villagers = Characters.Instance.FilterAliveCharacters(villagers);
        if (villagers.Count == 0)
        {
            OnActed(ETriggerPhase.Day, charRef, new ActedInfo("Viva la... revolution?", selection));
        }
        else
        {
            Character targetVillager = villagers[UnityEngine.Random.RandomRangeInt(0, villagers.Count)];
            selection.Add(targetVillager);
            OnActed(ETriggerPhase.Day, charRef, new ActedInfo("Viva la revolution!", selection));
            targetVillager.ExecuteAndReveal();
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo("Viva la revolution!", null);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("Viva la... revolution?", null);
    }
    public override int GetDamageToYou()
    {
        return 3;
    }
    private async void WaitTask()
    {
        await Task.Delay(1000);
        return;
    }
    public w_Revolutionary() : base(ClassInjector.DerivedConstructorPointer<w_Revolutionary>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Revolutionary(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        int diceRoll = Calculator.RollDice(10);
        CharacterData bluff = Characters.Instance.GetRandomUniqueBluff();

        //List<CharacterData> notInPlayCh = Gameplay.Instance.GetScriptCharacters();
        //notInPlayCh = Characters.Instance.FilterAlignmentCharacters(notInPlayCh, EAlignment.Good);
        //notInPlayCh = Characters.Instance.FilterBluffableCharacters(notInPlayCh);
        //return notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];

        if (diceRoll < 5)
        {
            // 100% Double Claim
            bluff = Characters.Instance.GetRandomDuplicateBluff();
        }
        else
        {
            // Become a new character
            bluff = Characters.Instance.GetRandomUniqueBluff();
            Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        }
        if (bluff.name != "Bombardier" && !charRef.statuses.Contains(ECharacterStatus.Corrupted))
        {
            charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
            charRef.statuses.AddStatus(ECharacterStatus.WorkingAbility, charRef);
        }
        return bluff;
    }
    public override bool CheckIfCanBeKilled(Character charRef)
    {
        if (charRef.statuses.statuses.Contains(ECharacterStatus.HealthyBluff))
            return charRef.bluff.role.CheckIfCanBeKilled(charRef);
        else
            return true;
    }
    private Character GetRandomAdjacentVillager(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> adjacentCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character ch in Gameplay.CurrentCharacters)
        { 
            if (charRef == ch)
            {
                adjacentCharacters = Characters.Instance.GetAdjacentCharacters(ch);
                break;
            }
        }
        adjacentCharacters = Characters.Instance.FilterCharacterType(adjacentCharacters, ECharacterType.Villager);
        if (adjacentCharacters.Count == 0)
        {
            return null;
        }
        else
        {
            return adjacentCharacters[UnityEngine.Random.RandomRangeInt(0, adjacentCharacters.Count)];
        }
    }
}


