using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppRewired.Glyphs;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using UnityEngine;
using static MelonLoader.MelonLaunchOptions;

namespace WingidonExpansionPack
{
    // Token: 0x02000007 RID: 7
    [RegisterTypeInIl2Cpp]
    public class wx_SavedScripts : Role
    {
        // Token: 0x06000019 RID: 25 RVA: 0x0000359C File Offset: 0x0000179C
        public wx_SavedScripts() : base(ClassInjector.DerivedConstructorPointer<wx_SavedScripts>())
        {
            ClassInjector.DerivedConstructorBody(this);
        }

        // Token: 0x0600001A RID: 26 RVA: 0x000035B2 File Offset: 0x000017B2
        public wx_SavedScripts(IntPtr ptr) : base(ptr)
        {
        }

    




        // This is where I store my miscellaneous scripts and some various things that aren't tied to any particular character.

    public Il2CppSystem.Collections.Generic.List<Character> SortList(Il2CppSystem.Collections.Generic.List<Character> list)
        {
            Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
            if (list.Count == 0) return newList;
            for (int i = 0; i < Gameplay.CurrentCharacters.Count + 3; i++)
            {
                foreach (Character character in list)
                {
                    if (character.id == i) newList.Add(character);
                }
            }
            return newList;
        }
        public Il2CppSystem.Collections.Generic.List<ECharacterType> SortList(Il2CppSystem.Collections.Generic.List<ECharacterType> list)
        {
            Il2CppSystem.Collections.Generic.List<ECharacterType> newList = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
            if (list.Count == 0) return newList;
            foreach (ECharacterType characterType in list)
            {
                if (characterType == ECharacterType.Villager)
                {
                    newList.Add(characterType);
                }
            }
            foreach (ECharacterType characterType in list)
            {
                if (characterType == ECharacterType.Outcast)
                {
                    newList.Add(characterType);
                }
            }
            foreach (ECharacterType characterType in list)
            {
                if (characterType == ECharacterType.Minion)
                {
                    newList.Add(characterType);
                }
            }
            foreach (ECharacterType characterType in list)
            {
                if (characterType == ECharacterType.Demon)
                {
                    newList.Add(characterType);
                }
            }
            return newList;
        }
        public Il2CppSystem.Collections.Generic.List<Character> GetFakeEvilTeam()
        {
            Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
            Character target = new Character();
            int totalEvils = 0;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                possibleTargets.Add(character);
                if (character.GetRegisterAlignment() == EAlignment.Evil)
                {
                    totalEvils++;
                }
            }
            for (int i = 0; i < totalEvils; i++)
            {
                target = possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)];
                newList.Add(target);
                possibleTargets.Remove(target);
            }
            return newList;
        }
        public Il2CppSystem.Collections.Generic.List<Character> GetFakeGroup(Il2CppSystem.Collections.Generic.List<Character> targets)
        {
            // MelonLogger.Msg("Getting fake group");
            Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
            Character target = new Character();
            int totalTargets = targets.Count;
            // MelonLogger.Msg($"Fake group has {totalTargets} characters in it");
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                possibleTargets.Add(character);
            }
            for (int i = 0; i < totalTargets; i++)
            {
                target = possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)];
                // MelonLogger.Msg($"Added #{target.id} to fake group");
                newList.Add(target);
                possibleTargets.Remove(target);
            }
            MelonLogger.Msg($"Fake group is {MentionEveryCharacterInList(newList, "and")}");
            return newList;
        }

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

        public int GetClosestDistance(Il2CppSystem.Collections.Generic.List<Character> targets, Character anchor)
        {
            int dist = 10000;
            int calcDist = 0;
            foreach (Character character in targets)
            {
                calcDist = GetDistanceBetweenCharacters(character, anchor);
                if (calcDist != 0 && dist > calcDist)
                {
                    dist = calcDist;
                }
            }
            if (dist == 10000) dist = 1;
            return dist;
        }

        public int GetFurthestDistance(Il2CppSystem.Collections.Generic.List<Character> targets, Character anchor)
        {
            int dist = 0;
            int calcDist = 0;
            foreach (Character character in targets)
            {
                calcDist = GetDistanceBetweenCharacters(character, anchor);
                if (calcDist != 0 && dist < calcDist)
                {
                    dist = calcDist;
                }
            }
            return dist;
        }


        public float GetTrustworthiness(Character target)
        {
            float trust = 1f;
            if (target.GetRegisterAs().type == ECharacterType.Villager) trust *= 5;
            if (target.GetRegisterAs().type == ECharacterType.Outcast) trust *= 3;
            if (target.GetRegisterAs().type == ECharacterType.Minion) trust *= 3;
            if (target.GetRegisterAlignment() == EAlignment.Good) trust *= 3;
            if (!CharacterHelper.CheckLyingAppearance(target)) trust *= 3;
            if (!CharacterHelper.CheckIfDisguisedAppearance(target)) trust *= 2.5f;
            return trust;
        }

        public string MentionEveryCharacterInList(Il2CppSystem.Collections.Generic.List<Character> characters, string andOr)
        {
            string returnString = "Return";
            int characterCount = characters.Count;
            int counter = 0;
            Il2CppSystem.Collections.Generic.List<Character> sortedCharacters = SortList(characters);
            foreach (Character character in sortedCharacters)
            {
                if (returnString == "Return")
                {
                    counter++;
                    returnString = $"#{character.id}";
                }
                else
                {
                    counter++;
                    if (counter == characterCount)
                    {
                        if (andOr == "And" || andOr == "and")
                        {
                            returnString = $"{returnString} and #{character.id}";
                        }
                        else if (andOr == "Or" || andOr == "or")
                        {
                            returnString = $"{returnString} or #{character.id}";
                        }
                        else
                        {
                            returnString = $"{returnString}, #{character.id}";
                        }
                    }
                    else
                    {
                        returnString = $"{returnString}, #{character.id}";
                    }
                }
            }
            return returnString;
        }


        public string MentionEveryRoleInList(Il2CppSystem.Collections.Generic.List<CharacterData> characters, string andOr)
        {
            string returnString = "Return";
            int characterCount = characters.Count;
            int counter = 0;
            Il2CppSystem.Collections.Generic.List<CharacterData> sortedCharacters = characters;
            foreach (CharacterData character in sortedCharacters)
            {
                if (returnString == "Return")
                {
                    counter++;
                    returnString = $"{character.characterName}";
                }
                else
                {
                    counter++;
                    if (counter == characterCount)
                    {
                        if (andOr == "And" || andOr == "and")
                        {
                            returnString = $"{returnString} and {character.characterName}";
                        }
                        else if (andOr == "Or" || andOr == "or")
                        {
                            returnString = $"{returnString} or {character.characterName}";
                        }
                        else
                        {
                            returnString = $"{returnString}, {character.characterName}";
                        }
                    }
                    else
                    {
                        returnString = $"{returnString}, {character.characterName}";
                    }
                }
            }
            return returnString;
        }

        public string MentionEveryStringInList(Il2CppSystem.Collections.Generic.List<string> characters, string andOr)
        {
            string returnString = "Return";
            int characterCount = characters.Count;
            int counter = 0;
            Il2CppSystem.Collections.Generic.List<string> sortedCharacters = characters;
            foreach (CharacterData character in sortedCharacters)
            {
                if (returnString == "Return")
                {
                    counter++;
                    returnString = $"{character}";
                }
                else
                {
                    counter++;
                    if (counter == characterCount)
                    {
                        if (andOr == "And" || andOr == "and")
                        {
                            returnString = $"{returnString} and {character}";
                        }
                        else if (andOr == "Or" || andOr == "or")
                        {
                            returnString = $"{returnString} or {character}";
                        }
                        else
                        {
                            returnString = $"{returnString}, {character}";
                        }
                    }
                    else
                    {
                        returnString = $"{returnString}, {character}";
                    }
                }
            }
            return returnString;
        }

        public string MentionEveryCharacterInUnsortedList(Il2CppSystem.Collections.Generic.List<Character> characters, string andOr)
        {
            string returnString = "Return";
            int characterCount = characters.Count;
            int counter = 0;
            Il2CppSystem.Collections.Generic.List<Character> sortedCharacters = characters;
            foreach (Character character in sortedCharacters)
            {
                if (returnString == "Return")
                {
                    counter++;
                    returnString = $"#{character.id}";
                }
                else
                {
                    counter++;
                    if (counter == characterCount)
                    {
                        if (andOr == "And" || andOr == "and")
                        {
                            returnString = $"{returnString} and #{character.id}";
                        }
                        else if (andOr == "Or" || andOr == "or")
                        {
                            returnString = $"{returnString} or #{character.id}";
                        }
                        else if (andOr == "Then" || andOr == "then")
                        {
                            returnString = $"{returnString}, then #{character.id}";
                        }
                        else
                        {
                            returnString = $"{returnString}, #{character.id}";
                        }
                    }
                    else if (andOr == "Then" || andOr == "then")
                    {
                        returnString = $"{returnString}, then #{character.id}";
                    }
                }
            }
            return returnString;
        }

        public string MentionEveryTypeInList(Il2CppSystem.Collections.Generic.List<ECharacterType> types, string andOr)
        {
            string returnString = "Return";
            int characterCount = types.Count;
            int counter = 0;
            Il2CppSystem.Collections.Generic.List<ECharacterType> sortedTypes = SortList(types);
            foreach (ECharacterType type in sortedTypes)
            {
                if (returnString == "Return")
                {
                    counter++;
                    returnString = type.ToString();
                }
                else
                {
                    counter++;
                    if (counter == characterCount)
                    {
                        if (andOr == "And" || andOr == "and")
                        {
                            returnString = $"{returnString} and {type.ToString()}";
                        }
                        else if (andOr == "Or" || andOr == "or")
                        {
                            returnString = $"{returnString} or {type.ToString()}";
                        }
                        else
                        {
                            returnString = $"{returnString}, {type.ToString()}";
                        }
                    }
                    else
                    {
                        returnString = $"{returnString}, {type.ToString()}";
                    }
                }
            }
            return returnString;
        }

        public bool CheckIfNeighbour(Character character1, Character character2)
        {
            if (GetDistanceBetweenCharacters(character1, character2) == 1) return true;
            return false;
        }
        public bool PercentChance(float percentage)
        {
            if (UnityEngine.Random.RandomRange(0, 100) <= percentage) return true;
            return false;
        }

        public Il2CppSystem.Collections.Generic.List<Character> GetCharacterNeighbours(Character targetChar)
        {
            Il2CppSystem.Collections.Generic.List<Character> outputList = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (CheckIfNeighbour(targetChar, character)) outputList.Add(character);
            }
            return outputList;
        }

        public Il2CppSystem.Collections.Generic.List<Character> GetCharactersWithinRange(Character charRef, int range)
        {
            Il2CppSystem.Collections.Generic.List<Character> outputList = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (GetDistanceBetweenCharacters(charRef, character) <= range && character != charRef)
                {
                    outputList.Add(character);
                }
            }
            return outputList;
        }



        public Il2CppSystem.Collections.Generic.List<string> AddStringToList(string input, Il2CppSystem.Collections.Generic.List<string> list, int weight)
        {
            for (int i = 0; i < weight; i++)
            {
                list.Add(input);
            }
            return list;
        }


        public string CheckIfThe(string Name)
        {
            Il2CppSystem.Collections.Generic.List<string> pluralNamesV = new Il2CppSystem.Collections.Generic.List<string>(); // There may be many of this character, they start with a vowel. #X is an Y.
            Il2CppSystem.Collections.Generic.List<string> pluralNamesC = new Il2CppSystem.Collections.Generic.List<string>(); // There may be many of this character, they start with a consonant. #X is a Y.
            Il2CppSystem.Collections.Generic.List<string> demonNames = new Il2CppSystem.Collections.Generic.List<string>(); // This character has an actual name. #X is Y.

            // Villagers
            pluralNamesC.Add("Citizen");

            // Outcasts
            pluralNamesC.Add("Pariah");
            pluralNamesC.Add("Trickster"); // From Skill Cycler's Riddles mod.

            // Minions
            demonNames.Add("Swarm");
            pluralNamesV.Add("Acolyte");
            pluralNamesV.Add("Underling");
            pluralNamesC.Add("Fanatic");
            pluralNamesC.Add("Mastermind"); // From Skill Cycler's Riddles mod
            pluralNamesC.Add("Zealot");

            // Vanilla
            demonNames.Add("Baa");
            demonNames.Add("Lilis");
            demonNames.Add("Pooka");

            // Wingidon's Expansion Pack
            demonNames.Add("Agmeres");
            demonNames.Add("Caedoccidere");
            demonNames.Add("Carnicarius");
            demonNames.Add("Emenverax");
            demonNames.Add("Iris");
            demonNames.Add("Leviathan");
            demonNames.Add("Magnere");
            demonNames.Add("Mendaverte");
            demonNames.Add("Praesect");
            demonNames.Add("Sanguitaurus");
            demonNames.Add("Specularus");
            demonNames.Add("Tenecaligo");
            demonNames.Add("Venelum");
            demonNames.Add("Veniyon");
            demonNames.Add("Vidiyon");
            demonNames.Add("Viciyon");

            // Role Ideas Collection
            demonNames.Add("Death");

            // ExtraRandomized
            demonNames.Add("Better Baa");

            // CarlzVilliagePack
            pluralNamesC.Add("Hydra");
            demonNames.Add("Pestilence");

            // CSK's Expansion Pack
            demonNames.Add("Belias");

            // LRZH's Circus
            demonNames.Add("Dominion");
            demonNames.Add("Mahr");
            demonNames.Add("Po");

            // Power Play
            demonNames.Add("Snowed In");
            demonNames.Add("Death");
            demonNames.Add("Famine");
            demonNames.Add("Pestilence");
            demonNames.Add("War");

            if (pluralNamesV.Contains(Name))
            {
                return "an ";
            }
            if (pluralNamesC.Contains(Name))
            {
                return "a ";
            }
            if (demonNames.Contains(Name))
            {
                return "";
            }
            return "the ";
        }




        public CharacterData GetOverrideNotInPlayBluff(Character charRef, bool outcastsAllowed)
        {
            CharacterData bluff = Characters.Instance.GetRandomUniqueBluff();
            Il2CppSystem.Collections.Generic.List<string> blacklistBluffs = new Il2CppSystem.Collections.Generic.List<string>();
            blacklistBluffs.Add("Bounty Hunter_39284184");
            if (charRef.dataRef.characterId == "Iris_WING") blacklistBluffs.Add("Baker_22847064");
            Il2CppSystem.Collections.Generic.List<CharacterData> possibleBluffs = Characters.Instance.FilterAlignmentCharacters(Gameplay.Instance.GetAllAscensionCharacters(), EAlignment.Good);
            Il2CppSystem.Collections.Generic.List<CharacterData> removeBluffs = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            possibleBluffs = Characters.Instance.FilterBluffableCharacters(possibleBluffs);
            foreach (CharacterData character in possibleBluffs)
            {
                if (blacklistBluffs.Contains(character.characterId))
                {
                    removeBluffs.Add(character);
                }
                if (!outcastsAllowed && character.type == ECharacterType.Outcast)
                {
                    removeBluffs.Add(character);
                }
            }
            foreach (CharacterData character in removeBluffs)
            {
                possibleBluffs.Remove(character);
            }
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                while (possibleBluffs.Contains(character.dataRef))
                {
                    possibleBluffs.Remove(character.dataRef);
                }
            }
            if (possibleBluffs.Count != 0)
            {
                bluff = possibleBluffs[UnityEngine.Random.RandomRangeInt(0, possibleBluffs.Count)];
            }
            return bluff;
        }

        public static int RoundValToInt(decimal val)
        {
            return (int)Math.Round(val);
        }

        public CharacterData GetOverrideDuplicateBluff(Character charRef)
        {
            CharacterData bluff = Characters.Instance.GetRandomDuplicateBluff();
            Il2CppSystem.Collections.Generic.List<CharacterData> possibleBluffs = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.dataRef.bluffable == true && character.dataRef.startingAlignment == EAlignment.Good)
                {
                    possibleBluffs.Add(character.dataRef);
                }
            }
            if (possibleBluffs.Count != 0)
            {
                bluff = possibleBluffs[UnityEngine.Random.RandomRangeInt(0, possibleBluffs.Count)];
            }
            return bluff;
        }

        public void TurnEvilIfPossible(Character character)
        {

            if (CheckIfAlwaysGood(character)) character.ChangeAlignment(EAlignment.Evil);
        }

        public bool CheckIfAlwaysGood(Character character)
        {
            Il2CppSystem.Collections.Generic.List<string> alwaysGoodIDs = new Il2CppSystem.Collections.Generic.List<string>();
            alwaysGoodIDs.Add("Saint_61372493");
            alwaysGoodIDs.Add("Politician_WING");
            alwaysGoodIDs.Add("Saint_WING");

            if (alwaysGoodIDs.Contains(character.dataRef.characterId)) return true;
            return false;
        }


        public int GetPairsOfCharactersInList(Il2CppSystem.Collections.Generic.List<Character> list)
        {
            //MelonLogger.Msg("Getting pairs");
            Il2CppSystem.Collections.Generic.List<Character> allCharactersPlusOne = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                allCharactersPlusOne.Add(character);
            }
            allCharactersPlusOne.Add(Gameplay.CurrentCharacters[0]);

            bool prevCount = false;
            int pairs = 0;
            foreach (Character character in allCharactersPlusOne)
            {
                //MelonLogger.Msg($"Checking #{character.id}");
                if (list.Contains(character) && prevCount == true)
                {
                    pairs++;
                    //MelonLogger.Msg($"Found a pair, there are now {pairs} pair(s)");
                }
                if (list.Contains(character))
                {
                    prevCount = true;
                    //MelonLogger.Msg($"#{character.id} is in the list, so they're ready to be part of the next pair");
                }
                else
                {
                    prevCount = false;
                    //MelonLogger.Msg($"#{character.id} is not in the list, so they will not be part of the next pair");
                }
                
            }
            return pairs;
        }


        public void DebugMessage(string message)
        {
            string debugVal = MelonPreferences.GetCategory("WingModSettings").GetEntry("DebugMode").GetValueAsString();
            if (debugVal == "True" || debugVal == "true" || debugVal == "t")
            {
                MelonLogger.Msg("DEBUG: " + message);
            }
        }



        public ActedInfo ReturnInfoWithSingleSelection(string info, Character selection)
        {
            Il2CppSystem.Collections.Generic.List<Character> selectionList = new Il2CppSystem.Collections.Generic.List<Character>();
            selectionList.Add(selection);
            return new ActedInfo(info, selectionList);
        }



        public Character GetRandomItemOfList(Il2CppSystem.Collections.Generic.List<Character> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            return list[UnityEngine.Random.RandomRangeInt(0, list.Count)];
        }
        public CharacterData GetRandomItemOfList(Il2CppSystem.Collections.Generic.List<CharacterData> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            return list[UnityEngine.Random.RandomRangeInt(0, list.Count)];
        }
        public string GetRandomItemOfList(Il2CppSystem.Collections.Generic.List<string> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            return list[UnityEngine.Random.RandomRangeInt(0, list.Count)];
        }
        public int GetRandomItemOfList(Il2CppSystem.Collections.Generic.List<int> list)
        {
            if (list.Count == 0)
            {
                return -1;
            }
            return list[UnityEngine.Random.RandomRangeInt(0, list.Count)];
        }

        public Il2CppSystem.Collections.Generic.List<CharacterData> GetUnderlingDatas(Character charRef)
        {
            if (allDatas.Length == 0)
            {
                var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
                if (loadedCharList != null)
                {
                    allDatas = new CharacterData[loadedCharList.Length];
                    for (int j = 0; j < loadedCharList.Length; j++)
                    {
                        allDatas[j] = loadedCharList[j]!.Cast<CharacterData>();
                    }
                }
            }

            Il2CppSystem.Collections.Generic.List<CharacterData> underlingDatas = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            underlingDatas.Add(charRef.dataRef);
            underlingDatas.Add(charRef.dataRef);
            underlingDatas.Add(charRef.dataRef);

            foreach (CharacterData character in allDatas)
            {
                if (character.characterId == "Underling_V_WING") underlingDatas[0] = character;
                if (character.characterId == "Underling_O_WING") underlingDatas[1] = character;
                if (character.characterId == "Underling_M_WING") underlingDatas[2] = character;
            }
            DebugMessage($"Underling roles found: {underlingDatas[0].characterName}, {underlingDatas[1].characterName}, {underlingDatas[2].characterName}");
            return underlingDatas;
        }



        public int MakeNumberWrong(int trueNumber, int falseNumber, int minimum)
        {
            int returnVal = falseNumber;
            if (returnVal < minimum)
            {
                while (returnVal < minimum) returnVal++;
            }
            if (trueNumber != falseNumber) return falseNumber;
            if (falseNumber == minimum) returnVal++;
            else returnVal--;
            return returnVal;
        }

        public int MakeNumberWrongByRange(int trueNumber, int falseNumber, int minimum, int maximum, int maxSubtract, int maxAdd)
        {
            int returnVal = falseNumber;
            if (returnVal < minimum)
            {
                while (returnVal < minimum) returnVal++;
            }
            if (returnVal > maximum)
            {
                while (returnVal > maximum) returnVal--;
            }
            if (trueNumber != falseNumber) return falseNumber;
            Il2CppSystem.Collections.Generic.List<int> possibleModifiers = new Il2CppSystem.Collections.Generic.List<int>();
            for (int i = (maxSubtract*-1); i < (maxAdd+1); i++)
            {
                if (i != 0 && (returnVal+i <= maximum) && (returnVal + i >= minimum))
                {
                    possibleModifiers.Add(i);
                }
            }
            if (possibleModifiers.Count == 0) return MakeNumberWrong(trueNumber, falseNumber, minimum);
            returnVal += possibleModifiers[UnityEngine.Random.RandomRangeInt(0, possibleModifiers.Count)];
            return returnVal;
        }

        public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
        public CharacterData GrabCharacterDataByID(string characterID)
        {
            if (allDatas.Length == 0)
            {
                var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
                if (loadedCharList != null)
                {
                    allDatas = new CharacterData[loadedCharList.Length];
                    for (int j = 0; j < loadedCharList.Length; j++)
                    {
                        allDatas[j] = loadedCharList[j]!.Cast<CharacterData>();
                    }
                }
            }

            for (int j = 0; j < allDatas.Length; j++)
            {
                if (characterID == allDatas[j].characterId)
                {
                    return allDatas[j];
                }
            }
            return null;
        }





        public Il2CppSystem.Collections.Generic.List<CharacterData> GetScriptRoles(EAlignment alignment)
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
            {
                if (character.startingAlignment == alignment)
                {
                    returnList.Add(character);
                }
            }
            return returnList;
        }

        public Il2CppSystem.Collections.Generic.List<CharacterData> GetScriptRoles(ECharacterType type)
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
            {
                if (character.type == type)
                {
                    returnList.Add(character);
                }
            }
            if (type == ECharacterType.Demon)
            {
                foreach (CharacterData character in GetAllDemons())
                {
                    returnList.Add(character);
                }
            }
            return returnList;
        }

        public Il2CppSystem.Collections.Generic.List<CharacterData> GetScriptRoles(EAlignment alignment, ECharacterType type)
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
            {
                if (character.startingAlignment == alignment && character.type == type)
                {
                    returnList.Add(character);
                }
            }
            if (type == ECharacterType.Demon)
            {
                foreach (CharacterData character in GetAllDemons())
                {
                    if (character.startingAlignment == alignment) returnList.Add(character);
                }
            }
            return returnList;
        }

        public Il2CppSystem.Collections.Generic.List<CharacterData> GetScriptRoles()
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (CharacterData character in GetScriptRoles(EAlignment.Good)) returnList.Add(character);
            foreach (CharacterData character in GetScriptRoles(EAlignment.Evil)) returnList.Add(character);
            return returnList;
        }

        public Il2CppSystem.Collections.Generic.List<CharacterData> GetAllDemons()
        {
            if (allDatas.Length == 0)
            {
                var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
                if (loadedCharList != null)
                {
                    allDatas = new CharacterData[loadedCharList.Length];
                    for (int j = 0; j < loadedCharList.Length; j++)
                    {
                        allDatas[j] = loadedCharList[j]!.Cast<CharacterData>();
                    }
                }
            }

            Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            for (int j = 0; j < allDatas.Length; j++)
            {
                if (allDatas[j].type == ECharacterType.Demon) returnList.Add(allDatas[j]);
            }
            return returnList;
        }

        public Il2CppSystem.Collections.Generic.List<CharacterData> GetPossibleHiddenRoles()
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
            {
                if (character.characterId == "Cryptid_WING")
                {
                    foreach (CharacterData character2 in GetScriptRoles(EAlignment.Evil, ECharacterType.Minion))
                    {
                        if (GetPossibleCharacterIDsOfRole("Cryptid_WING").Contains(character2.characterId))
                        {
                            returnList.Add(character2);
                        }
                    }
                }
                if (character.characterId == "Clown_LRZH") // This role hides the Demon
                {
                    foreach (CharacterData character2 in GetAllDemons())
                    {
                        returnList.Add(character2);
                    }
                }
            }
            return returnList;
        }




        public Il2CppSystem.Collections.Generic.List<string> GetPossibleCharacterIDsOfRole(string roleID)
        {
            Il2CppSystem.Collections.Generic.List<string> returnChars = new Il2CppSystem.Collections.Generic.List<string>();

            if (roleID == "Cryptid_WING")
            {
                // Vanilla
                returnChars.Add("Baron_04539999"); // Chancellor
                returnChars.Add("Poisoner_64796285"); // Poisoner
                returnChars.Add("Mezepheles_09511163"); // Puppeteer
                returnChars.Add("Shaman_26945607"); // Shaman
                returnChars.Add("Witch_25286521"); // Witch

                // Wingidon's Expansion Pack
                returnChars.Add("Heretic_WING"); // Heretic
                returnChars.Add("Professional_WING"); // Professional
                returnChars.Add("Ritualist_WING"); // Ritualist
                returnChars.Add("Saboteur_WING"); // Saboteur
                returnChars.Add("Snake Charmer_WING"); // Snake Charmer
                returnChars.Add("Turncoat_WING"); // Turncoat

                // Skill Cycler's Riddles
                returnChars.Add("Accuser_scm"); // Accuser
                returnChars.Add("Baffler_scm"); // Baffler
                returnChars.Add("Hypnotist_scm"); // Hypnotist

                // LRZH's Circus
                returnChars.Add("Clown_LRZH"); // Clown
                returnChars.Add("Wraith_LRZH"); // Wraith
            }
            if (roleID == "Occultist_WING")
            {
                /*
                Acts after Chancellor:
                - Accuser
                - Baffler
                - Heretic
                - Poisoner
                - Puppeteer
                - Saboteur
                - Shaman
                - Snake Charmer
                - Slanderer
                - Swarm
                - Witch

                Doesn't act on start:
                - Ritualist
                */

                // Vanilla
                returnChars.Add("Poisoner_64796285"); // Poisoner
                returnChars.Add("Mezepheles_09511163"); // Puppeteer
                returnChars.Add("Shaman_26945607"); // Shaman
                returnChars.Add("Witch_25286521"); // Witch

                // Wingidon's Expansion Pack
                returnChars.Add("Heretic_WING"); // Heretic
                returnChars.Add("Ritualist_WING"); // Ritualist
                returnChars.Add("Saboteur_WING"); // Saboteur
                returnChars.Add("Snake Charmer_WING"); // Snake Charmer 
                returnChars.Add("Swarm_Good_WING"); // Good Swarm 

                // Skill Cycler's Riddles
                returnChars.Add("Accuser_scm"); // Accuser
                returnChars.Add("Baffler_scm"); // Baffler
                returnChars.Add("Slanderer_scm"); // Slanderer
            }
            if (roleID == "Mutant_WING")
            {
                // Vanilla
                returnChars.Add("Minion_71804875"); // Minion
                returnChars.Add("Poisoner_64796285"); // Poisoner
                returnChars.Add("Mezepheles_09511163"); // Puppeteer
                returnChars.Add("Shaman_26945607"); // Shaman
                returnChars.Add("Twin Minion_15695218"); // Twin Minion
                returnChars.Add("Witch_25286521"); // Witch

                // Wingidon's Expansion Pack
                returnChars.Add("Acolyte_WING"); // Acolyte
                returnChars.Add("Heretic_WING"); // Heretic
                returnChars.Add("Professional_WING"); // Professional
                returnChars.Add("Ritualist_WING"); // Ritualist
                returnChars.Add("Saboteur_WING"); // Saboteur
                returnChars.Add("Snake Charmer_WING"); // Snake Charmer
                returnChars.Add("Swarm_Good_WING"); // Swarm
                returnChars.Add("Turncoat_WING"); // Turncoat
                returnChars.Add("Undying_WING"); // Undying
                returnChars.Add("Zealot_WING"); // Zealot

                // Skill Cycler's Riddles
                returnChars.Add("Accuser_scm"); // Accuser
                returnChars.Add("Baffler_scm"); // Baffler
                returnChars.Add("Guardian_scm"); // Guardian
                returnChars.Add("Hypnotist_scm"); // Hypnotist

                // LZRH's Circus
                returnChars.Add("Clown_LRZH"); // Clown

                // Tavern Mod
                returnChars.Add("Brewer_TAVERN"); // Brewer
                returnChars.Add("Florist_TAVERN"); // Florist
                returnChars.Add("Gangster_TAVERN"); // Gangster
                returnChars.Add("Strategist_TAVERN"); // Strategist
                returnChars.Add("Summoner_TAVERN"); // Summoner
                returnChars.Add("Trickster_TAVERN"); // Trickster

                // Mass Hysteria
                returnChars.Add("Siren_MaHy"); // Siren

                // ExtraRandomised
                returnChars.Add("Purifier_ER"); // Purifier

                // CarlzVillagePack
                returnChars.Add("Husher_VP"); // Blackmailer
                returnChars.Add("Lycaon_VP"); // Lycaon

                // RevealDilemma
                returnChars.Add("Ambush_rdm"); // Ambusher
                returnChars.Add("Martyr_rdm"); // Martyr

                // CSK's Expansion Pack
                returnChars.Add("Cavalier_EP"); // Cavalier
            }
            if (roleID == "Pandemonium_WING")
            {
                // Vanilla
                returnChars.Add("Imp_58992273"); // Baa
                returnChars.Add("Lillith_90453844"); // Lilis
                returnChars.Add("Pooka_13445289"); // Pooka

                // Wingidon's Expansion Pack
                returnChars.Add("Caedoccidere_WING"); // Caedoccidere
                returnChars.Add("Carnicarius_WING"); // Carnicarius
                returnChars.Add("Iris_WING"); // Iris
                returnChars.Add("Leviathan_WING"); // Leviathan
                //returnChars.Add("Mendaverte_WING"); // Mendaverte
                returnChars.Add("Praesect_WING"); // Praesect
                returnChars.Add("Mezepheles_WING"); // Venelum

                // Skill Cycler's Riddles
                returnChars.Add("Escapist_scm"); // Escapist
                returnChars.Add("Follower_scm"); // Follower
                returnChars.Add("Infestation_scm"); // Infestation
                returnChars.Add("Kingmaker_scm"); // Kingmaker
                returnChars.Add("Mystifier_scm"); // Mystifier
                returnChars.Add("Veil_scm"); // Veil

                // LRZH's Circus
                returnChars.Add("Lleech_LRZH"); // Lleech
                returnChars.Add("Po_LRZH"); // Po

                // Reveal Dilemma
                returnChars.Add("shroud_rdm"); // Shroud

                // Mass Hysteria
                returnChars.Add("Cackler_MaHy"); // Cakler

                // ExtraRandomized
                returnChars.Add("Hypnotist_ER"); // Hypnotist

                // CarlzVillagePack
                returnChars.Add("Hydra_VP"); // Hydra
                returnChars.Add("Phantom_VP"); // Phantom

                // CSK's Expansion Pack
                returnChars.Add("Belias_EP"); // Belias
            }
            return returnChars;
        }


        /* Will finish this later
        public string GetRoleIDByName(string name)
        {
            switch (name)
            {
                // Vanilla
                case "Alchemist": return "Alchemist_94446803"; // Alchemist
                case "Architect": return ""; // Architect
                case "Baker": return ""; // Baker
                case "Bard": return ""; // Bard
                case "Bishop": return ""; // Bishop
                case "Confessor": return ""; // Confessor
                case "Dreamer": return ""; // Dreamer
                case "Druid": return ""; // Druid
                case "Empress": return ""; // Empress
                case "Enlightened": return ""; // Enlightened
                case "Fortune Teller": return ""; // Fortune Teller
                case "Gemcrafter": return ""; // Gemcrafter
                case "Hunter": return ""; // Hunter
                case "Investigator": return ""; // Investigator
                case "Jester": return ""; // Jester
                case "Judge": return ""; // Judge
                case "Knight": return ""; // Knight
                case "Knitter": return ""; // Knitter
                case "Lover": return ""; // Lover
                case "Medium": return ""; // Medium
                case "Oracle": return ""; // Oracle
                case "Poet": return ""; // Poet
                case "Scout": return ""; // Scout
                case "Slayer": return ""; // Slayer
                case "Witness": return ""; // Witness

                case "Bombardier": return ""; // Bombardier
                case "Doppelganger": return ""; // Doppelganger
                case "Drunk": return ""; // Drunk
                case "Lycanthrope": return ""; // Lycanthrope
                case "Plague Doctor": return ""; // Plague Doctor
                case "Rambler": return ""; // Rambler
                case "Wretch": return ""; // Wretch

                case "Chancellor": return ""; // Chancellor
                case "Minion": return ""; // Minion
                case "Poisoner": return ""; // Poisoner
                case "Puppet": return ""; // Puppet
                case "Puppeteer": return ""; // Puppeteer
                case "Shaman": return ""; // Shaman
                case "Twin Minion": return ""; // Twin Minion
                case "": return ""; // Werewolf
                case "": return ""; // Witch

                case "": return ""; // Baa
                case "": return ""; // Lilis
                case "": return ""; // Pooka


                
                // Wingidon's Expansion Pack
                case "": return ""; // Arbiter
                case "": return ""; // Arithmetician
                case "": return ""; // Bloodseer
                case "": return ""; // Cardshark
                case "": return ""; // Chiromancer
                case "": return ""; // Clairvoyant
                case "": return ""; // Copycat
                case "": return ""; // Detective
                case "": return ""; // Devout
                case "": return ""; // Forager
                case "": return ""; // Gossip
                case "": return ""; // Graveakeeper
                case "": return ""; // Introvert
                case "": return ""; // Jewelsmith
                case "": return ""; // Knave
                case "": return ""; // Lamb
                case "": return ""; // Performer
                case "": return ""; // Prince
                case "": return ""; // Ranger
                case "": return ""; // Scavenger
                case "": return ""; // Sentinel
                case "": return ""; // Sheriff
                case "": return ""; // Spy
                case "": return ""; // Warden

                case "": return ""; // Chatterbox
                case "": return ""; // Lunatic
                case "": return ""; // Marionette
                case "": return ""; // Mutant
                case "": return ""; // Revolutionary
                case "": return ""; // Renegade
                case "": return ""; // Tergiversator

                case "": return ""; // Acolyte
                case "": return ""; // Fanatic
                case "": return ""; // Zealot
                case "": return ""; // Heretic
                case "": return ""; // Professional
                case "": return ""; // Ritualist
                case "": return ""; // Snake Charmer
                case "": return ""; // Swarm (Good)
                case "": return ""; // Swarm (Evil)
                case "": return ""; // Turncoat
                case "": return ""; // Undying

                case "": return ""; // Agmeres
                case "": return ""; // Caedoccidere
                case "": return ""; // Carnicarius
                case "": return ""; // Iris
                case "": return ""; // Leviathan
                case "": return ""; // Mendaverte
                case "": return ""; // Praesect
                case "": return ""; // Sanguitaurus
                case "": return ""; // Tenecaligo
                case "": return ""; // Venelum
                case "": return ""; // Veniyon
                case "": return ""; // Vidiyon
                case "": return ""; // Viciyon

                // Skill Cycler's Riddles
                case "": return ""; // Coach
                case "": return ""; // Comedian
                case "": return ""; // Commander
                case "": return ""; // Cowboy
                case "": return ""; // Director
                case "": return ""; // Engineer
                case "": return ""; // Governor
                case "": return ""; // Innkeeper
                case "": return ""; // Lawyer
                case "": return ""; // Mathematician
                case "": return ""; // Necromancer
                case "": return ""; // Nurse
                case "": return ""; // Obsessor
                case "": return ""; // Officer
                case "": return ""; // Pioneer
                case "": return ""; // Psychic
                case "": return ""; // Recruiter
                case "": return ""; // Riddler
                case "": return ""; // Scanner
                case "": return ""; // Stylist
                case "": return ""; // Surveyor
                case "": return ""; // Swapper
                case "": return ""; // Tracker
                case "": return ""; // Trickster
                case "": return ""; // Weaver

                case "": return ""; // Captivator
                case "": return ""; // Confectioner
                case "": return ""; // Gambler
                case "": return ""; // Ghost
                case "": return ""; // Hitman
                case "": return ""; // Mad Scientist
                case "": return ""; // Muddler
                case "": return ""; // Reflector

                case "": return ""; // Accuser
                case "": return ""; // Baffler
                case "": return ""; // Channeler
                case "": return ""; // Guardian
                case "": return ""; // Hypnotist
                case "": return ""; // Mastermind
                case "": return ""; // Sleeper
                case "": return ""; // Wizard

                case "": return ""; // Escapist
                case "": return ""; // Follower
                case "": return ""; // Infestation
                case "": return ""; // Kingmaker
                case "": return ""; // Mystifier
                case "": return ""; // Summoner
                case "": return ""; // Veil
            }
            return "";
        }
        */




        public void DoJinxes(Character charRef, string demonID, bool bluffs)
        {

            Il2CppSystem.Collections.Generic.List<string> jinxedIDs = new Il2CppSystem.Collections.Generic.List<string>();

            if (demonID == "Weather")
            {
                jinxedIDs.Add("Foggy_POW");
                jinxedIDs.Add("Snowy_POW");
                jinxedIDs.Add("SnowedIn_POW");
                jinxedIDs.Add("Stormy_POW");
                jinxedIDs.Add("Sunny_POW");
            }

            if (demonID == "Mendaverte_WING")
            {
                jinxedIDs.Add("Alchemist_94446803");
                jinxedIDs.Add("Confessor_18741708");
                jinxedIDs.Add("Knight_47970624");

                jinxedIDs.Add("Chatterbox_WING");
                jinxedIDs.Add("Lycanthrope_16077432");
                jinxedIDs.Add("MadScientist_scm");

                jinxedIDs.Add("Guardian_scm");
                jinxedIDs.Add("Mezepheles_09511163");
                jinxedIDs.Add("Poisoner_64796285");
                jinxedIDs.Add("Saboteur_WING");
                jinxedIDs.Add("Snake Charmer_WING");
                jinxedIDs.Add("Turncoat_WING");
            }

            if (demonID == "Legion_WING")
            {
                jinxedIDs.Add("Bishop_58855542");
                jinxedIDs.Add("Empress_13782227");

                jinxedIDs.Add("Confectioner_scm");
                jinxedIDs.Add("Captivator_scm");
                jinxedIDs.Add("Hypnotist_scm");
                jinxedIDs.Add("Chatterbox_WING");
                jinxedIDs.Add("Godfather_POW");
                jinxedIDs.Add("Marionette_WING");
                jinxedIDs.Add("Mobster_POW");
                jinxedIDs.Add("Mutant_WING");
                jinxedIDs.Add("Pirate_POW");
                jinxedIDs.Add("Psychopath_POW");
                jinxedIDs.Add("Renegade_WING");
                jinxedIDs.Add("Switchblade_WING");
                jinxedIDs.Add("Tergiversator_WING");
                jinxedIDs.Add("Veteran_POW");
                jinxedIDs.Add("Wretch_80988916");

                jinxedIDs.Add("Jinx_POW"); // Ambusher
                jinxedIDs.Add("Balancer_POW");
                jinxedIDs.Add("Baron_04539999");
                jinxedIDs.Add("Mezepheles_09511163");
                jinxedIDs.Add("Cryptid_WING");
                jinxedIDs.Add("Grenadier_POW");
                jinxedIDs.Add("Ritualist_WING");
                jinxedIDs.Add("Saboteur_WING");
                jinxedIDs.Add("Slinger_POW");
                jinxedIDs.Add("Snake Charmer_WING");
                jinxedIDs.Add("Swarm_Good_WING");
                jinxedIDs.Add("Undying_WING");
            }





            if (jinxedIDs.Count == 0) return;

            Il2CppSystem.Collections.Generic.List<CharacterData> underlingDatas = GetUnderlingDatas(charRef);
            if (!bluffs)
            {
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (jinxedIDs.Contains(character.dataRef.characterId))
                    {
                        int underlingID = 0;
                        if (character.dataRef.type == ECharacterType.Outcast) underlingID = 1;
                        if (character.dataRef.type == ECharacterType.Minion) underlingID = 2;
                        if (character.dataRef.type == (ECharacterType)40) underlingID = 1; // Replace Neutrals (Power Play) with Outcasts.
                        if (character.dataRef.type == (ECharacterType)50) underlingID = 2; // Replace Weather (Power Play) with Minions.
                        DebugMessage($"{charRef.dataRef.characterName} (#{charRef.id}) found {character.dataRef.characterName} at #{character.id}, replacing with {underlingDatas[underlingID].characterName}");
                        character.Init(underlingDatas[underlingID]);
                    }
                }
            }
            else
            {
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (character.bluff)
                    {
                        if (jinxedIDs.Contains(character.bluff.characterId))
                        {
                            int underlingID = 0;
                            if (character.bluff.type == ECharacterType.Outcast) underlingID = 1;
                            if (character.bluff.type == ECharacterType.Minion) underlingID = 2;
                            if (character.bluff.type == (ECharacterType)40) underlingID = 1; // Replace Neutrals (Power Play) with Outcasts.
                            if (character.bluff.type == (ECharacterType)50) underlingID = 2; // Replace Weather (Power Play) with Minions.
                            DebugMessage($"{charRef.dataRef.characterName} (#{charRef.id}) found bad bluff of {character.dataRef.characterName} at #{character.id}, replacing with {underlingDatas[underlingID].characterName}");
                            character.Init(underlingDatas[underlingID]);
                        }
                    }
                }
            }
        }


        public string GetVisionaryFlavour() // Just a fun little detail, Visionary's flavour is different every time you open the game.
        {
            var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
            if (loadedCharList != null)
            {
                allDatas = new CharacterData[loadedCharList.Length];
                for (int j = 0; j < loadedCharList.Length; j++)
                {
                    allDatas[j] = loadedCharList[j]!.Cast<CharacterData>();
                }
            }

            Il2CppSystem.Collections.Generic.List<CharacterData> goodCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<CharacterData> evilCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            for (int i = 0; i < allDatas.Length; i++)
            {
                if (allDatas[i].type == ECharacterType.Villager && allDatas[i].startingAlignment == EAlignment.Good && allDatas[i].characterId != "Visionary_WING")
                {
                    goodCharacters.Add(allDatas[i]);
                }
                if ((allDatas[i].type == ECharacterType.Minion || allDatas[i].type == ECharacterType.Demon) && allDatas[i].startingAlignment == EAlignment.Evil)
                {
                    evilCharacters.Add(allDatas[i]);
                }
            }
            if (goodCharacters.Count != 0 && evilCharacters.Count != 0)
            {
                CharacterData chosenGoodChar = goodCharacters[UnityEngine.Random.RandomRangeInt(0, goodCharacters.Count)];
                CharacterData chosenEvilChar = evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)];

                string goodPronoun = "its";
                string evilPronoun = "its";
                if (chosenGoodChar.gender == EGender.Male) goodPronoun = "his";
                if (chosenGoodChar.gender == EGender.Female) goodPronoun = "her";
                if (chosenGoodChar.gender == EGender.They) goodPronoun = "their";
                if (chosenEvilChar.gender == EGender.Male) evilPronoun = "his";
                if (chosenEvilChar.gender == EGender.Female) evilPronoun = "her";
                if (chosenEvilChar.gender == EGender.They) evilPronoun = "their";

                string goodCharTheName = CheckIfThe(chosenGoodChar.characterName) + chosenGoodChar.characterName;
                string evilCharTheName = CheckIfThe(chosenEvilChar.characterName) + chosenEvilChar.characterName;

                if (goodPronoun == evilPronoun || evilPronoun == "their" || goodPronoun == "their")
                {
                    return $"\"Last night, in her vision, she saw {goodCharTheName}.\nThen, in the moonlight, {evilPronoun} face shifted into something new:\nThe face of {evilCharTheName}.\"";
                }
                else
                {
                    return $"\"Last night, in her vision, she saw {goodCharTheName}.\nThen, in the moonlight, {goodPronoun}- No, <b>{evilPronoun}</b> face shifted into something new:\nThe face of {evilCharTheName}\"";
                }
            }
            return "\"Last night, she saw the Lamb.\nThen, in the moonlight, his face shifted into something new:\nThe face of Baa.\"";
        }





        public ActedInfo GetRandomInfo(Character charRef, bool lying, bool dizzyAllowed, bool selfAllowed)
        {
            ActedInfo returnInfo = new ActedInfo("");

            /*
              Possible info:
            - Learn that I am...
            -- Good
            -- Dizzy

            - Learn that a character is...
            -- Good
            -- Evil
            -- Corrupted
            -- Truthful
            -- Lying
            -- Honest
            -- a particular Good role

            - Learn that 1 of 2 characters is...
            -- a particular...
            --- Outcast
            --- Minion
            -- Corrupted
            -- Disguising
            -- Lying

            - Learn that 1 of 3 characters is...
            -- Evil

            - Learn that among 2-3 characters...
            -- ...there are a certain number of...
            --- Disguised characters
            --- Lying characters
            --- Evil characters
            --- Villagers
            -- ...there is a particular...
            --- Outcast
            --- Minion

            - Learn how far from...
            -- ...me to...
            --- ...a particular...
            ---- Outcast
            ---- Minion
            --- my closest...
            ---- Corrupted character
            ---- Evil character

            - Learn how many characters adjacent to...
            -- ...me are...
            --- Evil
            --- Lying
            --- Disguising

            - Learn how many pairs of characters that are...
            -- Evil
            -- Good
            -- Villagers


            */

            Il2CppSystem.Collections.Generic.List<Character> allCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> goodCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> evilCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> villagerCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> outcastCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> minionCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> demonCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> nonVillagerCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> nonOutcastCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> nonMinionCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> nonDemonCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> lyingCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> truthfulCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> honestCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> disguisedCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> corruptedCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> pureCharacters = new Il2CppSystem.Collections.Generic.List<Character>();


            string finalInfo = "";
            Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();

            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character != charRef || selfAllowed == true)
                {
                    allCharacters.Add(character);
                    if (character.GetRegisterAlignment() == EAlignment.Good) goodCharacters.Add(character);
                    else evilCharacters.Add(character);
                    if (character.GetCharacterType() == ECharacterType.Villager) villagerCharacters.Add(character); else nonVillagerCharacters.Add(character);
                    if (character.GetCharacterType() == ECharacterType.Outcast) outcastCharacters.Add(character); else nonOutcastCharacters.Add(character);
                    if (character.GetCharacterType() == ECharacterType.Minion) minionCharacters.Add(character); else nonMinionCharacters.Add(character);
                    if (character.GetCharacterType() == ECharacterType.Demon) demonCharacters.Add(character); else nonDemonCharacters.Add(character);
                    if (CharacterHelper.CheckLyingAppearance(character)) lyingCharacters.Add(character);
                    else truthfulCharacters.Add(character);
                    if (CharacterHelper.CheckIfDisguisedAppearance(character)) disguisedCharacters.Add(character);
                    else honestCharacters.Add(character);
                    if (character.statuses.Contains(ECharacterStatus.Corrupted)) corruptedCharacters.Add(character);
                    else pureCharacters.Add(character);
                }
            }

            Il2CppSystem.Collections.Generic.List<string> infoTypes = new Il2CppSystem.Collections.Generic.List<string>();

            // Confessor
            if (dizzyAllowed) infoTypes.Add("Confessor"); // I am Good // I am dizzy

            // Learn that a character is...
            infoTypes.Add("AlignmentCop"); // Alignment cop
            infoTypes.Add("Judge"); // Lying cop
            infoTypes.Add("Arbiter"); // Disguising cop
            infoTypes.Add("Forager"); // Villager Cop
            // infoTypes.Add("Cartomancer"); // Learns an in-play character and an out-of-play character, but not which is which.
            // infoTypes.Add("Duchess"); // Learns 4 characters, among which are exactly 3 Types.
            infoTypes.Add("Scout"); // Learns distance from an Evil character to its closest Evil.
            if (goodCharacters.Count != 0 && evilCharacters.Count != 0)
            {
                infoTypes.Add("Medium"); // #X is a Good Y
                infoTypes.Add("GoodChainLong"); // The longest chain of Good characters is X cards long
                infoTypes.Add("GoodChainShort"); // The shortest chain of Good characters is X cards long
            }
            if (evilCharacters.Count > 1)
            {
                infoTypes.Add("EvilChainLong"); // The longest chain of Evil characters is X cards long
                infoTypes.Add("EvilDistLong"); // The longest distance between two Evil characters is X cards
                infoTypes.Add("EvilDistShort"); // The shortest distance between two Evil characters is X cards
                infoTypes.Add("Visionary"); // Learns that a particular character is 1 of 2 roles.
            }
            // if (corruptedCharacters.Count != 0 && pureCharacters.Count != 0) infoTypes.Add("CorruptionChecker"); // #X is Corrupted

            // Learn that 1 of 2 characters is...
            // if (outcastCharacters.Count != 0 && nonOutcastCharacters.Count != 0) infoTypes.Add("Librarian"); // a particular Outcast // this was bugged and I couldn't be arsed to fix it
            if (minionCharacters.Count != 0 && nonMinionCharacters.Count != 0) infoTypes.Add("Oracle"); // a particular Minion
            if (corruptedCharacters.Count != 0 && pureCharacters.Count != 0) infoTypes.Add("Sentinel"); // Corrupted
            if (disguisedCharacters.Count != 0 && honestCharacters.Count != 0) infoTypes.Add("Prince"); // Disguised
            if (lyingCharacters.Count != 0 && truthfulCharacters.Count != 0) infoTypes.Add("Detective"); // Lying

            // Learn that 1 of 3 characters is...
            if (goodCharacters.Count > 2 && evilCharacters.Count > 1) infoTypes.Add("Empress"); // Evil

            // Learn that among 2-3 characters...
            if (disguisedCharacters.Count != 0 && honestCharacters.Count != 0) infoTypes.Add("PrinceCount"); // Learn how many are Disguised
            if (lyingCharacters.Count != 0 && truthfulCharacters.Count != 0) infoTypes.Add("DetectiveCount"); // Learn how many are Lying
            if (evilCharacters.Count != 0 && goodCharacters.Count != 0) infoTypes.Add("Jester"); // Learn how many are Evil
            if (villagerCharacters.Count != 0 && nonVillagerCharacters.Count != 0) infoTypes.Add("ForagerCount"); // Learn how many are Villagers

            // Learn how far from me to...
            if (evilCharacters.Count != 0)
            {
                infoTypes.Add("Hunter"); // my nearest Evil
                infoTypes.Add("Ranger"); // my furthest Evil
                infoTypes.Add("EvilLamb"); // a particular Evil
            }
            // if (outcastCharacters.Count != 0) infoTypes.Add("Lamb");
            if (corruptedCharacters.Count != 0) infoTypes.Add("Bard");


            // Learn how many characters adjacent to me are...
            if (evilCharacters.Count != 0) infoTypes.Add("Lover"); // Evil
            if (lyingCharacters.Count != 0) infoTypes.Add("LoverDetective"); // Lying
            if (disguisedCharacters.Count != 0) infoTypes.Add("LoverPrince"); // Disguising

            // Learn how many characters within 2 cards are...
            if (evilCharacters.Count != 0) infoTypes.Add("Sapper"); // Evil

            // Learn how many pairs are...
            if (evilCharacters.Count != 0 && goodCharacters.Count != 0)
            {
                infoTypes.Add("Knitter"); // Evil
                infoTypes.Add("GoodKnitter"); // Good
                infoTypes.Add("VillagerKnitter"); // Villagers
            }

            // Learn the...
            if (evilCharacters.Count != 0 && goodCharacters.Count != 0)
            {
                infoTypes.Add("Arithmetician"); // sum of all Evil
                if (UnityEngine.Random.RandomRangeInt(0, 2) == 0) infoTypes.Add("GoodArithmetician"); // sum of all Good. 50% chance to be possible to begin with.
                if (UnityEngine.Random.RandomRangeInt(0, 3) == 0) // 1 in 3 chance of being possible, since this is a weird info type
                {
                    // infoTypes.Add("ArithmeticianMultiply"); // product of all Evil
                    // infoTypes.Add("GoodArithmeticianMultiply"); // product of all Good
                }
            }

            // Total:
            // Confessor
            // AlignmentCop
            // Judge
            // Arbiter
            // Forager
            // Medium
            // CorruptionChecker
            // Librarian
            // Oracle
            // Sentinel
            // Prince
            // Detective
            // Empress
            // PrinceCount
            // DetectiveCount
            // Jester
            // ForagerCount
            // Hunter
            // Ranger
            // EvilLamb
            // Lamb
            // Bard
            // Lover
            // LoverDetective
            // LoverPrince
            // Sapper
            // Knitter
            // GoodKnitter
            // VillagerKnitter
            // Arithmetician
            // GoodArithmetician
            // ArithmeticianMultiply
            // GoodArithmeticianMultiply
            // LieCount
            // BluffCount



            // Let's do this.


            string chosenInfoType = infoTypes[UnityEngine.Random.RandomRangeInt(0, infoTypes.Count)];
            finalInfo = $"Info of type {chosenInfoType} is bugged";
            DebugMessage($"Chose info type of {chosenInfoType}. Lying?: {lying}");
            // Confessor
            if (chosenInfoType == "Confessor")
            {
                selection.Add(charRef);
                if (charRef.GetRegisterAlignment() == EAlignment.Evil || CharacterHelper.CheckLying(charRef) || lying)
                {
                    finalInfo = "I am dizzy";
                }
                else
                {
                    finalInfo = "I am Good";
                }
            }

            // AlignmentCop
            if (chosenInfoType == "AlignmentCop")
            {
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                finalInfo = $"#{selection[0].id} is ";
                if (selection[0].GetRegisterAlignment() == EAlignment.Good)
                {
                    if (lying) finalInfo += "Evil";
                    else finalInfo += "Good";
                }
                else
                {
                    if (lying) finalInfo += "Good";
                    else finalInfo += "Evil";
                }
            }

            // Judge
            if (chosenInfoType == "Judge")
            {
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                finalInfo = $"#{selection[0].id} is ";
                if (lyingCharacters.Contains(selection[0]))
                {
                    if (lying) finalInfo += "Truthful";
                    else finalInfo += "Lying";
                }
                else
                {
                    if (lying) finalInfo += "Lying";
                    else finalInfo += "Truthful";
                }
            }


            // Arbiter
            if (chosenInfoType == "Arbiter")
            {
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                finalInfo = $"#{selection[0].id} is ";
                if (disguisedCharacters.Contains(selection[0]))
                {
                    if (lying) finalInfo += "Honest";
                    else finalInfo += "Disguised";
                }
                else
                {
                    if (lying) finalInfo += "Disguised";
                    else finalInfo += "Honest";
                }
            }


            // Forager
            if (chosenInfoType == "Forager")
            {
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                finalInfo = $"#{selection[0].id} is ";
                if (villagerCharacters.Contains(selection[0]))
                {
                    if (lying) finalInfo += "not a Villager";
                    else finalInfo += "a Villager";
                }
                else
                {
                    if (lying) finalInfo += "a Villager";
                    else finalInfo += "not a Villager";
                }
            }


            // Medium
            if (chosenInfoType == "Medium")
            {
                string claimedRole = "";
                if (!lying)
                {
                    selection.Add(goodCharacters[UnityEngine.Random.RandomRangeInt(0, goodCharacters.Count)]);
                    claimedRole = selection[0].GetRegisterAs().characterName;
                }
                else
                {
                    selection.Add(evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)]);
                    if (selection[0].bluff) claimedRole = selection[0].bluff.characterName;
                    else claimedRole = selection[0].GetRegisterAs().characterName;
                }
                finalInfo = $"#{selection[0].id} is a Good {claimedRole}";
            }


            // CorruptionChecker
            if (chosenInfoType == "CorruptionChecker")
            {
                string claimedCorruption = "Pure";
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                if (lying == pureCharacters.Contains(selection[0]))
                {
                    claimedCorruption = "Corrupted";
                }
                finalInfo = $"#{selection[0].id} is {claimedCorruption}";
            }


            // Librarian
            if (chosenInfoType == "Librarian")
            {
                string claimedRole = "";
                if (lying)
                {
                    // Try to find a character Disguised as an Outcast
                    Il2CppSystem.Collections.Generic.List<Character> charactersDisguisedAsOutcasts = new Il2CppSystem.Collections.Generic.List<Character>();
                    foreach (Character character in allCharacters)
                    {
                        if (character.bluff)
                        {
                            if (character.bluff.type == ECharacterType.Outcast)
                            {
                                charactersDisguisedAsOutcasts.Add(character);
                            }
                        }
                    }
                    if (charactersDisguisedAsOutcasts.Count != 0)
                    {
                        selection.Add(charactersDisguisedAsOutcasts[UnityEngine.Random.RandomRangeInt(0, charactersDisguisedAsOutcasts.Count)]);
                        claimedRole = selection[0].bluff.characterName;
                    }
                    else
                    {
                        Il2CppSystem.Collections.Generic.List<CharacterData> deckOutcasts = new Il2CppSystem.Collections.Generic.List<CharacterData>();
                        Il2CppSystem.Collections.Generic.List<CharacterData> disguisedOutcasts = new Il2CppSystem.Collections.Generic.List<CharacterData>();
                        foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Outcast))
                        {
                            if (character.usuallyDisguised)
                            {
                                disguisedOutcasts.Add(character);
                            }
                        }
                        if (disguisedOutcasts.Count != 0)
                        {
                            claimedRole = disguisedOutcasts[UnityEngine.Random.RandomRangeInt(0, disguisedOutcasts.Count)].characterName;
                        }
                        else
                        {
                            claimedRole = deckOutcasts[UnityEngine.Random.RandomRangeInt(0, deckOutcasts.Count)].characterName;
                        }
                    }
                    foreach (Character character in Gameplay.CurrentCharacters)
                    {
                        if (character.GetRegisterAs().characterName == claimedRole)
                        {
                            allCharacters.Remove(character);
                        }
                    }
                    selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                }
                else
                {
                    selection.Add(outcastCharacters[UnityEngine.Random.RandomRangeInt(0, outcastCharacters.Count)]);
                    claimedRole = selection[0].GetRegisterAs().characterName;
                    allCharacters.Remove(selection[0]);
                    selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                }
                selection = SortList(selection);
                string the = CheckIfThe(claimedRole);
                finalInfo = $"#{selection[0].id} or #{selection[1].id} is {the}{claimedRole}";
            }

            // Oracle
            if (chosenInfoType == "Oracle")
            {
                string claimedRole = "";
                if (lying)
                {
                    bool cryptidPresent = false;
                    Il2CppSystem.Collections.Generic.List<CharacterData> deckMinions = new Il2CppSystem.Collections.Generic.List<CharacterData>();
                    foreach (Character character in Gameplay.CurrentCharacters)
                    {
                        if (character.dataRef.characterId == "Cryptid_WING")
                        {
                            cryptidPresent = true;
                        }
                    }
                    foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Minion))
                    {
                        deckMinions.Add(character);
                    }
                    if (cryptidPresent)
                    {
                        foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
                        {
                            if (GetPossibleCharacterIDsOfRole("Cryptid_WING").Contains(character.characterId))
                            {
                                deckMinions.Add(character);
                            }
                        }    
                    }
                    foreach (Character character in minionCharacters)
                    {
                        deckMinions.Add(character.GetRegisterAs());
                    }
                    claimedRole = deckMinions[UnityEngine.Random.RandomRangeInt(0, deckMinions.Count)].characterName;
                    foreach (Character character in Gameplay.CurrentCharacters)
                    {
                        if (character.GetRegisterAs().characterName == claimedRole)
                        {
                            allCharacters.Remove(character);
                        }
                    }
                    selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                    allCharacters.Remove(selection[0]);
                    selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                }
                else
                {
                    selection.Add(minionCharacters[UnityEngine.Random.RandomRangeInt(0, minionCharacters.Count)]);
                    claimedRole = selection[0].GetRegisterAs().characterName;
                    foreach (Character character in Gameplay.CurrentCharacters)
                    {
                        if (character.GetRegisterAs().characterName == claimedRole)
                        {
                            allCharacters.Remove(character);
                        }
                    }
                    selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                }
                selection = SortList(selection);
                string the = CheckIfThe(claimedRole);
                finalInfo = $"#{selection[0].id} or #{selection[1].id} is {the}{claimedRole}";
            }


            // Sentinel
            if (chosenInfoType == "Sentinel")
            {
                if (lying)
                {
                    bool canDoPure = true;
                    bool canDoCorrupt = true;
                    if (corruptedCharacters.Count < 2) canDoCorrupt = false;
                    if (pureCharacters.Count < 2) canDoPure = false;
                    if (canDoPure && canDoCorrupt)
                    {
                        if (UnityEngine.Random.RandomRangeInt(0, 2) == 0) canDoPure = false;
                        else canDoCorrupt = false;
                    }
                    if (canDoPure)
                    {
                        selection.Add(pureCharacters[UnityEngine.Random.RandomRangeInt(0, pureCharacters.Count)]);
                        pureCharacters.Remove(selection[0]);
                        selection.Add(pureCharacters[UnityEngine.Random.RandomRangeInt(0, pureCharacters.Count)]);
                    }
                    else
                    {
                        selection.Add(corruptedCharacters[UnityEngine.Random.RandomRangeInt(0, corruptedCharacters.Count)]);
                        corruptedCharacters.Remove(selection[0]);
                        selection.Add(corruptedCharacters[UnityEngine.Random.RandomRangeInt(0, corruptedCharacters.Count)]);
                    }
                }
                else
                {
                    selection.Add(corruptedCharacters[UnityEngine.Random.RandomRangeInt(0, corruptedCharacters.Count)]);
                    selection.Add(pureCharacters[UnityEngine.Random.RandomRangeInt(0, pureCharacters.Count)]);
                }
                selection = SortList(selection);
                finalInfo = $"One is Corrupted:\n#{selection[0].id}, #{selection[1].id}";
            }

            // Prince
            if (chosenInfoType == "Prince")
            {
                if (lying)
                {
                    bool canDoHonest = true;
                    bool canDoDisguised = true;
                    if (honestCharacters.Count < 2) canDoHonest = false;
                    if (disguisedCharacters.Count < 2) canDoDisguised = false;
                    if (canDoHonest && canDoDisguised)
                    {
                        if (UnityEngine.Random.RandomRangeInt(0, 2) == 0) canDoHonest = false;
                        else canDoDisguised = false;
                    }
                    if (canDoHonest)
                    {
                        selection.Add(honestCharacters[UnityEngine.Random.RandomRangeInt(0, honestCharacters.Count)]);
                        honestCharacters.Remove(selection[0]);
                        selection.Add(honestCharacters[UnityEngine.Random.RandomRangeInt(0, honestCharacters.Count)]);
                    }
                    else
                    {
                        selection.Add(disguisedCharacters[UnityEngine.Random.RandomRangeInt(0, disguisedCharacters.Count)]);
                        disguisedCharacters.Remove(selection[0]);
                        selection.Add(disguisedCharacters[UnityEngine.Random.RandomRangeInt(0, disguisedCharacters.Count)]);
                    }
                }
                else
                {
                    selection.Add(disguisedCharacters[UnityEngine.Random.RandomRangeInt(0, disguisedCharacters.Count)]);
                    selection.Add(honestCharacters[UnityEngine.Random.RandomRangeInt(0, honestCharacters.Count)]);
                }
                selection = SortList(selection);
                finalInfo = $"One is Disguised:\n#{selection[0].id}, #{selection[1].id}";
            }


            // Detective
            if (chosenInfoType == "Detective")
            {
                if (lying)
                {
                    bool canDoTruthful = true;
                    bool canDoLying = true;
                    if (honestCharacters.Count < 2) canDoTruthful = false;
                    if (disguisedCharacters.Count < 2) canDoLying = false;
                    if (canDoTruthful && canDoLying)
                    {
                        if (UnityEngine.Random.RandomRangeInt(0, 2) == 0) canDoTruthful = false;
                        else canDoLying = false;
                    }
                    if (canDoTruthful)
                    {
                        selection.Add(truthfulCharacters[UnityEngine.Random.RandomRangeInt(0, truthfulCharacters.Count)]);
                        truthfulCharacters.Remove(selection[0]);
                        selection.Add(truthfulCharacters[UnityEngine.Random.RandomRangeInt(0, truthfulCharacters.Count)]);
                    }
                    else
                    {
                        selection.Add(lyingCharacters[UnityEngine.Random.RandomRangeInt(0, lyingCharacters.Count)]);
                        lyingCharacters.Remove(selection[0]);
                        selection.Add(lyingCharacters[UnityEngine.Random.RandomRangeInt(0, lyingCharacters.Count)]);
                    }
                }
                else
                {
                    selection.Add(truthfulCharacters[UnityEngine.Random.RandomRangeInt(0, truthfulCharacters.Count)]);
                    selection.Add(lyingCharacters[UnityEngine.Random.RandomRangeInt(0, lyingCharacters.Count)]);
                }
                selection = SortList(selection);
                finalInfo = $"One is Lying:\n#{selection[0].id}, #{selection[1].id}";
            }


            // Empress
            if (chosenInfoType == "Empress")
            {
                if (lying)
                {
                    selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                    allCharacters.Remove(selection[0]);
                    goodCharacters.Remove(selection[0]);
                    evilCharacters.Remove(selection[0]);
                    selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                    allCharacters.Remove(selection[1]);
                    goodCharacters.Remove(selection[1]);
                    evilCharacters.Remove(selection[1]);
                    selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                    allCharacters.Remove(selection[2]);
                    goodCharacters.Remove(selection[2]);
                    evilCharacters.Remove(selection[2]);
                    int chosenEvils = 0;
                    foreach (Character character in selection)
                    {
                        if (character.GetRegisterAlignment() == EAlignment.Evil) chosenEvils++;
                    }
                    if (chosenEvils == 1)
                    {
                        if (selection[0].GetRegisterAlignment() == EAlignment.Good)
                        {
                            selection.Add(evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)]);
                            selection.Remove(selection[0]);
                        }
                        else
                        {
                            selection.Add(goodCharacters[UnityEngine.Random.RandomRangeInt(0, goodCharacters.Count)]);
                            selection.Remove(selection[0]);
                        }
                    }
                }
                else
                {
                    selection.Add(goodCharacters[UnityEngine.Random.RandomRangeInt(0, goodCharacters.Count)]);
                    goodCharacters.Remove(selection[0]);
                    selection.Add(goodCharacters[UnityEngine.Random.RandomRangeInt(0, goodCharacters.Count)]);
                    selection.Add(evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)]);
                }
                selection = SortList(selection);
                finalInfo = $"One is Evil:\n#{selection[0].id}, #{selection[1].id}, #{selection[2].id}";
            }


            // PrinceCount
            if (chosenInfoType == "PrinceCount")
            {
                int chosenChars = UnityEngine.Random.RandomRangeInt(2, 4); // Picks 2 or 3 characters
                int chosenNumber = 0;
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                allCharacters.Remove(selection[0]);
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                allCharacters.Remove(selection[1]);
                if (chosenChars == 3) selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);

                if (lying)
                {
                    int trueNumber = 0;
                    int falseNumber = 0;
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(evilCharacters);
                    foreach (Character character in selection)
                    {
                        if (disguisedCharacters.Contains(character)) trueNumber++;
                        if (fakeChars.Contains(character)) falseNumber++;
                    }
                    falseNumber = MakeNumberWrong(trueNumber, falseNumber, 0);
                    chosenNumber = falseNumber;
                }
                else
                {
                    foreach (Character character in selection)
                    {
                        if (disguisedCharacters.Contains(character)) chosenNumber++;
                    }
                }
                selection = SortList(selection);
                string isAre = "are";
                string charPlural = "characters";
                if (chosenNumber == 1)
                {
                    isAre = "is";
                    charPlural = "character";
                }

                if (chosenChars == 3) finalInfo = $"Among #{selection[0].id}, #{selection[1].id}, #{selection[2].id}, there {isAre}:\n{chosenNumber} Disguised {charPlural}";
                else finalInfo = $"Among #{selection[0].id}, #{selection[1].id}, there {isAre}:\n{chosenNumber} Disguised {charPlural}";
            }


            // DetectiveCount
            if (chosenInfoType == "DetectiveCount")
            {
                int chosenChars = UnityEngine.Random.RandomRangeInt(2, 4); // Picks 2 or 3 characters
                int chosenNumber = 0;
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                allCharacters.Remove(selection[0]);
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                allCharacters.Remove(selection[1]);
                if (chosenChars == 3) selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);

                if (lying)
                {
                    int trueNumber = 0;
                    int falseNumber = 0;
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(lyingCharacters);
                    foreach (Character character in selection)
                    {
                        if (lyingCharacters.Contains(character)) trueNumber++;
                        if (fakeChars.Contains(character)) falseNumber++;
                    }
                    falseNumber = MakeNumberWrong(trueNumber, falseNumber, 0);
                    chosenNumber = falseNumber;
                }
                else
                {
                    foreach (Character character in selection)
                    {
                        if (lyingCharacters.Contains(character)) chosenNumber++;
                    }
                }
                selection = SortList(selection);
                string isAre = "are";
                string charPlural = "characters";
                if (chosenNumber == 1)
                {
                    isAre = "is";
                    charPlural = "character";
                }

                if (chosenChars == 3) finalInfo = $"Among #{selection[0].id}, #{selection[1].id}, #{selection[2].id}, there {isAre}:\n{chosenNumber} Lying {charPlural}";
                else finalInfo = $"Among #{selection[0].id}, #{selection[1].id}, there {isAre}:\n{chosenNumber} Lying {charPlural}";
            }


            // Jester
            if (chosenInfoType == "Jester")
            {
                int chosenChars = UnityEngine.Random.RandomRangeInt(2, 4); // Picks 2 or 3 characters
                int chosenNumber = 0;
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                allCharacters.Remove(selection[0]);
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                allCharacters.Remove(selection[1]);
                if (chosenChars == 3) selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);

                if (lying)
                {
                    int trueNumber = 0;
                    int falseNumber = 0;
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(evilCharacters);
                    foreach (Character character in selection)
                    {
                        if (evilCharacters.Contains(character)) trueNumber++;
                        if (fakeChars.Contains(character)) falseNumber++;
                    }
                    falseNumber = MakeNumberWrong(trueNumber, falseNumber, 0);
                    chosenNumber = falseNumber;
                }
                else
                {
                    foreach (Character character in selection)
                    {
                        if (evilCharacters.Contains(character)) chosenNumber++;
                    }
                }
                selection = SortList(selection);
                string isAre = "are";
                string charPlural = "characters";
                if (chosenNumber == 1)
                {
                    isAre = "is";
                    charPlural = "character";
                }

                if (chosenChars == 3) finalInfo = $"Among #{selection[0].id}, #{selection[1].id}, #{selection[2].id}, there {isAre}:\n{chosenNumber} Evil {charPlural}";
                else finalInfo = $"Among #{selection[0].id}, #{selection[1].id}, there {isAre}:\n{chosenNumber} Evil {charPlural}";
            }


            // ForagerCount
            if (chosenInfoType == "ForagerCount")
            {
                int chosenChars = UnityEngine.Random.RandomRangeInt(2, 4); // Picks 2 or 3 characters
                int chosenNumber = 0;
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                allCharacters.Remove(selection[0]);
                selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                allCharacters.Remove(selection[1]);
                if (chosenChars == 3) selection.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);

                if (lying)
                {
                    int trueNumber = 0;
                    int falseNumber = 0;
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(villagerCharacters);
                    foreach (Character character in selection)
                    {
                        if (villagerCharacters.Contains(character)) trueNumber++;
                        if (fakeChars.Contains(character)) falseNumber++;
                    }
                    falseNumber = MakeNumberWrong(trueNumber, falseNumber, 0);
                    chosenNumber = falseNumber;
                }
                else
                {
                    foreach (Character character in selection)
                    {
                        if (villagerCharacters.Contains(character)) chosenNumber++;
                    }
                }
                selection = SortList(selection);
                string isAre = "are";
                string charPlural = "Villagers";
                if (chosenNumber == 1)
                {
                    isAre = "is";
                    charPlural = "Villager";
                }

                if (chosenChars == 3) finalInfo = $"Among #{selection[0].id}, #{selection[1].id}, #{selection[2].id}, there {isAre}:\n{chosenNumber} {charPlural}";
                else finalInfo = $"Among #{selection[0].id}, #{selection[1].id}, there {isAre}:\n{chosenNumber} {charPlural}";
            }


            // Ranger
            if (chosenInfoType == "Ranger")
            {
                int chosenDist = 0;

                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(evilCharacters);
                    if (fakeChars.Count == 1)
                    {
                        if (fakeChars[0].id == charRef.id)
                        {
                            fakeChars.Clear();
                            fakeChars.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                        }
                    }
                    int falseDist = GetFurthestDistance(fakeChars, charRef);
                    int trueDist = GetFurthestDistance(evilCharacters, charRef);
                    chosenDist = MakeNumberWrong(trueDist, falseDist, 1);
                }
                else
                {
                    chosenDist = GetFurthestDistance(evilCharacters, charRef);
                }
                selection = Characters.Instance.GetCharactersAtRange(chosenDist, charRef);
                string cardPlural = "cards";
                if (chosenDist == 1)
                {
                    cardPlural = "card";
                }

                finalInfo = $"I am {chosenDist} {cardPlural} away from my furthest Evil";
            }


            // EvilLamb
            if (chosenInfoType == "EvilLamb")
            {
                int chosenDist = 0;
                string chosenRole = "";

                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<CharacterData> possibleRoles = new Il2CppSystem.Collections.Generic.List<CharacterData>();
                    foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Evil))
                    {
                        possibleRoles.Add(character);
                    }
                    foreach (Character character in evilCharacters)
                    {
                        possibleRoles.Add(character.GetRegisterAs());
                    }
                    chosenRole = possibleRoles[UnityEngine.Random.RandomRangeInt(0, possibleRoles.Count)].characterName;
                    Il2CppSystem.Collections.Generic.List<Character> removeCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
                    Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
                    foreach (Character character in allCharacters)
                    {
                        possibleTargets.Add(character);
                    }
                    foreach (Character character in Gameplay.CurrentCharacters)
                    {
                        if (character.GetRegisterAs().characterName == chosenRole)
                        {
                            foreach (Character rangeChars in Characters.Instance.GetCharactersAtRange(GetDistanceBetweenCharacters(charRef, character), charRef))
                            {
                                removeCharacters.Add(rangeChars);
                            }
                        }
                    }
                    foreach (Character character in removeCharacters)
                    {
                        possibleTargets.Remove(character);
                    }
                    possibleTargets.Remove(charRef);
                    if (possibleTargets.Count == 0)
                    {
                        MelonLogger.Msg("Nevermind, I'm freaking out a little here, let's go with Hunter info instead");
                        chosenInfoType = "Hunter";
                    }
                    else
                    {
                        Character chosenTarget = possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)];
                        chosenDist = GetDistanceBetweenCharacters(charRef, chosenTarget);
                    }
                }
                else
                {
                    Character chosenTarget = evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)];
                    chosenRole = chosenTarget.GetRegisterAs().characterName;
                    chosenDist = GetDistanceBetweenCharacters(charRef, chosenTarget);
                }
                selection = Characters.Instance.GetCharactersAtRange(chosenDist, charRef);
                string cardPlural = "cards";
                if (chosenDist == 1)
                {
                    cardPlural = "card";
                }

                finalInfo = $"I am {chosenDist} {cardPlural} away from {CheckIfThe(chosenRole)}{chosenRole}";
            }


            // Hunter
            if (chosenInfoType == "Hunter")
            {
                int chosenDist = 0;

                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(evilCharacters);
                    if (fakeChars.Count == 1)
                    {
                        if (fakeChars[0].id == charRef.id)
                        {
                            fakeChars.Clear();
                            fakeChars.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                        }
                    }
                    int falseDist = GetClosestDistance(fakeChars, charRef);
                    int trueDist = GetClosestDistance(evilCharacters, charRef);
                    chosenDist = MakeNumberWrong(trueDist, falseDist, 1);
                }
                else
                {
                    chosenDist = GetClosestDistance(evilCharacters, charRef);
                }
                selection = Characters.Instance.GetCharactersAtRange(chosenDist, charRef);
                string cardPlural = "cards";
                if (chosenDist == 1)
                {
                    cardPlural = "card";
                }

                finalInfo = $"I am {chosenDist} {cardPlural} away from my closest Evil";
            }


            // Bard
            if (chosenInfoType == "Bard")
            {
                int chosenDist = 0;

                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(corruptedCharacters);
                    if (fakeChars.Count == 1)
                    {
                        if (fakeChars[0].id == charRef.id)
                        {
                            fakeChars.Clear();
                            fakeChars.Add(allCharacters[UnityEngine.Random.RandomRangeInt(0, allCharacters.Count)]);
                        }
                    }
                    int falseDist = GetClosestDistance(fakeChars, charRef);
                    int trueDist = GetClosestDistance(corruptedCharacters, charRef);
                    chosenDist = MakeNumberWrong(trueDist, falseDist, 1);
                }
                else
                {
                    chosenDist = GetClosestDistance(corruptedCharacters, charRef);
                }
                selection = Characters.Instance.GetCharactersAtRange(chosenDist, charRef);
                string cardPlural = "cards";
                if (chosenDist == 1)
                {
                    cardPlural = "card";
                }

                finalInfo = $"I am {chosenDist} {cardPlural} away from my closest Corruption";
            }


            // Lover
            if (chosenInfoType == "Lover")
            {
                selection = GetCharacterNeighbours(charRef);
                int chosenNum = 0;

                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(evilCharacters);
                    int fakeNum = 0;
                    int realNum = 0;
                    foreach (Character character in selection)
                    {
                        if (evilCharacters.Contains(character)) realNum++;
                        if (fakeChars.Contains(character)) fakeNum++;
                    }
                    chosenNum = MakeNumberWrong(realNum, fakeNum, 0);
                }
                else
                {
                    foreach (Character character in selection)
                    {
                        if (evilCharacters.Contains(character)) chosenNum++;
                    }
                }
                string number = chosenNum.ToString();
                string cardPlural = "Evils";
                if (chosenNum == 1)
                {
                    cardPlural = "Evil";
                }
                if (chosenNum == 0)
                {
                    number = "NO";
                }

                finalInfo = $"{number} {cardPlural} adjacent to me";
            }


            // LoverDetective
            if (chosenInfoType == "LoverDetective")
            {
                selection = GetCharacterNeighbours(charRef);
                int chosenNum = 0;

                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(lyingCharacters);
                    int fakeNum = 0;
                    int realNum = 0;
                    foreach (Character character in selection)
                    {
                        if (lyingCharacters.Contains(character)) realNum++;
                        if (fakeChars.Contains(character)) fakeNum++;
                    }
                    chosenNum = MakeNumberWrong(realNum, fakeNum, 0);
                }
                else
                {
                    foreach (Character character in selection)
                    {
                        if (lyingCharacters.Contains(character)) chosenNum++;
                    }
                }
                string number = chosenNum.ToString();
                string cardPlural = "Liars";
                if (chosenNum == 1)
                {
                    cardPlural = "Liar";
                }
                if (chosenNum == 0)
                {
                    number = "NO";
                }

                finalInfo = $"{number} {cardPlural} adjacent to me";
            }


            // LoverPrince
            if (chosenInfoType == "LoverPrince")
            {
                selection = GetCharacterNeighbours(charRef);
                int chosenNum = 0;

                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(disguisedCharacters);
                    int fakeNum = 0;
                    int realNum = 0;
                    foreach (Character character in selection)
                    {
                        if (disguisedCharacters.Contains(character)) realNum++;
                        if (fakeChars.Contains(character)) fakeNum++;
                    }
                    chosenNum = MakeNumberWrong(realNum, fakeNum, 0);
                }
                else
                {
                    foreach (Character character in selection)
                    {
                        if (disguisedCharacters.Contains(character)) chosenNum++;
                    }
                }
                string number = chosenNum.ToString();
                string cardPlural = "Disguised characters";
                if (chosenNum == 1)
                {
                    cardPlural = "Disguised character";
                }
                if (chosenNum == 0)
                {
                    number = "NO";
                }

                finalInfo = $"I am adjacent to {number} {cardPlural}";
            }


            // Sapper
            if (chosenInfoType == "Sapper")
            {
                selection = GetCharactersWithinRange(charRef, 2);
                int chosenNum = 0;

                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(evilCharacters);
                    int fakeNum = 0;
                    int realNum = 0;
                    foreach (Character character in selection)
                    {
                        if (evilCharacters.Contains(character)) realNum++;
                        if (fakeChars.Contains(character)) fakeNum++;
                    }
                    chosenNum = MakeNumberWrong(realNum, fakeNum, 0);
                }
                else
                {
                    foreach (Character character in selection)
                    {
                        if (evilCharacters.Contains(character)) chosenNum++;
                    }
                }
                string number = chosenNum.ToString();
                string cardPlural = "Evils";
                string isAre = "are";
                if (chosenNum == 1)
                {
                    isAre = "is";
                    cardPlural = "Evil";
                }
                if (chosenNum == 0)
                {
                    number = "NO";
                }

                finalInfo = $"There {isAre} {number} {cardPlural} within 2 cards of me";
            }


            // Knitter
            if (chosenInfoType == "Knitter")
            {
                if (charRef.GetRegisterAlignment() == EAlignment.Evil && !evilCharacters.Contains(charRef)) evilCharacters.Add(charRef);
                int chosenNum = 0;
                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(evilCharacters);
                    int fakeNum = GetPairsOfCharactersInList(fakeChars);
                    int realNum = GetPairsOfCharactersInList(evilCharacters);
                    chosenNum = MakeNumberWrong(realNum, fakeNum, 0);
                }
                else
                {
                    chosenNum = GetPairsOfCharactersInList(evilCharacters);
                }
                string number = chosenNum.ToString();
                string cardPlural = "pairs";
                string isAre = "are";
                if (chosenNum == 1)
                {
                    cardPlural = "pair";
                    isAre = "is only";
                }
                if (chosenNum == 0)
                {
                    number = "NO";
                }

                finalInfo = $"There {isAre} {number} {cardPlural} of Evil characters";
            }


            // GoodKnitter
            if (chosenInfoType == "GoodKnitter")
            {
                if (charRef.GetRegisterAlignment() == EAlignment.Good && !goodCharacters.Contains(charRef)) goodCharacters.Add(charRef);
                int chosenNum = 0;
                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(goodCharacters);
                    int fakeNum = GetPairsOfCharactersInList(fakeChars);
                    int realNum = GetPairsOfCharactersInList(goodCharacters);
                    chosenNum = MakeNumberWrong(realNum, fakeNum, 0);
                }
                else
                {
                    chosenNum = GetPairsOfCharactersInList(goodCharacters);
                }
                string number = chosenNum.ToString();
                string cardPlural = "pairs";
                string isAre = "are";
                if (chosenNum == 1)
                {
                    cardPlural = "pair";
                    isAre = "is only";
                }
                if (chosenNum == 0)
                {
                    number = "NO";
                }

                finalInfo = $"There {isAre} {number} {cardPlural} of Good characters";
            }


            // VillagerKnitter
            if (chosenInfoType == "VillagerKnitter")
            {
                if (charRef.GetCharacterType() == ECharacterType.Villager && !villagerCharacters.Contains(charRef)) villagerCharacters.Add(charRef);
                int chosenNum = 0;
                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(villagerCharacters);
                    int fakeNum = GetPairsOfCharactersInList(fakeChars);
                    int realNum = GetPairsOfCharactersInList(villagerCharacters);
                    chosenNum = MakeNumberWrong(realNum, fakeNum, 0);
                }
                else
                {
                    chosenNum = GetPairsOfCharactersInList(villagerCharacters);
                }
                string number = chosenNum.ToString();
                string cardPlural = "pairs";
                string villPlural = "Villagers";
                string isAre = "are";
                if (chosenNum == 1)
                {
                    cardPlural = "pair";
                    isAre = "is only";
                    villPlural = "Villager";
                }
                if (chosenNum == 0)
                {
                    number = "NO";
                }

                finalInfo = $"There {isAre} {number} {cardPlural} of Villagers";
            }


            // Arithmetician
            if (chosenInfoType == "Arithmetician")
            {
                if (charRef.GetRegisterAlignment() == EAlignment.Evil && !evilCharacters.Contains(charRef)) evilCharacters.Add(charRef);
                int chosenNum = 0;
                foreach (Character character in evilCharacters)
                {
                    chosenNum += character.id;
                }
                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(evilCharacters);
                    int fakeNum = 0;
                    foreach (Character character in fakeChars)
                    {
                        fakeNum += character.id;
                    }
                    chosenNum = MakeNumberWrong(chosenNum, fakeNum, 1);
                }

                finalInfo = $"The sum of all Evil is: {chosenNum}";
            }


            // GoodArithmetician
            if (chosenInfoType == "GoodArithmetician")
            {
                if (charRef.GetRegisterAlignment() == EAlignment.Good && !goodCharacters.Contains(charRef)) goodCharacters.Add(charRef);
                int chosenNum = 0;
                foreach (Character character in goodCharacters)
                {
                    chosenNum += character.id;
                }
                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(goodCharacters);
                    int fakeNum = 0;
                    foreach (Character character in fakeChars)
                    {
                        fakeNum += character.id;
                    }
                    chosenNum = MakeNumberWrong(chosenNum, fakeNum, 1);
                }

                finalInfo = $"The sum of all Good is: {chosenNum}";
            }


            // ArithmeticianMultiply
            if (chosenInfoType == "ArithmeticianMultiply")
            {
                if (charRef.GetRegisterAlignment() == EAlignment.Evil && !evilCharacters.Contains(charRef)) evilCharacters.Add(charRef);
                int chosenNum = 0;
                foreach (Character character in evilCharacters)
                {
                    chosenNum *= character.id;
                }
                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(evilCharacters);
                    int fakeNum = 0;
                    foreach (Character character in fakeChars)
                    {
                        fakeNum *= character.id;
                    }
                    chosenNum = MakeNumberWrong(chosenNum, fakeNum, 1);
                }

                finalInfo = $"The product of all Evil is: {chosenNum}";
            }


            // GoodArithmeticianMultiply
            if (chosenInfoType == "GoodArithmeticianMultiply")
            {
                if (charRef.GetRegisterAlignment() == EAlignment.Good && !goodCharacters.Contains(charRef)) goodCharacters.Add(charRef);
                int chosenNum = 0;
                foreach (Character character in goodCharacters)
                {
                    if (chosenNum == 0) chosenNum = character.id;
                    else chosenNum *= character.id;
                }
                if (lying)
                {
                    Il2CppSystem.Collections.Generic.List<Character> fakeChars = GetFakeGroup(goodCharacters);
                    int fakeNum = 0;
                    foreach (Character character in fakeChars)
                    {
                        if (fakeNum == 0) fakeNum = character.id;
                        else fakeNum *= character.id;
                    }
                    chosenNum = MakeNumberWrong(chosenNum, fakeNum, 1);
                }

                finalInfo = $"The product of all Good is: {chosenNum}";
            }


            // Longest Good Chain
            if (chosenInfoType == "GoodChainLong")
            {
                if (charRef.GetRegisterAlignment() == EAlignment.Good && !goodCharacters.Contains(charRef)) goodCharacters.Add(charRef);
                if (charRef.GetRegisterAlignment() == EAlignment.Evil && !evilCharacters.Contains(charRef)) evilCharacters.Add(charRef);
                Il2CppSystem.Collections.Generic.List<Character> villageTimesThree = new Il2CppSystem.Collections.Generic.List<Character>();
                for (int i = 0; i < 3; i++)
                {
                    foreach (Character character in Gameplay.CurrentCharacters)
                    {
                        villageTimesThree.Add(character);
                    }
                }

                int truthFinalNum = 0;
                int truthCheckNum = 0;
                int lieFinalNum = 0;
                int lieCheckNum = 0;

                Il2CppSystem.Collections.Generic.List<Character> fakeGroup = GetFakeGroup(goodCharacters);

                foreach (Character character in villageTimesThree)
                {
                    if (goodCharacters.Contains(character)) truthCheckNum++;
                    else
                    {
                        if (truthCheckNum > truthFinalNum) truthFinalNum = truthCheckNum;
                        truthCheckNum = 0;
                    }
                    if (fakeGroup.Contains(character)) lieCheckNum++;
                    else
                    {
                        if (lieCheckNum > lieFinalNum) lieFinalNum = lieCheckNum;
                        lieCheckNum = 0;
                    }
                }

                lieFinalNum = MakeNumberWrong(truthFinalNum, lieFinalNum, 1);

                int conjureNumber = truthFinalNum;
                if (lying) conjureNumber = lieFinalNum;

                if (conjureNumber == 1)
                {
                    finalInfo = "All Good characters are isolated";
                }
                else
                {
                    finalInfo = $"The longest chain of Good characters is:\n{conjureNumber} cards long";
                }
            }

            // Longest Evil Chain
            if (chosenInfoType == "EvilChainLong")
            {
                if (charRef.GetRegisterAlignment() == EAlignment.Good && !goodCharacters.Contains(charRef)) goodCharacters.Add(charRef);
                if (charRef.GetRegisterAlignment() == EAlignment.Evil && !evilCharacters.Contains(charRef)) evilCharacters.Add(charRef);
                Il2CppSystem.Collections.Generic.List<Character> villageTimesThree = new Il2CppSystem.Collections.Generic.List<Character>();
                for (int i = 0; i < 3; i++)
                {
                    foreach (Character character in Gameplay.CurrentCharacters)
                    {
                        villageTimesThree.Add(character);
                    }
                }

                int truthFinalNum = 0;
                int truthCheckNum = 0;
                int lieFinalNum = 0;
                int lieCheckNum = 0;

                Il2CppSystem.Collections.Generic.List<Character> fakeGroup = GetFakeGroup(evilCharacters);

                foreach (Character character in villageTimesThree)
                {
                    if (evilCharacters.Contains(character)) truthCheckNum++;
                    else
                    {
                        if (truthCheckNum > truthFinalNum) truthFinalNum = truthCheckNum;
                        truthCheckNum = 0;
                    }
                    if (fakeGroup.Contains(character)) lieCheckNum++;
                    else
                    {
                        if (lieCheckNum > lieFinalNum) lieFinalNum = lieCheckNum;
                        lieCheckNum = 0;
                    }
                }

                lieFinalNum = MakeNumberWrong(truthFinalNum, lieFinalNum, 1);

                int conjureNumber = truthFinalNum;
                if (lying) conjureNumber = lieFinalNum;

                if (conjureNumber == 1)
                {
                    finalInfo = "All Evil characters are isolated";
                }
                else
                {
                    finalInfo = $"The longest chain of Evil characters is:\n{conjureNumber} cards long";
                }
            }

            // Shortest Good Chain
            if (chosenInfoType == "GoodChainShort")
            {
                if (charRef.GetRegisterAlignment() == EAlignment.Good && !goodCharacters.Contains(charRef)) goodCharacters.Add(charRef);
                if (charRef.GetRegisterAlignment() == EAlignment.Evil && !evilCharacters.Contains(charRef)) evilCharacters.Add(charRef);
                Il2CppSystem.Collections.Generic.List<Character> villageTimesThree = new Il2CppSystem.Collections.Generic.List<Character>();
                for (int i = 0; i < 3; i++)
                {
                    foreach (Character character in Gameplay.CurrentCharacters)
                    {
                        villageTimesThree.Add(character);
                    }
                }

                int truthFinalNum = 1000;
                int truthCheckNum = 0;
                int lieFinalNum = 1000;
                int lieCheckNum = 0;

                Il2CppSystem.Collections.Generic.List<Character> fakeGroup = GetFakeGroup(goodCharacters);

                foreach (Character character in villageTimesThree)
                {
                    if (goodCharacters.Contains(character)) truthCheckNum++;
                    else
                    {
                        if (truthCheckNum < truthFinalNum && truthCheckNum != 0) truthFinalNum = truthCheckNum;
                        truthCheckNum = 0;
                    }
                    if (fakeGroup.Contains(character)) lieCheckNum++;
                    else
                    {
                        if (lieCheckNum < lieFinalNum && lieCheckNum != 0) lieFinalNum = lieCheckNum;
                        lieCheckNum = 0;
                    }
                }

                lieFinalNum = MakeNumberWrong(truthFinalNum, lieFinalNum, 1);

                int conjureNumber = truthFinalNum;
                if (lying) conjureNumber = lieFinalNum;

                if (conjureNumber == 1)
                {
                    finalInfo = "At least one Good character is isolated";
                }
                else
                {
                    finalInfo = $"The shortest chain of Good characters is:\n{conjureNumber} cards long";
                }
            }

            // Shortest Evil Distance
            if (chosenInfoType == "EvilDistShort")
            {
                if (charRef.GetRegisterAlignment() == EAlignment.Good && !goodCharacters.Contains(charRef)) goodCharacters.Add(charRef);
                if (charRef.GetRegisterAlignment() == EAlignment.Evil && !evilCharacters.Contains(charRef)) evilCharacters.Add(charRef);

                int truthFinalNum = 1000;
                int truthCheckNum = 0;
                int lieFinalNum = 1000;
                int lieCheckNum = 0;

                Il2CppSystem.Collections.Generic.List<Character> fakeGroup = GetFakeGroup(goodCharacters);

                foreach (Character character in evilCharacters)
                {
                    foreach (Character character2 in evilCharacters)
                    {
                        truthCheckNum = GetDistanceBetweenCharacters(character, character2);
                        if (truthCheckNum != 0 && truthCheckNum < truthFinalNum)
                        {
                            truthFinalNum = truthCheckNum;
                        }
                    }
                }
                foreach (Character character in fakeGroup)
                {
                    foreach (Character character2 in fakeGroup)
                    {
                        lieCheckNum = GetDistanceBetweenCharacters(character, character2);
                        if (lieCheckNum != 0 && lieCheckNum < lieFinalNum)
                        {
                            lieFinalNum = lieCheckNum;
                        }
                    }
                }

                lieFinalNum = MakeNumberWrong(truthFinalNum, lieFinalNum, 1);

                int conjureNumber = truthFinalNum;
                if (lying) conjureNumber = lieFinalNum;

                string cardsPlural = "cards";

                if (conjureNumber == 1)
                {
                    cardsPlural = "card";
                }
                finalInfo = $"The shortest distance between two Evils is:\n{conjureNumber} {cardsPlural}";
            }

            // Longest Evil Distance
            if (chosenInfoType == "EvilDistLong")
            {
                if (charRef.GetRegisterAlignment() == EAlignment.Good && !goodCharacters.Contains(charRef)) goodCharacters.Add(charRef);
                if (charRef.GetRegisterAlignment() == EAlignment.Evil && !evilCharacters.Contains(charRef)) evilCharacters.Add(charRef);

                int truthFinalNum = 0;
                int truthCheckNum = 0;
                int lieFinalNum = 0;
                int lieCheckNum = 0;

                Il2CppSystem.Collections.Generic.List<Character> fakeGroup = GetFakeGroup(goodCharacters);

                foreach (Character character in evilCharacters)
                {
                    foreach (Character character2 in evilCharacters)
                    {
                        truthCheckNum = GetDistanceBetweenCharacters(character, character2);
                        if (truthCheckNum != 0 && truthCheckNum > truthFinalNum)
                        {
                            truthFinalNum = truthCheckNum;
                        }
                    }
                }
                foreach (Character character in fakeGroup)
                {
                    foreach (Character character2 in fakeGroup)
                    {
                        lieCheckNum = GetDistanceBetweenCharacters(character, character2);
                        if (lieCheckNum != 0 && lieCheckNum > lieFinalNum)
                        {
                            lieFinalNum = lieCheckNum;
                        }
                    }
                }

                lieFinalNum = MakeNumberWrong(truthFinalNum, lieFinalNum, 1);

                int conjureNumber = truthFinalNum;
                if (lying) conjureNumber = lieFinalNum;

                string cardsPlural = "cards";

                if (conjureNumber == 1)
                {
                    cardsPlural = "card";
                }
                finalInfo = $"The longest distance between two Evils is:\n{conjureNumber} {cardsPlural}";
            }
            if (chosenInfoType == "Cartomancer")
            {
                w_Cartomancer cartoRole = new w_Cartomancer();
                if (lying) returnInfo = cartoRole.GetBluffInfo(charRef);
                else returnInfo = cartoRole.GetInfo(charRef);
                finalInfo = returnInfo.desc;
                selection = returnInfo.characters;
            }
            if (chosenInfoType == "Duchess")
            {
                Il2CppSystem.Collections.Generic.List<Character> charactersNotMe = new Il2CppSystem.Collections.Generic.List<Character>();
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (character != charRef) charactersNotMe.Add(character);
                }
                if (!lying)
                {
                    // Do truthful info first because it's easier
                    Il2CppSystem.Collections.Generic.List<ECharacterType> chosenTypes = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
                    Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
                    chosenTypes.Add(selection[0].GetRegisterAs().type);
                    selection.Add(Gameplay.CurrentCharacters[UnityEngine.Random.RandomRangeInt(0, Gameplay.CurrentCharacters.Count)]); // Start with a random character
                    foreach (Character character in charactersNotMe)
                    {
                        if (!chosenTypes.Contains(character.GetRegisterAs().type)) possibleTargets.Add(character);
                    }
                    if (possibleTargets.Count != 0)
                    {
                        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
                        chosenTypes.Add(selection[1].GetRegisterAs().type);
                    }
                    possibleTargets.Clear();
                    foreach (Character character in charactersNotMe)
                    {
                        if (!chosenTypes.Contains(character.GetRegisterAs().type)) possibleTargets.Add(character);
                    }
                    if (possibleTargets.Count != 0)
                    {
                        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
                        chosenTypes.Add(selection[2].GetRegisterAs().type);
                    }
                    possibleTargets.Clear();
                    foreach (Character character in charactersNotMe)
                    {
                        if (chosenTypes.Contains(character.GetRegisterAs().type)) possibleTargets.Add(character);
                    }
                    if (possibleTargets.Count != 0)
                    {
                        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
                    }
                }
                else
                {
                    Il2CppSystem.Collections.Generic.List<ECharacterType> possibleTypes = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
                    if (villagerCharacters.Count != 0) possibleTypes.Add(ECharacterType.Villager);
                    if (outcastCharacters.Count != 0) possibleTypes.Add(ECharacterType.Outcast);
                    if (minionCharacters.Count != 0) possibleTypes.Add(ECharacterType.Minion);
                    if (demonCharacters.Count != 0) possibleTypes.Add(ECharacterType.Demon);
                    if (possibleTypes.Count < 3) // If there's less than 3 in-play types, nothing to worry about, just grab four random characters.
                    {
                        selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
                        charactersNotMe.Remove(selection[0]);
                        selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
                        charactersNotMe.Remove(selection[1]);
                        selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
                        charactersNotMe.Remove(selection[2]);
                        selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
                    }
                    else
                    {
                        Il2CppSystem.Collections.Generic.List<int> possibleCounts = new Il2CppSystem.Collections.Generic.List<int>();
                        possibleCounts.Add(2);
                        possibleCounts.Add(2);
                        if (possibleTypes.Count > 3) possibleCounts.Add(4);

                        int chosenCount = possibleCounts[UnityEngine.Random.RandomRangeInt(0, possibleCounts.Count)];
                        if (chosenCount == 4)
                        {
                            selection.Add(villagerCharacters[UnityEngine.Random.RandomRangeInt(0, villagerCharacters.Count)]);
                            selection.Add(outcastCharacters[UnityEngine.Random.RandomRangeInt(0, outcastCharacters.Count)]);
                            selection.Add(minionCharacters[UnityEngine.Random.RandomRangeInt(0, minionCharacters.Count)]);
                            selection.Add(demonCharacters[UnityEngine.Random.RandomRangeInt(0, demonCharacters.Count)]);
                        }
                        else
                        {
                            possibleTypes.Clear();
                            if (villagerCharacters.Count > 1) possibleTypes.Add(ECharacterType.Villager);
                            if (outcastCharacters.Count > 1) possibleTypes.Add(ECharacterType.Outcast);
                            if (minionCharacters.Count > 1) possibleTypes.Add(ECharacterType.Minion);
                            if (demonCharacters.Count != 0) possibleTypes.Add(ECharacterType.Demon);
                            Il2CppSystem.Collections.Generic.List<ECharacterType> chosenTypes = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
                            chosenTypes.Add(possibleTypes[UnityEngine.Random.RandomRangeInt(0, possibleTypes.Count)]);
                            possibleTypes.Remove(chosenTypes[0]);
                            chosenTypes.Add(possibleTypes[UnityEngine.Random.RandomRangeInt(0, possibleTypes.Count)]);

                            Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
                            foreach (Character character in charactersNotMe)
                            {
                                if (chosenTypes.Contains(character.GetRegisterAs().type))
                                {
                                    possibleTargets.Add(character);
                                }
                            }
                            selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
                            possibleTargets.Remove(selection[0]);
                            selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
                            possibleTargets.Remove(selection[1]);
                            selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
                            possibleTargets.Remove(selection[2]);
                        }

                    }
                }

                if (selection.Count == 4)
                {
                    finalInfo = $"Among {MentionEveryCharacterInList(selection, "")}, there are 3 Types";
                }
                else
                {
                    selection.Clear();
                    finalInfo = "There are two or less in-play Types";
                }
            }

            if (chosenInfoType == "Scout")
            {
                string evilName = "";
                CharacterData evilData = charRef.dataRef;
                int chosenDist = 0;

                if (charRef.GetRegisterAlignment() == EAlignment.Good && !goodCharacters.Contains(charRef)) goodCharacters.Add(charRef);
                if (charRef.GetRegisterAlignment() == EAlignment.Evil && !evilCharacters.Contains(charRef)) evilCharacters.Add(charRef);

                Il2CppSystem.Collections.Generic.List<CharacterData> allPossibleEvils = new Il2CppSystem.Collections.Generic.List<CharacterData>();
                Il2CppSystem.Collections.Generic.List<CharacterData> inPlayEvils = new Il2CppSystem.Collections.Generic.List<CharacterData>();
                foreach (Character character in evilCharacters)
                {
                    inPlayEvils.Add(character.GetRegisterAs());
                    allPossibleEvils.Add(character.GetRegisterAs());
                }
                Il2CppSystem.Collections.Generic.List<CharacterData> outOfPlayEvils = GetPossibleHiddenRoles();
                foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Evil))
                {
                    if (!inPlayEvils.Contains(character)) outOfPlayEvils.Add(character);
                    if (!allPossibleEvils.Contains(character)) allPossibleEvils.Add(character);
                }

                if (allPossibleEvils.Count == 0 || (inPlayEvils.Count == 0 && !lying))
                {
                    returnInfo.desc = "There are no Evil characters";
                }
                else
                {
                    if (lying) evilData = GetRandomItemOfList(allPossibleEvils);
                    else evilData = GetRandomItemOfList(inPlayEvils);
                    evilName = evilData.characterName;
                    int trueDist = 10000;
                    int checkDist = 10000;
                    foreach (Character character in Gameplay.CurrentCharacters)
                    {
                        if (character.GetRegisterAs().characterName == evilName && character.GetRegisterAlignment() == EAlignment.Evil)
                        {
                            checkDist = GetClosestDistance(evilCharacters, character);
                            if (checkDist < trueDist) trueDist = checkDist;
                        }
                    }
                    if (!lying) chosenDist = trueDist;
                    else
                    {
                        Il2CppSystem.Collections.Generic.List<Character> fakeEvilTeam = GetFakeEvilTeam();
                        Character anchor = GetRandomItemOfList(fakeEvilTeam);
                        int falseDist = GetClosestDistance(fakeEvilTeam, anchor);
                        falseDist = MakeNumberWrongByRange(trueDist, falseDist, 1, 4, 3, 2);
                        chosenDist = falseDist;
                    }

                    string cards = "cards";
                    string their = "its";
                    if (chosenDist == 1) cards = "card";
                    if (evilData.gender == EGender.Male) their = "his";
                    if (evilData.gender == EGender.Female) their = "her";
                    if (evilData.gender == EGender.They) their = "their";

                    finalInfo = $"{evilName} is {chosenDist} {cards} away from {their} closest Evil";
                }
            }
            if (chosenInfoType == "Visionary")
            {
                w_Visionary visionaryRole = new w_Visionary();
                if (lying) returnInfo = visionaryRole.GetBluffInfo(charRef);
                else returnInfo = visionaryRole.GetInfo(charRef);
                finalInfo = returnInfo.desc;
                selection = returnInfo.characters;
            }

            returnInfo.desc = finalInfo;
            returnInfo.characters = selection;
            MelonLogger.Msg($"Info type of {chosenInfoType} is: {finalInfo}");
            return returnInfo;
        }

        public string ConjureStatusName(string statusID)
        {
            switch (statusID)
            {
                // This mod
                case "999": return "Hidden Role";
                case "132": return "Killed by Caedoccidere";
                case "311814931": return "Killed by Carnicarius";
                case "2018931154": return "Tricked by the Faerie";
                case "918918": return "Is Iris";
                case "918919": return "Hypnotised by Iris";
                case "1252291208": return "Targeted by Leviathan";
                case "968": return "Misled";
                case "1521203119": return "Evil Outcast";
                case "133": return "Killed by Sanguitaurus";
                case "82113114": return "Good (Mutant)";
                case "16118119": return "Evil (Mutant)";
                case "1615919151": return "Poisoned";
                case "1923920382": return "Killed by the Switchblade";
                case "2051879715": return "Good (Tergiversator/Switchblade)";
                case "2051879522": return "Evil (Tergiversator/Switchblade)";
                case "2114495619": return "Taunt, Fail (Undying)";
                case "2114495161": return "Taunt, Death (Undying)";
                case "2114495239": return "Taunt, Victory (Undying)";
                case "318251620": return "Is the Cryptid";

                // Skill Cycler's Riddles
                case "880": return "Evil-turned";
                case "879": return "Muddled";
                case "901": return "Villager (Trickster)";
                case "902": return "Outcast (Trickster)";
                case "903": return "Minion (Trickster)";
                case "904": return "Not Bugged (Trickster)";
                case "874": return "Killed by the Hitman";
                case "878": return "Guarded";
                case "882": return "Dead (Skill Cycler's Riddles)";
                case "881": return "Confused";
                case "1201": return "Has Ghost Ability (Mad Scientist)";
                case "1202": return "Has Sleeper Ability (Mad Scientist)";
                case "1203": return "Has Undying Ability (Mad Scientist)";
                case "1204": return "Has Guardian Ability (Mad Scientist)";
                case "873": return "Accused";
                case "876": return "Killed by the Follower";

                // Power Play
                case "195": return "Dueled (Pirate)";
                case "200": return "Starved (Famine)";
                case "205": return "Immune (Pestilence)";
                case "210": return "Protected";
                case "230": return "Jinxed (Ambusher)";
                case "235": return "Swapped (Godfather)";
                case "255": return "Targeted (Hangman)";
                case "260": return "Mad (260)";
                case "261": return "Mad (261)";
                case "265": return "Mad (265)";

                // The Salem Trials
                case "1615919000": return "Heretic (Inquisitor)";

                // LRZH's Circus
                case "311": return "Evil (Ogre)";
                case "312": return "Miraged (Mirage)";
                case "313": return "Hosted (Lleech)";
                case "314": return "Haunted (Wraith)";
            }
            return statusID;
        }



        public Il2CppSystem.Collections.Generic.List<Character> GetCurrentCharacters()
        {
            Il2CppSystem.Collections.Generic.List<Character> allChars = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                allChars.Add(character);
            }
            return allChars;
        }



        [HarmonyPatch(typeof(Gameplay), "OnCharacterReveal")]
        public static class w_AnyRevealPatch
        {
            public static ETriggerPhase AnyReveal = (ETriggerPhase)1121218522;
            [HarmonyPrefix]
            public static bool CharacterRevealPrefix(Character obj)
            {
                // MelonLogger.Msg("Revealing character...");
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    //MelonLogger.Msg("Calling on the Ritualist");
                    character.Act(AnyReveal);
                }
                return true;
            }
        }
        public static class w_StatusLog
        {
            [HarmonyPatch(typeof(Character), nameof(Character.RevealAllReal))]
            public static class pvt
            {
                public static void Postfix(Character __instance)
                {
                    if (__instance.statuses.statuses.Count != 0)
                    {
                        wx_SavedScripts sharedScripts = new wx_SavedScripts();
                        foreach (ECharacterStatus status in __instance.statuses.statuses)
                        {
                            sharedScripts.DebugMessage($"Found status on #{__instance.id}: {sharedScripts.ConjureStatusName(status.ToString())}");
                        }
                    }
                }
            }
        }




        /* Doesn't fucking work :(
        [HarmonyPatch(typeof(Gossip), nameof(Gossip.infoRoles))]
        private static class PoetInfo
        {
            private static void Postfix(ref Gossip __instance, Character charRef, ref Il2CppSystem.Collections.Generic.List<Role> __result)
            {
                __result.Add(new w_Arithmetician());
                __result.Add(new w_Chiromancer());
                __result.Add(new w_Clairvoyant());
                __result.Add(new w_Detective());
                __result.Add(new w_Devout());
                __result.Add(new w_Introvert());
                __result.Add(new w_Jewelsmith());
                __result.Add(new w_Lamb());
                __result.Add(new w_Prince());
                __result.Add(new w_Ranger());
                __result.Add(new w_Sentinel());
                __result.Add(new w_Sheriff());
                __result.Add(new w_Spy());
            }
        }
        */
    }

}
