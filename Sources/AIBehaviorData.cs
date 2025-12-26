//// Decompiled with JetBrains decompiler
//// Type: TaleWorlds.CampaignSystem.AIBehaviorData
//// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 21D5BF30-54A7-4DA5-81B3-31D796F5D6CE
//// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll

//using System;
//using TaleWorlds.CampaignSystem.Map;
//using TaleWorlds.CampaignSystem.Party;

//#nullable disable
//namespace TaleWorlds.CampaignSystem;

//public struct AIBehaviorData : IEquatable<AIBehaviorData>
//{
//  public static readonly AIBehaviorData Invalid = new AIBehaviorData((IMapPoint) null, AiBehavior.None, MobileParty.NavigationType.None, false, false, false);
//  public IMapPoint Party;
//  public CampaignVec2 Position;
//  public AiBehavior AiBehavior;
//  public bool WillGatherArmy;
//  public bool IsFromPort;
//  public bool IsTargetingPort;
//  public MobileParty.NavigationType NavigationType;

//  public AIBehaviorData(
//    IMapPoint party,
//    AiBehavior aiBehavior,
//    MobileParty.NavigationType navigationType,
//    bool willGatherArmy,
//    bool isFromPort,
//    bool isTargetingPort)
//  {
//    this.Party = party;
//    this.AiBehavior = aiBehavior;
//    this.NavigationType = navigationType;
//    this.WillGatherArmy = willGatherArmy;
//    this.IsFromPort = isFromPort;
//    this.IsTargetingPort = isTargetingPort;
//    this.Position = CampaignVec2.Zero;
//  }

//  public AIBehaviorData(
//    CampaignVec2 position,
//    AiBehavior aiBehavior,
//    MobileParty.NavigationType navigationType,
//    bool willGatherArmy,
//    bool isFromPort,
//    bool isTargetingPort)
//  {
//    this.Position = position;
//    this.Party = (IMapPoint) null;
//    this.AiBehavior = aiBehavior;
//    this.NavigationType = navigationType;
//    this.WillGatherArmy = willGatherArmy;
//    this.IsFromPort = isFromPort;
//    this.IsTargetingPort = isTargetingPort;
//  }

//  public override bool Equals(object obj)
//  {
//    return obj is AIBehaviorData aiBehaviorData && aiBehaviorData == this;
//  }

//  public bool Equals(AIBehaviorData other) => other == this;

//  public override int GetHashCode()
//  {
//    int hashCode = ((int) this.AiBehavior).GetHashCode();
//    return (((((this.Party != null ? hashCode * 397 ^ this.Party.GetHashCode() : hashCode) * 397 ^ this.WillGatherArmy.GetHashCode()) * 397 ^ this.IsTargetingPort.GetHashCode()) * 397 ^ this.IsFromPort.GetHashCode()) * 397 ^ this.NavigationType.GetHashCode()) * 397 ^ this.Position.GetHashCode();
//  }

//  public static bool operator ==(AIBehaviorData a, AIBehaviorData b)
//  {
//    return a.Party == b.Party && a.AiBehavior == b.AiBehavior && a.NavigationType == b.NavigationType && a.WillGatherArmy == b.WillGatherArmy && a.IsFromPort == b.IsFromPort && a.IsTargetingPort == b.IsTargetingPort && a.Position == b.Position;
//  }

//  public static bool operator !=(AIBehaviorData a, AIBehaviorData b) => !(a == b);
//}
