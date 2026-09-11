# AR Museum

A Unity augmented-reality museum prototype that uses tracked images and detected planes to position exhibit content and show contextual descriptions.

## Implementation

- `GroundPositioningHandler` reacts to tracked-image events, finds a nearby active plane and positions exhibit content with a configured offset.
- Tracked-image names select the corresponding exhibit description.
- `PlaneClassifier` filters planes by alignment and height for the placement workflow.
- Camera-facing helper scripts support the presentation of exhibit content.

## Project setup

The committed project uses **Unity 6000.0.51f1**, **AR Foundation 6.1.1** and **URP 17.1.0**.

Open the repository folder in the matching Unity editor, restore packages, then inspect `Assets/Scenes/AR_Museum.unity`. Check the image library, prefabs, description list, offsets and platform XR configuration before testing on a supported device.

## Source guide

| Path | Purpose |
| --- | --- |
| [GroundPositioningHandler.cs](Assets/Scripts/GroundPositioningHandler.cs) | Image-tracking events, exhibit placement and descriptions |
| [PlaneClassifier.cs](Assets/Scripts/PlaneClassifier.cs) | Plane filtering |
| [Assets/Scripts](Assets/Scripts) | Camera-facing and presentation helpers |
| [AR_Museum.unity](Assets/Scenes/AR_Museum.unity) | Museum scene |

## Status

This is a development prototype. It requires scene configuration and device testing; no published application or performance benchmark is claimed here.

[More Unity & XR projects](https://github.com/aminzoroufi/aminzoroufi/blob/main/projects/unity-xr.md)
