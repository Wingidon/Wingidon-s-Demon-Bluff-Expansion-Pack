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
using static WingidonExpansionPack.MainMod;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_MezephelesAlt : Demon
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
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
            Il2CppSystem.Collections.Generic.List<string> whitelistOutcasts = new Il2CppSystem.Collections.Generic.List<string>();

            // Vanilla
            whitelistOutcasts.Add("Doppleganger_52694042"); // Doppelganger
            whitelistOutcasts.Add("Drunk_15369527"); // Drunk
            whitelistOutcasts.Add("Plague Doctor_49312486"); // Plague Doctor
            whitelistOutcasts.Add("Rambler_13041651"); // Rambler
            whitelistOutcasts.Add("Rambler_57930131"); // Rambler

            // Wingidon's Expansion Pack
            whitelistOutcasts.Add("Chatterbox_WING"); // Chatterbox
            whitelistOutcasts.Add("Lunatic_WING"); // Lunatic
            // whitelistOutcasts.Add("Marionette_WING"); // Marionette // Trading places seems to work awkwardly with this.
            whitelistOutcasts.Add("Pilgrim_WING"); // Pilgrim
            whitelistOutcasts.Add("Revolutionary_WING"); // Revolutionary

            // Skill Cycler's Mod
            whitelistOutcasts.Add("Confectioner_scm"); // Confectioner
            // whitelistOutcasts.Add("MadScientist_scm"); // Mad Scientist // Had some issues during playtesting.
            // whitelistOutcasts.Add("Muddler_scm"); // Muddler // Muddler in general is annoying for this concept. Let's not add it.
            whitelistOutcasts.Add("Necromancer_scm"); // Necromancer

            // Reveal Dilemma
            whitelistOutcasts.Add("sabo_rdm"); // Saboteur

            // Mass Hysteria
            whitelistOutcasts.Add("Magician_MaHy"); // Magician

            // CSK's Expansion Pack
            whitelistOutcasts.Add("Atheist_EP"); // Atheist

            // CarlzVillagePack
            whitelistOutcasts.Add("Giant_VP"); // Giant
            whitelistOutcasts.Add("MoonChild_VP"); // Moonchild
            whitelistOutcasts.Add("Rook_VP"); // Rook

            // Role Ideas Collection
            whitelistOutcasts.Add("Rook_RCol"); // Rook


            Il2CppSystem.Collections.Generic.List<CharacterData> possibleOutcasts = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            for (int j = 0; j < allDatas.Length; j++)
            {
                if (allDatas[j].type == ECharacterType.Outcast && whitelistOutcasts.Contains(allDatas[j].characterId))
                {
                    possibleOutcasts.Add(allDatas[j]);
                }
            }

            foreach (Character character in Gameplay.CurrentCharacters)
            {
                possibleOutcasts.Remove(character.dataRef);
            }

            CharacterData chosenOutcast = possibleOutcasts[UnityEngine.Random.RandomRangeInt(0, possibleOutcasts.Count)];
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Outcast, chosenOutcast);
            Il2CppSystem.Collections.Generic.List<Character> possibleVictims = Characters.Instance.FilterRealCharacterType(Gameplay.CurrentCharacters, ECharacterType.Villager);
            possibleVictims = Characters.Instance.FilterRealAlignmentCharacters(possibleVictims, EAlignment.Good);
            Character chosenVictim = possibleVictims[UnityEngine.Random.RandomRangeInt(0, possibleVictims.Count)];

            Debug.Log($"Turning #{chosenVictim.id} into the {chosenOutcast.name.ToString()}");

            chosenVictim.Init(chosenOutcast);
            chosenVictim.ChangeAlignment(EAlignment.Evil);
            chosenVictim.statuses.AddStatus(MezephelesOutcastMadness.w_mezephelesMadnessOutcast, charRef);
            chosenVictim.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
        }
    }
    public w_MezephelesAlt() : base(ClassInjector.DerivedConstructorPointer<w_MezephelesAlt>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_MezephelesAlt(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }


    public static class MezephelesOutcastMadness
    {

        public static ECharacterStatus w_mezephelesMadnessOutcast = (ECharacterStatus)1521203119; // Evil Outcast
        [HarmonyPatch(typeof(Character), nameof(Character.RevealAllReal))]
        public static class pvt
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.statuses.Contains(w_mezephelesMadnessOutcast))
                {
                    string charName = __instance.dataRef.characterId;
                    if (charName == "Doppleganger_52694042") __instance.chName.text = "<color=#FF0000>MIMIC</color>";
                    if (charName == "Drunk_15369527") __instance.chName.text = "<color=#FF0000>BRAWLER</color>";
                    if (charName == "Plague Doctor_49312486") __instance.chName.text = "<color=#FF0000>TOXOMANCER</color>";
                    if (charName == "Rambler_13041651") __instance.chName.text = "<color=#FF0000>DIVERSIONIST</color>";
                    if (charName == "Rambler_57930131") __instance.chName.text = "<color=#FF0000>DIVERSIONIST</color>";
                    if (charName == "Chatterbox_WING") __instance.chName.text = "<color=#FF0000>BACKTALKER</color>";
                    if (charName == "Lunatic_WING") __instance.chName.text = "<color=#FF0000>MANIAC</color>";
                    if (charName == "Marionette_WING") __instance.chName.text = "<color=#FF0000>RAGDOLL</color>";
                    if (charName == "Pilgrim_WING") __instance.chName.text = "<color=#FF0000>CULTIST</color>";
                    if (charName == "Revolutionary_WING") __instance.chName.text = "<color=#FF0000>REBEL</color>";
                    if (charName == "Confectioner_scm") __instance.chName.text = "<color=#FF0000>BOTCHER</color>";
                    if (charName == "MadScientist_scm") __instance.chName.text = "<color=#FF0000>EVIL GENIUS</color>";
                    if (charName == "Muddler_scm") __instance.chName.text = "<color=#FF0000>OBSTRUCTIONIST</color>";
                    if (charName == "Necromancer_scm") __instance.chName.text = "<color=#FF0000>DARK MAGE</color>";
                    if (charName == "sabo_rdm") __instance.chName.text = "<color=#FF0000>BANDIT</color>";
                    if (charName == "Magician_MaHy") __instance.chName.text = "<color=#FF0000>THAUMATURGE</color>";
                    if (charName == "Atheist_EP") __instance.chName.text = "<color=#FF0000>HIDEBOUND</color>";
                    if (charName == "Giant_VP") __instance.chName.text = "<color=#FF0000>OGRE</color>";
                    if (charName == "MoonChild_VP") __instance.chName.text = "<color=#FF0000>CAMBION</color>";
                    if (charName == "Rook_VP") __instance.chName.text = "<color=#FF0000>CITADEL</color>";
                    if (charName == "Rook_RCol") __instance.chName.text = "<color=#FF0000>CITADEL</color>";
                }
            }
        }
    }
}


