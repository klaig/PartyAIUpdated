//// Decompiled with JetBrains decompiler
//// Type: TaleWorlds.CampaignSystem.Map.DistanceCache.SandBoxNavigationCache
//// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 21D5BF30-54A7-4DA5-81B3-31D796F5D6CE
//// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using TaleWorlds.CampaignSystem.ComponentInterfaces;
//using TaleWorlds.CampaignSystem.Party;
//using TaleWorlds.CampaignSystem.Settlements;
//using TaleWorlds.Library;

//#nullable disable
//namespace TaleWorlds.CampaignSystem.Map.DistanceCache;

//public class SandBoxNavigationCache : NavigationCache<Settlement>, MapDistanceModel.INavigationCache
//{
//  private readonly int[] _excludedFaceIds;
//  private readonly int _regionSwitchCostTo0;
//  private readonly int _regionSwitchCostTo1;

//  private IMapScene MapSceneWrapper => Campaign.Current.MapSceneWrapper;

//  public SandBoxNavigationCache(MobileParty.NavigationType navigationType)
//    : base(navigationType)
//  {
//    this._excludedFaceIds = Campaign.Current.Models.PartyNavigationModel.GetInvalidTerrainTypesForNavigationType(this._navigationType);
//    this._regionSwitchCostTo0 = Campaign.Current.Models.MapDistanceModel.RegionSwitchCostFromLandToSea;
//    this._regionSwitchCostTo1 = Campaign.Current.Models.MapDistanceModel.RegionSwitchCostFromSeaToLand;
//  }

//  protected override Settlement GetCacheElement(string settlementId)
//  {
//    return Settlement.Find(settlementId);
//  }

//  protected override NavigationCacheElement<Settlement> GetCacheElement(
//    Settlement settlement,
//    bool isPortUsed)
//  {
//    return new NavigationCacheElement<Settlement>(settlement, isPortUsed);
//  }

//  float MapDistanceModel.INavigationCache.GetSettlementToSettlementDistanceWithLandRatio(
//    Settlement settlement1,
//    bool isAtSea1,
//    Settlement settlement2,
//    bool isAtSea2,
//    out float landRatio)
//  {
//    return this.GetSettlementToSettlementDistanceWithLandRatio(this.GetCacheElement(settlement1, isAtSea1), this.GetCacheElement(settlement2, isAtSea2), out landRatio);
//  }

//  public override void GetSceneXmlCrcValues(out uint sceneXmlCrc, out uint sceneNavigationMeshCrc)
//  {
//    sceneXmlCrc = this.MapSceneWrapper.GetSceneXmlCrc();
//    sceneNavigationMeshCrc = this.MapSceneWrapper.GetSceneNavigationMeshCrc();
//  }

//  protected override int GetNavMeshFaceCount()
//  {
//    return this.MapSceneWrapper.GetNumberOfNavigationMeshFaces();
//  }

//  protected override Vec2 GetNavMeshFaceCenterPosition(int faceIndex)
//  {
//    return this.MapSceneWrapper.GetNavigationMeshCenterPosition(faceIndex);
//  }

//  protected override PathFaceRecord GetFaceRecordAtIndex(int faceIndex)
//  {
//    return this.MapSceneWrapper.GetFaceAtIndex(faceIndex);
//  }

//  protected override int GetRegionSwitchCostTo0() => this._regionSwitchCostTo0;

//  protected override int GetRegionSwitchCostTo1() => this._regionSwitchCostTo1;

//  protected override int[] GetExcludedFaceIds() => this._excludedFaceIds;

