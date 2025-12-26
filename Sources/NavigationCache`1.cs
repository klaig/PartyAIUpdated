//// Decompiled with JetBrains decompiler
//// Type: TaleWorlds.CampaignSystem.Map.DistanceCache.NavigationCache`1
//// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 21D5BF30-54A7-4DA5-81B3-31D796F5D6CE
//// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using TaleWorlds.CampaignSystem.Party;
//using TaleWorlds.Library;
//using TaleWorlds.ModuleManager;

//#nullable disable
//namespace TaleWorlds.CampaignSystem.Map.DistanceCache;

//public abstract class NavigationCache<T> where T : ISettlementDataHolder
//{
//  private Dictionary<NavigationCacheElement<T>, Dictionary<NavigationCacheElement<T>, (float, float)>> _settlementToSettlementDistanceWithLandRatio;
//  private Dictionary<T, MBReadOnlyList<T>> _fortificationNeighbors;
//  private Dictionary<int, NavigationCacheElement<T>> _closestSettlementsToFaceIndices;
//  protected const float AgentRadius = 0.3f;
//  protected const float ExtraCostMultiplierForNeighborDetection = 2f;

//  public float MaximumDistanceBetweenTwoConnectedSettlements { get; protected set; }

//  protected MobileParty.NavigationType _navigationType { get; private set; }

//  protected NavigationCache(MobileParty.NavigationType navigationType)
//  {
//    this._navigationType = navigationType;
//    this._settlementToSettlementDistanceWithLandRatio = new Dictionary<NavigationCacheElement<T>, Dictionary<NavigationCacheElement<T>, (float, float)>>();
//    this._fortificationNeighbors = new Dictionary<T, MBReadOnlyList<T>>();
//    this._closestSettlementsToFaceIndices = new Dictionary<int, NavigationCacheElement<T>>();
//  }

//  protected void FinalizeCacheInitialization()
//  {
//    if (this._fortificationNeighbors != null && !this._fortificationNeighbors.AnyQ<KeyValuePair<T, MBReadOnlyList<T>>>((Func<KeyValuePair<T, MBReadOnlyList<T>>, bool>) (x => x.Value.Count == 0)))
//      return;
//    Debug.FailedAssert("There is settlement with zero neighbor in neighbor cache, this should not be happening, check here", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Map\\DistanceCache\\NavigationCache.cs", nameof (FinalizeCacheInitialization), 44);
//    this.GenerateNeighborSettlementsCache();
//  }

//  public static void CopyTo<T1>(NavigationCache<T1> source, NavigationCache<T> target) where T1 : ISettlementDataHolder
//  {
//    target._navigationType = source._navigationType;
//    target.MaximumDistanceBetweenTwoConnectedSettlements = source.MaximumDistanceBetweenTwoConnectedSettlements;
//    target._settlementToSettlementDistanceWithLandRatio = new Dictionary<NavigationCacheElement<T>, Dictionary<NavigationCacheElement<T>, (float, float)>>(source._settlementToSettlementDistanceWithLandRatio.Count);
//    foreach (KeyValuePair<NavigationCacheElement<T1>, Dictionary<NavigationCacheElement<T1>, (float, float)>> keyValuePair1 in source._settlementToSettlementDistanceWithLandRatio)
//    {
//      NavigationCacheElement<T> cacheElement1 = target.GetCacheElement(target.GetCacheElement(keyValuePair1.Key.StringId), keyValuePair1.Key.IsPortUsed);
//      Dictionary<NavigationCacheElement<T>, (float, float)> dictionary = new Dictionary<NavigationCacheElement<T>, (float, float)>(keyValuePair1.Value.Count);
//      target._settlementToSettlementDistanceWithLandRatio.Add(cacheElement1, dictionary);
//      foreach (KeyValuePair<NavigationCacheElement<T1>, (float, float)> keyValuePair2 in keyValuePair1.Value)
//      {
//        NavigationCacheElement<T> cacheElement2 = target.GetCacheElement(target.GetCacheElement(keyValuePair2.Key.StringId), keyValuePair2.Key.IsPortUsed);
//        dictionary.Add(cacheElement2, keyValuePair2.Value);
//      }
//    }
//    target._fortificationNeighbors = new Dictionary<T, MBReadOnlyList<T>>(source._fortificationNeighbors.Count);
//    foreach (KeyValuePair<T1, MBReadOnlyList<T1>> fortificationNeighbor in source._fortificationNeighbors)
//    {
//      T cacheElement3 = target.GetCacheElement(fortificationNeighbor.Key.StringId);
//      List<T> source1 = new List<T>(fortificationNeighbor.Value.Count);
//      target._fortificationNeighbors.Add(cacheElement3, (MBReadOnlyList<T>) source1.ToMBList<T>());
//      foreach (T1 obj in (List<T1>) fortificationNeighbor.Value)
//      {
//        T cacheElement4 = target.GetCacheElement(obj.StringId);
//        source1.Add(cacheElement4);
//      }
//    }
//    target._closestSettlementsToFaceIndices = new Dictionary<int, NavigationCacheElement<T>>();
//    foreach (KeyValuePair<int, NavigationCacheElement<T1>> settlementsToFaceIndex in source._closestSettlementsToFaceIndices)
//    {
//      NavigationCacheElement<T> cacheElement = target.GetCacheElement(target.GetCacheElement(settlementsToFaceIndex.Value.StringId), settlementsToFaceIndex.Value.IsPortUsed);
//      target._closestSettlementsToFaceIndices.Add(settlementsToFaceIndex.Key, cacheElement);
//    }
//  }

