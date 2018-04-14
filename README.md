# FehDb

A back-end RESTful database and API modeling the characters and related entities in the mobile game Fire Emblem Heroes (FEH).

## What Does This API Cover?

The eventual goal is to cover the following (**bold** entries are implemented, _italics_ are in process):

* **Weapons**
* _Passives_
* Assists
* Characters and their stats
* Default ownership of weapons, passives, and assists
* Upgrades and ownership of those upgrades for weapons, passives, and assists

In addition, any entity that has an image associated with it will also be able to return a URL (or URI) to that image from a CDN (The default implementation are for my personal URIs for my personal CDN and would need to be modified for individual use).

## Security Notice

I am new to security. I tried to make sure the security was simple and followed what I had researched closely. That being said, I do wish to take it seriously and would welcome any advice on how to improve how its implemented or urgent warnings for any flaws in my implementation.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development.

### Prerequisites

The project was created using Visual Studio 2017 Version 15.3.4.

### Instructions for Building

Pull the project and open the solution in Visual Studio. It should be ready for compilation out of the box.

### Instructions for Deploying

Because the project involves some basic user security to allow updating during runtime, it is recommended you edit 'secrets.json' (located in _FehDb.API_) and change the secret keys to something long and unique.

## Authors

* **Kyle Domaszewicz** - [kdomasze](https://github.com/kdomasze)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
