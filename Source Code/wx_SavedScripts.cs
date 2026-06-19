using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppRewired.Glyphs;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static MelonLoader.MelonLaunchOptions;

namespace ExpansionPack
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
            for (int i = 0; i < Gameplay.CurrentCharacters.Count + 3; i++)
            {
                foreach (Character character in list)
                {
                    if (character.id == i) newList.Add(character);
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

        public bool CheckIfNeighbour(Character character1, Character character2)
        {
            if (GetDistanceBetweenCharacters(character1, character2) == 1) return true;
            return false;
        }
        public bool PercentChance(float percentage)
        {
            if (UnityEngine.Random.RandomRange(0, 100) <= percentage) return false;
            return true;
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
                                                                                                                            // Minions
            demonNames.Add("Swarm");
            pluralNamesV.Add("Acolyte");
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

            if (alwaysGoodIDs.Contains(character.dataRef.characterId)) return true;
            return false;
        }


        public int GetPairsOfCharactersInList(Il2CppSystem.Collections.Generic.List<Character> list)
        {
            MelonLogger.Msg("Getting pairs");
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
                MelonLogger.Msg($"Checking #{character.id}");
                if (list.Contains(character) && prevCount == true)
                {
                    pairs++;
                    MelonLogger.Msg($"Found a pair, there are now {pairs} pair(s)");
                }
                if (list.Contains(character))
                {
                    prevCount = true;
                    MelonLogger.Msg($"#{character.id} is in the list, so they're ready to be part of the next pair");
                }
                else
                {
                    prevCount = false;
                    MelonLogger.Msg($"#{character.id} is not in the list, so they will not be part of the next pair");
                }
                
            }
            return pairs;
        }



        public int MakeNumberWrong(int trueNumber, int falseNumber, int minimum)
        {
            int returnVal = falseNumber;
            if (trueNumber != falseNumber) return falseNumber;
            if (falseNumber == minimum) returnVal++;
            else returnVal--;
            return returnVal;
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
            if (goodCharacters.Count != 0 && evilCharacters.Count != 0) infoTypes.Add("Medium"); // #X is a Good Y
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
                infoTypes.Add("GoodArithmetician"); // sum of all Good
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
            MelonLogger.Msg($"Chose info type of {chosenInfoType}");
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
                    Il2CppSystem.Collections.Generic.List<CharacterData> deckMinions = new Il2CppSystem.Collections.Generic.List<CharacterData>();
                    foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Minion))
                    {
                        deckMinions.Add(character);
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




            returnInfo.desc = finalInfo;
            returnInfo.characters = selection;
            MelonLogger.Msg($"Info type of {chosenInfoType} is: {finalInfo}");
            return returnInfo;
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