//  public MBReadOnlyList<T> GetNeighbors(T settlement)
//  {
//    MBReadOnlyList<T> neighbors;
//    if (!this._fortificationNeighbors.TryGetValue(settlement, out neighbors))
//      neighbors = new MBReadOnlyList<T>();
//    return neighbors;
//  }

//  public T GetClosestSettlementToFaceIndex(int faceId, out bool isAtSea)
//  {
//    NavigationCacheElement<T> navigationCacheElement;
//    if (this._closestSettlementsToFaceIndices.TryGetValue(faceId, out navigationCacheElement))
//    {
//      isAtSea = navigationCacheElement.IsPortUsed;
//      return navigationCacheElement.Settlement;
//    }
//    isAtSea = false;
//    return default (T);
//  }

//  public void GenerateCacheData()
//  {
//    this.GenerateClosestSettlementToFaceCache();
//    this.GenerateSettlementToSettlementDistanceCache();
//    this.GenerateNeighborSettlementsCache();
//  }

//  protected float GetSettlementToSettlementDistanceWithLandRatio(
//    NavigationCacheElement<T> settlement1,
//    NavigationCacheElement<T> settlement2,
//    out float landRatio)
//  {
//    NavigationCacheElement<T>.Sort(ref settlement1, ref settlement2, out bool _);
//    Dictionary<NavigationCacheElement<T>, (float, float)> dictionary;
//    if (!this._settlementToSettlementDistanceWithLandRatio.TryGetValue(settlement1, out dictionary))
//    {
//      dictionary = new Dictionary<NavigationCacheElement<T>, (float, float)>();
//      this._settlementToSettlementDistanceWithLandRatio.Add(settlement1, dictionary);
//    }
//    (float, float) valueTuple;
//    if (!dictionary.TryGetValue(settlement2, out valueTuple))
//    {
//      float betweenSettlements = this.GetRealDistanceAndLandRatioBetweenSettlements(settlement1, settlement2, out landRatio);
//      this.SetSettlementToSettlementDistanceWithLandRatio(settlement1, settlement2, betweenSettlements, landRatio);
//      valueTuple = (betweenSettlements, landRatio);
//    }
//    landRatio = valueTuple.Item2;
//    return valueTuple.Item1;
//  }

//  protected void SetSettlementToSettlementDistanceWithLandRatio(
//    NavigationCacheElement<T> settlement1,
//    NavigationCacheElement<T> settlement2,
//    float distance,
//    float landRatio)
//  {
//    NavigationCacheElement<T>.Sort(ref settlement1, ref settlement2, out bool _);
//    Dictionary<NavigationCacheElement<T>, (float, float)> dictionary;
//    if (!this._settlementToSettlementDistanceWithLandRatio.TryGetValue(settlement1, out dictionary))
//    {
//      dictionary = new Dictionary<NavigationCacheElement<T>, (float, float)>();
//      this._settlementToSettlementDistanceWithLandRatio.Add(settlement1, dictionary);
//    }
//    if (dictionary.TryGetValue(settlement2, out (float, float) _))
//      Debug.FailedAssert("Element already exists", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Map\\DistanceCache\\NavigationCache.cs", nameof (SetSettlementToSettlementDistanceWithLandRatio), 215);
//    dictionary.Add(settlement2, (distance, landRatio));
//    if ((double) distance >= 100000000.0 || (double) distance <= (double) this.MaximumDistanceBetweenTwoConnectedSettlements)
//      return;
//    this.MaximumDistanceBetweenTwoConnectedSettlements = distance;
//  }

