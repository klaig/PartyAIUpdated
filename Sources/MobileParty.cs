//// Decompiled with JetBrains decompiler
//// Type: TaleWorlds.CampaignSystem.Party.MobileParty
//// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 038459B8-4640-4714-AE67-6181A9569366
//// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll

//using Helpers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Threading;
//using TaleWorlds.CampaignSystem.Actions;
//using TaleWorlds.CampaignSystem.CharacterDevelopment;
//using TaleWorlds.CampaignSystem.ComponentInterfaces;
//using TaleWorlds.CampaignSystem.GameState;
//using TaleWorlds.CampaignSystem.Map;
//using TaleWorlds.CampaignSystem.MapEvents;
//using TaleWorlds.CampaignSystem.Naval;
//using TaleWorlds.CampaignSystem.Party.PartyComponents;
//using TaleWorlds.CampaignSystem.Roster;
//using TaleWorlds.CampaignSystem.Settlements;
//using TaleWorlds.CampaignSystem.Siege;
//using TaleWorlds.Core;
//using TaleWorlds.Library;
//using TaleWorlds.Localization;
//using TaleWorlds.ObjectSystem;
//using TaleWorlds.SaveSystem;
//using TaleWorlds.SaveSystem.Load;

//#nullable disable
//namespace TaleWorlds.CampaignSystem.Party;

//public sealed class MobileParty : 
//  CampaignObjectBase,
//  ILocatable<MobileParty>,
//  IMapPoint,
//  ITrackableCampaignObject,
//  ITrackableBase,
//  IRandomOwner
//{
//  public const int DefaultPartyTradeInitialGold = 5000;
//  public const int ClanRoleAssignmentMinimumSkillValue = 0;
//  public const int MinimumSpareGoldForWageBudget = 5;
//  private const int ArrayLength = 6;
//  [SaveableField(1001)]
//  private Settlement _currentSettlement;
//  [CachedData]
//  private MBList<MobileParty> _attachedParties;
//  [SaveableField(1046)]
//  private MobileParty _attachedTo;
//  [SaveableField(1006)]
//  public float HasUnpaidWages;
//  [SaveableField(1060)]
//  private Vec2 _eventPositionAdder;
//  [SaveableField(1026)]
//  private bool _isInRaftState;
//  [SaveableField(1040)]
//  public bool IsInfoHidden;
//  [SaveableField(1024 /*0x0400*/)]
//  private bool _isVisible;
//  [CachedData]
//  internal float _lastCalculatedSpeed = 1f;
//  [CachedData]
//  public CampaignVec2 NextLongTermPathPoint = CampaignVec2.Invalid;
//  [SaveableField(1025)]
//  private bool _isInspected;
//  [SaveableField(1955)]
//  private CampaignTime _disorganizedUntilTime;
//  [CachedData]
//  private int _partyPureSpeedLastCheckVersion = -1;
//  [CachedData]
//  private bool _partyLastCheckIsPrisoner;
//  [CachedData]
//  private ExplainedNumber _lastCalculatedBaseSpeedExplained;
//  [CachedData]
//  private bool _partyLastCheckAtNight;
//  [CachedData]
//  private int _itemRosterVersionNo = -1;
//  [CachedData]
//  private int _partySizeRatioLastCheckVersion = -1;
//  [CachedData]
//  private int _partyWeightLastCheckVersionNo = -1;
//  [CachedData]
//  private float _cachedPartySizeRatio = 1f;
//  [SaveableField(1059)]
//  private BesiegerCamp _besiegerCamp;
//  [SaveableField(1048)]
//  private MobileParty _targetParty;
//  [SaveableField(1049)]
//  private Settlement _targetSettlement;
//  [SaveableField(224 /*0xE0*/)]
//  private CampaignVec2 _targetPosition;
//  private int _doNotAttackMainParty;
//  [SaveableField(1034)]
//  private Settlement _customHomeSettlement;
//  [SaveableField(1035)]
//  private Army _army;
//  [CachedData]
//  private bool _isDisorganized;
//  [SaveableField(1959)]
//  private bool _isCurrentlyUsedByAQuest;
//  [SaveableField(1956)]
//  private int _partyTradeGold;
//  [SaveableField(1063)]
//  private CampaignTime _ignoredUntilTime;
//  [SaveableField(1071)]
//  public Vec2 AverageFleeTargetDirection;
//  private bool _besiegerCampResetStarted;
//  [SaveableField(211)]
//  private bool _pathMode;
//  [SaveableField(220)]
//  private CampaignVec2 _pathLastPosition;
//  [SaveableField(221)]
//  public CampaignVec2 NextTargetPosition;
//  [SaveableField(222)]
//  public CampaignVec2 MoveTargetPoint;
//  [SaveableField(215)]
//  public MoveModeType PartyMoveMode;
//  [SaveableField(216)]
//  private Vec2 _formationPosition;
//  [SaveableField(217)]
//  public MobileParty MoveTargetParty;
//  [SaveableField(218)]
//  private AiBehavior _defaultBehavior;
//  [SaveableField(1110)]
//  private bool _isCurrentlyAtSea;
//  [SaveableField(1094)]
//  private bool _isTargetingPort;
//  public bool StartTransitionNextFrameToExitFromPort;
//  [SaveableField(1096)]
//  private CampaignTime _navigationTransitionStartTime;
//  [SaveableField(1093)]
//  private MobileParty.NavigationType _desiredAiNavigationType;
//  [CachedData]
//  private int _locatorNodeIndex;
//  [SaveableField(1120)]
//  private Clan _actualClan;
//  [SaveableField(1200)]
//  private float _moraleDueToEvents;
//  [CachedData]
//  private float _totalWeightCarriedCache;
//  [CachedData]
//  private PathFaceRecord _lastNavigationFace;
//  [CachedData]
//  private MapWeatherModel.WeatherEventEffectOnTerrain _lastWeatherTerrainEffect;
//  [CachedData]
//  private bool _aiPathNotFound;
//  [CachedData]
//  public NavigationPath Path;
//  [CachedData]
//  public PathFaceRecord PathLastFace;
//  [CachedData]
//  private MobileParty.NavigationType _lastComputedPathNavigationType;
//  [SaveableField(225)]
//  private CampaignVec2 _position;
//  private Vec2 _lastWind;
//  [SaveableField(210)]
//  private PartyComponent _partyComponent;

//  internal static void AutoGeneratedStaticCollectObjectsMobileParty(
//    object o,
//    List<object> collectedObjects)
//  {
//    ((MBObjectBase) o).AutoGeneratedInstanceCollectObjects(collectedObjects);
//  }

//  protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
//  {
//    base.AutoGeneratedInstanceCollectObjects(collectedObjects);
//    CampaignVec2.AutoGeneratedStaticCollectObjectsCampaignVec2((object) this.NextTargetPosition, collectedObjects);
//    CampaignVec2.AutoGeneratedStaticCollectObjectsCampaignVec2((object) this.MoveTargetPoint, collectedObjects);
//    collectedObjects.Add((object) this.MoveTargetParty);
//    collectedObjects.Add((object) this._currentSettlement);
//    collectedObjects.Add((object) this._attachedTo);
//    CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime((object) this._disorganizedUntilTime, collectedObjects);
//    collectedObjects.Add((object) this._besiegerCamp);
//    collectedObjects.Add((object) this._targetParty);
//    collectedObjects.Add((object) this._targetSettlement);
//    CampaignVec2.AutoGeneratedStaticCollectObjectsCampaignVec2((object) this._targetPosition, collectedObjects);
//    collectedObjects.Add((object) this._customHomeSettlement);
//    collectedObjects.Add((object) this._army);
//    CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime((object) this._ignoredUntilTime, collectedObjects);
//    CampaignVec2.AutoGeneratedStaticCollectObjectsCampaignVec2((object) this._pathLastPosition, collectedObjects);
//    CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime((object) this._navigationTransitionStartTime, collectedObjects);
//    collectedObjects.Add((object) this._actualClan);
//    CampaignVec2.AutoGeneratedStaticCollectObjectsCampaignVec2((object) this._position, collectedObjects);
//    collectedObjects.Add((object) this._partyComponent);
//    collectedObjects.Add((object) this.LastVisitedSettlement);
//    collectedObjects.Add((object) this.Ai);
//    collectedObjects.Add((object) this.Party);
//    CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime((object) this.StationaryStartTime, collectedObjects);
//    collectedObjects.Add((object) this.Anchor);
//    CampaignVec2.AutoGeneratedStaticCollectObjectsCampaignVec2((object) this.EndPositionForNavigationTransition, collectedObjects);
//    CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime((object) this.NavigationTransitionDuration, collectedObjects);
//    collectedObjects.Add((object) this.Scout);
//    collectedObjects.Add((object) this.Engineer);
//    collectedObjects.Add((object) this.Quartermaster);
//    collectedObjects.Add((object) this.Surgeon);
//  }

//  internal static object AutoGeneratedGetMemberValueLastVisitedSettlement(object o)
//  {
//    return (object) ((MobileParty) o).LastVisitedSettlement;
//  }

//  internal static object AutoGeneratedGetMemberValueBearing(object o)
//  {
//    return (object) ((MobileParty) o).Bearing;
//  }

//  internal static object AutoGeneratedGetMemberValueHasLandNavigationCapability(object o)
//  {
//    return (object) ((MobileParty) o).HasLandNavigationCapability;
//  }

//  internal static object AutoGeneratedGetMemberValueAggressiveness(object o)
//  {
//    return (object) ((MobileParty) o).Aggressiveness;
//  }

//  internal static object AutoGeneratedGetMemberValueArmyPositionAdder(object o)
//  {
//    return (object) ((MobileParty) o).ArmyPositionAdder;
//  }

//  internal static object AutoGeneratedGetMemberValueObjective(object o)
//  {
//    return (object) ((MobileParty) o).Objective;
//  }

//  internal static object AutoGeneratedGetMemberValueAi(object o) => (object) ((MobileParty) o).Ai;

//  internal static object AutoGeneratedGetMemberValueParty(object o)
//  {
//    return (object) ((MobileParty) o).Party;
//  }

//  internal static object AutoGeneratedGetMemberValueIsActive(object o)
//  {
//    return (object) ((MobileParty) o).IsActive;
//  }

//  internal static object AutoGeneratedGetMemberValueShortTermBehavior(object o)
//  {
//    return (object) ((MobileParty) o).ShortTermBehavior;
//  }

//  internal static object AutoGeneratedGetMemberValueIsPartyTradeActive(object o)
//  {
//    return (object) ((MobileParty) o).IsPartyTradeActive;
//  }

//  internal static object AutoGeneratedGetMemberValuePartyTradeTaxGold(object o)
//  {
//    return (object) ((MobileParty) o).PartyTradeTaxGold;
//  }

//  internal static object AutoGeneratedGetMemberValueStationaryStartTime(object o)
//  {
//    return (object) ((MobileParty) o).StationaryStartTime;
//  }

//  internal static object AutoGeneratedGetMemberValueShouldJoinPlayerBattles(object o)
//  {
//    return (object) ((MobileParty) o).ShouldJoinPlayerBattles;
//  }

//  internal static object AutoGeneratedGetMemberValueIsDisbanding(object o)
//  {
//    return (object) ((MobileParty) o).IsDisbanding;
//  }

//  internal static object AutoGeneratedGetMemberValueAnchor(object o)
//  {
//    return (object) ((MobileParty) o).Anchor;
//  }

//  internal static object AutoGeneratedGetMemberValueEndPositionForNavigationTransition(object o)
//  {
//    return (object) ((MobileParty) o).EndPositionForNavigationTransition;
//  }

//  internal static object AutoGeneratedGetMemberValueNavigationTransitionDuration(object o)
//  {
//    return (object) ((MobileParty) o).NavigationTransitionDuration;
//  }

//  internal static object AutoGeneratedGetMemberValueScout(object o)
//  {
//    return (object) ((MobileParty) o).Scout;
//  }

//  internal static object AutoGeneratedGetMemberValueEngineer(object o)
//  {
//    return (object) ((MobileParty) o).Engineer;
//  }

//  internal static object AutoGeneratedGetMemberValueQuartermaster(object o)
//  {
//    return (object) ((MobileParty) o).Quartermaster;
//  }

//  internal static object AutoGeneratedGetMemberValueSurgeon(object o)
//  {
//    return (object) ((MobileParty) o).Surgeon;
//  }

//  internal static object AutoGeneratedGetMemberValueHasUnpaidWages(object o)
//  {
//    return (object) ((MobileParty) o).HasUnpaidWages;
//  }

//  internal static object AutoGeneratedGetMemberValueIsInfoHidden(object o)
//  {
//    return (object) ((MobileParty) o).IsInfoHidden;
//  }

//  internal static object AutoGeneratedGetMemberValueAverageFleeTargetDirection(object o)
//  {
//    return (object) ((MobileParty) o).AverageFleeTargetDirection;
//  }

//  internal static object AutoGeneratedGetMemberValueNextTargetPosition(object o)
//  {
//    return (object) ((MobileParty) o).NextTargetPosition;
//  }

//  internal static object AutoGeneratedGetMemberValueMoveTargetPoint(object o)
//  {
//    return (object) ((MobileParty) o).MoveTargetPoint;
//  }

//  internal static object AutoGeneratedGetMemberValuePartyMoveMode(object o)
//  {
//    return (object) ((MobileParty) o).PartyMoveMode;
//  }

//  internal static object AutoGeneratedGetMemberValueMoveTargetParty(object o)
//  {
//    return (object) ((MobileParty) o).MoveTargetParty;
//  }

//  internal static object AutoGeneratedGetMemberValue_currentSettlement(object o)
//  {
//    return (object) ((MobileParty) o)._currentSettlement;
//  }

//  internal static object AutoGeneratedGetMemberValue_attachedTo(object o)
//  {
//    return (object) ((MobileParty) o)._attachedTo;
//  }

//  internal static object AutoGeneratedGetMemberValue_eventPositionAdder(object o)
//  {
//    return (object) ((MobileParty) o)._eventPositionAdder;
//  }

//  internal static object AutoGeneratedGetMemberValue_isInRaftState(object o)
//  {
//    return (object) ((MobileParty) o)._isInRaftState;
//  }

//  internal static object AutoGeneratedGetMemberValue_isVisible(object o)
//  {
//    return (object) ((MobileParty) o)._isVisible;
//  }

//  internal static object AutoGeneratedGetMemberValue_isInspected(object o)
//  {
//    return (object) ((MobileParty) o)._isInspected;
//  }

//  internal static object AutoGeneratedGetMemberValue_disorganizedUntilTime(object o)
//  {
//    return (object) ((MobileParty) o)._disorganizedUntilTime;
//  }

//  internal static object AutoGeneratedGetMemberValue_besiegerCamp(object o)
//  {
//    return (object) ((MobileParty) o)._besiegerCamp;
//  }

//  internal static object AutoGeneratedGetMemberValue_targetParty(object o)
//  {
//    return (object) ((MobileParty) o)._targetParty;
//  }

//  internal static object AutoGeneratedGetMemberValue_targetSettlement(object o)
//  {
//    return (object) ((MobileParty) o)._targetSettlement;
//  }

//  internal static object AutoGeneratedGetMemberValue_targetPosition(object o)
//  {
//    return (object) ((MobileParty) o)._targetPosition;
//  }

//  internal static object AutoGeneratedGetMemberValue_customHomeSettlement(object o)
//  {
//    return (object) ((MobileParty) o)._customHomeSettlement;
//  }

//  internal static object AutoGeneratedGetMemberValue_army(object o)
//  {
//    return (object) ((MobileParty) o)._army;
//  }

//  internal static object AutoGeneratedGetMemberValue_isCurrentlyUsedByAQuest(object o)
//  {
//    return (object) ((MobileParty) o)._isCurrentlyUsedByAQuest;
//  }

//  internal static object AutoGeneratedGetMemberValue_partyTradeGold(object o)
//  {
//    return (object) ((MobileParty) o)._partyTradeGold;
//  }

//  internal static object AutoGeneratedGetMemberValue_ignoredUntilTime(object o)
//  {
//    return (object) ((MobileParty) o)._ignoredUntilTime;
//  }

//  internal static object AutoGeneratedGetMemberValue_pathMode(object o)
//  {
//    return (object) ((MobileParty) o)._pathMode;
//  }

//  internal static object AutoGeneratedGetMemberValue_pathLastPosition(object o)
//  {
//    return (object) ((MobileParty) o)._pathLastPosition;
//  }

//  internal static object AutoGeneratedGetMemberValue_formationPosition(object o)
//  {
//    return (object) ((MobileParty) o)._formationPosition;
//  }

//  internal static object AutoGeneratedGetMemberValue_defaultBehavior(object o)
//  {
//    return (object) ((MobileParty) o)._defaultBehavior;
//  }

//  internal static object AutoGeneratedGetMemberValue_isCurrentlyAtSea(object o)
//  {
//    return (object) ((MobileParty) o)._isCurrentlyAtSea;
//  }

//  internal static object AutoGeneratedGetMemberValue_isTargetingPort(object o)
//  {
//    return (object) ((MobileParty) o)._isTargetingPort;
//  }

//  internal static object AutoGeneratedGetMemberValue_navigationTransitionStartTime(object o)
//  {
//    return (object) ((MobileParty) o)._navigationTransitionStartTime;
//  }

//  internal static object AutoGeneratedGetMemberValue_desiredAiNavigationType(object o)
//  {
//    return (object) ((MobileParty) o)._desiredAiNavigationType;
//  }

//  internal static object AutoGeneratedGetMemberValue_actualClan(object o)
//  {
//    return (object) ((MobileParty) o)._actualClan;
//  }

//  internal static object AutoGeneratedGetMemberValue_moraleDueToEvents(object o)
//  {
//    return (object) ((MobileParty) o)._moraleDueToEvents;
//  }

//  internal static object AutoGeneratedGetMemberValue_position(object o)
//  {
//    return (object) ((MobileParty) o)._position;
//  }

//  internal static object AutoGeneratedGetMemberValue_partyComponent(object o)
//  {
//    return (object) ((MobileParty) o)._partyComponent;
//  }

//  public static MobileParty MainParty => Campaign.Current.MainParty;

//  public static MBReadOnlyList<MobileParty> All => Campaign.Current.MobileParties;

//  public static MBReadOnlyList<MobileParty> AllCaravanParties => Campaign.Current.CaravanParties;

//  public static MBReadOnlyList<MobileParty> AllPatrolParties => Campaign.Current.PatrolParties;

//  public static MBReadOnlyList<MobileParty> AllBanditParties => Campaign.Current.BanditParties;

//  public static MBReadOnlyList<MobileParty> AllLordParties => Campaign.Current.LordParties;

//  public static MBReadOnlyList<MobileParty> AllGarrisonParties => Campaign.Current.GarrisonParties;

//  public static MBReadOnlyList<MobileParty> AllMilitiaParties => Campaign.Current.MilitiaParties;

//  public static MBReadOnlyList<MobileParty> AllVillagerParties => Campaign.Current.VillagerParties;

