# Unit Conversion API

A simple, reliable API that converts numbers between different units – meters to feet, kilograms to pounds, Celsius to Fahrenheit, and more. Built with **ASP.NET Core 8** and designed to easily support hundreds of conversion types.

---

## What does it do?

You send a **POST request** with:
- `category` – `"Length"`, `"Weight"`, or `"Temperature"`
- `fromUnit` – the unit you have (e.g., `"meter"`, `"kilogram"`, `"celsius"`)
- `toUnit` – the unit you want (e.g., `"foot"`, `"pound"`, `"fahrenheit"`)
- `value` – the numeric value to convert

The API replies with the converted value.

### Example

**Request:**
```json
POST /api/conversion/convert
{
  "category": "Length",
  "fromUnit": "meter",
  "toUnit": "foot",
  "value": 10
}
Response:

json
{
  "category": "Length",
  "fromUnit": "meter",
  "toUnit": "foot",
  "originalValue": 10,
  "convertedValue": 32.8084
}
Supported units
Category	Units
Length	meter, kilometer, centimeter, millimeter, inch, foot, yard, mile
Weight	gram, kilogram, pound, ounce
Temperature	Celsius, Fahrenheit, Kelvin (also accepts c, f, k as shortcuts)
How to start the project
1. Prerequisites
.NET 8 SDK installed

2. Clone and build
bash
git clone <your-repo-url>
cd UnitConversionApi
dotnet restore
dotnet build
3. Run the API
bash
dotnet run --project src/UnitConversion.API
You will see:

text
Now listening on: http://localhost:5151
4. Test it
Open http://localhost:5151/swagger in your browser – interactive documentation.

Or use curl:

bash
curl -X POST "http://localhost:5151/api/conversion/convert" \
  -H "Content-Type: application/json" \
  -d '{"category":"Length","fromUnit":"meter","toUnit":"foot","value":10}'
5. Run unit tests
bash
dotnet test
How it works (explained simply)
Controller – receives your request and checks if it is valid (category not empty, value a number, etc.).

Service – looks at the category and picks the right converter (Length, Weight, or Temperature).

Converter – does the actual math using the correct formula.

Response – the converted value is sent back as JSON.

Design decisions
Concept	Why it matters
Strategy Pattern	Each unit category has its own converter. To add a new category (e.g., Volume), you just add one new converter class – no changes to existing code.
Dependency Injection	The service doesn't create converters itself; they are given to it. This makes testing and swapping easy.
Clean Architecture	The code is split into Domain, Application, Infrastructure, and API. Each part has one job.
Adding a new category (e.g., Volume)
Create VolumeConverter.cs in src/UnitConversion.Infrastructure/Converters/

Implement IUnitConverter (methods: Category, CanConvert, Convert)

Register it in src/UnitConversion.API/Extensions/ServiceCollectionExtensions.cs:

csharp
services.AddScoped<IUnitConverter, VolumeConverter>();
Done! No other files need to be touched.

Technologies used
.NET 8 – framework and runtime

ASP.NET Core Web API – building REST endpoints

Swagger / OpenAPI – automatic API documentation and testing page

xUnit – unit testing

Moq – mocking dependencies in tests

Clean Architecture – separation of concerns

Project structure (simplified)
text
UnitConversionApi/
├── src/
│   ├── UnitConversion.API/         # Controllers, middleware, startup
│   ├── UnitConversion.Application/ # Business logic, DTOs, interfaces
│   ├── UnitConversion.Domain/      # Enums, exceptions, core models
│   └── UnitConversion.Infrastructure/ # Concrete converters
├── tests/
│   └── UnitConversion.Tests/       # Unit tests
├── UnitConversionApi.sln
└── README.md
Error handling
Invalid requests return 400 Bad Request with a clear message:

json
{
  "error": "Cannot convert from 'meter' to 'celsius'."
}
Unexpected server errors return 500 Internal Server Error with a generic message (details are logged server-side).

License
MIT – free for personal and commercial use.

