//// Decompiled with JetBrains decompiler
//// Type: TaleWorlds.CampaignSystem.Map.DistanceCache.NavigationCacheElement`1
//// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 038459B8-4640-4714-AE67-6181A9569366
//// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll

//using System;
//using System.Collections.Generic;
//using TaleWorlds.Library;

//#nullable disable
//namespace TaleWorlds.CampaignSystem.Map.DistanceCache;

//public readonly struct NavigationCacheElement<T>(T settlement, bool isPortUsed) : 
//  IEquatable<NavigationCacheElement<T>>
//  where T : ISettlementDataHolder
//{
//  public readonly T Settlement = settlement;
//  public readonly bool IsPortUsed = isPortUsed;

//  public CampaignVec2 PortPosition => this.Settlement.PortPosition;

//  public CampaignVec2 GatePosition => this.Settlement.GatePosition;

//  public string StringId => this.Settlement.StringId;

//  public static void Sort(
//    ref NavigationCacheElement<T> settlement1,
//    ref NavigationCacheElement<T> settlement2,
//    out bool isPairChanged)
//  {
//    isPairChanged = false;
//    int num = string.Compare(settlement1.StringId, settlement2.StringId, StringComparison.Ordinal);
//    if (num < 0 || num == 0 && settlement1.IsPortUsed)
//      return;
//    NavigationCacheElement<T> navigationCacheElement1 = settlement2;
//    NavigationCacheElement<T> navigationCacheElement2 = settlement1;
//    settlement1 = navigationCacheElement1;
//    settlement2 = navigationCacheElement2;
//    isPairChanged = true;
//  }

//  public override int GetHashCode()
//  {
//    return this.StringId.GetDeterministicHashCode() * 2 + (this.IsPortUsed ? 1 : 0);
//  }

//  public override bool Equals(object obj)
//  {
//    return obj is NavigationCacheElement<T> navigationCacheElement && this.StringId == navigationCacheElement.StringId && this.IsPortUsed == navigationCacheElement.IsPortUsed;
//  }

//  public bool Equals(NavigationCacheElement<T> other)
//  {
//    return EqualityComparer<T>.Default.Equals(this.Settlement, other.Settlement) && this.IsPortUsed == other.IsPortUsed;
//  }

//  public static bool operator ==(NavigationCacheElement<T> left, NavigationCacheElement<T> right)
//  {
//    return left.Equals(right);
//  }

//  public static bool operator !=(NavigationCacheElement<T> left, NavigationCacheElement<T> right)
//  {
//    return !left.Equals(right);
//  }
//}
