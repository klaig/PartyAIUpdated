// Decompiled with JetBrains decompiler
// Type: TaleWorlds.CampaignSystem.CampaignBehaviors.AiBehaviors.AiVisitSettlementBehavior
// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21D5BF30-54A7-4DA5-81B3-31D796F5D6CE
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll
/*
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

#nullable disable
namespace TaleWorlds.CampaignSystem.CampaignBehaviors.AiBehaviors;

public class AiVisitSettlementBehavior : CampaignBehaviorBase
{
  public const float GoodEnoughScore = 8f;
  public const float MeaningfulScoreThreshold = 0.025f;
  public const float BaseVisitScore = 1.6f;
  private const float DefaultMoneyLimitForRecruiting = 2000f;
  private SortedDictionary<(float, int), (Settlement, MobileParty.NavigationType, bool, bool)> _settlementsWithDistances = new SortedDictionary<(float, int), (Settlement, MobileParty.NavigationType, bool, bool)>();
  private IDisbandPartyCampaignBehavior _disbandPartyCampaignBehavior;

  private static float GetMaximumDistanceAsDays(MobileParty.NavigationType navigationType)
  {
    return (float) ((double) Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(navigationType) * 4.0 / ((double) Campaign.Current.EstimatedAverageLordPartySpeed * (double) CampaignTime.HoursInDay));
  }

  private float MaximumMeaningfulDistanceAsDays(MobileParty.NavigationType navigationType)
  {
    return AiVisitSettlementBehavior.GetMaximumDistanceAsDays(navigationType) * 0.7f;
  }

  private static float SearchForNeutralSettlementRadiusAsDays => 0.5f;

  private float NumberOfHoursAtDay => (float) Campaign.Current.Models.CampaignTimeModel.HoursInDay;

  private float IdealTimePeriodForVisitingOwnedSettlement
  {
    get => (float) Campaign.Current.Models.CampaignTimeModel.HoursInDay * 15f;
  }

  public override void RegisterEvents()
  {
    CampaignEvents.AiHourlyTickEvent.AddNonSerializedListener((object) this, new Action<MobileParty, PartyThinkParams>(this.AiHourlyTick));
    CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener((object) this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
  }

  private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
  {
    this._disbandPartyCampaignBehavior = Campaign.Current.GetCampaignBehavior<IDisbandPartyCampaignBehavior>();
  }

  public override void SyncData(IDataStore dataStore)
  {
  }

  private void AiHourlyTick(MobileParty mobileParty, PartyThinkParams p)
  {
    if (mobileParty.CurrentSettlement?.SiegeEvent != null)
      return;
    Settlement forAiCalculation = MobilePartyHelper.GetCurrentSettlementOfMobilePartyForAICalculation(mobileParty);
    if (mobileParty.IsBandit)
    {
      this.CalculateVisitHideoutScoresForBanditParty(mobileParty, forAiCalculation, p);
    }
    else
    {
      IFaction mapFaction = mobileParty.MapFaction;
      if (mobileParty.IsMilitia || mobileParty.IsCaravan || mobileParty.IsPatrolParty || mobileParty.IsVillager || !mapFaction.IsMinorFaction && !mapFaction.IsKingdomFaction && (mobileParty.LeaderHero == null || !mobileParty.LeaderHero.IsLord) || mobileParty.Army != null && mobileParty.AttachedTo != null && mobileParty.Army.LeaderParty != mobileParty)
        return;
      Hero leaderHero = mobileParty.LeaderHero;
      (float num1, float num2, int num3, int num4) = this.CalculatePartyParameters(mobileParty);
      float num5 = num2 / Math.Min(1f, Math.Max(0.1f, num1));
      float y = (double) num5 >= 1.0 ? 0.33f : (float) (((double) MathF.Max(1f, MathF.Min(2f, num5)) - 0.5) / 1.5);
      float food = mobileParty.Food;
      float num6 = -mobileParty.FoodChange;
      int partyTradeGold = mobileParty.PartyTradeGold;
      if (mobileParty.Army != null && mobileParty == mobileParty.Army.LeaderParty)
      {
        foreach (MobileParty attachedParty in (List<MobileParty>) mobileParty.Army.LeaderParty.AttachedParties)
        {
          food += attachedParty.Food;
          num6 += -attachedParty.FoodChange;
          partyTradeGold += attachedParty.PartyTradeGold;
        }
      }
      float num7 = 1f;
      if (leaderHero != null && mobileParty.IsLordParty)
        num7 = this.CalculateSellItemScore(mobileParty);
      int prisonerSizeLimit = mobileParty.Party.PrisonerSizeLimit;
      if (mobileParty.Army != null)
      {
        foreach (MobileParty attachedParty in (List<MobileParty>) mobileParty.Army.LeaderParty.AttachedParties)
          prisonerSizeLimit += attachedParty.Party.PrisonerSizeLimit;
      }
      this._settlementsWithDistances.Clear();
      AiVisitSettlementBehavior.FillSettlementsToVisitWithDistancesAsDays(mobileParty, this._settlementsWithDistances);
      float partySizeNormalLimit = PartyBaseHelper.FindPartySizeNormalLimit(mobileParty);
      float num8 = 2000f;
      float num9 = 2000f;
      if (leaderHero != null)
      {
        num8 = HeroHelper.StartRecruitingMoneyLimitForClanLeader(leaderHero);
        num9 = HeroHelper.StartRecruitingMoneyLimit(leaderHero);
      }
      float a = 0.2f;
      float num10 = 1f;
      foreach (KeyValuePair<(float, int), (Settlement, MobileParty.NavigationType, bool, bool)> settlementsWithDistance in this._settlementsWithDistances)
      {
        Settlement settlement = settlementsWithDistance.Value.Item1;
        MobileParty.NavigationType navigationType = settlementsWithDistance.Value.Item2;
        float num11 = settlementsWithDistance.Key.Item1;
        bool isFromPort = settlementsWithDistance.Value.Item3;
        bool isTargetingPortBetter = settlementsWithDistance.Value.Item4;
        float visitingNearbySettlementScore1 = 1.6f;
        if (!mobileParty.IsDisbanding)
        {
          IDisbandPartyCampaignBehavior campaignBehavior = this._disbandPartyCampaignBehavior;
          if ((campaignBehavior != null ? (campaignBehavior.IsPartyWaitingForDisband(mobileParty) ? 1 : 0) : 0) == 0)
          {
            if (leaderHero == null)
            {
              bool canMerge;
              float forLeaderlessParty = this.CalculateMergeScoreForLeaderlessParty(mobileParty, settlement, num11, out canMerge);
              if (canMerge)
              {
                this.AddBehaviorTupleWithScore(p, settlement, forLeaderlessParty, navigationType, isFromPort, isTargetingPortBetter);
                goto label_91;
              }
              goto label_91;
            }
            if ((double) num11 >= (double) this.MaximumMeaningfulDistanceAsDays(navigationType))
            {
              this.AddBehaviorTupleWithScore(p, settlement, 0.025f, navigationType, isFromPort, isTargetingPortBetter);
              continue;
            }
            float num12 = MathF.Max(a, num11);
            float num13 = 1f;
            if ((double) num11 > (double) a)
              num13 = num10 / (num10 - a + num11);
            float num14 = num13;
            if ((double) num1 < 0.60000002384185791)
              num14 = MathF.Pow(num13, MathF.Pow(0.6f / MathF.Max(0.15f, num1), 0.3f));
            float num15 = 1f;
            float num16 = (float) num3 / (float) num4;
            bool flag = mobileParty.Army != null && mobileParty.AttachedTo == null && mobileParty.Army.LeaderParty != mobileParty;
            if (settlement.IsFortification && (double) num16 > 0.20000000298023224)
            {
              num15 = MBMath.Map(num16 - 0.2f, 0.0f, 0.8f, 1f, 5f);
              if (flag || mobileParty.MapEvent != null || mobileParty.SiegeEvent != null)
                num15 *= 0.6f;
            }
            float num17 = 1f;
            if (mobileParty.DefaultBehavior == AiBehavior.GoToSettlement && (settlement == forAiCalculation && forAiCalculation.IsFortification || forAiCalculation == null && settlement == mobileParty.TargetSettlement))
              num17 = 1.2f;
            else if (forAiCalculation == null && settlement == mobileParty.LastVisitedSettlement)
              num17 = 0.8f;
            float val2 = (double) num16 > 0.20000000298023224 ? 1f : 0.16f;
            float num18 = Math.Max(0.0f, food) / num6;
            if ((double) num6 > 0.0 && (mobileParty.BesiegedSettlement == null || (double) num18 <= 1.0) && partyTradeGold > 100 && (settlement.IsTown || settlement.IsVillage && mobileParty.Army == null))
            {
              float thresholdForSiege = Campaign.Current.Models.MobilePartyAIModel.NeededFoodsInDaysThresholdForSiege;
              if ((double) num18 < (double) thresholdForSiege)
              {
                int num19 = (int) ((double) num6 * ((double) num18 >= 1.0 || !settlement.IsVillage ? (double) Campaign.Current.Models.PartyFoodBuyingModel.MinimumDaysFoodToLastWhileBuyingFoodFromTown : (double) Campaign.Current.Models.PartyFoodBuyingModel.MinimumDaysFoodToLastWhileBuyingFoodFromVillage)) + 1;
                float val1_1 = thresholdForSiege * 0.5f;
                float num20 = val1_1 - Math.Min(val1_1, Math.Max(0.0f, num18 - 1f));
                float val1_2 = (float) num19 + (float) (20.0 * (settlement.IsTown ? 2.0 : 1.0) * ((double) num12 > (double) num10 ? 1.0 : (double) num12 / (double) num10));
                int val1_3 = (int) ((double) (partyTradeGold - 100) / (double) Campaign.Current.Models.PartyFoodBuyingModel.LowCostFoodPriceAverage);
                val2 += (float) ((double) num20 * (double) num20 * 0.093000002205371857 * ((double) num18 < (double) val1_1 ? 15.0 + 0.5 * ((double) val1_1 - (double) num18) : 1.0)) * Math.Min(val1_2, (float) Math.Min(val1_3, settlement.ItemRoster.TotalFood)) / val1_2;
              }
            }
            float num21 = 0.0f;
            float num22 = 1f;
            if (!settlement.IsCastle && (double) num1 < 1.0 && mobileParty.GetAvailableWageBudget() > 0)
            {
              int num23 = settlement.NumberOfLordPartiesAt;
              int num24 = settlement.NumberOfLordPartiesTargeting;
              if (forAiCalculation == settlement)
              {
                int num25 = num23;
                Army army = mobileParty.Army;
                int num26 = army != null ? army.LeaderPartyAndAttachedPartiesCount : 1;
                num23 = num25 - num26;
                if (num23 < 0)
                  num23 = 0;
              }
              if (mobileParty.TargetSettlement == settlement || mobileParty.Army != null && mobileParty.Army.LeaderParty.TargetSettlement == settlement)
              {
                int num27 = num24;
                Army army = mobileParty.Army;
                int num28 = army != null ? army.LeaderPartyAndAttachedPartiesCount : 1;
                num24 = num27 - num28;
                if (num24 < 0)
                  num24 = 0;
              }
              if (mobileParty.Army != null)
                num24 += mobileParty.Army.LeaderPartyAndAttachedPartiesCount;
              if (!mobileParty.Party.IsStarving && (double) mobileParty.PartyTradeGold > (double) num9 && (leaderHero.Clan.Leader == leaderHero || (double) leaderHero.Clan.Gold > (double) num8) && (double) partySizeNormalLimit > (double) mobileParty.PartySizeRatio)
              {
                (int, float) dataFromSettlement = this.GetApproximateVolunteersCanBeRecruitedDataFromSettlement(leaderHero, settlement);
                num21 = (float) dataFromSettlement.Item1;
                if ((double) num21 > 0.0)
                {
                  float num29 = dataFromSettlement.Item2;
                  num21 = Math.Min(num21, (float) MathF.Floor((float) mobileParty.GetAvailableWageBudget() / num29));
                }
              }
              float x = num21 * num13 / MathF.Sqrt((float) (1 + num23 + num24));
              float num30 = (double) x < 1.0 ? x : (float) Math.Pow((double) x, (double) y);
              num22 = Math.Max(Math.Min(1f, val2), Math.Max(mapFaction == settlement.MapFaction ? 0.25f : 0.16f, (float) ((double) num5 * (double) Math.Max(1f, Math.Min(2f, num5)) * (double) num30 * (1.0 - 0.89999997615814209 * (double) num16) * (1.0 - 0.89999997615814209 * (double) num16))));
            }
            float visitingNearbySettlementScore2 = visitingNearbySettlementScore1 * (num22 * num15 * val2 * num14);
            if ((double) visitingNearbySettlementScore2 >= 8.0)
            {
              this.AddBehaviorTupleWithScore(p, settlement, visitingNearbySettlementScore2, navigationType, isFromPort, isTargetingPortBetter);
              break;
            }
            float num31 = 1f;
            if ((double) num21 > 0.0 && !flag)
              num31 = (float) (1.0 + (mobileParty.DefaultBehavior != AiBehavior.GoToSettlement || settlement == forAiCalculation || (double) num12 >= (double) a ? 0.0 : 0.10000000149011612 * (double) MathF.Min(5f, num21) - 0.10000000149011612 * (double) MathF.Min(5f, num21) * ((double) num12 / (double) a) * ((double) num12 / (double) a)));
            float num32 = !settlement.IsCastle || flag || (double) val2 >= 1.0 ? 1f : 1.4f;
            float visitingNearbySettlementScore3 = visitingNearbySettlementScore2 * ((settlement.IsTown ? num7 : 1f) * num31 * num32);
            if ((double) visitingNearbySettlementScore3 >= 8.0)
            {
              this.AddBehaviorTupleWithScore(p, settlement, visitingNearbySettlementScore3, navigationType, isFromPort, isTargetingPortBetter);
              break;
            }
            int totalRegulars = mobileParty.PrisonRoster.TotalRegulars;
            if (mobileParty.PrisonRoster.TotalHeroes > 0)
            {
              foreach (TroopRosterElement troopRosterElement in (List<TroopRosterElement>) mobileParty.PrisonRoster.GetTroopRoster())
              {
                if (troopRosterElement.Character.IsHero && troopRosterElement.Character.HeroObject.Clan.IsAtWarWith(settlement.MapFaction))
                  totalRegulars += 6;
              }
            }
            float num33 = 1f;
            float num34 = 1f;
            if (mobileParty.Army != null && mobileParty.Army.LeaderParty.AttachedParties.Contains(mobileParty))
            {
              if (mobileParty.Army.LeaderParty != mobileParty)
                num33 = ((float) mobileParty.Army.CohesionThresholdForDispersion - mobileParty.Army.Cohesion) / (float) mobileParty.Army.CohesionThresholdForDispersion;
              num34 = MobileParty.MainParty == null || mobileParty.Army != MobileParty.MainParty.Army ? 0.8f : 0.6f;
              foreach (MobileParty attachedParty in (List<MobileParty>) mobileParty.Army.LeaderParty.AttachedParties)
              {
                totalRegulars += attachedParty.PrisonRoster.TotalRegulars;
                if (attachedParty.PrisonRoster.TotalHeroes > 0)
                {
                  foreach (TroopRosterElement troopRosterElement in (List<TroopRosterElement>) attachedParty.PrisonRoster.GetTroopRoster())
                  {
                    if (troopRosterElement.Character.IsHero && troopRosterElement.Character.HeroObject.Clan.IsAtWarWith(settlement.MapFaction))
                      totalRegulars += 6;
                  }
                }
              }
            }
            float num35 = settlement.IsFortification ? (float) (1.0 + 2.0 * (double) (totalRegulars / prisonerSizeLimit)) : 1f;
            float num36 = mobileParty.DesiredAiNavigationType == navigationType ? 1.5f : 1f;
            float num37 = 1f;
            float num38 = 1f;
            float num39 = 1f;
            float num40 = 1f;
            float num41 = 1f;
            if ((double) val2 <= 0.5)
              (num37, num38, num39, num40) = this.CalculateBeingSettlementOwnerScores(mobileParty, settlement, forAiCalculation, -1f, num13, num1);
            float num42 = 1f;
            if (settlement.HasPort && mobileParty.Ships.Any<Ship>())
            {
              float num43 = mobileParty.Ships.AverageQ<Ship>((Func<Ship, float>) (x => x.HitPoints / x.MaxHitPoints));
              if ((double) num43 < 0.800000011920929)
                num42 = (double) num43 <= 0.60000002384185791 ? ((double) num43 <= 0.40000000596046448 ? 3f : 1.75f) : 1.5f;
            }
            visitingNearbySettlementScore1 = visitingNearbySettlementScore3 * (num41 * num17 * num33 * num35 * num34 * num37 * num39 * num38 * num40 * num36 * num42);
            goto label_91;
          }
        }
        float forDisbandingParty = this.CalculateMergeScoreForDisbandingParty(mobileParty, settlement, num11);
        this.AddBehaviorTupleWithScore(p, settlement, forDisbandingParty, navigationType, isFromPort, isTargetingPortBetter);
label_91:
        if ((double) visitingNearbySettlementScore1 > 0.02500000037252903)
          this.AddBehaviorTupleWithScore(p, settlement, visitingNearbySettlementScore1, navigationType, isFromPort, isTargetingPortBetter);
      }
    }
  }

  private (int, float) GetApproximateVolunteersCanBeRecruitedDataFromSettlement(
    Hero hero,
    Settlement settlement)
  {
    int num1 = 4;
    if (hero.MapFaction != settlement.MapFaction)
      num1 = 2;
    int num2 = 0;
    int num3 = 0;
    foreach (Hero notable in (List<Hero>) settlement.Notables)
    {
      if (notable.IsAlive)
      {
        for (int index = 0; index < num1; ++index)
        {
          if (notable.VolunteerTypes[index] != null)
          {
            ++num2;
            num3 += Campaign.Current.Models.PartyWageModel.GetCharacterWage(notable.VolunteerTypes[index]);
          }
        }
      }
    }
    if (num2 > 0)
      num3 /= num2;
    return (num2, (float) num3);
  }

  private float CalculateSellItemScore(MobileParty mobileParty)
  {
    float num1 = 0.0f;
    float num2 = 0.0f;
    for (int index = 0; index < mobileParty.ItemRoster.Count; ++index)
    {
      ItemRosterElement itemRosterElement = mobileParty.ItemRoster[index];
      EquipmentElement equipmentElement = itemRosterElement.EquipmentElement;
      if (equipmentElement.Item.IsMountable)
      {
        double num3 = (double) num2;
        int amount = itemRosterElement.Amount;
        equipmentElement = itemRosterElement.EquipmentElement;
        int num4 = equipmentElement.Item.Value;
        double num5 = (double) (amount * num4);
        num2 = (float) (num3 + num5);
      }
      else
      {
        equipmentElement = itemRosterElement.EquipmentElement;
        if (!equipmentElement.Item.IsFood)
        {
          double num6 = (double) num1;
          int amount = itemRosterElement.Amount;
          equipmentElement = itemRosterElement.EquipmentElement;
          int num7 = equipmentElement.Item.Value;
          double num8 = (double) (amount * num7);
          num1 = (float) (num6 + num8);
        }
      }
    }
    float x = ((double) num2 > (double) mobileParty.PartyTradeGold * 0.10000000149011612 ? MathF.Min(3f, MathF.Pow((float) (((double) num2 + 1000.0) / ((double) mobileParty.PartyTradeGold * 0.10000000149011612 + 1000.0)), 0.33f)) : 1f) * (1f + MathF.Min(3f, MathF.Pow(num1 / (float) (((double) mobileParty.MemberRoster.TotalManCount + 5.0) * 100.0), 0.33f)));
    if (mobileParty.Army != null)
      x = MathF.Sqrt(x);
    return x;
  }

  private (float, float, int, int) CalculatePartyParameters(MobileParty mobileParty)
  {
    float num1 = 0.0f;
    int num2 = 0;
    int num3 = 0;
    float num4;
    float num5;
    if (mobileParty.Army != null && (mobileParty.AttachedTo != null || mobileParty.Army.LeaderParty == mobileParty))
    {
      float num6 = 0.0f;
      foreach (MobileParty attachedParty in (List<MobileParty>) mobileParty.AttachedParties)
      {
        float partySizeRatio = attachedParty.PartySizeRatio;
        num6 += partySizeRatio;
        num2 += attachedParty.MemberRoster.TotalWounded;
        num3 += attachedParty.MemberRoster.TotalManCount;
        float partySizeNormalLimit = PartyBaseHelper.FindPartySizeNormalLimit(attachedParty);
        num1 += partySizeNormalLimit;
      }
      num4 = num6 / (float) mobileParty.Army.Parties.Count;
      num5 = num1 / (float) mobileParty.Army.Parties.Count;
    }
    else
    {
      num4 = mobileParty.PartySizeRatio;
      num2 += mobileParty.MemberRoster.TotalWounded;
      num3 += mobileParty.MemberRoster.TotalManCount;
      num5 = num1 + PartyBaseHelper.FindPartySizeNormalLimit(mobileParty);
    }
    return (num4, num5, num2, num3);
  }

  private void CalculateVisitHideoutScoresForBanditParty(
    MobileParty mobileParty,
    Settlement currentSettlement,
    PartyThinkParams p)
  {
    if (!mobileParty.MapFaction.Culture.CanHaveSettlement || currentSettlement != null && currentSettlement.IsHideout)
      return;
    int val1 = 0;
    for (int index = 0; index < mobileParty.ItemRoster.Count; ++index)
    {
      ItemRosterElement itemRosterElement = mobileParty.ItemRoster[index];
      val1 += itemRosterElement.Amount * itemRosterElement.EquipmentElement.Item.Value;
    }
    float num1 = (float) (1.0 + 4.0 * (double) Math.Min((float) val1, 1000f) / 1000.0);
    int num2 = 0;
    MBReadOnlyList<Hideout> allHideouts = Campaign.Current.AllHideouts;
    foreach (Hideout hideout in (List<Hideout>) allHideouts)
    {
      if (hideout.Settlement.Culture == mobileParty.Party.Culture && hideout.IsInfested)
        ++num2;
    }
    float num3 = (float) (1.0 + 4.0 * Math.Sqrt((double) (mobileParty.PrisonRoster.TotalManCount / mobileParty.Party.PrisonerSizeLimit)));
    int ahideoutToInfestIt = Campaign.Current.Models.BanditDensityModel.NumberOfMinimumBanditPartiesInAHideoutToInfestIt;
    int partiesInEachHideout = Campaign.Current.Models.BanditDensityModel.NumberOfMaximumBanditPartiesInEachHideout;
    int eachBanditFaction = Campaign.Current.Models.BanditDensityModel.NumberOfMaximumHideoutsAtEachBanditFaction;
    foreach (Hideout hideout in (List<Hideout>) allHideouts)
    {
      Settlement settlement = hideout.Settlement;
      if (settlement.Party.MapEvent == null && settlement.Culture == mobileParty.Party.Culture)
      {
        bool isTargetingPort = false;
        MobileParty.NavigationType bestNavigationType;
        float bestNavigationDistance;
        AiHelper.GetBestNavigationTypeAndAdjustedDistanceOfSettlementForMobileParty(mobileParty, settlement, isTargetingPort, out bestNavigationType, out bestNavigationDistance, out bool _);
        if (bestNavigationType != MobileParty.NavigationType.None)
        {
          double withNavigationType = (double) Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(bestNavigationType);
          float num4 = (float) (withNavigationType * 6.0 / ((double) Campaign.Current.EstimatedAverageBanditPartySpeed * (double) CampaignTime.HoursInDay));
          bestNavigationDistance = Math.Max((float) (withNavigationType * 0.15000000596046448), bestNavigationDistance);
          float num5 = bestNavigationDistance / (Campaign.Current.EstimatedAverageBanditPartySpeed * (float) CampaignTime.HoursInDay);
          float num6 = num4 / (num4 + num5);
          int val2 = 0;
          foreach (MobileParty party in (List<MobileParty>) settlement.Parties)
          {
            if (party.IsBandit && !party.IsBanditBossParty)
              ++val2;
          }
          float num7;
          if (val2 < ahideoutToInfestIt)
          {
            float num8 = (float) (eachBanditFaction - num2) / (float) eachBanditFaction;
            num7 = num2 < eachBanditFaction ? (float) (0.25 + 0.75 * (double) num8) : 0.0f;
          }
          else
            num7 = Math.Max(0.0f, (float) (1.0 * (1.0 - (double) (Math.Min(partiesInEachHideout, val2) - ahideoutToInfestIt) / (double) (partiesInEachHideout - ahideoutToInfestIt))));
          float num9 = mobileParty.DefaultBehavior != AiBehavior.GoToSettlement || mobileParty.TargetSettlement != settlement ? MBRandom.RandomFloat * MBRandom.RandomFloat * MBRandom.RandomFloat * MBRandom.RandomFloat * MBRandom.RandomFloat * MBRandom.RandomFloat * MBRandom.RandomFloat * MBRandom.RandomFloat : 1f;
          float visitingNearbySettlementScore = num6 * num7 * num1 * num9 * num3;
          if ((double) visitingNearbySettlementScore > 0.0)
            this.AddBehaviorTupleWithScore(p, hideout.Settlement, visitingNearbySettlementScore, bestNavigationType, false, false);
        }
      }
    }
  }

  private (float, float, float, float) CalculateBeingSettlementOwnerScores(
    MobileParty mobileParty,
    Settlement settlement,
    Settlement currentSettlement,
    float idealGarrisonStrengthPerWalledCenter,
    float distanceScorePure,
    float averagePartySizeRatioToMaximumSize)
  {
    float num1 = 1f;
    float num2 = 1f;
    float num3 = 1f;
    float num4 = 1f;
    Hero leaderHero = mobileParty.LeaderHero;
    IFaction mapFaction = mobileParty.MapFaction;
    if (currentSettlement != settlement && (mobileParty.Army == null || mobileParty.Army.LeaderParty != mobileParty))
    {
      if (settlement.OwnerClan.Leader == leaderHero)
      {
        float currentTime = Campaign.CurrentTime;
        float visitTimeOfOwner = settlement.LastVisitTimeOfOwner;
        float num5 = ((double) currentTime - (double) visitTimeOfOwner > (double) this.NumberOfHoursAtDay ? currentTime - visitTimeOfOwner : (float) (((double) this.NumberOfHoursAtDay - ((double) currentTime - (double) visitTimeOfOwner)) * ((double) this.IdealTimePeriodForVisitingOwnedSettlement / (double) this.NumberOfHoursAtDay))) / this.IdealTimePeriodForVisitingOwnedSettlement;
        num1 += num5;
      }
      if ((double) MBRandom.RandomFloatWithSeed((uint) mobileParty.RandomValue, (uint) CampaignTime.Now.ToDays) < 0.5 && settlement.IsFortification && leaderHero.Clan != Clan.PlayerClan && (settlement.OwnerClan.Leader == leaderHero || settlement.OwnerClan == leaderHero.Clan))
      {
        if ((double) idealGarrisonStrengthPerWalledCenter == -1.0)
          idealGarrisonStrengthPerWalledCenter = FactionHelper.FindIdealGarrisonStrengthPerWalledCenter(mapFaction as Kingdom);
        int takeFromGarrison = Campaign.Current.Models.SettlementGarrisonModel.FindNumberOfTroopsToTakeFromGarrison(mobileParty, settlement, idealGarrisonStrengthPerWalledCenter);
        if (takeFromGarrison > 0)
        {
          num2 = 1f + MathF.Pow((float) takeFromGarrison, 0.67f);
          if (mobileParty.Army != null && mobileParty.Army.LeaderParty == mobileParty)
            num2 = (float) (1.0 + ((double) num2 - 1.0) / (double) MathF.Sqrt((float) mobileParty.Army.Parties.Count));
        }
      }
    }
    if (settlement == leaderHero.HomeSettlement && mobileParty.Army == null && !settlement.IsVillage)
    {
      float num6 = leaderHero.HomeSettlement.IsCastle ? 1.5f : 1f;
      if (currentSettlement == settlement)
        num3 += (float) (3000.0 * (double) num6 / (250.0 + (double) leaderHero.PassedTimeAtHomeSettlement * (double) leaderHero.PassedTimeAtHomeSettlement));
      else
        num3 += (float) (1000.0 * (double) num6 / (250.0 + (double) leaderHero.PassedTimeAtHomeSettlement * (double) leaderHero.PassedTimeAtHomeSettlement));
    }
    if (settlement != currentSettlement)
    {
      float num7 = 1f;
      if (mobileParty.LastVisitedSettlement == settlement)
        num7 = 0.25f;
      if (settlement.IsFortification && settlement.MapFaction == mapFaction && settlement.OwnerClan != Clan.PlayerClan)
      {
        float num8 = settlement.Town.GarrisonParty != null ? settlement.Town.GarrisonParty.Party.EstimatedStrength : 0.0f;
        float num9 = FactionHelper.OwnerClanEconomyEffectOnGarrisonSizeConstant(settlement.OwnerClan);
        float num10 = FactionHelper.SettlementProsperityEffectOnGarrisonSizeConstant(settlement.Town);
        float num11 = FactionHelper.SettlementFoodPotentialEffectOnGarrisonSizeConstant(settlement);
        if ((double) idealGarrisonStrengthPerWalledCenter == -1.0)
          idealGarrisonStrengthPerWalledCenter = FactionHelper.FindIdealGarrisonStrengthPerWalledCenter(mapFaction as Kingdom);
        float num12 = idealGarrisonStrengthPerWalledCenter;
        float num13;
        if (settlement.Town.GarrisonParty != null && settlement.Town.GarrisonParty.HasLimitedWage())
        {
          num13 = (float) settlement.Town.GarrisonParty.PaymentLimit / Campaign.Current.AverageWage;
        }
        else
        {
          if (mobileParty.Army != null)
            num12 *= 0.75f;
          num13 = num12 * (num9 * num10 * num11);
        }
        float num14 = num13;
        if ((double) num8 < (double) num14)
        {
          float num15 = settlement.OwnerClan == leaderHero.Clan ? 149f : 99f;
          if (settlement.OwnerClan == Clan.PlayerClan)
            num15 *= 0.5f;
          float num16 = (float) (1.0 - (double) num8 / (double) num14);
          num4 = (float) (1.0 + (double) num15 * (double) distanceScorePure * (double) distanceScorePure * ((double) averagePartySizeRatioToMaximumSize - 0.5) * (double) num16 * (double) num16 * (double) num16 * (double) num7);
        }
      }
    }
    return (num1, num2, num3, num4);
  }

  private float CalculateMergeScoreForDisbandingParty(
    MobileParty disbandParty,
    Settlement settlement,
    float distanceAsDays)
  {
    float val1 = Campaign.MapDiagonal / (disbandParty._lastCalculatedSpeed * (float) CampaignTime.HoursInDay);
    double num1 = (double) MathF.Pow((float) (3.5 - 0.949999988079071 * ((double) Math.Min(val1, distanceAsDays) / (double) val1)), 3f);
    float num2 = disbandParty.Party.Owner?.Clan == settlement.OwnerClan ? 1f : (disbandParty.Party.Owner?.MapFaction == settlement.MapFaction ? 0.35f : 0.025f);
    float num3 = disbandParty.DefaultBehavior != AiBehavior.GoToSettlement || disbandParty.TargetSettlement != settlement ? 0.3f : 1f;
    float num4 = settlement.IsFortification ? 3f : 1f;
    double num5 = (double) num2;
    float forDisbandingParty = (float) (num1 * num5) * num3 * num4;
    if ((double) forDisbandingParty < 0.02500000037252903)
      forDisbandingParty = 0.035f;
    return forDisbandingParty;
  }

  private float CalculateMergeScoreForLeaderlessParty(
    MobileParty leaderlessParty,
    Settlement settlement,
    float distanceAsDays,
    out bool canMerge)
  {
    if (settlement.IsVillage)
    {
      canMerge = false;
      return -1f;
    }
    float val1 = Campaign.MapDiagonal / (leaderlessParty._lastCalculatedSpeed * (float) CampaignTime.HoursInDay);
    double num1 = (double) MathF.Pow((float) (3.5 - 0.949999988079071 * ((double) Math.Min(val1, distanceAsDays) / (double) val1)), 3f);
    float num2 = leaderlessParty.ActualClan == settlement.OwnerClan ? 2f : (leaderlessParty.ActualClan?.MapFaction == settlement.MapFaction ? 0.35f : 0.0f);
    float num3 = leaderlessParty.DefaultBehavior != AiBehavior.GoToSettlement || leaderlessParty.TargetSettlement != settlement ? 0.3f : 1f;
    float num4 = settlement.IsFortification ? 3f : 0.5f;
    canMerge = true;
    double num5 = (double) num2;
    return (float) (num1 * num5) * num3 * num4;
  }

  private static void FillSettlementsToVisitWithDistancesAsDays(
    MobileParty mobileParty,
    SortedDictionary<(float, int), (Settlement, MobileParty.NavigationType, bool, bool)> listToFill)
  {
    float radius = AiVisitSettlementBehavior.SearchForNeutralSettlementRadiusAsDays * Campaign.Current.EstimatedAverageLordPartySpeed * (float) CampaignTime.HoursInDay;
    if (mobileParty.LeaderHero != null && mobileParty.LeaderHero.MapFaction.IsKingdomFaction)
    {
      MBReadOnlyList<Settlement> settlements = mobileParty.MapFaction.Settlements;
      float num = 0.0f;
      foreach (Settlement settlement in (List<Settlement>) settlements)
      {
        if (AiVisitSettlementBehavior.IsSettlementSuitableForVisitingCondition(mobileParty, settlement))
        {
          MobileParty.NavigationType bestNavigationType;
          float distanceAsDays;
          bool isFromPort;
          bool isTargetingPortBetter;
          AiVisitSettlementBehavior.GetBestNavigationDataForVisitingSettlement(mobileParty, settlement, out bestNavigationType, out distanceAsDays, out isFromPort, out isTargetingPortBetter);
          if (bestNavigationType != MobileParty.NavigationType.None && (double) distanceAsDays < (double) AiVisitSettlementBehavior.GetMaximumDistanceAsDays(bestNavigationType))
          {
            num += distanceAsDays;
            listToFill.Add((distanceAsDays, settlement.GetHashCode()), (settlement, bestNavigationType, isFromPort, isTargetingPortBetter));
          }
        }
      }
      if ((double) (num / (float) listToFill.Count) > (double) AiVisitSettlementBehavior.GetMaximumDistanceAsDays(mobileParty.NavigationCapability) * 0.699999988079071 && (mobileParty.Army == null || mobileParty.Army.LeaderParty == mobileParty))
      {
        LocatableSearchData<Settlement> data = Settlement.StartFindingLocatablesAroundPosition(mobileParty.Position.ToVec2(), radius);
        for (Settlement nextLocatable = Settlement.FindNextLocatable(ref data); nextLocatable != null; nextLocatable = Settlement.FindNextLocatable(ref data))
        {
          if (!nextLocatable.IsCastle && nextLocatable.MapFaction != mobileParty.MapFaction && AiVisitSettlementBehavior.IsSettlementSuitableForVisitingCondition(mobileParty, nextLocatable))
          {
            MobileParty.NavigationType bestNavigationType;
            float distanceAsDays;
            bool isFromPort;
            bool isTargetingPortBetter;
            AiVisitSettlementBehavior.GetBestNavigationDataForVisitingSettlement(mobileParty, nextLocatable, out bestNavigationType, out distanceAsDays, out isFromPort, out isTargetingPortBetter);
            if (bestNavigationType != MobileParty.NavigationType.None && (double) distanceAsDays < (double) AiVisitSettlementBehavior.GetMaximumDistanceAsDays(bestNavigationType))
              listToFill.Add((distanceAsDays, nextLocatable.GetHashCode()), (nextLocatable, bestNavigationType, isFromPort, isTargetingPortBetter));
          }
        }
      }
    }
    else
    {
      LocatableSearchData<Settlement> data = Settlement.StartFindingLocatablesAroundPosition(mobileParty.Position.ToVec2(), radius * 1.6f);
      for (Settlement nextLocatable = Settlement.FindNextLocatable(ref data); nextLocatable != null; nextLocatable = Settlement.FindNextLocatable(ref data))
      {
        if (AiVisitSettlementBehavior.IsSettlementSuitableForVisitingCondition(mobileParty, nextLocatable))
        {
          MobileParty.NavigationType bestNavigationType;
          float distanceAsDays;
          bool isFromPort;
          bool isTargetingPortBetter;
          AiVisitSettlementBehavior.GetBestNavigationDataForVisitingSettlement(mobileParty, nextLocatable, out bestNavigationType, out distanceAsDays, out isFromPort, out isTargetingPortBetter);
          if (bestNavigationType != MobileParty.NavigationType.None && (double) distanceAsDays < (double) AiVisitSettlementBehavior.GetMaximumDistanceAsDays(bestNavigationType))
            listToFill.Add((distanceAsDays, nextLocatable.GetHashCode()), (nextLocatable, bestNavigationType, isFromPort, isTargetingPortBetter));
        }
      }
    }
    if (listToFill.AnyQ<KeyValuePair<(float, int), (Settlement, MobileParty.NavigationType, bool, bool)>>())
      return;
    Settlement factionMidSettlement = mobileParty.MapFaction.FactionMidSettlement;
    if (factionMidSettlement == null)
      return;
    if (factionMidSettlement.IsFortification)
    {
      foreach (Village boundVillage in (List<Village>) factionMidSettlement.BoundVillages)
      {
        if (AiVisitSettlementBehavior.IsSettlementSuitableForVisitingCondition(mobileParty, boundVillage.Settlement))
        {
          MobileParty.NavigationType bestNavigationType;
          float distanceAsDays;
          bool isFromPort;
          bool isTargetingPortBetter;
          AiVisitSettlementBehavior.GetBestNavigationDataForVisitingSettlement(mobileParty, boundVillage.Settlement, out bestNavigationType, out distanceAsDays, out isFromPort, out isTargetingPortBetter);
          if (bestNavigationType != MobileParty.NavigationType.None)
            listToFill.Add((distanceAsDays, boundVillage.GetHashCode()), (boundVillage.Settlement, bestNavigationType, isFromPort, isTargetingPortBetter));
        }
      }
    }
    else
    {
      if (!AiVisitSettlementBehavior.IsSettlementSuitableForVisitingCondition(mobileParty, factionMidSettlement))
        return;
      MobileParty.NavigationType bestNavigationType;
      float distanceAsDays;
      bool isFromPort;
      bool isTargetingPortBetter;
      AiVisitSettlementBehavior.GetBestNavigationDataForVisitingSettlement(mobileParty, factionMidSettlement, out bestNavigationType, out distanceAsDays, out isFromPort, out isTargetingPortBetter);
      if (bestNavigationType == MobileParty.NavigationType.None)
        return;
      listToFill.Add((distanceAsDays, factionMidSettlement.GetHashCode()), (factionMidSettlement, bestNavigationType, isFromPort, isTargetingPortBetter));
    }
  }

  private static void GetBestNavigationDataForVisitingSettlement(
    MobileParty mobileParty,
    Settlement settlement,
    out MobileParty.NavigationType bestNavigationType,
    out float distanceAsDays,
    out bool isFromPort,
    out bool isTargetingPortBetter)
  {
    bestNavigationType = MobileParty.NavigationType.None;
    float bestNavigationDistance1 = float.MaxValue;
    bool isFromPort1 = false;
    isTargetingPortBetter = false;
    isFromPort = false;
    if ((!settlement.HasPort || settlement.SiegeEvent == null || settlement.SiegeEvent.IsBlockadeActive ? 0 : (mobileParty.HasNavalNavigationCapability ? 1 : 0)) == 0)
      AiHelper.GetBestNavigationTypeAndAdjustedDistanceOfSettlementForMobileParty(mobileParty, settlement, false, out bestNavigationType, out bestNavigationDistance1, out isFromPort1);
    if (mobileParty.HasNavalNavigationCapability && settlement.HasPort)
    {
      MobileParty.NavigationType bestNavigationType1;
      float bestNavigationDistance2;
      bool isFromPort2;
      AiHelper.GetBestNavigationTypeAndAdjustedDistanceOfSettlementForMobileParty(mobileParty, settlement, true, out bestNavigationType1, out bestNavigationDistance2, out isFromPort2);
      if ((double) bestNavigationDistance2 < (double) bestNavigationDistance1)
      {
        bestNavigationType = bestNavigationType1;
        bestNavigationDistance1 = bestNavigationDistance2;
        isFromPort = isFromPort2;
        isTargetingPortBetter = true;
      }
      else
      {
        isFromPort = isFromPort1;
        isTargetingPortBetter = false;
      }
    }
    distanceAsDays = bestNavigationDistance1 / (Campaign.Current.EstimatedAverageLordPartySpeed * (float) CampaignTime.HoursInDay);
  }

  private void AddBehaviorTupleWithScore(
    PartyThinkParams p,
    Settlement settlement,
    float visitingNearbySettlementScore,
    MobileParty.NavigationType navigationType,
    bool isFromPort,
    bool isTargetingPortBetter)
  {
    AIBehaviorData aiBehaviorData = new AIBehaviorData((IMapPoint) settlement, AiBehavior.GoToSettlement, navigationType, false, isFromPort, isTargetingPortBetter);
    float score;
    if (p.TryGetBehaviorScore(in aiBehaviorData, out score))
      p.SetBehaviorScore(in aiBehaviorData, score + visitingNearbySettlementScore);
    else
      p.AddBehaviorScore((aiBehaviorData, visitingNearbySettlementScore));
  }

  private static bool IsSettlementSuitableForVisitingCondition(
    MobileParty mobileParty,
    Settlement settlement)
  {
    if (settlement.Party.MapEvent != null || settlement.Party.SiegeEvent != null && (settlement.Party.SiegeEvent.IsBlockadeActive || !mobileParty.HasNavalNavigationCapability) || mobileParty.Party.Owner.MapFaction.IsAtWarWith(settlement.MapFaction) && (!mobileParty.Party.Owner.MapFaction.IsMinorFaction && mobileParty.MapFaction.Settlements.Count != 0 || !settlement.IsVillage) || !settlement.IsVillage && !settlement.IsFortification)
      return false;
    return !settlement.IsVillage || settlement.Village.VillageState == Village.VillageStates.Normal;
  }
}
*/