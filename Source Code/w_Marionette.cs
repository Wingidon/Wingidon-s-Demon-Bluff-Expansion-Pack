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
public class w_Marionette : Role
{
    public w_Marionette() : base(ClassInjector.DerivedConstructorPointer<w_Marionette>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Marionette(System.IntPtr ptr) : base(ptr)
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
            SitNextToDemon(charRef);
        }
        if (trigger == ETriggerPhase.Day)
        {
            if (!charRef.bluff) OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo(GetRamblings());
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo(GetRamblings() + "\n\n" + GetCorruptedRamblings());
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
        {
        }
        if (trigger == ETriggerPhase.Day)
        {
            if (!charRef.bluff) OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
        }
    }
    public string ConjourInfo()
    {
        return "";
    }

    private string GetRamblings()
    {
        Il2CppSystem.Collections.Generic.List<string> marionetteLines = new();
        marionetteLines.Add("There's no Demon!\nI'm free!");
        marionetteLines.Add("What is... happening...?");
        marionetteLines.Add("What the hell...?");
        marionetteLines.Add("This sense of freedom is... new...");
        marionetteLines.Add("I feel... free?");
        marionetteLines.Add("Is... is this what freedom feels like?");
        marionetteLines.Add("I feel like a weight has been lifted off my shoulders...");
        marionetteLines.Add("Am... am I free?");
        marionetteLines.Add("What... happened...?");
        marionetteLines.Add("Is... is this reality?");
        marionetteLines.Add("Is... is this real?");
        marionetteLines.Add("Is this a dream...?");
        marionetteLines.Add("This is a dream, right? Surely...");
        marionetteLines.Add("What...?");
        marionetteLines.Add("I feel... weird...");
        marionetteLines.Add("I feel... strange...");
        marionetteLines.Add("This doesn't feel right...");
        marionetteLines.Add("What's this string tied around my wrist?");
        marionetteLines.Add("What are these strands collapsing all around me...?");
        marionetteLines.Add("It feels like my entire world is falling apart...");
        marionetteLines.Add("I feel like something was stopping me from breathing... until now...");
        marionetteLines.Add("What's this rope on my wrist...?");
        marionetteLines.Add("What's this string collapsing around me? Was I captive?");
        marionetteLines.Add("What is this weird strand...");
        marionetteLines.Add("Was this connected to the sky...?");
        return marionetteLines[UnityEngine.Random.RandomRangeInt(0, marionetteLines.Count)];
    }

    private string GetCorruptedRamblings()
    {
        Il2CppSystem.Collections.Generic.List<string> marionetteLines = new();
        marionetteLines.Add("I still feel off though...");
        marionetteLines.Add("These strings are so tight though...");
        marionetteLines.Add("Why does it still feel so tight around my wrist...");
        marionetteLines.Add("It still hurts... why does it still hurt...");
        return marionetteLines[UnityEngine.Random.RandomRangeInt(0, marionetteLines.Count)];
    }

    private void ApplyStatuses(Character charRef)
    {
    }
    public override int GetDamageToYou()
    {
        return 3;
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        //if (satNextToDemon(charRef))
        //{
        Il2CppSystem.Collections.Generic.List<Character> demonNeighbours = new wx_SavedScripts().GetCharacterNeighbours(charRef);
        demonNeighbours = Characters.Instance.FilterRealCharacterType(demonNeighbours, ECharacterType.Demon);
        if (demonNeighbours.Count == 0) return null;
            int diceRoll = Calculator.RollDice(10);

            //List<CharacterData> notInPlayCh = Gameplay.Instance.GetScriptCharacters();
            //notInPlayCh = Characters.Instance.FilterAlignmentCharacters(notInPlayCh, EAlignment.Good);
            //notInPlayCh = Characters.Instance.FilterBluffableCharacters(notInPlayCh);
            //return notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];

            if (diceRoll < 5)
            {
                // 100% Double Claim
                return Characters.Instance.GetRandomDuplicateBluff();
            }
            else
            {
                // Become a new character
                CharacterData bluff = Characters.Instance.GetRandomUniqueBluff();
                Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

                return bluff;
            }
        //}
        //else return null;
    }
    private void SitNextToDemon(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> checkDemons = new Il2CppSystem.Collections.Generic.List<Character>();
        checkDemons = Characters.Instance.FilterRealCharacterType(Gameplay.CurrentCharacters, ECharacterType.Demon);
        if (checkDemons.Count == 0) return;

        Character pickedDemon = checkDemons[UnityEngine.Random.Range(0, checkDemons.Count)];

        Il2CppSystem.Collections.Generic.List<Character> adjacentCharacters = Characters.Instance.GetAdjacentAliveCharacters(pickedDemon);
        Character pickedSwapCharacter = adjacentCharacters[UnityEngine.Random.Range(0, adjacentCharacters.Count)];
        CharacterData pickedData = pickedSwapCharacter.dataRef;
        pickedSwapCharacter.Init(charRef.dataRef);
        charRef.Init(pickedData);
    }
    public override CharacterData GetRegisterAsRole(Character charRef)
    {
        //Il2CppSystem.Collections.Generic.List<CharacterData> allChars = Gameplay.Instance.GetScriptCharacters();
        //allChars = Characters.Instance.FilterAlignmentCharacters(allChars, EAlignment.Evil);

        //CharacterData randomMinion = allChars[UnityEngine.Random.Range(0, allChars.Count)];

        //return randomMinion;

        var marionetteRegisterPuppet = ProjectContext.Instance.gameData.GetCharacterDataOfId("Puppet_15989619");
        return marionetteRegisterPuppet;
    }
    private bool satNextToDemon(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> adjacentCharacters = Characters.Instance.GetAdjacentCharacters(charRef);
        bool adjacentDemon = false;
        foreach (Character character in adjacentCharacters)
        {
            if (character.dataRef.type == ECharacterType.Demon)
            {
                adjacentDemon = true;
            }
        }
        return adjacentDemon;
    }
}


