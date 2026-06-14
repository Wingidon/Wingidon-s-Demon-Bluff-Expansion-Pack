using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static ExpansionPack.MainMod;
using static ExpansionPack.w_Mezepheles;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Mutant : Role
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            charRef.statuses.AddStatus(ECharacterStatus.AppearTruthfull, charRef);
            Il2CppSystem.Collections.Generic.List<CharacterData> possibleMinions = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<string> possibleMinionIDs = new Il2CppSystem.Collections.Generic.List<string>();
            // Characters with Game Start who act after Chancellor: Undying, Swarm, Saboteur, Poisoner, Witch, Puppeteer, Shaman, Trickster, Summoner, Florist, Siren, Purifier, Blackmailer, Lycaon.
            // Characters wihout Game Start: Acolyte, Zealot, Professional, Turncoat, Brewer, Gangster, Strategist, Ambusher, Martyr, Cavalier, Minion, Twin Minion.
            possibleMinionIDs.Add("Undying_WING"); // Undying
            possibleMinionIDs.Add("Swarm_Good_WING"); // Swarm
            possibleMinionIDs.Add("Saboteur_WING"); // Saboteur
            possibleMinionIDs.Add("Poisoner_64796285"); // Poisoner
            possibleMinionIDs.Add("Witch_25286521"); // Witch
            possibleMinionIDs.Add("Mezepheles_09511163"); // Puppeteer
            possibleMinionIDs.Add("Shaman_26945607"); // Shaman
            possibleMinionIDs.Add("Trickster_TAVERN"); // Trickster
            possibleMinionIDs.Add("Summoner_TAVERN"); // Summoner
            possibleMinionIDs.Add("Florist_TAVERN"); // Florist
            possibleMinionIDs.Add("Siren_MaHy"); // Siren
            possibleMinionIDs.Add("Purifier_ER"); // Purifier
            possibleMinionIDs.Add("Husher_VP"); // Blackmailer
            possibleMinionIDs.Add("Lycaon_VP"); // Lycaon
            possibleMinionIDs.Add("Acolyte_WING"); // Acolyte
            possibleMinionIDs.Add("Zealot_WING"); // Zealot
            possibleMinionIDs.Add("Professional_WING"); // Professional
            possibleMinionIDs.Add("Turncoat_WING"); // Turncoat
            possibleMinionIDs.Add("Brewer_TAVERN"); // Brewer
            possibleMinionIDs.Add("Gangster_TAVERN"); // Gangster
            possibleMinionIDs.Add("Strategist_TAVERN"); // Strategist
            possibleMinionIDs.Add("Ambush_rdm"); // Ambusher
            possibleMinionIDs.Add("Martyr_rdm"); // Martyr
            possibleMinionIDs.Add("Cavalier_EP"); // Cavalier
            possibleMinionIDs.Add("Minion_71804875"); // Minion
            possibleMinionIDs.Add("Twin Minion_15695218"); // Twin Minion
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
                if (possibleMinionIDs.Contains(allDatas[j].characterId))
                {
                    possibleMinions.Add(allDatas[j]);
                }
            }
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (possibleMinions.Contains(character.dataRef))
                {
                    possibleMinions.Remove(character.dataRef);
                }
            }
            foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
            {
                if (possibleMinions.Contains(character))
                {
                    possibleMinions.Remove(character);
                }
            }    
            CharacterData fakeMinion = possibleMinions[UnityEngine.Random.RandomRangeInt(0, possibleMinions.Count)];
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, fakeMinion);


            if (UnityEngine.Random.RandomRangeInt(0,2) == 1)
            {
                charRef.ChangeAlignment(EAlignment.Evil);
                charRef.statuses.AddStatus(w_Mutant.MutantStatuses.mutantEvil, charRef);
            }
            else
            {
                charRef.ChangeAlignment(EAlignment.Good);
                Il2CppSystem.Collections.Generic.List<Character> allVillagers = Characters.Instance.FilterRealCharacterType(Gameplay.CurrentCharacters, ECharacterType.Villager);
                allVillagers = Characters.Instance.FilterRealAlignmentCharacters(allVillagers, EAlignment.Good);
                Character targetVillager = new Character();
                targetVillager = allVillagers[UnityEngine.Random.RandomRangeInt(0, allVillagers.Count)];
                targetVillager.Init(fakeMinion);
                charRef.statuses.AddStatus(w_Mutant.MutantStatuses.mutantGood, charRef);
            }
        }
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        if (charRef.alignment == EAlignment.Good)
        {
            charRef.statuses.AddStatus(MutantStatuses.mutantGood, charRef);
        }
        if (charRef.alignment == EAlignment.Evil)
        {
            charRef.statuses.AddStatus(MutantStatuses.mutantEvil, charRef);
        }
        return null;
    }
    public w_Mutant() : base(ClassInjector.DerivedConstructorPointer<w_Mutant>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Mutant(System.IntPtr ptr) : base(ptr)
    {

    }
    public static class MutantStatuses
    {
        public static ECharacterStatus mutantGood = (ECharacterStatus)82113114;
        public static ECharacterStatus mutantEvil = (ECharacterStatus)16118119;
        [HarmonyPatch(typeof(Character), nameof(Character.RevealAllReal))]
        public static class pvt
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.statuses.Contains(mutantGood))
                {
                    __instance.chName.text = "HUMAN";
                }
                if (__instance.statuses.Contains(mutantEvil))
                {
                    __instance.chName.text = "PARASITE";
                }
                if (__instance.statuses.Contains(mutantEvil) && __instance.statuses.Contains(mutantGood))
                {
                    __instance.chName.text = "SYMBIOTE";
                }
            }
        }
    }
}


