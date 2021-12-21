## Table of contents
* [General info](#general-info)
* [Komodo Cafe](#komodo-cafe)
* [Komodo Claims](#komodo-claims)
* [Komodo Green Plan](#komodo-green-plan)

## General info
This projects consists of 3 challenges.
	
## Komodo Cafe
A simple menu system for a cafe.

Allows the user to enter the following information describing a meal:

* Name
* Description
* List of ingredients (list items must be separated by a comma [ , ] to be properly parsed into the list)
* Price

Meal number is automatically assigned as it is tied to the item's index in the list.

## Komodo Claims
A simple system that allows the user to see a list of insurance claims.  It uses the `Queue` data structure to allow the user access to the claim at the head of the queue.

When creating a new claim the user must enter,

* A Claim ID
* Claim Type
* Description of the incident
* A valuation of the damage
* Date the incident occured
* Date the claim was filed

Validity of the claim will be calculated automatically.  Only claims filed within thirty (30) days of the incident will be considered valid.

## Komodo Green Plan
A simple database to track vehicle data.

A user may,

* List Vehicles
* Add Vehicles to the database
* Update Vehicle data
* Delete a vehicle from the database

Currently only allows for tracking of Make, Model, and Fuel Type.

ID is assigned automatically when a vehicle is added to the database.