//  public static MBReadOnlyList<MobileParty> AllCustomParties => Campaign.Current.CustomParties;

//  public static MBReadOnlyList<MobileParty> AllPartiesWithoutPartyComponent
//  {
//    get => Campaign.Current.PartiesWithoutPartyComponent;
//  }

//  public static int Count => Campaign.Current.MobileParties.Count;

//  public static MobileParty ConversationParty
//  {
//    get => Campaign.Current.ConversationManager.ConversationParty;
//  }

//  public TextObject Name
//  {
//    get
//    {
//      if (!TextObject.IsNullOrEmpty(this.Party.CustomName))
//        return this.Party.CustomName;
//      if (this._partyComponent != null)
//        return this._partyComponent.Name;
//      Debug.FailedAssert("UnnamedMobileParty", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Party\\MobileParty.cs", nameof (Name), 120);
//      return new TextObject("{=!}unnamedMobileParty");
//    }
//  }

//  [SaveableProperty(1002)]
//  public Settlement LastVisitedSettlement { get; private set; }

//  [SaveableProperty(1004)]
//  public Vec2 Bearing { get; internal set; }

//  public MBReadOnlyList<MobileParty> AttachedParties
//  {
//    get => (MBReadOnlyList<MobileParty>) this._attachedParties;
//  }

//  [SaveableProperty(1099)]
//  public bool HasLandNavigationCapability { get; private set; } = true;

//  public void SetLandNavigationAccess(bool access) => this.HasLandNavigationCapability = access;

//  public MBReadOnlyList<Ship> Ships => this.Party.Ships;

//  public bool HasNavalNavigationCapability
//  {
//    get => Campaign.Current.Models.PartyNavigationModel.HasNavalNavigationCapability(this);
//  }

//  [SaveableProperty(1009)]
//  public float Aggressiveness { get; set; }

//  public int PaymentLimit
//  {
//    get
//    {
//      PartyComponent partyComponent = this._partyComponent;
//      return partyComponent == null ? Campaign.Current.Models.PartyWageModel.MaxWagePaymentLimit : partyComponent.WagePaymentLimit;
//    }
//  }

//  public Banner Banner
//  {
//    get
//    {
//      if (this.Party.CustomBanner != null)
//        return this.Party.CustomBanner;
//      if (this.PartyComponent != null && this.PartyComponent.GetDefaultComponentBanner() != null)
//        return this.PartyComponent.GetDefaultComponentBanner();
//      return this.MapFaction != null ? this.MapFaction.Banner : (Banner) null;
//    }
//  }

//  public override TextObject GetName() => this.Name;

//  public bool HasLimitedWage()
//  {
//    return this.PaymentLimit != Campaign.Current.Models.PartyWageModel.MaxWagePaymentLimit;
//  }

//  public int GetAvailableWageBudget() => this.PaymentLimit - this.TotalWage - 5;

//  public bool IsWageLimitExceeded() => this.HasLimitedWage() && this.PaymentLimit < this.TotalWage;

//  public void SetWagePaymentLimit(int newLimit)
//  {
//    this.PartyComponent?.SetWagePaymentLimit(newLimit);
//  }

//  [SaveableProperty(1005)]
//  public Vec2 ArmyPositionAdder { get; private set; }

//  public CampaignVec2 AiBehaviorTarget => this.Ai.BehaviorTarget;

//  [SaveableProperty(1090)]
//  public MobileParty.PartyObjective Objective { get; private set; }

//  [CachedData]
//  MobileParty ILocatable<MobileParty>.NextLocatable { get; set; }

//  [SaveableProperty(1019)]
//  public MobilePartyAi Ai { get; private set; }

//  [SaveableProperty(1020)]
//  public PartyBase Party { get; private set; }

//  [SaveableProperty(1023 /*0x03FF*/)]
//  public bool IsActive { get; set; }

//  public bool IsInRaftState
//  {
//    get => this._isInRaftState;
//    set
//    {
//      if (this._isInRaftState == value)
//        return;
//      this._isInRaftState = value;
//      if (this._isInRaftState)
//        this.Anchor.ResetPosition();
//      this.Party.SetVisualAsDirty();
//    }
//  }

//  public CampaignTime DisorganizedUntilTime => this._disorganizedUntilTime;

//  [CachedData]
//  public float LastCalculatedBaseSpeed => this._lastCalculatedBaseSpeedExplained.ResultNumber;

//  [CachedData]
//  public PartyThinkParams ThinkParamsCache { get; private set; }

//  public float Speed
//  {
//    get
//    {
//      if (this.IsActive)
//        return this.CalculateSpeed();
//      Debug.FailedAssert("!IsActive", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Party\\MobileParty.cs", nameof (Speed), 310);
//      return 0.0f;
//    }
//  }

//  public ExplainedNumber SpeedExplained
//  {
//    get
//    {
//      ExplainedNumber baseSpeed = Campaign.Current.Models.PartySpeedCalculatingModel.CalculateBaseSpeed(this, true);
//      Campaign.Current.Models.PartySpeedCalculatingModel.CalculateFinalSpeed(this, baseSpeed);
//      this._lastCalculatedSpeed = baseSpeed.ResultNumber;
//      return baseSpeed;
//    }
//  }

//  public MobileParty ShortTermTargetParty => this.Ai.AiBehaviorPartyBase?.MobileParty;

//  public Settlement ShortTermTargetSettlement => this.Ai.AiBehaviorPartyBase?.Settlement;

//  public bool IsDisorganized => this._isDisorganized;

//  public bool IsCurrentlyUsedByAQuest => this._isCurrentlyUsedByAQuest;

//  [SaveableProperty(1050)]
//  public AiBehavior ShortTermBehavior { get; internal set; }

//  [SaveableProperty(1958)]
//  public bool IsPartyTradeActive { get; private set; }

//  public int PartyTradeGold
//  {
//    get
//    {
//      return this.IsLordParty && this.LeaderHero != null ? this.LeaderHero.Gold : this._partyTradeGold;
//    }
//    set
//    {
//      if (this.IsLordParty && this.LeaderHero != null)
//        this.LeaderHero.Gold = MathF.Max(value, 0);
//      else
//        this._partyTradeGold = MathF.Max(value, 0);
//    }
//  }

//  [SaveableProperty(1957)]
//  public int PartyTradeTaxGold { get; private set; }

//  [SaveableProperty(1960)]
//  public CampaignTime StationaryStartTime { get; private set; }

//  [CachedData]
//  public int VersionNo { get; private set; }

//  [SaveableProperty(1080)]
//  public bool ShouldJoinPlayerBattles { get; set; }

//  [SaveableProperty(1081)]
//  public bool IsDisbanding { get; set; }

//  public int RandomValue => this.Party.RandomValue;

//  public MobileParty.NavigationType NavigationCapability
//  {
//    get
//    {
//      MobileParty.NavigationType navigationCapability = MobileParty.NavigationType.None;
//      if (this.HasLandNavigationCapability)
//        navigationCapability |= MobileParty.NavigationType.Default;
//      if (this.HasNavalNavigationCapability)
//        navigationCapability |= MobileParty.NavigationType.Naval;
//      return navigationCapability;
//    }
//  }

//  public bool IsCurrentlyAtSea
//  {
//    get => this._isCurrentlyAtSea;
//    set
//    {
//      if (this._isCurrentlyAtSea == value)
//        return;
//      this._isCurrentlyAtSea = value;
//      foreach (MobileParty attachedParty in (List<MobileParty>) this._attachedParties)
//        attachedParty.IsCurrentlyAtSea = value;
//      this.UpdateVersionNo();
//      CampaignEventDispatcher.Instance.OnMobilePartyNavigationStateChanged(this);
//      this.Party.SetVisualAsDirty();
//      if (this.IsCurrentlyAtSea)
//        return;
//      this.SetNavalVisualAsDirty();
//    }
//  }

//  [CachedData]
//  public bool IsNavalVisualDirty { get; private set; }

//  public void SetNavalVisualAsDirty() => this.IsNavalVisualDirty = true;

//  public void OnNavalVisualsUpdated() => this.IsNavalVisualDirty = false;

//  public bool IsTargetingPort => this._isTargetingPort;

//  [SaveableProperty(1092)]
//  public AnchorPoint Anchor { get; private set; }

//  public bool IsTransitionInProgress => this.NavigationTransitionStartTime != CampaignTime.Zero;

//  [SaveableProperty(223)]
//  public CampaignVec2 EndPositionForNavigationTransition { get; private set; }

//  public CampaignTime NavigationTransitionStartTime
//  {
//    get => this._navigationTransitionStartTime;
//    private set
//    {
//      this._navigationTransitionStartTime = value;
//      if (this._navigationTransitionStartTime != CampaignTime.Zero)
//      {
//        if (this.IsCurrentlyAtSea)
//          this.NavigationTransitionDuration = Campaign.Current.Models.PartyTransitionModel.GetTransitionTimeDisembarking(this);
//        else
//          this.NavigationTransitionDuration = Campaign.Current.Models.PartyTransitionModel.GetTransitionTimeForEmbarking(this);
//      }
//      else
//        this.NavigationTransitionDuration = CampaignTime.Zero;
//    }
//  }

//  [SaveableProperty(1097)]
//  public CampaignTime NavigationTransitionDuration { get; private set; } = CampaignTime.Zero;

//  internal void InitializeNavigationTransitionParallel(
//    CampaignVec2 transitionStartPosition,
//    CampaignVec2 transitionEndPosition,
//    ref int gridChangeCount,
//    ref MobileParty[] gridChangeMobilePartyList)
//  {
//    this.NavigationTransitionStartTime = CampaignTime.Now;
//    this.Party.SetVisualAsDirty();
//    if (this.CurrentSettlement == null)
//    {
//      this.EndPositionForNavigationTransition = transitionEndPosition;
//      this.SetPositionParallel(in transitionStartPosition, ref gridChangeCount, ref gridChangeMobilePartyList);
//    }
//    foreach (MobileParty attachedParty in (List<MobileParty>) this._attachedParties)
//      attachedParty.InitializeNavigationTransitionParallel(transitionStartPosition, transitionEndPosition, ref gridChangeCount, ref gridChangeMobilePartyList);
//  }

//  public void SetSailAtPosition(CampaignVec2 position)
//  {
//    this.IsCurrentlyAtSea = true;
//    this.Position = position;
//    this.TargetPosition = position;
//    this.Anchor.ResetPosition();
//    for (int index = 0; index < this.AttachedParties.Count; ++index)
//    {
//      this.AttachedParties[index].Position = this.Position;
//      this.AttachedParties[index].Anchor.ResetPosition();
//    }
//  }

//  public void CancelNavigationTransition() => this.CancelNavigationTransitionParallel();

//  private void CancelNavigationTransitionParallel()
//  {
//    this.NavigationTransitionStartTime = CampaignTime.Zero;
//    this.EndPositionForNavigationTransition = CampaignVec2.Invalid;
//    this.Party.SetVisualAsDirty();
//    foreach (MobileParty attachedParty in (List<MobileParty>) this._attachedParties)
//      attachedParty.CancelNavigationTransitionParallel();
//  }

//  internal void FinishNavigationTransitionInternal()
//  {
//    bool flag = !this.IsCurrentlyAtSea;
//    this._isCurrentlyAtSea = flag;
//    if (this.Path.Size > this.PathBegin + 1)
//      ++this.PathBegin;
//    if (flag || this.IsInRaftState)
//      this.Anchor.ResetPosition();
//    else if (this.Ships.Any<Ship>())
//      this.Anchor.SetPosition(this.Position);
//    this.NavigationTransitionStartTime = CampaignTime.Zero;
//    this._position = this.CurrentSettlement == null ? (this.Army == null || this.AttachedTo == null ? this.EndPositionForNavigationTransition : this.Army.LeaderParty.EndPositionForNavigationTransition) : (!flag ? this.CurrentSettlement.GatePosition : this.CurrentSettlement.PortPosition);
//    if (this.IsInRaftState)
//      RaftStateChangeAction.DeactivateRaftStateForParty(this);
//    foreach (MobileParty attachedParty in (List<MobileParty>) this._attachedParties)
//    {
//      attachedParty.FinishNavigationTransitionInternal();
//      attachedParty.ArmyPositionAdder = Vec2.Zero;
//    }
//    this.ComputePath(this.MoveTargetPoint, this.NavigationCapability, !this.IsFleeing());
//    this.UpdateVersionNo();
//    CampaignEventDispatcher.Instance.OnMobilePartyNavigationStateChanged(this);
//    this.Party.SetVisualAsDirty();
//    if (!this.IsCurrentlyAtSea)
//      this.SetNavalVisualAsDirty();
//    this.Anchor.SetLastUsedDisembarkPosition(this.EndPositionForNavigationTransition);
//    this.EndPositionForNavigationTransition = CampaignVec2.Invalid;
//  }

//  public void ChangeIsCurrentlyAtSeaCheat()
//  {
//    this.IsCurrentlyAtSea = !this.IsCurrentlyAtSea;
//    this.Anchor.ResetPosition();
//    for (int index = 0; index < this.AttachedParties.Count; ++index)
//      this.AttachedParties[index].Anchor.ResetPosition();
//  }

//  public MobileParty.NavigationType DesiredAiNavigationType
//  {
//    get => this._desiredAiNavigationType;
//    set => this._desiredAiNavigationType = value;
//  }

//  public Settlement CurrentSettlement
//  {
//    get => this._currentSettlement;
//    set
//    {
//      if (value == this._currentSettlement)
//        return;
//      if (this._currentSettlement != null)
//      {
//        this._currentSettlement.RemoveMobileParty(this);
//        this.ArmyPositionAdder = Vec2.Zero;
//      }
//      this._currentSettlement = value;
//      if (this._currentSettlement != null)
//      {
//        this._currentSettlement.AddMobileParty(this);
//        this.Position = this.IsCurrentlyAtSea ? this._currentSettlement.PortPosition : this._currentSettlement.GatePosition;
//        this.LastVisitedSettlement = value;
//        this.EndPositionForNavigationTransition = this.Position;
//      }
//      else
//        this.EndPositionForNavigationTransition = CampaignVec2.Invalid;
//      foreach (MobileParty attachedParty in (List<MobileParty>) this._attachedParties)
//        attachedParty.CurrentSettlement = value;
//      if (this._currentSettlement != null && this._currentSettlement.IsFortification)
//      {
//        this.ArmyPositionAdder = Vec2.Zero;
//        this.Bearing = Vec2.Zero;
//        foreach (MobileParty party in (List<MobileParty>) this._currentSettlement.Parties)
//          party.Party.SetVisualAsDirty();
//      }
//      this.Party.SetVisualAsDirty();
//    }
//  }

//  public Settlement HomeSettlement
//  {
//    get
//    {
//      Settlement customHomeSettlement = this._customHomeSettlement;
//      if (customHomeSettlement != null)
//        return customHomeSettlement;
//      return this._partyComponent?.HomeSettlement;
//    }
//  }

//  public void SetCustomHomeSettlement(Settlement customHomeSettlement)
//  {
//    this._customHomeSettlement = customHomeSettlement;
//  }

//  public MobileParty AttachedTo
//  {
//    get => this._attachedTo;
//    set
//    {
//      if (this._attachedTo == value)
//        return;
//      this.SetAttachedToInternal(value);
//    }
//  }

//  private void SetAttachedToInternal(MobileParty value)
//  {
//    if (this._attachedTo != null)
//    {
//      if (this.IsTransitionInProgress)
//        this.CancelNavigationTransitionParallel();
//      this._attachedTo.RemoveAttachedPartyInternal(this);
//      if (this.Party.MapEventSide != null && this.IsActive)
//      {
//        this.Party.MapEventSide.HandleMapEventEndForPartyInternal(this.Party);
//        this.Party.MapEventSide = (MapEventSide) null;
//      }
//      if (this.BesiegerCamp != null)
//        this.BesiegerCamp = (BesiegerCamp) null;
//      this.OnAttachedToRemoved();
//    }
//    this._attachedTo = value;
//    if (this._attachedTo != null)
//    {
//      this._attachedTo.AddAttachedPartyInternal(this);
//      this.Party.MapEventSide = this._attachedTo.Party.MapEventSide;
//      this.BesiegerCamp = this._attachedTo.BesiegerCamp;
//      this.CurrentSettlement = this._attachedTo.CurrentSettlement;
//      if (this._attachedTo.IsTransitionInProgress)
//        this.NavigationTransitionStartTime = CampaignTime.Now;
//      else if (this.IsTransitionInProgress)
//        this.CancelNavigationTransitionParallel();
//      if (this.IsCurrentlyAtSea != this._attachedTo.IsCurrentlyAtSea)
//        this.IsCurrentlyAtSea = this._attachedTo.IsCurrentlyAtSea;
//    }
//    this.Party.SetVisualAsDirty();
//  }

//  private void AddAttachedPartyInternal(MobileParty mobileParty)
//  {
//    if (this._attachedParties == null)
//      this._attachedParties = new MBList<MobileParty>();
//    this._attachedParties.Add(mobileParty);
//    if (CampaignEventDispatcher.Instance == null)
//      return;
//    CampaignEventDispatcher.Instance.OnPartyAttachedAnotherParty(mobileParty);
//  }

//  private void RemoveAttachedPartyInternal(MobileParty mobileParty)
//  {
//    this._attachedParties.Remove(mobileParty);
//  }

//  private void OnAttachedToRemoved()
//  {
//    this.ArmyPositionAdder = Vec2.Zero;
//    this.SetMoveModeHold();
//  }

//  public Army Army
//  {
//    get => this._army;
//    set
//    {
//      if (this._army == value)
//        return;
//      this.UpdateVersionNo();
//      Army army = this._army;
//      if (this._army != null)
//        this._army.OnRemovePartyInternal(this);
//      this._army = value;
//      if (value == null)
//      {
//        if (this == MobileParty.MainParty && Game.Current.GameStateManager.ActiveState is MapState)
//          ((MapState) Game.Current.GameStateManager.ActiveState).OnLeaveArmy();
//        CampaignEventDispatcher.Instance.OnPartyLeftArmy(this, army);
//      }
//      else
//        this._army.OnAddPartyInternal(this);
//    }
//  }

//  public BesiegerCamp BesiegerCamp
//  {
//    get => this._besiegerCamp;
//    set
//    {
//      if (this._besiegerCamp == value || this._besiegerCampResetStarted)
//        return;
//      this._besiegerCampResetStarted = true;
//      if (this._besiegerCamp != null)
//        this.OnPartyLeftSiegeInternal();
//      this._besiegerCamp = value;
//      if (this._besiegerCamp != null)
//        this.OnPartyJoinedSiegeInternal();
//      foreach (MobileParty attachedParty in (List<MobileParty>) this._attachedParties)
//        attachedParty.BesiegerCamp = value;
//      this.Party.SetVisualAsDirty();
//      this._besiegerCampResetStarted = false;
//    }
//  }

