# Soso.UI

## Introduction
Soso.UI is mainly a code first animation library. However, I do plan to add all sorts
of utilities in the future. 

## Useage
### Soso.UI.Animation
Use it as an extension for your transforms:
```
transform.AnimateLocalScale(Vector3.one, 0.25f, EASEING.EaseOutBounce);
```
They can also be awaited in an async method:
```
await transform.AnimateLocalScale(Vector3.one, 0.25f, EASEING.Cubic).GetAwaiter();
```
Or chained together:
```
transform.AnimateLocalScale(Vector3.one, 0.25f, EASEING.EaseOutBounce)
    .Then(transform.AnimateLocalScale(Vector3.zero, 0.25f, EASEING.Lerp));
```

It's easy to set up your own custom operations as well. 
Simply extend either SosoOperation or implement ISosoOperation. If implementing
ISosoOperation, you must use SosoAwaitable. Check out SosoOperation.

## Installation
Install via UPM by going to 'Package Manager' -> '+' -> 'Add package from git URL...' from the Unity Editor

Note: use '?path=package'

Example: 'https://github.com/mkyprice/Soso.UI.git?path=package'
