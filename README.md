# Booma.Client.Common

Booma is the code name for a networked ORPG/MMORPG being developed by the BoomaNation organization.

Booma.Client.Common is library for shared code between all client libraries.

## Setup

To use this project you'll first need a couple of things:
  - Visual Studio 2015

## Subprojects

This repo contains the following libraries that are commonly shared across all clients:

Logging: [Logging Directory](https://github.com/BoomaNation/Booma.Client.Common/tree/master/src/Booma.Client.Logging)

## Builds

Available on a Nuget Feed: https://www.myget.org/F/hellokitty/api/v2 [![hellokitty MyGet Build Status](https://www.myget.org/BuildSource/Badge/hellokitty?identifier=280ebec4-18cb-43d7-b389-0a03aa2371ed)](https://www.myget.org/)

##Tests

Testing of external Unity3D libraries is generally not possible. Refer to: https://github.com/HelloKitty/Testity as an incomplete solution to this important problem.

#### Linux/Mono - Unit Tests
||Debug x86|Debug x64|Release x86|Release x64|
|:--:|:--:|:--:|:--:|:--:|:--:|
|**master**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/BoomaNation/Booma.Client.Common.svg?branch=master)](https://travis-ci.org/BoomaNation/Booma.Client.Common) |
|**dev**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/BoomaNation/Booma.Client.Common.svg?branch=dev)](https://travis-ci.org/BoomaNation/Booma.Client.Common)|

#### Windows - Unit Tests

(Done locally)

##Licensing

This project is licensed under the GPL license which will be actively enforced.
