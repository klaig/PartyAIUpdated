//// Decompiled with JetBrains decompiler
//// Type: TaleWorlds.CampaignSystem.Actions.SetPartyAiAction
//// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 21D5BF30-54A7-4DA5-81B3-31D796F5D6CE
//// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll

//using TaleWorlds.CampaignSystem.Map;
//using TaleWorlds.CampaignSystem.Party;
//using TaleWorlds.CampaignSystem.Settlements;

//#nullable disable
//namespace TaleWorlds.CampaignSystem.Actions;

//public static class SetPartyAiAction
//{
//  private static void ApplyInternal(
//    MobileParty owner,
//    Settlement settlement,
//    MobileParty mobileParty,
//    CampaignVec2 position,
//    SetPartyAiAction.SetPartyAiActionDetail detail,
//    MobileParty.NavigationType navigationType,
//    bool isFromPort,
//    bool isTargetingPort)
//  {
//    switch (detail)
//    {
//      case SetPartyAiAction.SetPartyAiActionDetail.GoToSettlement:
//        if (owner.DefaultBehavior != AiBehavior.GoToSettlement || owner.TargetSettlement != settlement || navigationType != owner.DesiredAiNavigationType || owner.IsTargetingPort != isTargetingPort || owner.StartTransitionNextFrameToExitFromPort != isFromPort)
//        {
//          if (isFromPort && !owner.IsTransitionInProgress)
//            owner.StartTransitionNextFrameToExitFromPort = true;
//          owner.SetMoveGoToSettlement(settlement, navigationType, isTargetingPort);
//        }
//        if (owner.Army == null || owner.Army.LeaderParty != owner)
//          break;
//        owner.Army.ArmyType = Army.ArmyTypes.Defender;
//        owner.Army.AiBehaviorObject = (IMapPoint) settlement;
//        break;
//      case SetPartyAiAction.SetPartyAiActionDetail.PatrolAroundSettlement:
//        if (owner.DefaultBehavior != AiBehavior.PatrolAroundPoint || owner.TargetSettlement != settlement || navigationType != owner.DesiredAiNavigationType || owner.IsTargetingPort != isTargetingPort || owner.StartTransitionNextFrameToExitFromPort != isFromPort)
//        {
//          if (isFromPort && !owner.IsTransitionInProgress)
//            owner.StartTransitionNextFrameToExitFromPort = true;
//          owner.SetMovePatrolAroundSettlement(settlement, navigationType, isTargetingPort);
//        }
//        if (owner.Army == null || owner.Army.LeaderParty != owner)
//          break;
//        owner.Army.ArmyType = Army.ArmyTypes.Defender;
//        owner.Army.AiBehaviorObject = (IMapPoint) settlement;
//        break;
//      case SetPartyAiAction.SetPartyAiActionDetail.PatrolAroundPoint:
//        if (owner.DefaultBehavior == AiBehavior.PatrolAroundPoint && navigationType == owner.DesiredAiNavigationType)
//          break;
//        owner.SetMovePatrolAroundPoint(position, navigationType);
//        break;
//      case SetPartyAiAction.SetPartyAiActionDetail.RaidSettlement:
//        if (owner.DefaultBehavior == AiBehavior.RaidSettlement && owner.TargetSettlement == settlement && navigationType == owner.DesiredAiNavigationType && owner.StartTransitionNextFrameToExitFromPort == isFromPort)
//          break;
//        if (isFromPort && !owner.IsTransitionInProgress)
//          owner.StartTransitionNextFrameToExitFromPort = true;
//        owner.SetMoveRaidSettlement(settlement, navigationType);
//        if (owner.Army == null || owner.Army.LeaderParty != owner)
//          break;
//        owner.Army.ArmyType = Army.ArmyTypes.Raider;
//        owner.Army.AiBehaviorObject = (IMapPoint) settlement;
//        break;
//      case SetPartyAiAction.SetPartyAiActionDetail.BesiegeSettlement:
//        if (owner.DefaultBehavior == AiBehavior.BesiegeSettlement && owner.TargetSettlement == settlement && navigationType == owner.DesiredAiNavigationType && owner.StartTransitionNextFrameToExitFromPort == isFromPort)
//          break;
//        if (isFromPort && !owner.IsTransitionInProgress)
//          owner.StartTransitionNextFrameToExitFromPort = true;
//        owner.SetMoveBesiegeSettlement(settlement, navigationType);
//        if (owner.Army == null || owner.Army.LeaderParty != owner)
//          break;
//        owner.Army.ArmyType = Army.ArmyTypes.Besieger;
//        owner.Army.AiBehaviorObject = (IMapPoint) settlement;
//        break;
//      case SetPartyAiAction.SetPartyAiActionDetail.EngageParty:
//        if (owner.DefaultBehavior == AiBehavior.EngageParty && owner == mobileParty && navigationType == owner.DesiredAiNavigationType && owner.StartTransitionNextFrameToExitFromPort == isFromPort)
//          break;
//        if (isFromPort && !owner.IsTransitionInProgress)
//          owner.StartTransitionNextFrameToExitFromPort = true;
//        owner.SetMoveEngageParty(mobileParty, navigationType);
//        break;
//      case SetPartyAiAction.SetPartyAiActionDetail.GoAroundParty:
//        if (owner.DefaultBehavior == AiBehavior.GoAroundParty && owner == mobileParty && navigationType == owner.DesiredAiNavigationType && owner.StartTransitionNextFrameToExitFromPort == isFromPort)
//          break;
//        if (isFromPort && !owner.IsTransitionInProgress)
//          owner.StartTransitionNextFrameToExitFromPort = true;
//        owner.SetMoveGoAroundParty(mobileParty, navigationType);
//        break;
//      case SetPartyAiAction.SetPartyAiActionDetail.DefendParty:
//        if (owner.DefaultBehavior == AiBehavior.DefendSettlement && owner == mobileParty && navigationType == owner.DesiredAiNavigationType && owner.StartTransitionNextFrameToExitFromPort == isFromPort && owner.IsTargetingPort == isTargetingPort)
//          break;
//        if (isFromPort && !owner.IsTransitionInProgress)
//          owner.StartTransitionNextFrameToExitFromPort = true;
//        owner.SetMoveDefendSettlement(settlement, isTargetingPort, navigationType);
//        if (owner.Army == null || owner.Army.LeaderParty != owner)
//          break;
//        owner.Army.ArmyType = Army.ArmyTypes.Defender;
//        owner.Army.AiBehaviorObject = (IMapPoint) settlement;
//        break;
//      case SetPartyAiAction.SetPartyAiActionDetail.EscortParty:
//        if (owner.DefaultBehavior == AiBehavior.EscortParty && owner.TargetParty == mobileParty && navigationType == owner.DesiredAiNavigationType && owner.StartTransitionNextFrameToExitFromPort == isFromPort && owner.IsTargetingPort == isTargetingPort)
//          break;
//        if (isFromPort && !owner.IsTransitionInProgress)
//          owner.StartTransitionNextFrameToExitFromPort = true;
//        owner.SetMoveEscortParty(mobileParty, navigationType, isTargetingPort);
//        break;
//      case SetPartyAiAction.SetPartyAiActionDetail.MoveToNearestLand:
//        if (owner.DefaultBehavior == AiBehavior.MoveToNearestLandOrPort)
//          break;
//        owner.SetMoveToNearestLand(settlement);
//        break;
//    }
//  }

