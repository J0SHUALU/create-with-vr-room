# Joshua VR Rooms

A Meta Quest VR experience built in Unity 6 (6000.4.7f1) with the XR Interaction Toolkit and OpenXR. It contains three scenes: a start hub, the Create with VR living room and the VR Room station.

## Scenes

| Scene | Path | What it is |
| --- | --- | --- |
| Start | Assets/Scenes/Start.unity | Entry hub with the room selection screen |
| Living Room | Assets/Scenes/Joshua Room.unity | Create with VR course project |
| Station | Assets/Scenes/VR Room Station.unity | VR Room assignment scene built from the provided assets |

Build order: Start, Joshua Room, VR Room Station.

## Running

1. Open the project in Unity 6000.4.7f1 with Android Build Support.
2. Open Assets/Scenes/Start.unity.
3. Quest over Link: press Play. Standalone Quest: File > Build Profiles > Android > Build And Run.
4. Without a headset, enable the XR Interaction Simulator in the Living Room scene.

## Controls

| Input | Action |
| --- | --- |
| Grip | Grab objects, the doorknob and the projector |
| Trigger | Select UI, activate held objects (TV remote) |
| Left thumbstick | Smooth turn |
| Right thumbstick | Snap turn |
| Primary button or thumbstick touch | Show the teleport ray, release to teleport |
| Y (left) | Open or close the ring menu |
| B (right), Living Room | Reset panel |

## Where to find each feature

- Object grabbing: Station > Interactables (boxes, tubes, containers, chess pieces, mug, flashlight, pillows, statues). Living Room > DYNAMIC (ball, racket, hats, remote).
- Sockets: Station > SOCKETS. Chess Square Sockets accept the Chess layer, Flashlight Socket accepts the Flashlight layer, Mug Socket accepts the Mug layer.
- Physics door: Station > Level > GlassDoor > glass_panel_1_door (Rigidbody, HingeJoint, XRGrabInteractable on the doorknob, DoorCreak).
- Start UI: Start > INTERFACE > Start Canvas.
- In-project UI: the Signal Ring Menu on the left controller in every scene, plus the Objectives Board and tooltips in the Station.
- Wall clock: Station > Wall Clock (AnalogClock and DigitalClock).
- Animated hands: XR Origin Rig prefab > Left/Right Hand Controller > Hand Visual (ControllerHandAnimator).
- Scene navigation: SceneNavigator on the XR Origin Rig prefab.

## Scripts

All gameplay scripts are in Assets/Scripts.

| Script | Purpose |
| --- | --- |
| SceneNavigator | Fades out, loads a scene by name, fades in |
| MenuToggle | Opens and closes the ring menu from a controller button |
| AnalogClock | Rotates hour, minute and second hands from DateTime.Now |
| DigitalClock | Shows the time, seconds and date |
| ControllerHandAnimator | Curls finger joints from grip and trigger values |
| DoorCreak | Door audio, latch sound and frame collision setup |
| DoorObjective | Reports the door objective once it opens past 45 degrees |
| ObjectiveTracker / ObjectiveTrigger | Station task list and completion banner |
| ProjectorSwitch | Toggles the projector beam, light and hum |
| HoverHighlight | Emission glow while an interactable is hovered |
| ProximityTooltip / LookAtCamera | Tooltips that face the player and fade in nearby |
| ToggleLightSource | Light toggle helper |

Course scripts from the Create with VR library are in Assets/_Course Library/Scripts.
