using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_FiDragonfly : Role
{
    Character chRef;
    private Il2CppSystem.Action action1;
    private Il2CppSystem.Action action2;
    private Il2CppSystem.Action action3;
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
            return "Forces a new Disguise on a character.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action3;
        CharacterPicker.OnStopPick += action2;
    }
    private void CharacterPicked()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);

        string info = $"I tricked #{chars[0].id}";

        CharacterData targetNewBluff = new CharacterData();
        if (chars[0].bluff)
        {
            targetNewBluff = GetRandomValidDisguise(chars[0].bluff);
        }
        else
        {
            targetNewBluff = GetRandomValidDisguise(chars[0].dataRef);
        }

        if (targetNewBluff == null)
        {
            info = $"#{chars[0].id} did not fall for my tricks";
            OnActed(ETriggerPhase.Day, chRef, new ActedInfo(info, chars));
            return;
        }

        bool targetLying = CharacterHelper.CheckLying(chars[0]);
        if (!targetLying)
        {
            chars[0].statuses.AddStatus(ECharacterStatus.HealthyBluff, chRef);
        }
        chars[0].statuses.AddStatus(w_FiDragonflyPrank.w_fiDragonflyTricked, chRef);
        chars[0].GiveBluff(targetNewBluff);
        chars[0].RevealBluff();
        chars[0].RefreshCharacter();
        chars[0].onReveal.Invoke();
        if (targetNewBluff.picking)
        {
            chars[0].pickable.SetActive(true);
            chars[0].pickableUses += 1;
        }
        else
        {
            chars[0].Act(ETriggerPhase.Day);
        }
        if (chars[0].state == ECharacterState.Dead) chars[0].RevealReal();
        OnActed(ETriggerPhase.Day, chRef, new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);

        string info = $"I tricked #{chars[0].id}";

        CharacterData targetNewBluff = new CharacterData();
        if (chars[0].bluff)
        {
            targetNewBluff = GetRandomValidDisguise(chars[0].bluff);
        }
        else
        {
            targetNewBluff = GetRandomValidDisguise(chars[0].dataRef);
        }

        if (targetNewBluff == null)
        {
            info = $"#{chars[0].id} did not fall for my tricks";
            OnActed(ETriggerPhase.Day, chRef, new ActedInfo(info, chars));
            return;
        }

        chars[0].statuses.AddStatus(ECharacterStatus.Corrupted, chRef);
        chars[0].statuses.AddStatus(w_FiDragonflyPrank.w_fiDragonflyTricked, chRef);
        chars[0].statuses.statuses.Remove(ECharacterStatus.HealthyBluff);
        chars[0].GiveBluff(targetNewBluff);
        chars[0].RevealBluff();
        chars[0].RefreshCharacter();
        chars[0].onReveal.Invoke();
        if (targetNewBluff.picking)
        {
            chars[0].pickable.SetActive(true);
            chars[0].pickableUses += 1;
        }
        else
        {
            chars[0].Act(ETriggerPhase.Day);
        }
        if (chars[0].state == ECharacterState.Dead) chars[0].RevealReal();
        OnActed(ETriggerPhase.Day, chRef, new ActedInfo(info, chars));
        Debug.Log($"{info}");
    }
    private CharacterData GetRandomValidDisguise(CharacterData targetChar)
    {
        Il2CppSystem.Collections.Generic.List<string> validCharacterIDs = new Il2CppSystem.Collections.Generic.List<string>();
        // Whitelist characters, vanilla: Architect, Bard, Bishop, Confessor, Dreamer, Druid, Empress, Enlightened, Fortune Teller, Gemcrafter, Hunter, Jester, Judge, Knitter, Lover, Medium, Oracle, Poet, Scout, Slayer, Witness, Plague Doctor
        validCharacterIDs.Add("Architect_39883285"); // Architect
        validCharacterIDs.Add("Athlete_95133291"); // Bard
        validCharacterIDs.Add("Bishop_58855542"); // Bishop
        validCharacterIDs.Add("Confessor_18741708"); // Confessor
        validCharacterIDs.Add("Dreamer_32014895"); // Dreamer
        validCharacterIDs.Add("Druid_89845092"); // Druid
        validCharacterIDs.Add("Empress_13782227"); // Empress
        validCharacterIDs.Add("Enlightened_62576217"); // Enlightened
        validCharacterIDs.Add("Fortune Teller_74565681"); // Fortune Teller
        validCharacterIDs.Add("Archivist_34476114"); // Gemcrafter
        validCharacterIDs.Add("Hunter_93427887"); // Hunter
        validCharacterIDs.Add("Jester_41367606"); // Jester
        validCharacterIDs.Add("Judge_87202475"); // Judge
        validCharacterIDs.Add("Knitter_32352172"); // Knitter
        validCharacterIDs.Add("Lover_91302708"); // Lover
        validCharacterIDs.Add("Lookout_41018246"); // Medium
        validCharacterIDs.Add("Oracle_07039445"); // Oracle
        validCharacterIDs.Add("Gossip_WING"); // Poet
        validCharacterIDs.Add("Scout_88081716"); // Scout
        validCharacterIDs.Add("Gambler_42592744"); // Slayer
        validCharacterIDs.Add("Witness_25155076"); // Witness
        validCharacterIDs.Add("Plague Doctor_49312486"); // Plague Doctor (Useful if someone is Corrupted, despite being an Outcast)
        // Whitelist characters, Wing's Expansion: Arbiter, Arithmetician, Cardshark, Chiromancer, Clairvoyant, Convict, Forager, Gossip, Introvert, Jewelsmith, Lamb, Performer, Prince, Ranger, Scavenger, Sentinel, Sheriff, Spy, Warden
        validCharacterIDs.Add("Arbiter_WING"); // Arbiter
        validCharacterIDs.Add("Arithmetician_WING"); // Arithmetician
        validCharacterIDs.Add("Cardshark_WING"); // Cardshark
        validCharacterIDs.Add("Chiromancer_WING"); // Chiromancer
        validCharacterIDs.Add("Clairvoyant_WING"); // Clairvoyant
        validCharacterIDs.Add("Slayer_WING"); // Convict
        validCharacterIDs.Add("Forager_WING"); // Forager
        validCharacterIDs.Add("Gossip_WING"); // Gossip
        validCharacterIDs.Add("Gravekeeper_WING"); // Gravekeeper
        validCharacterIDs.Add("Introvert_WING"); // Introverrt
        validCharacterIDs.Add("Jewelsmith_WING"); // Jewelsmith
        validCharacterIDs.Add("Lamb_WING"); // Lamb
        validCharacterIDs.Add("Performer_WING"); // Performer
        validCharacterIDs.Add("Prince_WING"); // Prince
        validCharacterIDs.Add("Ranger_WING"); // Ranger
        validCharacterIDs.Add("Scavenger_WING"); // Scavenger
        validCharacterIDs.Add("Sentinel_WING"); // Sentinel
        validCharacterIDs.Add("Sheriff_WING"); // Sheriff
        validCharacterIDs.Add("Spy_WING"); // Spy
        validCharacterIDs.Add("Warden_WING"); // Warden
        // Whitelist characters, Role Ideas Collection: Lookout
        validCharacterIDs.Add("Lookout_RCol"); // Lookout
        // Whitelist characters, Reveal Dilemma: Auditor
        validCharacterIDs.Add("auditor_rdm"); // Auditor
        // Whitelist characters, Mass Hysteria: Actor, Empath, Tax Collector
        validCharacterIDs.Add("Actor_MaHy"); // Actor
        validCharacterIDs.Add("Empath_MaHy"); // Empath
        validCharacterIDs.Add("TaxCollector_MaHy"); // Tax Collector
        // Whitelist characters, ExtraRandomized: Cleric, Psychic
        validCharacterIDs.Add("Cleric_ER"); // Cleric
        validCharacterIDs.Add("Psychic_ER"); //  Psychic
        // Whitelist characters, CarlzVilliagePack: Detective, Painter, Shepherd, Village Idiot
        validCharacterIDs.Add("Detective_VP"); // Detective
        validCharacterIDs.Add("Painter_VP"); // Painter
        validCharacterIDs.Add("Shepard_VP"); // Shepherd
        validCharacterIDs.Add("Village Idiot_VP"); // Village Idiot
        // Whitelist characters, CSK's Expansion Pack: Assassin, Cleric
        validCharacterIDs.Add("Assassin_EP"); // Assassin
        validCharacterIDs.Add("Cleric_EP"); // Cleric
        // Whitelist characters, Riddler's Mod: Commander, Mathematician
        validCharacterIDs.Add("Commander_scm"); // Commander
        validCharacterIDs.Add("Director_scm"); // Director
        validCharacterIDs.Add("Mathematician_scm"); // Mathematician
        validCharacterIDs.Add("Nurse_scm"); // Nurse
        validCharacterIDs.Add("Obsessor_scm"); // Obsessor
        validCharacterIDs.Add("Riddler_scm"); // Riddler
        validCharacterIDs.Add("Scanner_scm"); // Scanner
        validCharacterIDs.Add("Stylist_scm"); // Stylist
        validCharacterIDs.Add("Necromancer_scm"); // Necromancer (Can be useful sometimes, despite being an Outcast)

        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.dataRef.characterId == "Swarm_Good_WING")
            {
                validCharacterIDs.Add("Swarm_Good_WING"); // Good Swarm, if it is already in-play.
            }
        }


        // Make sure we're not choosing the already face-up character.
        if (validCharacterIDs.Contains(targetChar.characterId))
        {
            validCharacterIDs.Remove(targetChar.characterId);
        }
        Il2CppSystem.Collections.Generic.List<CharacterData> possibleCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
        {
            if (validCharacterIDs.Contains(character.characterId))
            {
                possibleCharacters.Add(character);
            }
        }
        if (possibleCharacters.Count == 0)
        {
            return null;
        }
        return possibleCharacters[UnityEngine.Random.RandomRangeInt(0, possibleCharacters.Count)];
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_FiDragonfly() : base(ClassInjector.DerivedConstructorPointer<w_FiDragonfly>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_FiDragonfly(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public static class w_FiDragonflyPrank
    {
        public static ECharacterStatus w_fiDragonflyTricked = (ECharacterStatus)2018931154;

        [HarmonyPatch(typeof(Character), nameof(Character.RevealAllReal))]
        public static class pvt
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.statuses.Contains(w_fiDragonflyTricked))
                {
                    if (__instance.statuses.Contains(ECharacterStatus.Corrupted))
                    {
                        if (__instance.statuses.Contains(w_Mezepheles.w_MezephelesMadness.w_mezephelesMadness))
                        {
                            __instance.chName.text = __instance.dataRef.name.ToUpper() + "<size=18>\n<color=#FF0000>Con></color><color=#70E8FF><foun</color><color=#FF00DD>ded></color></size>";
                        }
                        else
                        {
                            __instance.chName.text = __instance.dataRef.name.ToUpper() + "<size=18>\n<color=#70E8FF><Bewil</color><color=#FF00DD>dered></color></size>";
                        }
                    }
                    else
                    {
                        __instance.chName.text = __instance.dataRef.name.ToUpper() + "<size=18>\n<color=#70E8FF><Tricked></color></size>";
                    }
                }
            }
        }
    }
}