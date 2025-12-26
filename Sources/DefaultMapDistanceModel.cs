//// Decompiled with JetBrains decompiler
//// Type: TaleWorlds.CampaignSystem.GameComponents.DefaultMapDistanceModel
//// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 038459B8-4640-4714-AE67-6181A9569366
//// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll

//using TaleWorlds.CampaignSystem.ComponentInterfaces;
//using TaleWorlds.CampaignSystem.Party;
//using TaleWorlds.CampaignSystem.Settlements;
//using TaleWorlds.Library;

//#nullable disable
//namespace TaleWorlds.CampaignSystem.GameComponents;

//public class DefaultMapDistanceModel : MapDistanceModel
//{
//  private MapDistanceModel.INavigationCache _navigationCache;

//  public override int RegionSwitchCostFromLandToSea => 0;

//  public override int RegionSwitchCostFromSeaToLand => 0;

//  public override float MaximumSpawnDistanceForCompanionsAfterDisband => 150f;

//  public override void RegisterDistanceCache(
//    MobileParty.NavigationType navigationCapability,
//    MapDistanceModel.INavigationCache cacheToRegister)
//  {
//    this._navigationCache = cacheToRegister;
//    cacheToRegister.FinalizeInitialization();
//  }

//  public override float GetMaximumDistanceBetweenTwoConnectedSettlements(
//    MobileParty.NavigationType navigationCapabilities)
//  {
//    MapDistanceModel.INavigationCache navigationCache = this._navigationCache;
//    return navigationCache == null ? 0.0f : navigationCache.MaximumDistanceBetweenTwoConnectedSettlements;
//  }

//  public override float GetLandRatioOfPathBetweenSettlements(
//    Settlement fromSettlement,
//    Settlement toSettlement,
//    bool isFromPort,
//    bool isTargetingPort)
//  {
//    if (this._navigationCache == null)
//      return 1f;
//    float landRatio;
//    double distanceWithLandRatio = (double) this._navigationCache.GetSettlementToSettlementDistanceWithLandRatio(fromSettlement, false, toSettlement, false, out landRatio);
//    return landRatio;
//  }

//  public override float GetDistance(
//    Settlement fromSettlement,
//    Settlement toSettlement,
//    bool isFromPort = false,
//    bool isTargetingPort = false,
//    MobileParty.NavigationType navigationCapability = MobileParty.NavigationType.Default)
//  {
//    return this.GetDistance(fromSettlement, toSettlement, isFromPort, isTargetingPort, MobileParty.NavigationType.Default, out float _);
//  }

//  public override float GetDistance(
//    Settlement fromSettlement,
//    Settlement toSettlement,
//    bool isFromPort,
//    bool isTargetingPort,
//    MobileParty.NavigationType navigationCapability,
//    out float landRatio)
//  {
//    float distance = float.MaxValue;
//    landRatio = 1f;
//    if (fromSettlement != null && toSettlement != null)
//    {
//      if (fromSettlement != toSettlement)
//        return this._navigationCache.GetSettlementToSettlementDistanceWithLandRatio(fromSettlement, isFromPort, toSettlement, isTargetingPort, out landRatio);
//      distance = 0.0f;
//    }
//    return distance;
//  }

//  public override float GetDistance(
//    MobileParty fromMobileParty,
//    Settlement toSettlement,
//    bool isTargetingPort,
//    MobileParty.NavigationType customCapability,
//    out float estimatedLandRatio)
//  {
//    float num = 1E+08f;
//    estimatedLandRatio = 1f;
//    if (fromMobileParty.CurrentNavigationFace.FaceIndex == toSettlement.GatePosition.Face.FaceIndex)
//    {
//      if (Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(Campaign.Current.MapSceneWrapper.GetFaceTerrainType(fromMobileParty.Position.Face), MobileParty.NavigationType.Default))
//        num = fromMobileParty.Position.Distance(toSettlement.GatePosition);
//    }
//    else if (fromMobileParty.IsCurrentlyAtSea)
//    {
//      num = 1E+08f;
//    }
//    else
//    {
//      Settlement fromSettlement = Campaign.Current.Models.MapDistanceModel.GetClosestEntranceToFace(fromMobileParty.CurrentNavigationFace, MobileParty.NavigationType.Default).Item1;
//      if (fromSettlement != null)
//        num = fromMobileParty.Position.Distance(toSettlement.GatePosition) - fromSettlement.GatePosition.Distance(toSettlement.GatePosition) + Campaign.Current.Models.MapDistanceModel.GetDistance(fromSettlement, toSettlement, false, false, MobileParty.NavigationType.Default);
//    }
//    return MBMath.ClampFloat(num, 0.0f, float.MaxValue);
//  }

//  public override float GetDistance(
//    MobileParty fromMobileParty,
//    MobileParty toMobileParty,
//    MobileParty.NavigationType customCapability,
//    out float landRatio)
//  {
//    float distance;
//    Campaign.Current.Models.MapDistanceModel.GetDistance(fromMobileParty, toMobileParty, customCapability, 1E+08f, out distance, out landRatio);
//    return distance;
//  }

