Efficient spatial searching using MS Sql spatial index and geography data type to index spatial data and query distance between points. 

Uses postcode.io to retrieve location data based on postcode. 

Currently tested a query with over 10000 UK postcodes in the database and still returning and then plotting results in under 1s. 

Currently, the limitation is the way I have built the front end as it has no pagination so when returning 1000s of results, the DB query runs almost instantaniously however plotting on the FE takes a few seconds and the page becomes laggy. 

I plan to do some accurate timed test times for different queries to demonstrate the speed accurately but I don't plan to make improvements to the FE as this is only a proof of concept. 
