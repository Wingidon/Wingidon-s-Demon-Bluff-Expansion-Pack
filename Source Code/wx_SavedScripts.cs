using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using UnityEngine;
using HarmonyLib;

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
            if (UnityEngine.Random.RandomRange(0, 100) < percentage) return false;
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



        public Il2CppSystem.Collections.Generic.List<string> AddStringToList(string input, Il2CppSystem.Collections.Generic.List<string> list, int weight)
        {
            for (int i = 0; i < weight; i++)
            {
                list.Add(input);
            }
            return list;
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