//  protected override float GetRealDistanceAndLandRatioBetweenSettlements(
//    NavigationCacheElement<Settlement> settlement1,
//    NavigationCacheElement<Settlement> settlement2,
//    out float landRatio)
//  {
//    landRatio = 1f;
//    float findingMaxCostLimit = (float) Campaign.PathFindingMaxCostLimit;
//    CampaignVec2 campaignVec2_1 = settlement1.IsPortUsed ? settlement1.PortPosition : settlement1.GatePosition;
//    CampaignVec2 campaignVec2_2 = settlement2.IsPortUsed ? settlement2.PortPosition : settlement2.GatePosition;
//    NavigationPath path = new NavigationPath();
//    Campaign.Current.MapSceneWrapper.GetPathBetweenAIFaces(campaignVec2_1.Face, campaignVec2_2.Face, campaignVec2_1.ToVec2(), campaignVec2_2.ToVec2(), 0.3f, path, this.GetExcludedFaceIds(), 1f, this.GetRegionSwitchCostTo0(), this.GetRegionSwitchCostTo1());
//    float distance1;
//    Campaign.Current.MapSceneWrapper.GetPathDistanceBetweenAIFaces(campaignVec2_1.Face, campaignVec2_2.Face, campaignVec2_1.ToVec2(), campaignVec2_2.ToVec2(), 0.3f, findingMaxCostLimit, out distance1, this.GetExcludedFaceIds(), this.GetRegionSwitchCostTo0(), this.GetRegionSwitchCostTo1());
//    float distance2;
//    Campaign.Current.MapSceneWrapper.GetPathDistanceBetweenAIFaces(campaignVec2_2.Face, campaignVec2_1.Face, campaignVec2_2.ToVec2(), campaignVec2_1.ToVec2(), 0.3f, findingMaxCostLimit, out distance2, this.GetExcludedFaceIds(), this.GetRegionSwitchCostTo0(), this.GetRegionSwitchCostTo1());
//    float betweenSettlements = (float) (((double) distance1 + (double) distance2) * 0.5);
//    if ((double) betweenSettlements <= 0.0)
//      return 0.0f;
//    if (this._navigationType == MobileParty.NavigationType.Naval)
//      landRatio = 0.0f;
//    else if (this._navigationType == MobileParty.NavigationType.All)
//      landRatio = this.GetLandRatioOfPath(path, campaignVec2_1.ToVec2());
//    bool isPairChanged;
//    NavigationCacheElement<Settlement>.Sort(ref settlement1, ref settlement2, out isPairChanged);
//    int num = isPairChanged ? 1 : 0;
//    return betweenSettlements;
//  }

//  protected override void GetFaceRecordForPoint(Vec2 position, out bool isOnRegion1)
//  {
//    isOnRegion1 = true;
//    PathFaceRecord faceIndex = Campaign.Current.MapSceneWrapper.GetFaceIndex(new CampaignVec2(position, true));
//    if (!faceIndex.IsValid())
//    {
//      isOnRegion1 = false;
//      faceIndex = Campaign.Current.MapSceneWrapper.GetFaceIndex(new CampaignVec2(position, false));
//    }
//    if (faceIndex.IsValid())
//      return;
//    Debug.Print($"{position} has no region data.", color: Debug.DebugColor.Red);
//  }

