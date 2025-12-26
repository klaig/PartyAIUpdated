//// Decompiled with JetBrains decompiler
//// Type: TaleWorlds.CampaignSystem.ComponentInterfaces.MapDistanceModel
//// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 038459B8-4640-4714-AE67-6181A9569366
//// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll

//using TaleWorlds.CampaignSystem.Party;
//using TaleWorlds.CampaignSystem.Settlements;
//using TaleWorlds.Core;
//using TaleWorlds.Library;

//#nullable disable
//namespace TaleWorlds.CampaignSystem.ComponentInterfaces;

//public abstract class MapDistanceModel : MBGameModel<MapDistanceModel>
//{
//  public const float PossibleMaximumMapBoundary = 1E+08f;

//  public abstract int RegionSwitchCostFromLandToSea { get; }

//  public abstract int RegionSwitchCostFromSeaToLand { get; }

//  public abstract float MaximumSpawnDistanceForCompanionsAfterDisband { get; }

//  public abstract float GetMaximumDistanceBetweenTwoConnectedSettlements(
//    MobileParty.NavigationType navigationType);

//  public abstract float GetLandRatioOfPathBetweenSettlements(
//    Settlement fromSettlement,
//    Settlement toSettlement,
//    bool isFromPort,
//    bool isTargetingPort);

//  public abstract float GetDistance(
//    MobileParty fromMobileParty,
//    Settlement toSettlement,
//    bool isTargetingPort,
//    MobileParty.NavigationType customCapability,
//    out float estimatedLandRatio);

//  public abstract float GetDistance(
//    MobileParty fromMobileParty,
//    MobileParty toMobileParty,
//    MobileParty.NavigationType customCapability,
//    out float landRatio);

//  public abstract bool GetDistance(
//    MobileParty fromMobileParty,
//    MobileParty toMobileParty,
//    MobileParty.NavigationType customCapability,
//    float maxDistance,
//    out float distance,
//    out float landRatio);

//  public abstract float GetDistance(
//    Settlement fromSettlement,
//    Settlement toSettlement,
//    bool isFromPort,
//    bool isTargetingPort,
//    MobileParty.NavigationType navigationCapability);

//  public abstract float GetDistance(
//    Settlement fromSettlement,
//    Settlement toSettlement,
//    bool isFromPort,
//    bool isTargetingPort,
//    MobileParty.NavigationType navigationCapability,
//    out float landRatio);

//  public abstract float GetDistance(
//    MobileParty fromMobileParty,
//    in CampaignVec2 toPoint,
//    MobileParty.NavigationType navigationType,
//    out float landRatio);

//  public abstract float GetDistance(
//    Settlement fromSettlement,
//    in CampaignVec2 toPoint,
//    bool isFromPort,
//    MobileParty.NavigationType navigationType);

//  public abstract float GetPortToGateDistanceForSettlement(Settlement settlement);

//  public abstract bool PathExistBetweenPoints(
//    in CampaignVec2 fromPoint,
//    in CampaignVec2 toPoint,
//    MobileParty.NavigationType navigationType);

//  public abstract void RegisterDistanceCache(
//    MobileParty.NavigationType navigationCapability,
//    MapDistanceModel.INavigationCache cacheToRegister);

//  public abstract (Settlement, bool) GetClosestEntranceToFace(
//    PathFaceRecord face,
//    MobileParty.NavigationType navigationCapabilities);

//  public abstract MBReadOnlyList<Settlement> GetNeighborsOfFortification(
//    Town town,
//    MobileParty.NavigationType navigationCapabilities);

//  public abstract float GetTransitionCostAdjustment(
//    Settlement settlement1,
//    bool isFromPort,
//    Settlement settlement2,
//    bool isTargetingPort,
//    bool fromIsCurrentlyAtSea,
//    bool toIsCurrentlyAtSea);

//  public interface INavigationCache
//  {
//    float MaximumDistanceBetweenTwoConnectedSettlements { get; }

//    float GetSettlementToSettlementDistanceWithLandRatio(
//      Settlement settlement1,
//      bool isAtSea1,
//      Settlement settlement2,
//      bool isAtSea2,
//      out float landRatio);

//    MBReadOnlyList<Settlement> GetNeighbors(Settlement settlement);

//    Settlement GetClosestSettlementToFaceIndex(int faceId, out bool isAtSea);

//    void FinalizeInitialization();
//  }
//}