//  protected void AddNeighbor(T settlement1, T settlement2)
//  {
//    bool flag = false;
//    foreach (KeyValuePair<T, MBReadOnlyList<T>> fortificationNeighbor in this._fortificationNeighbors)
//    {
//      T key = fortificationNeighbor.Key;
//      if (!key.StringId.Equals(settlement1.StringId) || !fortificationNeighbor.Value.Contains(settlement2))
//      {
//        key = fortificationNeighbor.Key;
//        if (!key.StringId.Equals(settlement2.StringId) || !fortificationNeighbor.Value.Contains(settlement1))
//          continue;
//      }
//      flag = true;
//      break;
//    }
//    if (flag)
//      return;
//    MBReadOnlyList<T> collection1;
//    if (!this._fortificationNeighbors.TryGetValue(settlement1, out collection1))
//      this._fortificationNeighbors.Add(settlement1, new MBReadOnlyList<T>());
//    MBList<T> mbList1;
//    if (collection1 != null)
//    {
//      mbList1 = new MBList<T>(collection1.Count + 1);
//      mbList1.AddRange((IEnumerable<T>) collection1);
//    }
//    else
//      mbList1 = new MBList<T>(1);
//    mbList1.Add(settlement2);
//    this._fortificationNeighbors[settlement1] = (MBReadOnlyList<T>) mbList1;
//    MBReadOnlyList<T> collection2;
//    if (!this._fortificationNeighbors.TryGetValue(settlement2, out collection2))
//      this._fortificationNeighbors.Add(settlement2, new MBReadOnlyList<T>());
//    MBList<T> mbList2;
//    if (collection2 != null)
//    {
//      mbList2 = new MBList<T>(collection2.Count + 1);
//      mbList2.AddRange((IEnumerable<T>) collection2);
//    }
//    else
//      mbList2 = new MBList<T>(1);
//    mbList2.Add(settlement1);
//    this._fortificationNeighbors[settlement2] = (MBReadOnlyList<T>) mbList2;
//  }

//  protected void SetClosestSettlementToFaceIndex(int faceId, NavigationCacheElement<T> settlement)
//  {
//    this._closestSettlementsToFaceIndices.Add(faceId, settlement);
//  }

//  protected abstract float GetRealDistanceAndLandRatioBetweenSettlements(
//    NavigationCacheElement<T> settlement1,
//    NavigationCacheElement<T> settlement2,
//    out float landRatio);

//  protected abstract T GetCacheElement(string settlementId);

//  protected abstract NavigationCacheElement<T> GetCacheElement(T settlement, bool isPortUsed);

//  protected float GetLandRatioOfPath(NavigationPath path, Vec2 startPosition)
//  {
//    float num1 = 0.0f;
//    float num2 = 0.0f;
//    List<Vec2> vec2List = new List<Vec2>((IEnumerable<Vec2>) path.PathPoints);
//    vec2List.Insert(0, startPosition);
//    for (int index1 = 0; index1 < vec2List.Count - 1; ++index1)
//    {
//      Vec2 vec2_1 = vec2List[index1];
//      Vec2 vec2_2 = vec2List[index1 + 1];
//      if (!(vec2_2 == Vec2.Zero))
//      {
//        Vec2 vec2_3 = vec2_2 - vec2_1;
//        float num3 = vec2_3.Length / 0.5f;
//        double num4 = (double) vec2_3.Normalize();
//        for (int index2 = 0; (double) index2 < (double) num3 - 1.0; ++index2)
//        {
//          Vec2 position = vec2_1 + vec2_3 * (float) index2 * 0.5f;
//          Vec2 vec2_4 = vec2_1 + vec2_3 * (float) (index2 + 1) * 0.5f;
//          bool isOnRegion1_1;
//          this.GetFaceRecordForPoint(position, out isOnRegion1_1);
//          bool isOnRegion1_2;
//          this.GetFaceRecordForPoint(vec2_4, out isOnRegion1_2);
//          float num5 = position.Distance(vec2_4);
//          if (isOnRegion1_2 & isOnRegion1_1)
//            num1 += num5;
//          else if (isOnRegion1_2 != isOnRegion1_1)
//            num1 += num5 / 2f;
//          num2 += num5;
//        }
//      }
//      else
//        break;
//    }
//    if (vec2List.Count != 1)
//      return MBMath.ClampFloat(num1 / num2, 0.0f, 1f);
//    bool isOnRegion1;
//    this.GetFaceRecordForPoint(vec2List[0], out isOnRegion1);
//    return isOnRegion1 ? 1f : 0.0f;
//  }