//  protected override bool CheckBeingNeighbor(
//    List<Settlement> settlementsToConsider,
//    Settlement settlement1,
//    Settlement settlement2,
//    bool useGate1,
//    bool useGate2,
//    out float distance)
//  {
//    CampaignVec2 vec2_1 = useGate1 ? settlement1.GatePosition : settlement1.PortPosition;
//    CampaignVec2 vec2_2 = useGate2 ? settlement2.GatePosition : settlement2.PortPosition;
//    PathFaceRecord faceIndex1 = this.MapSceneWrapper.GetFaceIndex(in vec2_1);
//    PathFaceRecord faceIndex2 = this.MapSceneWrapper.GetFaceIndex(in vec2_2);
//    if (!faceIndex1.IsValid() || !faceIndex2.IsValid())
//      Debug.FailedAssert("Settlement navFace index should not be -1, check here", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Map\\DistanceCache\\SandboxNavigationCache.cs", nameof (CheckBeingNeighbor), 193);
//    NavigationPath path = new NavigationPath();
//    this.MapSceneWrapper.GetPathBetweenAIFaces(faceIndex1, faceIndex2, vec2_1.ToVec2(), vec2_2.ToVec2(), 0.3f, path, this.GetExcludedFaceIds(), 2f, this.GetRegionSwitchCostTo0(), this.GetRegionSwitchCostTo1());
//    bool flag = path.Size > 0 || faceIndex1.FaceIndex == faceIndex2.FaceIndex;
//    bool isOnLand = useGate1;
//    if (!this.MapSceneWrapper.GetPathDistanceBetweenAIFaces(faceIndex1, faceIndex2, vec2_1.ToVec2(), vec2_2.ToVec2(), 0.3f, Campaign.MapDiagonalSquared, out distance, this.GetExcludedFaceIds(), this.GetRegionSwitchCostTo0(), this.GetRegionSwitchCostTo1()))
//      distance = Campaign.MapDiagonalSquared;
//    for (int i = 0; i < path.Size & flag; ++i)
//    {
//      Vec2 vec2_3 = path[i] - (i == 0 ? vec2_1.ToVec2() : path[i - 1]);
//      float num1 = vec2_3.Length / 1f;
//      double num2 = (double) vec2_3.Normalize();
//      for (int index = 0; (double) index < (double) num1; ++index)
//      {
//        Vec2 vec2_4 = (i == 0 ? vec2_1.ToVec2() : path[i - 1]) + vec2_3 * 1f * (float) index;
//        if (vec2_4 != vec2_1.ToVec2() && vec2_4 != vec2_2.ToVec2())
//        {
//          CampaignVec2 vec2_5 = new CampaignVec2(vec2_4, isOnLand);
//          PathFaceRecord faceIndex3 = this.MapSceneWrapper.GetFaceIndex(in vec2_5);
//          if (faceIndex3.FaceIndex == -1)
//          {
//            isOnLand = !isOnLand;
//            vec2_5 = new CampaignVec2(vec2_4, isOnLand);
//            faceIndex3 = this.MapSceneWrapper.GetFaceIndex(in vec2_5);
//          }
//          bool isPort;
//          float positionToSettlement1 = this.GetRealPathDistanceFromPositionToSettlement(vec2_4, faceIndex3, distance, settlement1, out isPort);
//          float positionToSettlement2 = this.GetRealPathDistanceFromPositionToSettlement(vec2_4, faceIndex3, distance, settlement2, out isPort);
//          float num3 = (double) positionToSettlement1 < (double) positionToSettlement2 ? positionToSettlement1 : positionToSettlement2;
//          if (faceIndex3.FaceIndex != -1)
//          {
//            Settlement settlementToPosition = this.GetClosestSettlementToPosition(vec2_4, faceIndex3, this.GetExcludedFaceIds(), settlementsToConsider, this.GetRegionSwitchCostTo0(), this.GetRegionSwitchCostTo1(), num3 * 0.8f, out isPort);
//            if (settlementToPosition != null && settlementToPosition != settlement1 && settlementToPosition != settlement2)
//            {
//              flag = false;
//              break;
//            }
//          }
//        }
//      }
//    }
//    return flag;
//  }