//  public static void GetActionForVisitingSettlement(
//    MobileParty owner,
//    Settlement settlement,
//    MobileParty.NavigationType navigationType,
//    bool isFromPort,
//    bool isTargetingPort)
//  {
//    SetPartyAiAction.ApplyInternal(owner, settlement, (MobileParty) null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.GoToSettlement, navigationType, isFromPort, isTargetingPort);
//  }

//  public static void GetActionForPatrollingAroundSettlement(
//    MobileParty owner,
//    Settlement settlement,
//    MobileParty.NavigationType navigationType,
//    bool isFromPort,
//    bool isTargetingPort)
//  {
//    SetPartyAiAction.ApplyInternal(owner, settlement, (MobileParty) null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.PatrolAroundSettlement, navigationType, isFromPort, isTargetingPort);
//  }

//  public static void GetActionForPatrollingAroundPoint(
//    MobileParty owner,
//    CampaignVec2 position,
//    MobileParty.NavigationType navigationType,
//    bool isFromPort)
//  {
//    SetPartyAiAction.ApplyInternal(owner, (Settlement) null, (MobileParty) null, position, SetPartyAiAction.SetPartyAiActionDetail.PatrolAroundPoint, navigationType, isFromPort, false);
//  }

//  public static void GetActionForRaidingSettlement(
//    MobileParty owner,
//    Settlement settlement,
//    MobileParty.NavigationType navigationType,
//    bool isFromPort)
//  {
//    SetPartyAiAction.ApplyInternal(owner, settlement, (MobileParty) null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.RaidSettlement, navigationType, isFromPort, false);
//  }