//  protected abstract void GetFaceRecordForPoint(Vec2 position, out bool isOnRegion1);

//  protected void GenerateClosestSettlementToFaceCache()
//  {
//    int navMeshFaceCount = this.GetNavMeshFaceCount();
//    for (int index = 0; index < navMeshFaceCount; ++index)
//    {
//      Debug.Print($"Face-Settlement cache creation progress % {index * 100 / navMeshFaceCount}     {this._navigationType}");
//      Vec2 faceCenterPosition = this.GetNavMeshFaceCenterPosition(index);
//      PathFaceRecord faceRecordAtIndex = this.GetFaceRecordAtIndex(index);
//      bool isPort = false;
//      T settlementToPosition = this.GetClosestSettlementToPosition(faceCenterPosition, faceRecordAtIndex, this.GetExcludedFaceIds(), this.GetAllRegisteredSettlements(), this.GetRegionSwitchCostTo0(), this.GetRegionSwitchCostTo1(), float.MaxValue, out isPort);
//      if (!object.Equals((object) settlementToPosition, (object) default (T)))
//        this.SetClosestSettlementToFaceIndex(index, new NavigationCacheElement<T>(settlementToPosition, isPort));
//    }
//  }

//  protected abstract int GetNavMeshFaceCount();

//  protected abstract Vec2 GetNavMeshFaceCenterPosition(int faceIndex);

//  protected abstract PathFaceRecord GetFaceRecordAtIndex(int faceIndex);

//  protected abstract int[] GetExcludedFaceIds();

//  protected abstract int GetRegionSwitchCostTo0();

//  protected abstract int GetRegionSwitchCostTo1();

//  protected void GenerateSettlementToSettlementDistanceCache()
//  {
//    List<T> registeredSettlements = this.GetAllRegisteredSettlements();
//    for (int index1 = 0; index1 < registeredSettlements.Count; ++index1)
//    {
//      Debug.Print($"Settlement to settlement cache creation index {index1},    total count: {registeredSettlements.Count}     {this._navigationType}");
//      T settlement1 = registeredSettlements[index1];
//      for (int index2 = index1 + 1; index2 < registeredSettlements.Count; ++index2)
//      {
//        T settlement2 = registeredSettlements[index2];
//        if (this._navigationType == MobileParty.NavigationType.Default)
//          this.AddClosestEntrancePairBase(settlement1, false, settlement2, false);
//        else if (this._navigationType == MobileParty.NavigationType.Naval)
//        {
//          if (settlement1.HasPort && settlement2.HasPort)
//            this.AddClosestEntrancePairBase(settlement1, true, settlement2, true);
//        }
//        else if (this._navigationType == MobileParty.NavigationType.All)
//        {
//          this.AddClosestEntrancePairBase(settlement1, false, settlement2, false);
//          if (settlement1.HasPort && settlement2.HasPort)
//            this.AddClosestEntrancePairBase(settlement1, true, settlement2, true);
//          if (settlement2.HasPort)
//            this.AddClosestEntrancePairBase(settlement1, false, settlement2, true);
//          if (settlement1.HasPort)
//            this.AddClosestEntrancePairBase(settlement1, true, settlement2, false);
//        }
//      }
//    }
//  }

