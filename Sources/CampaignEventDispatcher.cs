//// Decompiled with JetBrains decompiler
//// Type: TaleWorlds.CampaignSystem.CampaignEventDispatcher
//// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 038459B8-4640-4714-AE67-6181A9569366
//// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll

//using Helpers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using TaleWorlds.CampaignSystem.Actions;
//using TaleWorlds.CampaignSystem.BarterSystem;
//using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
//using TaleWorlds.CampaignSystem.CharacterCreationContent;
//using TaleWorlds.CampaignSystem.CharacterDevelopment;
//using TaleWorlds.CampaignSystem.Conversation.Persuasion;
//using TaleWorlds.CampaignSystem.CraftingSystem;
//using TaleWorlds.CampaignSystem.Election;
//using TaleWorlds.CampaignSystem.GameComponents;
//using TaleWorlds.CampaignSystem.GameMenus;
//using TaleWorlds.CampaignSystem.Incidents;
//using TaleWorlds.CampaignSystem.Issues;
//using TaleWorlds.CampaignSystem.Map;
//using TaleWorlds.CampaignSystem.MapEvents;
//using TaleWorlds.CampaignSystem.Naval;
//using TaleWorlds.CampaignSystem.Party;
//using TaleWorlds.CampaignSystem.Party.PartyComponents;
//using TaleWorlds.CampaignSystem.Roster;
//using TaleWorlds.CampaignSystem.Settlements;
//using TaleWorlds.CampaignSystem.Settlements.Buildings;
//using TaleWorlds.CampaignSystem.Settlements.Workshops;
//using TaleWorlds.CampaignSystem.Siege;
//using TaleWorlds.Core;
//using TaleWorlds.Library;

//#nullable disable
//namespace TaleWorlds.CampaignSystem;

//public class CampaignEventDispatcher : CampaignEventReceiver
//{
//  private CampaignEventReceiver[] _eventReceivers;

//  public static CampaignEventDispatcher Instance => Campaign.Current?.CampaignEventDispatcher;

//  internal CampaignEventDispatcher(IEnumerable<CampaignEventReceiver> eventReceivers)
//  {
//    this._eventReceivers = eventReceivers.ToArray<CampaignEventReceiver>();
//  }

//  internal void AddCampaignEventReceiver(CampaignEventReceiver receiver)
//  {
//    CampaignEventReceiver[] campaignEventReceiverArray = new CampaignEventReceiver[this._eventReceivers.Length + 1];
//    for (int index = 0; index < this._eventReceivers.Length; ++index)
//      campaignEventReceiverArray[index] = this._eventReceivers[index];
//    campaignEventReceiverArray[this._eventReceivers.Length] = receiver;
//    this._eventReceivers = campaignEventReceiverArray;
//  }

//  public override void RemoveListeners(object o)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.RemoveListeners(o);
//  }

//  public override void OnPlayerBodyPropertiesChanged()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerBodyPropertiesChanged();
//  }

//  public override void OnHeroLevelledUp(Hero hero, bool shouldNotify = true)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroLevelledUp(hero, shouldNotify);
//  }

//  public override void OnHomeHideoutChanged(
//    BanditPartyComponent banditPartyComponent,
//    Hideout oldHomeHideout)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHomeHideoutChanged(banditPartyComponent, oldHomeHideout);
//  }

//  public override void OnCharacterCreationIsOver()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCharacterCreationIsOver();
//  }

//  public override void OnHeroGainedSkill(
//    Hero hero,
//    SkillObject skill,
//    int change = 1,
//    bool shouldNotify = true)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroGainedSkill(hero, skill, change, shouldNotify);
//  }

//  public override void OnHeroWounded(Hero woundedHero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroWounded(woundedHero);
//  }

//  public override void OnHeroRelationChanged(
//    Hero effectiveHero,
//    Hero effectiveHeroGainedRelationWith,
//    int relationChange,
//    bool showNotification,
//    ChangeRelationAction.ChangeRelationDetail detail,
//    Hero originalHero,
//    Hero originalGainedRelationWith)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroRelationChanged(effectiveHero, effectiveHeroGainedRelationWith, relationChange, showNotification, detail, originalHero, originalGainedRelationWith);
//  }

//  public override void OnLootDistributedToParty(
//    PartyBase winnerParty,
//    PartyBase defeatedParty,
//    ItemRoster lootedItems)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnLootDistributedToParty(winnerParty, defeatedParty, lootedItems);
//  }

//  public override void OnHeroOccupationChanged(Hero hero, Occupation oldOccupation)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroOccupationChanged(hero, oldOccupation);
//  }

//  public override void OnBarterAccepted(Hero offererHero, Hero otherHero, List<Barterable> barters)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBarterAccepted(offererHero, otherHero, barters);
//  }

//  public override void OnBarterCanceled(Hero offererHero, Hero otherHero, List<Barterable> barters)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBarterCanceled(offererHero, otherHero, barters);
//  }

//  public override void OnHeroCreated(Hero hero, bool isBornNaturally = false)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroCreated(hero, isBornNaturally);
//  }

//  public override void OnQuestLogAdded(QuestBase quest, bool hideInformation)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnQuestLogAdded(quest, hideInformation);
//  }

//  public override void OnIssueLogAdded(IssueBase issue, bool hideInformation)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnIssueLogAdded(issue, hideInformation);
//  }

//  public override void OnClanTierChanged(Clan clan, bool shouldNotify = true)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnClanTierChanged(clan, shouldNotify);
//  }

//  public override void OnClanChangedKingdom(
//    Clan clan,
//    Kingdom oldKingdom,
//    Kingdom newKingdom,
//    ChangeKingdomAction.ChangeKingdomActionDetail actionDetail,
//    bool showNotification = true)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnClanChangedKingdom(clan, oldKingdom, newKingdom, actionDetail, showNotification);
//  }

//  public override void OnClanDefected(Clan clan, Kingdom oldKingdom, Kingdom newKingdom)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnClanDefected(clan, oldKingdom, newKingdom);
//  }