//  protected override float GetRealPathDistanceFromPositionToSettlement(
//    Vec2 checkPosition,
//    PathFaceRecord currentFaceRecord,
//    float maxDistanceToLookForPathDetection,
//    Settlement currentSettlementToLook,
//    out bool isPort)
//  {
//    float positionToSettlement = float.MaxValue;
//    isPort = false;
//    switch (this._navigationType)
//    {
//      case MobileParty.NavigationType.Default:
//        CampaignVec2 vec2_1 = currentSettlementToLook.GatePosition;
//        PathFaceRecord faceIndex1 = this.MapSceneWrapper.GetFaceIndex(in vec2_1);
//        float distance1;
//        if (this.MapSceneWrapper.GetPathDistanceBetweenAIFaces(currentFaceRecord, faceIndex1, checkPosition, currentSettlementToLook.GatePosition.ToVec2(), 0.3f, maxDistanceToLookForPathDetection, out distance1, this.GetExcludedFaceIds(), this.GetRegionSwitchCostTo0(), this.GetRegionSwitchCostTo1()))
//        {
//          positionToSettlement = distance1;
//          break;
//        }
//        break;
//      case MobileParty.NavigationType.Naval:
//        CampaignVec2 vec2_2 = currentSettlementToLook.PortPosition;
//        PathFaceRecord faceIndex2 = this.MapSceneWrapper.GetFaceIndex(in vec2_2);
//        float distance2;
//        if (this.MapSceneWrapper.GetPathDistanceBetweenAIFaces(currentFaceRecord, faceIndex2, checkPosition, currentSettlementToLook.PortPosition.ToVec2(), 0.3f, maxDistanceToLookForPathDetection, out distance2, this.GetExcludedFaceIds(), this.GetRegionSwitchCostTo0(), this.GetRegionSwitchCostTo1()))
//        {
//          positionToSettlement = distance2;
//          isPort = true;
//          break;
//        }
//        break;
//      case MobileParty.NavigationType.All:
//        CampaignVec2 vec2_3 = currentSettlementToLook.GatePosition;
//        PathFaceRecord faceIndex3 = this.MapSceneWrapper.GetFaceIndex(in vec2_3);
//        float distance3;
//        if (this.MapSceneWrapper.GetPathDistanceBetweenAIFaces(currentFaceRecord, faceIndex3, checkPosition, currentSettlementToLook.GatePosition.ToVec2(), 0.3f, maxDistanceToLookForPathDetection, out distance3, this.GetExcludedFaceIds(), this.GetRegionSwitchCostTo0(), this.GetRegionSwitchCostTo1()))
//          positionToSettlement = distance3;
//        if (currentSettlementToLook.HasPort)
//        {
//          CampaignVec2 vec2_4 = currentSettlementToLook.PortPosition;
//          PathFaceRecord faceIndex4 = this.MapSceneWrapper.GetFaceIndex(in vec2_4);
//          float distance4;
//          if (this.MapSceneWrapper.GetPathDistanceBetweenAIFaces(currentFaceRecord, faceIndex4, checkPosition, currentSettlementToLook.PortPosition.ToVec2(), 0.3f, maxDistanceToLookForPathDetection, out distance4, this.GetExcludedFaceIds(), this.GetRegionSwitchCostTo0(), this.GetRegionSwitchCostTo1()) && (double) distance4 < (double) distance3)
//          {
//            positionToSettlement = distance4;
//            isPort = true;
//            break;
//          }
//          break;
//        }
//        break;
//    }
//    return positionToSettlement;
//  }

//  protected override IEnumerable<Settlement> GetClosestSettlementsToPositionInCache(
//    Vec2 checkPosition,
//    List<Settlement> settlements)
//  {
//    return this._navigationType == MobileParty.NavigationType.Naval ? (IEnumerable<Settlement>) settlements.Where<Settlement>((Func<Settlement, bool>) (x => x.HasPort)).OrderBy<Settlement, float>((Func<Settlement, float>) (x => checkPosition.DistanceSquared(x.PortPosition.ToVec2()))) : (IEnumerable<Settlement>) settlements.OrderBy<Settlement, float>((Func<Settlement, float>) (x => checkPosition.DistanceSquared(x.GatePosition.ToVec2())));
//  }

//  protected override List<Settlement> GetAllRegisteredSettlements()
//  {
//    return Settlement.All.ToList<Settlement>();
//  }

//  public void FinalizeInitialization() => this.FinalizeCacheInitialization();
//}