//  private void AddClosestEntrancePairBase(
//    T settlement1,
//    bool isPort1,
//    T settlement2,
//    bool isPort2)
//  {
//    NavigationCacheElement<T> cacheElement1 = this.GetCacheElement(settlement1, isPort1);
//    NavigationCacheElement<T> cacheElement2 = this.GetCacheElement(settlement2, isPort2);
//    float landRatio1;
//    float landRatio2;
//    float distance = (float) (((double) this.GetRealDistanceAndLandRatioBetweenSettlements(cacheElement1, cacheElement2, out landRatio1) + (double) this.GetRealDistanceAndLandRatioBetweenSettlements(cacheElement2, cacheElement1, out landRatio2)) * 0.5);
//    if ((double) distance <= 0.0)
//      return;
//    float landRatio3 = 1f;
//    if (this._navigationType == MobileParty.NavigationType.Naval)
//      landRatio3 = 0.0f;
//    else if (this._navigationType == MobileParty.NavigationType.All)
//      landRatio3 = landRatio1;
//    bool isPairChanged;
//    NavigationCacheElement<T>.Sort(ref cacheElement1, ref cacheElement2, out isPairChanged);
//    if (isPairChanged)
//      landRatio3 = landRatio2;
//    this.SetSettlementToSettlementDistanceWithLandRatio(cacheElement1, cacheElement2, distance, landRatio3);
//  }

//  protected void GenerateNeighborSettlementsCache()
//  {
//    this._fortificationNeighbors.Clear();
//    List<T> neighborDetection = this.GetUpdatedSettlementsForNeighborDetection(this.GetAllRegisteredSettlements());
//    for (int index1 = 0; index1 < neighborDetection.Count - 1; ++index1)
//    {
//      Debug.Print($"Neighbor cache progress for navigation {this._navigationType}, current index: {index1}  - total count: {neighborDetection.Count}");
//      T settlement1 = neighborDetection[index1];
//      if (settlement1.IsFortification)
//      {
//        for (int index2 = index1 + 1; index2 < neighborDetection.Count; ++index2)
//        {
//          T settlement2 = neighborDetection[index2];
//          if (settlement2.IsFortification && this.CheckBeingNeighbor(neighborDetection, settlement1, settlement2))
//            this.AddNeighbor(settlement1, settlement2);
//        }
//      }
//    }
//  }

//  private void CheckNeighbourAux(
//    List<T> settlementsToConsider,
//    T settlement1,
//    T settlement2,
//    bool useGate1,
//    bool useGate2,
//    ref float distance,
//    ref bool isNeighbour)
//  {
//    float foundDistance;
//    bool flag = this.CheckBeingNeighbor(settlementsToConsider, settlement1, settlement2, useGate1, useGate2, out foundDistance);
//    if ((double) foundDistance >= (double) distance)
//      return;
//    distance = foundDistance;
//    isNeighbour = flag;
//  }

//  protected bool CheckBeingNeighbor(List<T> settlementsToConsider, T settlement1, T settlement2)
//  {
//    float maxValue = float.MaxValue;
//    bool isNeighbour = false;
//    if (this._navigationType == MobileParty.NavigationType.Default || this._navigationType == MobileParty.NavigationType.All)
//    {
//      this.CheckNeighbourAux(settlementsToConsider, settlement1, settlement2, true, true, ref maxValue, ref isNeighbour);
//      this.CheckNeighbourAux(settlementsToConsider, settlement2, settlement1, true, true, ref maxValue, ref isNeighbour);
//    }
//    if (this._navigationType == MobileParty.NavigationType.Naval || this._navigationType == MobileParty.NavigationType.All)
//    {
//      bool hasPort = settlement1.HasPort;
//      int num = settlement2.HasPort ? 1 : 0;
//      if (hasPort)
//      {
//        this.CheckNeighbourAux(settlementsToConsider, settlement1, settlement2, false, true, ref maxValue, ref isNeighbour);
//        this.CheckNeighbourAux(settlementsToConsider, settlement2, settlement1, true, false, ref maxValue, ref isNeighbour);
//      }
//      if (num != 0)
//      {
//        this.CheckNeighbourAux(settlementsToConsider, settlement1, settlement2, true, false, ref maxValue, ref isNeighbour);
//        this.CheckNeighbourAux(settlementsToConsider, settlement2, settlement1, false, true, ref maxValue, ref isNeighbour);
//      }
//      if ((num & (hasPort ? 1 : 0)) != 0)
//      {
//        this.CheckNeighbourAux(settlementsToConsider, settlement1, settlement2, false, false, ref maxValue, ref isNeighbour);
//        this.CheckNeighbourAux(settlementsToConsider, settlement2, settlement1, false, false, ref maxValue, ref isNeighbour);
//      }
//    }
//    return isNeighbour;
//  }

