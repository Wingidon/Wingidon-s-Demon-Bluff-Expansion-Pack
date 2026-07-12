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
public class w_Saboteur : Role
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public w_Saboteur() : base(ClassInjector.DerivedConstructorPointer<w_Saboteur>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Saboteur(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
        {
            UnityEngine.Debug.Log(string.Format("Saboteur at {0} acting.", charRef.id));
            Character pickedCharacter = new Character();
            Il2CppSystem.Collections.Generic.List<Character> viableCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            viableCharacters = Characters.Instance.FilterCharacterType(Gameplay.CurrentCharacters, ECharacterType.Villager);
            viableCharacters = Characters.Instance.FilterAlignmentCharacters(viableCharacters, EAlignment.Good);
            viableCharacters = Characters.Instance.FilterCharacterMissingStatus(viableCharacters, ECharacterStatus.Corrupted);
            UnityEngine.Debug.Log(string.Format("Found {0} available Saboteur targets", viableCharacters.Count));

            if (viableCharacters.Count == 0) return;

            Il2CppSystem.Collections.Generic.List<Character> myTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            int savedDistance = 0;
            int curDistance = 0;
            bool distGreater = false;
            foreach (Character character in viableCharacters)
            {
                curDistance = GetDistanceBetweenCharacters(charRef, character, Gameplay.CurrentCharacters.Count);
                distGreater = (savedDistance < curDistance);
                UnityEngine.Debug.Log(string.Format("Checking distance from Saboteur to #{0}, result is {1}. Saved distance is {2}. Current distance greater? {3}", character.id, curDistance, savedDistance, distGreater.ToString()));
                if (savedDistance < curDistance)
                {
                    myTargets.Clear();
                    savedDistance = curDistance;
                    myTargets.Add(character);
                    UnityEngine.Debug.Log(string.Format("Current distance is greater. New saved distance is {0}.", savedDistance));
                }
            }
            pickedCharacter = myTargets[UnityEngine.Random.RandomRangeInt(0, myTargets.Count)];
            UnityEngine.Debug.Log(string.Format("Attempting to poison #{0}.", pickedCharacter.id));

            pickedCharacter.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
            pickedCharacter.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);

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
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        Act(trigger, charRef);
    }
    public string ConjourInfo()
    {
        return "";
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
        return bluff;
    }
    public int GetDistanceBetweenCharacters(Character char1, Character char2, int totalCharCount)
    {
        int tempDist = char1.id - char2.id;
        if (tempDist < 0)
        {
            tempDist = tempDist + totalCharCount;
        }
        int tempDist2 = char2.id - char1.id;
        if (tempDist2 < 0)
        {
            tempDist2 = tempDist2 + totalCharCount;
        }
        if (tempDist > tempDist2)
        {
            return tempDist2;
        }
        return tempDist;
    }
}


