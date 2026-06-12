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
```

**Response:**

```json
{
  "category": "Length",
  "fromUnit": "meter",
  "toUnit": "foot",
  "originalValue": 10,
  "convertedValue": 32.8084
}
```

---

## Supported Units

| Category    | Units                                                                 |
|-------------|-----------------------------------------------------------------------|
| Length      | meter, kilometer, centimeter, millimeter, inch, foot, yard, mile     |
| Weight      | gram, kilogram, pound, ounce                                         |
| Temperature | Celsius, Fahrenheit, Kelvin (also accepts `c`, `f`, `k` as shortcuts)|

---

## How to Start the Project

### 1. Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) installed

### 2. Clone and Build

```bash
git clone <your-repo-url>
cd UnitConversionApi
dotnet restore
dotnet build
```

### 3. Run the API

```bash
dotnet run --project src/UnitConversion.API
```

### 4. Test It

Open **http://localhost:5151/swagger** in your browser for interactive documentation.

Or use `curl`:

```bash
curl -X POST "http://localhost:5151/api/conversion/convert" \
  -H "Content-Type: application/json" \
  -d '{"category":"Length","fromUnit":"meter","toUnit":"foot","value":10}'
```

### 5. Run Unit Tests

```bash
dotnet test
```

---

## How It Works

1. **Controller** – Receives your request and validates it (category not empty, value is a number, etc.).
2. **Service** – Looks at the category and picks the right converter (Length, Weight, or Temperature).
3. **Converter** – Does the actual math using the correct formula.
4. **Response** – The converted value is sent back as JSON.

---

## Design Decisions

| Concept               | Why It Matters                                                                                                       |
|-----------------------|----------------------------------------------------------------------------------------------------------------------|
| Strategy Pattern      | Each unit category has its own converter. To add a new category (e.g., Volume), you just add one new converter class – no changes to existing code. |
| Dependency Injection  | The service doesn't create converters itself; they are injected. This makes testing and swapping easy.              |
| Clean Architecture    | Code is split into Domain, Application, Infrastructure, and API. Each part has one job.                            |

---

## Adding a New Category (e.g., Volume)

1. Create `VolumeConverter.cs` in `src/UnitConversion.Infrastructure/Converters/`
2. Implement `IUnitConverter` (methods: `Category`, `CanConvert`, `Convert`)
3. Register it in `src/UnitConversion.API/Extensions/ServiceCollectionExtensions.cs`:

```csharp
services.AddScoped<IUnitConverter, VolumeConverter>();
```

Done! No other files need to be touched.

---

## Technologies Used

- **[.NET 8](https://dotnet.microsoft.com/)** – Framework and runtime
- **ASP.NET Core Web API** – Building REST endpoints
- **Swagger / OpenAPI** – Automatic API documentation and testing page
- **xUnit** – Unit testing
- **Moq** – Mocking dependencies in tests
- **Clean Architecture** – Separation of concerns

---

## Project Structure

UnitConversionApi/

├── src/

│   ├── UnitConversion.API/            # Controllers, middleware, startup

│   ├── UnitConversion.Application/    # Business logic, DTOs, interfaces

│   ├── UnitConversion.Domain/         # Enums, exceptions, core models

│   └── UnitConversion.Infrastructure/ # Concrete converters

├── tests/

│   └── UnitConversion.Tests/          # Unit tests

├── UnitConversionApi.sln

└── README.md

---

## Error Handling

**Invalid requests** return `400 Bad Request` with a clear message:

```json
{
  "error": "Cannot convert from 'meter' to 'celsius'."
}
```

**Unexpected server errors** return `500 Internal Server Error` with a generic message (details are logged server-side).

---

## License

MIT – free for personal and commercial use.