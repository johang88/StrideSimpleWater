# Stride Simple Water

Simple water material with screen space refractions, bad fake caustics, smoth edges and some depth fog.

![Screenshot](Screenshot.jpg?raw=true "Screenshot")

## Graphics Compostior

Forward Renderer must have `Bind Depth As Resoruce During Transparent Rendering` and `Bind Opaque As Resoruce During Transparent Rendering` checked.

## Material Setup

Instructions assume that a material of type `PBR Material: Metalness` is used.

### Surface

* Set normal map to shader, set source to `WaterNormalMap`
* In composition nodes, set speed to a nice like `x: 0.02, y: 0.02`, this controls the scrolling speed. Set Texture to the normal map you want to used
* Disable `Scale & Offset` and `Reconstruct Z`
* Normal maps must be compressed, if not then modify the source of `WaterNormalMap` to not reconstruct the z axis

### Micro Surface / Gloss map

Set to a high value

### Diffuse

Pick a color

### Specular / Metalness

Set to 0

### Emissive

Set to `Water Emissive Map`. Most of the water shading is calculated in this custom feature. Set the caustics texture to something, the rest of the options should have sane default.

### Transparency

Set to blend with alpha = 1, this is just to force rendering of the material to happen in the transparent stage

## Mesh

Use a plane with whatever UV scale works for your scene. 