//  public AiBehavior DefaultBehavior
//  {
//    get => this._defaultBehavior;
//    private set
//    {
//      if (this._defaultBehavior == value)
//        return;
//      this._defaultBehavior = value;
//      this.Ai.DefaultBehaviorNeedsUpdate = true;
//      this.RecalculateShortTermBehavior();
//    }
//  }

//  public Settlement TargetSettlement => this._targetSettlement;

//  public void SetTargetSettlement(Settlement settlement, bool isTargetingPort)
//  {
//    if (settlement != this._targetSettlement || this.IsTargetingPort != isTargetingPort)
//    {
//      this._targetSettlement = settlement;
//      if (settlement != null)
//        this.MoveTargetPoint = isTargetingPort ? settlement.PortPosition : settlement.GatePosition;
//      this.Ai.DefaultBehaviorNeedsUpdate = true;
//    }
//    this._isTargetingPort = isTargetingPort;
//  }

//  public CampaignVec2 TargetPosition
//  {
//    get => this._targetPosition;
//    internal set
//    {
//      if (!(this._targetPosition != value))
//        return;
//      this._targetPosition = value;
//      this.Ai.DefaultBehaviorNeedsUpdate = true;
//    }
//  }

//  public MobileParty TargetParty
//  {
//    get => this._targetParty;
//    internal set
//    {
//      if (value == this._targetParty)
//        return;
//      this._targetParty = value;
//      this.Ai.DefaultBehaviorNeedsUpdate = true;
//    }
//  }

//  public MobileParty()
//  {
//    this._isVisible = false;
//    this.IsActive = true;
//    this._isCurrentlyUsedByAQuest = false;
//    this.Party = new PartyBase(this);
//    this.Anchor = new AnchorPoint(this);
//    this.InitMembers();
//    this.InitCached();
//    this.Initialize();
//  }

//  public Hero LeaderHero => this.PartyComponent?.Leader;

//  [SaveableProperty(1070)]
//  private Hero Scout { get; set; }

//  [SaveableProperty(1072)]
//  private Hero Engineer { get; set; }

//  [SaveableProperty(1071)]
//  private Hero Quartermaster { get; set; }

//  [SaveableProperty(1073)]
//  private Hero Surgeon { get; set; }

//  public Hero Owner => this._partyComponent?.PartyOwner;

//  public Hero EffectiveScout
//  {
//    get => this.Scout == null || this.Scout.PartyBelongedTo != this ? this.LeaderHero : this.Scout;
//  }

//  public Hero EffectiveQuartermaster
//  {
//    get
//    {
//      return this.Quartermaster == null || this.Quartermaster.PartyBelongedTo != this ? this.LeaderHero : this.Quartermaster;
//    }
//  }

//  public Hero EffectiveEngineer
//  {
//    get
//    {
//      return this.Engineer == null || this.Engineer.PartyBelongedTo != this ? this.LeaderHero : this.Engineer;
//    }
//  }

//  public Hero EffectiveSurgeon
//  {
//    get
//    {
//      return this.Surgeon == null || this.Surgeon.PartyBelongedTo != this ? this.LeaderHero : this.Surgeon;
//    }
//  }

//  public void SetPartyScout(Hero hero)
//  {
//    this.RemoveHeroPartyRole(hero);
//    this.Scout = hero;
//  }

//  public void SetPartyQuartermaster(Hero hero)
//  {
//    this.RemoveHeroPartyRole(hero);
//    this.Quartermaster = hero;
//  }

//  public void SetPartyEngineer(Hero hero)
//  {
//    this.RemoveHeroPartyRole(hero);
//    this.Engineer = hero;
//  }

//  public void SetPartySurgeon(Hero hero)
//  {
//    this.RemoveHeroPartyRole(hero);
//    this.Surgeon = hero;
//  }

//  internal void StartUp()
//  {
//    this.NextTargetPosition = this.Position;
//    this.PathLastFace = PathFaceRecord.NullFaceRecord;
//    this.MoveTargetPoint = this.Position;
//    this.ForceAiNoPathMode = false;
//  }

//  [LateLoadInitializationCallback]
//  private void OnLateLoad(MetaData metaData, ObjectLoadData objectLoadData)
//  {
//    if (MBSaveLoad.LastLoadedGameVersion.IsOlderThan(ApplicationVersion.FromString("v1.3.0")))
//    {
//      TextObject memberValueBySaveId = (TextObject) objectLoadData.GetMemberValueBySaveId(1021);
//      if (!TextObject.IsNullOrEmpty(memberValueBySaveId))
//        this.Party.SetCustomName(memberValueBySaveId);
//    }
//    if (MBSaveLoad.LastLoadedGameVersion.IsOlderThan(ApplicationVersion.FromString("v1.3.0")))
//      this._position = new CampaignVec2((Vec2) objectLoadData.GetMemberValueBySaveId(1100), true);
//    if (MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.1.0"))
//    {
//      PartyBase memberValueBySaveId1 = (PartyBase) objectLoadData.GetMemberValueBySaveId(1052);
//      IInteractablePoint interactablePoint = (IInteractablePoint) null;
//      if (memberValueBySaveId1 != null && (memberValueBySaveId1.IsSettlement || memberValueBySaveId1.IsMobile))
//        interactablePoint = (IInteractablePoint) memberValueBySaveId1;
//      object memberValueBySaveId2 = objectLoadData.GetMemberValueBySaveId(1036);
//      object memberValueBySaveId3 = objectLoadData.GetMemberValueBySaveId(1037);
//      object memberValueBySaveId4 = objectLoadData.GetMemberValueBySaveId(1064);
//      object memberValueBySaveId5 = objectLoadData.GetMemberValueBySaveId(1065);
//      object memberValueBySaveId6 = objectLoadData.GetMemberValueBySaveId(1047);
//      object memberValueBySaveId7 = objectLoadData.GetMemberValueBySaveId(1051);
//      object memberValueBySaveId8 = objectLoadData.GetMemberValueBySaveId(1038);
//      object memberValueBySaveId9 = objectLoadData.GetMemberValueBySaveId(1039);
//      object memberValueBySaveId10 = objectLoadData.GetMemberValueBySaveId(1055);
//      object memberValueBySaveId11 = objectLoadData.GetMemberValueBySaveId(1054);
//      object memberValueBySaveId12 = objectLoadData.GetMemberValueBySaveId(1062);
//      object memberValueBySaveId13 = objectLoadData.GetMemberValueBySaveId(1061);
//      object fieldValueBySaveId = objectLoadData.GetFieldValueBySaveId(1070);
//      object memberValueBySaveId14 = objectLoadData.GetMemberValueBySaveId(1022);
//      object obj = (object) interactablePoint ?? objectLoadData.GetMemberValueBySaveId(1056);
//      object memberValueBySaveId15 = objectLoadData.GetMemberValueBySaveId(1074);
//      if (memberValueBySaveId2 != null)
//      {
//        IInteractablePoint oldAiBehaviorMapEntity = (IInteractablePoint) null;
//        switch (obj)
//        {
//          case null:
//            this.Ai.InitializeForOldSaves((float) memberValueBySaveId2, (float) memberValueBySaveId3, (CampaignTime) memberValueBySaveId4, (int) memberValueBySaveId5, (AiBehavior) memberValueBySaveId6, (Vec2) memberValueBySaveId7, (bool) memberValueBySaveId8, (bool) memberValueBySaveId9, memberValueBySaveId10 != null ? (MoveModeType) memberValueBySaveId10 : MoveModeType.Hold, (MobileParty) memberValueBySaveId11, (Vec2) memberValueBySaveId12, (Vec2) memberValueBySaveId13, (Vec2) fieldValueBySaveId, (Vec2) memberValueBySaveId14, oldAiBehaviorMapEntity, (CampaignTime?) memberValueBySaveId15 ?? CampaignTime.Never);
//            break;
//          case Settlement settlement:
//            oldAiBehaviorMapEntity = (IInteractablePoint) settlement.Party;
//            goto case null;
//          case MobileParty mobileParty:
//            oldAiBehaviorMapEntity = (IInteractablePoint) mobileParty.Party;
//            goto case null;
//          default:
//            oldAiBehaviorMapEntity = (IInteractablePoint) obj;
//            goto case null;
//        }
//      }
//      this.UpdatePartyComponentFlags();
//      if (this.IsGarrison || this.IsLordParty)
//      {
//        object memberValueBySaveId16 = objectLoadData.GetMemberValueBySaveId(1010);
//        if (memberValueBySaveId16 != null)
//          this.SetWagePaymentLimit((int) memberValueBySaveId16);
//      }
//    }
//    if (!MBSaveLoad.IsUpdatingGameVersion || !MBSaveLoad.LastLoadedGameVersion.IsOlderThan(ApplicationVersion.FromString("v1.3.0")))
//      return;
//    this._position = new CampaignVec2((Vec2) objectLoadData.GetMemberValueBySaveId(1100), true);
//    object memberValueBySaveId17 = objectLoadData.GetMemberValueBySaveId(1053);
//    if (memberValueBySaveId17 != null)
//      this._targetPosition = new CampaignVec2((Vec2) memberValueBySaveId17, true);
//    object memberValueBySaveId18 = objectLoadData.GetMemberValueBySaveId(212);
//    if (memberValueBySaveId18 != null)
//      this._pathLastPosition = new CampaignVec2((Vec2) memberValueBySaveId18, true);
//    object memberValueBySaveId19 = objectLoadData.GetMemberValueBySaveId(213);
//    if (memberValueBySaveId19 != null)
//      this.NextTargetPosition = new CampaignVec2((Vec2) memberValueBySaveId19, true);
//    object memberValueBySaveId20 = objectLoadData.GetMemberValueBySaveId(214);
//    if (memberValueBySaveId20 == null)
//      return;
//    this.MoveTargetPoint = new CampaignVec2((Vec2) memberValueBySaveId20, true);
//  }

//  public float RecentEventsMorale
//  {
//    get => this._moraleDueToEvents;
//    set
//    {
//      this._moraleDueToEvents = value;
//      if ((double) this._moraleDueToEvents < -100.0)
//      {
//        this._moraleDueToEvents = -100f;
//      }
//      else
//      {
//        if ((double) this._moraleDueToEvents <= 100.0)
//          return;
//        this._moraleDueToEvents = 100f;
//      }
//    }
//  }

//  public override string ToString()
//  {
//    return $"{this.StringId}:{(object) this.Party.Index} Name: {this.Name.ToString()}";
//  }

//  public ExplainedNumber SeeingRangeExplanation
//  {
//    get => Campaign.Current.Models.MapVisibilityModel.GetPartySpottingRange(this, true);
//  }

//  public int InventoryCapacity
//  {
//    get
//    {
//      return (int) Campaign.Current.Models.InventoryCapacityModel.CalculateInventoryCapacity(this, this.IsCurrentlyAtSea).ResultNumber;
//    }
//  }

//  public ExplainedNumber InventoryCapacityExplainedNumber
//  {
//    get
//    {
//      return Campaign.Current.Models.InventoryCapacityModel.CalculateInventoryCapacity(this, this.IsCurrentlyAtSea, true);
//    }
//  }

//  public float TotalWeightCarried
//  {
//    get
//    {
//      if (this.IsWeightCacheInvalid())
//      {
//        this._partyWeightLastCheckVersionNo = this.GetVersionNoForWeightCalculation();
//        this._totalWeightCarriedCache = Campaign.Current.Models.InventoryCapacityModel.CalculateTotalWeightCarried(this, this.IsCurrentlyAtSea).ResultNumber;
//      }
//      return this._totalWeightCarriedCache;
//    }
//  }

//  public MapEventSide MapEventSide
//  {
//    get => this.Party.MapEventSide;
//    set => this.Party.MapEventSide = value;
//  }

//  public ExplainedNumber TotalWeightCarriedExplainedNumber
//  {
//    get
//    {
//      return Campaign.Current.Models.InventoryCapacityModel.CalculateTotalWeightCarried(this, this.IsCurrentlyAtSea, true);
//    }
//  }

//  public float Morale
//  {
//    get
//    {
//      float resultNumber = Campaign.Current.Models.PartyMoraleModel.GetEffectivePartyMorale(this).ResultNumber;
//      return (double) resultNumber < 0.0 ? 0.0f : ((double) resultNumber > 100.0 ? 100f : resultNumber);
//    }
//  }

//  public float FoodChange
//  {
//    get
//    {
//      return Campaign.Current.Models.MobilePartyFoodConsumptionModel.CalculateDailyFoodConsumptionf(this, Campaign.Current.Models.MobilePartyFoodConsumptionModel.CalculateDailyBaseFoodConsumptionf(this)).ResultNumber;
//    }
//  }

//  public float BaseFoodChange
//  {
//    get
//    {
//      return Campaign.Current.Models.MobilePartyFoodConsumptionModel.CalculateDailyBaseFoodConsumptionf(this).ResultNumber;
//    }
//  }

//  public Clan ActualClan
//  {
//    get => this._actualClan;
//    set
//    {
//      if (this._actualClan == value)
//        return;
//      if (this._actualClan != null && value != null && this.PartyComponent is WarPartyComponent partyComponent)
//        partyComponent.OnClanChange(this._actualClan, value);
//      this._actualClan = value;
//    }
//  }

//  public ExplainedNumber FoodChangeExplained
//  {
//    get
//    {
//      return Campaign.Current.Models.MobilePartyFoodConsumptionModel.CalculateDailyFoodConsumptionf(this, Campaign.Current.Models.MobilePartyFoodConsumptionModel.CalculateDailyBaseFoodConsumptionf(this, true));
//    }
//  }

//  public ExplainedNumber MoraleExplained
//  {
//    get => Campaign.Current.Models.PartyMoraleModel.GetEffectivePartyMorale(this, true);
//  }

//  internal void ValidateSpeed()
//  {
//    double speed = (double) this.CalculateSpeed();
//  }

//  public void ChangePartyLeader(Hero newLeader) => this.PartyComponent.ChangePartyLeader(newLeader);

//  public void OnPartyInteraction(MobileParty engagingParty)
//  {
//    MobileParty mobileParty = this;
//    if (mobileParty.AttachedTo != null && engagingParty != mobileParty.AttachedTo)
//      mobileParty = mobileParty.AttachedTo;
//    bool flag = false;
//    if (mobileParty.CurrentSettlement != null)
//    {
//      if (mobileParty.MapEvent != null)
//        flag = mobileParty.MapEvent.MapEventSettlement == mobileParty.CurrentSettlement && (mobileParty.MapEvent.AttackerSide.LeaderParty.MapFaction == engagingParty.MapFaction || mobileParty.MapEvent.DefenderSide.LeaderParty.MapFaction == engagingParty.MapFaction);
//    }
//    else
//      flag = engagingParty != MobileParty.MainParty || !mobileParty.IsEngaging || mobileParty.ShortTermTargetParty != MobileParty.MainParty;
//    if (!flag)
//      return;
//    if (engagingParty == MobileParty.MainParty && Game.Current.GameStateManager.ActiveState is MapState activeState)
//      activeState.OnMainPartyEncounter();
//    EncounterManager.StartPartyEncounter(engagingParty.Party, mobileParty.Party);
//  }

//  public void SetPositionAfterMapChange(CampaignVec2 newPosition) => this.Position = newPosition;

//  public void RemovePartyLeader() => this.PartyComponent.ChangePartyLeader((Hero) null);

//  public void CheckPositionsForMapChangeAndUpdateIfNeeded()
//  {
//    if (this.Position.IsValid() && this.IsCurrentlyAtSea != this.Position.IsOnLand)
//      return;
//    this.Position = NavigationHelper.FindPointAroundPosition(NavigationHelper.GetClosestNavMeshFaceCenterPositionForPosition(this.Position, Campaign.Current.Models.PartyNavigationModel.GetInvalidTerrainTypesForNavigationType(this.IsCurrentlyAtSea ? MobileParty.NavigationType.Naval : MobileParty.NavigationType.Default)), this.NavigationCapability, 8f, 1f);
//  }

//  public void CheckAiForMapChangeAndUpdateIfNeeded()
//  {
//    if (this.IsMainParty)
//      return;
//    CampaignVec2 campaignVec2_1 = CampaignVec2.Invalid;
//    if (this.TargetSettlement != null)
//      campaignVec2_1 = this.IsTargetingPort ? this.TargetSettlement.PortPosition : this.TargetSettlement.GatePosition;
//    this.Ai.CacheAiBehaviorPartyBase();
//    MobileParty mobileParty1 = this.MoveTargetParty ?? this.TargetParty;
//    mobileParty1?.CheckPositionsForMapChangeAndUpdateIfNeeded();
//    switch (this.DefaultBehavior)
//    {
//      case AiBehavior.Hold:
//        this.MoveTargetPoint = this.Position;
//        break;
//      case AiBehavior.GoToSettlement:
//      case AiBehavior.AssaultSettlement:
//      case AiBehavior.RaidSettlement:
//      case AiBehavior.BesiegeSettlement:
//        this.MoveTargetPoint = campaignVec2_1;
//        break;
//      case AiBehavior.EngageParty:
//      case AiBehavior.JoinParty:
//      case AiBehavior.EscortParty:
//        this.MoveTargetPoint = mobileParty1.Position;
//        break;
//      case AiBehavior.GoAroundParty:
//        this.MoveTargetPoint = this.Ai.AiBehaviorPartyBase.Position;
//        break;
//      case AiBehavior.GoToPoint:
//        if (this.Army != null)
//        {
//          CampaignVec2 campaignVec2_2;
//          switch (this.Army.AiBehaviorObject)
//          {
//            case Settlement settlement:
//              campaignVec2_2 = this.IsTargetingPort ? settlement.PortPosition : settlement.GatePosition;
//              break;
//            case MobileParty mobileParty2:
//              campaignVec2_2 = mobileParty2.Position;
//              break;
//            default:
//              campaignVec2_2 = this.Army.AiBehaviorObject.Position;
//              break;
//          }
//          this.MoveTargetPoint = campaignVec2_2;
//          break;
//        }
//        this.MoveTargetPoint = this.Position;
//        this.SetMoveModeHold();
//        break;
//      case AiBehavior.FleeToPoint:
//        CampaignVec2 fleeTargetPoint;
//        this.Ai.CalculateFleePosition(out fleeTargetPoint, this.ShortTermTargetParty, (this.ShortTermTargetParty.Position - this.Position).ToVec2());
//        this.MoveTargetPoint = fleeTargetPoint;
//        break;
//      case AiBehavior.FleeToGate:
//        this.MoveTargetPoint = campaignVec2_1;
//        break;
//      case AiBehavior.FleeToParty:
//        this.MoveTargetPoint = this.MoveTargetParty.Position;
//        break;
//      case AiBehavior.PatrolAroundPoint:
//        if (this.MoveTargetParty != null)
//        {
//          this.MoveTargetPoint = this.MoveTargetParty.Position;
//          break;
//        }
//        if (this.TargetSettlement != null)
//        {
//          this.MoveTargetPoint = campaignVec2_1;
//          break;
//        }
//        Settlement settlement1 = SettlementHelper.FindNearestHideoutToMobileParty(this, this.NavigationCapability)?.Settlement;
//        if (settlement1 != null)
//        {
//          this.MoveTargetPoint = this.NavigationCapability == MobileParty.NavigationType.Naval ? settlement1.PortPosition : settlement1.GatePosition;
//          break;
//        }
//        this.MoveTargetPoint = this.Position;
//        this.SetMoveModeHold();
//        break;
//      case AiBehavior.DefendSettlement:
//        this.MoveTargetPoint = this.TargetSettlement.SiegeEvent == null ? campaignVec2_1 : this.TargetSettlement.SiegeEvent.BesiegerCamp.LeaderParty.Position;
//        break;
//    }
//    if (this.DefaultBehavior == AiBehavior.EscortParty)
//      return;
//    this.TargetPosition = this.MoveTargetPoint;
//    this.Ai.BehaviorTarget = this.MoveTargetPoint;
//    this.NextTargetPosition = this.MoveTargetPoint;
//  }