//  public override void OnClanCreated(Clan clan, bool isCompanion)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnClanCreated(clan, isCompanion);
//  }

//  public override void OnHeroJoinedParty(Hero hero, MobileParty party)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroJoinedParty(hero, party);
//  }

//  public override void OnKingdomDecisionAdded(KingdomDecision decision, bool isPlayerInvolved)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnKingdomDecisionAdded(decision, isPlayerInvolved);
//  }

//  public override void OnKingdomDecisionCancelled(KingdomDecision decision, bool isPlayerInvolved)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnKingdomDecisionCancelled(decision, isPlayerInvolved);
//  }

//  public override void OnKingdomDecisionConcluded(
//    KingdomDecision decision,
//    DecisionOutcome chosenOutcome,
//    bool isPlayerInvolved)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnKingdomDecisionConcluded(decision, chosenOutcome, isPlayerInvolved);
//  }

//  public override void OnHeroOrPartyTradedGold(
//    (Hero, PartyBase) giver,
//    (Hero, PartyBase) recipient,
//    (int, string) goldAmount,
//    bool showNotification)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroOrPartyTradedGold(giver, recipient, goldAmount, showNotification);
//  }

//  public override void OnHeroOrPartyGaveItem(
//    (Hero, PartyBase) giver,
//    (Hero, PartyBase) receiver,
//    ItemRosterElement itemRosterElement,
//    bool showNotification)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroOrPartyGaveItem(giver, receiver, itemRosterElement, showNotification);
//  }

//  public override void OnBanditPartyRecruited(MobileParty banditParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBanditPartyRecruited(banditParty);
//  }

//  public override void OnArmyCreated(Army army)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnArmyCreated(army);
//  }

//  public override void OnPartyAttachedAnotherParty(MobileParty mobileParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyAttachedAnotherParty(mobileParty);
//  }

//  public override void OnNearbyPartyAddedToPlayerMapEvent(MobileParty mobileParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnNearbyPartyAddedToPlayerMapEvent(mobileParty);
//  }

//  public override void OnArmyDispersed(
//    Army army,
//    Army.ArmyDispersionReason reason,
//    bool isPlayersArmy)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnArmyDispersed(army, reason, isPlayersArmy);
//  }

//  public override void OnArmyGathered(Army army, IMapPoint gatheringPoint)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnArmyGathered(army, gatheringPoint);
//  }

//  public override void OnPerkOpened(Hero hero, PerkObject perk)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPerkOpened(hero, perk);
//  }

//  public override void OnPerkReset(Hero hero, PerkObject perk)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPerkReset(hero, perk);
//  }

//  public override void OnPlayerTraitChanged(TraitObject trait, int previousLevel)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerTraitChanged(trait, previousLevel);
//  }

//  public override void OnVillageStateChanged(
//    Village village,
//    Village.VillageStates oldState,
//    Village.VillageStates newState,
//    MobileParty raiderParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnVillageStateChanged(village, oldState, newState, raiderParty);
//  }

//  public override void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSettlementEntered(party, settlement, hero);
//  }

//  public override void OnAfterSettlementEntered(
//    MobileParty party,
//    Settlement settlement,
//    Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnAfterSettlementEntered(party, settlement, hero);
//  }

//  public override void OnBeforeSettlementEntered(
//    MobileParty party,
//    Settlement settlement,
//    Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBeforeSettlementEntered(party, settlement, hero);
//  }

//  public override void OnMercenaryTroopChangedInTown(
//    Town town,
//    CharacterObject oldTroopType,
//    CharacterObject newTroopType)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMercenaryTroopChangedInTown(town, oldTroopType, newTroopType);
//  }

//  public override void OnMercenaryNumberChangedInTown(Town town, int oldNumber, int newNumber)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMercenaryNumberChangedInTown(town, oldNumber, newNumber);
//  }

//  public override void OnAlleyOccupiedByPlayer(Alley alley, TroopRoster troops)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnAlleyOccupiedByPlayer(alley, troops);
//  }

//  public override void OnAlleyOwnerChanged(Alley alley, Hero newOwner, Hero oldOwner)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnAlleyOwnerChanged(alley, newOwner, oldOwner);
//  }

//  public override void OnAlleyClearedByPlayer(Alley alley)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnAlleyClearedByPlayer(alley);
//  }

//  public override void OnRomanticStateChanged(
//    Hero hero1,
//    Hero hero2,
//    Romance.RomanceLevelEnum romanceLevel)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnRomanticStateChanged(hero1, hero2, romanceLevel);
//  }

//  public override void OnBeforeHeroesMarried(Hero hero1, Hero hero2, bool showNotification)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBeforeHeroesMarried(hero1, hero2, showNotification);
//  }

//  public override void OnPlayerEliminatedFromTournament(int round, Town town)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerEliminatedFromTournament(round, town);
//  }

//  public override void OnPlayerStartedTournamentMatch(Town town)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerStartedTournamentMatch(town);
//  }

//  public override void OnTournamentStarted(Town town)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnTournamentStarted(town);
//  }

//  public override void OnTournamentFinished(
//    CharacterObject winner,
//    MBReadOnlyList<CharacterObject> participants,
//    Town town,
//    ItemObject prize)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnTournamentFinished(winner, participants, town, prize);
//  }

//  public override void OnTournamentCancelled(Town town)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnTournamentCancelled(town);
//  }

//  public override void OnWarDeclared(
//    IFaction faction1,
//    IFaction faction2,
//    DeclareWarAction.DeclareWarDetail declareWarDetail)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnWarDeclared(faction1, faction2, declareWarDetail);
//  }

//  public override void OnRulingClanChanged(Kingdom kingdom, Clan newRulingClan)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnRulingClanChanged(kingdom, newRulingClan);
//  }

//  public override void OnStartBattle(
//    PartyBase attackerParty,
//    PartyBase defenderParty,
//    object subject,
//    bool showNotification)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnStartBattle(attackerParty, defenderParty, subject, showNotification);
//  }