//  public override bool GetDistance(
//    MobileParty fromMobileParty,
//    MobileParty toMobileParty,
//    MobileParty.NavigationType customCapability,
//    float maxDistance,
//    out float distance,
//    out float landRatio)
//  {
//    landRatio = 1f;
//    distance = float.MaxValue;
//    if (fromMobileParty.CurrentNavigationFace.FaceIndex == toMobileParty.CurrentNavigationFace.FaceIndex)
//    {
//      if (Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(Campaign.Current.MapSceneWrapper.GetFaceTerrainType(fromMobileParty.Position.Face), MobileParty.NavigationType.Default))
//        distance = fromMobileParty.Position.Distance(toMobileParty.Position);
//    }
//    else
//      distance = fromMobileParty.IsCurrentlyAtSea || toMobileParty.IsCurrentlyAtSea ? float.MaxValue : fromMobileParty.Position.Distance(toMobileParty.Position);
//    distance = MBMath.ClampFloat(distance, 0.0f, float.MaxValue);
//    return (double) distance <= (double) maxDistance;
//  }

//  public override float GetDistance(
//    MobileParty fromMobileParty,
//    in CampaignVec2 toPoint,
//    MobileParty.NavigationType customCapability,
//    out float landRatio)
//  {
//    float num = float.MaxValue;
//    landRatio = 1f;
//    PathFaceRecord face = toPoint.Face;
//    if (fromMobileParty.CurrentNavigationFace.FaceIndex == face.FaceIndex)
//    {
//      if (Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(Campaign.Current.MapSceneWrapper.GetFaceTerrainType(fromMobileParty.Position.Face), MobileParty.NavigationType.Default))
//        num = fromMobileParty.Position.Distance(toPoint);
//    }
//    else
//    {
//      MapDistanceModel mapDistanceModel = Campaign.Current.Models.MapDistanceModel;
//      (Settlement, bool) closestEntranceToFace1 = mapDistanceModel.GetClosestEntranceToFace(fromMobileParty.CurrentNavigationFace, MobileParty.NavigationType.Default);
//      (Settlement, bool) closestEntranceToFace2 = mapDistanceModel.GetClosestEntranceToFace(face, MobileParty.NavigationType.Default);
//      Settlement fromSettlement = closestEntranceToFace1.Item1;
//      Settlement toSettlement = closestEntranceToFace2.Item1;
//      if (fromSettlement != null && toSettlement != null)
//        num = fromMobileParty.Position.Distance(toPoint) - fromSettlement.GatePosition.Distance(toSettlement.GatePosition) + this.GetDistance(fromSettlement, toSettlement, false, false, MobileParty.NavigationType.Default);
//    }
//    return MBMath.ClampFloat(num, 0.0f, float.MaxValue);
//  }

//  public override float GetDistance(
//    Settlement fromSettlement,
//    in CampaignVec2 toPoint,
//    bool isFromPort,
//    MobileParty.NavigationType customCapability)
//  {
//    float num1 = float.MaxValue;
//    CampaignVec2 campaignVec2_1 = isFromPort ? fromSettlement.PortPosition : fromSettlement.GatePosition;
//    CampaignVec2 campaignVec2_2 = toPoint;
//    PathFaceRecord face1 = campaignVec2_2.Face;
//    PathFaceRecord face2 = campaignVec2_1.Face;
//    if (face2.FaceIndex == face1.FaceIndex)
//    {
//      if (Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(Campaign.Current.MapSceneWrapper.GetFaceTerrainType(face2), MobileParty.NavigationType.Default))
//        num1 = campaignVec2_1.Distance(toPoint);
//    }
//    else
//    {
//      MapDistanceModel mapDistanceModel = Campaign.Current.Models.MapDistanceModel;
//      Settlement toSettlement = mapDistanceModel.GetClosestEntranceToFace(face1, MobileParty.NavigationType.Default).Item1;
//      if (toSettlement != null)
//      {
//        campaignVec2_2 = fromSettlement.GatePosition;
//        double num2 = (double) campaignVec2_2.Distance(toPoint);
//        campaignVec2_2 = fromSettlement.GatePosition;
//        double num3 = (double) campaignVec2_2.Distance(toSettlement.GatePosition);
//        num1 = (float) (num2 - num3) + mapDistanceModel.GetDistance(fromSettlement, toSettlement, false, false, MobileParty.NavigationType.Default);
//      }
//    }
//    return MBMath.ClampFloat(num1, 0.0f, 1E+08f);
//  }

//  public override float GetPortToGateDistanceForSettlement(Settlement settlement) => 1E+08f;

//  public override bool PathExistBetweenPoints(
//    in CampaignVec2 fromPoint,
//    in CampaignVec2 toPoint,
//    MobileParty.NavigationType navigationType)
//  {
//    return fromPoint.IsOnLand && toPoint.IsOnLand;
//  }

//  public override (Settlement, bool) GetClosestEntranceToFace(
//    PathFaceRecord face,
//    MobileParty.NavigationType navigationCapabilities)
//  {
//    bool isAtSea;
//    return (this._navigationCache.GetClosestSettlementToFaceIndex(face.FaceIndex, out isAtSea), isAtSea);
//  }

//  public override MBReadOnlyList<Settlement> GetNeighborsOfFortification(
//    Town town,
//    MobileParty.NavigationType navigationCapabilities)
//  {
//    return this._navigationCache.GetNeighbors(town.Settlement);
//  }

//  public override float GetTransitionCostAdjustment(
//    Settlement settlement1,
//    bool isFromPort,
//    Settlement settlement2,
//    bool isTargetingPort,
//    bool fromIsCurrentlyAtSea,
//    bool toIsCurrentlyAtSea)
//  {
//    return 0.0f;
//  }
//}