//  [LoadInitializationCallback]
//  private void OnLoad(MetaData metaData, ObjectLoadData objectLoadData)
//  {
//    object memberValueBySaveId = objectLoadData.GetMemberValueBySaveId(1032);
//    if (memberValueBySaveId != null)
//      this._doNotAttackMainParty = (int) memberValueBySaveId;
//    if (!MBSaveLoad.LastLoadedGameVersion.IsOlderThan(ApplicationVersion.FromString("v1.3.0")))
//      return;
//    this.HasLandNavigationCapability = true;
//    this.Anchor = new AnchorPoint(this);
//  }

//  protected override void PreAfterLoad()
//  {
//    this.UpdatePartyComponentFlags();
//    this.PartyComponent?.Initialize(this);
//    this.ComputePathAfterLoad();
//    this.Anchor?.InitializeOnLoad(this);
//    this.Ai.PreAfterLoad();
//    if (this._disorganizedUntilTime.IsFuture)
//      this._isDisorganized = true;
//    if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.2"))
//    {
//      if (this.LeaderHero != null && this != MobileParty.MainParty && this.LeaderHero.PartyBelongedTo != this || this.MapEvent == null && this.StringId.Contains("troops_of_"))
//        DestroyPartyAction.Apply((PartyBase) null, this);
//      if (this.MapEvent == null && (this.StringId.Contains("troops_of_CharacterObject") || this.StringId.Contains("troops_of_TaleWorlds.CampaignSystem.CharacterObject")))
//      {
//        if (!this.IsActive)
//          this.IsActive = true;
//        DestroyPartyAction.Apply((PartyBase) null, this);
//      }
//    }
//    if (!MBSaveLoad.IsUpdatingGameVersion || !(MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.3.0")) || !this.IsActive || this.MapFaction != null)
//      return;
//    if (this.MapEvent != null)
//      this.MapEventSide = (MapEventSide) null;
//    this.RemoveParty();
//  }

//  private void ComputePathAfterLoad()
//  {
//    if (this.DesiredAiNavigationType == MobileParty.NavigationType.None)
//      return;
//    if (!this.MoveTargetPoint.Face.IsValid())
//      this.MoveTargetPoint = new CampaignVec2(this.MoveTargetPoint.ToVec2(), !this.MoveTargetPoint.IsOnLand);
//    TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(this.MoveTargetPoint.Face);
//    MobileParty.NavigationType navigationType = this.DesiredAiNavigationType;
//    if (this.DesiredAiNavigationType == MobileParty.NavigationType.Naval != this.IsCurrentlyAtSea || !Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(faceTerrainType, this.DesiredAiNavigationType))
//      navigationType = MobileParty.NavigationType.All;
//    if (navigationType == MobileParty.NavigationType.None)
//      return;
//    this.ComputePath(this.MoveTargetPoint, navigationType, !this.IsFleeing());
//  }

//  protected override void OnBeforeLoad()
//  {
//    this.InitMembers();
//    this.InitCached();
//    this._attachedTo?.AddAttachedPartyInternal(this);
//  }

//  private void InitCached()
//  {
//    this.Path = new NavigationPath();
//    this.PathLastFace = PathFaceRecord.NullFaceRecord;
//    this.ForceAiNoPathMode = false;
//    ((ILocatable<MobileParty>) this).LocatorNodeIndex = -1;
//    this.ThinkParamsCache = new PartyThinkParams(this);
//    this.ResetCached();
//  }

//  private void ResetCached()
//  {
//    this._partySizeRatioLastCheckVersion = -1;
//    this._cachedPartySizeRatio = 1f;
//    this.VersionNo = 0;
//    this._partyPureSpeedLastCheckVersion = -1;
//    this._itemRosterVersionNo = -1;
//    this.Party.InitCache();
//  }

//  protected override void AfterLoad()
//  {
//    this.Party.AfterLoad();
//    if (this.IsGarrison && this.MapEvent == null && this.SiegeEvent == null && this.TargetParty != null && this.CurrentSettlement != null)
//      this.SetMoveModeHold();
//    if (this.CurrentSettlement != null && !this.CurrentSettlement.Parties.Contains(this))
//    {
//      this.CurrentSettlement.AddMobileParty(this);
//      foreach (MobileParty attachedParty in (List<MobileParty>) this._attachedParties)
//      {
//        if (this.Army.LeaderParty != this)
//          this.CurrentSettlement.AddMobileParty(attachedParty);
//      }
//    }
//    if (this._doNotAttackMainParty > 0)
//      this.Ai.DoNotAttackMainPartyUntil = CampaignTime.HoursFromNow((float) this._doNotAttackMainParty);
//    if (this.IsCaravan && this.Army != null)
//      this.Army = (Army) null;
//    if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.1.0") && (this.PaymentLimit == 2000 || this == MobileParty.MainParty && this.PaymentLimit == 0))
//      this.SetWagePaymentLimit(Campaign.Current.Models.PartyWageModel.MaxWagePaymentLimit);
//    if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.0") && this.IsCaravan && this.Owner == Hero.MainHero && this.ActualClan == null)
//      this.ActualClan = this.Owner.Clan;
//    if (!MBSaveLoad.IsUpdatingGameVersion || !(MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.1.4")))
//      return;
//    if (this.TargetParty != null)
//    {
//      IFaction mapFaction = this.TargetParty.MapFaction;
//      if ((mapFaction != null ? (!mapFaction.IsAtWarWith(this.MapFaction) ? 1 : 0) : 1) != 0)
//        goto label_28;
//    }
//    if (this.TargetSettlement != null)
//    {
//      IFaction mapFaction = this.TargetSettlement.MapFaction;
//      if ((mapFaction != null ? (!mapFaction.IsAtWarWith(this.MapFaction) ? 1 : 0) : 1) != 0)
//        goto label_28;
//    }
//    if (this.ShortTermTargetParty == null)
//      return;
//    MobileParty shortTermTargetParty = this.ShortTermTargetParty;
//    int num;
//    if (shortTermTargetParty == null)
//    {
//      num = 1;
//    }
//    else
//    {
//      bool? nullable = shortTermTargetParty.MapFaction?.IsAtWarWith(this.MapFaction);
//      bool flag = true;
//      num = !(nullable.GetValueOrDefault() == flag & nullable.HasValue) ? 1 : 0;
//    }
//    if (num == 0)
//      return;
//label_28:
//    this.SetMoveModeHold();
//  }

//  internal void OnFinishLoadState() => Campaign.Current.MobilePartyLocator.UpdateLocator(this);

//  int ILocatable<MobileParty>.LocatorNodeIndex
//  {
//    get => this._locatorNodeIndex;
//    set => this._locatorNodeIndex = value;
//  }

//  internal void HourlyTick()
//  {
//    if (!this.IsActive)
//      return;
//    if (this.LeaderHero != null && this.CurrentSettlement != null && this.CurrentSettlement == this.LeaderHero.HomeSettlement)
//      ++this.LeaderHero.PassedTimeAtHomeSettlement;
//    this.Anchor.HourlyTick();
//  }

//  public void MovePartyToTheClosestLand()
//  {
//    CampaignVec2 positionWithPath = Campaign.Current.MapSceneWrapper.GetNearestFaceCenterForPositionWithPath(this.CurrentNavigationFace, true, Campaign.MapDiagonal / 2f, Campaign.Current.Models.PartyNavigationModel.GetInvalidTerrainTypesForNavigationType(MobileParty.NavigationType.All));
//    this.SetNavigationModePoint(positionWithPath);
//    this.SetMoveGoToPoint(positionWithPath, MobileParty.NavigationType.All);
//  }

//  internal void DailyTick()
//  {
//    this.RecentEventsMorale -= this.RecentEventsMorale * 0.1f;
//    if (this.LeaderHero == null)
//      return;
//    this.LeaderHero.PassedTimeAtHomeSettlement *= 0.9f;
//  }

//  public TextObject GetBehaviorText()
//  {
//    TextObject behaviorText = TextObject.GetEmpty();
//    if (this.Army != null && (this.AttachedTo != null || this.Army.LeaderParty == this) && !this.Army.LeaderParty.IsEngaging && !this.Army.LeaderParty.IsFleeing())
//      behaviorText = this.Army.GetLongTermBehaviorText();
//    float estimatedLandRatio;
//    if (behaviorText.IsEmpty())
//    {
//      if (this.DefaultBehavior == AiBehavior.Hold || this.ShortTermBehavior == AiBehavior.Hold || this.IsMainParty && Campaign.Current.IsMainPartyWaiting)
//        behaviorText = !this.IsVillager || !this.HasNavalNavigationCapability ? new TextObject("{=RClxLG6N}Holding.") : new TextObject("{=WYxUqYpu}Fishing.");
//      else if (this.ShortTermBehavior == AiBehavior.EngageParty && this.ShortTermTargetParty != null)
//      {
//        behaviorText = new TextObject("{=5bzk75Ql}Engaging {TARGET_PARTY}.");
//        behaviorText.SetTextVariable("TARGET_PARTY", this.ShortTermTargetParty.Name);
//      }
//      else if (this.DefaultBehavior == AiBehavior.GoAroundParty && this.ShortTermBehavior == AiBehavior.GoToPoint)
//      {
//        behaviorText = new TextObject("{=XYAVu2f0}Chasing {TARGET_PARTY}.");
//        behaviorText.SetTextVariable("TARGET_PARTY", this.TargetParty.Name);
//      }
//      else if (this.ShortTermBehavior == AiBehavior.FleeToParty && this.ShortTermTargetParty != null)
//      {
//        behaviorText = new TextObject("{=R8vuwKaf}Running from {TARGET_PARTY} to ally party.");
//        behaviorText.SetTextVariable("TARGET_PARTY", this.ShortTermTargetParty.Name);
//      }
//      else if (this.ShortTermBehavior == AiBehavior.FleeToPoint)
//      {
//        if (this.ShortTermTargetParty != null)
//        {
//          behaviorText = new TextObject("{=AcMayd1p}Running from {TARGET_PARTY}.");
//          behaviorText.SetTextVariable("TARGET_PARTY", this.ShortTermTargetParty.Name);
//        }
//        else
//          behaviorText = new TextObject("{=5W2oZOwu}Sailing away from storm.");
//      }
//      else if (this.ShortTermBehavior == AiBehavior.FleeToGate && this.ShortTermTargetSettlement != null)
//        behaviorText = new TextObject("{=p0C3WfHE}Running to settlement.");
//      else if (this.DefaultBehavior == AiBehavior.DefendSettlement)
//      {
//        behaviorText = new TextObject("{=rGy8vjOv}Defending {TARGET_SETTLEMENT}.");
//        if (this.ShortTermBehavior == AiBehavior.GoToPoint)
//        {
//          if (!this.IsMoving)
//            behaviorText = new TextObject("{=LAt87KjS}Waiting for ally parties to defend {TARGET_SETTLEMENT}.");
//          else if (this.ShortTermTargetParty != null && this.ShortTermTargetParty.MapFaction == this.MapFaction)
//            behaviorText = new TextObject("{=yD7rL5Nc}Helping ally party to defend {TARGET_SETTLEMENT}.");
//        }
//        behaviorText.SetTextVariable("TARGET_SETTLEMENT", this.TargetSettlement.Name);
//      }
//      else if (this.DefaultBehavior == AiBehavior.RaidSettlement)
//      {
//        Settlement targetSettlement = this.TargetSettlement;
//        behaviorText = (double) Campaign.Current.Models.MapDistanceModel.GetDistance(this, targetSettlement, this.IsTargetingPort, this.NavigationCapability, out estimatedLandRatio) <= (double) Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(MobileParty.NavigationType.All) * 0.5 ? new TextObject("{=VtWa9Pmh}Raiding {TARGET_SETTLEMENT}.") : new TextObject("{=BqIRb85N}Going to raid {TARGET_SETTLEMENT}");
//        behaviorText.SetTextVariable("TARGET_SETTLEMENT", targetSettlement.Name);
//      }
//      else if (this.DefaultBehavior == AiBehavior.BesiegeSettlement)
//      {
//        behaviorText = new TextObject("{=JTxI3sW2}Besieging {TARGET_SETTLEMENT}.");
//        behaviorText.SetTextVariable("TARGET_SETTLEMENT", this.TargetSettlement.Name);
//      }
//      else if (this.ShortTermBehavior == AiBehavior.GoToPoint && this.DefaultBehavior != AiBehavior.EscortParty)
//      {
//        if (this.ShortTermTargetParty != null)
//        {
//          behaviorText = new TextObject("{=AcMayd1p}Running from {TARGET_PARTY}.");
//          behaviorText.SetTextVariable("TARGET_PARTY", this.ShortTermTargetParty.Name);
//        }
//        else if (this.TargetSettlement != null)
//        {
//          if (this.DefaultBehavior == AiBehavior.PatrolAroundPoint)
//          {
//            bool flag = this.IsLordParty && !this.AiBehaviorTarget.IsOnLand;
//            behaviorText = (double) Campaign.Current.Models.MapDistanceModel.GetDistance(this, this.TargetSettlement, this.IsTargetingPort, this.NavigationCapability, out estimatedLandRatio) <= (double) Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(MobileParty.NavigationType.All) * 0.5 ? (flag ? new TextObject("{=8qvUbTvW}Guarding the coastal waters off {TARGET_SETTLEMENT}.") : new TextObject("{=yUVv3z5V}Patrolling around {TARGET_SETTLEMENT}.")) : (flag ? new TextObject("{=avhlH79s}Heading to patrol the coastal waters off {TARGET_SETTLEMENT}.") : new TextObject("{=MNoogAgk}Heading to patrol around {TARGET_SETTLEMENT}."));
//            behaviorText.SetTextVariable("TARGET_SETTLEMENT", this.TargetSettlement != null ? this.TargetSettlement.Name : this.HomeSettlement.Name);
//          }
//          else
//            behaviorText = new TextObject("{=TaK6ydAx}Travelling.");
//        }
//        else
//          behaviorText = this.DefaultBehavior != AiBehavior.MoveToNearestLandOrPort ? (this.DefaultBehavior != AiBehavior.PatrolAroundPoint ? (!this.IsInRaftState ? new TextObject("{=XAL3t1bs}Going to a point.") : new TextObject("{=vxdIEThU}Drifting to shore.")) : new TextObject("{=BifGz0h4}Patrolling.")) : new TextObject("{=8vuOdcpy}Moving to the nearest shore.");
//      }
//      else if (this.ShortTermBehavior == AiBehavior.GoToSettlement || this.DefaultBehavior == AiBehavior.GoToSettlement)
//      {
//        if (this.ShortTermBehavior == AiBehavior.GoToSettlement && this.ShortTermTargetSettlement != null && this.ShortTermTargetSettlement != this.TargetSettlement)
//        {
//          if (this.DefaultBehavior == AiBehavior.MoveToNearestLandOrPort)
//          {
//            behaviorText = new TextObject("{=amHKbKfV}Running away from the sea.");
//            behaviorText.SetTextVariable("TARGET_PARTY", this.ShortTermTargetSettlement.Name);
//          }
//          else
//          {
//            behaviorText = this.ShortTermTargetParty == null || !this.ShortTermTargetParty.MapFaction.IsAtWarWith(this.MapFaction) ? new TextObject("{=EQHq3bHM}Travelling to {TARGET_PARTY}") : new TextObject("{=NRpbagbZ}Running to {TARGET_PARTY}.");
//            behaviorText.SetTextVariable("TARGET_PARTY", this.ShortTermTargetSettlement.Name);
//          }
//        }
//        else if (this.DefaultBehavior == AiBehavior.GoToSettlement && this.TargetSettlement != null)
//        {
//          behaviorText = this.CurrentSettlement != this.TargetSettlement ? new TextObject("{=EQHq3bHM}Travelling to {TARGET_PARTY}") : new TextObject("{=Y65gdbrx}Waiting in {TARGET_PARTY}.");
//          behaviorText.SetTextVariable("TARGET_PARTY", this.TargetSettlement.Name);
//        }
//        else if (this.ShortTermTargetParty != null)
//        {
//          behaviorText = new TextObject("{=AcMayd1p}Running from {TARGET_PARTY}.");
//          behaviorText.SetTextVariable("TARGET_PARTY", this.ShortTermTargetParty.Name);
//        }
//        else
//          behaviorText = new TextObject("{=QGyoSLeY}Traveling to a settlement.");
//      }
//      else if (this.ShortTermBehavior == AiBehavior.AssaultSettlement)
//      {
//        behaviorText = new TextObject("{=exnL6SS7}Attacking {TARGET_SETTLEMENT}.");
//        behaviorText.SetTextVariable("TARGET_SETTLEMENT", this.ShortTermTargetSettlement.Name);
//      }
//      else if (this.DefaultBehavior == AiBehavior.EscortParty || this.ShortTermBehavior == AiBehavior.EscortParty)
//      {
//        behaviorText = new TextObject("{=OpzzCPiP}Following {TARGET_PARTY}.");
//        behaviorText.SetTextVariable("TARGET_PARTY", this.ShortTermTargetParty != null ? this.ShortTermTargetParty.Name : this.TargetParty.Name);
//      }
//      else
//        behaviorText = this.DefaultBehavior != AiBehavior.MoveToNearestLandOrPort ? new TextObject("{=QXBf26Rv}Unknown Behavior.") : new TextObject("{=amHKbKfV}Running away from the sea.");
//    }
//    return behaviorText;
//  }

//  public override void Initialize()
//  {
//    base.Initialize();
//    this.Aggressiveness = 1f;
//    this.Ai = new MobilePartyAi(this);
//    this._formationPosition.x = 10000f;
//    this._formationPosition.y = 10000f;
//    while ((double) this._formationPosition.LengthSquared > 0.36000001430511475 || (double) this._formationPosition.LengthSquared < 0.2199999988079071)
//      this._formationPosition = new Vec2((float) ((double) MBRandom.RandomFloat * 1.2000000476837158 - 0.60000002384185791), (float) ((double) MBRandom.RandomFloat * 1.2000000476837158 - 0.60000002384185791));
//    CampaignEventDispatcher.Instance.OnPartyVisibilityChanged(this.Party);
//  }

