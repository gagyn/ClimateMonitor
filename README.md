# Climate Monitor & Dashboard

## 1. Introduction

### 1.1. Purpose of the application

The purpose of this application is to collect temperature and humidity data from digital sensors using Raspberry Pi or ESP8266.

Collecting data is customisable - you can freely add/remove/manage sensors from which the data should be collected. You can also defined your own data collect frequency using *cron* or setting the schedule in settings via UI.

### 1.2. Repository structure

This repository contains two projects:

1. **Data collector**:
    * Supported devices: **Raspberry Pi**, **ESP8266**, *In future: Arduino*
    * Supported sensors: **DHT11**, **DS18B20**
2. **Dashboard**:
    * Based on **React** framework with **Typescript**
    * Backend handled using **ASP .NET Core 8**

### 1.3. Projects' overview

#### Data collector

The main aim of **Climate Monitor** is to allow easily **collect data** about temperature and humidity in different places, both easily accessible (using Raspberry Pi) or remote places (using ESP8266) as well. Considering the fact, that the .NET API is **hosted online** the data collector can be **placed anywhere**, with the only requirements of **power** (at least from battery) and **internet connection** (may be wireless as well).

#### Dashboard

But collecting the data doesn't give any value, when it's not presented in understandable and pretty way. This is what the **Dashboard** is for. It presents the collected data using **charts and tables**, also making the data be **easily exportable** to Excel format.
Using the application in your environment allows you to **collect and analyse** on a data around you.

### 1.4. Running the project

1. First you need to have a running MSSQL database. You can use the command below to run it in a Docker container (regardless the OS you're on):

```powershell
docker run --name sql_database -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=yourStrong(!)Password' -p 1434:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

### 1.5. Running Data collector on sensor device

1. Pull the repository on a Raspberry PI.
2. Go to `src/ClimateMonitor.IoT/RaspberrySensor` path.
3. Run `python3 -m venv venv` and `source venv/bin/activate`.
4. Install required packages using `pip3 install -r requirements.txt`.
5. Run the script `python3 setup.py`.

### 1.6. [Optional] Run Data collector as background service on linux

1. Create .service file using `sudo nano /lib/systemd/system/climate-monitor.service`.
2. Put the following content:

```sh
[Unit]
Description=Climate Monitor Collector
After=multi-user.target

[Service]
Type=idle
ExecStart=/bin/sh -c 'cd /home/pi/ClimateMonitor/src/ClimateMonitor.IoT/RaspberrySensor && /home/pi/ClimateMonitor/src/$
[Install]
WantedBy=multi-user.target
```

>***Note:*** **`ExecStart`** may need to be adjusted in your case, depending on your current username and the path where you have the repo clonned locally.

3. Save and close the editor.
4. Set the permission on the file using `sudo chmod 644 /lib/systemd/system/climate-monitor.service`.
5. Restart systemd using `sudo systemctl daemon-reload`.
6. Start the background service using `sudo systemctl enable sample.service`.

## 2. Domain

### 2.0. Overview

Each user can have devices assigned to his account, which allows to manage them and see records read only by his own devices.
The system can have two types of users - 1. User and 2. Device. Each role is restricted to some endpoints.

### 2.1. Structure

![Domain structure](./docs/Domain.jpg)

### 2.1. Processes

#### 2.1.1. Adding new Device to User account

Each new device needs to be linked to registered user. The diagram below explains the process of adding new device to user's account.

![Process schema of adding new device to user account](./docs/Adding_new_device.jpg)

## 3. Infrastructure level

## 4. Integration between projects

### 4.1. Secure connection

IoT devices communicate over websockets with ASP.NET Core API (with SignalR). The connection is secure (HTTPS) and authenticated (Bearer token). The API's endpoints are secured via authorization as well, by checking the role assigned to the calling user - Device or User. The *User* role is allowed for example to view and manage sensors configuration and view data, while *Device* can download current configuration and upload reading record.

### 4.2. Core API

The API is a central point of the project. It collects and saves all the data from all connected sensors/devcies. Later it allows to access stored records. The dashboard project consumes the API to show user friendly charts.