//  public override void OnRebellionFinished(Settlement settlement, Clan oldOwnerClan)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnRebellionFinished(settlement, oldOwnerClan);
//  }

//  public override void TownRebelliousStateChanged(Town town, bool rebelliousState)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.TownRebelliousStateChanged(town, rebelliousState);
//  }

//  public override void OnRebelliousClanDisbandedAtSettlement(
//    Settlement settlement,
//    Clan rebelliousClan)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnRebelliousClanDisbandedAtSettlement(settlement, rebelliousClan);
//  }

//  public override void OnItemsLooted(MobileParty mobileParty, ItemRoster items)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnItemsLooted(mobileParty, items);
//  }

//  public override void OnMobilePartyDestroyed(MobileParty mobileParty, PartyBase destroyerParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMobilePartyDestroyed(mobileParty, destroyerParty);
//  }

//  public override void OnMobilePartyCreated(MobileParty party)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMobilePartyCreated(party);
//  }

//  public override void OnMapInteractableCreated(IInteractablePoint interactable)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMapInteractableCreated(interactable);
//  }

//  public override void OnMapInteractableDestroyed(IInteractablePoint interactable)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMapInteractableDestroyed(interactable);
//  }

//  public override void OnMobilePartyQuestStatusChanged(MobileParty party, bool isUsedByQuest)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMobilePartyQuestStatusChanged(party, isUsedByQuest);
//  }

//  public override void OnHeroKilled(
//    Hero victim,
//    Hero killer,
//    KillCharacterAction.KillCharacterActionDetail detail,
//    bool showNotification = true)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroKilled(victim, killer, detail, showNotification);
//  }

//  public override void OnBeforeHeroKilled(
//    Hero victim,
//    Hero killer,
//    KillCharacterAction.KillCharacterActionDetail detail,
//    bool showNotification = true)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBeforeHeroKilled(victim, killer, detail, showNotification);
//  }

//  public override void OnChildEducationCompleted(Hero hero, int age)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnChildEducationCompleted(hero, age);
//  }

//  public override void OnHeroComesOfAge(Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroComesOfAge(hero);
//  }

//  public override void OnHeroReachesTeenAge(Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroReachesTeenAge(hero);
//  }

//  public override void OnHeroGrowsOutOfInfancy(Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroGrowsOutOfInfancy(hero);
//  }

//  public override void OnCharacterDefeated(Hero winner, Hero loser)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCharacterDefeated(winner, loser);
//  }

//  public override void OnHeroPrisonerTaken(PartyBase capturer, Hero prisoner)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroPrisonerTaken(capturer, prisoner);
//  }

//  public override void OnHeroPrisonerReleased(
//    Hero prisoner,
//    PartyBase party,
//    IFaction capturerFaction,
//    EndCaptivityDetail detail,
//    bool showNotification = true)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroPrisonerReleased(prisoner, party, capturerFaction, detail, showNotification);
//  }

//  public override void OnCharacterBecameFugitive(Hero hero, bool showNotification)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCharacterBecameFugitive(hero, showNotification);
//  }

//  public override void OnPlayerLearnsAboutHero(Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerLearnsAboutHero(hero);
//  }

//  public override void OnPlayerMetHero(Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerMetHero(hero);
//  }

//  public override void OnRenownGained(Hero hero, int gainedRenown, bool doNotNotify)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnRenownGained(hero, gainedRenown, doNotNotify);
//  }

//  public override void OnCrimeRatingChanged(IFaction kingdom, float deltaCrimeAmount)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCrimeRatingChanged(kingdom, deltaCrimeAmount);
//  }

//  public override void OnNewCompanionAdded(Hero newCompanion)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnNewCompanionAdded(newCompanion);
//  }

//  public override void OnAfterMissionStarted(IMission iMission)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnAfterMissionStarted(iMission);
//  }

//  public override void OnGameMenuOpened(MenuCallbackArgs args)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnGameMenuOpened(args);
//  }

//  public override void OnMakePeace(
//    IFaction side1Faction,
//    IFaction side2Faction,
//    MakePeaceAction.MakePeaceDetail detail)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMakePeace(side1Faction, side2Faction, detail);
//  }

//  public override void OnKingdomDestroyed(Kingdom destroyedKingdom)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnKingdomDestroyed(destroyedKingdom);
//  }

//  public override void CanKingdomBeDiscontinued(Kingdom kingdom, ref bool result)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.CanKingdomBeDiscontinued(kingdom, ref result);
//  }

//  public override void OnKingdomCreated(Kingdom createdKingdom)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnKingdomCreated(createdKingdom);
//  }

//  public override void OnVillageBecomeNormal(Village village)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnVillageBecomeNormal(village);
//  }

//  public override void OnVillageBeingRaided(Village village)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnVillageBeingRaided(village);
//  }

//  public override void OnVillageLooted(Village village)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnVillageLooted(village);
//  }

//  public override void OnConversationEnded(IEnumerable<CharacterObject> characters)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnConversationEnded(characters);
//  }

//  public override void OnAgentJoinedConversation(IAgent agent)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnAgentJoinedConversation(agent);
//  }

//  public override void OnMapEventEnded(MapEvent mapEvent)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMapEventEnded(mapEvent);
//  }

//  public override void OnMapEventStarted(
//    MapEvent mapEvent,
//    PartyBase attackerParty,
//    PartyBase defenderParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMapEventStarted(mapEvent, attackerParty, defenderParty);
//  }

//  public override void OnPrisonersChangeInSettlement(
//    Settlement settlement,
//    FlattenedTroopRoster prisonerRoster,
//    Hero prisonerHero,
//    bool takenFromDungeon)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPrisonersChangeInSettlement(settlement, prisonerRoster, prisonerHero, takenFromDungeon);
//  }

//  public override void OnMissionStarted(IMission mission)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMissionStarted(mission);
//  }

