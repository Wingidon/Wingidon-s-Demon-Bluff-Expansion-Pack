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
public class w_Chatterbox : Role
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public Character chatterPoisonTarget = new Character();
    public w_Chatterbox() : base(ClassInjector.DerivedConstructorPointer<w_Chatterbox>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Chatterbox(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            //new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Day)
        {
            chatterPoisonTarget = charRef;
            Il2CppSystem.Collections.Generic.List<Character> unrevealedCharacters = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
            unrevealedCharacters = Characters.Instance.FilterAliveCharacters(unrevealedCharacters);
            unrevealedCharacters.Remove(charRef);
            if (unrevealedCharacters.Count == 0)
            {
                onActed.Invoke(SomethingReallyInteresting());
                return;
            }
            unrevealedCharacters = Characters.Instance.FilterCharacterType(unrevealedCharacters, ECharacterType.Villager);
            unrevealedCharacters = Characters.Instance.FilterAlignmentCharacters(unrevealedCharacters, EAlignment.Good);
            unrevealedCharacters = Characters.Instance.FilterCharacterMissingStatus(unrevealedCharacters, ECharacterStatus.Corrupted);
            if (unrevealedCharacters.Count == 0)
            {
                onActed.Invoke(GetInfo(charRef));
                return;
            }
            Character poisonTarget = unrevealedCharacters[UnityEngine.Random.RandomRangeInt(0, unrevealedCharacters.Count)];
            chatterPoisonTarget = poisonTarget;
            poisonTarget.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
            if (poisonTarget.dataRef.characterId == "Copycat_WING") poisonTarget.statuses.statuses.Remove(ECharacterStatus.HealthyBluff);
            onActed.Invoke(GetInfo(charRef));
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        string info = "Info";
        Il2CppSystem.Collections.Generic.List<Character> unrevealedCharacters = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
        unrevealedCharacters = Characters.Instance.FilterAliveCharacters(unrevealedCharacters);
        unrevealedCharacters.Remove(charRef);

        unrevealedCharacters.Remove(chatterPoisonTarget);
        for (int i = 0; i < 2; i++)
        {
            if (unrevealedCharacters.Count != 0)
            {
                Character charAdd = unrevealedCharacters[UnityEngine.Random.RandomRangeInt(0, unrevealedCharacters.Count)];
                selection.Add(charAdd);
                unrevealedCharacters.Remove(charAdd);
            }
        }
        if (chatterPoisonTarget && chatterPoisonTarget != charRef)
        {
            selection.Add(chatterPoisonTarget);
        }
        else
        {
            if (unrevealedCharacters.Count != 0) selection.Add(unrevealedCharacters[UnityEngine.Random.RandomRangeInt(0, unrevealedCharacters.Count)]);
        }
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        selection = sharedScripts.SortList(selection);

        if (selection.Count == 0)
        {
            //info = "All characters have been revealed";
            info = SomethingReallyInteresting().desc;
        }
        else if (selection.Count == 1)
        {
            info = string.Format("I spoke to #{0}", selection[0].id);
        }
        else
        {
            info = $"I spoke to {sharedScripts.MentionEveryCharacterInList(selection, "or")}";
        }
        return new ActedInfo(info, selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("");
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            //new wx_SavedScripts().DebugMessage($"Initialised lying Chatterbox ({charRef.dataRef.characterName}) at #{charRef.id}");
        }
        if (trigger != ETriggerPhase.Day) return;
        onActed.Invoke(GetInfo(charRef));
    }
    public string ConjourInfo()
    {
        return "";
    }

    private ActedInfo SomethingReallyInteresting()
    {
        Il2CppSystem.Collections.Generic.List<string> randomNames = wx_RandomLists.GetNameList();
        Il2CppSystem.Collections.Generic.List<string> somethingReallyInteresting = new Il2CppSystem.Collections.Generic.List<string>();

        string randomNameOne = randomNames[UnityEngine.Random.RandomRangeInt(0, randomNames.Count)];
        randomNames.Remove(randomNameOne);
        string randomNameTwo = randomNames[UnityEngine.Random.RandomRangeInt(0, randomNames.Count)];
        randomNames.Remove(randomNameTwo);
        string randomNameThree = randomNames[UnityEngine.Random.RandomRangeInt(0, randomNames.Count)];
        randomNames.Remove(randomNameThree);
        string randomNameFour = randomNames[UnityEngine.Random.RandomRangeInt(0, randomNames.Count)];

        somethingReallyInteresting.Add("Something really interesting!");
        somethingReallyInteresting.Add("Have you heard the latest gossip?");
        somethingReallyInteresting.Add("Did you hear?");
        somethingReallyInteresting.Add("Blah blah blah blah blah blah blah blah blah blah blah blah");
        somethingReallyInteresting.Add("Do you wanna hear something funny?");
        somethingReallyInteresting.Add("Do you wanna hear something interesting?");
        somethingReallyInteresting.Add("Do you wanna hear something cool?");
        somethingReallyInteresting.Add($"Did {randomNames[UnityEngine.Random.RandomRangeInt(0, randomNames.Count)]} update you on the latest drama?");
        somethingReallyInteresting.Add($"Did {randomNames[UnityEngine.Random.RandomRangeInt(0, randomNames.Count)]} update you with the recent news?");
        somethingReallyInteresting.Add($"Did {randomNames[UnityEngine.Random.RandomRangeInt(0, randomNames.Count)]} tell you what happened earlier?");
        somethingReallyInteresting.Add($"I saw {randomNameOne} with {randomNameTwo} yesterday...");
        somethingReallyInteresting.Add($"What was {randomNameOne} doing with {randomNameTwo} yesterday, I wonder");
        somethingReallyInteresting.Add($"Did you hear that {randomNameOne} saw {randomNameTwo} and {randomNameThree} together yesterday?");
        somethingReallyInteresting.Add($"I saw {randomNameOne}, {randomNameTwo}, {randomNameThree} and {randomNameFour} sitting around in a circle yesterday");
        somethingReallyInteresting.Add($"I'm prettty sure {randomNameOne} is planning something sinister");
        somethingReallyInteresting.Add($"{randomNameOne} should probably watch their back, I think {randomNameTwo} wants to do something to them...");
        somethingReallyInteresting.Add($"{randomNameOne} is plotting something...");
        somethingReallyInteresting.Add($"Between you and me, I think {randomNameOne} is suspicious");
        somethingReallyInteresting.Add($"{randomNameOne} is Evil!");
        somethingReallyInteresting.Add($"{randomNameOne} is Good!");
        somethingReallyInteresting.Add($"Not liking our chances here");
        somethingReallyInteresting.Add($"I saw {randomNameOne} on a date with {randomNameTwo} the other day...\n\nNah, I just made that up");
        somethingReallyInteresting.Add($"I think {randomNameOne} and {randomNameTwo} are in <i>looooove</i>!");
        somethingReallyInteresting.Add($"I think {randomNameOne} is hiding something!");
        somethingReallyInteresting.Add($"{randomNameOne} is suspicious!");
        somethingReallyInteresting.Add($"{randomNameOne} seems innocent");
        somethingReallyInteresting.Add($"I wouldn't trust {randomNameOne} if I were you");
        somethingReallyInteresting.Add($"{randomNameOne} and {randomNameTwo} had a massive argument yesterday!");
        somethingReallyInteresting.Add($"Did you hear? Apparently {randomNameOne} stole something from {randomNameTwo} while they weren't looking");
        somethingReallyInteresting.Add($"I heard {randomNameOne} discussing something about hideout locations with {randomNameTwo} the other day");
        somethingReallyInteresting.Add($"I think {randomNameOne} is the Demon!");
        somethingReallyInteresting.Add($"{randomNameOne} was muttering something to themselves earlier today");
        somethingReallyInteresting.Add($"Keep an eye on {randomNameOne}!");
        somethingReallyInteresting.Add($"The other day, {randomNameOne} told me how they saw {randomNameTwo} and {randomNameThree}...");
        somethingReallyInteresting.Add($"Apparently {randomNameOne} and {randomNameTwo} have been talking about {randomNameThree} behind their back!");
        somethingReallyInteresting.Add($"{randomNameOne} and {randomNameTwo} had a big argument with {randomNameThree} and {randomNameFour} a few days ago");
        somethingReallyInteresting.Add($"{randomNameOne} was talking about me behind my back! How scummy!");
        somethingReallyInteresting.Add($"Don't tell anyone, but... {randomNameOne} has a crush on {randomNameTwo}");
        somethingReallyInteresting.Add($"You holding up okay?");
        somethingReallyInteresting.Add($"Come to think of it, I haven't seen {randomNameOne} in a while...");
        somethingReallyInteresting.Add($"Wonder how {randomNameOne} is doing...");
        somethingReallyInteresting.Add($"Hey Exe, what's the news today?");
        somethingReallyInteresting.Add($"I'm meeting up with {randomNameOne} in an hour or so, they heard something about {randomNameTwo}");


        // Now for the reference
        string toxicGossip = "I heard that";
        string chosenName = "";
        for (int i = 0; i < 5; i++)
        {
            chosenName = randomNames[UnityEngine.Random.RandomRangeInt(0, randomNames.Count)];
            randomNames.Remove(chosenName);
            if (chosenName.Length > 4) toxicGossip += $" {chosenName} said";
            else toxicGossip += $" {chosenName} said that";
        }
        toxicGossip += "...";
        somethingReallyInteresting.Add(toxicGossip);
        ActedInfo returnInfo = new ActedInfo(somethingReallyInteresting[UnityEngine.Random.RandomRangeInt(0, somethingReallyInteresting.Count)]);
        return returnInfo;
    }

    public override CharacterData GetBluffIfAble(Character charRef)
    {
        return null;
    }
}


