# GaiaOnline.Unity.Library

![Showcase](http://i.imgur.com/waYynnx.gif "Demo of some components")

Library for interacting with the GaiaOnline API, endpoints and content inside of Unity3D. Provides Unity3D compatible compnents for rendering, animating, networking and more to bring GaiaOnline to life in Unity3D.

## How?

It relies on [TypeSafe.Http.Net](https://github.com/HelloKitty/TypeSafe.Http.Net), a fantastic library I wrote, for the HTTP networking required for querying the GaiaOnline API. This is done in such a way that does not differ from browsers nor other addons like BetterGaia. No reverse engineering etc. was required in any form to do this.

## Setup

You will need:

* Visual Studio 2017
* Unity2017 with .NET 4.6 enabled
* Add Nuget feed: https://www.myget.org/F/hellokitty/api/v3/index.json

## Builds

Clone and run build.bat and import the compiled assemblies in *build/release* into a Unity3D project.

TODO: nuget

TODO: release link

## License

[AGPL](https://www.gnu.org/licenses/agpl-3.0.en.html). If you do not understand the AGPL then the simpliest way to follow it is to simply make available your source code for anything you do with this project. This, unlike GPL, covers running this software on a server or as a network service.