//  private void InitMembers()
//  {
//    if (this._attachedParties != null)
//      return;
//    this._attachedParties = new MBList<MobileParty>();
//  }

//  public void InitializeMobilePartyAtPosition(CampaignVec2 position)
//  {
//    this.IsCurrentlyAtSea = !position.IsOnLand;
//    this.CreateFigure(position);
//    this.SetMoveModeHold();
//  }

//  public void InitializeMobilePartyAtPosition(
//    TroopRoster memberRoster,
//    TroopRoster prisonerRoster,
//    CampaignVec2 position,
//    bool isNaval = false)
//  {
//    this.InitializeMobilePartyWithRosterInternal(memberRoster, prisonerRoster, position);
//  }

//  public void InitializeMobilePartyAroundPosition(
//    TroopRoster memberRoster,
//    TroopRoster prisonerRoster,
//    CampaignVec2 position,
//    float spawnRadius,
//    float minSpawnRadius = 0.0f,
//    bool isNaval = false)
//  {
//    if ((double) spawnRadius > 0.0)
//    {
//      MobileParty.NavigationType navigationCapability = isNaval ? MobileParty.NavigationType.Naval : MobileParty.NavigationType.Default;
//      position = NavigationHelper.FindReachablePointAroundPosition(position, navigationCapability, spawnRadius, minSpawnRadius);
//    }
//    this.InitializeMobilePartyWithRosterInternal(memberRoster, prisonerRoster, position);
//  }

//  public void InitializeMobilePartyAtPosition(PartyTemplateObject pt, CampaignVec2 position)
//  {
//    this.InitializeMobilePartyWithPartyTemplate(pt, position);
//  }

//  private void InitializeMobilePartyWithPartyTemplate(PartyTemplateObject pt, CampaignVec2 position)
//  {
//    TroopRoster rosterForMobileParty = Campaign.Current.Models.PartySizeLimitModel.FindAppropriateInitialRosterForMobileParty(this, pt);
//    foreach (Ship ship in Campaign.Current.Models.PartySizeLimitModel.FindAppropriateInitialShipsForMobileParty(this, pt))
//      ChangeShipOwnerAction.ApplyByMobilePartyCreation(this.Party, ship);
//    this.InitializeMobilePartyWithRosterInternal(rosterForMobileParty, (TroopRoster) null, position);
//  }

//  private void InitializeMobilePartyWithRosterInternal(
//    TroopRoster memberRoster,
//    TroopRoster prisonerRoster,
//    CampaignVec2 position)
//  {
//    this.MemberRoster.Add(memberRoster);
//    if (prisonerRoster != null)
//      this.PrisonRoster.Add(prisonerRoster);
//    this.InitializeMobilePartyAtPosition(position);
//  }

//  public void InitializeMobilePartyAroundPosition(
//    PartyTemplateObject pt,
//    CampaignVec2 position,
//    float spawnRadius,
//    float minSpawnRadius = 0.0f)
//  {
//    if ((double) spawnRadius > 0.0)
//      position = NavigationHelper.FindReachablePointAroundPosition(position, !position.IsOnLand ? MobileParty.NavigationType.Naval : MobileParty.NavigationType.Default, spawnRadius, minSpawnRadius);
//    this.InitializeMobilePartyWithPartyTemplate(pt, position);
//  }

//  internal void InitializePartyForOldSave(
//    int numberOfRecentFleeingFromAParty,
//    AiBehavior defaultBehavior,
//    bool aiPathMode,
//    MoveModeType partyMoveMode,
//    Vec2 formationPosition,
//    MobileParty moveTargetParty,
//    Vec2 nextTargetPosition,
//    Vec2 moveTargetPoint,
//    Vec2 aiPathLastPosition)
//  {
//    this._defaultBehavior = defaultBehavior;
//    this._pathMode = aiPathMode;
//    this.PartyMoveMode = partyMoveMode;
//    this._formationPosition = formationPosition;
//    this.MoveTargetParty = moveTargetParty;
//    this.NextTargetPosition = new CampaignVec2(nextTargetPosition, true);
//    this.MoveTargetPoint = new CampaignVec2(moveTargetPoint, true);
//    this._pathLastPosition = new CampaignVec2(aiPathLastPosition, true);
//  }

//  [CachedData]
//  public PathFaceRecord CurrentNavigationFace => this.Position.Face;

//  [CachedData]
//  public int PathBegin { get; private set; }

//  [CachedData]
//  public bool ForceAiNoPathMode { get; set; }

//  internal void TickForStationaryMobileParty(
//    ref MobileParty.CachedPartyVariables variables,
//    float dt,
//    float realDt)
//  {
//    if (this.StationaryStartTime == CampaignTime.Never)
//      this.StationaryStartTime = CampaignTime.Now;
//    this.CheckIsDisorganized();
//    this.DoUpdatePosition(ref variables, dt, realDt);
//  }

//  internal void FillCurrentTickMoveDataForMovingMobileParty(
//    ref MobileParty.CachedPartyVariables variables,
//    float dt,
//    float realDt)
//  {
//    this.ComputeNextMoveDistance(ref variables, dt);
//    this.CommonMovingPartyTick(ref variables, dt, realDt);
//  }

//  internal void FillCurrentTickMoveDataForMovingArmyLeader(
//    ref MobileParty.CachedPartyVariables variables,
//    float dt,
//    float realDt)
//  {
//    this.ComputeNextMoveDistanceForArmyLeader(ref variables, dt);
//    this.CommonMovingPartyTick(ref variables, dt, realDt);
//  }

//  private void CommonMovingPartyTick(
//    ref MobileParty.CachedPartyVariables variables,
//    float dt,
//    float realDt)
//  {
//    this.StationaryStartTime = CampaignTime.Never;
//    this.CheckIsDisorganized();
//    this.DoAiPathMode(ref variables);
//    this.DoUpdatePosition(ref variables, dt, realDt);
//  }

//  internal void CommonTransitioningPartyTick(
//    ref MobileParty.CachedPartyVariables variables,
//    ref int navigationTypeChangeCounter,
//    ref MobileParty[] navigationTypeChangeList,
//    float dt)
//  {
//    if (this.ShouldEndTransition())
//    {
//      int index = Interlocked.Increment(ref navigationTypeChangeCounter);
//      navigationTypeChangeList[index] = this;
//    }
//    else
//    {
//      if ((double) dt <= 0.0)
//        return;
//      if (!this.HasNavalNavigationCapability)
//      {
//        this.CancelNavigationTransitionParallel();
//      }
//      else
//      {
//        if (this.CurrentSettlement != null)
//          return;
//        Vec2 vec2 = CampaignVec2.Normalized(variables.NextPosition - this.Position).ToVec2();
//        if (!vec2.IsNonZero())
//          return;
//        NavigationHelper.EmbarkDisembarkData embarkDisembarkData = this.IsMainParty ? NavigationHelper.GetEmbarkAndDisembarkDataForPlayer(this.Position, vec2, this.MoveTargetPoint, true) : NavigationHelper.GetEmbarkDisembarkDataForTick(this.Position, vec2);
//        PathFaceRecord face1 = embarkDisembarkData.TransitionEndPosition.Face;
//        PathFaceRecord face2 = this.EndPositionForNavigationTransition.Face;
//        if (!embarkDisembarkData.IsTargetingOwnSideOfTheDeadZone && face1.FaceIndex != this.CurrentNavigationFace.FaceIndex && face2.FaceIndex == face1.FaceIndex)
//          return;
//        this.CancelNavigationTransitionParallel();
//      }
//    }
//  }

//  internal void InitializeCachedPartyVariables(ref MobileParty.CachedPartyVariables variables)
//  {
//    variables.HasMapEvent = this.MapEvent != null;
//    variables.CurrentPosition = this.Position;
//    variables.TargetPartyPositionAtFrameStart = CampaignVec2.Invalid;
//    variables.LastCurrentPosition = this.Position;
//    variables.IsAttachedArmyMember = false;
//    variables.IsMoving = this.IsMoving;
//    variables.IsArmyLeader = false;
//    variables.NextPosition = this.Position;
//    variables.IsTransitionInProgress = this.IsTransitionInProgress;
//    if (this.Army == null)
//      return;
//    if (this.Army.LeaderParty == this)
//    {
//      variables.IsArmyLeader = true;
//    }
//    else
//    {
//      if (this.AttachedTo == null)
//        return;
//      variables.IsAttachedArmyMember = true;
//      variables.IsMoving = this.IsMoving || this.Army.LeaderParty.IsMoving;
//      variables.IsTransitionInProgress = this.Army.LeaderParty.IsTransitionInProgress;
//    }
//  }

//  internal void ComputeNextMoveDistanceForArmyLeader(
//    ref MobileParty.CachedPartyVariables variables,
//    float dt)
//  {
//    if ((double) dt > 0.0)
//    {
//      double speedForPartyUnified = (double) this.CalculateSpeedForPartyUnified();
//      variables.NextMoveDistance = this.Speed * dt;
//    }
//    else
//      variables.NextMoveDistance = 0.0f;
//  }

//  internal void ComputeNextMoveDistance(ref MobileParty.CachedPartyVariables variables, float dt)
//  {
//    if ((double) dt > 0.0)
//    {
//      double speed = (double) this.CalculateSpeed();
//      variables.NextMoveDistance = this.Speed * dt;
//    }
//    else
//      variables.NextMoveDistance = 0.0f;
//  }

//  internal void UpdateStationaryTimer()
//  {
//    if (!this.IsMoving)
//    {
//      if (!(this.StationaryStartTime == CampaignTime.Never))
//        return;
//      this.StationaryStartTime = CampaignTime.Now;
//    }
//    else
//      this.StationaryStartTime = CampaignTime.Never;
//  }

//  private void CheckIsDisorganized()
//  {
//    if (!this._isDisorganized || !this._disorganizedUntilTime.IsPast)
//      return;
//    this.SetDisorganized(false);
//  }

//  public void SetDisorganized(bool isDisorganized)
//  {
//    if (isDisorganized)
//      this._disorganizedUntilTime = CampaignTime.HoursFromNow(Campaign.Current.Models.PartyImpairmentModel.GetDisorganizedStateDuration(this).ResultNumber);
//    this._isDisorganized = isDisorganized;
//    this.UpdateVersionNo();
//  }

//  internal void CacheTargetPartyVariablesAtFrameStart(ref MobileParty.CachedPartyVariables variables)
//  {
//    if (this.MoveTargetParty == null)
//      return;
//    variables.TargetPartyPositionAtFrameStart = this.MoveTargetParty.Position;
//    variables.IsTargetMovingAtFrameStart = this.MoveTargetParty.IsMoving || this.MoveTargetParty.IsTransitionInProgress;
//  }

//  public void RecalculateShortTermBehavior()
//  {
//    if (this.DefaultBehavior == AiBehavior.RaidSettlement)
//      this.SetShortTermBehavior(AiBehavior.RaidSettlement, (IInteractablePoint) this.TargetSettlement.Party);
//    else if (this.DefaultBehavior == AiBehavior.BesiegeSettlement)
//      this.SetShortTermBehavior(AiBehavior.BesiegeSettlement, (IInteractablePoint) this.TargetSettlement.Party);
//    else if (this.DefaultBehavior == AiBehavior.GoToSettlement)
//      this.SetShortTermBehavior(AiBehavior.GoToSettlement, (IInteractablePoint) this.TargetSettlement.Party);
//    else if (this.DefaultBehavior == AiBehavior.EngageParty)
//      this.SetShortTermBehavior(AiBehavior.EngageParty, (IInteractablePoint) this.TargetParty.Party);
//    else if (this.DefaultBehavior == AiBehavior.DefendSettlement)
//      this.SetShortTermBehavior(AiBehavior.GoToPoint, (IInteractablePoint) this.TargetSettlement.Party);
//    else if (this.DefaultBehavior == AiBehavior.EscortParty)
//      this.SetShortTermBehavior(AiBehavior.EscortParty, (IInteractablePoint) this.TargetParty.Party);
//    else if (this.DefaultBehavior == AiBehavior.GoToPoint)
//      this.SetShortTermBehavior(AiBehavior.GoToPoint, this.Ai.AiBehaviorInteractable);
//    else if (this.DefaultBehavior == AiBehavior.MoveToNearestLandOrPort)
//    {
//      this.SetShortTermBehavior(AiBehavior.GoToPoint, (IInteractablePoint) null);
//    }
//    else
//    {
//      if (this.DefaultBehavior != AiBehavior.None)
//        return;
//      this.ShortTermBehavior = AiBehavior.None;
//    }
//  }

//  internal void SetShortTermBehavior(AiBehavior newBehavior, IInteractablePoint mapEntity)
//  {
//    if (this.ShortTermBehavior != newBehavior)
//      this.ShortTermBehavior = newBehavior;
//    if (!this.IsMainParty && this.DefaultBehavior == AiBehavior.Hold && this.DesiredAiNavigationType == MobileParty.NavigationType.None && !this.IsMilitia && !this.IsGarrison)
//      this.DesiredAiNavigationType = this.IsCurrentlyAtSea ? (this.HasNavalNavigationCapability ? MobileParty.NavigationType.Naval : MobileParty.NavigationType.None) : MobileParty.NavigationType.Default;
//    this.Ai.AiBehaviorInteractable = mapEntity;
//  }

//  public static bool IsFleeBehavior(AiBehavior aiBehavior)
//  {
//    return aiBehavior == AiBehavior.FleeToPoint || aiBehavior == AiBehavior.FleeToGate || aiBehavior == AiBehavior.FleeToParty;
//  }

//  public bool IsFleeing()
//  {
//    return MobileParty.IsFleeBehavior(this.ShortTermBehavior) || MobileParty.IsFleeBehavior(this.DefaultBehavior);
//  }

//  private void UpdatePathModeWithPosition(CampaignVec2 newTargetPosition)
//  {
//    this.MoveTargetPoint = newTargetPosition;
//    NavigationHelper.IsPositionValidForNavigationType(newTargetPosition, this.NavigationCapability);
//  }

//  internal void TryToMoveThePartyWithCurrentTickMoveData(
//    ref MobileParty.CachedPartyVariables variables,
//    ref int gridChangeCount,
//    ref MobileParty[] gridChangeMobilePartyList)
//  {
//    if ((double) variables.NextMoveDistance <= 0.0 || !variables.IsMoving || this.BesiegedSettlement != null || variables.HasMapEvent)
//      return;
//    this.CheckTransitionParallel(ref variables, ref gridChangeCount, ref gridChangeMobilePartyList);
//    if (this.IsTransitionInProgress && !this.IsInRaftState)
//      return;
//    if (variables.IsAttachedArmyMember && (double) (this.Army.LeaderParty.Position.ToVec2() - (this.Position.ToVec2() + this.ArmyPositionAdder + (variables.NextPosition - this.Position).ToVec2())).Length > 9.9999997473787516E-06)
//    {
//      CampaignVec2 position = this.Army.LeaderParty.Position;
//      this.SetPositionParallel(in position, ref gridChangeCount, ref gridChangeMobilePartyList);
//      Vec2 armyPositionAdder = this.ArmyPositionAdder;
//      Vec2 vec2_1 = variables.NextPosition.ToVec2();
//      position = this.Position;
//      Vec2 vec2_2 = position.ToVec2();
//      Vec2 vec2_3 = vec2_1 - vec2_2;
//      this.ArmyPositionAdder = armyPositionAdder + vec2_3;
//    }
//    else
//    {
//      if (!this.CurrentNavigationFace.IsValid() || this.CurrentNavigationFace.FaceIslandIndex != variables.NextPosition.Face.FaceIslandIndex)
//        return;
//      this.SetPositionParallel(in variables.NextPosition, ref gridChangeCount, ref gridChangeMobilePartyList);
//    }
//  }

//  internal void CheckTransitionParallel(
//    ref MobileParty.CachedPartyVariables variables,
//    ref int gridChangeCount,
//    ref MobileParty[] gridChangeMobilePartyList)
//  {
//    if (this.AttachedTo != null || this.IsMainParty && !this.IsCurrentlyAtSea || this.IsTransitionInProgress || !this.CurrentNavigationFace.IsValid() || (this.IsCurrentlyAtSea ? (this.HasLandNavigationCapability ? 1 : 0) : (this.HasNavalNavigationCapability ? 1 : 0)) == 0)
//      return;
//    Vec2 vec2 = CampaignVec2.Normalized(variables.NextPosition - variables.CurrentPosition).ToVec2();
//    NavigationHelper.EmbarkDisembarkData embarkDisembarkData = this.IsMainParty ? NavigationHelper.GetEmbarkAndDisembarkDataForPlayer(this.Position, vec2, this.MoveTargetPoint, true) : NavigationHelper.GetEmbarkDisembarkDataForTick(this.Position, vec2);
//    if (!embarkDisembarkData.IsValidTransition || embarkDisembarkData.IsTargetingOwnSideOfTheDeadZone)
//      return;
//    TerrainType faceGroupIndex = (TerrainType) embarkDisembarkData.TransitionEndPosition.Face.FaceGroupIndex;
//    if (Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(faceGroupIndex, this.IsCurrentlyAtSea ? MobileParty.NavigationType.Naval : MobileParty.NavigationType.Default) || !Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(faceGroupIndex, MobileParty.NavigationType.All))
//      return;
//    float num1 = embarkDisembarkData.TransitionStartPosition.Distance(embarkDisembarkData.NavMeshEdgePosition);
//    float num2 = embarkDisembarkData.NavMeshEdgePosition.Distance(variables.CurrentPosition);
//    if ((double) this.MoveTargetPoint.Distance(variables.CurrentPosition) <= (double) num2 || (double) num2 >= (double) num1 && (double) num2 >= (double) variables.NextMoveDistance)
//      return;
//    this.InitializeNavigationTransitionParallel(embarkDisembarkData.TransitionStartPosition, embarkDisembarkData.TransitionEndPosition, ref gridChangeCount, ref gridChangeMobilePartyList);
//    variables.NextMoveDistance = 0.0f;
//  }

//  public Vec2 EventPositionAdder
//  {
//    get => this._eventPositionAdder;
//    set => this._eventPositionAdder = value;
//  }

//  private bool ShouldEndTransition()
//  {
//    return this.IsTransitionInProgress && (this.NavigationTransitionStartTime + this.NavigationTransitionDuration).IsPast && this.AttachedParties.All<MobileParty>((Func<MobileParty, bool>) (x => x.ShouldEndTransition()));
//  }

//  public bool IsVisible
//  {
//    get => this._isVisible;
//    set
//    {
//      if (this._isVisible == value)
//        return;
//      this._isVisible = value;
//      this.Party.OnVisibilityChanged(value);
//      if (!this.IsCurrentlyAtSea || this.IsTransitionInProgress)
//      {
//        SiegeEvent siegeEvent = this.SiegeEvent;
//        if ((siegeEvent != null ? (siegeEvent.IsBlockadeActive ? 1 : 0) : 0) == 0)
//          return;
//      }
//      this.SetNavalVisualAsDirty();
//    }
//  }