//  public override void OnPlayerBoardGameOver(
//    Hero opposingHero,
//    BoardGameHelper.BoardGameState state)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerBoardGameOver(opposingHero, state);
//  }

//  public override void OnRansomOfferedToPlayer(Hero captiveHero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnRansomOfferedToPlayer(captiveHero);
//  }

//  public override void OnRansomOfferCancelled(Hero captiveHero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnRansomOfferCancelled(captiveHero);
//  }

//  public override void OnPeaceOfferedToPlayer(
//    IFaction opponentFaction,
//    int tributeAmount,
//    int tributeDurationInDays)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPeaceOfferedToPlayer(opponentFaction, tributeAmount, tributeDurationInDays);
//  }

//  public override void OnTradeAgreementSigned(Kingdom kingdom, Kingdom other)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnTradeAgreementSigned(kingdom, other);
//  }

//  public override void OnPeaceOfferResolved(IFaction opponentFaction)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPeaceOfferResolved(opponentFaction);
//  }

//  public override void OnMarriageOfferedToPlayer(Hero suitor, Hero maiden)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMarriageOfferedToPlayer(suitor, maiden);
//  }

//  public override void OnMarriageOfferCanceled(Hero suitor, Hero maiden)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMarriageOfferCanceled(suitor, maiden);
//  }

//  public override void OnVassalOrMercenaryServiceOfferedToPlayer(Kingdom offeredKingdom)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnVassalOrMercenaryServiceOfferedToPlayer(offeredKingdom);
//  }

//  public override void OnCommonAreaStateChanged(
//    Alley alley,
//    Alley.AreaState oldState,
//    Alley.AreaState newState)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCommonAreaStateChanged(alley, oldState, newState);
//  }

//  public override void OnVassalOrMercenaryServiceOfferCanceled(Kingdom offeredKingdom)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnVassalOrMercenaryServiceOfferCanceled(offeredKingdom);
//  }

//  public override void BeforeMissionOpened()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.BeforeMissionOpened();
//  }

//  public override void OnPartyRemoved(PartyBase party)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyRemoved(party);
//  }

//  public override void OnPartySizeChanged(PartyBase party)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartySizeChanged(party);
//  }

//  public override void OnSettlementOwnerChanged(
//    Settlement settlement,
//    bool openToClaim,
//    Hero newOwner,
//    Hero oldOwner,
//    Hero capturerHero,
//    ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSettlementOwnerChanged(settlement, openToClaim, newOwner, oldOwner, capturerHero, detail);
//  }

//  public override void OnGovernorChanged(Town fortification, Hero oldGovernor, Hero newGovernor)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnGovernorChanged(fortification, oldGovernor, newGovernor);
//  }

//  public override void OnSettlementLeft(MobileParty party, Settlement settlement)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSettlementLeft(party, settlement);
//  }

//  public override void Tick(float dt)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.Tick(dt);
//  }

//  public override void OnSessionStart(CampaignGameStarter campaignGameStarter)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSessionStart(campaignGameStarter);
//  }

//  public override void OnAfterSessionStart(CampaignGameStarter campaignGameStarter)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnAfterSessionStart(campaignGameStarter);
//  }

//  public override void OnNewGameCreated(CampaignGameStarter campaignGameStarter)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnNewGameCreated(campaignGameStarter);
//  }

//  public override void OnGameEarlyLoaded(CampaignGameStarter campaignGameStarter)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnGameEarlyLoaded(campaignGameStarter);
//  }

//  public override void OnGameLoaded(CampaignGameStarter campaignGameStarter)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnGameLoaded(campaignGameStarter);
//  }

//  public override void OnGameLoadFinished()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnGameLoadFinished();
//  }

//  public override void OnPartyJoinedArmy(MobileParty mobileParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyJoinedArmy(mobileParty);
//  }

//  public override void OnPartyRemovedFromArmy(MobileParty mobileParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyRemovedFromArmy(mobileParty);
//  }

//  public override void OnPlayerArmyLeaderChangedBehavior()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerArmyLeaderChangedBehavior();
//  }

//  public override void OnArmyOverlaySetDirty()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnArmyOverlaySetDirty();
//  }

//  public override void OnPlayerDesertedBattle(int sacrificedMenCount)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerDesertedBattle(sacrificedMenCount);
//  }

//  public override void MissionTick(float dt)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.MissionTick(dt);
//  }

//  public override void OnChildConceived(Hero mother)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnChildConceived(mother);
//  }

//  public override void OnGivenBirth(Hero mother, List<Hero> aliveChildren, int stillbornCount)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnGivenBirth(mother, aliveChildren, stillbornCount);
//  }

//  public override void OnUnitRecruited(CharacterObject character, int amount)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnUnitRecruited(character, amount);
//  }

//  public override void OnPlayerBattleEnd(MapEvent mapEvent)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerBattleEnd(mapEvent);
//  }

//  public override void OnMissionEnded(IMission mission)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMissionEnded(mission);
//  }

//  public override void TickPartialHourlyAi(MobileParty party)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.TickPartialHourlyAi(party);
//  }

//  public override void QuarterDailyPartyTick(MobileParty party)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.QuarterDailyPartyTick(party);
//  }

//  public override void AiHourlyTick(MobileParty party, PartyThinkParams partyThinkParams)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.AiHourlyTick(party, partyThinkParams);
//  }

//  public override void HourlyTick()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.HourlyTick();
//  }

//  public override void QuarterHourlyTick()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.QuarterHourlyTick();
//  }

//  public override void HourlyTickParty(MobileParty mobileParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.HourlyTickParty(mobileParty);
//  }

//  public override void HourlyTickSettlement(Settlement settlement)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.HourlyTickSettlement(settlement);
//  }

//  public override void HourlyTickClan(Clan clan)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.HourlyTickClan(clan);
//  }

//  public override void DailyTick()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.DailyTick();
//  }

//  public override void DailyTickParty(MobileParty mobileParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.DailyTickParty(mobileParty);
//  }

