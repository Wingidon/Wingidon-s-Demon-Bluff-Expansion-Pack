using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using HarmonyLib;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Tergiversator : Role
{
    int alignmentTimer = 0;
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public w_Tergiversator() : base(ClassInjector.DerivedConstructorPointer<w_Tergiversator>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Tergiversator(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            if (sharedScripts.PercentChance(50))
            {
                charRef.ChangeAlignment(EAlignment.Evil);
                charRef.statuses.AddStatus(ECharacterStatus.AppearLying, charRef);
                charRef.statuses.AddStatus(ECharacterStatus.AppearDisguised, charRef);
                charRef.statuses.AddStatus(TergiStatus.w_tergiEvil, charRef);
                charRef.statuses.statuses.Remove(ECharacterStatus.AppearTruthfull);
                charRef.statuses.statuses.Remove(ECharacterStatus.AppearHonest);
                charRef.statuses.statuses.Remove(TergiStatus.w_tergiGood);
            }
            else
            {
                charRef.ChangeAlignment(EAlignment.Good);
                charRef.statuses.AddStatus(ECharacterStatus.AppearTruthfull, charRef);
                charRef.statuses.AddStatus(ECharacterStatus.AppearHonest, charRef);
                charRef.statuses.AddStatus(TergiStatus.w_tergiGood, charRef);
                charRef.statuses.statuses.Remove(ECharacterStatus.AppearLying);
                charRef.statuses.statuses.Remove(ECharacterStatus.AppearDisguised);
                charRef.statuses.statuses.Remove(TergiStatus.w_tergiEvil);
            }
        }
        if (trigger == wx_SavedScripts.w_AnyRevealPatch.AnyReveal)
        {
            if (charRef.GetState() == ECharacterState.Dead) return;
            alignmentTimer++;
            if (alignmentTimer < 4) return;
            alignmentTimer -= 4;
            if (charRef.alignment == EAlignment.Good)
            {
                charRef.ChangeAlignment(EAlignment.Evil);
                charRef.statuses.AddStatus(ECharacterStatus.AppearLying, charRef);
                charRef.statuses.AddStatus(ECharacterStatus.AppearDisguised, charRef);
                charRef.statuses.AddStatus(TergiStatus.w_tergiEvil, charRef);
                charRef.statuses.statuses.Remove(ECharacterStatus.AppearTruthfull);
                charRef.statuses.statuses.Remove(ECharacterStatus.AppearHonest);
                charRef.statuses.statuses.Remove(TergiStatus.w_tergiGood);
            }
            else
            {
                charRef.ChangeAlignment(EAlignment.Good);
                charRef.statuses.AddStatus(ECharacterStatus.AppearTruthfull, charRef);
                charRef.statuses.AddStatus(ECharacterStatus.AppearHonest, charRef);
                charRef.statuses.AddStatus(TergiStatus.w_tergiGood, charRef);
                charRef.statuses.statuses.Remove(ECharacterStatus.AppearLying);
                charRef.statuses.statuses.Remove(ECharacterStatus.AppearDisguised);
                charRef.statuses.statuses.Remove(TergiStatus.w_tergiEvil);
                bool gameEnd = true;
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (character.alignment == EAlignment.Evil && character.GetState() != ECharacterState.Dead)
                    {
                        gameEnd = false;
                        break;
                    }
                }
                if (gameEnd)
                {
                    charRef.RevealReal();
                    charRef.KillByDemon(charRef);
                }
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetRandomNonsense());
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {

        return GetRandomNonsense();
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return GetRandomNonsense();
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        Act(trigger, charRef);
    }
    public string ConjourInfo()
    {
        return "";
    }

    public ActedInfo GetRandomNonsense()
    {
        Il2CppSystem.Collections.Generic.List<string> randomNonsense = new Il2CppSystem.Collections.Generic.List<string>();
        randomNonsense.Add("To be Good or not to be Good - THAT is the question");
        randomNonsense.Add("To be Evil or not to be Evil - THAT is the question");
        randomNonsense.Add("The Arbiter and the Judge have an immense amount of respect for each other");
        randomNonsense.Add("The Arithmetician has never made a mistake in her life");
        randomNonsense.Add("Nobody is sure why the Bloodseer is considered \"Good\". The Empress says she's \"helpful\"");
        randomNonsense.Add("The Cardshark has never lost a card game. People think she cheats, but she doesn't");
        //randomNonsense.Add("People go to the Fortune Teller when they want to hear something, the Oracle when they need to hear something, and the Chiromancer when they aren't sure");
        randomNonsense.Add("The Clairvoyant considers the Enlightened a role model");
        randomNonsense.Add("The Copycat is the Doppelganger's sibling, and only the Druid knows so far");
        randomNonsense.Add("One time, the Detective was so frustrated with us, she declared everyone a liar and stormed off with a huff");
        randomNonsense.Add("The Devout's real name is Faith");
        randomNonsense.Add("The Forager and the Dreamer once both called the same person a cabbage. They're best friends now!");
        randomNonsense.Add("The Gossip knows almost everything about everyone. She's who I'm getting most of my info from!");
        randomNonsense.Add("The Gravekeeper and the Medium have a friendly rivalry with each other");
        randomNonsense.Add("The Introvert and the Confessor are best friends");
        //randomNonsense.Add("I heard the Gemcrafter telling the Jewelsmith that she bedazzled Mendaverte once. The Jewelsmith said she was probably just Corrupted");
        randomNonsense.Add("The Knave's been going on about some \"Riddle\" nonsense lately");
        randomNonsense.Add("The first shepherd the Lamb ever tried was the Drunk. I'm sure you can guess how that went");
        //randomNonsense.Add("One time, a random stranger came into the village and told the Performer that \"the muppets always lie\". Called themselves \"docomputernerd4096\", whatever that means");
        randomNonsense.Add("The Prince wishes his mother, the Empress, was more trusting. The Empress wishes her son had her instinct");
        //randomNonsense.Add("The Ranger and the Hunter have a friendly rivalry going on, they constantly try to one-up the other during target practice");
        randomNonsense.Add("The Scavenger is the only person who isn't sad at funerals. We're used to it by now");
        randomNonsense.Add("One time, the Sentinel called the Drunk an idiot, and the Drunk agreed!");
        randomNonsense.Add("The Sheriff wants to jail the Doppelganger and Copycat for identity theft, but he can't find either of them");
        randomNonsense.Add("The Spy sleeps on the roof of his house sometimes. He thinks nobody's caught on");
        randomNonsense.Add("The Sheriff and Warden both think the other is an idiot, but they love each other. Romantically");

        randomNonsense.Add("Would it surprise you to learn that the Chatterbox and the Rambler usually get along?");
        randomNonsense.Add("Some think the Lunatic is just stupid, but I'm pretty sure the guy is actually crazy");
        randomNonsense.Add("I saw a Puppet next to the Demon instead of a Puppeteer earlier, it was pretty weird");
        randomNonsense.Add("I still don't know what's up with the Mutant, but he scares me");
        randomNonsense.Add("One time, the Revolutionary yelled out \"Viva la revolution!\" and then proceeded to execute Iris");
        randomNonsense.Add("The Renegade and the Turncoat go way back. I think they defected together");

        randomNonsense.Add("The Acolyte is actually kind of smart, but the Zealot is an idiot");
        randomNonsense.Add("The Fanatic's just a poor naive kid who thought the Undying was really cool");
        randomNonsense.Add("The Zealot is an idiot, compared to their actually smart counterpart that is the Acolyte");
        randomNonsense.Add("The Bishop hates the Heretic, but doesn't know who exactly it is. The Heretic loves messing with her");
        randomNonsense.Add("One night, I saw the Jester following Lilis around. It was weird");
        //randomNonsense.Add("The Architect is going crazy trying to work out what was wrong with their work. They shush us whenever we try to tell them about the Saboteur");
        randomNonsense.Add("I don't know where the Swarm comes from or who any of them are, but I'm just glad one of them is defective");
        randomNonsense.Add("We aren't sure why the Turncoat chose to, well, turn coat. The Renegade might've, but she's not going to be the one to tell us");
        randomNonsense.Add("The Undying is the Judge's younger brother. They still love each other, as much as the Undying hates to admit it");

        randomNonsense.Add("Some folks consider Agmeres to be the smartest of the Demons. I think they're the only sensible one");
        randomNonsense.Add("Caedoccidere is a very violent demon, known for lashing out violently when things go wrong");
        randomNonsense.Add("The Empress tried to disguise herself to be safe from Carnicarius once. It didn't work");
        randomNonsense.Add("I've never actually seen Iris myself, I've only heard rumours");
        randomNonsense.Add("Leviathan is feared by even the rest of the Demons. Ever heard the Undying ask you to reason with them?");
        //randomNonsense.Add("She's not a temptress, but most think she's Good.\nShe's truly Evil, but her teammates betray her.\nWhen she arrives in town, mass hysteria ensues.\n\nWho is she?\nMendaverte!");
        randomNonsense.Add("I feel bad for Praesect's Zealot. The Acolyte knows what's going on, but the Zealot is just a clueless kid");
        randomNonsense.Add("When that dense mist rolls in, I worry that Tenecaligo is soon to follow");
        randomNonsense.Add("Venelum sometimes randomly asks the villagers questions. Recently, it asked me \"What's a mezepheles?\"");
        randomNonsense.Add("Veniyon is the second-oldest of her generation in her family. She gets really worried when Vidiyon goes anywhere without her");
        randomNonsense.Add("Vidiyon is the third-oldest of his generation in his family. He has five other siblings!");
        randomNonsense.Add("Viciyon is the oldest of her generation in her family. She hates her siblings, but she loves them as family");
        ActedInfo returnInfo = new ActedInfo(randomNonsense[UnityEngine.Random.RandomRangeInt(0, randomNonsense.Count)]);
        return returnInfo;
    }

    public override CharacterData GetBluffIfAble(Character charRef)
    {
        return null;
    }
}
public static class TergiStatus
{
    public static ECharacterStatus w_tergiGood = (ECharacterStatus)2051879715;
    public static ECharacterStatus w_tergiEvil = (ECharacterStatus)2051879522;

    [HarmonyPatch(typeof(Character), nameof(Character.RevealAllReal))]
    public static class pvt
    {
        public static void Postfix(Character __instance)
        {
            if (__instance.statuses.Contains(w_tergiGood))
            {
                __instance.chName.text = __instance.dataRef.name.ToUpper() + "<color=#00FF00><size=18>\n<Good></color></size>";
            }
            if (__instance.statuses.Contains(w_tergiEvil))
            {
                __instance.chName.text = __instance.dataRef.name.ToUpper() + "<color=#FF0000><size=18>\n<Evil></color></size>";
            }
        }
    }
}


