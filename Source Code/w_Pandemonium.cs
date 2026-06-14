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

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Pandemonium : Demon
{
    public override Il2CppSystem.Collections.Generic.List<SpecialRule> GetRules()
    {
        Il2CppSystem.Collections.Generic.List<SpecialRule> sr = new Il2CppSystem.Collections.Generic.List<SpecialRule>();
        sr.Add(new NightModeRule(4));
        return sr;
    }
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            Health health = PlayerController.PlayerInfo.health;
            health.AddMaxHp(Gameplay.CurrentCharacters.Count);
            health.AddMaxHp(-10);
            health.Heal(100);
            Il2CppSystem.Collections.Generic.List<CharacterData> possibleDemons = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<string> possibleDemonIDs = new Il2CppSystem.Collections.Generic.List<string>();
            // Possible Demons: Baa, Lilis, Pooka, Caedoccidere, Emenverax, Praesect, Venelum, Veniyon, Shroud, Cakler, Hypnotist, Hydra, Phantom, Belias
            possibleDemonIDs.Add("Imp_58992273"); // Baa
            possibleDemonIDs.Add("Lillith_90453844"); // Lilis
            possibleDemonIDs.Add("Pooka_13445289"); // Pooka
            possibleDemonIDs.Add("Caedoccidere_WING"); // Caedoccidere
            possibleDemonIDs.Add("Illusionist_WING"); // Emenverax
            possibleDemonIDs.Add("Praesect_WING"); // Praesect
            possibleDemonIDs.Add("Mezepheles_WING"); // Venelum
            possibleDemonIDs.Add("TwinDemon_WING"); // Veniyon
            possibleDemonIDs.Add("shroud_rdm"); // Shroud
            possibleDemonIDs.Add("Cackler_MaHy"); // Cakler
            possibleDemonIDs.Add("Hypnotist_ER"); // Hypnotist
            possibleDemonIDs.Add("Hydra_VP"); // Hydra
            possibleDemonIDs.Add("Phantom_VP"); // Phantom
            possibleDemonIDs.Add("Belias_EP"); // Belias
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
                if (possibleDemonIDs.Contains(allDatas[j].characterId))
                {
                    possibleDemons.Add(allDatas[j]);
                }
            }
            CharacterData fakeDemon = possibleDemons[UnityEngine.Random.RandomRangeInt(0, possibleDemons.Count)];
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, fakeDemon);
            possibleDemons.Remove(fakeDemon);
            CharacterData realDemon = possibleDemons[UnityEngine.Random.RandomRangeInt(0, possibleDemons.Count)];
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, realDemon);
            charRef.Init(realDemon);
        }
    }
    public w_Pandemonium() : base(ClassInjector.DerivedConstructorPointer<w_Pandemonium>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Pandemonium(System.IntPtr ptr) : base(ptr)
    {

    }
}


