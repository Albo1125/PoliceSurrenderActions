# PoliceSurrenderActions
PoliceSurrenderActions is a UK-based resource for FiveM by Albo1125 that provides commands to interact with police. It is available at [https://github.com/Albo1125/PoliceSurrenderActions](https://github.com/Albo1125/PoliceSurrenderActions)

## Installation & Usage
1. Download the latest release.
2. Unzip the PoliceSurrenderActions folder into your resources folder on your FiveM server.
3. Add the following to your server.cfg file:
```text
ensure PoliceSurrenderActions
```
4. Optionally, customise the commands in sv_PoliceSurrenderActions.lua.

## Commands & Controls
* /cuff ID - Toggles cuffs for the player with ID (if ID unspecified toggles for yourself). Alias /c
* /frontcuff ID - Toggles front cuffs for the player with ID (if ID unspecified toggles for yourself). Alias /fc
* /drag ID - Toggles drag for the player with ID. Alias /d
* /stopdrag - Releases yourself from being dragged by another player.
* /handsup - Toggles handsup for your player. Alias /hu
* /kneel - Toggles kneel for your player.
* /facedown - Toggles facedown for your player. Alias /fd
* /dropweapon - Drops your current weapon on the ground.


## Improvements & Licencing
Please view the license. Improvements and new feature additions are very welcome, please feel free to create a pull request. As a guideline, please do not release separate versions with minor modifications, but contribute to this repository directly. However, if you really do wish to release modified versions of my work, proper credit is always required and you should always link back to this original source and respect the licence.

## Libraries used (many thanks to their authors)
* [CitizenFX.Core.Client](https://www.nuget.org/packages/CitizenFX.Core.Client)
