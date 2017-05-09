# Booma.Library

Booma.Library is a repository containing all the dll libraries for the Booma project. This is a repo that was created by merging approximately 10 other [Booma Nation](www.github.com/BoomaNation) repos. It contains anything from general/common libraries down to specific client/server libraries like the Instance client/server project.

## Setup

To use this project you'll first need a couple of things:

* Visual Studio 2017 (earlier versions will likely be incompatible)
* Add Nuget Feed https://www.myget.org/F/hellokitty/api/v2 in VS (Options -> NuGet -> Package Sources)

## Current Dependency Graph

To help understand the relationships between the various csprojs it may be helpful for some to view and skim this dependency graph generated from the project itself.

It is not directly embed here because it is VERY LARGE but is only about 700kb. You can view it [here](http://i.imgur.com/gFSK3Uc.png).

## Deprecated Libraries

This section describes projects or libraries which are now deprecated, removed from the main project and/or have been renamed.

* Booma.Common.ServerSelection -> **Booma.ServerSelection.Common**

* Booma.Client.Common.IoCModules -> **Booma.Unity.IoCModules.Common**

* Booma.Client.Logging -> **Booma.Unity.Logging.Common**

* Booma.Client.Network.Common -> **Booma.Unity.Network.Common**

* Booma.Client.Network.Common -> **Booma.Unity.Network.Client** + **Booma.Unity.Network.IoCModules.Client**

* Booma.Server.Network.Unity.Common -> **Booma.Unity.Network.Server** + **Booma.Unity.Network.IoCModules.Server**

* Booma.Client.ServerSelection.Authentication -> **Booma.ServerSelection.Client** + **Booma.ServerSelection.IoCModules.Client**

* Booma.Instance.Client.Handlers -> **Booma.Instance.Handlers.Client**

* Booma.Instance.Server.Handlers -> **Booma.Instance.Handlers.Server**


## Licensing

This project is protected under the GPL licensing agreement. It will be actively enforced.
