# FoodFacilities.Api
A .NET 7 Web Api to navigate mobile food vendor data.

This was coded as part of a technical assessment, done in about a day's worth of work.

# Prereqs
- Dotnet 7
- Docker (if you're going to dockerize)
- Swashbuckle (if you're doing any extras)

# Technologies
- Entity Framework Core for Data Seeding https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
- CsvHelper for CSV Parsing https://joshclose.github.io/CsvHelper/ (apache license + MS-PL license)
- Swashbuckle for Swagger Generation, Library Documentation, and general cross-language usefulness
- FakeItEasy to make Unit Testing...Easy!
# Running the Project

Option A: Just run it
- `dotnet run` should get you started without issue.

Option B: Docker
- `docker compose .` to build the dockerfile, and then make sure you deploy to your container properly! 
- If you can't deploy it from here, talk to me for some Docker lessons.

## Extras

1. Swashbuckle Code Generation
	- first, install the tool 
	  - `dotnet tool install --global Swashbuckle.AspnetCore.Cli`
	  - *if the tool stops working, do `dotnet tool restore`*
	  - clean and build so you have a .dll
	 - You can use `swagger tofile` after `dotnet build`ing the project to generate an OpenApi-consumable swagger.json*
	 	- ex: `swagger tofile --output swagger.json bin/Debug/net7.0/FoodFacilities.Api.dll v1`

*You can `dotnet test` to run the unit tests! Check PROBLEMS.5 to see why the unit tests look the way they do.*

# Technical Discussion

This API was coded following the instructions of **README_original-prompt.md** at the hands of an intelligent user.

This API services the data on the Mobile Food Facilities by allowing users to search for vendors by name, address, or lat/long. There were a few requests to include "status" as a field.

As part of beng an API, it required the following things:
1. Endpoints
	- .NET 7 was selected to show off the latest and greatest, linked with library generation so you don't have to recode classes between projects.
2. A database
	- Reference provided CSV DAL/Mobile_Food_Facility_Permit.csv for the data that was used to seed the database. This CSV contained extra fields not present on the original datasheet  (https://data.sfgov.org/Economy-and-Community/Mobile-Food-Facility-Permit/rqzj-sfat/data)

This project can be run as standard or with the included Dockerfile. 
	- Included is some code that references how you would use this API to generate a consumable swagger.json as part of the docker process, which is useful when you eventually update your API.

---

MobileFoodFacilitiesController services these endpoints in the following ways 
> 
1. SearchMobileFoodVendorsByNameAsync 
	> Search by name of applicant. Include optional filter on "Status" field.
	- This endpoint allows you to search for vendors by name. It is case insensitive and will return all vendors that contain the search term in their name.
	- The status option parameter is handled to allow you to filter by status. If you do not provide a status, it will return all vendors.
	- Typically case specificity on names is low, so we resort to lowercase-ness in case someone has a "DeMarc" or combination in their name.
		- My Take: Personally, I see little benefit in making it harder to access this data.
		- This was a personal choice to add to the endpoint, outside of the scope of the original request.
	- COMPROMISE: Tried to do it in a few lines with a ternary with FirstOrDefaultAsync, but chose to go with a more verbose approach to make it easier to read // get this done.
	- This endpoint isn't designed to service Autocomplete, you usually want a search index for that.
	
>
2. SearchMobileFoodVendorsByAddressAsync
	> Search by street name. The user should be able to type just part of the address. Example: Searching for "SAN" should return food trucks on "SANSOME ST"
	- This endpoint allows you to search for vendors by address. It is case insensitive and will return all vendors that contain the search term in their address.
	- This endpoint isn't designed to service Autocomplete, you usually want a search index for that.
		
3. SearchMobileFoodVendorsByLocationAsync
	> Given a latitude and longitude, the API should return the 5 nearest food trucks. By default, this should only return food trucks with status "APPROVED", but the user should be able to override this and search for all statuses.
		> - You can use any external services to help with this (e.g. Google Maps API).
	
	- This Endpoint allows you to search within an area of 0.05miles by 0.06miles, based on the provided Latitude and Longitude.
	- Instead of linking a whole extra dependency, I chose to filter the results out by latitude and longitude in sequence. 
		- For this dataset size, this is a small task. 
		- Using the hundredth of a degree of Lat/Long, we can get close realistically.
		- This endpoint could be configured to have a "scale" or "factor" we multiply onto the lat/long for a range. 
		- There are more sophisticated solutions to this and those should be sought out when real time accuracy is paramount.
		- 0.01 Latitude and Longitude gave accurate results when testing.
	- COMPROMISE: Same compromise with FirstOrDefault with Status, except it applies across all of these. There's a cleaner solution but the speed loss on this isn't bad.
	
4. GetAllMobileFoodVendorsAsync
	> PERSONAL ADDITION
	- Wanted an easy way to view the data for testing.
	- .Take is there because I don't like storing 300 expanded string entries in my DOM

5. Additional Notes
	- We append Async to all of these so when we generate the library, the consumer knows what's async
	- ProducesResponseType manages the responses nicely for the DOM usually

# Problems with the Implementation:
1. Security
	- Didn't spend time writing up a header to auth into the controller because this is public information
	- no delimiting on a user writing a bot to hit us, so hope you have Cloudflare or consider adding an Auth Token (OAuth2 is not hard)
2. Servicing Autocomplete
	- A lot of these endpoints look like they're going to be in someone's `<input>` and we'll have to eat a request for every character input. You won't want to tie up the DB that way, should use a Search Index.
3. Data Model
	- Designed MobileFoodFacility.cs to handle this, but the data has a lot of inconsistencies in it. Check out the "dev note"s. We result to string a lot.
4. Versioning
	- Basic versioning was added for namespaces & swagger documentation, can be managed in other ways
5. Unit Testing
	- Could not consume the local csv correctly, so unit tests do very little right now other than show objects can be created.
	- a Todo was added
	- Did not spend extra time scaffolding integration/unit testing beyond a basic positive/negative test
6. Naming
	- Mobile Food Facility is a cumbersome class name
	- Named the project FoodFacilities while it's MobileFoodFacilities. Oops. Not a terrible refactor.
7. Git History	
	- as a Dev, I don't compartmentalize my commits & instead rely on PR squashing with a branch. Locally, this get a little messy.
 
# Time Spent:
- Git History should reveal the general time spent.
	- ~1 hour on setup and Model defining
	- ~2 hours to define controller and get entityframeworkcore to behave with the csv
	- ~1 hour functional testing to discover bad data entries
	- ~1.5 hours writing this Readme!
	- ~2 hours on Unit Testing with this weird local DbContext



# Response to Questions
1. What would I have done different if I had spent more time on this?
	- Actually stood up a database instead of learning how to load up a csv in the new entity framework's language
	- implement some authorization so the container doesn't explode from requests
2. What are the trade-offs I have made?
	- a trade-off on the User Name search is that, if looking for two users of the same name but different capitalization, you will struggle to get the correct one. I don't believe this was a use-case.
	- in my haste, may have left a spare library or two around or a dangling using case. No ReSharper to do my job for me haha
3. What did I leave out?
	- meaningful unit testing, as getting a fake database to work is useful time on a robust accurate system but not here.
4. What would I solve to scale for a large number of users?
	- Implement a Search Index (Azure has a decent one but the OData query language is unique) for autocomplete
	- This is an API so you can have a site that services the users and contacts the API for that data (MICROSERVICE ARCHITECTURE)

# Final Comments
Reach out if you have any questions or business inquiries!