//  protected abstract List<T> GetAllRegisteredSettlements();

//  protected List<T> GetUpdatedSettlementsForNeighborDetection(List<T> settlements)
//  {
//    return this._navigationType == MobileParty.NavigationType.Naval ? settlements.Where<T>((Func<T, bool>) (x => x.IsFortification && x.HasPort)).ToList<T>() : settlements.Where<T>((Func<T, bool>) (x => x.IsFortification)).ToList<T>();
//  }

//  protected abstract bool CheckBeingNeighbor(
//    List<T> settlementsToConsider,
//    T settlement1,
//    T settlement2,
//    bool useGate1,
//    bool useGate2,
//    out float foundDistance);

//  protected abstract float GetRealPathDistanceFromPositionToSettlement(
//    Vec2 checkPosition,
//    PathFaceRecord currentFaceRecord,
//    float maxDistanceToLookForPathDetection,
//    T currentSettlementToLook,
//    out bool isPort);

//  protected T GetClosestSettlementToPosition(
//    Vec2 checkPosition,
//    PathFaceRecord currentFaceRecord,
//    int[] excludedFaceIds,
//    List<T> settlementRecords,
//    int regionSwitchCostTo0,
//    int regionSwitchCostTo1,
//    float minPathScoreEverFound,
//    out bool isPort)
//  {
//    isPort = false;
//    T settlementToPosition = default (T);
//    foreach (T currentSettlementToLook in this.GetClosestSettlementsToPositionInCache(checkPosition, settlementRecords))
//    {
//      bool isPort1;
//      float positionToSettlement = this.GetRealPathDistanceFromPositionToSettlement(checkPosition, currentFaceRecord, minPathScoreEverFound * 2f, currentSettlementToLook, out isPort1);
//      if ((double) positionToSettlement < (double) minPathScoreEverFound)
//      {
//        minPathScoreEverFound = positionToSettlement;
//        settlementToPosition = currentSettlementToLook;
//        isPort = isPort1;
//      }
//    }
//    return settlementToPosition;
//  }

//  protected abstract IEnumerable<T> GetClosestSettlementsToPositionInCache(
//    Vec2 checkPosition,
//    List<T> settlements);

//  public abstract void GetSceneXmlCrcValues(out uint sceneXmlCrc, out uint sceneNavigationMeshCrc);

//  public bool GetSettlementsDistanceCacheFileForCapability(string moduleId, out string filePath)
//  {
//    string str1 = ModuleHelper.GetModuleFullPath(moduleId) + "ModuleData/DistanceCaches";
//    string str2 = this._navigationType.ToString();
//    filePath = $"{str1}/settlements_distance_cache_{str2}.bin";
//    int num = File.Exists(filePath) ? 1 : 0;
//    if (num == 0)
//      return num != 0;
//    Debug.Print($"Found distance cache at: {moduleId}, {str1}, {this._navigationType}");
//    return num != 0;
//  }

//  public void Serialize(string path)
//  {
//    System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter((Stream) File.Open(path, FileMode.Create));
//    uint sceneXmlCrc;
//    uint sceneNavigationMeshCrc;
//    this.GetSceneXmlCrcValues(out sceneXmlCrc, out sceneNavigationMeshCrc);
//    binaryWriter.Write(sceneXmlCrc);
//    binaryWriter.Write(sceneNavigationMeshCrc);
//    binaryWriter.Write(this._settlementToSettlementDistanceWithLandRatio.Count);
//    foreach (KeyValuePair<NavigationCacheElement<T>, Dictionary<NavigationCacheElement<T>, (float, float)>> keyValuePair1 in this._settlementToSettlementDistanceWithLandRatio)
//    {
//      binaryWriter.Write(keyValuePair1.Key.StringId);
//      binaryWriter.Write(keyValuePair1.Key.IsPortUsed);
//      binaryWriter.Write(keyValuePair1.Value.Count);
//      foreach (KeyValuePair<NavigationCacheElement<T>, (float, float)> keyValuePair2 in keyValuePair1.Value)
//      {
//        binaryWriter.Write(keyValuePair2.Key.StringId);
//        binaryWriter.Write(keyValuePair2.Key.IsPortUsed);
//        binaryWriter.Write(keyValuePair2.Value.Item1);
//        if (this._navigationType == MobileParty.NavigationType.All)
//          binaryWriter.Write(keyValuePair2.Value.Item2);
//      }
//    }
//    binaryWriter.Write(this._fortificationNeighbors.SumQ<KeyValuePair<T, MBReadOnlyList<T>>>((Func<KeyValuePair<T, MBReadOnlyList<T>>, int>) (x => x.Value.Count)));
//    foreach (KeyValuePair<T, MBReadOnlyList<T>> fortificationNeighbor in this._fortificationNeighbors)
//    {
//      string stringId = fortificationNeighbor.Key.StringId;
//      foreach (T obj in (List<T>) fortificationNeighbor.Value)
//      {
//        binaryWriter.Write(stringId);
//        binaryWriter.Write(obj.StringId);
//      }
//    }
//    binaryWriter.Write(this._closestSettlementsToFaceIndices.Count);
//    foreach (KeyValuePair<int, NavigationCacheElement<T>> settlementsToFaceIndex in this._closestSettlementsToFaceIndices)
//    {
//      binaryWriter.Write(settlementsToFaceIndex.Key);
//      binaryWriter.Write(settlementsToFaceIndex.Value.StringId);
//      binaryWriter.Write(settlementsToFaceIndex.Value.IsPortUsed);
//    }
//    binaryWriter.Close();
//  }