//  public static void GetActionForBesiegingSettlement(
//    MobileParty owner,
//    Settlement settlement,
//    MobileParty.NavigationType navigationType,
//    bool isFromPort)
//  {
//    SetPartyAiAction.ApplyInternal(owner, settlement, (MobileParty) null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.BesiegeSettlement, navigationType, isFromPort, false);
//  }

//  public static void GetActionForEngagingParty(
//    MobileParty owner,
//    MobileParty mobileParty,
//    MobileParty.NavigationType navigationType,
//    bool isFromPort)
//  {
//    SetPartyAiAction.ApplyInternal(owner, (Settlement) null, mobileParty, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.EngageParty, navigationType, isFromPort, false);
//  }

//  public static void GetActionForGoingAroundParty(
//    MobileParty owner,
//    MobileParty mobileParty,
//    MobileParty.NavigationType navigationType,
//    bool isFromPort)
//  {
//    SetPartyAiAction.ApplyInternal(owner, (Settlement) null, mobileParty, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.GoAroundParty, navigationType, isFromPort, false);
//  }

//  public static void GetActionForDefendingSettlement(
//    MobileParty owner,
//    Settlement settlement,
//    MobileParty.NavigationType navigationType,
//    bool isFromPort,
//    bool isTargetingPort)
//  {
//    SetPartyAiAction.ApplyInternal(owner, settlement, (MobileParty) null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.DefendParty, navigationType, isFromPort, isTargetingPort);
//  }

//  public static void GetActionForEscortingParty(
//    MobileParty owner,
//    MobileParty mobileParty,
//    MobileParty.NavigationType navigationType,
//    bool isFromPort,
//    bool isTargetingPort)
//  {
//    SetPartyAiAction.ApplyInternal(owner, (Settlement) null, mobileParty, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.EscortParty, navigationType, isFromPort, isTargetingPort);
//  }

//  public static void GetActionForMovingToNearestLand(MobileParty owner, Settlement settlement)
//  {
//    SetPartyAiAction.ApplyInternal(owner, settlement, (MobileParty) null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.MoveToNearestLand, MobileParty.NavigationType.Naval, false, false);
//  }

//  private enum SetPartyAiActionDetail
//  {
//    GoToSettlement,
//    PatrolAroundSettlement,
//    PatrolAroundPoint,
//    RaidSettlement,
//    BesiegeSettlement,
//    EngageParty,
//    GoAroundParty,
//    DefendParty,
//    EscortParty,
//    MoveToNearestLand,
//  }
//}
