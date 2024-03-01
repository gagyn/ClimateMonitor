# Climate Monitor & Dashboard!

The purpose of the application is to collect temperature and humidity data from digital sensors using Raspberry Pi or ESP8266.

Collecting data is customisable - you can freely add/remove/manage sensors from which the data should be collected. You can also defined your own data collect frequency using *cron* or setting the schedule in settings via UI.

# Repository architecture

The repo contains two projects:
1. **Data collector**:
	* Supported devices: **Raspberry Pi**, **ESP8266**, *In future: Arduino*
	* Supported sensors: **DHT11**, **DS18B20**
2. **Dashboard**:
	* Based on **React** framework with **Typescript**
	* Backend handled using **ASP .NET Core 8**

## Project's aims

### Data collector
The main aim of **Climate Monitor** is to allow easily **collect data** about temperature and humidity in different places, both easily accessible (using Raspberry Pi) or remote places (using ESP8266) as well. Considering the fact, that the .NET API is **hosted online** the data collector can be **placed anywhere**, with the only requirements of **power** (at least from battery) and **internet connection** (may be wireless as well).

### Dashboard
But collecting the data doesn't give any value, when it's not presented in understandable and pretty way. This is what the **Dashboard** is for. It presents the collected data using **charts and tables**, also making the data be **easily exportable** to Excel format.
Using the application in your environment allows you to **collect and analyse** on a data around you.
