# BroadcastsPlus
Adds Broadcasts which play over time.

[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/powered-by-coffee.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/reading-6th-grade-level.svg)](https://forthebadge.com)

## Features
* MOTD that is shown on a player join for the player
* Broadcasts in a given intervall
* C.A.S.S.I.E Announcements in a given intervall
* Manage the Broadcasts/C.A.S.S.I.E Announcements ingame

## Installation
1. [Install Synapse](https://github.com/SynapseSL/Synapse/wiki#hosting-guides)
2. Place the BroadcastsPlus.dll file that you can download [here](https://github.com/TheVoidNebula/BroadcastsPlus/releases) in your plugin directory
3. Restart/Start your server.

## Commands
Command  | Usage | Aliases | Permission | Description
------------ | ------------ | ------------- | ------------ | ------------ 
`broadcastplus` | `broadcastplus` | `bp` | `bd.broadcasts` | Show all Broadcasts commands
`broadcastplus` | `broadcastplus` `list` | `bp` | `bd.broadcasts.list` | Show all available Broadcasts with their IDs, Durations and Text
`broadcastplus` | `broadcastplus` `add` `<ID as number>` `<Duration as number>` `<Text>` | `bp` | `bd.broadcasts.list` | Add a new Broadcast directly into the Configs
`broadcastplus` | `broadcastplus` `remove` `<ID as number>` | `bp` | `bd.broadcasts.remove` | Remove a already existing Broadcast via the ID
`broadcastplus` | `broadcastplus` `force` `<ID as number>` | `bp` | `bd.broadcasts.force` | Show a Broadcast to everyone via the ID
`broadcastplus` | `broadcastplus` `reset`  | `bp` | `bd.broadcasts.reset` | Reset the whole BroadcastsPlus Config
`motd` | `motd` `set`  | `bp` | `bd.motd` | Show all MOTD commands
`motd` | `motd` `set`  | `bp` | `bd.motd.set` | Set the MOTD ingame
`motd` | `motd` `enable/on`  | `bp` | `bd.motd.enable` | Enables the MOTD
`motd` | `motd` `disable/off`  | `bp` | `bd.motd.enable` | Disables the MOTD
`motd` | `motd` `force`  | `bp` | `bd.motd.enable` | Shows the MOTD to everyone
`cassieannouncement` | `cassieannouncement` | `ca`, `announcements` | `bd.announcements` | Show all C.A.S.S.I.E Announcement commands
`cassieannouncement` | `cassieannouncement` `list` | `ca`, `announcements` | `bd.announcements.list` | Show all available  C.A.S.S.I.E Announcements with their IDs, Durations and Text
`cassieannouncement` | `cassieannouncement` `add` `<ID as number>` `<Text>` | `ca`, `announcements` | `bd.announcements.add` | Add a new C.A.S.S.I.E Announcement directly into the Configs
`cassieannouncement` | `cassieannouncement` `remove` `<ID as number>` | `ca`, `announcements` | `bd.announcements.remove` | Remove a already existing C.A.S.S.I.E Announcement via the ID
`cassieannouncement` | `cassieannouncement` `force` `<ID as number>` | `ca`, `announcements` | `bd.announcements.force` | Show a C.A.S.S.I.E Announcement to everyone via the ID

## Config
Name  | Type | Default | Description
------------ | ------------ | ------------- | ------------ 
`IsEnabled` | Boolean | true | Is this plugin enabled?
`EnableBroadcasts` | Boolean | true | Should Broadcasts be posted in a given intervall?
`BroadcastsIntervall` | ushort | 60 | The intervall in which the Broadcasts are posted
`EnableCassieAnnouncements` | Boolean | false | Should C.A.S.S.I.E announcements be posted in a given intervall?
`CassieAnnouncementsIntervall` | ushort | 180 | The intervall in which the C.A.S.S.I.E announcements are posted
`MOTD` | MOTD | IsEnabled = true, Duration = 10, Text = "<color=green>Welcome to our Server!</color>\nWe are currently looking for staff!" | The intervall in which the C.A.S.S.I.E announcements are posted
`Broadcasts` | List | ID = 1, Duration = 10, Text = "<b>This is a Test Broadcast!</b>" | The Broadcasts
`CassieAnnouncements` | List | ID = 1, Text = "based" | The C.A.S.S.I.E Announcements
`IsEnabled` | Boolean | true | Is this plugin enabled?

## Config.syml
```yml
[BroadcastsPlus]
{
broadcasts:
- iD: 1
  duration: 10
  text: <b>This is a Test Broadcast!</b>
- iD: 2
  duration: 10
  text: <b>Remember to read our rules!</b>
cassieAnnouncements:
- iD: 1
  text: based
# Should this plugin be enabled?
isEnabled: true
# Should Broadcasts be posted in a given intervall?
enableBroadcasts: true
# The intervall in which the Broadcasts are posted
broadcastsIntervall: 60
# Should C.A.S.S.I.E announcements be posted in a given intervall?
enableCassieAnnouncements: true
# The intervall in which the C.A.S.S.I.E announcements are posted
cassieAnnouncementsIntervall: 180
# The intervall in which the C.A.S.S.I.E announcements are posted
mOTD:
  isEnabled: true
  duration: 10
  text: >-
    <color=green>Welcome to our Server!</color>

    We are currently looking for staff!
}
```