//  public CampaignVec2 Position
//  {
//    get => this._position;
//    set
//    {
//      if (!(this._position != value))
//        return;
//      this._position = value;
//      Campaign.Current.MobilePartyLocator.UpdateLocator(this);
//    }
//  }

//  private void SetPositionParallel(
//    in CampaignVec2 value,
//    ref int gridChangeCounter,
//    ref MobileParty[] gridChangeList)
//  {
//    if (!(this.Position != value))
//      return;
//    this._lastNavigationFace = this.Position.Face;
//    this._position = value;
//    if (Campaign.Current.MobilePartyLocator.CheckWhetherPositionsAreInSameNode(value.ToVec2(), (ILocatable<MobileParty>) this))
//      return;
//    int index = Interlocked.Increment(ref gridChangeCounter);
//    gridChangeList[index] = this;
//  }

//  public void SetPartyUsedByQuest(bool isActivelyUsed)
//  {
//    if (this._isCurrentlyUsedByAQuest == isActivelyUsed)
//      return;
//    this._isCurrentlyUsedByAQuest = isActivelyUsed;
//    CampaignEventDispatcher.Instance.OnMobilePartyQuestStatusChanged(this, isActivelyUsed);
//  }

//  public bool IsInspected
//  {
//    get => this.Army != null && this.Army == MobileParty.MainParty.Army || this._isInspected;
//    set => this._isInspected = value;
//  }

//  public void IgnoreForHours(float hours)
//  {
//    this._ignoredUntilTime = CampaignTime.HoursFromNow(hours);
//  }

//  public void IgnoreByOtherPartiesTill(CampaignTime time) => this._ignoredUntilTime = time;

//  public Vec2 GetPosition2D => this.Position.ToVec2();

//  public int TotalWage
//  {
//    get
//    {
//      return (int) Campaign.Current.Models.PartyWageModel.GetTotalWage(this, this.MemberRoster).ResultNumber;
//    }
//  }

//  public ExplainedNumber TotalWageExplained
//  {
//    get => Campaign.Current.Models.PartyWageModel.GetTotalWage(this, this.MemberRoster, true);
//  }

//  public MapEvent MapEvent => this.Party.MapEvent;

//  private void OnRemoveParty()
//  {
//    this.Army = (Army) null;
//    this.CurrentSettlement = (Settlement) null;
//    this.AttachedTo = (MobileParty) null;
//    this.BesiegerCamp = (BesiegerCamp) null;
//    List<Settlement> settlementList = new List<Settlement>();
//    if (this.CurrentSettlement != null)
//      settlementList.Add(this.CurrentSettlement);
//    else if ((this.IsGarrison || this.IsMilitia || this.IsBandit || this.IsVillager || this.IsPatrolParty) && this.HomeSettlement != null)
//      settlementList.Add(this.HomeSettlement);
//    this.PartyComponent?.Finish();
//    this.ActualClan = (Clan) null;
//    this.Anchor = (AnchorPoint) null;
//    Campaign.Current.CampaignObjectManager.RemoveMobileParty(this);
//    foreach (Settlement settlement in settlementList)
//      settlement.SettlementComponent.OnRelatedPartyRemoved(this);
//  }

//  public void SetAnchor(AnchorPoint anchor)
//  {
//    this.Anchor = anchor;
//    this.Anchor.InitializeOnLoad(this);
//  }

//  public void SetPartyObjective(MobileParty.PartyObjective objective) => this.Objective = objective;

//  public void UpdateVersionNo() => ++this.VersionNo;

//  public TroopRoster MemberRoster => this.Party.MemberRoster;

//  public TroopRoster PrisonRoster => this.Party.PrisonRoster;

//  private bool IsLastSpeedCacheInvalid()
//  {
//    Hero leaderHero = this.LeaderHero;
//    bool flag = !this.IsActive || leaderHero == null || leaderHero.PartyBelongedToAsPrisoner != null;
//    bool isNight = Campaign.Current.IsNight;
//    Vec2 vec2 = this._lastWind;
//    if (this.IsCurrentlyAtSea)
//      vec2 = Campaign.Current.Models.MapWeatherModel.GetWindForPosition(this.Position);
//    return this._lastNavigationFace.FaceIndex != this.CurrentNavigationFace.FaceIndex || this._partyLastCheckIsPrisoner != flag || this._partyLastCheckAtNight != isNight || (double) Math.Abs(this._lastWind.RotationInRadians - vec2.RotationInRadians) > 0.059999998658895493 || (double) Math.Abs(this._lastWind.LengthSquared - vec2.LengthSquared) > 9.9999997473787516E-05;
//  }

//  private bool IsBaseSpeedCacheInvalid()
//  {
//    this.UpdateCommonCacheVersions();
//    MapWeatherModel.WeatherEventEffectOnTerrain terrainForPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEffectOnTerrainForPosition(this.Position.ToVec2());
//    return this._partyPureSpeedLastCheckVersion != this.GetVersionNoForBaseSpeedCalculation() || this._lastWeatherTerrainEffect != terrainForPosition;
//  }

//  private int GetVersionNoForWeightCalculation()
//  {
//    return this.IsCurrentlyAtSea ? (17 * 31 /*0x1F*/ + this.VersionNo) * 31 /*0x1F*/ + this.Party.GetShipsVersion() : this.VersionNo;
//  }

//  private int GetVersionNoForBaseSpeedCalculation() => this.GetVersionNoForWeightCalculation();

//  private float CalculateSpeedForPartyUnified()
//  {
//    bool flag1 = false;
//    if (this.IsBaseSpeedCacheInvalid())
//    {
//      this._lastCalculatedBaseSpeedExplained = this.Army == null || !this.Army.LeaderParty.AttachedParties.Contains(this) ? Campaign.Current.Models.PartySpeedCalculatingModel.CalculateBaseSpeed(this) : this.Army.LeaderParty._lastCalculatedBaseSpeedExplained;
//      this._lastWeatherTerrainEffect = Campaign.Current.Models.MapWeatherModel.GetWeatherEffectOnTerrainForPosition(this.Position.ToVec2());
//      this._partyPureSpeedLastCheckVersion = this.GetVersionNoForBaseSpeedCalculation();
//      flag1 = true;
//    }
//    if (flag1)
//      this._lastCalculatedSpeed = Campaign.Current.Models.PartySpeedCalculatingModel.CalculateFinalSpeed(this, this._lastCalculatedBaseSpeedExplained).ResultNumber;
//    else if (this.IsLastSpeedCacheInvalid())
//    {
//      Hero leaderHero = this.LeaderHero;
//      bool flag2 = !this.IsActive || leaderHero == null || leaderHero.PartyBelongedToAsPrisoner != null;
//      bool isNight = Campaign.Current.IsNight;
//      if (this.IsCurrentlyAtSea)
//        this._lastWind = Campaign.Current.Models.MapWeatherModel.GetWindForPosition(this.Position);
//      this._lastNavigationFace = this.CurrentNavigationFace;
//      this._partyLastCheckIsPrisoner = flag2;
//      this._partyLastCheckAtNight = isNight;
//      this._lastCalculatedSpeed = Campaign.Current.Models.PartySpeedCalculatingModel.CalculateFinalSpeed(this, this._lastCalculatedBaseSpeedExplained).ResultNumber;
//    }
//    return this._lastCalculatedSpeed;
//  }

//  private bool IsWeightCacheInvalid()
//  {
//    this.UpdateCommonCacheVersions();
//    return this._partyWeightLastCheckVersionNo != this.GetVersionNoForWeightCalculation();
//  }

//  private void UpdateCommonCacheVersions()
//  {
//    if (this._itemRosterVersionNo == this.Party.ItemRoster.VersionNo)
//      return;
//    this._itemRosterVersionNo = this.ItemRoster.VersionNo;
//    this.UpdateVersionNo();
//  }

//  private float CalculateSpeed()
//  {
//    if (this.Army != null)
//    {
//      if (this.Army.LeaderParty.AttachedParties.Contains(this))
//      {
//        CampaignVec2 position;
//        Vec2 vec2_1;
//        if (this.Army.LeaderParty.MapEvent == null)
//        {
//          vec2_1 = this.Army.LeaderParty.NextTargetPosition.ToVec2();
//        }
//        else
//        {
//          position = this.Army.LeaderParty.Position;
//          vec2_1 = position.ToVec2();
//        }
//        position = this.Army.LeaderParty.Position;
//        Vec2 vec2_2 = position.ToVec2();
//        Vec2 vec2_3 = vec2_1 - vec2_2;
//        Vec2 armyFacing = vec2_3.Normalized();
//        position = this.Army.LeaderParty.Position;
//        Vec2 vec2_4 = position.ToVec2() + armyFacing.TransformToParentUnitF(this.Army.GetRelativePositionForParty(this, armyFacing));
//        vec2_3 = this.Bearing;
//        return this.Army.LeaderParty._lastCalculatedSpeed * MBMath.ClampFloat((float) (1.0 + (double) vec2_3.DotProduct(vec2_4 - this.VisualPosition2DWithoutError) * 1.0), 0.7f, 1.3f);
//      }
//    }
//    else if (this.DefaultBehavior == AiBehavior.EscortParty && this.TargetParty != null && (double) this._lastCalculatedSpeed > (double) this.TargetParty._lastCalculatedSpeed)
//      return this.TargetParty._lastCalculatedSpeed;
//    return this.CalculateSpeedForPartyUnified();
//  }

//  public ItemRoster ItemRoster => this.Party.ItemRoster;

//  public bool IsSpotted() => this.IsVisible;

//  public bool IsMainParty => this == MobileParty.MainParty;

//  public int AddElementToMemberRoster(CharacterObject element, int numberToAdd, bool insertAtFront = false)
//  {
//    return this.Party.AddElementToMemberRoster(element, numberToAdd, insertAtFront);
//  }

//  public int AddPrisoner(CharacterObject element, int numberToAdd)
//  {
//    return this.Party.AddPrisoner(element, numberToAdd);
//  }

//  public IFaction MapFaction
//  {
//    get
//    {
//      return this.ActualClan == null ? (this.Party.Owner == null ? (this.HomeSettlement == null ? (this.LeaderHero != null ? this.LeaderHero.MapFaction : (IFaction) null) : this.HomeSettlement.OwnerClan.MapFaction) : (this.Party.Owner != Hero.MainHero ? (!this.Party.Owner.IsNotable ? (!this.IsMilitia && !this.IsGarrison && !this.IsVillager && !this.IsPatrolParty || this.HomeSettlement?.OwnerClan == null ? (this.IsCaravan || this.IsBanditBossParty ? this.Party.Owner.MapFaction : (!this._isCurrentlyUsedByAQuest || this.Party.Owner == null ? (this.LeaderHero != null ? this.LeaderHero.MapFaction : (IFaction) null) : this.Party.Owner.MapFaction)) : this.HomeSettlement.OwnerClan.MapFaction) : this.Party.Owner.HomeSettlement.MapFaction) : this.Party.Owner.MapFaction)) : this.ActualClan.MapFaction;
//    }
//  }

//  public TextObject ArmyName
//  {
//    get => this.Army == null || this.Army.LeaderParty != this ? this.Name : this.Army.Name;
//  }

//  public SiegeEvent SiegeEvent => this.BesiegerCamp?.SiegeEvent;

//  public float Food
//  {
//    get => (float) this.Party.RemainingFoodPercentage * 0.01f + (float) this.TotalFoodAtInventory;
//  }

//  public Vec3 GetPositionAsVec3() => this.Position.AsVec3();

//  public int TotalFoodAtInventory => this.ItemRoster.TotalFood;

//  public float SeeingRange
//  {
//    get => Campaign.Current.Models.MapVisibilityModel.GetPartySpottingRange(this).ResultNumber;
//  }

//  public Settlement BesiegedSettlement => this.BesiegerCamp?.SiegeEvent.BesiegedSettlement;

//  public float GetTotalLandStrengthWithFollowers(bool includeNonAttachedArmyMembers = true)
//  {
//    MobileParty mobileParty = this.DefaultBehavior == AiBehavior.EscortParty ? this.TargetParty : this;
//    float powerOfParty = Campaign.Current.Models.MilitaryPowerModel.GetPowerOfParty(mobileParty.Party, BattleSideEnum.Attacker, MapEvent.PowerCalculationContext.PlainBattle);
//    if (mobileParty.Army != null && mobileParty == mobileParty.Army.LeaderParty)
//    {
//      foreach (MobileParty party in (List<MobileParty>) mobileParty.Army.Parties)
//      {
//        if (party.Army.LeaderParty != party && party.AttachedTo != null | includeNonAttachedArmyMembers)
//          powerOfParty += Campaign.Current.Models.MilitaryPowerModel.GetPowerOfParty(party.Party, BattleSideEnum.Attacker, MapEvent.PowerCalculationContext.PlainBattle);
//      }
//    }
//    return powerOfParty;
//  }

//  private void OnPartyJoinedSiegeInternal()
//  {
//    this._besiegerCamp.AddSiegePartyInternal(this);
//    Town town = this.SiegeEvent.BesiegedSettlement.Town;
//    CampaignVec2 campPartyPosition = this._besiegerCamp.GetSiegeCampPartyPosition(this, town.BesiegerCampPositions1, town.BesiegerCampPositions2);
//    this.Position = !campPartyPosition.IsValid() ? town.Settlement.GatePosition : campPartyPosition;
//    if (this.IsMainParty && this.SiegeEvent.IsBlockadeActive)
//      this.Anchor.IsDisabled = true;
//    CampaignEventDispatcher.Instance.OnMobilePartyJoinedToSiegeEvent(this);
//  }

//  private void OnPartyLeftSiegeInternal()
//  {
//    if (this.IsMainParty && this.SiegeEvent.IsBlockadeActive)
//      this.Anchor.IsDisabled = false;
//    this._besiegerCamp.RemoveSiegePartyInternal(this);
//    this.EventPositionAdder = Vec2.Zero;
//    CampaignEventDispatcher.Instance.OnMobilePartyLeftSiegeEvent(this);
//  }

//  public bool HasPerk(PerkObject perk, bool checkSecondaryRole = false)
//  {
//    switch (checkSecondaryRole ? (int) perk.SecondaryRole : (int) perk.PrimaryRole)
//    {
//      case 2:
//        return this.LeaderHero != null && (this.LeaderHero.Clan?.Leader?.GetPerkValue(perk) ?? false);
//      case 4:
//        return this.Army?.LeaderParty?.LeaderHero?.GetPerkValue(perk) ?? false;
//      case 5:
//        Hero leaderHero1 = this.LeaderHero;
//        return leaderHero1 != null && leaderHero1.GetPerkValue(perk);
//      case 7:
//        Hero effectiveSurgeon = this.EffectiveSurgeon;
//        return effectiveSurgeon != null && effectiveSurgeon.GetPerkValue(perk);
//      case 8:
//        Hero effectiveEngineer = this.EffectiveEngineer;
//        return effectiveEngineer != null && effectiveEngineer.GetPerkValue(perk);
//      case 9:
//        Hero effectiveScout = this.EffectiveScout;
//        return effectiveScout != null && effectiveScout.GetPerkValue(perk);
//      case 10:
//        Hero effectiveQuartermaster = this.EffectiveQuartermaster;
//        return effectiveQuartermaster != null && effectiveQuartermaster.GetPerkValue(perk);
//      case 11:
//        foreach (TroopRosterElement troopRosterElement in (List<TroopRosterElement>) this.MemberRoster.GetTroopRoster())
//        {
//          if (troopRosterElement.Character.IsHero && troopRosterElement.Character.HeroObject.GetPerkValue(perk))
//            return true;
//        }
//        return false;
//      case 12:
//        Debug.FailedAssert("personal perk is called in party", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Party\\MobileParty.cs", nameof (HasPerk), 3038);
//        Hero leaderHero2 = this.LeaderHero;
//        return leaderHero2 != null && leaderHero2.GetPerkValue(perk);
//      default:
//        return false;
//    }
//  }

//  public bool IsEngaging
//  {
//    get
//    {
//      return this.DefaultBehavior == AiBehavior.EngageParty || this.ShortTermBehavior == AiBehavior.EngageParty;
//    }
//  }

//  public void SetHeroPartyRole(Hero hero, PartyRole partyRole)
//  {
//    switch (partyRole)
//    {
//      case PartyRole.Surgeon:
//        this.SetPartySurgeon(hero);
//        break;
//      case PartyRole.Engineer:
//        this.SetPartyEngineer(hero);
//        break;
//      case PartyRole.Scout:
//        this.SetPartyScout(hero);
//        break;
//      case PartyRole.Quartermaster:
//        this.SetPartyQuartermaster(hero);
//        break;
//    }
//  }

//  public PartyRole GetHeroPartyRole(Hero hero)
//  {
//    if (this.Engineer == hero)
//      return PartyRole.Engineer;
//    if (this.Quartermaster == hero)
//      return PartyRole.Quartermaster;
//    if (this.Surgeon == hero)
//      return PartyRole.Surgeon;
//    return this.Scout == hero ? PartyRole.Scout : PartyRole.None;
//  }

//  public void RemoveHeroPartyRole(Hero hero)
//  {
//    if (this.Engineer == hero)
//      this.Engineer = (Hero) null;
//    if (this.Quartermaster == hero)
//      this.Quartermaster = (Hero) null;
//    if (this.Surgeon == hero)
//      this.Surgeon = (Hero) null;
//    if (this.Scout == hero)
//      this.Scout = (Hero) null;
//    this.ResetCached();
//  }

//  public Hero GetRoleHolder(PartyRole partyRole)
//  {
//    switch (partyRole)
//    {
//      case PartyRole.PartyLeader:
//        return this.LeaderHero;
//      case PartyRole.Surgeon:
//        return this.Surgeon;
//      case PartyRole.Engineer:
//        return this.Engineer;
//      case PartyRole.Scout:
//        return this.Scout;
//      case PartyRole.Quartermaster:
//        return this.Quartermaster;
//      default:
//        return (Hero) null;
//    }
//  }

//  public Hero GetEffectiveRoleHolder(PartyRole partyRole)
//  {
//    switch (partyRole)
//    {
//      case PartyRole.PartyLeader:
//        return this.LeaderHero;
//      case PartyRole.Surgeon:
//        return this.EffectiveSurgeon;
//      case PartyRole.Engineer:
//        return this.EffectiveEngineer;
//      case PartyRole.Scout:
//        return this.EffectiveScout;
//      case PartyRole.Quartermaster:
//        return this.EffectiveQuartermaster;
//      default:
//        return (Hero) null;
//    }
//  }

//  internal bool IsCurrentlyEngagingSettlement
//  {
//    get
//    {
//      return this.ShortTermBehavior == AiBehavior.GoToSettlement || this.ShortTermBehavior == AiBehavior.RaidSettlement || this.ShortTermBehavior == AiBehavior.AssaultSettlement || this.ShortTermBehavior == AiBehavior.FleeToGate;
//    }
//  }