//  public void Deserialize(string path)
//  {
//    Debug.Print("Reading SettlementsDistanceCacheFilePath: " + path);
//    System.IO.BinaryReader binaryReader = new System.IO.BinaryReader((Stream) File.Open(path, FileMode.Open, FileAccess.Read));
//    int num1 = (int) binaryReader.ReadUInt32();
//    int num2 = (int) binaryReader.ReadUInt32();
//    int sceneXmlCrc = (int) Campaign.Current.MapSceneWrapper.GetSceneXmlCrc();
//    int navigationMeshCrc = (int) Campaign.Current.MapSceneWrapper.GetSceneNavigationMeshCrc();
//    int capacity1 = binaryReader.ReadInt32();
//    this._settlementToSettlementDistanceWithLandRatio = new Dictionary<NavigationCacheElement<T>, Dictionary<NavigationCacheElement<T>, (float, float)>>(capacity1);
//    for (int index1 = 0; index1 < capacity1; ++index1)
//    {
//      NavigationCacheElement<T> cacheElement1 = this.GetCacheElement(this.GetCacheElement(binaryReader.ReadString()), binaryReader.ReadBoolean());
//      int capacity2 = binaryReader.ReadInt32();
//      this._settlementToSettlementDistanceWithLandRatio.Add(cacheElement1, new Dictionary<NavigationCacheElement<T>, (float, float)>(capacity2));
//      for (int index2 = 0; index2 < capacity2; ++index2)
//      {
//        NavigationCacheElement<T> cacheElement2 = this.GetCacheElement(this.GetCacheElement(binaryReader.ReadString()), binaryReader.ReadBoolean());
//        NavigationCacheElement<T>.Sort(ref cacheElement1, ref cacheElement2, out bool _);
//        float distance = binaryReader.ReadSingle();
//        float landRatio = this._navigationType == MobileParty.NavigationType.Naval ? 0.0f : 1f;
//        if (this._navigationType == MobileParty.NavigationType.All)
//          landRatio = binaryReader.ReadSingle();
//        this.SetSettlementToSettlementDistanceWithLandRatio(cacheElement1, cacheElement2, distance, landRatio);
//      }
//    }
//    int capacity3 = binaryReader.ReadInt32();
//    this._fortificationNeighbors = new Dictionary<T, MBReadOnlyList<T>>(capacity3);
//    for (int index = 0; index < capacity3; ++index)
//      this.AddNeighbor(this.GetCacheElement(binaryReader.ReadString()), this.GetCacheElement(binaryReader.ReadString()));
//    int capacity4 = binaryReader.ReadInt32();
//    this._closestSettlementsToFaceIndices = new Dictionary<int, NavigationCacheElement<T>>(capacity4);
//    for (int index = 0; index < capacity4; ++index)
//      this.SetClosestSettlementToFaceIndex(binaryReader.ReadInt32(), this.GetCacheElement(this.GetCacheElement(binaryReader.ReadString()), binaryReader.ReadBoolean()));
//    binaryReader.Close();
//  }
//}
