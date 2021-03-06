﻿CREATE TABLE IF NOT EXISTS Users (
    UserId INTEGER PRIMARY KEY NOT NULL,
    UserName TEXT NOT NULL

);

CREATE TABLE IF NOT EXISTS Temperatures (
    MeasurementId INTEGER PRIMARY KEY NOT NULL,
    UserId INTEGER NOT NULL,
    Timestamp DATETIME NOT NULL,
    Voltage REAL NOT NULL,
    FOREIGN KEY(UserId) REFERENCES Users(UserId)
);