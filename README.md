<img src="https://github.com/prom3theu5/WindowsOnDeck/assets/1518610/a0a3f9de-6589-4b15-8345-eb96abd16236" width="200" />

# [WindowsOnDeck](https://github.com/prom3theu5/WindowsOnDeck)

## About

WindowsOnDeck: Automatic configuration Tool for Windows 11 running on a Steam Deck.

## What does this include?

**All items are optional, with checkboxes to enable / disable them**

## Windows Tweaks

* Disable Hibernation
* Set CPU Idle Min to 0% (Reduce fan speed)
* Disable Password on wake from sleep
* Set internal Clock to UTC
* Disable GameDVR
* Remove OneDrive
* Disable Unneeded Services
* Cleanup Windows
* Set display scaling to 100% down from 125%

## Downloads

* APU Chipset Drivers from Valve
* Audio Drivers 1/2 from Valve (cs35l41)
* Audio Drivers 2/2 from Valve (NAU88L21)
* Wireless LAN Drivers from Windows Update
* Bluetooth Drivers from Windows Update
* MicroSD Card Reader Drivers from Windows Update
* VC++ All in One Redistributable
* DirectX Setup
* DotNet 6.0
* ViGEmBus Setup
* RivaTuner Setup
* Steam Deck Tools
* EqualizerAPO
* EqualizerAPO VST Plugin
* Playnite Game Launcher
* Steam
* CRU - Custom Resolution Utility
* Ciphrays Custom Binary for CRU

Based on the powershell scripts from [CelesteHeartsong](https://github.com/CelesteHeartsong/SteamDeckAutomatedInstall)

## Instructions for Install

* Download ZIP package from releases page.
* Extract to a directory
* Run WindowsOnDeck.exe
* Click Next after agreeing you take responsibility.
* Select Windows Tweaks to Install - by default the first four are enabled, with the others being optional - scroll down.
* Click next to apply windows tweaks.
* Select which downloads to enable - all are enabled by default
* Click next to Download files
* Click next to install files, and apply configuration. Please note, if you are prompted to restart at any point - say `NO`.
   * APU Graphics drivers will probably prompt for a restart, say 'NO'
   * EquilizerAPO will create a popup asking which audio device to apply to.
      * Select "Speakers"
      * Move to the next tab (Capture Devices) and select "Microphone"
* Click next to view the final page
* Reboot Steam Deck with the close button.

## CRU

If you selected to install CRU, you will need to follow the steps from here: [https://baldsealion.com/Refresh-Rate-and-RTSS.html](https://baldsealion.com/Refresh-Rate-and-RTSS.html) There will be a shortcut on the desktop, and the custom binary file can be found at `c:\CRU\steamdeck.bin`

## Controller Setup

If you selected to install Steam Deck Tools, you can follow the guide here to customise your controller layouts: [https://baldsealion.com/Controller-Setup.html](https://baldsealion.com/Controller-Setup.html)

## Demo of usage

[Here on Youtube](https://www.youtube.com/watch?v=D3TLr1cjRc0)

*In parallels on a mac, not on steam deck so there is an error you can see when SteamDeckTools starts ^^*
