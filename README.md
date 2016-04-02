# GladLive.Common

GladLive is network session service comparable to Xboxlive or Stream. 

GladLive.Common is shared code between GladLive network services on both client and server.

## GladLive Services

GladLive.ProxyLoadBalancer: https://github.com/GladLive/GladLive.ProxyLoadBalancer

GladLive.AuthenticationService: TBA

## Setup

To use this project you'll first need a couple of things:
  - Visual Studio 2015
  - Add Nuget Feed https://www.myget.org/F/hellokitty/api/v2 in VS (Options -> NuGet -> Package Sources)

## Builds

Available on a Nuget Feed: https://www.myget.org/F/hellokitty/api/v2 [![hellokitty MyGet Build Status](https://www.myget.org/BuildSource/Badge/hellokitty?identifier=803ea136-5799-45fa-abeb-6c5f5f3eb963)](https://www.myget.org/gallery/hellokitty)

##Tests

#### Linux/Mono - Unit Tests
||Debug x86|Debug x64|Release x86|Release x64|
|:--:|:--:|:--:|:--:|:--:|:--:|
|**master**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/GladLive/GladLive.Common.svg?branch=master)](https://travis-ci.org/HelloKitty/GladLive/GladLive.Common) |
|**dev**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/GladLive/GladLive.Common.svg?branch=dev)](https://travis-ci.org/GladLive/GladLive.Common)|

#### Windows - Unit Tests

(Done locally)

##Licensing

This project is licensed under the MIT license with the additional requirement of adding the GladLive splashscreen to your product.
