# Luma.SmartHub

[![Chat on Gitter](https://img.shields.io/gitter/room/czesiu/ManagedBass.svg)](https://gitter.im/czesiu/Luma.SmartHub)
[![Build Status](https://ci.appveyor.com/api/projects/status/rr9jjv65oi72ulde/branch/master?svg=true)](https://ci.appveyor.com/project/czesiu/luma-smarthub)

Integration platform for smart home solutions.

Targetting Raspberry Pi, but also should work on Orange Pi or Odroid and other linux ARM boards.

## Features

- Multiroom audio with multiple input methods like DLNA, UpNP, remote playing (maybe Google Cast too?), line in and bluetooth. Audio can be played to multiple outputs (rooms) at same time - it is switchable by user. 
- Lighting control by relay switches connected directly to board GPIO or by external AVR module (e.g. [Controllino PLC](http://controllino.biz/)).
- Led strip control. Predefined animations e.g. VU meter. Can be activated if an event occurs e.g. doorbell press.
- Mp3 ringtone. Mp3 file can be played if an event occurs e.g. doorbell press, phone call or some notification.
- Video center. USB and IP cameras in one place. Remote access. Recording and sending to remote FTP. Motion detection and notification.
- Face detection and identification of persons
- Motion detection - from PIR sensors and cameras
- Voice control - integration with Google Cloud Speeh API, Microsoft Speach Recognition engine or other APIs.
- Speech synthesis / voice information - if an event occurs e.g. system says "Good morning <your name>" when you wake up.
- Heating control - integration with wireless thermostatic heads e.g. [Open HR20](https://github.com/OpenHR20/OpenHR20)
- Remote control - using infrared and radio frequency. For controlling TV, air conditioners and other appliances - also as other input to system. Integration with existing solutions e.g. Broadlink Black Bean IR Remote Controller or RM Pro Remote Controller.
- Wireless buttons - as other inputs.
- Presence detection - adjusting home state, notification about burglary.
- Notifications - various methods: sound, light, email and phone. 

SmartHub is modular. You can add your own module implementation or add new features. So above list is only main planned features list.

## Modules

Here's list of started modules:

#### [Audio](https://github.com/czesiu/Luma.SmartHub.Audio.Bass)
Audio features implementation. Currently using un4seen Bass library, but other implementations will be added in the future.

#### [Led strip](https://github.com/czesiu/Luma.SmartHub.LedStrip)
Led strip drivers implementations. Currently only for WS2812B led strips.

### [Controllino](https://github.com/czesiu/Luma.SmartHub.Controllino)
Controllino firmware to control lights.