//  public override void DailyTickTown(Town town)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.DailyTickTown(town);
//  }

//  public override void DailyTickSettlement(Settlement settlement)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.DailyTickSettlement(settlement);
//  }

//  public override void DailyTickHero(Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.DailyTickHero(hero);
//  }

//  public override void DailyTickClan(Clan clan)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.DailyTickClan(clan);
//  }

//  public override void WeeklyTick()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.WeeklyTick();
//  }

//  public override void CollectAvailableTutorials(ref List<CampaignTutorial> tutorials)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.CollectAvailableTutorials(ref tutorials);
//  }

//  public override void OnTutorialCompleted(string tutorial)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnTutorialCompleted(tutorial);
//  }

//  public override void BeforeGameMenuOpened(MenuCallbackArgs args)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.BeforeGameMenuOpened(args);
//  }

//  public override void AfterGameMenuInitialized(MenuCallbackArgs args)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.AfterGameMenuInitialized(args);
//  }

//  public override void OnBarterablesRequested(BarterData args)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBarterablesRequested(args);
//  }

//  public override void OnPartyVisibilityChanged(PartyBase party)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyVisibilityChanged(party);
//  }

//  public override void OnCompanionRemoved(
//    Hero companion,
//    RemoveCompanionAction.RemoveCompanionDetail detail)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCompanionRemoved(companion, detail);
//  }

//  public override void TrackDetected(Track track)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.TrackDetected(track);
//  }

//  public override void TrackLost(Track track)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.TrackLost(track);
//  }

//  public override void LocationCharactersAreReadyToSpawn(
//    Dictionary<string, int> unusedUsablePointCount)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.LocationCharactersAreReadyToSpawn(unusedUsablePointCount);
//  }

//  public override void LocationCharactersSimulated()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.LocationCharactersSimulated();
//  }

//  public override void OnBeforePlayerAgentSpawn(ref MatrixFrame spawnFrame)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBeforePlayerAgentSpawn(ref spawnFrame);
//  }

//  public override void OnPlayerAgentSpawned()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerAgentSpawned();
//  }

//  public override void OnPlayerUpgradedTroops(
//    CharacterObject upgradeFromTroop,
//    CharacterObject upgradeToTroop,
//    int number)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerUpgradedTroops(upgradeFromTroop, upgradeToTroop, number);
//  }

//  public override void OnHeroCombatHit(
//    CharacterObject attackerTroop,
//    CharacterObject attackedTroop,
//    PartyBase party,
//    WeaponComponentData usedWeapon,
//    bool isFatal,
//    int xp)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroCombatHit(attackerTroop, attackedTroop, party, usedWeapon, isFatal, xp);
//  }

//  public override void OnCharacterPortraitPopUpOpened(CharacterObject character)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCharacterPortraitPopUpOpened(character);
//  }

//  public override void OnCharacterPortraitPopUpClosed()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCharacterPortraitPopUpClosed();
//  }

//  public override void OnPlayerStartTalkFromMenu(Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerStartTalkFromMenu(hero);
//  }

//  public override void OnGameMenuOptionSelected(GameMenu gameMenu, GameMenuOption gameMenuOption)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnGameMenuOptionSelected(gameMenu, gameMenuOption);
//  }

//  public override void OnPlayerStartRecruitment(CharacterObject recruitTroopCharacter)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerStartRecruitment(recruitTroopCharacter);
//  }

//  public override void OnBeforePlayerCharacterChanged(Hero oldPlayer, Hero newPlayer)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBeforePlayerCharacterChanged(oldPlayer, newPlayer);
//  }

//  public override void OnPlayerCharacterChanged(
//    Hero oldPlayer,
//    Hero newPlayer,
//    MobileParty newPlayerParty,
//    bool isMainPartyChanged)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerCharacterChanged(oldPlayer, newPlayer, newPlayerParty, isMainPartyChanged);
//  }

//  public override void OnClanLeaderChanged(Hero oldLeader, Hero newLeader)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnClanLeaderChanged(oldLeader, newLeader);
//  }

//  public override void OnSiegeEventStarted(SiegeEvent siegeEvent)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSiegeEventStarted(siegeEvent);
//  }

//  public override void OnPlayerSiegeStarted()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerSiegeStarted();
//  }

//  public override void OnSiegeEventEnded(SiegeEvent siegeEvent)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSiegeEventEnded(siegeEvent);
//  }

//  public override void OnSiegeAftermathApplied(
//    MobileParty attackerParty,
//    Settlement settlement,
//    SiegeAftermathAction.SiegeAftermath aftermathType,
//    Clan previousSettlementOwner,
//    Dictionary<MobileParty, float> partyContributions)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSiegeAftermathApplied(attackerParty, settlement, aftermathType, previousSettlementOwner, partyContributions);
//  }

//  public override void OnSiegeBombardmentHit(
//    MobileParty besiegerParty,
//    Settlement besiegedSettlement,
//    BattleSideEnum side,
//    SiegeEngineType weapon,
//    SiegeBombardTargets target)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSiegeBombardmentHit(besiegerParty, besiegedSettlement, side, weapon, target);
//  }

//  public override void OnSiegeBombardmentWallHit(
//    MobileParty besiegerParty,
//    Settlement besiegedSettlement,
//    BattleSideEnum side,
//    SiegeEngineType weapon,
//    bool isWallCracked)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSiegeBombardmentWallHit(besiegerParty, besiegedSettlement, side, weapon, isWallCracked);
//  }

//  public override void OnSiegeEngineDestroyed(
//    MobileParty besiegerParty,
//    Settlement besiegedSettlement,
//    BattleSideEnum side,
//    SiegeEngineType destroyedEngine)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSiegeEngineDestroyed(besiegerParty, besiegedSettlement, side, destroyedEngine);
//  }

//  public override void OnTradeRumorIsTaken(List<TradeRumor> newRumors, Settlement sourceSettlement = null)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnTradeRumorIsTaken(newRumors, sourceSettlement);
//  }

