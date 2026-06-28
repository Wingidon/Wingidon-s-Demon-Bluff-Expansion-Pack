using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using static MelonLoader.MelonLogger;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Saint : Role
{
    public override string Description
    {
        get
        {
            return "I am always Good";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, new ActedInfo(GetRandomSaintQuote()));
        }
        if (CheckTriggerPhases().Contains(trigger))
        {
            SaintCureStatuses(charRef);
            if (charRef.alignment == EAlignment.Evil)
            {
                charRef.ChangeAlignment(EAlignment.Good);
                if (charRef.dataRef.characterId != "Saint_WING")
                {
                    wx_SavedScripts sharedScripts = new wx_SavedScripts();
                    CharacterData saintRef = sharedScripts.GrabCharacterDataByID("Saint_WING");
                    charRef.Init(saintRef);
                }
            }
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        Act(trigger, charRef);
    }

    public Il2CppSystem.Collections.Generic.List<ETriggerPhase> CheckTriggerPhases()
    {
        Il2CppSystem.Collections.Generic.List<ETriggerPhase> returnList = new Il2CppSystem.Collections.Generic.List<ETriggerPhase>();
        returnList.Add(ETriggerPhase.Start);
        returnList.Add(ETriggerPhase.AfterRoundStart);
        returnList.Add(ETriggerPhase.Day);
        returnList.Add(ETriggerPhase.Night);
        returnList.Add(ETriggerPhase.OnReveal);
        returnList.Add(ETriggerPhase.OnExecuted);
        returnList.Add(ETriggerPhase.OnPicked);
        returnList.Add(ETriggerPhase.OnDied);
        returnList.Add(wx_SavedScripts.w_AnyRevealPatch.AnyReveal);
        return returnList;
    }

    public void SaintCureStatuses(Character charRef)
    {
        if (charRef.statuses.Contains(w_Mezepheles.w_MezephelesMadness.w_mezephelesMadness))
        {
            charRef.statuses.statuses.Remove(w_Mezepheles.w_MezephelesMadness.w_mezephelesMadness);
        }
        if (charRef.statuses.Contains(IrisStatus.w_irisTrick))
        {
            charRef.statuses.statuses.Remove(IrisStatus.w_irisTrick);
        }
        if (charRef.statuses.Contains((ECharacterStatus)880)) // Evil-turned (Skill Cycler's Riddles)
        {
            charRef.statuses.statuses.Remove((ECharacterStatus)880);
        }
    }


    public static string GetRandomSaintQuote()
    {
        Il2CppSystem.Collections.Generic.List<string> quotes = SaintQuoteList();
        return quotes[UnityEngine.Random.RandomRangeInt(0, quotes.Count)];
    }

    public static Il2CppSystem.Collections.Generic.List<string> SaintQuoteList()
    {
        Il2CppSystem.Collections.Generic.List<string> returnList = new Il2CppSystem.Collections.Generic.List<string>();
        // From BotC's Saint quote
        returnList.Add("Wisdom begets peace\nPatience begets wisdom");
        returnList.Add("Fear not, for the time shall come when fear too shall pass");
        returnList.Add("Let us pray, and may the unity of our vision make Saints of us all");

        // https://www.franciscanmedia.org/franciscan-spirit-blog/quotes-from-catholic-saints/
        returnList.Add("Neither graces, nor revelations, nor raptures, nor gifts granted to a soul make it perfect, but rather the intimate union of the soul with God"); // Quote from Saint Maria Faustina Kowalska
        returnList.Add("This morning my soul is greater than the world since it possesses you, you whom heaven and earth do not contain"); // Quote from Saint Margaret of Cortona

        // https://www.goodreads.com/quotes/40124-have-patience-with-all-things-but-chiefly-have-patience-with
        returnList.Add("Have patience with all things, but chiefly have patience with yourself"); // Quote from Saint Francis de Sales
        returnList.Add("Do not lose courage in considering your own imperfections"); // Quote from Saint Francis de Sales

        // https://www.catholictradition.org/Saints/saintly-quotes12.htm
        returnList.Add("I do not fear at all what men can do to me for speaking the truth. I only fear what God would do if I were to lie"); // Quote from Saint John Bosco
        returnList.Add("Pray! Pray, but with faith – with living faith! Courage! Onward, ever onward!"); // Quote from Saint John Bosco
        returnList.Add("Be brave, do not be led by what others think or say!"); // Quote from Saint John Bosco

        // https://www.dosp.org/our-faith/saints/saint-quotes/
        returnList.Add("You see my heart, you know my desires. Possess all that I am - you alone. I am your sheep; make me worthy to overcome the devil"); // Quote from Saint Agatha
        returnList.Add("It is with the smallest brushes that the artist paints the most exquisitely beautiful pictures"); // Quote from Saint André Bessette
        returnList.Add("When we pray, the voice of the heart must be heard more than that proceeding from the mouth"); // Quote from Saint Bonaventure
        returnList.Add("In everything, whether it is a thing sensed or a thing known, God himself is hidden within"); // Quote from Saint Bonaventure
        returnList.Add("Think well. Speak well. Do well. These three things, through the mercy of God, will make one go to Heaven"); // Quote from Saint Camillus de Lellis
        returnList.Add("Be joyful, and keep your faith and your creed"); // Quote from Saint David of Wales
        returnList.Add("The Lord has turned all our sunsets into sunrise"); // Quote from Saint Clement
        returnList.Add("Arm yourself with prayer rather than a sword; wear humility rather than fine clothes"); // Quote from Saint Dominic
        returnList.Add("Nothing is so strong as gentleness; nothing is so gentle as real strength"); // Quote from Saint Francis de Sales
        returnList.Add("Never miss an opportunity to do good"); // Quote from Saint Francis de Sales
        returnList.Add("I am who I am before God, no more and no less"); // Quote from Saint Francis of Assisi
        returnList.Add("Walk with your feet on earth, but in your heart be in heaven"); // Quote from Saint John Bosco
        returnList.Add("Darkness can only be scattered by light. Hatred can only be conquered by love"); // Quote from Saint John Paul II
        returnList.Add("Be not afraid"); // Quote from Saint John Paul II
        returnList.Add("Do not be afraid. Do not be satisfied with mediocrity. Put out into the deep and let down your nets for a catch"); // Quote from Saint John Paul II
        returnList.Add("The patient and humble endurance of the cross - whatever nature it may be - is the highest work we have to do"); // Quote from Saint Katharine Drexel
        returnList.Add("Do not let a day pass without doing some good in it"); // Quote from Saint Philip Neri
        returnList.Add("Let nothing trouble you, let nothing make you afraid. All things pass away"); // Quote from Saint Teresa of Avila
        returnList.Add("Life is short; our trials last but a moment"); // Quote from Saint Teresa of Avila
        return returnList;
    }

    public w_Saint() : base(ClassInjector.DerivedConstructorPointer<w_Saint>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Saint(IntPtr ptr) : base(ptr)
    {
    }
}


