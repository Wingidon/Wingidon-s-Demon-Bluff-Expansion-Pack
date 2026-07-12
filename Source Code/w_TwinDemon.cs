using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static Il2CppSystem.Globalization.CultureInfo;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_TwinDemon : Demon
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public w_TwinDemon() : base(ClassInjector.DerivedConstructorPointer<w_TwinDemon>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_TwinDemon(System.IntPtr ptr) : base(ptr)
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
            Il2CppSystem.Collections.Generic.List<Character> validPoisonTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            int savedDist = 100;
            int checkDist = 0;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.dataRef.type == ECharacterType.Villager && character.alignment == EAlignment.Good && character.GetRegisterAs().type == ECharacterType.Villager && character.GetRegisterAlignment() == EAlignment.Good) // If they're a Good Villager who is Registering as such
                {
                    checkDist = GetDistanceBetweenCharacters(charRef, character); // Grab the distance
                    if (savedDist == checkDist) // If it's the same distance as the current min dist,
                    {
                        validPoisonTargets.Add(character); // Add the checked character to the list.
                    }
                    if (savedDist > checkDist) // If it's LOWER, clear the list, then add it.
                    {
                        validPoisonTargets.Clear(); // Clear valid poison target list
                        validPoisonTargets.Add(character); // Add the checked character to the list.
                        savedDist = checkDist; // Reset the savedDist variable.
                    }
                }
            }
            Character poisonTarget = validPoisonTargets[UnityEngine.Random.RandomRangeInt(0, validPoisonTargets.Count)];
            poisonTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
            poisonTarget.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
        }
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
    /* Old code
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
        {
            Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>(Gameplay.CurrentCharacters.Pointer);
            Character pickedChar = new Character();
            chars = Characters.Instance.FilterCharacterType(chars, ECharacterType.Villager);
            chars = Characters.Instance.FilterAlignmentCharacters(chars, EAlignment.Good);
            if (chars.Count > 0)
            {
                pickedChar = chars[UnityEngine.Random.Range(0, chars.Count)];
                bool twinHellspawnInPlay = false;
                foreach (Character c in Gameplay.CurrentCharacters)
                {
                    if (c.dataRef.characterId == "TwinDemonTwin_WING")
                    {
                        twinHellspawnInPlay = true;
                    }
                }
                foreach (Character c in Gameplay.CurrentCharacters)
                {
                    if (c == pickedChar)
                    {
                        if (allDatas.Length == 0)
                        {
                            var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
                            if (loadedCharList != null)
                            {
                                allDatas = new CharacterData[loadedCharList.Length];
                                for (int i = 0; i < loadedCharList.Length; i++)
                                {
                                    allDatas[i] = loadedCharList[i]!.Cast<CharacterData>();
                                    c.statuses.AddStatus(ECharacterStatus.AlteredCharacter, charRef);
                                }
                            }
                        }

                        for (int i = 0; i < allDatas.Length; i++)
                        {
                            if (!twinHellspawnInPlay)
                            {
                                if (allDatas[i].characterId == "TwinDemonTwin_WING")
                                {
                                    Gameplay.Instance.AddScriptCharacter(ECharacterType.Demon, allDatas[i]);
                                    if (c.GetRegisterAs().characterId != allDatas[i].characterId)
                                    {
                                        c.Init(allDatas[i]);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (allDatas[i].characterId == "TwinDemonTriplet_WING")
                                {
                                    Gameplay.Instance.AddScriptCharacter(ECharacterType.Demon, allDatas[i]);
                                    if (c.GetRegisterAs().characterId != allDatas[i].characterId)
                                    {
                                        c.Init(allDatas[i]);
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
    */
    public int GetDistanceBetweenCharacters(Character char1, Character char2)
    {
        int totalCharCount = Gameplay.CurrentCharacters.Count;
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


