//// Decompiled with JetBrains decompiler
//// Type: TaleWorlds.CampaignSystem.PartyThinkParams
//// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 038459B8-4640-4714-AE67-6181A9569366
//// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll

//using System.Collections.Generic;
//using TaleWorlds.CampaignSystem.Party;
//using TaleWorlds.Library;

//#nullable disable
//namespace TaleWorlds.CampaignSystem;

//public class PartyThinkParams
//{
//  public MobileParty MobilePartyOf;
//  private readonly MBList<(AIBehaviorData, float)> _aiBehaviorScores;
//  private MBList<MobileParty> _possibleArmyMembersUponArmyCreation;
//  public float CurrentObjectiveValue;
//  public bool WillGatherAnArmy;
//  public bool DoNotChangeBehavior;
//  public float StrengthOfLordsWithoutArmy;
//  public float StrengthOfLordsWithArmy;
//  public float StrengthOfLordsAtSameClanWithoutArmy;

//  public MBReadOnlyList<(AIBehaviorData, float)> AIBehaviorScores
//  {
//    get => (MBReadOnlyList<(AIBehaviorData, float)>) this._aiBehaviorScores;
//  }

//  public MBReadOnlyList<MobileParty> PossibleArmyMembersUponArmyCreation
//  {
//    get => (MBReadOnlyList<MobileParty>) this._possibleArmyMembersUponArmyCreation;
//  }

//  public PartyThinkParams(MobileParty mobileParty)
//  {
//    this._aiBehaviorScores = new MBList<(AIBehaviorData, float)>(32 /*0x20*/);
//    this._possibleArmyMembersUponArmyCreation = (MBList<MobileParty>) null;
//    this.MobilePartyOf = mobileParty;
//    this.WillGatherAnArmy = false;
//    this.DoNotChangeBehavior = false;
//    this.CurrentObjectiveValue = 0.0f;
//  }

//  public void Reset(MobileParty mobileParty)
//  {
//    this._aiBehaviorScores.Clear();
//    this._possibleArmyMembersUponArmyCreation?.Clear();
//    this.MobilePartyOf = mobileParty;
//    this.WillGatherAnArmy = false;
//    this.DoNotChangeBehavior = false;
//    this.CurrentObjectiveValue = 0.0f;
//    this.StrengthOfLordsWithoutArmy = 0.0f;
//    this.StrengthOfLordsWithArmy = 0.0f;
//    this.StrengthOfLordsAtSameClanWithoutArmy = 0.0f;
//  }

//  public void Initialization()
//  {
//    this.StrengthOfLordsWithoutArmy = 0.0f;
//    this.StrengthOfLordsWithArmy = 0.0f;
//    this.StrengthOfLordsAtSameClanWithoutArmy = 0.0f;
//    foreach (Hero hero in (List<Hero>) this.MobilePartyOf.MapFaction.Heroes)
//    {
//      if (hero.PartyBelongedTo != null)
//      {
//        MobileParty partyBelongedTo = hero.PartyBelongedTo;
//        if (partyBelongedTo.Army != null)
//        {
//          this.StrengthOfLordsWithArmy += partyBelongedTo.Party.EstimatedStrength;
//        }
//        else
//        {
//          this.StrengthOfLordsWithoutArmy += partyBelongedTo.Party.EstimatedStrength;
//          if (hero.Clan == this.MobilePartyOf.LeaderHero?.Clan)
//            this.StrengthOfLordsAtSameClanWithoutArmy += partyBelongedTo.Party.EstimatedStrength;
//        }
//      }
//    }
//  }

//  public void AddPotentialArmyMember(MobileParty armyMember)
//  {
//    if (this._possibleArmyMembersUponArmyCreation == null)
//      this._possibleArmyMembersUponArmyCreation = new MBList<MobileParty>(16 /*0x10*/);
//    this._possibleArmyMembersUponArmyCreation.Add(armyMember);
//  }

//  public bool TryGetBehaviorScore(in AIBehaviorData aiBehaviorData, out float score)
//  {
//    foreach ((AIBehaviorData, float) aiBehaviorScore in (List<(AIBehaviorData, float)>) this._aiBehaviorScores)
//    {
//      if (aiBehaviorScore.Item1.Equals(aiBehaviorData))
//      {
//        score = aiBehaviorScore.Item2;
//        return true;
//      }
//    }
//    score = 0.0f;
//    return false;
//  }

//  public void SetBehaviorScore(in AIBehaviorData aiBehaviorData, float score)
//  {
//    for (int index = 0; index < this._aiBehaviorScores.Count; ++index)
//    {
//      if (this._aiBehaviorScores[index].Item1.Equals(aiBehaviorData))
//      {
//        this._aiBehaviorScores[index] = (this._aiBehaviorScores[index].Item1, score);
//        return;
//      }
//    }
//    Debug.FailedAssert("AIBehaviorScore not found.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\ICampaignBehaviorManager.cs", nameof (SetBehaviorScore), 200);
//  }

//  public void AddBehaviorScore(in (AIBehaviorData, float) value)
//  {
//    this._aiBehaviorScores.Add(value);
//  }
//}