//  internal bool IsCurrentlyEngagingParty => this.ShortTermBehavior == AiBehavior.EngageParty;

//  public int GetNumDaysForFoodToLast()
//  {
//    int num = this.ItemRoster.TotalFood * 100;
//    if (this == MobileParty.MainParty)
//      num += this.Party.RemainingFoodPercentage;
//    return (int) ((double) num / (100.0 * -(double) this.FoodChange));
//  }

//  TextObject ITrackableBase.GetName() => this.Name;

//  Vec3 ITrackableBase.GetPosition() => this.GetPositionAsVec3();

//  Banner ITrackableCampaignObject.GetBanner() => this.Banner;

//  public float PartySizeRatio
//  {
//    get
//    {
//      int versionNo = this.Party.MemberRoster.VersionNo;
//      float cachedPartySizeRatio = this._cachedPartySizeRatio;
//      if (this._partySizeRatioLastCheckVersion != versionNo || this == MobileParty.MainParty)
//      {
//        this._partySizeRatioLastCheckVersion = versionNo;
//        this._cachedPartySizeRatio = (float) this.Party.NumberOfAllMembers / (float) this.Party.PartySizeLimit;
//        cachedPartySizeRatio = this._cachedPartySizeRatio;
//      }
//      return cachedPartySizeRatio;
//    }
//  }

//  public Vec2 VisualPosition2DWithoutError
//  {
//    get => this.Position.ToVec2() + this.EventPositionAdder + this.ArmyPositionAdder;
//  }

//  private Settlement DetermineRelatedBesiegedSettlementWhileDestroyingParty()
//  {
//    Settlement whileDestroyingParty = this.BesiegedSettlement ?? (this.ShortTermBehavior == AiBehavior.AssaultSettlement ? this.ShortTermTargetSettlement : (Settlement) null);
//    if (whileDestroyingParty == null && (this.IsGarrison || this.IsMilitia) && this.CurrentSettlement != null)
//    {
//      MapEvent mapEvent = this.CurrentSettlement.LastAttackerParty?.MapEvent;
//      if (mapEvent != null && (mapEvent.IsSiegeAssault || mapEvent.IsSiegeOutside || mapEvent.IsSallyOut) && mapEvent.DefeatedSide != BattleSideEnum.None && mapEvent.State == MapEventState.WaitingRemoval)
//        whileDestroyingParty = this.CurrentSettlement;
//    }
//    return whileDestroyingParty;
//  }

//  internal void RemoveParty()
//  {
//    this.IsActive = false;
//    this.IsVisible = false;
//    Settlement whileDestroyingParty = this.DetermineRelatedBesiegedSettlementWhileDestroyingParty();
//    Campaign current = Campaign.Current;
//    this.AttachedTo = (MobileParty) null;
//    this.BesiegerCamp = (BesiegerCamp) null;
//    this.ReleaseHeroPrisoners();
//    this.ItemRoster.Clear();
//    this.MemberRoster.Clear();
//    this.PrisonRoster.Clear();
//    for (int index = this.Ships.Count - 1; index >= 0; --index)
//      DestroyShipAction.Apply(this.Ships[index]);
//    Campaign.Current.MobilePartyLocator.RemoveLocatable(this);
//    Campaign.Current.VisualTrackerManager.RemoveTrackedObject((ITrackableBase) this, true);
//    CampaignEventDispatcher.Instance.OnPartyRemoved(this.Party);
//    GC.SuppressFinalize((object) this.Party);
//    foreach (MobileParty mobileParty in (List<MobileParty>) current.MobileParties)
//    {
//      bool flag = (mobileParty.Ai.AiBehaviorPartyBase == this.Party || mobileParty.TargetSettlement != null && mobileParty.TargetSettlement == whileDestroyingParty && mobileParty.CurrentSettlement != whileDestroyingParty || mobileParty.ShortTermTargetSettlement != null && mobileParty.ShortTermTargetSettlement == whileDestroyingParty && mobileParty.CurrentSettlement != whileDestroyingParty) && !mobileParty.IsInRaftState && mobileParty.MapEvent == null;
//      if (mobileParty.TargetParty != null && mobileParty.TargetParty == this)
//        flag = true;
//      if (flag && mobileParty.TargetSettlement != null && (mobileParty.MapEvent == null || mobileParty.MapEvent.IsFinalized) && mobileParty.DefaultBehavior == AiBehavior.GoToSettlement)
//      {
//        Settlement targetSettlement = mobileParty.TargetSettlement;
//        mobileParty.SetMoveModeHold();
//        mobileParty.SetNavigationModeHold();
//        mobileParty.Ai.RethinkAtNextHourlyTick = true;
//        flag = false;
//      }
//      if (flag)
//      {
//        mobileParty.SetMoveModeHold();
//        mobileParty.SetNavigationModeHold();
//      }
//    }
//    this.OnRemoveParty();
//    this._customHomeSettlement = (Settlement) null;
//  }

//  public bool IsMoving
//  {
//    get
//    {
//      return this.IsMainParty ? !this.IsTransitionInProgress && !Campaign.Current.IsMainPartyWaiting : this.IsActive && !this.IsTransitionInProgress && this.CurrentSettlement == null && this.MapEvent == null && this.BesiegedSettlement == null && this.Position != this.MoveTargetPoint;
//    }
//  }

//  private void ReleaseHeroPrisoners()
//  {
//    for (int index = this.PrisonRoster.Count - 1; index >= 0; --index)
//    {
//      if (this.PrisonRoster.GetElementNumber(index) > 0)
//      {
//        TroopRosterElement elementCopyAtIndex = this.PrisonRoster.GetElementCopyAtIndex(index);
//        if (elementCopyAtIndex.Character.IsHero && !elementCopyAtIndex.Character.IsPlayerCharacter)
//          EndCaptivityAction.ApplyByReleasedByChoice(elementCopyAtIndex.Character.HeroObject);
//      }
//    }
//  }

//  public bool ShouldBeIgnored => this._ignoredUntilTime.IsFuture || this.IsInRaftState;

//  private void CreateFigureAux(CampaignVec2 position)
//  {
//    IMapScene mapSceneWrapper = Campaign.Current.MapSceneWrapper;
//    this.Position = position;
//    Vec2 vec2 = new Vec2(1f, 0.0f);
//    float angleInRadians = (float) ((double) MBRandom.RandomFloat * 2.0 * 3.1415927410125732);
//    vec2.RotateCCW(angleInRadians);
//    this.Bearing = vec2;
//    this.Party.UpdateVisibilityAndInspected(MobileParty.MainParty.Position);
//    this.StartUp();
//  }

//  private void CreateFigure(CampaignVec2 position) => this.CreateFigureAux(position);

//  internal void TeleportPartyToOutSideOfEncounterRadius()
//  {
//    float num = this.Army == null || this.AttachedTo == null ? Campaign.Current.Models.EncounterModel.NeededMaximumDistanceForEncounteringMobileParty : Campaign.Current.Models.EncounterModel.MaximumAllowedDistanceForEncounteringMobilePartyInArmy;
//    float maxDistance = num * 1.25f;
//    MobileParty.NavigationType navigationCapability = this.IsCurrentlyAtSea ? MobileParty.NavigationType.Naval : MobileParty.NavigationType.Default;
//    if (this.IsCurrentlyAtSea && !this.HasNavalNavigationCapability)
//      return;
//    for (int index = 0; index < 15; ++index)
//    {
//      CampaignVec2 pointAroundPosition = NavigationHelper.FindReachablePointAroundPosition(this.Position, navigationCapability, maxDistance, num);
//      bool flag = true;
//      LocatableSearchData<MobileParty> data = MobileParty.StartFindingLocatablesAroundPosition(pointAroundPosition.ToVec2(), num);
//      for (MobileParty nextLocatable = MobileParty.FindNextLocatable(ref data); nextLocatable != null; nextLocatable = MobileParty.FindNextLocatable(ref data))
//      {
//        if (nextLocatable.MapFaction.IsAtWarWith(this.MapFaction))
//        {
//          flag = false;
//          break;
//        }
//      }
//      if (flag)
//      {
//        this.Position = pointAroundPosition;
//        using (List<MobileParty>.Enumerator enumerator = this.AttachedParties.GetEnumerator())
//        {
//          while (enumerator.MoveNext())
//          {
//            MobileParty current = enumerator.Current;
//            current.Position = pointAroundPosition;
//            current.ArmyPositionAdder = Vec2.Zero;
//            current.EventPositionAdder = Vec2.Zero;
//          }
//          break;
//        }
//      }
//    }
//  }

//  private void DoAiPathMode(ref MobileParty.CachedPartyVariables variables)
//  {
//    if (variables.IsAttachedArmyMember)
//    {
//      this._pathMode = false;
//    }
//    else
//    {
//      this.DoAIMove(ref variables);
//      if (this.IsTransitionInProgress || !this._pathMode)
//        return;
//      bool flag;
//      do
//      {
//        flag = false;
//        this.NextTargetPosition = new CampaignVec2(this.Path[this.PathBegin], !this.IsCurrentlyAtSea);
//        float lengthSquared = (this.NextTargetPosition.ToVec2() - variables.CurrentPosition.ToVec2()).LengthSquared;
//        float num = variables.NextMoveDistance * variables.NextMoveDistance;
//        if ((double) lengthSquared < (double) num)
//        {
//          flag = true;
//          variables.NextMoveDistance -= MathF.Sqrt(lengthSquared);
//          variables.LastCurrentPosition = variables.CurrentPosition;
//          variables.CurrentPosition = this.NextTargetPosition;
//          ++this.PathBegin;
//        }
//      }
//      while (flag && this.PathBegin < this.Path.Size);
//      if (this.PathBegin < this.Path.Size)
//        return;
//      this._pathMode = false;
//      if (this.Path.Size <= 0)
//        return;
//      variables.CurrentPosition = variables.LastCurrentPosition;
//      this.NextTargetPosition = new CampaignVec2(this.Path[this.Path.Size - 1], !this.IsCurrentlyAtSea);
//    }
//  }

//  public bool RecalculateLongTermPath()
//  {
//    MobileParty.NavigationType navigationType = this.DesiredAiNavigationType;
//    if (navigationType == MobileParty.NavigationType.None)
//      navigationType = this.IsCurrentlyAtSea ? MobileParty.NavigationType.Naval : MobileParty.NavigationType.Default;
//    if (navigationType == MobileParty.NavigationType.Naval && !this.IsCurrentlyAtSea || navigationType == MobileParty.NavigationType.Default && this.IsCurrentlyAtSea)
//      navigationType = MobileParty.NavigationType.All;
//    CampaignVec2 newTargetPosition = this.TargetSettlement == null ? (this.TargetParty == null ? this.TargetPosition : this.TargetParty.Position) : (this.IsTargetingPort ? this.TargetSettlement.PortPosition : this.TargetSettlement.GatePosition);
//    return (!newTargetPosition.IsOnLand || this.DesiredAiNavigationType != MobileParty.NavigationType.Naval ? (newTargetPosition.IsOnLand ? 0 : (this.DesiredAiNavigationType == MobileParty.NavigationType.Default ? 1 : 0)) : 1) == 0 && this.ComputePath(newTargetPosition, navigationType, true);
//  }

//  private bool ComputePath(
//    CampaignVec2 newTargetPosition,
//    MobileParty.NavigationType navigationType,
//    bool cacheNextLongTermPathPoint)
//  {
//    bool path = false;
//    if (this.Position.IsValid())
//    {
//      if (newTargetPosition.IsValid())
//      {
//        if (!Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(Campaign.Current.MapSceneWrapper.GetFaceTerrainType(newTargetPosition.Face), this.NavigationCapability) && !this.IsInRaftState)
//          return false;
//        CampaignVec2 position = this.Position;
//        if (this.IsMainParty && MobileParty.MainParty.IsCurrentlyAtSea && NavigationHelper.IsPositionValidForNavigationType(newTargetPosition, MobileParty.NavigationType.Naval))
//          navigationType = MobileParty.NavigationType.Naval;
//        float agentRadius = this.IsCurrentlyAtSea ? Campaign.Current.Models.PartyNavigationModel.GetEmbarkDisembarkThresholdDistance() : 0.3f;
//        int[] forNavigationType = Campaign.Current.Models.PartyNavigationModel.GetInvalidTerrainTypesForNavigationType(navigationType);
//        path = Campaign.Current.MapSceneWrapper.GetPathBetweenAIFaces(this.CurrentNavigationFace, newTargetPosition.Face, position.ToVec2(), newTargetPosition.ToVec2(), agentRadius, this.Path, forNavigationType, 2f, this.GetRegionSwitchCostFromSeaToLand(), this.GetRegionSwitchCostFromLandToSea());
//        if (cacheNextLongTermPathPoint)
//        {
//          CampaignVec2 campaignVec2 = new CampaignVec2(this.Path.PathPoints[0], !this.IsCurrentlyAtSea);
//          if (!campaignVec2.IsValid())
//            campaignVec2 = new CampaignVec2(this.Path.PathPoints[0], this.IsCurrentlyAtSea);
//          this.NextLongTermPathPoint = campaignVec2;
//        }
//      }
//      else
//        Debug.FailedAssert("Path finding target is not valid", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Party\\MobileParty.cs", nameof (ComputePath), 3604);
//    }
//    this.PathBegin = 0;
//    if (!path)
//      this._pathMode = false;
//    return path;
//  }

//  public int GetRegionSwitchCostFromLandToSea()
//  {
//    return this.IsMainParty ? Campaign.PlayerRegionSwitchCostFromLandToSea : Campaign.Current.Models.MapDistanceModel.RegionSwitchCostFromLandToSea;
//  }

//  public int GetRegionSwitchCostFromSeaToLand()
//  {
//    return Campaign.Current.Models.MapDistanceModel.RegionSwitchCostFromSeaToLand;
//  }

//  private void DoAIMove(ref MobileParty.CachedPartyVariables variables)
//  {
//    CampaignVec2 finalTargetPosition;
//    bool forceNoPathMode;
//    this.GetTargetCampaignPosition(ref variables, out finalTargetPosition, out forceNoPathMode);
//    PathFaceRecord face = finalTargetPosition.Face;
//    if (this._pathMode && (face.FaceIndex != this.PathLastFace.FaceIndex || (double) finalTargetPosition.Distance(this._pathLastPosition) > 9.9999997473787516E-06))
//    {
//      this._pathMode = false;
//      this.PathLastFace = PathFaceRecord.NullFaceRecord;
//    }
//    if (!this._pathMode && (face.FaceIndex != this.PathLastFace.FaceIndex || (double) finalTargetPosition.Distance(this._pathLastPosition) > 9.9999997473787516E-06) && this._aiPathNotFound)
//      this._aiPathNotFound = false;
//    MobileParty.NavigationType navigationType;
//    if (this.IsComputePathCacheDirty(finalTargetPosition, forceNoPathMode, out navigationType))
//    {
//      if (this.CurrentNavigationFace.FaceIndex != this.MoveTargetPoint.Face.FaceIndex)
//      {
//        this._aiPathNotFound = !this.ComputePath(finalTargetPosition, navigationType, !this.IsFleeing());
//        if (!this._aiPathNotFound)
//        {
//          this.PathLastFace = face;
//          this._pathLastPosition = finalTargetPosition;
//          this._lastComputedPathNavigationType = navigationType;
//          this._pathMode = true;
//        }
//      }
//      else
//      {
//        this._pathMode = false;
//        this.PathLastFace = PathFaceRecord.NullFaceRecord;
//      }
//    }
//    else if (face.FaceIndex == this.PathLastFace.FaceIndex && this.CurrentNavigationFace.FaceIndex != face.FaceIndex)
//      this._pathMode = true;
//    if (this._pathMode)
//      return;
//    this.NextTargetPosition = finalTargetPosition;
//  }

//  private bool IsComputePathCacheDirty(
//    CampaignVec2 finalTargetPosition,
//    bool forceNoPathMode,
//    out MobileParty.NavigationType navigationType)
//  {
//    navigationType = this.DesiredAiNavigationType;
//    TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(finalTargetPosition.Face);
//    if (!this.StartTransitionNextFrameToExitFromPort && (this.DesiredAiNavigationType == MobileParty.NavigationType.Naval != this.IsCurrentlyAtSea || !Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(faceTerrainType, this.DesiredAiNavigationType)))
//      navigationType = MobileParty.NavigationType.All;
//    if (this._lastComputedPathNavigationType != navigationType || !this._pathMode && !forceNoPathMode && !this._aiPathNotFound)
//      return true;
//    return finalTargetPosition.Face.FaceIndex != this.PathLastFace.FaceIndex && finalTargetPosition.IsValid();
//  }

//  private void GetTargetCampaignPosition(
//    ref MobileParty.CachedPartyVariables variables,
//    out CampaignVec2 finalTargetPosition,
//    out bool forceNoPathMode)
//  {
//    finalTargetPosition = this.Position;
//    forceNoPathMode = false;
//    if (this.PartyMoveMode == MoveModeType.Point)
//    {
//      finalTargetPosition = this.MoveTargetPoint;
//      forceNoPathMode = this.ForceAiNoPathMode;
//    }
//    else
//    {
//      if (this.PartyMoveMode != MoveModeType.Party || !this.MoveTargetParty.Party.IsValid)
//        return;
//      finalTargetPosition = variables.TargetPartyPositionAtFrameStart;
//      bool flag = !NavigationHelper.IsPositionValidForNavigationType(variables.TargetPartyPositionAtFrameStart, this.NavigationCapability);
//      if (flag && !variables.IsTargetMovingAtFrameStart)
//        variables.IsTargetMovingAtFrameStart = true;
//      if (!(variables.IsTargetMovingAtFrameStart & flag))
//        return;
//      finalTargetPosition = this.Position;
//      forceNoPathMode = false;
//      if (this.Ai.IsDisabled || this.IsMainParty)
//        return;
//      this.Ai.DefaultBehaviorNeedsUpdate = true;
//    }
//  }

