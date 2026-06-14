using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Gravekeeper : Role
{
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
            return "Learns random info about all dead characters.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        Il2CppSystem.Collections.Generic.List<Character> deadCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        ActedInfo tempInfo = new ActedInfo("");
        ActedInfo myInfo = new ActedInfo("");
        string myStatement = "Info";
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.state == ECharacterState.Dead)
            {
                deadCharacters.Add(character);
            }
        }
        if (deadCharacters.Count == 0)
        {
            UnityEngine.Debug.Log("Why can't the Gravekeeper gather info on alive characters, is he stupid?");
            return;
        }
        Character targetChar = new Character();

        deadCharacters = SortList(deadCharacters);

        foreach (Character character in deadCharacters)
        {
            tempInfo = getTrueInfoOnChar(charRef, character);
            foreach (Character selectionChar in tempInfo.characters)
            {
                selection.Add(selectionChar);
            }
            if (myStatement == "Info")
            {
                myStatement = tempInfo.desc;
            }
            else
            {
                myStatement = $"{myStatement}\n{tempInfo.desc}";
            }
        }

        onActed?.Invoke(new ActedInfo(myStatement, selection));
        Debug.Log($"{myStatement}");
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        Il2CppSystem.Collections.Generic.List<Character> deadCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        ActedInfo tempInfo = new ActedInfo("");
        ActedInfo myInfo = new ActedInfo("");
        string myStatement = "Info";
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.state == ECharacterState.Dead)
            {
                deadCharacters.Add(character);
            }
        }
        if (deadCharacters.Count == 0) return;
        int falseInfoAmount = UnityEngine.Random.RandomRangeInt(0, deadCharacters.Count) + 1;
        Il2CppSystem.Collections.Generic.List<Character> trueInfoCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in deadCharacters)
        {
            trueInfoCharacters.Add(character);
        }
        Il2CppSystem.Collections.Generic.List<Character> falseInfoCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        Character targetChar = new Character();
        for (int i = 0; i < falseInfoAmount; i++)
        {
            if (trueInfoCharacters.Count == 0) break;
            targetChar = trueInfoCharacters[UnityEngine.Random.RandomRangeInt(0, trueInfoCharacters.Count)];
            trueInfoCharacters.Remove(targetChar);
            falseInfoCharacters.Add(targetChar);
        }


        deadCharacters = SortList(deadCharacters);

        foreach (Character character in deadCharacters)
        {
            if (falseInfoCharacters.Contains(character))
            {
                tempInfo = getFalseInfoOnChar(charRef, character);
            }
            else
            {
                tempInfo = getTrueInfoOnChar(charRef, character);
            }
            foreach (Character selectionChar in tempInfo.characters)
            {
                selection.Add(selectionChar);
            }
            if (myStatement == "Info")
            {
                myStatement = tempInfo.desc;
            }
            else
            {
                myStatement = $"{myStatement}\n{tempInfo.desc}";
            }
        }

        onActed?.Invoke(new ActedInfo(myStatement, selection));
        Debug.Log($"{myStatement}");
    }
    private ActedInfo getTrueInfoOnChar(Character charRef, Character targetRef) // Hey Gossip, can I copy your homework?
    {
        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(targetRef);

        string info = $"";
        Il2CppSystem.Collections.Generic.List<string> possibleInfo = new Il2CppSystem.Collections.Generic.List<string>();
        //if (chars[0].GetCharacterType() != ECharacterType.Demon)
        //{
        //    possibleInfo.Add("Type");
        //}
        //if (chars[0].GetRegisterAlignment() == EAlignment.Good)
        //{
        //    possibleInfo.Add("Good");
        //}
        //possibleInfo.Add("Truthfulness");
        //possibleInfo.Add("Honesty");
        //if (chars[0].statuses.Contains(ECharacterStatus.Corrupted))
        //{
        //    possibleInfo.Add("Corruption");
        //}
        possibleInfo.Add("VillagerNeighbours");
        possibleInfo.Add("EvilNeighbours");
        bool anyOutcasts = false;
        bool anyExtraEvils = false;
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetCharacterType() == ECharacterType.Outcast)
            {
                anyOutcasts = true;
            }
            if (character.GetRegisterAlignment() == EAlignment.Evil && character != chars[0])
            {
                anyExtraEvils = true;
            }
        }
        //if (anyOutcasts)
        //{
        //    possibleInfo.Add("OutcastDistance");
        //}
        if (anyExtraEvils)
        {
            possibleInfo.Add("ClosestEvilDistance");
            possibleInfo.Add("FurthestEvilDistance");
        }
        if (chars[0].dataRef != chars[0].GetRegisterAs())
        {
            possibleInfo.Add("Misregister");
        }
        if (chars[0].statuses.Contains(ECharacterStatus.MessedUpByEvil))
        {
            possibleInfo.Add("AffectedByEvil");
        }
        possibleInfo.Add("TeamRelation");
        string newString = possibleInfo[UnityEngine.Random.RandomRangeInt(0, possibleInfo.Count)];
        ActedInfo actInfo = getRandomInfo(charRef, chars[0], newString);
        return actInfo;
    }
    private ActedInfo getFalseInfoOnChar(Character charRef, Character targetRef) // Sure, just change it up a little so it doesn't look like you copied.
    {
        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(targetRef);

        string info = $"";
        Il2CppSystem.Collections.Generic.List<string> possibleInfo = new Il2CppSystem.Collections.Generic.List<string>();
        //possibleInfo.Add("Type"); // Built-in Lying logic
        //if (chars[0].GetRegisterAlignment() == EAlignment.Evil) // Yield this info only if the target is Evil, not Good
        //{
        //    possibleInfo.Add("Good");
        //}
        //possibleInfo.Add("Truthfulness"); // Built-in Lying logic
        //possibleInfo.Add("Honesty"); // Built-in Lying logic
        //if (!chars[0].statuses.Contains(ECharacterStatus.Corrupted)) // Yield this info only if the target is Pure, not Corrupted
        //{
        //    possibleInfo.Add("Corruption");
        //}
        possibleInfo.Add("VillagerNeighbours"); // Built-in Lying logic
        possibleInfo.Add("EvilNeighbours"); // Built-in Lying logic
        bool anyOutcasts = false;
        bool anyExtraEvils = false;
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetCharacterType() == ECharacterType.Outcast)
            {
                anyOutcasts = true;
            }
            if (character.GetRegisterAlignment() == EAlignment.Evil && character != chars[0])
            {
                anyExtraEvils = true;
            }
        }
        //if (anyOutcasts)
        //{
        //    possibleInfo.Add("OutcastDistance");
        //}
        if (anyExtraEvils)
        {
            possibleInfo.Add("ClosestEvilDistance"); // Built-in Lying logic
            possibleInfo.Add("FurthestEvilDistance"); // Built-in Lying logic
        }
        //if (chars[0].dataRef == chars[0].GetRegisterAs())
        //{
        //    possibleInfo.Add("Misregister"); // This one will be interesting.
        //}
        if (!chars[0].statuses.Contains(ECharacterStatus.MessedUpByEvil)) // Yield this info only if the target is NOT messed up by Evil
        {
            possibleInfo.Add("AffectedByEvil");
        }
        possibleInfo.Add("TeamRelation"); // Built-in Lying logic
        string newString = possibleInfo[UnityEngine.Random.RandomRangeInt(0, possibleInfo.Count)];
        ActedInfo actInfo = getRandomFalseInfo(charRef, chars[0], newString);
        return actInfo;
    }
    private ActedInfo getRandomInfo(Character charRef, Character targetRef, string InfoType)
    {
        UnityEngine.Debug.Log(string.Format("Getting truthful Gossip info of type {0}", InfoType));
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(targetRef);
        string info = string.Format("Placeholder info, you shouldn't see this. Please notify Wingidon that the info type of {0} is bugged for a truthful Gossip.", InfoType);
        bool bluffing = false;
        if (InfoType == "Type")
        {
            if (targetRef.GetCharacterType() != ECharacterType.Outcast)
            {
                info = string.Format("#{0} is a {1}", targetRef.id, targetRef.GetCharacterType());
            }
            else
            {
                info = string.Format("#{0} is an {1}", targetRef.id, targetRef.GetCharacterType());
            }
        }
        if (InfoType == "Good")
        {
            info = string.Format("#{0} is Good", targetRef.id);
        }
        if (InfoType == "Truthfulness")
        {
            bool targetLying = false;
            if (targetRef.bluff == null || targetRef.statuses.Contains(ECharacterStatus.Corrupted))
            {
                targetLying = true;
            }
            if (targetRef.statuses.Contains(ECharacterStatus.HealthyBluff) || targetRef.bluff.characterId == "Confessor_18741708" || targetRef.dataRef.characterId == "Confessor_18741708")
            {
                targetLying = false;
            }
            if (targetLying)
            {
                info = string.Format("#{0} is Lying", targetRef.id);
            }
            else
            {
                info = string.Format("#{0} is telling the Truth", targetRef.id);
            }
        }
        if (InfoType == "Honesty")
        {
            if (targetRef.bluff)
            {
                bluffing = true;
            }
            if (bluffing)
            {
                info = string.Format("#{0} is Disguised", targetRef.id);
            }
            else
            {
                info = string.Format("#{0} is Honest", targetRef.id);
            }
        }
        if (InfoType == "Corruption")
        {
            info = string.Format("#{0} is Corrupted", targetRef.id);
        }
        Il2CppSystem.Collections.Generic.List<Character> neighbours = Characters.Instance.GetAdjacentCharacters(targetRef);
        int detectedNeighbours = 0;
        if (InfoType == "VillagerNeighbours")
        {
            foreach (Character neighbour in neighbours)
            {
                selection.Add(neighbour);
                if (neighbour.GetCharacterType() == ECharacterType.Villager)
                {
                    detectedNeighbours++;
                }
            }
            if (detectedNeighbours == 1)
            {
                info = string.Format("#{0} has {1} Villager neighbour", targetRef.id, detectedNeighbours);
            }
            else
            {
                info = string.Format("#{0} has {1} Villager neighbours", targetRef.id, detectedNeighbours);
            }
        }
        if (InfoType == "EvilNeighbours")
        {
            foreach (Character neighbour in neighbours)
            {
                selection.Add(neighbour);
                if (neighbour.GetRegisterAlignment() == EAlignment.Evil)
                {
                    detectedNeighbours++;
                }
            }
            if (detectedNeighbours == 1)
            {
                info = string.Format("#{0} has {1} Evil neighbour", targetRef.id, detectedNeighbours);
            }
            else
            {
                info = string.Format("#{0} has {1} Evil neighbours", targetRef.id, detectedNeighbours);
            }
        }
        int detectedDistance = 0;
        int checkDistance = 0;
        if (InfoType == "ClosestEvilDistance")
        {
            checkDistance = 1000;
            detectedDistance = 1000;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.GetRegisterAlignment() == EAlignment.Evil && character != targetRef)
                {
                    checkDistance = GetDistanceBetweenCharacters(character, targetRef, Gameplay.CurrentCharacters.Count);
                    if (checkDistance < detectedDistance)
                    {
                        detectedDistance = checkDistance;
                    }
                }
            }
            foreach (Character character in Characters.Instance.GetCharactersAtRange(detectedDistance, targetRef))
            {
                selection.Add(character);
            }
            if (detectedDistance == 1)
            {
                info = string.Format("#{0} is {1} card away from their closest Evil", targetRef.id, detectedDistance);
            }
            else
            {
                info = string.Format("#{0} is {1} cards away from their closest Evil", targetRef.id, detectedDistance);
            }
        }
        if (InfoType == "FurthestEvilDistance")
        {
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.GetRegisterAlignment() == EAlignment.Evil && character != targetRef)
                {
                    checkDistance = GetDistanceBetweenCharacters(character, targetRef, Gameplay.CurrentCharacters.Count);
                    if (checkDistance > detectedDistance)
                    {
                        detectedDistance = checkDistance;
                    }
                }
            }
            foreach (Character character in Characters.Instance.GetCharactersAtRange(detectedDistance, targetRef))
            {
                selection.Add(character);
            }
            if (detectedDistance == 1)
            {
                info = string.Format("#{0} is {1} card away from their furthest Evil", targetRef.id, detectedDistance);
            }
            else
            {
                info = string.Format("#{0} is {1} cards away from their furthest Evil", targetRef.id, detectedDistance);
            }
        }
        if (InfoType == "OutcastDistance")
        {
            checkDistance = 1000;
            detectedDistance = 1000;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.GetCharacterType() == ECharacterType.Outcast && character != targetRef)
                {
                    checkDistance = GetDistanceBetweenCharacters(character, targetRef, Gameplay.CurrentCharacters.Count);
                    if (checkDistance < detectedDistance)
                    {
                        detectedDistance = checkDistance;
                    }
                }
            }
            if (detectedDistance == 1000)
            {
                info = string.Format("#{0} is the only Outcast", targetRef.id, detectedDistance);
            }
            else
            {
                foreach (Character character in Characters.Instance.GetCharactersAtRange(detectedDistance, targetRef))
                {
                    selection.Add(character);
                }
                if (detectedDistance == 1)
                {
                    info = string.Format("#{0} is {1} card away from their closest Outcast", targetRef.id, detectedDistance);
                }
                else
                {
                    info = string.Format("#{0} is {1} cards away from their closest Outcast", targetRef.id, detectedDistance);
                }
            }
        }
        if (InfoType == "Misregister")
        {
            info = string.Format("#{0} is Registering falsely", targetRef.id);
        }
        if (InfoType == "AffectedByEvil")
        {
            info = string.Format("#{0} was affected by an Evil ability", targetRef.id);
        }
        if (InfoType == "TeamRelation")
        {
            Il2CppSystem.Collections.Generic.List<Character> allChars = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character != targetRef)
                {
                    allChars.Add(character);
                }
            }
            Character targetChar = allChars[UnityEngine.Random.RandomRangeInt(0, allChars.Count)];
            selection.Add(targetChar);
            if (targetChar.GetRegisterAlignment() == targetRef.GetRegisterAlignment())
            {
                info = string.Format("#{0} is on the same team as #{1}", targetRef.id, targetChar.id);
            }
            else
            {
                info = string.Format("#{0} is enemies with #{1}", targetRef.id, targetChar.id);
            }
        }
        return new ActedInfo(info, selection);
    }

    private ActedInfo getRandomFalseInfo(Character charRef, Character targetRef, string InfoType)
    {
        UnityEngine.Debug.Log(string.Format("Getting false Gossip info of type {0}", InfoType));
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(targetRef);
        string info = string.Format("Placeholder info, you shouldn't see this. Please notify Wingidon that the info type of {0} is bugged for a Lying Gossip.", InfoType);
        bool bluffing = false;
        if (InfoType == "Type")
        {
            Il2CppSystem.Collections.Generic.List<string> types = new Il2CppSystem.Collections.Generic.List<string>();
            if (targetRef.GetCharacterType() != ECharacterType.Villager)
            {
                types.Add("a Villager");
                types.Add("a Villager");
            }
            if (targetRef.GetCharacterType() != ECharacterType.Outcast)
            {
                types.Add("an Outcast");
            }
            if (targetRef.GetCharacterType() != ECharacterType.Minion)
            {
                types.Add("a Minion");
            }
            info = string.Format("#{0} is {1}", targetRef.id, types[UnityEngine.Random.RandomRangeInt(0,types.Count)]);
        }
        if (InfoType == "Good")
        {
            info = string.Format("#{0} is Good", targetRef.id);
        }
        if (InfoType == "Truthfulness")
        {
            bool targetLying = false;
            if (targetRef.bluff == null || targetRef.statuses.Contains(ECharacterStatus.Corrupted))
            {
                targetLying = true;
            }
            if (targetRef.statuses.Contains(ECharacterStatus.HealthyBluff) || targetRef.bluff.characterId == "Confessor_18741708" || targetRef.dataRef.characterId == "Confessor_18741708")
            {
                targetLying = false;
            }
            if (targetLying)
            {
                info = string.Format("#{0} is telling the Truth", targetRef.id);
            }
            else
            {
                info = string.Format("#{0} is Lying", targetRef.id);
            }
        }
        if (InfoType == "Honesty")
        {
            if (targetRef.bluff)
            {
                bluffing = true;
            }
            if (bluffing)
            {
                info = string.Format("#{0} is Honest", targetRef.id);
            }
            else
            {
                info = string.Format("#{0} is Disguised", targetRef.id);
            }
        }
        if (InfoType == "Corruption")
        {
            info = string.Format("#{0} is Corrupted", targetRef.id);
        }
        Il2CppSystem.Collections.Generic.List<Character> neighbours = Characters.Instance.GetAdjacentCharacters(targetRef);
        int detectedNeighbours = 0;
        Il2CppSystem.Collections.Generic.List<string> possibleCounts = new Il2CppSystem.Collections.Generic.List<string>();
        if (InfoType == "VillagerNeighbours")
        {
            foreach (Character neighbour in neighbours)
            {
                selection.Add(neighbour);
                if (neighbour.GetCharacterType() == ECharacterType.Villager)
                {
                    detectedNeighbours++;
                }
            }
            if (detectedNeighbours != 0)
            {
                possibleCounts.Add("0");
            }
            if (detectedNeighbours != 1)
            {
                possibleCounts.Add("1");
                possibleCounts.Add("1");
                possibleCounts.Add("1");
                possibleCounts.Add("1");
            }
            if (detectedNeighbours != 2)
            {
                possibleCounts.Add("2");
                possibleCounts.Add("2");
            }
            string chosenCount = possibleCounts[UnityEngine.Random.RandomRangeInt(0, possibleCounts.Count)];

            if (chosenCount == "1")
            {
                info = string.Format("#{0} has {1} Villager neighbour", targetRef.id, chosenCount);
            }
            else
            {
                info = string.Format("#{0} has {1} Villager neighbours", targetRef.id, chosenCount);
            }
        }
        if (InfoType == "EvilNeighbours") // TODO
        {
            foreach (Character neighbour in neighbours)
            {
                selection.Add(neighbour);
                if (neighbour.GetRegisterAlignment() == EAlignment.Evil)
                {
                    detectedNeighbours++;
                }
            }
            if (detectedNeighbours != 0)
            {
                possibleCounts.Add("0");
            }
            if (detectedNeighbours != 1)
            {
                possibleCounts.Add("1");
                possibleCounts.Add("1");
                possibleCounts.Add("1");
                possibleCounts.Add("1");
            }
            if (detectedNeighbours != 2)
            {
                possibleCounts.Add("2");
                possibleCounts.Add("2");
            }
            string chosenCount = possibleCounts[UnityEngine.Random.RandomRangeInt(0, possibleCounts.Count)];

            if (chosenCount == "1")
            {
                info = string.Format("#{0} has {1} Evil neighbour", targetRef.id, chosenCount);
            }
            else
            {
                info = string.Format("#{0} has {1} Evil neighbours", targetRef.id, chosenCount);
            }
        }
        int detectedDistance = 0;
        int checkDistance = 0;
        int detectedEvils = 0;
        Il2CppSystem.Collections.Generic.List<Character> allCharsForDistance = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character != targetRef)
            {
                allCharsForDistance.Add(character);
            }
            if (character.GetRegisterAlignment() == EAlignment.Evil)
            {
                detectedEvils++;
            }
        }
        if (InfoType == "ClosestEvilDistance") // TODO
        {
            checkDistance = 1000;
            detectedDistance = 1000;
            Il2CppSystem.Collections.Generic.List<Character> simulatedEvilTeam = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.GetRegisterAlignment() == EAlignment.Evil && character != targetRef)
                {
                    checkDistance = GetDistanceBetweenCharacters(character, targetRef, Gameplay.CurrentCharacters.Count);
                    if (checkDistance < detectedDistance)
                    {
                        detectedDistance = checkDistance;
                    }
                }
            }
            foreach (Character character in Characters.Instance.GetCharactersAtRange(detectedDistance, targetRef))
            {
                allCharsForDistance.Remove(character);
            }
            checkDistance = 1000;
            detectedDistance = 1000;
            for (int i = 0; i < detectedEvils; i++)
            {
                Character simulatedEvil = allCharsForDistance[UnityEngine.Random.RandomRangeInt(0, allCharsForDistance.Count)];
                checkDistance = GetDistanceBetweenCharacters(simulatedEvil, targetRef, Gameplay.CurrentCharacters.Count);
                if (checkDistance < detectedDistance)
                {
                    detectedDistance = checkDistance;
                }
            }
            foreach (Character character in Characters.Instance.GetCharactersAtRange(detectedDistance, targetRef))
            {
                selection.Add(character);
            }
            if (detectedDistance == 1)
            {
                info = string.Format("#{0} is {1} card away from their closest Evil", targetRef.id, detectedDistance);
            }
            else
            {
                info = string.Format("#{0} is {1} cards away from their closest Evil", targetRef.id, detectedDistance);
            }
        }
        if (InfoType == "FurthestEvilDistance") // TODO
        {
            checkDistance = 0;
            detectedDistance = 0;
            Il2CppSystem.Collections.Generic.List<Character> simulatedEvilTeam = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.GetRegisterAlignment() == EAlignment.Evil && character != targetRef)
                {
                    checkDistance = GetDistanceBetweenCharacters(character, targetRef, Gameplay.CurrentCharacters.Count);
                    if (checkDistance > detectedDistance)
                    {
                        detectedDistance = checkDistance;
                    }
                }
            }
            foreach (Character character in Characters.Instance.GetCharactersAtRange(detectedDistance, targetRef))
            {
                allCharsForDistance.Remove(character);
            }
            checkDistance = 0;
            detectedDistance = 0;
            for (int i = 0; i < detectedEvils; i++)
            {
                Character simulatedEvil = allCharsForDistance[UnityEngine.Random.RandomRangeInt(0, allCharsForDistance.Count)];
                checkDistance = GetDistanceBetweenCharacters(simulatedEvil, targetRef, Gameplay.CurrentCharacters.Count);
                if (checkDistance > detectedDistance)
                {
                    detectedDistance = checkDistance;
                }
            }
            foreach (Character character in Characters.Instance.GetCharactersAtRange(detectedDistance, targetRef))
            {
                selection.Add(character);
            }
            if (detectedDistance == 1)
            {
                info = string.Format("#{0} is {1} card away from their furthest Evil", targetRef.id, detectedDistance);
            }
            else
            {
                info = string.Format("#{0} is {1} cards away from their furthest Evil", targetRef.id, detectedDistance);
            }
        }
        if (InfoType == "OutcastDistance") // TODO
        {
            info = "I changed my mind about this being possible info";
        }
        if (InfoType == "Misregister") // TODO
        {
            info = string.Format("#{0} is Registering falsely", targetRef.id);
        }
        if (InfoType == "AffectedByEvil") // TODO
        {
            info = string.Format("#{0} was affected by an Evil ability", targetRef.id);
        }
        if (InfoType == "TeamRelation") // TODO
        {
            Il2CppSystem.Collections.Generic.List<Character> allChars = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character != targetRef)
                {
                    allChars.Add(character);
                }
            }
            Character targetChar = allChars[UnityEngine.Random.RandomRangeInt(0, allChars.Count)];
            selection.Add(targetChar);
            if (targetChar.GetRegisterAlignment() != targetRef.GetRegisterAlignment())
            {
                info = string.Format("#{0} is on the same team as #{1}", targetRef.id, targetChar.id);
            }
            else
            {
                info = string.Format("#{0} is enemies with #{1}", targetRef.id, targetChar.id);
            }
        }
        return new ActedInfo(info, selection);
    }
    public w_Gravekeeper() : base(ClassInjector.DerivedConstructorPointer<w_Gravekeeper>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Gravekeeper(System.IntPtr ptr) : base(ptr)
    {
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
    public Il2CppSystem.Collections.Generic.List<Character> SortList(Il2CppSystem.Collections.Generic.List<Character> list)
    {
        Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
        for (int i = 0; i < Gameplay.CurrentCharacters.Count + 3; i++)
        {
            foreach (Character character in list)
            {
                if (character.id == i) newList.Add(character);
            }
        }
        return newList;
    }
}