//  public override void OnCheckForIssue(Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCheckForIssue(hero);
//  }

//  public override void OnIssueUpdated(
//    IssueBase issue,
//    IssueBase.IssueUpdateDetails details,
//    Hero issueSolver)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnIssueUpdated(issue, details, issueSolver);
//  }

//  public override void OnTroopsDeserted(MobileParty mobileParty, TroopRoster desertedTroops)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnTroopsDeserted(mobileParty, desertedTroops);
//  }

//  public override void OnTroopRecruited(
//    Hero recruiterHero,
//    Settlement recruitmentSettlement,
//    Hero recruitmentSource,
//    CharacterObject troop,
//    int amount)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnTroopRecruited(recruiterHero, recruitmentSettlement, recruitmentSource, troop, amount);
//  }

//  public override void OnTroopGivenToSettlement(
//    Hero giverHero,
//    Settlement recipientSettlement,
//    TroopRoster roster)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnTroopGivenToSettlement(giverHero, recipientSettlement, roster);
//  }

//  public override void OnItemSold(
//    PartyBase receiverParty,
//    PartyBase payerParty,
//    ItemRosterElement itemRosterElement,
//    int number,
//    Settlement currentSettlement)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnItemSold(receiverParty, payerParty, itemRosterElement, number, currentSettlement);
//  }

//  public override void OnCaravanTransactionCompleted(
//    MobileParty caravanParty,
//    Town town,
//    List<(EquipmentElement, int)> itemRosterElements)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCaravanTransactionCompleted(caravanParty, town, itemRosterElements);
//  }

//  public override void OnPrisonerSold(
//    PartyBase sellerParty,
//    PartyBase buyerParty,
//    TroopRoster prisoners)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPrisonerSold(sellerParty, buyerParty, prisoners);
//  }

//  public override void OnPartyDisbanded(MobileParty disbandParty, Settlement relatedSettlement)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyDisbanded(disbandParty, relatedSettlement);
//  }

//  public override void OnPartyDisbandStarted(MobileParty disbandParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyDisbandStarted(disbandParty);
//  }

//  public override void OnPartyDisbandCanceled(MobileParty disbandParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyDisbandCanceled(disbandParty);
//  }

//  public override void OnBuildingLevelChanged(Town town, Building building, int levelChange)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBuildingLevelChanged(town, building, levelChange);
//  }

//  public override void OnHideoutSpotted(PartyBase party, PartyBase hideoutParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHideoutSpotted(party, hideoutParty);
//  }

//  public override void OnHideoutDeactivated(Settlement hideout)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHideoutDeactivated(hideout);
//  }

//  public override void OnHeroSharedFoodWithAnother(
//    Hero supporterHero,
//    Hero supportedHero,
//    float influence)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroSharedFoodWithAnother(supporterHero, supportedHero, influence);
//  }

//  public override void OnItemsDiscardedByPlayer(ItemRoster roster)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnItemsDiscardedByPlayer(roster);
//  }

//  public override void OnPlayerInventoryExchange(
//    List<(ItemRosterElement, int)> purchasedItems,
//    List<(ItemRosterElement, int)> soldItems,
//    bool isTrading)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerInventoryExchange(purchasedItems, soldItems, isTrading);
//  }

//  public override void OnPersuasionProgressCommitted(
//    Tuple<PersuasionOptionArgs, PersuasionOptionResult> progress)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPersuasionProgressCommitted(progress);
//  }

//  public override void OnQuestCompleted(QuestBase quest, QuestBase.QuestCompleteDetails detail)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnQuestCompleted(quest, detail);
//  }

//  public override void OnQuestStarted(QuestBase quest)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnQuestStarted(quest);
//  }

//  public override void OnItemProduced(ItemObject itemObject, Settlement settlement, int count)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnItemProduced(itemObject, settlement, count);
//  }

//  public override void OnItemConsumed(ItemObject itemObject, Settlement settlement, int count)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnItemConsumed(itemObject, settlement, count);
//  }

//  public override void OnPartyConsumedFood(MobileParty party)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyConsumedFood(party);
//  }

//  public override void OnNewIssueCreated(IssueBase issue)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnNewIssueCreated(issue);
//  }

//  public override void OnIssueOwnerChanged(IssueBase issue, Hero oldOwner)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnIssueOwnerChanged(issue, oldOwner);
//  }

//  public override void OnBeforeMainCharacterDied(
//    Hero victim,
//    Hero killer,
//    KillCharacterAction.KillCharacterActionDetail detail,
//    bool showNotification = true)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBeforeMainCharacterDied(victim, killer, detail, showNotification);
//  }

//  public override void OnGameOver()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnGameOver();
//  }

//  public override void SiegeCompleted(
//    Settlement siegeSettlement,
//    MobileParty attackerParty,
//    bool isWin,
//    MapEvent.BattleTypes battleType)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.SiegeCompleted(siegeSettlement, attackerParty, isWin, battleType);
//  }

//  public override void AfterSiegeCompleted(
//    Settlement siegeSettlement,
//    MobileParty attackerParty,
//    bool isWin,
//    MapEvent.BattleTypes battleType)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.AfterSiegeCompleted(siegeSettlement, attackerParty, isWin, battleType);
//  }

//  public override void SiegeEngineBuilt(
//    SiegeEvent siegeEvent,
//    BattleSideEnum side,
//    SiegeEngineType siegeEngine)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.SiegeEngineBuilt(siegeEvent, side, siegeEngine);
//  }

//  public override void RaidCompleted(BattleSideEnum winnerSide, RaidEventComponent raidEvent)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.RaidCompleted(winnerSide, raidEvent);
//  }

//  public override void ForceSuppliesCompleted(
//    BattleSideEnum winnerSide,
//    ForceSuppliesEventComponent forceSuppliesEvent)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.ForceSuppliesCompleted(winnerSide, forceSuppliesEvent);
//  }