//  private void DoUpdatePosition(
//    ref MobileParty.CachedPartyVariables variables,
//    float dt,
//    float realDt)
//  {
//    Vec2 vec2_1;
//    if (variables.IsAttachedArmyMember)
//    {
//      if (variables.HasMapEvent || this.CurrentSettlement != null)
//      {
//        vec2_1 = Vec2.Zero;
//      }
//      else
//      {
//        CampaignVec2 position;
//        Vec2 vec2_2;
//        if (!variables.HasMapEvent)
//        {
//          vec2_2 = this.Army.LeaderParty.NextTargetPosition.ToVec2();
//        }
//        else
//        {
//          position = this.Army.LeaderParty.Position;
//          vec2_2 = position.ToVec2();
//        }
//        Vec2 vec2_3 = vec2_2;
//        CampaignVec2 finalTargetPosition;
//        this.Army.LeaderParty.GetTargetCampaignPosition(ref variables, out finalTargetPosition, out bool _);
//        Vec2 vec2_4 = vec2_3;
//        position = this.Army.LeaderParty.Position;
//        Vec2 vec2_5 = position.ToVec2();
//        Vec2 vec2_6 = vec2_4 - vec2_5;
//        Vec2 vec2_7;
//        if ((double) vec2_6.LengthSquared >= 0.0025000001769512892)
//        {
//          Vec2 vec2_8 = vec2_3;
//          position = this.Army.LeaderParty.Position;
//          Vec2 vec2_9 = position.ToVec2();
//          vec2_6 = vec2_8 - vec2_9;
//          vec2_7 = vec2_6.Normalized();
//        }
//        else
//        {
//          vec2_6 = this.Army.LeaderParty.Bearing;
//          vec2_7 = vec2_6.Normalized();
//        }
//        Vec2 armyFacing = vec2_7;
//        Vec2 parentUnitF = armyFacing.TransformToParentUnitF(this.Army.GetRelativePositionForParty(this, armyFacing));
//        vec2_1 = vec2_3 + parentUnitF - this.VisualPosition2DWithoutError;
//        vec2_6 = finalTargetPosition.ToVec2() + parentUnitF - this.VisualPosition2DWithoutError;
//        if ((double) vec2_6.LengthSquared < 1.0000001111620804E-06 || (double) vec2_1.LengthSquared < 1.0000001111620804E-06)
//          vec2_1 = Vec2.Zero;
//        vec2_6 = vec2_1.LeftVec();
//        vec2_6 = vec2_6.Normalized();
//        ref Vec2 local = ref vec2_6;
//        position = this.Army.LeaderParty.Position;
//        Vec2 v = position.ToVec2() + parentUnitF - this.VisualPosition2DWithoutError;
//        float num = local.DotProduct(v);
//        vec2_1.RotateCCW((double) num < 0.0 ? MathF.Max(num * 2f, -0.7853982f) : MathF.Min(num * 2f, 0.7853982f));
//      }
//    }
//    else
//      vec2_1 = (variables.HasMapEvent ? this.Party.MapEvent.Position.ToVec2() : this.NextTargetPosition.ToVec2()) - this.VisualPosition2DWithoutError;
//    float num1 = vec2_1.Normalize();
//    if ((double) num1 < (double) variables.NextMoveDistance)
//      variables.NextMoveDistance = num1;
//    if (this.BesiegedSettlement != null || this.CurrentSettlement != null || (double) variables.NextMoveDistance <= 0.0 && !variables.HasMapEvent)
//      return;
//    Vec2 vec2_10 = this.Bearing;
//    if ((double) num1 > 0.0)
//    {
//      vec2_10 = vec2_1;
//      if (!variables.IsAttachedArmyMember || !variables.HasMapEvent)
//        this.Bearing = vec2_10;
//    }
//    else if (variables.IsAttachedArmyMember && variables.HasMapEvent)
//    {
//      vec2_10 = this.Army.LeaderParty.Bearing;
//      this.Bearing = vec2_10;
//    }
//    variables.NextPosition = variables.CurrentPosition + vec2_10 * variables.NextMoveDistance;
//  }

//  private void ResetAllMovementParameters()
//  {
//    this.TargetParty = (MobileParty) null;
//    this.SetTargetSettlement((Settlement) null, false);
//    this.DefaultBehavior = AiBehavior.None;
//    this.SetShortTermBehavior(AiBehavior.None, (IInteractablePoint) null);
//    this.TargetPosition = this.Position;
//    this.MoveTargetPoint = this.Position;
//  }

//  public void SetMoveModeHold()
//  {
//    this.ResetAllMovementParameters();
//    this.DefaultBehavior = AiBehavior.Hold;
//    this.SetShortTermBehavior(AiBehavior.Hold, (IInteractablePoint) null);
//    this.SetTargetSettlement((Settlement) null, false);
//    this.TargetParty = (MobileParty) null;
//    this.TargetPosition = this.Position;
//    this.MoveTargetPoint = this.Position;
//    this.DesiredAiNavigationType = MobileParty.NavigationType.None;
//  }

//  public void SetMoveEngageParty(MobileParty party, MobileParty.NavigationType navigationType)
//  {
//    this.ResetAllMovementParameters();
//    this.TargetParty = party;
//    this.MoveTargetPoint = party.Position;
//    this.DesiredAiNavigationType = navigationType;
//    this.DefaultBehavior = AiBehavior.EngageParty;
//  }

//  public void SetMoveGoAroundParty(MobileParty party, MobileParty.NavigationType navigationType)
//  {
//    this.ResetAllMovementParameters();
//    this.TargetParty = party;
//    this.MoveTargetPoint = party.Position;
//    this.DesiredAiNavigationType = navigationType;
//    this.DefaultBehavior = AiBehavior.GoAroundParty;
//  }

//  public void SetMoveGoToSettlement(
//    Settlement settlement,
//    MobileParty.NavigationType navigationType,
//    bool isTargetingThePort)
//  {
//    this.ResetAllMovementParameters();
//    this.SetTargetSettlement(settlement, isTargetingThePort);
//    CampaignVec2 campaignVec2 = isTargetingThePort ? settlement.PortPosition : settlement.GatePosition;
//    this.TargetPosition = campaignVec2;
//    this.MoveTargetPoint = campaignVec2;
//    this.DesiredAiNavigationType = navigationType;
//    this.DefaultBehavior = AiBehavior.GoToSettlement;
//  }

//  public void SetMoveGoToPoint(CampaignVec2 point, MobileParty.NavigationType navigationType)
//  {
//    this.ResetAllMovementParameters();
//    this.TargetPosition = point;
//    this.MoveTargetPoint = point;
//    this.DesiredAiNavigationType = navigationType;
//    this.Ai.AiBehaviorInteractable = (IInteractablePoint) null;
//    this.DefaultBehavior = AiBehavior.GoToPoint;
//  }

//  public void SetMoveToNearestLand(Settlement settlement)
//  {
//    this.ResetAllMovementParameters();
//    if (settlement != null)
//      this.SetTargetSettlement(settlement, true);
//    this.DesiredAiNavigationType = this.HasLandNavigationCapability ? MobileParty.NavigationType.All : MobileParty.NavigationType.Naval;
//    this.DefaultBehavior = AiBehavior.MoveToNearestLandOrPort;
//  }

//  public void SetMoveGoToInteractablePoint(
//    IInteractablePoint point,
//    MobileParty.NavigationType navigationType)
//  {
//    this.ResetAllMovementParameters();
//    this.TargetPosition = point.GetInteractionPosition(this);
//    this.MoveTargetPoint = this.TargetPosition;
//    this.Ai.AiBehaviorInteractable = point;
//    this.DesiredAiNavigationType = navigationType;
//    this.DefaultBehavior = AiBehavior.GoToPoint;
//  }

//  public void SetMoveEscortParty(
//    MobileParty mobileParty,
//    MobileParty.NavigationType navigationType,
//    bool isTargetingPort)
//  {
//    this.ResetAllMovementParameters();
//    this.TargetParty = mobileParty;
//    this.MoveTargetPoint = mobileParty.Position;
//    if (isTargetingPort)
//      this.SetTargetSettlement(mobileParty.CurrentSettlement, true);
//    this.DesiredAiNavigationType = navigationType;
//    this.DefaultBehavior = AiBehavior.EscortParty;
//  }

//  public void SetMovePatrolAroundPoint(
//    CampaignVec2 point,
//    MobileParty.NavigationType navigationType)
//  {
//    this.ResetAllMovementParameters();
//    this.TargetPosition = point;
//    this.MoveTargetPoint = point;
//    this.DesiredAiNavigationType = navigationType;
//    this.DefaultBehavior = AiBehavior.PatrolAroundPoint;
//  }

//  public void SetMovePatrolAroundSettlement(
//    Settlement settlement,
//    MobileParty.NavigationType navigationType,
//    bool isTargetingPort)
//  {
//    this.SetMovePatrolAroundPoint(isTargetingPort ? settlement.PortPosition : settlement.GatePosition, navigationType);
//    this.SetTargetSettlement(settlement, isTargetingPort);
//  }

//  public void SetMoveRaidSettlement(
//    Settlement settlement,
//    MobileParty.NavigationType navigationType)
//  {
//    this.ResetAllMovementParameters();
//    this.SetTargetSettlement(settlement, false);
//    CampaignVec2 gatePosition = settlement.GatePosition;
//    this.TargetPosition = gatePosition;
//    this.MoveTargetPoint = gatePosition;
//    this.DesiredAiNavigationType = navigationType;
//    this.DefaultBehavior = AiBehavior.RaidSettlement;
//  }

//  public void SetMoveBesiegeSettlement(
//    Settlement settlement,
//    MobileParty.NavigationType navigationType)
//  {
//    this.ResetAllMovementParameters();
//    if (this.BesiegedSettlement != null && this.BesiegedSettlement != settlement)
//      this.BesiegerCamp = (BesiegerCamp) null;
//    this.SetTargetSettlement(settlement, false);
//    this.DesiredAiNavigationType = navigationType;
//    this.DefaultBehavior = AiBehavior.BesiegeSettlement;
//  }

//  public void SetMoveDefendSettlement(
//    Settlement settlement,
//    bool isTargetingPort,
//    MobileParty.NavigationType navigationType)
//  {
//    this.ResetAllMovementParameters();
//    this.SetTargetSettlement(settlement, isTargetingPort);
//    this.DesiredAiNavigationType = navigationType;
//    this.DefaultBehavior = AiBehavior.DefendSettlement;
//  }

//  internal void SetNavigationModeHold()
//  {
//    this.PartyMoveMode = MoveModeType.Hold;
//    this._pathMode = false;
//    this.NextTargetPosition = this.Position;
//    this.MoveTargetParty = (MobileParty) null;
//  }

//  internal void SetNavigationModePoint(CampaignVec2 newTargetPosition)
//  {
//    this.PartyMoveMode = MoveModeType.Point;
//    this.UpdatePathModeWithPosition(newTargetPosition);
//    this._aiPathNotFound = false;
//    this.MoveTargetParty = (MobileParty) null;
//  }

//  internal void SetNavigationModeParty(MobileParty targetParty)
//  {
//    this.PartyMoveMode = MoveModeType.Party;
//    this.MoveTargetParty = targetParty;
//    this.MoveTargetPoint = targetParty.Position;
//    this._aiPathNotFound = false;
//  }

//  public static LocatableSearchData<MobileParty> StartFindingLocatablesAroundPosition(
//    Vec2 position,
//    float radius)
//  {
//    return Campaign.Current.MobilePartyLocator.StartFindingLocatablesAroundPosition(position, radius);
//  }

//  public static MobileParty FindNextLocatable(ref LocatableSearchData<MobileParty> data)
//  {
//    return Campaign.Current.MobilePartyLocator.FindNextLocatable(ref data);
//  }

//  public static void UpdateLocator(MobileParty party)
//  {
//    Campaign.Current.MobilePartyLocator.UpdateLocator(party);
//  }

//  internal void OnHeroAdded(Hero hero) => hero.OnAddedToParty(this);

//  internal void OnHeroRemoved(Hero hero) => hero.OnRemovedFromParty(this);

//  internal void CheckExitingSettlementParallel(
//    ref int exitingPartyCount,
//    ref MobileParty[] exitingPartyList,
//    ref int gridChangeCount,
//    ref MobileParty[] gridChangeMobilePartyList)
//  {
//    if (this.Ai.IsDisabled || this.ShortTermBehavior == AiBehavior.Hold || this.CurrentSettlement == null || (this.ShortTermTargetSettlement != null || this.TargetSettlement == this.CurrentSettlement) && this.ShortTermTargetSettlement == this.CurrentSettlement || this.IsMainParty || this.Army != null && this.AttachedTo != null && this.Army.LeaderParty != this)
//      return;
//    if (this.StartTransitionNextFrameToExitFromPort)
//    {
//      this.StartTransitionNextFrameToExitFromPort = false;
//      if (this.IsCurrentlyAtSea)
//        return;
//      this.InitializeNavigationTransitionParallel(this.CurrentSettlement.PortPosition, this.CurrentSettlement.PortPosition, ref gridChangeCount, ref gridChangeMobilePartyList);
//    }
//    else
//    {
//      if (this.IsTransitionInProgress)
//        return;
//      int index = Interlocked.Increment(ref exitingPartyCount);
//      exitingPartyList[index] = this;
//    }
//  }

//  public bool ComputeIsWaiting()
//  {
//    MobileParty moveTargetParty = this.MoveTargetParty;
//    CampaignVec2 campaignVec2 = moveTargetParty != null ? moveTargetParty.Position : this.MoveTargetPoint;
//    if ((double) ((2f * this.Position).ToVec2() - campaignVec2.ToVec2() - this.NextTargetPosition.ToVec2()).LengthSquared < 9.9999997473787516E-06 || this.DefaultBehavior == AiBehavior.Hold)
//      return true;
//    return (this.DefaultBehavior == AiBehavior.EngageParty || this.DefaultBehavior == AiBehavior.EscortParty) && this.Ai.AiBehaviorPartyBase != null && this.Ai.AiBehaviorPartyBase.IsValid && this.Ai.AiBehaviorPartyBase.IsActive && this.Ai.AiBehaviorPartyBase.IsMobile && this.Ai.AiBehaviorPartyBase.MobileParty.CurrentSettlement != null;
//  }

//  public void InitializePartyTrade(int initialGold)
//  {
//    this.IsPartyTradeActive = true;
//    this.PartyTradeGold = initialGold;
//  }

//  public void AddTaxGold(int amount) => this.PartyTradeTaxGold += amount;

//  public static MobileParty CreateParty(string stringId, PartyComponent component)
//  {
//    stringId = Campaign.Current.CampaignObjectManager.FindNextUniqueStringId<MobileParty>(stringId);
//    MobileParty party = new MobileParty();
//    party.StringId = stringId;
//    party._partyComponent = component;
//    party.UpdatePartyComponentFlags();
//    party._partyComponent?.Create(party);
//    party._partyComponent?.Initialize(party);
//    Campaign.Current.CampaignObjectManager.AddMobileParty(party);
//    CampaignEventDispatcher.Instance.OnMobilePartyCreated(party);
//    CampaignEventDispatcher.Instance.OnMapInteractableCreated((IInteractablePoint) party.Party);
//    return party;
//  }

//  public VillagerPartyComponent VillagerPartyComponent
//  {
//    get => this._partyComponent as VillagerPartyComponent;
//  }

//  public CaravanPartyComponent CaravanPartyComponent
//  {
//    get => this._partyComponent as CaravanPartyComponent;
//  }

//  public WarPartyComponent WarPartyComponent => this._partyComponent as WarPartyComponent;

//  public BanditPartyComponent BanditPartyComponent => this._partyComponent as BanditPartyComponent;

//  public PatrolPartyComponent PatrolPartyComponent => this._partyComponent as PatrolPartyComponent;

//  public LordPartyComponent LordPartyComponent => this._partyComponent as LordPartyComponent;

//  public GarrisonPartyComponent GarrisonPartyComponent
//  {
//    get => this._partyComponent as GarrisonPartyComponent;
//  }

//  public PartyComponent PartyComponent => this._partyComponent;

//  public void SetPartyComponent(PartyComponent partyComponent, bool firstTimePartyComponentCreation = true)
//  {
//    if (this._partyComponent == partyComponent)
//      return;
//    if (this._partyComponent != null)
//      this._partyComponent.Finish();
//    Campaign.Current.CampaignObjectManager.BeforePartyComponentChanged(this);
//    this._partyComponent = partyComponent;
//    this.UpdatePartyComponentFlags();
//    Campaign.Current.CampaignObjectManager.AfterPartyComponentChanged(this);
//    if (this._partyComponent != null)
//    {
//      if (firstTimePartyComponentCreation)
//        this._partyComponent.Create(this);
//      this._partyComponent.Initialize(this);
//    }
//    this.Party.SetVisualAsDirty();
//  }

//  [CachedData]
//  public bool IsMilitia { get; private set; }

//  [CachedData]
//  public bool IsLordParty { get; private set; }

//  public void UpdatePartyComponentFlags()
//  {
//    this.IsLordParty = this._partyComponent is LordPartyComponent;
//    this.IsVillager = this._partyComponent is VillagerPartyComponent;
//    this.IsMilitia = this._partyComponent is MilitiaPartyComponent;
//    this.IsCaravan = this._partyComponent is CaravanPartyComponent;
//    this.IsPatrolParty = this._partyComponent is PatrolPartyComponent;
//    this.IsGarrison = this._partyComponent is GarrisonPartyComponent;
//    this.IsCustomParty = this._partyComponent is CustomPartyComponent;
//    this.IsBandit = this._partyComponent is BanditPartyComponent;
//  }

//  [CachedData]
//  public bool IsVillager { get; private set; }

//  [CachedData]
//  public bool IsCaravan { get; private set; }

//  [CachedData]
//  public bool IsPatrolParty { get; private set; }

//  [CachedData]
//  public bool IsGarrison { get; private set; }

//  [CachedData]
//  public bool IsCustomParty { get; private set; }

//  [CachedData]
//  public bool IsBandit { get; private set; }

//  public bool IsBanditBossParty => this.IsBandit && this.BanditPartyComponent.IsBossParty;

//  public bool AvoidHostileActions
//  {
//    get => this._partyComponent != null && this._partyComponent.AvoidHostileActions;
//  }

//  [SpecialName]
//  bool ITrackableCampaignObject.get_IsReady() => this.IsReady;

//  public enum PartyObjective
//  {
//    Neutral,
//    Defensive,
//    Aggressive,
//    NumberOfPartyObjectives,
//  }

//  [Flags]
//  public enum NavigationType
//  {
//    None = 0,
//    Default = 1,
//    Naval = 2,
//    All = Naval | Default, // 0x00000003
//  }

//  internal struct CachedPartyVariables
//  {
//    internal bool IsAttachedArmyMember;
//    internal bool IsArmyLeader;
//    internal bool IsMoving;
//    internal bool HasMapEvent;
//    internal float NextMoveDistance;
//    internal CampaignVec2 CurrentPosition;
//    internal CampaignVec2 LastCurrentPosition;
//    internal CampaignVec2 NextPosition;
//    internal CampaignVec2 TargetPartyPositionAtFrameStart;
//    internal bool IsTargetMovingAtFrameStart;
//    internal bool IsTransitionInProgress;

//    public override string ToString()
//    {
//      return $"IsAttachedArmyMember:{this.IsAttachedArmyMember.ToString()}\nIsArmyLeader{this.IsArmyLeader.ToString()}\nIsMoving{this.IsMoving.ToString()}\nHasMapEvent{this.HasMapEvent.ToString()}\nNextMoveDistance{(object) this.NextMoveDistance}\nCurrentPosition{(object) this.CurrentPosition}\nLastCurrentPosition{(object) this.LastCurrentPosition}\nNextPosition{(object) this.NextPosition}\nTargetPartyPositionAtFrameStart{(object) this.TargetPartyPositionAtFrameStart}\n";
//    }
//  }
//}
