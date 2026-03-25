# Project 2 - Authenticated API Automation Framework (C# + xUnit + RestSharp)

![CI](https://github.com/TF-Loza/project2-api-automation/actions/workflows/dotnet-tests.yml/badge.svg)

## Overview
This project is an API automation framework built in C# using xUnit and RestSharp.

It focuses on testing an authenticated e-commerce-style API and demonstrates:
- login/authentication testing
- token handling
- reusable API client design
- request/response models
- endpoint constants
- positive and negative API testing
- GitHub Actions CI

Public API used:
- DummyJSON

## Tech Stack
- C#
- .NET 8
- xUnit
- RestSharp
- GitHub Actions

## Project Structure
```text
Project2.ApiTests
├── Clients
│   ├── ApiClient.cs
│   └── AuthenticatedApiClient.cs
├── Config
│   ├── ConfigurationHelper.cs
│   └── TestSettings.cs
├── Constants
│   └── ApiEndpoints.cs
├── Helpers
│   └── JsonHelper.cs
├── Models
│   ├── Requests
│   │   └── LoginRequest.cs
│   └── Responses
│       ├── LoginResponse.cs
│       ├── ProductResponse.cs
│       ├── ProductsListResponse.cs
│       └── UserResponse.cs
├── Tests
│   ├── AuthTests.cs
│   ├── ProductTests.cs
│   ├── UserTests.cs
│   └── CartTests.cs
├── appsettings.json
└── Project2.ApiTests.csproj