//  public override void ForceVolunteersCompleted(
//    BattleSideEnum winnerSide,
//    ForceVolunteersEventComponent forceVolunteersEvent)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.ForceVolunteersCompleted(winnerSide, forceVolunteersEvent);
//  }

//  public override void OnHideoutBattleCompleted(
//    BattleSideEnum winnerSide,
//    HideoutEventComponent hideoutEventComponent)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHideoutBattleCompleted(winnerSide, hideoutEventComponent);
//  }

//  public override void OnClanDestroyed(Clan destroyedClan)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnClanDestroyed(destroyedClan);
//  }

//  public override void OnNewItemCrafted(
//    ItemObject itemObject,
//    ItemModifier overriddenItemModifier,
//    bool isCraftingOrderItem)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnNewItemCrafted(itemObject, overriddenItemModifier, isCraftingOrderItem);
//  }

//  public override void OnWorkshopOwnerChanged(Workshop workshop, Hero oldOwner)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnWorkshopOwnerChanged(workshop, oldOwner);
//  }

//  public override void OnWorkshopInitialized(Workshop workshop)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnWorkshopInitialized(workshop);
//  }

//  public override void OnWorkshopTypeChanged(Workshop workshop)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnWorkshopTypeChanged(workshop);
//  }

//  public override void OnMainPartyPrisonerRecruited(FlattenedTroopRoster roster)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMainPartyPrisonerRecruited(roster);
//  }

//  public override void OnPrisonerDonatedToSettlement(
//    MobileParty donatingParty,
//    FlattenedTroopRoster donatedPrisoners,
//    Settlement donatedSettlement)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPrisonerDonatedToSettlement(donatingParty, donatedPrisoners, donatedSettlement);
//  }

//  public override void OnEquipmentSmeltedByHero(Hero hero, EquipmentElement equipmentElement)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnEquipmentSmeltedByHero(hero, equipmentElement);
//  }

//  public override void OnPrisonerTaken(FlattenedTroopRoster roster)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPrisonerTaken(roster);
//  }

//  public override void OnBeforeSave()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBeforeSave();
//  }

//  public override void OnSaveStarted()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSaveStarted();
//  }

//  public override void OnSaveOver(bool isSuccessful, string saveName)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnSaveOver(isSuccessful, saveName);
//  }

//  public override void OnPrisonerReleased(FlattenedTroopRoster roster)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPrisonerReleased(roster);
//  }

//  public override void OnHeroChangedClan(Hero hero, Clan oldClan)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroChangedClan(hero, oldClan);
//  }

//  public override void OnHeroGetsBusy(Hero hero, HeroGetsBusyReasons heroGetsBusyReason)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroGetsBusy(hero, heroGetsBusyReason);
//  }

//  public override void OnPlayerTradeProfit(int profit)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerTradeProfit(profit);
//  }

//  public override void CraftingPartUnlocked(CraftingPiece craftingPiece)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.CraftingPartUnlocked(craftingPiece);
//  }

//  public override void OnClanEarnedGoldFromTribute(Clan receiverClan, IFaction payingFaction)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnClanEarnedGoldFromTribute(receiverClan, payingFaction);
//  }

//  public override void OnCollectLootItems(PartyBase winnerParty, ItemRoster gainedLoots)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCollectLootItems(winnerParty, gainedLoots);
//  }

//  public override void OnHeroTeleportationRequested(
//    Hero hero,
//    Settlement targetSettlement,
//    MobileParty targetParty,
//    TeleportHeroAction.TeleportationDetail detail)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroTeleportationRequested(hero, targetSettlement, targetParty, detail);
//  }

//  public override void OnClanInfluenceChanged(Clan clan, float change)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnClanInfluenceChanged(clan, change);
//  }

//  public override void OnPlayerPartyKnockedOrKilledTroop(CharacterObject strikedTroop)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerPartyKnockedOrKilledTroop(strikedTroop);
//  }

//  public override void OnPlayerEarnedGoldFromAsset(
//    DefaultClanFinanceModel.AssetIncomeType incomeType,
//    int incomeAmount)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerEarnedGoldFromAsset(incomeType, incomeAmount);
//  }

//  public override void OnPartyLeaderChangeOfferCanceled(MobileParty party)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyLeaderChangeOfferCanceled(party);
//  }

//  public override void OnPartyLeaderChanged(MobileParty mobileParty, Hero oldLeader)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyLeaderChanged(mobileParty, oldLeader);
//  }

//  public override void OnMainPartyStarving()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMainPartyStarving();
//  }

//  public override void OnPlayerJoinedTournament(Town town, bool isParticipant)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPlayerJoinedTournament(town, isParticipant);
//  }

//  public override void OnCraftingOrderCompleted(
//    Town town,
//    CraftingOrder craftingOrder,
//    ItemObject craftedItem,
//    Hero completerHero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCraftingOrderCompleted(town, craftingOrder, craftedItem, completerHero);
//  }

//  public override void OnItemsRefined(Hero hero, TaleWorlds.Core.Crafting.RefiningFormula refineFormula)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnItemsRefined(hero, refineFormula);
//  }

//  public override void OnMapEventContinuityNeedsUpdate(IFaction faction)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMapEventContinuityNeedsUpdate(faction);
//  }

//  public override void OnHeirSelectionRequested(Dictionary<Hero, int> heirApparents)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeirSelectionRequested(heirApparents);
//  }

//  public override void OnHeirSelectionOver(Hero selectedHeir)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeirSelectionOver(selectedHeir);
//  }

//  public override void OnCharacterCreationInitialized(
//    CharacterCreationManager characterCreationManager)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCharacterCreationInitialized(characterCreationManager);
//  }

//  public override void OnShipDestroyed(
//    PartyBase owner,
//    Ship ship,
//    DestroyShipAction.ShipDestroyDetail detail)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnShipDestroyed(owner, ship, detail);
//  }

//  public override void OnPartyLeftArmy(MobileParty party, Army army)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyLeftArmy(party, army);
//  }

