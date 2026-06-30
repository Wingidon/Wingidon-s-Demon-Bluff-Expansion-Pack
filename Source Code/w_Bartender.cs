using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using System.Runtime.CompilerServices;
using static MelonLoader.MelonLaunchOptions;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Bartender : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> corruptedChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<string> chosenRoles = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<Character> targetNeighbours = new Il2CppSystem.Collections.Generic.List<Character>();
        Character chosenCharacter = charRef;
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.statuses.Contains(ECharacterStatus.Corrupted))
            {
                corruptedChars.Add(character);
            }
        }
        if (corruptedChars.Count == 0)
        {
            return new ActedInfo("There are no Corrupted characters");
        }
        chosenCharacter = corruptedChars[UnityEngine.Random.RandomRangeInt(0, corruptedChars.Count)];

        targetNeighbours = sharedScripts.GetCharacterNeighbours(chosenCharacter);
        foreach (Character character in targetNeighbours)
        {
            chosenRoles.Add(character.GetRegisterAs().name.ToString());
        }
        string line = ConjureInfo(chosenRoles);

        ActedInfo actedInfo = new ActedInfo(line);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> corruptedChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> pureChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<string> trueInfo = new Il2CppSystem.Collections.Generic.List<string>();
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.statuses.Contains(ECharacterStatus.Corrupted))
            {
                corruptedChars.Add(character);
            }
            else
            {
                pureChars.Add(character);
            }
        }

        Il2CppSystem.Collections.Generic.List<Character> checkNeighbours = new Il2CppSystem.Collections.Generic.List<Character>();
        if (corruptedChars.Count != 0)
        {
            foreach (Character character in corruptedChars)
            {
                checkNeighbours = sharedScripts.GetCharacterNeighbours(character);
                trueInfo.Add($"{checkNeighbours[0].GetRegisterAs().characterName}, {checkNeighbours[1].GetRegisterAs().characterName}");
                trueInfo.Add($"{checkNeighbours[1].GetRegisterAs().characterName}, {checkNeighbours[0].GetRegisterAs().characterName}");
            }
        }

        Il2CppSystem.Collections.Generic.List<string> possibleInfoOne = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<string> possibleInfoTwo = new Il2CppSystem.Collections.Generic.List<string>();

        // First, we check how much we can get away with in the "wrong character's neighbours" department.
        foreach (Character character in pureChars)
        {
            checkNeighbours = sharedScripts.GetCharacterNeighbours(character);
            if (!trueInfo.Contains($"{checkNeighbours[0].GetRegisterAs().characterName}, {checkNeighbours[1].GetRegisterAs().characterName}"))
            {
                possibleInfoOne.Add(checkNeighbours[0].GetRegisterAs().characterName);
                possibleInfoTwo.Add(checkNeighbours[1].GetRegisterAs().characterName);
            }
        }

        int targetWrongNeighbourGoal = 5;
        if (possibleInfoOne.Count != 0)
        {
            targetWrongNeighbourGoal = possibleInfoOne.Count * 2;
        }

        // Next, we get one or more Corrupted character's neighbours' roles wrong
        Character chosenTarget = new Character();
        Il2CppSystem.Collections.Generic.List<int> falseCount = new Il2CppSystem.Collections.Generic.List<int>();
        Il2CppSystem.Collections.Generic.List<string> trueNeighbours = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<string> falseNeighbours = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<CharacterData> possibleWrongRoles = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
        {
            if (character.usuallyDisguised) possibleWrongRoles.Add(character);
        }
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.dataRef.usuallyDisguised && !possibleWrongRoles.Contains(character.dataRef)) possibleWrongRoles.Add(character.dataRef);
        }
        if (sharedScripts.GetPossibleHiddenRoles().Count != 0)
        {
            foreach (CharacterData character in sharedScripts.GetPossibleHiddenRoles())
            {
                possibleWrongRoles.Add(character);
            }
        }
        falseCount.Add(1);
        falseCount.Add(1);
        falseCount.Add(2);
        int chosenFalseCount = 1;
        bool problem = false;
        int attempts = 0;
        string checkFalseInfo = "";
        string chosenNeighbourOne = "";
        string chosenNeighbourTwo = "";
        if (corruptedChars.Count != 0)
        {
            while (possibleInfoOne.Count < targetWrongNeighbourGoal && !problem)
            {
                chosenTarget = sharedScripts.GetRandomItemOfList(pureChars);
                checkNeighbours = sharedScripts.GetCharacterNeighbours(chosenTarget); // We have the neighbours now
                chosenFalseCount = sharedScripts.GetRandomItemOfList(falseCount);
                trueNeighbours.Clear();
                trueNeighbours.Add(checkNeighbours[0].GetRegisterAs().characterName);
                trueNeighbours.Add(checkNeighbours[1].GetRegisterAs().characterName);
                falseNeighbours.Clear();
                foreach (CharacterData character in possibleWrongRoles)
                {
                    if (!trueNeighbours.Contains(character.characterName) && !falseNeighbours.Contains(character.characterName))
                    {
                        falseNeighbours.Add(character.characterName);
                    }
                }

                if (falseNeighbours.Count < chosenFalseCount || attempts > 10) problem = true;
                else
                {
                    if (chosenFalseCount == 1)
                    {
                        chosenNeighbourOne = sharedScripts.GetRandomItemOfList(trueNeighbours);
                        chosenNeighbourTwo = sharedScripts.GetRandomItemOfList(falseNeighbours);
                        if (!trueInfo.Contains($"{chosenNeighbourOne}, {chosenNeighbourTwo}"))
                        {
                            possibleInfoOne.Add(chosenNeighbourOne);
                            possibleInfoTwo.Add(chosenNeighbourTwo);
                        }
                    }
                    else
                    {
                        chosenNeighbourOne = sharedScripts.GetRandomItemOfList(falseNeighbours);
                        falseNeighbours.Remove(chosenNeighbourOne);
                        chosenNeighbourTwo = sharedScripts.GetRandomItemOfList(falseNeighbours);
                        if (!trueInfo.Contains($"{chosenNeighbourOne}, {chosenNeighbourTwo}"))
                        {
                            possibleInfoOne.Add(chosenNeighbourOne);
                            possibleInfoTwo.Add(chosenNeighbourTwo);
                        }
                    }
                }
            }
        }

        // Okay, that was a lot more than expected, but we're almost done.
        string line = "";
        if (possibleInfoOne.Count == 0)
        {
            line = "Something does not make sense";
        }
        else
        {
            Il2CppSystem.Collections.Generic.List<string> chosenRoles = new Il2CppSystem.Collections.Generic.List<string>();
            if (corruptedChars.Count != 0 && sharedScripts.PercentChance(15)) // 15% chance to say "There are no Corrupted characters" if valid
            {
                chosenRoles.Add("0");
            }
            else
            {
                int infoID = UnityEngine.Random.RandomRangeInt(0, possibleInfoOne.Count);
                chosenRoles.Add(possibleInfoOne[infoID]);
                chosenRoles.Add(possibleInfoTwo[infoID]);
            }
            line = ConjureInfo(chosenRoles);
        }


        ActedInfo actedInfo = new ActedInfo(line);
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "Learn the neighbours of a Corrupted character";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
        }
    }

    private string ConjureInfo(Il2CppSystem.Collections.Generic.List<string> roles)
    {
        if (roles[0] == "0")
        {
            return "There are no Corrupted characters";
        }
        if (UnityEngine.Random.RandomRangeInt(0,2) == 1)
        {
            return $"{roles[0]} and {roles[1]} neighbour a Corrupted character";
        }
        return $"{roles[1]} and {roles[0]} neighbour a Corrupted character";

    }
    public w_Bartender() : base(ClassInjector.DerivedConstructorPointer<w_Bartender>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Bartender(IntPtr ptr) : base(ptr)
    {
    }
}


