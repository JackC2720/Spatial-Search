# Efficient spatial searching using SQL server to query spatial data and find distance between points in large sets of data. 

This project wasn't originally meant to be shared so if you need any help getting it running give me a shout as the DB setup is a bit odd. 

Uses postcode.io to retrieve location data for poscodes and SQL server to store and query geography data. 

Currently, the limitation is the way I have built the front end, I didn't intend to use a big set of data to begin with so it has no pagination so when returning 1000s of results, the DB query runs very quickly but with lots of results (1000+) plotting the map with Leaflet.js and listing the results on the FE takes a few seconds and the page becomes slow. 

I plan to do some accurate timed test times for different queries to demonstrate the speed accurately but I don't plan to make improvements to the FE as this is only a proof of concept.