//  public override void OnShipOwnerChanged(
//    Ship ship,
//    PartyBase oldOwner,
//    ChangeShipOwnerAction.ShipOwnerChangeDetail changeDetail)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnShipOwnerChanged(ship, oldOwner, changeDetail);
//  }

//  public override void OnShipRepaired(Ship ship, Settlement repairPort)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnShipRepaired(ship, repairPort);
//  }

//  public override void OnFigureheadUnlocked(Figurehead figurehead)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnFigureheadUnlocked(figurehead);
//  }

//  public override void OnPartyAddedToMapEvent(PartyBase party)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnPartyAddedToMapEvent(party);
//  }

//  public override void OnIncidentResolved(Incident incident)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnIncidentResolved(incident);
//  }

//  public override void OnMobilePartyNavigationStateChanged(MobileParty mobileParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMobilePartyNavigationStateChanged(mobileParty);
//  }

//  public override void OnMobilePartyJoinedToSiegeEvent(MobileParty mobileParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMobilePartyJoinedToSiegeEvent(mobileParty);
//  }

//  public override void OnMobilePartyLeftSiegeEvent(MobileParty mobileParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMobilePartyLeftSiegeEvent(mobileParty);
//  }

//  public override void OnBlockadeActivated(SiegeEvent siegeEvent)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBlockadeActivated(siegeEvent);
//  }

//  public override void OnBlockadeDeactivated(SiegeEvent siegeEvent)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnBlockadeDeactivated(siegeEvent);
//  }

//  public override void OnMapMarkerCreated(MapMarker mapMarker)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMapMarkerCreated(mapMarker);
//  }

//  public override void OnMapMarkerRemoved(MapMarker mapMarker)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMapMarkerRemoved(mapMarker);
//  }

//  public override void OnMercenaryServiceStarted(
//    Clan mercenaryClan,
//    StartMercenaryServiceAction.StartMercenaryServiceActionDetails details)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMercenaryServiceStarted(mercenaryClan, details);
//  }

//  public override void OnMercenaryServiceEnded(
//    Clan mercenaryClan,
//    EndMercenaryServiceAction.EndMercenaryServiceActionDetails details)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMercenaryServiceEnded(mercenaryClan, details);
//  }

//  public override void OnAllianceStarted(Kingdom kingdom1, Kingdom kingdom2)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnAllianceStarted(kingdom1, kingdom2);
//  }

//  public override void OnAllianceEnded(Kingdom kingdom1, Kingdom kingdom2)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnAllianceEnded(kingdom1, kingdom2);
//  }

//  public override void OnCallToWarAgreementStarted(
//    Kingdom callingKingdom,
//    Kingdom calledKingdom,
//    Kingdom kingdomToCallToWarAgainst)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCallToWarAgreementStarted(callingKingdom, calledKingdom, kingdomToCallToWarAgainst);
//  }

//  public override void OnCallToWarAgreementEnded(
//    Kingdom callingKingdom,
//    Kingdom calledKingdom,
//    Kingdom kingdomToCallToWarAgainst)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnCallToWarAgreementEnded(callingKingdom, calledKingdom, kingdomToCallToWarAgainst);
//  }

//  public override void CanHeroLeadParty(Hero hero, ref bool result)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//    {
//      eventReceiver.CanHeroLeadParty(hero, ref result);
//      if (!result)
//        break;
//    }
//  }

//  public override void CanHeroMarry(Hero hero, ref bool result)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//    {
//      eventReceiver.CanHeroMarry(hero, ref result);
//      if (!result)
//        break;
//    }
//  }

//  public override void CanHeroEquipmentBeChanged(Hero hero, ref bool result)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//    {
//      eventReceiver.CanHeroEquipmentBeChanged(hero, ref result);
//      if (!result)
//        break;
//    }
//  }

//  public override void CanBeGovernorOrHavePartyRole(Hero hero, ref bool result)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//    {
//      eventReceiver.CanBeGovernorOrHavePartyRole(hero, ref result);
//      if (!result)
//        break;
//    }
//  }

//  public override void CanHeroDie(
//    Hero hero,
//    KillCharacterAction.KillCharacterActionDetail causeOfDeath,
//    ref bool result)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//    {
//      eventReceiver.CanHeroDie(hero, causeOfDeath, ref result);
//      if (!result)
//        break;
//    }
//  }

//  public override void CanHeroBecomePrisoner(Hero hero, ref bool result)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//    {
//      eventReceiver.CanHeroBecomePrisoner(hero, ref result);
//      if (!result)
//        break;
//    }
//  }

//  public override void CanPlayerMeetWithHeroAfterConversation(Hero hero, ref bool result)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//    {
//      eventReceiver.CanPlayerMeetWithHeroAfterConversation(hero, ref result);
//      if (!result)
//        break;
//    }
//  }

//  public override void CanMoveToSettlement(Hero hero, ref bool result)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//    {
//      eventReceiver.CanMoveToSettlement(hero, ref result);
//      if (!result)
//        break;
//    }
//  }

//  public override void CanHaveCampaignIssues(Hero hero, ref bool result)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//    {
//      eventReceiver.CanHaveCampaignIssues(hero, ref result);
//      if (!result)
//        break;
//    }
//  }

//  public override void IsSettlementBusy(Settlement settlement, object asker, ref int priority)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.IsSettlementBusy(settlement, asker, ref priority);
//  }

//  public override void OnHeroUnregistered(Hero hero)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnHeroUnregistered(hero);
//  }

//  public override void OnShipCreated(Ship ship, Settlement createdSettlement)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnShipCreated(ship, createdSettlement);
//  }

//  public override void OnConfigChanged()
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnConfigChanged();
//  }

//  public override void OnMobilePartyRaftStateChanged(MobileParty mobileParty)
//  {
//    foreach (CampaignEventReceiver eventReceiver in this._eventReceivers)
//      eventReceiver.OnMobilePartyRaftStateChanged(mobileParty);
//  }
//}
